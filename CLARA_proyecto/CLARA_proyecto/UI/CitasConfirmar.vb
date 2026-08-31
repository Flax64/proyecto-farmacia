Imports System.Net.Http

Public Class CitasConfirmar
    Private idCita As Integer
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://localhost:5133/api/citas"

    ' Recibimos el ID desde la ventana anterior
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

    Private Async Sub btn_confirmar_Click(sender As Object, e As EventArgs) Handles btn_confirmar.Click
        btn_confirmar.Enabled = False
        btn_confirmar.Text = "Confirmando..."

        Try
            ' Le pegamos a nuestro nuevo atajo en el Backend. Usamos StringContent vacío porque es un PUT ligero.
            Dim content As New StringContent("", System.Text.Encoding.UTF8, "application/json")
            Dim response = Await clienteHttp.PutAsync($"{urlBase}/confirmar/{idCita}", content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show("¡La cita ha sido confirmada exitosamente!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("Error al intentar confirmar la cita.", "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btn_confirmar.Enabled = True
            btn_confirmar.Text = "CONFIRMAR"
        End Try
    End Sub
End Class