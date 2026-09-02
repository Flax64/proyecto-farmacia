Imports System.Net.Http
Imports System.Text.Json

Public Class MedicamentosDelete
    ' Variable para guardar el ID del medicamento a desactivar
    Private idMedicamentoSeleccionado As Integer
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/medicamentos"

    ' --- CONSTRUCTOR ---
    ' Recibe el ID desde la tabla principal al hacer clic en el basurero
    Public Sub New(idMed As Integer)
        InitializeComponent()
        idMedicamentoSeleccionado = idMed
    End Sub

    Private Sub MedicamentosDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuramos el cliente HTTP ignorando certificados locales
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
    End Sub

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_Cancelar.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' --- BOTÓN BORRAR ---
    Private Async Sub btn_borrar_Click(sender As Object, e As EventArgs) Handles btn_Borrar.Click
        btn_Borrar.Enabled = False
        btn_Borrar.Text = "Borrando..."

        Try
            ' Hacemos la petición DELETE a la API (que C# convertirá en Estatus Inactivo)
            Dim response = Await clienteHttp.DeleteAsync($"{urlBase}/{idMedicamentoSeleccionado}")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Medicamento dado de baja exitosamente (Estatus: Inactivo).", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Le decimos a la pantalla principal que todo salió bien para que recargue la tabla
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "No se pudo dar de baja el medicamento."
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

                MessageBox.Show(errorMsg, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' Restauramos el botón por si quiere intentar otra vez o cancelar
                btn_Borrar.Enabled = True
                btn_Borrar.Text = "BORRAR"
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btn_Borrar.Enabled = True
            btn_Borrar.Text = "BORRAR"
        End Try
    End Sub
End Class