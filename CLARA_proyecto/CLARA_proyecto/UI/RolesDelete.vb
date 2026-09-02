Imports System.Net.Http
Imports System.Text.Json

Public Class RolesDelete
    ' Variable para guardar el ID del rol que vamos a borrar
    Private idRolSeleccionado As Integer
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/roles"

    ' --- CONSTRUCTOR ---
    ' Esta es la magia que recibe el ID cuando abres la ventana desde RolesRead
    Public Sub New(idRol As Integer)
        ' Esta llamada es requerida por el diseñador (nunca la borres)
        InitializeComponent()

        ' Guardamos el ID que nos mandaron
        idRolSeleccionado = idRol
    End Sub

    ' --- AL CARGAR LA PANTALLA ---
    Private Sub RolesDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuramos el cliente HTTP (ignorando certificado local)
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
    End Sub

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        ' Solo cerramos la ventana sin hacer nada, la pantalla de atrás seguirá intacta
        Me.Close()
    End Sub

    ' --- BOTÓN BORRAR ---
    Private Async Sub btn_borrar_Click(sender As Object, e As EventArgs) Handles btn_borrar.Click
        btn_borrar.Enabled = False
        btn_borrar.Text = "Borrando..."

        Try
            ' Hacemos la petición DELETE directamente a la URL con el ID (ej. api/roles/3)
            Dim response As HttpResponseMessage = Await clienteHttp.DeleteAsync($"{urlBase}/{idRolSeleccionado}")
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Rol eliminado correctamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Si se borró bien, cerramos la ventana. 
                ' La pantalla principal (RolesRead) detectará que se cerró y recargará la tabla sola.
                Me.Close()
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND (Ej: "El rol está en uso")
                Dim errorMsg As String = "No se pudo eliminar este rol."
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

                ' Si es un 400 BadRequest (Ej. "El rol está en uso") lo mostramos como advertencia
                If response.StatusCode = Net.HttpStatusCode.BadRequest Then
                    MessageBox.Show(errorMsg, "Aviso de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Hubo un problema al borrar el rol." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                ' Restauramos el botón por si quiere intentar otra cosa o cancelar
                btn_borrar.Enabled = True
                btn_borrar.Text = "BORRAR"
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btn_borrar.Enabled = True
            btn_borrar.Text = "BORRAR"
        End Try
    End Sub
End Class