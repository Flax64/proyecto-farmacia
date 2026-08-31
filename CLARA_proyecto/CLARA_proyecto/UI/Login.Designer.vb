<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Label1 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        txb_password = New TextBox()
        txb_email = New TextBox()
        btn_login = New Button()
        lblk_change_password = New LinkLabel()
        tt_mail = New ToolTip(components)
        tt_password = New ToolTip(components)
        tt_login = New ToolTip(components)
        lblk_sign_in = New LinkLabel()
        pbx_ver = New PictureBox()
        pbx_ocultar = New PictureBox()
        CType(pbx_ver, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbx_ocultar, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        resources.ApplyResources(Label1, "Label1")
        Label1.ForeColor = Color.Black
        Label1.Name = "Label1"
        ' 
        ' Label3
        ' 
        resources.ApplyResources(Label3, "Label3")
        Label3.ForeColor = Color.Black
        Label3.Name = "Label3"
        ' 
        ' Label4
        ' 
        resources.ApplyResources(Label4, "Label4")
        Label4.ForeColor = Color.Black
        Label4.Name = "Label4"
        ' 
        ' txb_password
        ' 
        txb_password.BackColor = Color.Silver
        resources.ApplyResources(txb_password, "txb_password")
        txb_password.Name = "txb_password"
        tt_password.SetToolTip(txb_password, resources.GetString("txb_password.ToolTip"))
        txb_password.UseSystemPasswordChar = True
        ' 
        ' txb_email
        ' 
        txb_email.BackColor = Color.Silver
        resources.ApplyResources(txb_email, "txb_email")
        txb_email.Name = "txb_email"
        tt_mail.SetToolTip(txb_email, resources.GetString("txb_email.ToolTip"))
        ' 
        ' btn_login
        ' 
        btn_login.BackColor = SystemColors.HotTrack
        resources.ApplyResources(btn_login, "btn_login")
        btn_login.ForeColor = Color.Black
        btn_login.Name = "btn_login"
        tt_login.SetToolTip(btn_login, resources.GetString("btn_login.ToolTip"))
        btn_login.UseVisualStyleBackColor = False
        ' 
        ' lblk_change_password
        ' 
        resources.ApplyResources(lblk_change_password, "lblk_change_password")
        lblk_change_password.LinkColor = Color.Black
        lblk_change_password.Name = "lblk_change_password"
        lblk_change_password.TabStop = True
        ' 
        ' lblk_sign_in
        ' 
        resources.ApplyResources(lblk_sign_in, "lblk_sign_in")
        lblk_sign_in.LinkColor = Color.Black
        lblk_sign_in.Name = "lblk_sign_in"
        lblk_sign_in.TabStop = True
        ' 
        ' pbx_ver
        ' 
        resources.ApplyResources(pbx_ver, "pbx_ver")
        pbx_ver.Name = "pbx_ver"
        pbx_ver.TabStop = False
        ' 
        ' pbx_ocultar
        ' 
        resources.ApplyResources(pbx_ocultar, "pbx_ocultar")
        pbx_ocultar.Name = "pbx_ocultar"
        pbx_ocultar.TabStop = False
        ' 
        ' Login
        ' 
        resources.ApplyResources(Me, "$this")
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Control
        Controls.Add(pbx_ocultar)
        Controls.Add(pbx_ver)
        Controls.Add(lblk_sign_in)
        Controls.Add(lblk_change_password)
        Controls.Add(btn_login)
        Controls.Add(txb_email)
        Controls.Add(txb_password)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "Login"
        CType(pbx_ver, ComponentModel.ISupportInitialize).EndInit()
        CType(pbx_ocultar, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txb_password As TextBox
    Friend WithEvents txb_email As TextBox
    Friend WithEvents btn_login As Button
    Friend WithEvents lblk_change_password As LinkLabel
    Friend WithEvents tt_mail As ToolTip
    Friend WithEvents tt_password As ToolTip
    Friend WithEvents tt_login As ToolTip
    Friend WithEvents lblk_sign_in As LinkLabel
    Friend WithEvents pbx_ver As PictureBox
    Friend WithEvents pbx_ocultar As PictureBox

End Class
