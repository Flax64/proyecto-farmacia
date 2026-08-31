Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class CambiarPassword2
    ' 1. Esta variable recibirá el token desde la Pantalla 1
    <System.ComponentModel.Browsable(False), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property TokenSeguridad As String

    Private Sub CambiarPassword2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Async Sub btn_enviar_Click(sender As Object, e As EventArgs) Handles btn_cambiar.Click
        ' Validación de seguridad en el Front-end
        If String.IsNullOrWhiteSpace(txb_newPass.Text) OrElse String.IsNullOrWhiteSpace(txb_newPassConf.Text) Then
            MessageBox.Show("Por favor llena ambos campos.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If txb_newPass.Text <> txb_newPassConf.Text Then
            MessageBox.Show("Las contraseñas no coinciden. Intenta de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim regexSeguridad As New Regex("^(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$")
        If Not regexSeguridad.IsMatch(txb_newPass.Text) Then
            MessageBox.Show("La contraseña debe tener mínimo 8 caracteres, una mayúscula y un número.", "Contraseña Débil", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Bloqueamos interfaz
        btn_cambiar.Enabled = False
        btn_cambiar.Text = "Cambiando..."

        ' Empaquetamos enviando el Token que recibimos de la otra pantalla
        Dim requestData As New RestablecerDirectoRequestVB With {
            .Token = TokenSeguridad,
            .NuevaPassword = txb_newPass.Text.Trim
        }
        Dim jsonString = JsonSerializer.Serialize(requestData)
        Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

        ' Creamos un "Manejador" que tiene permiso de ignorar el gafete (certificado SSL)
        Dim manejador As New HttpClientHandler
        manejador.ServerCertificateCustomValidationCallback = Function(sen, cert, chain, sslPolicyErrors) True

        ' IMPORTANTE: Le pasamos el manejador al cliente para que aplique la regla
        Using client As New HttpClient(manejador)
            Try
                Dim urlAPI = "http://localhost:5133/api/auth/restablecer-password"
                Dim response = Await client.PostAsync(urlAPI, content)
                Dim responseBody = Await response.Content.ReadAsStringAsync()

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("¡Tu contraseña ha sido actualizada con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close() ' Al cerrar, el usuario regresará al Login
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
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ' Pase lo que pase, regresamos el botón a la normalidad
                btn_cambiar.Enabled = True
                btn_cambiar.Text = "CAMBIAR CONTRASEÑA"
            End Try
        End Using
    End Sub

    Private Sub txb_newPass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_newPass.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub lblk_change_password_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblk_regresar.LinkClicked
        Me.Close()
    End Sub
End Class