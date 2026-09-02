Imports System.Net.Http

Public Class CitasDelete
    Private idCita As Integer
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/citas"

    ' Recibimos el ID desde la tabla
    Public Sub New(id As Integer)
        InitializeComponent()
        idCita = id

        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
    End Sub

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Async Sub btn_borrar_Click(sender As Object, e As EventArgs) Handles btn_borrar.Click
        btn_borrar.Enabled = False
        btn_borrar.Text = "Borrando..."

        Try
            ' Le pegamos al endpoint DELETE del backend
            Dim response = Await clienteHttp.DeleteAsync($"{urlBase}/{idCita}")

            If response.IsSuccessStatusCode Then
                MessageBox.Show("La cita ha sido cancelada y la hora ha sido liberada exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("Error al intentar cancelar la cita.", "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btn_borrar.Enabled = True
            btn_borrar.Text = "BORRAR"
        End Try
    End Sub
End Class