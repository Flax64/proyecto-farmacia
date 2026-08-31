Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class EmpleadosCreate
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://localhost:5133/api/empleados" ' Ajusta tu puerto

    ' --- AL ABRIR LA PANTALLA ---
    Private Async Sub EmpleadosCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        Await CargarCatalogos()

        ' 1. La contraseña empieza OCULTA (con puntitos) por defecto
        txt_password.UseSystemPasswordChar = True

        ' 2. Empezamos mostrando el OJITO ABIERTO (para indicar que se puede ver)
        pbx_ver.Visible = True
        pbx_ocultar.Visible = False ' El ojito cerrado empieza escondido
    End Sub

    Private Async Function CargarCatalogos() As Task
        Try
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/catalogos")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim catalogos = JsonSerializer.Deserialize(Of CatalogosResponseVB)(responseBody, opciones)

                cmb_rol.DataSource = catalogos.Roles
                cmb_rol.DisplayMember = "Nombre"
                cmb_rol.ValueMember = "Id"
                cmb_rol.SelectedIndex = -1

                cmb_genero.DataSource = catalogos.Generos
                cmb_genero.DisplayMember = "Nombre"
                cmb_genero.ValueMember = "Id"
                cmb_genero.SelectedIndex = -1

                cmb_estatus.DataSource = catalogos.Estatus
                cmb_estatus.DisplayMember = "Nombre"
                cmb_estatus.ValueMember = "Id"
                cmb_estatus.SelectedIndex = cmb_estatus.FindStringExact("Activo")
                cmb_estatus.Enabled = False
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudieron cargar las listas desplegables." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    ' --- BOTÓN GUARDAR (CON VALIDACIONES) ---
    Private Async Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click

        '  1.1 Validar que no haya campos vacíos
        If String.IsNullOrWhiteSpace(txt_nombre.Text) OrElse
           String.IsNullOrWhiteSpace(txt_apPaterno.Text) OrElse
           String.IsNullOrWhiteSpace(txt_email.Text) OrElse
           String.IsNullOrWhiteSpace(txt_password.Text) OrElse
           String.IsNullOrWhiteSpace(txt_telefono.Text) OrElse
           cmb_rol.SelectedIndex = -1 OrElse
           cmb_genero.SelectedIndex = -1 OrElse
           cmb_estatus.SelectedIndex = -1 Then

            MessageBox.Show("Por favor, llena todos los campos obligatorios.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        '  1.2 Validar formato del correo
        Dim patron As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Dim regex As New Regex(patron)
        If Not regex.IsMatch(txt_email.Text) Then
            MessageBox.Show("Por favor, ingresa un formato de correo válido (ejemplo@dominio.com).", "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        '  1.3 Validar fuerza de contraseña
        Dim regexSeguridad As New Regex("^(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$")
        If Not regexSeguridad.IsMatch(txt_password.Text) Then
            MessageBox.Show("La contraseña debe tener mínimo 8 caracteres, una mayúscula y un número.", "Contraseña Débil", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        '  1.4 Validar Edad y Fecha de Nacimiento
        If dtp_fechaNac.Value.Date > DateTime.Now.Date Then
            MessageBox.Show("La fecha de nacimiento no puede estar en el futuro.", "Fecha no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim edad As Integer = DateTime.Now.Year - dtp_fechaNac.Value.Year
        If DateTime.Now.Date < dtp_fechaNac.Value.AddYears(edad) Then edad -= 1 ' Ajuste si aún no cumple años este año

        If edad < 18 Then
            MessageBox.Show("El empleado debe ser mayor de 18 años para ser registrado.", "Edad no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' --- INTERCEPTAR SI ES MÉDICO ---
        Dim cedulaStr As String = ""
        Dim especialidadStr As String = ""

        If cmb_rol.Text.ToLower().Contains("medico") OrElse cmb_rol.Text.ToLower().Contains("médico") Then
            Dim formMedico As New DatosMedico()

            If formMedico.ShowDialog() = DialogResult.OK Then
                cedulaStr = formMedico.Cedula
                especialidadStr = formMedico.Especialidad
            Else
                Return ' Detenemos el código aquí si cancelan
            End If
        End If

        ' --- SI PASÓ TODAS LAS VALIDACIONES, GUARDAMOS ---
        btn_guardar.Enabled = False
        btn_guardar.Text = "Guardando..."

        Try
            Dim nuevoEmpleado As New UsuarioRequestVB() With {
                .IdEstatus = Convert.ToInt32(cmb_estatus.SelectedValue),
                .IdGenero = Convert.ToInt32(cmb_genero.SelectedValue),
                .IdRol = Convert.ToInt32(cmb_rol.SelectedValue),
                .Nombre = txt_nombre.Text.Trim(),
                .ApellidoPaterno = txt_apPaterno.Text.Trim(),
                .ApellidoMaterno = txt_apMaterno.Text.Trim(),
                .Email = txt_email.Text.Trim(),
                .Password = txt_password.Text.Trim(),
                .Telefono = txt_telefono.Text.Trim(),
                .FechaNacimiento = dtp_fechaNac.Value.ToString("yyyy-MM-dd"),
                .CedulaProfesional = cedulaStr,
                .Especialidad = especialidadStr
            }

            ' 3. Convertimos a JSON y enviamos (Petición POST)
            Dim jsonString As String = JsonSerializer.Serialize(nuevoEmpleado)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            Dim response = Await clienteHttp.PostAsync(urlBase, content)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            ' --- 4. EVALUAMOS LA RESPUESTA ---
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Empleado creado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()

            ElseIf response.StatusCode = System.Net.HttpStatusCode.Conflict Then
                '  CÓDIGO 409: EL CORREO ES DE UN PACIENTE
                Dim idUsuarioExistente As Integer = 0
                Dim mensajePregunta As String = "¿Deseas convertir este paciente en empleado?"

                Try
                    Using doc = JsonDocument.Parse(responseBody)
                        If doc.RootElement.TryGetProperty("idUsuario", Nothing) Then idUsuarioExistente = doc.RootElement.GetProperty("idUsuario").GetInt32()
                        ' Ajuste para leer "error" en lugar de "message" según nuestro nuevo backend
                        If doc.RootElement.TryGetProperty("error", Nothing) Then
                            mensajePregunta = doc.RootElement.GetProperty("error").GetString()
                        ElseIf doc.RootElement.TryGetProperty("message", Nothing) Then
                            mensajePregunta = doc.RootElement.GetProperty("message").GetString()
                        End If
                    End Using
                Catch
                End Try

                Dim respuestaDialogo = MessageBox.Show(mensajePregunta, "Paciente Detectado", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If respuestaDialogo = DialogResult.Yes Then
                    ' 🚀 Si dice que SÍ, reutilizamos el endpoint de ACTUALIZAR (PUT)
                    Dim responseUpdate = Await clienteHttp.PutAsync($"{urlBase}/{idUsuarioExistente}", content)
                    Dim responseUpdateBody = Await responseUpdate.Content.ReadAsStringAsync()

                    If responseUpdate.IsSuccessStatusCode Then
                        MessageBox.Show("Paciente actualizado y convertido a empleado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                    Else
                        '  ATRAPAMOS EL ERROR SI FALLA LA CONVERSIÓN
                        Dim errorUpdateMsg As String = "Error desconocido al actualizar."
                        Try
                            Dim errorData = JsonDocument.Parse(responseUpdateBody).RootElement
                            If errorData.TryGetProperty("error", Nothing) Then errorUpdateMsg = errorData.GetProperty("error").GetString()
                        Catch
                            errorUpdateMsg = responseUpdateBody
                        End Try
                        MessageBox.Show("Hubo un error al intentar actualizar al paciente." & vbCrLf & "Motivo: " & errorUpdateMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If

            Else
                ' CÓDIGO 400 o 500: EL CORREO YA ES DE OTRO EMPLEADO O HUBO UN ERROR
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then
                        errorMsg = errorData.GetProperty("error").GetString()
                    ElseIf errorData.TryGetProperty("message", Nothing) Then
                        errorMsg = errorData.GetProperty("message").GetString()
                    End If
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudo crear el empleado." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_guardar.Enabled = True
            btn_guardar.Text = "CREAR EMPLEADO" ' Ajusta si tu botón originalmente decía "GUARDAR"
        End Try
    End Sub

    ' --- EVENTOS PARA MEJORAR LA EXPERIENCIA DEL USUARIO (UX) ---
    Private Sub txt_telefono_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_telefono.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub NavegarConEnter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nombre.KeyPress, txt_apPaterno.KeyPress, txt_apMaterno.KeyPress, txt_email.KeyPress, txt_password.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub pbx_ver_MouseDown(sender As Object, e As MouseEventArgs) Handles pbx_ver.MouseDown
        txt_password.UseSystemPasswordChar = False
        pbx_ver.Visible = False
        pbx_ocultar.Visible = True
    End Sub

    '  CORRECCIÓN INCLUIDA: Escuchamos ambos ojitos para evitar bugs visuales
    Private Sub pbx_ver_MouseUp(sender As Object, e As MouseEventArgs) Handles pbx_ver.MouseUp, pbx_ocultar.MouseUp
        txt_password.UseSystemPasswordChar = True
        pbx_ver.Visible = True
        pbx_ocultar.Visible = False
    End Sub
End Class