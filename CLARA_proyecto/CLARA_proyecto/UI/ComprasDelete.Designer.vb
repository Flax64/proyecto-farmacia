<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ComprasDelete
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        btn_Borrar = New Button()
        btn_Cancelar = New Button()
        Label2 = New Label()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' btn_Borrar
        ' 
        btn_Borrar.BackColor = SystemColors.HotTrack
        btn_Borrar.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_Borrar.ForeColor = Color.Black
        btn_Borrar.ImeMode = ImeMode.NoControl
        btn_Borrar.Location = New Point(33, 100)
        btn_Borrar.Name = "btn_Borrar"
        btn_Borrar.Size = New Size(112, 37)
        btn_Borrar.TabIndex = 28
        btn_Borrar.Text = "BORRAR"
        btn_Borrar.UseVisualStyleBackColor = False
        ' 
        ' btn_Cancelar
        ' 
        btn_Cancelar.BackColor = SystemColors.HotTrack
        btn_Cancelar.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_Cancelar.ForeColor = Color.Black
        btn_Cancelar.ImeMode = ImeMode.NoControl
        btn_Cancelar.Location = New Point(151, 100)
        btn_Cancelar.Name = "btn_Cancelar"
        btn_Cancelar.Size = New Size(112, 37)
        btn_Cancelar.TabIndex = 27
        btn_Cancelar.Text = "CANCELAR"
        btn_Cancelar.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(23, 46)
        Label2.Name = "Label2"
        Label2.Size = New Size(240, 20)
        Label2.TabIndex = 26
        Label2.Text = "Esta acción no se puede dehsaser"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.ImeMode = ImeMode.NoControl
        Label1.Location = New Point(18, 4)
        Label1.Name = "Label1"
        Label1.Size = New Size(264, 25)
        Label1.TabIndex = 25
        Label1.Text = "¿Deseas borrar esta compra?"
        ' 
        ' ComprasDelete
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(301, 141)
        ControlBox = False
        Controls.Add(btn_Borrar)
        Controls.Add(btn_Cancelar)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "ComprasDelete"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btn_Borrar As Button
    Friend WithEvents btn_Cancelar As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
