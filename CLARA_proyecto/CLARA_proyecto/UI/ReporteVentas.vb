Imports System.Net.Http
Imports System.Text.Json
Imports System.Drawing.Printing
Imports CLARA_proyecto.Models

Public Class ReporteVentas
    Private clienteHttp As HttpClient
    ' Sirve para recordar en qué fila nos quedamos si el PDF necesita varias páginas
    Private filaImpresionActual As Integer = 0

    '  1. EL CONSTRUCTOR FALTANTE (¡Súper importante para que no explote!)
    Public Sub New()
        InitializeComponent()
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
        clienteHttp.BaseAddress = New Uri("http://54.89.200.65:5133/")
    End Sub

    '  2. PREPARAMOS LA TABLA AL ABRIR LA PANTALLA
    Private Sub ReporteVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dvg_Ventas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dvg_Ventas.AllowUserToAddRows = False
        dvg_Ventas.RowHeadersVisible = False
        dvg_Ventas.BackgroundColor = Color.White
    End Sub

    Private Async Sub btn_Generar_Click(sender As Object, e As EventArgs) Handles btn_generar.Click
        '  1. VALIDACIÓN DE FECHAS (Frontend)
        ' Usamos .Date para comparar solo los días y ignorar si la hora es diferente
        If dtp_inicio.Value.Date > dtp_final.Value.Date Then
            MessageBox.Show("La fecha de inicio no puede ser mayor a la fecha final. Por favor, corrige el rango de fechas.", "Fechas Incorrectas", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return ' Detenemos el código aquí, no avanza a la API
        End If

        '  2. BLOQUEO TEMPORAL (Evita que el usuario sature la API)
        btn_generar.Enabled = False
        btn_generar.Text = "Generando..."

        Try
            ' 3. Obtener fechas formateadas de los controles
            Dim fechaInicio As String = dtp_inicio.Value.ToString("yyyy-MM-dd")
            Dim fechaFin As String = dtp_final.Value.ToString("yyyy-MM-dd")

            ' 4. Llamar a la API con los parámetros de fecha
            Dim url As String = $"api/Reportes/ventas?inicio={fechaInicio}&fin={fechaFin}"
            Dim respuesta = Await clienteHttp.GetAsync(url)

            If respuesta.IsSuccessStatusCode Then
                Dim json As String = Await respuesta.Content.ReadAsStringAsync()
                Dim listaVentas = JsonSerializer.Deserialize(Of List(Of VentaReporteDTO))(json,
                    New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})

                ' 5. Llenar la tabla
                dvg_Ventas.DataSource = listaVentas

                ' FORZAR EL ORDEN DE LAS COLUMNAS MANUALMENTE
                If dvg_Ventas.Columns.Contains("Vendedor") AndAlso dvg_Ventas.Columns.Contains("Total") Then
                    dvg_Ventas.Columns("Vendedor").DisplayIndex = 4 ' Penúltima posición
                    dvg_Ventas.Columns("Total").DisplayIndex = 5    ' Última posición
                End If

                ' Le damos formato de dinero a la columna de la tabla automáticamente
                If dvg_Ventas.Columns.Contains("Total") Then
                    dvg_Ventas.Columns("Total").DefaultCellStyle.Format = "C2"
                End If

                ' Le damos formato de dinero a la columna de la tabla automáticamente
                If dvg_Ventas.Columns.Contains("Total") Then
                    dvg_Ventas.Columns("Total").DefaultCellStyle.Format = "C2"
                End If

                ' 6. Limpiar nombres y calcular el Gran Total acumulado
                Dim acumulado As Decimal = 0
                If listaVentas IsNot Nothing Then
                    For Each v In listaVentas
                        ' TRUCO DE LIMPIEZA: Si el nombre tiene " (Receta", lo cortamos desde ahí
                        If v.Cliente.Contains(" (Receta") Then
                            v.Cliente = v.Cliente.Substring(0, v.Cliente.IndexOf(" (Receta")).Trim()
                        End If

                        acumulado += v.Total
                    Next
                End If

                ' Usamos "C2" que automáticamente le pone el signo de $ y dos decimales
                lbl_granTotal.Text = "Total en Caja: " & acumulado.ToString("C2")
            Else
                MessageBox.Show("No se pudo obtener el reporte del servidor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error al generar reporte: " & ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '  7. PASE LO QUE PASE, DESBLOQUEAMOS EL BOTÓN AL TERMINAR
            btn_generar.Enabled = True
            btn_generar.Text = "GENERAR REPORTE"
        End Try
    End Sub

    Private Sub btn_Descargar_Click(sender As Object, e As EventArgs) Handles btn_Descargar.Click
        ' 1. Validar que haya datos en la tabla antes de imprimir
        If dvg_Ventas.Rows.Count = 0 Then
            MessageBox.Show("No hay datos para generar el PDF. Genera el reporte primero.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim ventanaGuardar As New SaveFileDialog()
        ventanaGuardar.Filter = "Archivos PDF (*.pdf)|*.pdf"
        ventanaGuardar.Title = "Guardar Reporte de Ventas"
        ventanaGuardar.FileName = "Reporte_Ventas_" & DateTime.Now.ToString("yyyyMMdd") & ".pdf"

        If ventanaGuardar.ShowDialog() = DialogResult.OK Then
            Try
                Dim documentoVirtual As New Printing.PrintDocument()

                ' APAGA cualquier cuadro de diálogo de Windows (Modo fantasma)
                documentoVirtual.PrintController = New Printing.StandardPrintController()

                documentoVirtual.PrinterSettings.PrinterName = "Microsoft Print to PDF"
                documentoVirtual.PrinterSettings.PrintToFile = True
                documentoVirtual.PrinterSettings.PrintFileName = ventanaGuardar.FileName
                documentoVirtual.DefaultPageSettings.Landscape = True ' Acostado para que quepan más columnas

                ' Conectamos el documento con nuestra función de dibujo
                AddHandler documentoVirtual.PrintPage, AddressOf DibujarTablaEnPDF

                ' Reiniciamos el contador antes de empezar
                filaImpresionActual = 0

                ' Disparamos la creación del PDF
                documentoVirtual.Print()

                MessageBox.Show("El reporte de ventas se guardó correctamente.", "Descarga Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al generar el PDF. Asegúrate de tener 'Microsoft Print to PDF' activado. Detalle: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub DibujarTablaEnPDF(sender As Object, e As Printing.PrintPageEventArgs)
        ' 1. Configuración de tipografías y colores
        Dim fuenteTitulo As New Font("Segoe UI", 16, FontStyle.Bold)
        Dim fuenteFecha As New Font("Segoe UI", 10, FontStyle.Italic)
        Dim fuenteEncabezados As New Font("Segoe UI", 11, FontStyle.Bold)
        Dim fuenteNormal As New Font("Segoe UI", 10, FontStyle.Regular)
        Dim fuenteTotal As New Font("Segoe UI", 12, FontStyle.Bold)

        Dim colorTexto As New SolidBrush(Color.Black)

        ' 2. Coordenadas de inicio y columnas (Eje X) perfeccionadas
        Dim Y As Integer = 50
        Dim colFolio As Integer = 50
        Dim colFecha As Integer = 120
        Dim colHora As Integer = 230
        Dim colCliente As Integer = 330
        Dim colVendedor As Integer = 580 ' Le damos buen espacio al Cliente
        Dim colTotal As Integer = 850    ' El Total se va a la última posición derecha

        ' 3. Dibujamos el Título y las Fechas (Solo en la primera página)
        If filaImpresionActual = 0 Then
            e.Graphics.DrawString("Reporte de Ventas - Sistema CLARA", fuenteTitulo, colorTexto, colFolio, Y)
            Y += 30

            Dim rangoFechas As String = $"Periodo consultado: {dtp_inicio.Value.ToString("dd/MM/yyyy")} al {dtp_final.Value.ToString("dd/MM/yyyy")}"
            e.Graphics.DrawString(rangoFechas, fuenteFecha, colorTexto, colFolio, Y)
            Y += 20

            Dim fechaEmision As String = "Fecha de emisión: " & DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")
            e.Graphics.DrawString(fechaEmision, fuenteFecha, colorTexto, colFolio, Y)
            Y += 40
        End If

        ' 4. Dibujamos los Encabezados (En cada página nueva)
        e.Graphics.DrawString("Folio", fuenteEncabezados, colorTexto, colFolio, Y)
        e.Graphics.DrawString("Fecha", fuenteEncabezados, colorTexto, colFecha, Y)
        e.Graphics.DrawString("Hora", fuenteEncabezados, colorTexto, colHora, Y)
        e.Graphics.DrawString("Cliente", fuenteEncabezados, colorTexto, colCliente, Y)
        e.Graphics.DrawString("Vendedor", fuenteEncabezados, colorTexto, colVendedor, Y)
        e.Graphics.DrawString("Total", fuenteEncabezados, colorTexto, colTotal, Y) ' ✨ Total al final
        Y += 25
        e.Graphics.DrawLine(Pens.Black, colFolio, Y, colTotal + 100, Y)
        Y += 15

        ' 5. Empezamos a dibujar los datos fila por fila
        While filaImpresionActual < dvg_Ventas.Rows.Count
            Dim row As DataGridViewRow = dvg_Ventas.Rows(filaImpresionActual)

            ' Extracción segura
            Dim folio As String = If(row.Cells("Folio").Value, "").ToString()
            Dim fecha As String = If(row.Cells("Fecha").Value, "").ToString()
            Dim hora As String = If(row.Cells("Hora").Value, "").ToString()
            Dim cliente As String = If(row.Cells("Cliente").Value, "").ToString()
            Dim vendedor As String = If(row.Cells("Vendedor").Value, "").ToString() ' Extraer vendedor de la tabla
            Dim total As String = Convert.ToDecimal(row.Cells("Total").Value).ToString("C2")

            ' Recortar nombres para que no se encimen
            If cliente.Length > 28 Then cliente = cliente.Substring(0, 25) & "..."
            If vendedor.Length > 28 Then vendedor = vendedor.Substring(0, 25) & "..."

            ' Imprimimos (Asegúrate de que este bloque esté en este orden)
            e.Graphics.DrawString(folio, fuenteNormal, colorTexto, colFolio, Y)
            e.Graphics.DrawString(fecha, fuenteNormal, colorTexto, colFecha, Y)
            e.Graphics.DrawString(hora, fuenteNormal, colorTexto, colHora, Y)
            e.Graphics.DrawString(cliente, fuenteNormal, colorTexto, colCliente, Y)
            e.Graphics.DrawString(vendedor, fuenteNormal, colorTexto, colVendedor, Y)
            e.Graphics.DrawString(total, fuenteNormal, colorTexto, colTotal, Y)

            Y += 25
            filaImpresionActual += 1

            ' 6. Paginación
            If Y > e.MarginBounds.Bottom - 40 Then
                e.HasMorePages = True
                Return
            End If
        End While

        ' 7. IMPRIMIR EL GRAN TOTAL AL FINAL DE TODO
        Y += 20
        e.Graphics.DrawLine(Pens.Black, colFolio, Y, colTotal + 100, Y)
        Y += 10
        e.Graphics.DrawString(lbl_granTotal.Text, fuenteTotal, colorTexto, colTotal - 150, Y)

        e.HasMorePages = False
    End Sub
End Class