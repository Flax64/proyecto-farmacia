Imports System.Net.Http
Imports System.Text.Json
Imports System.Text
Imports System.ComponentModel
Imports System.Drawing.Printing

Public Class Reportes_Expedientes_
    Private clienteHttp As HttpClient
    ' Usamos una lista vinculada para el historial
    Private historialPaciente As New BindingList(Of FilaHistorial)
    Private listaPacientesOriginal As List(Of ExpedientePaciente)
    Private filaImpresionActual As Integer = 0 ' Para controlar las páginas

    Public Sub New()
        InitializeComponent()
        ' Configuración del Cliente HTTP
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
        ' Asegúrate de que este puerto coincida con tu Backend en C#
        clienteHttp.BaseAddress = New Uri("http://localhost:5133/")
    End Sub

    Private Async Sub Reportes_Expedientes__Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Ajustamos ambas tablas para que no se corte el texto
        dgv_Expedientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_Historial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Encendemos el Autocompletado del buscador
        tb_Buscar.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tb_Buscar.AutoCompleteSource = AutoCompleteSource.CustomSource

        Await CargarExpedientes()
    End Sub

    ' --- CARGA DE LISTA GENERAL DE PACIENTES ---
    Private Async Function CargarExpedientes() As Task
        Try
            Dim respuesta = Await clienteHttp.GetAsync("api/Reportes/expedientes")

            If respuesta.IsSuccessStatusCode Then
                Dim json As String = Await respuesta.Content.ReadAsStringAsync()
                ' Deserializamos usando nombres insensibles a mayúsculas/minúsculas
                Dim opciones = New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                listaPacientesOriginal = JsonSerializer.Deserialize(Of List(Of ExpedientePaciente))(json, opciones)
                dgv_Expedientes.DataSource = listaPacientesOriginal
                FormatearColumnasPacientes()

                Dim diccionarioNombres As New AutoCompleteStringCollection()
                For Each paciente In listaPacientesOriginal
                    diccionarioNombres.Add(paciente.nombreCompleto)
                Next
                tb_Buscar.AutoCompleteCustomSource = diccionarioNombres
            End If
        Catch ex As Exception
            MessageBox.Show("Error al conectar con los expedientes: " & ex.Message)
        End Try
    End Function

    ' --- BOTÓN PARA VER DETALLE EN LA MISMA PANTALLA ---
    Private Async Sub btn_detalle_Click(sender As Object, e As EventArgs) Handles btn_detalle.Click
        ' 1. Validamos selección
        If dgv_Expedientes.SelectedRows.Count > 0 Then
            Try
                ' 2. Extraer ID y Nombre
                Dim idPaciente As Integer = Convert.ToInt32(dgv_Expedientes.SelectedRows(0).Cells("id").Value)
                Dim nombreP As String = dgv_Expedientes.SelectedRows(0).Cells("nombreCompleto").Value.ToString()

                ' 3. Mostrar nombre en el TextBox
                tb_Buscar.Text = nombreP

                ' 4. Limpiar historial previo y cargar el nuevo
                historialPaciente.Clear()
                Await CargarConsultasPaciente(idPaciente)

                ' Le decimos a la nueva tabla que dibuje las consultas
                dgv_Historial.DataSource = historialPaciente

                ' Limpiamos la vista para dejar solo lo esencial
                FormatearColumnasHistorial()

            Catch ex As Exception
                MessageBox.Show("Error al procesar la selección: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Por favor, selecciona un paciente de la lista primero.", "Aviso")
        End If
    End Sub

    ' --- CARGA DEL HISTORIAL MÉDICO COMPLETO ---
    Private Async Function CargarConsultasPaciente(id As Integer) As Task
        Try
            Dim respuesta = Await clienteHttp.GetAsync($"api/Reportes/historial/{id}")

            If respuesta.IsSuccessStatusCode Then
                Dim json As String = Await respuesta.Content.ReadAsStringAsync()
                Dim opciones = New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim datos = JsonSerializer.Deserialize(Of List(Of FilaHistorial))(json, opciones)

                ' Agregar los datos a la BindingList para que se vean en dgv_Historial
                For Each fila In datos
                    historialPaciente.Add(fila)
                Next
            Else
                MessageBox.Show("No se encontró historial clínico para este paciente.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar historial: " & ex.Message)
        End Try
    End Function

    Private Sub FormatearColumnasPacientes()
        If dgv_Expedientes.Columns.Count > 0 Then
            dgv_Expedientes.Columns("id").Visible = False
            dgv_Expedientes.Columns("nombreCompleto").HeaderText = "Nombre Completo"
            dgv_Expedientes.Columns("telefono").HeaderText = "Teléfono"
            dgv_Expedientes.Columns("correo").HeaderText = "Correo Electrónico"
        End If
    End Sub

    ' Método para dejar el Grid súper limpio
    Private Sub FormatearColumnasHistorial()
        If dgv_Historial.Columns.Count > 0 Then
            ' Ocultamos toda la información densa
            dgv_Historial.Columns("IdConsulta").Visible = False
            dgv_Historial.Columns("Sintomas").Visible = False
            dgv_Historial.Columns("Observaciones").Visible = False
            dgv_Historial.Columns("Peso").Visible = False
            dgv_Historial.Columns("Altura").Visible = False

            ' Si la lista de la receta intenta mostrarse como columna, la ocultamos
            If dgv_Historial.Columns.Contains("Receta") Then
                dgv_Historial.Columns("Receta").Visible = False
            End If

            ' Renombramos lo esencial
            dgv_Historial.Columns("Fecha").HeaderText = "Fecha"
            dgv_Historial.Columns("Hora").HeaderText = "Hora"
            dgv_Historial.Columns("Medico").HeaderText = "Médico Tratante"
            dgv_Historial.Columns("Diagnostico").HeaderText = "Diagnóstico Principal"
        End If
    End Sub

    Private Sub tb_Buscar_TextChanged(sender As Object, e As EventArgs) Handles tb_Buscar.TextChanged
        If listaPacientesOriginal IsNot Nothing Then
            Dim textoBusqueda As String = tb_Buscar.Text.Trim().ToLower()

            If textoBusqueda = "" Then
                dgv_Expedientes.DataSource = listaPacientesOriginal
                historialPaciente.Clear()
            Else
                Dim listaFiltrada = listaPacientesOriginal.Where(Function(p) p.nombreCompleto.ToLower().Contains(textoBusqueda)).ToList()
                dgv_Expedientes.DataSource = listaFiltrada
            End If
            FormatearColumnasPacientes()
        End If
    End Sub

    Private Async Sub tb_Buscar_KeyDown(sender As Object, e As KeyEventArgs) Handles tb_Buscar.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True

            If listaPacientesOriginal IsNot Nothing Then
                Dim pacienteEncontrado = listaPacientesOriginal.FirstOrDefault(Function(p) p.nombreCompleto.ToLower() = tb_Buscar.Text.ToLower())

                If pacienteEncontrado IsNot Nothing Then
                    historialPaciente.Clear()
                    Await CargarConsultasPaciente(pacienteEncontrado.id)
                    dgv_Historial.DataSource = historialPaciente
                    FormatearColumnasHistorial()

                    Dim listaUnica As New List(Of ExpedientePaciente) From {pacienteEncontrado}
                    dgv_Expedientes.DataSource = listaUnica
                Else
                    MessageBox.Show("Paciente no encontrado. Por favor selecciona uno de la lista autocompletada.", "Aviso")
                End If
            End If
        End If
    End Sub

    Private Sub btn_Descargar_Click(sender As Object, e As EventArgs) Handles btn_Descargar.Click
        If dgv_Historial.Rows.Count = 0 OrElse dgv_Expedientes.CurrentRow Is Nothing Then
            MessageBox.Show("Busca y selecciona un paciente con historial médico antes de descargar el expediente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim nombreArchivo As String = tb_Buscar.Text.Replace(" ", "_")

        Dim ventanaGuardar As New SaveFileDialog()
        ventanaGuardar.Filter = "Archivos PDF (*.pdf)|*.pdf"
        ventanaGuardar.Title = "Guardar Expediente Clínico"
        ventanaGuardar.FileName = $"Expediente_{nombreArchivo}_{DateTime.Now.ToString("yyyyMMdd")}.pdf"

        If ventanaGuardar.ShowDialog() = DialogResult.OK Then
            Try
                Dim documentoVirtual As New Printing.PrintDocument()
                documentoVirtual.PrintController = New Printing.StandardPrintController()
                documentoVirtual.PrinterSettings.PrinterName = "Microsoft Print to PDF"
                documentoVirtual.PrinterSettings.PrintToFile = True
                documentoVirtual.PrinterSettings.PrintFileName = ventanaGuardar.FileName
                documentoVirtual.DefaultPageSettings.Landscape = False

                AddHandler documentoVirtual.PrintPage, AddressOf DibujarExpedienteEnPDF
                filaImpresionActual = 0

                documentoVirtual.Print()
                MessageBox.Show("El expediente se guardó correctamente.", "Descarga Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al generar el PDF. Detalle: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' IMPRESIÓN DEL PDF CON TODOS LOS DATOS COMPLETOS
    Private Sub DibujarExpedienteEnPDF(sender As Object, e As Printing.PrintPageEventArgs)
        Dim fuenteTitulo As New Font("Segoe UI", 16, FontStyle.Bold)
        Dim fuenteSubtitulo As New Font("Segoe UI", 12, FontStyle.Bold)
        Dim fuenteNegrita As New Font("Segoe UI", 10, FontStyle.Bold)
        Dim fuenteNormal As New Font("Segoe UI", 10, FontStyle.Regular)
        Dim fuenteCursiva As New Font("Segoe UI", 9, FontStyle.Italic)
        Dim fuentePequena As New Font("Segoe UI", 9, FontStyle.Regular)

        Dim colorTexto As New SolidBrush(Color.Black)
        Dim colorAcento As New SolidBrush(Color.DarkBlue)
        Dim plumaSeparadora As New Pen(Color.LightGray, 2)

        Dim Y As Integer = 50
        Dim margenIzq As Integer = 60
        Dim anchoMaximo As Integer = e.PageBounds.Width - (margenIzq * 2)

        ' ENCABEZADO (Página 1)
        If filaImpresionActual = 0 Then
            e.Graphics.DrawString("Expediente Clínico - Sistema CLARA", fuenteTitulo, colorTexto, margenIzq, Y)
            Y += 35

            Dim rowPaciente = dgv_Expedientes.CurrentRow
            Dim nombre As String = If(rowPaciente.Cells("nombreCompleto").Value, "").ToString()
            Dim tel As String = If(rowPaciente.Cells("telefono").Value, "").ToString()
            Dim correo As String = If(rowPaciente.Cells("correo").Value, "").ToString()

            e.Graphics.DrawString("Paciente: " & nombre, fuenteSubtitulo, colorTexto, margenIzq, Y)
            Y += 25
            e.Graphics.DrawString($"Teléfono: {tel}   |   Correo: {correo}", fuenteNormal, colorTexto, margenIzq, Y)
            Y += 20
            e.Graphics.DrawString("Fecha de impresión: " & DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"), fuenteCursiva, colorTexto, margenIzq, Y)
            Y += 30

            e.Graphics.DrawLine(Pens.Black, margenIzq, Y, margenIzq + anchoMaximo, Y)
            Y += 25
        End If

        ' RECORRER CONSULTAS
        While filaImpresionActual < historialPaciente.Count
            Dim consultaActual = historialPaciente(filaImpresionActual)

            Dim fecha As String = consultaActual.Fecha
            Dim hora As String = consultaActual.Hora
            Dim medico As String = consultaActual.Medico
            Dim peso As String = consultaActual.Peso.ToString()
            Dim altura As String = consultaActual.Altura.ToString()
            Dim sintomas As String = consultaActual.Sintomas
            Dim diagnostico As String = consultaActual.Diagnostico
            Dim observaciones As String = consultaActual.Observaciones

            ' Medir altura de los textos largos
            Dim altoSintomas As Single = e.Graphics.MeasureString(sintomas, fuenteNormal, anchoMaximo).Height
            Dim altoDiag As Single = e.Graphics.MeasureString(diagnostico, fuenteNormal, anchoMaximo).Height
            Dim altoObs As Single = e.Graphics.MeasureString(observaciones, fuenteNormal, anchoMaximo).Height

            ' Calcular altura aproximada de la receta
            Dim altoReceta As Single = 0
            If consultaActual.Receta IsNot Nothing AndAlso consultaActual.Receta.Count > 0 Then
                altoReceta = 30 + (consultaActual.Receta.Count * 20) ' 30 para el título, 20 por cada medicina
            End If

            ' Validar si todo el bloque cabe
            Dim altoTotalBloque As Single = 150 + altoSintomas + altoDiag + altoObs + altoReceta
            If Y + altoTotalBloque > e.MarginBounds.Bottom Then
                e.HasMorePages = True
                Return
            End If

            ' Cabecera de la consulta
            e.Graphics.DrawString($"Consulta del: {fecha} a las {hora}", fuenteSubtitulo, colorAcento, margenIzq, Y)
            Y += 20
            e.Graphics.DrawString($"Atendió: Dr(a). {medico}  |  Peso: {peso} kg  |  Altura: {altura} m", fuenteNegrita, colorTexto, margenIzq, Y)
            Y += 25

            ' Síntomas
            e.Graphics.DrawString("Síntomas:", fuenteNegrita, colorTexto, margenIzq, Y)
            Y += 20
            Dim rectSintomas As New RectangleF(margenIzq, Y, anchoMaximo, altoSintomas)
            e.Graphics.DrawString(sintomas, fuenteNormal, colorTexto, rectSintomas)
            Y += CInt(altoSintomas) + 15

            ' Diagnóstico
            e.Graphics.DrawString("Diagnóstico:", fuenteNegrita, colorTexto, margenIzq, Y)
            Y += 20
            Dim rectDiag As New RectangleF(margenIzq, Y, anchoMaximo, altoDiag)
            e.Graphics.DrawString(diagnostico, fuenteNormal, colorTexto, rectDiag)
            Y += CInt(altoDiag) + 15

            ' Observaciones
            e.Graphics.DrawString("Observaciones:", fuenteNegrita, colorTexto, margenIzq, Y)
            Y += 20
            Dim rectObs As New RectangleF(margenIzq, Y, anchoMaximo, altoObs)
            e.Graphics.DrawString(observaciones, fuenteNormal, colorTexto, rectObs)
            Y += CInt(altoObs) + 20

            ' Receta Médica (Imprimir solo si recetaron algo)
            If consultaActual.Receta IsNot Nothing AndAlso consultaActual.Receta.Count > 0 Then
                e.Graphics.DrawString("Receta Médica:", fuenteNegrita, colorTexto, margenIzq, Y)
                Y += 20

                For Each med In consultaActual.Receta
                    Dim textoMedicina As String = $"• {med.Medicamento} - Dosis: {med.Dosis} - Frecuencia: {med.Frecuencia} por {med.Duracion}"
                    e.Graphics.DrawString(textoMedicina, fuentePequena, colorTexto, margenIzq + 15, Y)
                    Y += 20
                Next
                Y += 10
            End If

            ' Línea separadora
            e.Graphics.DrawLine(plumaSeparadora, margenIzq + 80, Y, margenIzq + anchoMaximo - 80, Y)
            Y += 30

            filaImpresionActual += 1
        End While

        e.HasMorePages = False
    End Sub
End Class