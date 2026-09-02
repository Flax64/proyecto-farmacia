Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class CitasCreate
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/citas"
    Private isCargando As Boolean = True

    Private Async Sub CitasCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' Ajustes visuales
        cmb_Hora.MaxDropDownItems = 8
        cmb_Hora.IntegralHeight = False
        dtp_Fecha.MinDate = DateTime.Today

        '  ORDEN DEL TABULADOR (Para navegar rápido con el teclado)
        cmb_Paciente.TabIndex = 1
        dtp_Fecha.TabIndex = 2
        cmb_Medico.TabIndex = 3
        cmb_Hora.TabIndex = 4
        btn_guardar.TabIndex = 5

        '  Cargamos los catálogos base PRIMERO
        Await CargarPacientes()
        Await CargarMedicos()

        isCargando = False

        ' Luego cargamos las horas de quien haya quedado seleccionado por defecto
        Await CargarHorasDisponibles()
    End Sub

    ' --- CARGAR PACIENTES ---
    Private Async Function CargarPacientes() As Task
        Try
            Dim correo As String = SesionGlobal.correo
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/pacientes?correo={correo}")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim listaPacientes = JsonSerializer.Deserialize(Of List(Of PacienteCombo))(responseBody, opciones)

                cmb_Paciente.DataSource = listaPacientes
                cmb_Paciente.DisplayMember = "Nombre"
                cmb_Paciente.ValueMember = "Id"

                If listaPacientes.Count = 1 Then
                    cmb_Paciente.SelectedIndex = 0
                    cmb_Paciente.Enabled = False
                Else
                    cmb_Paciente.SelectedIndex = -1
                End If
            End If
        Catch ex As Exception
        End Try
    End Function

    ' --- CARGAR TODOS LOS MÉDICOS ACTIVOS ---
    Private Async Function CargarMedicos() As Task
        Try
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/medicos")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim listaMedicos = JsonSerializer.Deserialize(Of List(Of MedicoCombo))(responseBody, opciones)

                cmb_Medico.DataSource = listaMedicos
                cmb_Medico.DisplayMember = "Nombre"
                cmb_Medico.ValueMember = "Id"
                cmb_Medico.SelectedIndex = -1
            End If
        Catch ex As Exception
        End Try
    End Function

    ' --- CARGAR HORAS DEL MÉDICO SELECCIONADO ---
    Private Async Function CargarHorasDisponibles() As Task
        ' Si está cargando la pantalla o no hay médico, no hace nada
        If isCargando OrElse cmb_Medico.SelectedValue Is Nothing Then Return

        Try
            Dim fechaApi As String = dtp_Fecha.Value.ToString("yyyy-MM-dd")
            Dim idMedico As Integer = Convert.ToInt32(cmb_Medico.SelectedValue)

            ' Mandamos el idMedico y la fecha a la API
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/horas-disponibles?fecha={fechaApi}&idMedico={idMedico}")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            cmb_Hora.DataSource = Nothing

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim horas = JsonSerializer.Deserialize(Of List(Of String))(responseBody, opciones)
                Dim listaHoras As New List(Of HoraCombo)()

                If horas.Count > 0 Then
                    For Each hora In horas
                        Dim dtHora As DateTime = DateTime.ParseExact(hora, "HH:mm", Globalization.CultureInfo.InvariantCulture)
                        listaHoras.Add(New HoraCombo With {
                            .ValorApi = hora & ":00",
                            .TextoAMPM = dtHora.ToString("hh:mm tt")
                        })
                    Next

                    cmb_Hora.DataSource = listaHoras
                    cmb_Hora.DisplayMember = "TextoAMPM"
                    cmb_Hora.ValueMember = "ValorApi"
                    cmb_Hora.SelectedIndex = 0
                    btn_guardar.Enabled = True
                Else
                    ' Si no tiene horario de trabajo o ya se le llenaron las citas de ese día
                    listaHoras.Add(New HoraCombo With {.ValorApi = "", .TextoAMPM = "Doctor sin turno libre"})
                    cmb_Hora.DataSource = listaHoras
                    cmb_Hora.DisplayMember = "TextoAMPM"
                    cmb_Hora.ValueMember = "ValorApi"
                    btn_guardar.Enabled = False
                End If
            End If
        Catch ex As Exception
            Dim listaError As New List(Of HoraCombo) From {New HoraCombo With {.ValorApi = "", .TextoAMPM = "Error al cargar"}}
            cmb_Hora.DataSource = listaError
            cmb_Hora.DisplayMember = "TextoAMPM"
            cmb_Hora.ValueMember = "ValorApi"
            btn_guardar.Enabled = False
        End Try
    End Function

    ' --- EVENTOS DINÁMICOS ---
    Private Async Sub dtp_Fecha_ValueChanged(sender As Object, e As EventArgs) Handles dtp_Fecha.ValueChanged
        Await CargarHorasDisponibles()
    End Sub

    Private Async Sub cmb_Medico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Medico.SelectedIndexChanged
        Await CargarHorasDisponibles()
    End Sub

    ' --- BOTÓN AGENDAR (GUARDAR) ---
    Private Async Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        If cmb_Paciente.SelectedValue Is Nothing Then
            MessageBox.Show("Por favor, selecciona un paciente válido.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If cmb_Medico.SelectedValue Is Nothing OrElse cmb_Hora.SelectedValue Is Nothing OrElse String.IsNullOrEmpty(cmb_Hora.SelectedValue.ToString()) Then
            MessageBox.Show("No hay un médico asignado o el horario seleccionado no es válido.", "Sin Médico u Horario", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        btn_guardar.Enabled = False
        btn_guardar.Text = "Guardando..."

        Try
            Dim nuevaCita = New With {
                .IdPaciente = Convert.ToInt32(cmb_Paciente.SelectedValue),
                .IdMedico = Convert.ToInt32(cmb_Medico.SelectedValue),
                .Fecha = dtp_Fecha.Value.ToString("yyyy-MM-dd"),
                .Hora = cmb_Hora.SelectedValue.ToString()
            }

            Dim jsonString = JsonSerializer.Serialize(nuevaCita)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            Dim response = Await clienteHttp.PostAsync(urlBase, content)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim msjExito As String = "Operación completada."
                Try
                    Using doc = JsonDocument.Parse(responseBody)
                        msjExito = doc.RootElement.GetProperty("message").GetString()
                    End Using
                Catch
                End Try

                MessageBox.Show(msjExito, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show(errorMsg, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_guardar.Enabled = True
            btn_guardar.Text = "AGENDAR"
        End Try
    End Sub

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

End Class