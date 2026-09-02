Imports System.Net.Http
Imports System.Text.Json

Public Class EmpleadosDelete
    ' Variable para recibir el ID desde la tabla principal
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property EmpleadoId As Integer
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/empleados"

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' --- BOTÓN BORRAR ---
    Private Async Sub btn_borrar_Click(sender As Object, e As EventArgs) Handles btn_borrar.Click
        btn_borrar.Enabled = False
        btn_borrar.Text = "Borrando..."

        Try
            Dim manejador As New HttpClientHandler()
            manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True

            Using clienteHttp As New HttpClient(manejador)
                ' Enviamos la petición DELETE con el ID en la URL
                Dim response = Await clienteHttp.DeleteAsync($"{urlBase}/{EmpleadoId}")
                Dim responseBody = Await response.Content.ReadAsStringAsync()

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("El empleado ha sido dado de baja correctamente (Estatus: Inactivo).", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    '  ATRAPAMOS EL ERROR DEL BACKEND
                    Dim errorMsg As String = "No se pudo eliminar al empleado."
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

                    ' Si es el error 1451 (Historial), mostramos la advertencia de seguridad amarilla
                    If response.StatusCode = Net.HttpStatusCode.BadRequest Then
                        MessageBox.Show(errorMsg, "Aviso de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Else
                        MessageBox.Show("Hubo un problema al borrar." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    Me.DialogResult = DialogResult.Cancel
                    Me.Close()
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_borrar.Enabled = True
            btn_borrar.Text = "BORRAR"
        End Try
    End Sub

    Private Sub EmpleadosDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class