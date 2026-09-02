Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions
Public Class CambiarPassword1
    ' Creamos nuestro cronómetro y la variable para guardar el token temporal
    Private WithEvents temporizadorPolling As New Timer()
    Private tokenMágico As String = ""
    Private clienteHttp As HttpClient ' Solo lo declaramos aquí 

    Private Sub CambiarPassword1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Creamos el manejador para ignorar el error de SSL de tu red local
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True

        ' Construimos el clienteHttp inyectándole nuestro manejador
        clienteHttp = New HttpClient(manejador)

        ' Configuramos el cronómetro para que "lata" cada 3000 milisegundos (3 segundos)
        temporizadorPolling.Interval = 3000
    End Sub

    Private Sub lblk_change_password_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblk_change_password.LinkClicked
        Me.Close()
    End Sub

    ' Cuando el usuario da clic en "ENVIAR ENLACE"
    Private Async Sub btn_enviar_Click(sender As Object, e As EventArgs) Handles btn_enviar.Click
        ' Validar campos vacíos
        If String.IsNullOrWhiteSpace(txb_email.Text) Then
            MessageBox.Show("Por favor, ingresa tu correo electrónico.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validar formato del correo
        Dim patron As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Dim regex As New Regex(patron)

        If Not regex.IsMatch(txb_email.Text) Then
            MessageBox.Show("Por favor, ingresa un formato de correo válido (ejemplo@dominio.com).", "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Preparar interfaz de espera
        btn_enviar.Enabled = False
        btn_enviar.Text = "Enviando..."

        Dim requestData As New SolicitarEnlaceRequestVB With {
            .Correo = txb_email.Text.Trim()
        }
        Dim jsonRequest As String = JsonSerializer.Serialize(requestData)
        Dim content As New StringContent(jsonRequest, Encoding.UTF8, "application/json")

        Try
            Dim urlAPI As String = "http://54.89.200.65:5133/api/auth/solicitar-enlace"
            Dim response As HttpResponseMessage = Await clienteHttp.PostAsync(urlAPI, content)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                'Leemos el Token que nos mandó la API de C#
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim datosRespuesta = JsonSerializer.Deserialize(Of SolicitarEnlaceResponseVB)(responseBody, opciones)

                tokenMágico = datosRespuesta.Token

                ' Cambiamos la interfaz para avisarle al usuario
                btn_enviar.Text = "Esperando confirmación..."
                MessageBox.Show("Te hemos enviado un enlace. Revisa tu correo y haz clic en él. Esta pantalla avanzará sola cuando lo hagas.", "Enlace Enviado", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' ¡ENCENDEMOS EL CRONÓMETRO!
                temporizadorPolling.Start()
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

                MessageBox.Show("No se pudo enviar el enlace." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' Restauramos el botón en caso de error
                btn_enviar.Enabled = True
                btn_enviar.Text = "ENVIAR ENLACE"
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btn_enviar.Enabled = True
            btn_enviar.Text = "ENVIAR ENLACE"
        End Try
    End Sub

    ' EL LATIDO: Este evento se dispara solo, cada 3 segundos, mientras el cronómetro esté encendido
    Private Async Sub temporizadorPolling_Tick(sender As Object, e As EventArgs) Handles temporizadorPolling.Tick
        Try
            ' Le susurramos a la API: "¿Ya le dio clic?"
            Dim urlAPI As String = $"http://54.89.200.65:5133/api/auth/estado-enlace/{tokenMágico}"
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlAPI)

            If response.IsSuccessStatusCode Then
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim estado = JsonSerializer.Deserialize(Of EstadoEnlaceResponseVB)(responseBody, opciones)

                ' Si la API nos dice que SÍ...
                If estado.Confirmado Then
                    ' APAGAMOS EL CRONÓMETRO PARA NO SATURAR LA RED
                    temporizadorPolling.Stop()

                    ' Abrimos la Pantalla 2 y le pasamos el Token de forma invisible
                    Me.Hide()
                    Dim pantalla2 As New CambiarPassword2()
                    pantalla2.TokenSeguridad = tokenMágico ' <- ¡Paso clave!
                    pantalla2.ShowDialog()
                    Me.Close() ' Cerramos todo el flujo
                End If
            End If
        Catch ex As Exception
            ' Si hay un micro-corte de internet, simplemente lo ignoramos y lo vuelve a intentar en 3 segundos
        End Try
    End Sub


End Class