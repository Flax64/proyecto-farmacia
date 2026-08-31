Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class HorariosCreate
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://localhost:5133/api/horarios"

    Private Async Sub HorariosCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' Ajustamos las horas por defecto (Ej: 08:00 AM a 04:00 PM)
        dtp_HoraEntrada.Value = DateTime.Today.AddHours(8)
        dtp_HoraSalida.Value = DateTime.Today.AddHours(16)

        Await CargarCatalogos()
    End Sub

    Private Async Function CargarCatalogos() As Task
        Try
            ' Cargar Médicos
            Dim resMedicos = Await clienteHttp.GetAsync($"{urlBase}/medicos")
            If resMedicos.IsSuccessStatusCode Then
                Dim jsonMedicos = Await resMedicos.Content.ReadAsStringAsync()
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim listaMedicos = JsonSerializer.Deserialize(Of List(Of CatalogoCombo))(jsonMedicos, opciones)
                cmb_Medico.DataSource = listaMedicos
                cmb_Medico.DisplayMember = "Nombre"
                cmb_Medico.ValueMember = "Id"
                cmb_Medico.SelectedIndex = -1 ' No seleccionar ningún médico por defecto
            End If

            ' Cargar Días
            Dim resDias = Await clienteHttp.GetAsync($"{urlBase}/dias")
            If resDias.IsSuccessStatusCode Then
                Dim jsonDias = Await resDias.Content.ReadAsStringAsync()
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim listaDias = JsonSerializer.Deserialize(Of List(Of CatalogoCombo))(jsonDias, opciones)
                cmb_Dia.DataSource = listaDias
                cmb_Dia.DisplayMember = "Nombre"
                cmb_Dia.ValueMember = "Id"
                cmb_Dia.SelectedIndex = -1 ' No seleccionar ningún día por defecto
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    Private Async Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        If cmb_Medico.SelectedValue Is Nothing OrElse cmb_Dia.SelectedValue Is Nothing Then
            MessageBox.Show("Por favor selecciona un Médico y un Día.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validación visual rápida
        If dtp_HoraSalida.Value.TimeOfDay <= dtp_HoraEntrada.Value.TimeOfDay Then
            MessageBox.Show("La hora de salida debe ser estrictamente posterior a la hora de entrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        btn_guardar.Enabled = False
        btn_guardar.Text = "GUARDANDO..."

        Try
            ' Preparamos los datos enviando la hora en formato 24 hrs (HH:mm:ss) para MySQL
            Dim nuevoHorario = New With {
                .IdMedico = Convert.ToInt32(cmb_Medico.SelectedValue),
                .IdDia = Convert.ToInt32(cmb_Dia.SelectedValue),
                .HoraEntrada = dtp_HoraEntrada.Value.ToString("HH:mm:00"),
                .HoraSalida = dtp_HoraSalida.Value.ToString("HH:mm:00")
            }

            Dim jsonString = JsonSerializer.Serialize(nuevoHorario)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            Dim response = Await clienteHttp.PostAsync(urlBase, content)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Horario creado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Dim errorMsg As String = "Error desconocido."
                Try
                    Using doc = JsonDocument.Parse(responseBody)
                        errorMsg = doc.RootElement.GetProperty("error").GetString()
                    End Using
                Catch
                End Try
                MessageBox.Show(errorMsg, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_guardar.Enabled = True
            btn_guardar.Text = "CREAR HORARIO"
        End Try
    End Sub
End Class