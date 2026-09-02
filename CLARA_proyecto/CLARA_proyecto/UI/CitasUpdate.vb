Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class CitasUpdate
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/citas"

    Private idCitaActual As Integer
    Private horaOriginal As String = ""

    ' BANDERAS PARA SILENCIAR EVENTOS MIENTRAS CARGA LA PANTALLA
    Private isCargando As Boolean = True
    Private isInicializando As Boolean = True

    Public Sub New(id As Integer)
        InitializeComponent()
        idCitaActual = id

        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
    End Sub

    Private Async Sub CitasUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmb_Hora.MaxDropDownItems = 8
        cmb_Hora.IntegralHeight = False

        ' 1. CARGAMOS AMBOS CATÁLOGOS COMPLETOS PRIMERO
        Await CargarPacientes()
        Await CargarMedicos()

        ' 2. LUEGO CARGAMOS LOS DATOS GUARDADOS DE LA CITA
        Await CargarDatosDeLaCita()
    End Sub

    ' --- DESCARGAR DATOS DE LA CITA ACTUAL ---
    Private Async Function CargarDatosDeLaCita() As Task
        Try
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/{idCitaActual}")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim cita = JsonSerializer.Deserialize(Of CitaEditDTO)(responseBody, opciones)

                ' Seleccionamos el paciente y el médico que tenía la cita guardada
                cmb_Paciente.SelectedValue = cita.IdPaciente
                cmb_Medico.SelectedValue = cita.IdMedico

                ' ESTADO (SIEMPRE BLOQUEADO PARA QUE NO SE MODIFIQUE DESDE EL EDITAR)
                Dim idEstado As Integer = 7 ' Por defecto Pendiente (7)
                If cita.Estado = "Confirmada" Then idEstado = 3
                If cita.Estado = "Cancelada" Then idEstado = 4

                Dim listaEstadoActual As New List(Of EstadoCombo) From {
                    New EstadoCombo With {.Id = idEstado, .Nombre = cita.Estado}
                }
                cmb_Estado.DataSource = listaEstadoActual
                cmb_Estado.DisplayMember = "Nombre"
                cmb_Estado.ValueMember = "Id"
                cmb_Estado.SelectedIndex = 0
                cmb_Estado.Enabled = False

                ' Fecha
                Dim fechaParseada = DateTime.Parse(cita.Fecha)
                If fechaParseada < DateTime.Today Then
                    dtp_Fecha.MinDate = fechaParseada
                Else
                    dtp_Fecha.MinDate = DateTime.Today
                End If

                dtp_Fecha.Value = fechaParseada
                horaOriginal = cita.Hora

                ' Cargamos las horas de ESE médico en ESA fecha
                isCargando = False
                Await CargarHorasDisponibles()

                ' Buscamos la hora original en la lista y la seleccionamos
                If cmb_Hora.DataSource IsNot Nothing Then
                    Dim listaHoras = CType(cmb_Hora.DataSource, List(Of HoraCombo))
                    Dim buscarHora = listaHoras.FirstOrDefault(Function(h) h.ValorApi = horaOriginal)
                    If buscarHora IsNot Nothing Then
                        cmb_Hora.SelectedValue = horaOriginal
                    Else
                        cmb_Hora.SelectedIndex = 0
                    End If
                End If

                ' QUITAMOS EL FRENO DE MANO PARA PERMITIR CAMBIOS MANUALES
                isInicializando = False
                btn_guardar.Enabled = True
            Else
                MessageBox.Show("No se pudo cargar la cita.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Error de conexión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Function

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
                End If
            End If
        Catch ex As Exception
        End Try
    End Function

    ' NUEVO: CARGAR TODOS LOS MÉDICOS ACTIVOS
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

    ' MODIFICADO: CARGAR HORAS DEL MÉDICO SELECCIONADO
    Private Async Function CargarHorasDisponibles() As Task
        If isCargando OrElse cmb_Medico.SelectedValue Is Nothing Then Return

        Try
            Dim fechaApi As String = dtp_Fecha.Value.ToString("yyyy-MM-dd")
            Dim idMedico As Integer = Convert.ToInt32(cmb_Medico.SelectedValue)

            ' Mandamos fecha, idMedico e idCita (para que libere la hora original)
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/horas-disponibles?fecha={fechaApi}&idMedico={idMedico}&idCita={idCitaActual}")
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

    ' EVENTOS DINÁMICOS
    Private Async Sub dtp_Fecha_ValueChanged(sender As Object, e As EventArgs) Handles dtp_Fecha.ValueChanged
        If Not isInicializando Then
            Await CargarHorasDisponibles()
        End If
    End Sub

    ' Al editar y cambiar el médico manualmente, recalculamos sus horas
    Private Async Sub cmb_Medico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Medico.SelectedIndexChanged
        If Not isInicializando Then
            Await CargarHorasDisponibles()
        End If
    End Sub

    ' --- ACTUALIZAR DATOS ---
    Private Async Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        If cmb_Paciente.SelectedValue Is Nothing OrElse cmb_Medico.SelectedValue Is Nothing Then
            MessageBox.Show("Revisa que los datos estén completos y haya médico disponible.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        btn_guardar.Enabled = False
        btn_guardar.Text = "Actualizando..."

        Try
            Dim datosUpdate = New With {
                .IdPaciente = Convert.ToInt32(cmb_Paciente.SelectedValue),
                .IdMedico = Convert.ToInt32(cmb_Medico.SelectedValue),
                .Fecha = dtp_Fecha.Value.ToString("yyyy-MM-dd"),
                .Hora = cmb_Hora.SelectedValue.ToString(),
                .IdEstatus = Convert.ToInt32(cmb_Estado.SelectedValue)
            }

            Dim jsonString = JsonSerializer.Serialize(datosUpdate)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            Dim response = Await clienteHttp.PutAsync($"{urlBase}/{idCitaActual}", content)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Cita actualizada exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            MessageBox.Show("Error de conexión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_guardar.Enabled = True
            btn_guardar.Text = "ACTUALIZAR CITA"
        End Try
    End Sub

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub
End Class