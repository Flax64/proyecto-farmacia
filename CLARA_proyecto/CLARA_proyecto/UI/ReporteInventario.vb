Imports System.Drawing.Printing
Imports System.Net.Http
Imports System.Text.Json
Imports CLARA_proyecto.Models

Public Class ReporteInventario
    Private clienteHttp As HttpClient

    '  1. LA MEMORIA DEL BUSCADOR: Aquí guardaremos el inventario original
    Private listaInventarioOriginal As List(Of InventarioDTO)
    '  Sirve para recordar en qué fila nos quedamos si el PDF necesita varias páginas
    Private filaImpresionActual As Integer = 0

    Public Sub New()
        InitializeComponent()
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
        clienteHttp.BaseAddress = New Uri("http://54.89.200.65:5133/")
    End Sub

    Private Async Sub Reporte_Inventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgv_Inventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_Inventario.AllowUserToAddRows = False

        Await CargarInventario()
    End Sub

    Private Async Function CargarInventario() As Task
        Try
            Dim respuesta = Await clienteHttp.GetAsync("api/Reportes/inventario")

            If respuesta.IsSuccessStatusCode Then
                Dim json As String = Await respuesta.Content.ReadAsStringAsync()
                Dim opciones = New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}

                '  2. ALMACENAMOS LA LISTA ORIGINAL
                listaInventarioOriginal = JsonSerializer.Deserialize(Of List(Of InventarioDTO))(json, opciones)

                ' Se la asignamos a la tabla
                dgv_Inventario.DataSource = listaInventarioOriginal
                FormatearColumnas()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al conectar con el servidor: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    '  3. EL BUSCADOR PROFESIONAL EN TIEMPO REAL
    Private Sub txt_Buscar_TextChanged(sender As Object, e As EventArgs) Handles txt_Buscar.TextChanged
        If listaInventarioOriginal IsNot Nothing Then
            Dim textoBusqueda As String = txt_Buscar.Text.Trim().ToLower()

            If textoBusqueda = "" Then
                ' Si está en blanco, restauramos todo el inventario
                dgv_Inventario.DataSource = listaInventarioOriginal
            Else
                ' Filtramos buscando coincidencias en el nombre del medicamento
                Dim listaFiltrada = listaInventarioOriginal.Where(Function(x) x.Nombre.ToLower().Contains(textoBusqueda)).ToList()
                dgv_Inventario.DataSource = listaFiltrada
            End If
            FormatearColumnas()
        End If
    End Sub

    ' Mantiene los colores rojos vivos aunque el usuario filtre o busque
    Private Sub dgv_Inventario_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_Inventario.DataBindingComplete
        For Each row As DataGridViewRow In dgv_Inventario.Rows
            If row.Cells("Alerta").Value IsNot Nothing AndAlso row.Cells("Alerta").Value.ToString() = "REABASTECER" Then
                row.DefaultCellStyle.BackColor = Color.LightCoral
                row.DefaultCellStyle.ForeColor = Color.White
            End If
        Next
    End Sub

    '  4. DESCARGA DIRECTA A PDF SILENCIOSA (MODO FANTASMA)
    Private Sub btn_Descargar_Click(sender As Object, e As EventArgs) Handles btn_Descargar.Click
        Dim ventanaGuardar As New SaveFileDialog()
        ventanaGuardar.Filter = "Archivos PDF (*.pdf)|*.pdf"
        ventanaGuardar.Title = "Guardar Reporte de Inventario"
        ventanaGuardar.FileName = "Reporte_Inventario_" & DateTime.Now.ToString("yyyyMMdd") & ".pdf"

        If ventanaGuardar.ShowDialog() = DialogResult.OK Then
            Try
                ' 1. Creamos un documento nuevo directo en la RAM
                Dim documentoVirtual As New Printing.PrintDocument()

                '  LA LÍNEA MÁGICA: Apaga cualquier cuadro de diálogo de Windows
                documentoVirtual.PrintController = New Printing.StandardPrintController()

                ' 2. Le inyectamos el motor de PDF
                documentoVirtual.PrinterSettings.PrinterName = "Microsoft Print to PDF"
                documentoVirtual.PrinterSettings.PrintToFile = True
                documentoVirtual.PrinterSettings.PrintFileName = ventanaGuardar.FileName
                documentoVirtual.DefaultPageSettings.Landscape = True

                ' 3. Conectamos el documento con nuestra función de dibujo
                AddHandler documentoVirtual.PrintPage, AddressOf DibujarTablaEnPDF

                '  Reiniciamos el contador antes de empezar
                filaImpresionActual = 0

                ' 4. Disparamos la creación del PDF
                documentoVirtual.Print()

                MessageBox.Show("El PDF se guardó correctamente.", "Descarga Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al generar el PDF. Asegúrate de tener 'Microsoft Print to PDF' activado. Detalle: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub DibujarTablaEnPDF(sender As Object, e As Printing.PrintPageEventArgs)
        ' 1. Configuración de tipografías y colores
        Dim fuenteTitulo As New Font("Segoe UI", 16, FontStyle.Bold)
        Dim fuenteFecha As New Font("Segoe UI", 10, FontStyle.Italic) '  Nueva fuente para la fecha
        Dim fuenteEncabezados As New Font("Segoe UI", 11, FontStyle.Bold)
        Dim fuenteNormal As New Font("Segoe UI", 10, FontStyle.Regular)
        Dim colorTexto As New SolidBrush(Color.Black)
        Dim colorPeligro As New SolidBrush(Color.Red)

        ' 2. Coordenadas de inicio y columnas (Eje X)
        Dim Y As Integer = 50 ' Margen superior
        Dim colMedicamento As Integer = 50
        Dim colPrecio As Integer = 450
        Dim colStock As Integer = 600
        Dim colAlerta As Integer = 750

        ' 3. Dibujamos el Título y la Fecha (¡Solo si estamos en la primera página!)
        If filaImpresionActual = 0 Then
            e.Graphics.DrawString("Reporte de Inventario - Sistema CLARA", fuenteTitulo, colorTexto, colMedicamento, Y)
            Y += 30

            '  Extraemos la fecha y hora exacta de este momento
            Dim fechaActual As String = "Fecha de emisión: " & DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")
            e.Graphics.DrawString(fechaActual, fuenteFecha, colorTexto, colMedicamento, Y)
            Y += 40
        End If

        ' 4. Dibujamos los Encabezados (Estos sí se imprimen al inicio de cada hoja)
        e.Graphics.DrawString("Medicamento", fuenteEncabezados, colorTexto, colMedicamento, Y)
        e.Graphics.DrawString("Precio", fuenteEncabezados, colorTexto, colPrecio, Y)
        e.Graphics.DrawString("Stock", fuenteEncabezados, colorTexto, colStock, Y)
        e.Graphics.DrawString("Alerta", fuenteEncabezados, colorTexto, colAlerta, Y)
        Y += 25
        e.Graphics.DrawLine(Pens.Black, colMedicamento, Y, colAlerta + 100, Y) ' Línea separadora
        Y += 15

        ' 5. Empezamos a dibujar los datos fila por fila
        While filaImpresionActual < dgv_Inventario.Rows.Count
            Dim row As DataGridViewRow = dgv_Inventario.Rows(filaImpresionActual)

            ' Extraemos los textos de la fila actual
            Dim nombre As String = row.Cells("Nombre").Value.ToString()
            Dim precio As String = Convert.ToDecimal(row.Cells("Precio").Value).ToString("C2")
            Dim stock As String = row.Cells("Stock").Value.ToString()
            Dim alerta As String = row.Cells("Alerta").Value.ToString()

            ' Si la alerta es "REABASTECER", cambiamos la "tinta" a rojo
            Dim pincelActual As Brush = If(alerta = "REABASTECER", colorPeligro, colorTexto)

            ' Imprimimos los textos en sus columnas
            e.Graphics.DrawString(nombre, fuenteNormal, pincelActual, colMedicamento, Y)
            e.Graphics.DrawString(precio, fuenteNormal, pincelActual, colPrecio, Y)
            e.Graphics.DrawString(stock, fuenteNormal, pincelActual, colStock, Y)
            e.Graphics.DrawString(alerta, fuenteNormal, pincelActual, colAlerta, Y)

            Y += 25 ' Bajamos un renglón para el siguiente medicamento
            filaImpresionActual += 1

            ' 6. Lógica de paginación automática
            ' Si llegamos al final de la hoja (margen inferior), pedimos otra página
            If Y > e.MarginBounds.Bottom Then
                e.HasMorePages = True
                Return ' Salimos del método temporalmente para que Windows cambie de hoja
            End If
        End While

        ' Si ya terminamos con todas las filas, le decimos a Windows que ya no hay más páginas
        e.HasMorePages = False
    End Sub

    Private Sub FormatearColumnas()
        If dgv_Inventario.Columns.Count > 0 Then
            ' 1. Ocultamos las columnas técnicas o vacías
            If dgv_Inventario.Columns.Contains("Id") Then dgv_Inventario.Columns("Id").Visible = False
            If dgv_Inventario.Columns.Contains("Concentracion") Then dgv_Inventario.Columns("Concentracion").Visible = False

            ' 2. Formateamos y ajustamos los tamaños (FillWeight)
            If dgv_Inventario.Columns.Contains("Nombre") Then
                dgv_Inventario.Columns("Nombre").HeaderText = "Medicamento"
                ' Le damos más del doble de espacio que a las demás
                dgv_Inventario.Columns("Nombre").FillWeight = 250
            End If

            If dgv_Inventario.Columns.Contains("Precio") Then
                dgv_Inventario.Columns("Precio").DefaultCellStyle.Format = "C2" ' Formato de moneda
                dgv_Inventario.Columns("Precio").FillWeight = 80
            End If

            If dgv_Inventario.Columns.Contains("Stock") Then dgv_Inventario.Columns("Stock").FillWeight = 80
            If dgv_Inventario.Columns.Contains("Alerta") Then dgv_Inventario.Columns("Alerta").FillWeight = 100
        End If
    End Sub
End Class