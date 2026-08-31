Public Class ComprasDelete
    Private idCompraABorrar As Integer

    '  CREAMOS EL CONSTRUCTOR PARA QUE RECIBA EL ID Y NO MARQUE ERROR
    Public Sub New(id As Integer)
        InitializeComponent()
        idCompraABorrar = id

        ' Opcional: Si tienes un label de título, puedes ponerle el ID
        ' LabelTitulo.Text = "Cancelar Compra #" & idCompraABorrar
    End Sub

    ' Botón BORRAR
    Private Sub btn_Borrar_Click(sender As Object, e As EventArgs) Handles btn_Borrar.Click
        ' Le decimos al sistema que el usuario confirmó
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub

    ' Botón CANCELAR
    Private Sub btn_Cancelar_Click(sender As Object, e As EventArgs) Handles btn_Cancelar.Click
        ' Le decimos al sistema que el usuario canceló
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class