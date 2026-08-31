<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HorariosDelete
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
        btn_borrar = New Button()
        btn_cancelar = New Button()
        Label2 = New Label()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' btn_borrar
        ' 
        btn_borrar.BackColor = SystemColors.HotTrack
        btn_borrar.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_borrar.ForeColor = Color.Black
        btn_borrar.ImeMode = ImeMode.NoControl
        btn_borrar.Location = New Point(38, 96)
        btn_borrar.Name = "btn_borrar"
        btn_borrar.Size = New Size(112, 37)
        btn_borrar.TabIndex = 36
        btn_borrar.Text = "BORRAR"
        btn_borrar.UseVisualStyleBackColor = False
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.ForeColor = Color.Black
        btn_cancelar.ImeMode = ImeMode.NoControl
        btn_cancelar.Location = New Point(156, 96)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(112, 37)
        btn_cancelar.TabIndex = 35
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(34, 46)
        Label2.Name = "Label2"
        Label2.Size = New Size(240, 20)
        Label2.TabIndex = 34
        Label2.Text = "Esta acción no se puede dehsaser"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.ImeMode = ImeMode.NoControl
        Label1.Location = New Point(22, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(262, 25)
        Label1.TabIndex = 33
        Label1.Text = "¿Deseas borrar este horario?"
        ' 
        ' HorariosDelete
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(301, 141)
        ControlBox = False
        Controls.Add(btn_borrar)
        Controls.Add(btn_cancelar)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "HorariosDelete"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btn_borrar As Button
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
