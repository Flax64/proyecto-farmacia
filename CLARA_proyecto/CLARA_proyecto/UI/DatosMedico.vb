Public Class DatosMedico

    ' Variables públicas que el formulario EmpleadosCreate va a leer
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property Cedula As String = ""

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property Especialidad As String = ""
    ' --- BOTÓN ACEPTAR / GUARDAR ---
    Private Sub btn_aceptar_Click(sender As Object, e As EventArgs) Handles btn_aceptar.Click
        ' 1. Validamos que el usuario no deje campos en blanco
        If String.IsNullOrWhiteSpace(txt_cedula.Text) OrElse String.IsNullOrWhiteSpace(txt_especialidad.Text) Then
            MessageBox.Show("Por favor, llena ambos campos para poder registrar al médico.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Guardamos los textos en nuestras variables públicas
        Cedula = txt_cedula.Text.Trim()
        Especialidad = txt_especialidad.Text.Trim()

        ' 3. Le avisamos al sistema que todo salió bien (OK) y cerramos la ventanita
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        ' Le avisamos al sistema que el usuario canceló la acción
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' --- EVENTOS PARA MEJORAR LA EXPERIENCIA (UX) ---

    ' Brincar de Cédula a Especialidad con Enter
    Private Sub txt_cedula_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_cedula.KeyPress
        ' 1. Bloqueamos todo lo que NO sea número ni teclas de control (como borrar)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        ' 2. Brincamos a la especialidad si presiona Enter
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    ' Guardar automáticamente al presionar Enter en Especialidad
    Private Sub txt_especialidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_especialidad.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btn_aceptar.PerformClick() ' Simula un clic en el botón Aceptar
        End If
    End Sub

    Private Sub DatosMedico_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class