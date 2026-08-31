<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CambiarPassword1
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
        Label3 = New Label()
        Label2 = New Label()
        txb_email = New TextBox()
        btn_enviar = New Button()
        lblk_change_password = New LinkLabel()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(81, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(359, 37)
        Label1.TabIndex = 1
        Label1.Text = "RECUPERAR CONTRASEÑA"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(34, 74)
        Label3.Name = "Label3"
        Label3.Size = New Size(454, 42)
        Label3.TabIndex = 3
        Label3.Text = "Ingrese el correo electrónico asociado a su cuenta." & vbCrLf & "Le enviaremos un enlace para crear una nueva contraseña."
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(34, 181)
        Label2.Name = "Label2"
        Label2.Size = New Size(141, 21)
        Label2.TabIndex = 4
        Label2.Text = "Correo Electrónico:"
        ' 
        ' txb_email
        ' 
        txb_email.BackColor = Color.Silver
        txb_email.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txb_email.Location = New Point(34, 205)
        txb_email.MaxLength = 50
        txb_email.Name = "txb_email"
        txb_email.Size = New Size(300, 29)
        txb_email.TabIndex = 0
        ' 
        ' btn_enviar
        ' 
        btn_enviar.BackColor = SystemColors.HotTrack
        btn_enviar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_enviar.ForeColor = Color.Black
        btn_enviar.Location = New Point(121, 318)
        btn_enviar.Name = "btn_enviar"
        btn_enviar.Size = New Size(258, 43)
        btn_enviar.TabIndex = 1
        btn_enviar.Text = "ENVIAR ENLACE"
        btn_enviar.UseVisualStyleBackColor = False
        ' 
        ' lblk_change_password
        ' 
        lblk_change_password.AutoSize = True
        lblk_change_password.LinkColor = Color.Black
        lblk_change_password.Location = New Point(185, 374)
        lblk_change_password.Name = "lblk_change_password"
        lblk_change_password.Size = New Size(128, 15)
        lblk_change_password.TabIndex = 8
        lblk_change_password.TabStop = True
        lblk_change_password.Text = "<Volver a Iniciar Sesión"
        ' 
        ' CambiarPassword1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Control
        ClientSize = New Size(514, 461)
        ControlBox = False
        Controls.Add(lblk_change_password)
        Controls.Add(btn_enviar)
        Controls.Add(txb_email)
        Controls.Add(Label2)
        Controls.Add(Label3)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "CambiarPassword1"
        StartPosition = FormStartPosition.CenterScreen
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txb_email As TextBox
    Friend WithEvents btn_enviar As Button
    Friend WithEvents lblk_change_password As LinkLabel
End Class
