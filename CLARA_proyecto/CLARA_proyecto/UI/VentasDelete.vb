Imports System.Net.Http
Imports System.Text.Json '  IMPORTANTE PARA LEER LOS ERRORES DEL BACKEND

Public Class VentasDelete
    Private idVentaABorrar As Integer
    Private clienteHttp As HttpClient

    ' 1. Modificamos el constructor para que reciba el ID de la venta
    Public Sub New(id As Integer)
        InitializeComponent()
        idVentaABorrar = id

        ' Configuramos el cliente HTTP
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' Quitamos los bordes de Windows para que se vea como tu diseño de tarjeta
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub

    Private Sub VentasDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close() ' Solo cerramos la ventana sin hacer nada
    End Sub

    Private Async Sub btn_borrar_Click(sender As Object, e As EventArgs) Handles btn_borrar.Click
        Try
            ' Cambiamos el texto del botón para que el usuario sepa que está cargando
            btn_borrar.Text = "Borrando..."
            btn_borrar.Enabled = False

            Dim urlEliminar As String = $"http://54.89.200.65:5133/api/ventas/{idVentaABorrar}"
            Dim response As HttpResponseMessage = Await clienteHttp.DeleteAsync(urlEliminar)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("¡Venta cancelada y stock revertido con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close() ' Cerramos la ventanita
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

                MessageBox.Show("No se pudo cancelar la venta." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' Restauramos el botón en caso de error para que puedan volver a intentar
                btn_borrar.Text = "BORRAR"
                btn_borrar.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub
End Class