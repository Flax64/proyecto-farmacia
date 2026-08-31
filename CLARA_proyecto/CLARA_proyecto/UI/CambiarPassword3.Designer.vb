<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CambiarPassword3
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
        btn_guardar = New Button()
        txb_newPassConf = New TextBox()
        Label4 = New Label()
        Label3 = New Label()
        txb_newPass = New TextBox()
        Label2 = New Label()
        Label1 = New Label()
        Label5 = New Label()
        txb_passActual = New TextBox()
        btn_cancelar = New Button()
        SuspendLayout()
        ' 
        ' btn_guardar
        ' 
        btn_guardar.BackColor = SystemColors.HotTrack
        btn_guardar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_guardar.ForeColor = Color.Black
        btn_guardar.Location = New Point(53, 378)
        btn_guardar.Name = "btn_guardar"
        btn_guardar.Size = New Size(123, 37)
        btn_guardar.TabIndex = 4
        btn_guardar.Text = "GUARDAR"
        btn_guardar.UseVisualStyleBackColor = False
        ' 
        ' txb_newPassConf
        ' 
        txb_newPassConf.BackColor = Color.Silver
        txb_newPassConf.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txb_newPassConf.Location = New Point(53, 285)
        txb_newPassConf.MaxLength = 50
        txb_newPassConf.Name = "txb_newPassConf"
        txb_newPassConf.Size = New Size(300, 29)
        txb_newPassConf.TabIndex = 2
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.Black
        Label4.Location = New Point(53, 261)
        Label4.Name = "Label4"
        Label4.Size = New Size(167, 21)
        Label4.TabIndex = 16
        Label4.Text = "Confirmar Contraseña:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(65, 223)
        Label3.Name = "Label3"
        Label3.Size = New Size(257, 15)
        Label3.TabIndex = 15
        Label3.Text = "(Minimo 8 caracteres, 1 mayúscula y 1 número)"
        ' 
        ' txb_newPass
        ' 
        txb_newPass.BackColor = Color.Silver
        txb_newPass.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txb_newPass.Location = New Point(53, 191)
        txb_newPass.MaxLength = 50
        txb_newPass.Name = "txb_newPass"
        txb_newPass.Size = New Size(300, 29)
        txb_newPass.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(53, 167)
        Label2.Name = "Label2"
        Label2.Size = New Size(141, 21)
        Label2.TabIndex = 14
        Label2.Text = "Nueva Contraseña:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(58, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(388, 37)
        Label1.TabIndex = 13
        Label1.Text = "CREAR NUEVA CONTRASEÑA"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.Black
        Label5.Location = New Point(53, 84)
        Label5.Name = "Label5"
        Label5.Size = New Size(139, 21)
        Label5.TabIndex = 18
        Label5.Text = "Contraseña Actual:"
        ' 
        ' txb_passActual
        ' 
        txb_passActual.BackColor = Color.Silver
        txb_passActual.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txb_passActual.Location = New Point(53, 108)
        txb_passActual.MaxLength = 50
        txb_passActual.Name = "txb_passActual"
        txb_passActual.Size = New Size(300, 29)
        txb_passActual.TabIndex = 0
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.ForeColor = Color.Black
        btn_cancelar.Location = New Point(199, 378)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(123, 37)
        btn_cancelar.TabIndex = 7
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' CambiarPassword3
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(514, 447)
        ControlBox = False
        Controls.Add(btn_cancelar)
        Controls.Add(txb_passActual)
        Controls.Add(Label5)
        Controls.Add(btn_guardar)
        Controls.Add(txb_newPassConf)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(txb_newPass)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "CambiarPassword3"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btn_guardar As Button
    Friend WithEvents txb_newPassConf As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txb_newPass As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txb_passActual As TextBox
    Friend WithEvents btn_cancelar As Button
End Class
