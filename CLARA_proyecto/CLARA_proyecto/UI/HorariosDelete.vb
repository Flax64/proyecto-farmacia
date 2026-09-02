Imports System.Net.Http

Public Class HorariosDelete
    Private idHorario As Integer
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/horarios"

    Public Sub New(id As Integer)
        InitializeComponent()
        idHorario = id

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
        btn_borrar.Text = "BORRANDO..."

        Try
            ' Borrado duro directo a la Base de Datos
            Dim response = Await clienteHttp.DeleteAsync($"{urlBase}/{idHorario}")

            If response.IsSuccessStatusCode Then
                MessageBox.Show("El horario ha sido eliminado permanentemente del sistema.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("No se encontró el horario o no pudo ser eliminado.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al conectar con el servidor.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btn_borrar.Enabled = True
            btn_borrar.Text = "BORRAR"
        End Try
    End Sub
End Class