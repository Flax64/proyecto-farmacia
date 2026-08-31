Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class CambiarPassword3

    Private Async Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        ' 1. Validaciones básicas
        If String.IsNullOrWhiteSpace(txb_passActual.Text) OrElse
           String.IsNullOrWhiteSpace(txb_newPass.Text) OrElse
           String.IsNullOrWhiteSpace(txb_newPassConf.Text) Then
            MessageBox.Show("Por favor, llena todos los campos.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If txb_newPass.Text <> txb_newPassConf.Text Then
            MessageBox.Show("Las contraseñas nuevas no coinciden. Intenta de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim regexSeguridad As New Regex("^(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$")
        If Not regexSeguridad.IsMatch(txb_newPass.Text) Then
            MessageBox.Show("La nueva contraseña debe tener mínimo 8 caracteres, una mayúscula y un número.", "Contraseña Débil", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Bloqueamos interfaz
        btn_guardar.Enabled = False
        btn_guardar.Text = "Guardando..."

        Try
            ' 2. Empaquetamos los datos exactamente como los pide C#
            Dim requestData = New With {
                .PasswordActual = txb_passActual.Text,
                .NuevaPassword = txb_newPass.Text
            }
            Dim jsonString As String = JsonSerializer.Serialize(requestData)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            ' 3. Preparamos el cliente HTTP
            Dim manejador As New HttpClientHandler()
            manejador.ServerCertificateCustomValidationCallback = Function(sen, cert, chain, sslPolicyErrors) True

            Using client As New HttpClient(manejador)
                ' Verifica que el puerto 5133 sea el correcto en tu computadora
                Dim urlAPI As String = $"http://localhost:5133/api/perfil/cambiar-password/{SesionGlobal.correo}"

                ' Fíjate que aquí usamos PutAsync (porque en C# pusimos HttpPut)
                Dim response As HttpResponseMessage = Await client.PutAsync(urlAPI, content)
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("¡Tu contraseña ha sido actualizada con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                Else
                    '  ATRAPAMOS EL ERROR DEL BACKEND
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

                    MessageBox.Show("No se pudo cambiar la contraseña." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Restaurar el botón
            btn_guardar.Enabled = True
            btn_guardar.Text = "GUARDAR"
        End Try
    End Sub

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    Private Sub txb_passActual_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_passActual.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txb_newPass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_newPass.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txb_newPassConf_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_newPassConf.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub CambiarPassword3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class