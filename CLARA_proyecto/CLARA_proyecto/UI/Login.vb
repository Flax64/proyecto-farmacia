Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class Login

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Aseguramos que la contraseña empiece OCULTA (con puntitos)
        txb_password.UseSystemPasswordChar = True

        ' 2. Mostramos el ojito abierto y escondemos el cerrado
        pbx_ver.Visible = True
        pbx_ocultar.Visible = False
    End Sub

    ' Manejo de "Enter" para pasar al siguiente campo
    Private Sub txb_email_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_email.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Private Sub btn_login_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btn_login.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    ' Navegación a Cambiar Contraseña
    Private Sub lblk_change_password_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblk_change_password.LinkClicked
        Me.Hide()
        Dim cambiarPasswordForm As New CambiarPassword1()
        cambiarPasswordForm.ShowDialog()
        Me.Show()
        ' Limpiamos los campos
        txb_email.Clear()
        txb_password.Clear()
    End Sub

    ' Navegación a Registro
    Private Sub lblk_sign_in_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblk_sign_in.LinkClicked
        Me.Hide()
        Dim registrarForm As New Registrar()
        registrarForm.ShowDialog()
        Me.Show()
        ' Limpiamos los campos
        txb_email.Clear()
        txb_password.Clear()
    End Sub

    ' =========================================================================
    ' 4. EL NÚCLEO: COMUNICACIÓN CON LA API (Async / Await)
    ' =========================================================================
    Private Async Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        ' --- PASO A: VALIDACIONES LOCALES ---
        If String.IsNullOrWhiteSpace(txb_email.Text) OrElse String.IsNullOrWhiteSpace(txb_password.Text) Then
            MessageBox.Show("Por favor, ingresa tu correo y contraseña.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim patron As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Dim regex As New Regex(patron)
        If Not regex.IsMatch(txb_email.Text) Then
            MessageBox.Show("Por favor, ingresa un formato de correo válido (ejemplo@dominio.com).", "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' --- PASO B: PREPARAR INTERFAZ ---
        btn_login.Enabled = False
        btn_login.Text = "Conectando..."
        lblk_change_password.Enabled = False
        lblk_sign_in.Enabled = False

        ' --- PASO C: EMPAQUETADO ---
        Dim requestData As New LoginRequestVB() With {
            .Email = txb_email.Text.Trim(),
            .Password = txb_password.Text
        }

        Dim jsonString As String = JsonSerializer.Serialize(requestData)
        Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

        ' --- PASO D: EL VIAJE A LA API ---
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(sen, cert, chain, sslPolicyErrors) True
        Using client As New HttpClient(manejador)
            Try
                Dim urlApi As String = "http://54.89.200.65:5133/api/auth/login"
                Dim response As HttpResponseMessage = Await client.PostAsync(urlApi, content)
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                ' --- PASO E: EVALUAR LA RESPUESTA ---
                If response.IsSuccessStatusCode Then
                    ' LEEMOS EL JSON QUE MANDÓ C# PARA ATRAPAR EL ID
                    Dim jsonDoc = JsonDocument.Parse(responseBody)
                    Dim idExtraido As Integer = jsonDoc.RootElement.GetProperty("idUsuario").GetInt32()

                    ' GUARDAMOS LOS DATOS EN LA SESIÓN GLOBAL
                    SesionGlobal.correo = txb_email.Text
                    SesionGlobal.idUsuario = idExtraido

                    ' Éxito: Abrimos el menú principal
                    Me.Hide()
                    Dim menuForm As New Menu()
                    menuForm.ShowDialog()

                    ' Cuando el Menú se cierre, volvemos a mostrar el Login
                    Me.Show()
                    txb_email.Clear()
                    txb_password.Clear()
                Else
                    '  ATRAPAMOS CUALQUIER TIPO DE ERROR (Credenciales incorrectas, servidor, etc.)
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

                    ' Si fue un error de "Unauthorized" (401), ponemos un título distinto
                    If response.StatusCode = Net.HttpStatusCode.Unauthorized Then
                        MessageBox.Show(errorMsg, "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Else
                        MessageBox.Show("Ocurrió un problema." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ' --- PASO F: RESTAURACIÓN ---
                btn_login.Enabled = True
                btn_login.Text = "INICIAR SESIÓN"
                lblk_change_password.Enabled = True
                lblk_sign_in.Enabled = True
            End Try
        End Using
    End Sub

    Private Sub txb_password_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_password.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Private Sub pbx_ver_MouseDown(sender As Object, e As MouseEventArgs) Handles pbx_ver.MouseDown
        ' 1. REVELAMOS la contraseña
        txb_password.UseSystemPasswordChar = False

        ' 2. Intercambiamos los íconos
        pbx_ver.Visible = False
        pbx_ocultar.Visible = True
    End Sub
    Private Sub pbx_ver_MouseUp(sender As Object, e As MouseEventArgs) Handles pbx_ver.MouseUp
        ' 1. VOLVEMOS A OCULTAR la contraseña
        txb_password.UseSystemPasswordChar = True

        ' 2. Restauramos los íconos originales
        pbx_ver.Visible = True
        pbx_ocultar.Visible = False
    End Sub
End Class
