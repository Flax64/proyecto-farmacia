<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EmpleadosCreate
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EmpleadosCreate))
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        txt_nombre = New TextBox()
        txt_apPaterno = New TextBox()
        txt_apMaterno = New TextBox()
        dtp_fechaNac = New DateTimePicker()
        btn_guardar = New Button()
        btn_cancelar = New Button()
        Label11 = New Label()
        cmb_rol = New ComboBox()
        cmb_genero = New ComboBox()
        cmb_estatus = New ComboBox()
        Label12 = New Label()
        txt_telefono = New TextBox()
        txt_email = New TextBox()
        txt_password = New TextBox()
        pbx_ver = New PictureBox()
        pbx_ocultar = New PictureBox()
        Label13 = New Label()
        CType(pbx_ver, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbx_ocultar, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(176, 18)
        Label1.Name = "Label1"
        Label1.Size = New Size(344, 37)
        Label1.TabIndex = 0
        Label1.Text = "CREACIÓN DE EMPLEADO"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(12, 72)
        Label2.Name = "Label2"
        Label2.Size = New Size(302, 30)
        Label2.TabIndex = 1
        Label2.Text = "Ingrese los datos del empleado"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(63, 140)
        Label3.Name = "Label3"
        Label3.Size = New Size(71, 21)
        Label3.TabIndex = 2
        Label3.Text = "Nombre:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(7, 213)
        Label4.Name = "Label4"
        Label4.Size = New Size(127, 21)
        Label4.TabIndex = 3
        Label4.Text = "Apellido Paterno:"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(1, 279)
        Label5.Name = "Label5"
        Label5.Size = New Size(133, 21)
        Label5.TabIndex = 4
        Label5.Text = "Apellido Materno:"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(12, 343)
        Label6.Name = "Label6"
        Label6.Size = New Size(158, 21)
        Label6.TabIndex = 5
        Label6.Text = "Fecha de Nacimiento:"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(17, 395)
        Label7.Name = "Label7"
        Label7.Size = New Size(64, 21)
        Label7.TabIndex = 6
        Label7.Text = "Género:"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(377, 147)
        Label8.Name = "Label8"
        Label8.Size = New Size(71, 21)
        Label8.TabIndex = 7
        Label8.Text = "Teléfono:"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(387, 205)
        Label9.Name = "Label9"
        Label9.Size = New Size(61, 21)
        Label9.TabIndex = 8
        Label9.Text = "Correo:"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(390, 338)
        Label10.Name = "Label10"
        Label10.Size = New Size(36, 21)
        Label10.TabIndex = 9
        Label10.Text = "Rol:"
        ' 
        ' txt_nombre
        ' 
        txt_nombre.BackColor = Color.Silver
        txt_nombre.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_nombre.Location = New Point(140, 139)
        txt_nombre.MaxLength = 40
        txt_nombre.Name = "txt_nombre"
        txt_nombre.Size = New Size(204, 29)
        txt_nombre.TabIndex = 10
        ' 
        ' txt_apPaterno
        ' 
        txt_apPaterno.BackColor = Color.Silver
        txt_apPaterno.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_apPaterno.Location = New Point(140, 208)
        txt_apPaterno.MaxLength = 30
        txt_apPaterno.Name = "txt_apPaterno"
        txt_apPaterno.Size = New Size(204, 29)
        txt_apPaterno.TabIndex = 11
        ' 
        ' txt_apMaterno
        ' 
        txt_apMaterno.BackColor = Color.Silver
        txt_apMaterno.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_apMaterno.Location = New Point(140, 273)
        txt_apMaterno.MaxLength = 30
        txt_apMaterno.Name = "txt_apMaterno"
        txt_apMaterno.Size = New Size(204, 29)
        txt_apMaterno.TabIndex = 12
        ' 
        ' dtp_fechaNac
        ' 
        dtp_fechaNac.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtp_fechaNac.Format = DateTimePickerFormat.Short
        dtp_fechaNac.Location = New Point(176, 342)
        dtp_fechaNac.Name = "dtp_fechaNac"
        dtp_fechaNac.Size = New Size(124, 25)
        dtp_fechaNac.TabIndex = 13
        ' 
        ' btn_guardar
        ' 
        btn_guardar.BackColor = SystemColors.HotTrack
        btn_guardar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_guardar.Location = New Point(419, 459)
        btn_guardar.Name = "btn_guardar"
        btn_guardar.Size = New Size(116, 35)
        btn_guardar.TabIndex = 18
        btn_guardar.Text = "CREAR"
        btn_guardar.UseVisualStyleBackColor = False
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.Location = New Point(541, 459)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(116, 35)
        btn_cancelar.TabIndex = 19
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label11.Location = New Point(356, 279)
        Label11.Name = "Label11"
        Label11.Size = New Size(92, 21)
        Label11.TabIndex = 21
        Label11.Text = "Contraseña:"
        ' 
        ' cmb_rol
        ' 
        cmb_rol.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_rol.FormattingEnabled = True
        cmb_rol.Location = New Point(432, 340)
        cmb_rol.Name = "cmb_rol"
        cmb_rol.Size = New Size(205, 23)
        cmb_rol.TabIndex = 22
        ' 
        ' cmb_genero
        ' 
        cmb_genero.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_genero.FormattingEnabled = True
        cmb_genero.Location = New Point(87, 395)
        cmb_genero.Name = "cmb_genero"
        cmb_genero.Size = New Size(210, 23)
        cmb_genero.TabIndex = 23
        ' 
        ' cmb_estatus
        ' 
        cmb_estatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_estatus.FormattingEnabled = True
        cmb_estatus.Location = New Point(432, 394)
        cmb_estatus.Name = "cmb_estatus"
        cmb_estatus.Size = New Size(205, 23)
        cmb_estatus.TabIndex = 24
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.Location = New Point(364, 394)
        Label12.Name = "Label12"
        Label12.Size = New Size(62, 21)
        Label12.TabIndex = 25
        Label12.Text = "Estatus:"
        ' 
        ' txt_telefono
        ' 
        txt_telefono.BackColor = Color.Silver
        txt_telefono.Font = New Font("Segoe UI", 12F)
        txt_telefono.Location = New Point(452, 136)
        txt_telefono.MaxLength = 10
        txt_telefono.Name = "txt_telefono"
        txt_telefono.Size = New Size(205, 29)
        txt_telefono.TabIndex = 26
        ' 
        ' txt_email
        ' 
        txt_email.BackColor = Color.Silver
        txt_email.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_email.Location = New Point(452, 202)
        txt_email.MaxLength = 70
        txt_email.Name = "txt_email"
        txt_email.Size = New Size(204, 29)
        txt_email.TabIndex = 16
        ' 
        ' txt_password
        ' 
        txt_password.BackColor = Color.Silver
        txt_password.Font = New Font("Segoe UI", 12F)
        txt_password.Location = New Point(451, 273)
        txt_password.MaxLength = 50
        txt_password.Name = "txt_password"
        txt_password.Size = New Size(205, 29)
        txt_password.TabIndex = 27
        txt_password.UseSystemPasswordChar = True
        ' 
        ' pbx_ver
        ' 
        pbx_ver.Image = CType(resources.GetObject("pbx_ver.Image"), Image)
        pbx_ver.ImeMode = ImeMode.NoControl
        pbx_ver.Location = New Point(663, 271)
        pbx_ver.Name = "pbx_ver"
        pbx_ver.Size = New Size(29, 29)
        pbx_ver.SizeMode = PictureBoxSizeMode.StretchImage
        pbx_ver.TabIndex = 28
        pbx_ver.TabStop = False
        ' 
        ' pbx_ocultar
        ' 
        pbx_ocultar.Image = CType(resources.GetObject("pbx_ocultar.Image"), Image)
        pbx_ocultar.ImeMode = ImeMode.NoControl
        pbx_ocultar.Location = New Point(663, 271)
        pbx_ocultar.Name = "pbx_ocultar"
        pbx_ocultar.Size = New Size(29, 29)
        pbx_ocultar.SizeMode = PictureBoxSizeMode.StretchImage
        pbx_ocultar.TabIndex = 29
        pbx_ocultar.TabStop = False
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label13.ForeColor = Color.Black
        Label13.Location = New Point(432, 305)
        Label13.Name = "Label13"
        Label13.Size = New Size(257, 15)
        Label13.TabIndex = 30
        Label13.Text = "(Minimo 8 caracteres, 1 mayúscula y 1 número)"
        ' 
        ' EmpleadosCreate
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(727, 521)
        ControlBox = False
        Controls.Add(Label13)
        Controls.Add(pbx_ocultar)
        Controls.Add(pbx_ver)
        Controls.Add(txt_password)
        Controls.Add(txt_telefono)
        Controls.Add(Label12)
        Controls.Add(cmb_estatus)
        Controls.Add(cmb_genero)
        Controls.Add(cmb_rol)
        Controls.Add(Label11)
        Controls.Add(btn_cancelar)
        Controls.Add(btn_guardar)
        Controls.Add(txt_email)
        Controls.Add(dtp_fechaNac)
        Controls.Add(txt_apMaterno)
        Controls.Add(txt_apPaterno)
        Controls.Add(txt_nombre)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "EmpleadosCreate"
        StartPosition = FormStartPosition.CenterParent
        CType(pbx_ver, ComponentModel.ISupportInitialize).EndInit()
        CType(pbx_ocultar, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_nombre As TextBox
    Friend WithEvents txt_apPaterno As TextBox
    Friend WithEvents txt_apMaterno As TextBox
    Friend WithEvents dtp_fechaNac As DateTimePicker
    Friend WithEvents btn_guardar As Button
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents cmb_rol As ComboBox
    Friend WithEvents cmb_genero As ComboBox
    Friend WithEvents cmb_estatus As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txt_telefono As TextBox
    Friend WithEvents txt_email As TextBox
    Friend WithEvents txt_password As TextBox
    Friend WithEvents pbx_ver As PictureBox
    Friend WithEvents pbx_ocultar As PictureBox
    Friend WithEvents Label13 As Label
End Class
