<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CambiarPassword2
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
        Label1 = New Label()
        Label2 = New Label()
        txb_newPass = New TextBox()
        Label3 = New Label()
        Label4 = New Label()
        txb_newPassConf = New TextBox()
        btn_cambiar = New Button()
        lblk_regresar = New LinkLabel()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(63, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(388, 37)
        Label1.TabIndex = 2
        Label1.Text = "CREAR NUEVA CONTRASEÑA"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(63, 101)
        Label2.Name = "Label2"
        Label2.Size = New Size(141, 21)
        Label2.TabIndex = 5
        Label2.Text = "Nueva Contraseña:"
        ' 
        ' txb_newPass
        ' 
        txb_newPass.BackColor = Color.Silver
        txb_newPass.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txb_newPass.Location = New Point(63, 125)
        txb_newPass.MaxLength = 50
        txb_newPass.Name = "txb_newPass"
        txb_newPass.Size = New Size(300, 29)
        txb_newPass.TabIndex = 0
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(75, 157)
        Label3.Name = "Label3"
        Label3.Size = New Size(257, 15)
        Label3.TabIndex = 7
        Label3.Text = "(Minimo 8 caracteres, 1 mayúscula y 1 número)"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.Black
        Label4.Location = New Point(63, 204)
        Label4.Name = "Label4"
        Label4.Size = New Size(167, 21)
        Label4.TabIndex = 8
        Label4.Text = "Confirmar Contraseña:"
        ' 
        ' txb_newPassConf
        ' 
        txb_newPassConf.BackColor = Color.Silver
        txb_newPassConf.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txb_newPassConf.Location = New Point(63, 228)
        txb_newPassConf.MaxLength = 50
        txb_newPassConf.Name = "txb_newPassConf"
        txb_newPassConf.Size = New Size(300, 29)
        txb_newPassConf.TabIndex = 1
        ' 
        ' btn_cambiar
        ' 
        btn_cambiar.BackColor = SystemColors.HotTrack
        btn_cambiar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cambiar.ForeColor = Color.Black
        btn_cambiar.Location = New Point(121, 343)
        btn_cambiar.Name = "btn_cambiar"
        btn_cambiar.Size = New Size(258, 43)
        btn_cambiar.TabIndex = 10
        btn_cambiar.Text = "CAMBIAR CONTRASEÑA"
        btn_cambiar.UseVisualStyleBackColor = False
        ' 
        ' lblk_regresar
        ' 
        lblk_regresar.AutoSize = True
        lblk_regresar.LinkColor = Color.Black
        lblk_regresar.Location = New Point(184, 417)
        lblk_regresar.Name = "lblk_regresar"
        lblk_regresar.Size = New Size(128, 15)
        lblk_regresar.TabIndex = 11
        lblk_regresar.TabStop = True
        lblk_regresar.Text = "<Volver a Iniciar Sesión"
        ' 
        ' CambiarPassword2
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(514, 461)
        ControlBox = False
        Controls.Add(lblk_regresar)
        Controls.Add(btn_cambiar)
        Controls.Add(txb_newPassConf)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(txb_newPass)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "CambiarPassword2"
        StartPosition = FormStartPosition.CenterScreen
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txb_newPass As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txb_newPassConf As TextBox
    Friend WithEvents btn_cambiar As Button
    Friend WithEvents lblk_regresar As LinkLabel
End Class
