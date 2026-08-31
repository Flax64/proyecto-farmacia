Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class HorariosUpdate
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://localhost:5133/api/horarios"
    Private idHorarioActual As Integer

    ' Recibimos el ID desde la tabla
    Public Sub New(id As Integer)
        InitializeComponent()
        idHorarioActual = id

        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
    End Sub

    Private Async Sub HorariosUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuramos relojes
        dtp_HoraEntrada.Format = DateTimePickerFormat.Custom
        dtp_HoraEntrada.CustomFormat = "hh:mm tt"
        dtp_HoraEntrada.ShowUpDown = True

        dtp_HoraSalida.Format = DateTimePickerFormat.Custom
        dtp_HoraSalida.CustomFormat = "hh:mm tt"
        dtp_HoraSalida.ShowUpDown = True

        ' 1. Cargamos catálogos
        Await CargarCatalogos()
        ' 2. Cargamos los datos del horario a editar
        Await CargarDatosHorario()
    End Sub

    Private Async Function CargarCatalogos() As Task
        Try
            Dim resMedicos = Await clienteHttp.GetAsync($"{urlBase}/medicos")
            Dim jsonMedicos = Await resMedicos.Content.ReadAsStringAsync()
            cmb_Medico.DataSource = JsonSerializer.Deserialize(Of List(Of CatalogoCombo))(jsonMedicos, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})
            cmb_Medico.DisplayMember = "Nombre"
            cmb_Medico.ValueMember = "Id"

            Dim resDias = Await clienteHttp.GetAsync($"{urlBase}/dias")
            Dim jsonDias = Await resDias.Content.ReadAsStringAsync()
            cmb_Dia.DataSource = JsonSerializer.Deserialize(Of List(Of CatalogoCombo))(jsonDias, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})
            cmb_Dia.DisplayMember = "Nombre"
            cmb_Dia.ValueMember = "Id"
        Catch ex As Exception
        End Try
    End Function

    Private Async Function CargarDatosHorario() As Task
        Try
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/{idHorarioActual}")
            If response.IsSuccessStatusCode Then
                Dim json = Await response.Content.ReadAsStringAsync()
                Dim h = JsonSerializer.Deserialize(Of HorarioEditDTO)(json, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})

                cmb_Medico.SelectedValue = h.IdMedico
                cmb_Dia.SelectedValue = h.IdDia

                ' Convertimos los strings de la API (HH:mm) a valores de DateTimePicker
                dtp_HoraEntrada.Value = DateTime.ParseExact(h.HoraEntrada, "HH:mm", Nothing)
                dtp_HoraSalida.Value = DateTime.ParseExact(h.HoraSalida, "HH:mm", Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar datos del horario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Function

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    Private Async Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        If dtp_HoraSalida.Value.TimeOfDay <= dtp_HoraEntrada.Value.TimeOfDay Then
            MessageBox.Show("La hora de salida debe ser estrictamente posterior a la de entrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        btn_guardar.Enabled = False
        btn_guardar.Text = "ACTUALIZANDO..."

        Try
            Dim datos = New With {
                .IdMedico = Convert.ToInt32(cmb_Medico.SelectedValue),
                .IdDia = Convert.ToInt32(cmb_Dia.SelectedValue),
                .HoraEntrada = dtp_HoraEntrada.Value.ToString("HH:mm:00"),
                .HoraSalida = dtp_HoraSalida.Value.ToString("HH:mm:00")
            }

            Dim jsonString = JsonSerializer.Serialize(datos)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            Dim response = Await clienteHttp.PutAsync($"{urlBase}/{idHorarioActual}", content)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Horario actualizado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Dim errorMsg As String = "Error desconocido."
                Try
                    errorMsg = JsonDocument.Parse(responseBody).RootElement.GetProperty("error").GetString()
                Catch
                End Try
                MessageBox.Show(errorMsg, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error de conexión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_guardar.Enabled = True
            btn_guardar.Text = "ACTUALIZAR HORARIO"
        End Try
    End Sub
End Class