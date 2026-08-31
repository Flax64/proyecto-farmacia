<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Registrar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Registrar))
        Label1 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        txb_nombre = New TextBox()
        txb_materno = New TextBox()
        txb_telefono = New TextBox()
        txb_email = New TextBox()
        txb_password = New TextBox()
        txb_paterno = New TextBox()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        dtpk_nacimiento = New DateTimePicker()
        cbx_genero = New ComboBox()
        lblk_login = New LinkLabel()
        btn_registrar = New Button()
        Label10 = New Label()
        pbx_ocultar = New PictureBox()
        pbx_ver = New PictureBox()
        CType(pbx_ocultar, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbx_ver, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold)
        Label1.ForeColor = Color.Black
        Label1.ImeMode = ImeMode.NoControl
        Label1.Location = New Point(224, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(275, 37)
        Label1.TabIndex = 1
        Label1.Text = "REGISTRAR CUENTA"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F)
        Label3.ForeColor = Color.Black
        Label3.ImeMode = ImeMode.NoControl
        Label3.Location = New Point(35, 102)
        Label3.Name = "Label3"
        Label3.Size = New Size(71, 21)
        Label3.TabIndex = 3
        Label3.Text = "Nombre:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F)
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(35, 252)
        Label2.Name = "Label2"
        Label2.Size = New Size(133, 21)
        Label2.TabIndex = 4
        Label2.Text = "Apellido Materno:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F)
        Label4.ForeColor = Color.Black
        Label4.ImeMode = ImeMode.NoControl
        Label4.Location = New Point(35, 327)
        Label4.Name = "Label4"
        Label4.Size = New Size(71, 21)
        Label4.TabIndex = 5
        Label4.Text = "Teléfono:"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F)
        Label5.ForeColor = Color.Black
        Label5.ImeMode = ImeMode.NoControl
        Label5.Location = New Point(389, 252)
        Label5.Name = "Label5"
        Label5.Size = New Size(141, 21)
        Label5.TabIndex = 6
        Label5.Text = "Correo Electrónico:"
        ' 
        ' txb_nombre
        ' 
        txb_nombre.BackColor = Color.Silver
        txb_nombre.Font = New Font("Segoe UI", 12F)
        txb_nombre.Location = New Point(35, 126)
        txb_nombre.MaxLength = 50
        txb_nombre.Name = "txb_nombre"
        txb_nombre.Size = New Size(300, 29)
        txb_nombre.TabIndex = 0
        ' 
        ' txb_materno
        ' 
        txb_materno.BackColor = Color.Silver
        txb_materno.Font = New Font("Segoe UI", 12F)
        txb_materno.Location = New Point(35, 276)
        txb_materno.MaxLength = 50
        txb_materno.Name = "txb_materno"
        txb_materno.Size = New Size(300, 29)
        txb_materno.TabIndex = 2
        ' 
        ' txb_telefono
        ' 
        txb_telefono.BackColor = Color.Silver
        txb_telefono.Font = New Font("Segoe UI", 12F)
        txb_telefono.Location = New Point(35, 351)
        txb_telefono.MaxLength = 10
        txb_telefono.Name = "txb_telefono"
        txb_telefono.Size = New Size(300, 29)
        txb_telefono.TabIndex = 3
        ' 
        ' txb_email
        ' 
        txb_email.BackColor = Color.Silver
        txb_email.Font = New Font("Segoe UI", 12F)
        txb_email.Location = New Point(389, 276)
        txb_email.MaxLength = 50
        txb_email.Name = "txb_email"
        txb_email.Size = New Size(300, 29)
        txb_email.TabIndex = 4
        ' 
        ' txb_password
        ' 
        txb_password.BackColor = Color.Silver
        txb_password.Font = New Font("Segoe UI", 12F)
        txb_password.Location = New Point(389, 351)
        txb_password.MaxLength = 50
        txb_password.Name = "txb_password"
        txb_password.Size = New Size(300, 29)
        txb_password.TabIndex = 5
        txb_password.UseSystemPasswordChar = True
        ' 
        ' txb_paterno
        ' 
        txb_paterno.BackColor = Color.Silver
        txb_paterno.Font = New Font("Segoe UI", 12F)
        txb_paterno.Location = New Point(35, 195)
        txb_paterno.MaxLength = 50
        txb_paterno.Name = "txb_paterno"
        txb_paterno.Size = New Size(300, 29)
        txb_paterno.TabIndex = 1
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 12F)
        Label6.ForeColor = Color.Black
        Label6.ImeMode = ImeMode.NoControl
        Label6.Location = New Point(389, 327)
        Label6.Name = "Label6"
        Label6.Size = New Size(92, 21)
        Label6.TabIndex = 14
        Label6.Text = "Contraseña:"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 12F)
        Label7.ForeColor = Color.Black
        Label7.ImeMode = ImeMode.NoControl
        Label7.Location = New Point(389, 96)
        Label7.Name = "Label7"
        Label7.Size = New Size(64, 21)
        Label7.TabIndex = 13
        Label7.Text = "Género:"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 12F)
        Label8.ForeColor = Color.Black
        Label8.ImeMode = ImeMode.NoControl
        Label8.Location = New Point(389, 171)
        Label8.Name = "Label8"
        Label8.Size = New Size(155, 21)
        Label8.TabIndex = 12
        Label8.Text = "Fecha de Nacimiento"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 12F)
        Label9.ForeColor = Color.Black
        Label9.ImeMode = ImeMode.NoControl
        Label9.Location = New Point(35, 171)
        Label9.Name = "Label9"
        Label9.Size = New Size(127, 21)
        Label9.TabIndex = 11
        Label9.Text = "Apellido Paterno:"
        ' 
        ' dtpk_nacimiento
        ' 
        dtpk_nacimiento.CalendarMonthBackground = Color.Tan
        dtpk_nacimiento.Checked = False
        dtpk_nacimiento.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtpk_nacimiento.Location = New Point(389, 201)
        dtpk_nacimiento.Name = "dtpk_nacimiento"
        dtpk_nacimiento.Size = New Size(300, 25)
        dtpk_nacimiento.TabIndex = 6
        ' 
        ' cbx_genero
        ' 
        cbx_genero.DropDownStyle = ComboBoxStyle.DropDownList
        cbx_genero.FormattingEnabled = True
        cbx_genero.Location = New Point(389, 126)
        cbx_genero.Name = "cbx_genero"
        cbx_genero.Size = New Size(300, 23)
        cbx_genero.TabIndex = 10
        ' 
        ' lblk_login
        ' 
        lblk_login.AutoSize = True
        lblk_login.ImeMode = ImeMode.NoControl
        lblk_login.LinkColor = Color.Black
        lblk_login.Location = New Point(366, 460)
        lblk_login.Name = "lblk_login"
        lblk_login.Size = New Size(76, 15)
        lblk_login.TabIndex = 21
        lblk_login.TabStop = True
        lblk_login.Text = "Iniciar Sesión"
        lblk_login.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_registrar
        ' 
        btn_registrar.BackColor = SystemColors.HotTrack
        btn_registrar.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold)
        btn_registrar.ForeColor = Color.Black
        btn_registrar.ImeMode = ImeMode.NoControl
        btn_registrar.Location = New Point(107, 399)
        btn_registrar.Name = "btn_registrar"
        btn_registrar.Size = New Size(495, 43)
        btn_registrar.TabIndex = 22
        btn_registrar.Text = "REGISTRARSE"
        btn_registrar.UseVisualStyleBackColor = False
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.ForeColor = Color.Black
        Label10.ImeMode = ImeMode.NoControl
        Label10.Location = New Point(272, 460)
        Label10.Name = "Label10"
        Label10.Size = New Size(95, 15)
        Label10.TabIndex = 23
        Label10.Text = "Ya tengo cuenta."
        ' 
        ' pbx_ocultar
        ' 
        pbx_ocultar.Image = CType(resources.GetObject("pbx_ocultar.Image"), Image)
        pbx_ocultar.ImeMode = ImeMode.NoControl
        pbx_ocultar.Location = New Point(693, 351)
        pbx_ocultar.Name = "pbx_ocultar"
        pbx_ocultar.Size = New Size(29, 29)
        pbx_ocultar.SizeMode = PictureBoxSizeMode.StretchImage
        pbx_ocultar.TabIndex = 24
        pbx_ocultar.TabStop = False
        ' 
        ' pbx_ver
        ' 
        pbx_ver.Image = CType(resources.GetObject("pbx_ver.Image"), Image)
        pbx_ver.ImeMode = ImeMode.NoControl
        pbx_ver.Location = New Point(693, 351)
        pbx_ver.Name = "pbx_ver"
        pbx_ver.Size = New Size(29, 29)
        pbx_ver.SizeMode = PictureBoxSizeMode.StretchImage
        pbx_ver.TabIndex = 25
        pbx_ver.TabStop = False
        ' 
        ' Registrar
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(734, 511)
        ControlBox = False
        Controls.Add(pbx_ver)
        Controls.Add(pbx_ocultar)
        Controls.Add(Label10)
        Controls.Add(btn_registrar)
        Controls.Add(lblk_login)
        Controls.Add(cbx_genero)
        Controls.Add(dtpk_nacimiento)
        Controls.Add(txb_password)
        Controls.Add(txb_paterno)
        Controls.Add(Label6)
        Controls.Add(Label7)
        Controls.Add(Label8)
        Controls.Add(Label9)
        Controls.Add(txb_email)
        Controls.Add(txb_telefono)
        Controls.Add(txb_materno)
        Controls.Add(txb_nombre)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label2)
        Controls.Add(Label3)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "Registrar"
        StartPosition = FormStartPosition.CenterParent
        CType(pbx_ocultar, ComponentModel.ISupportInitialize).EndInit()
        CType(pbx_ver, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txb_nombre As TextBox
    Friend WithEvents txb_materno As TextBox
    Friend WithEvents txb_telefono As TextBox
    Friend WithEvents txb_email As TextBox
    Friend WithEvents txb_password As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents txb_paterno As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents dtpk_nacimiento As DateTimePicker
    Friend WithEvents cbx_genero As ComboBox
    Friend WithEvents lblk_login As LinkLabel
    Friend WithEvents btn_registrar As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents pbx_ocultar As PictureBox
    Friend WithEvents pbx_ver As PictureBox
End Class
