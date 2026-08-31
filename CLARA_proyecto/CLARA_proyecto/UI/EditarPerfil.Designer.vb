<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditarPerfil
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
        cmb_genero = New ComboBox()
        dtpk_nacimiento = New DateTimePicker()
        txb_paterno = New TextBox()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        txb_email = New TextBox()
        txb_telefono = New TextBox()
        txb_materno = New TextBox()
        txb_nombre = New TextBox()
        Label5 = New Label()
        Label4 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label1 = New Label()
        btn_guardar = New Button()
        btn_cancelar = New Button()
        btn_actualizar_password = New Button()
        SuspendLayout()
        ' 
        ' cmb_genero
        ' 
        cmb_genero.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_genero.FormattingEnabled = True
        cmb_genero.Location = New Point(378, 134)
        cmb_genero.Name = "cmb_genero"
        cmb_genero.Size = New Size(300, 23)
        cmb_genero.TabIndex = 4
        ' 
        ' dtpk_nacimiento
        ' 
        dtpk_nacimiento.CalendarMonthBackground = Color.Tan
        dtpk_nacimiento.Checked = False
        dtpk_nacimiento.Location = New Point(378, 209)
        dtpk_nacimiento.Name = "dtpk_nacimiento"
        dtpk_nacimiento.Size = New Size(300, 23)
        dtpk_nacimiento.TabIndex = 5
        ' 
        ' txb_paterno
        ' 
        txb_paterno.BackColor = Color.Silver
        txb_paterno.Font = New Font("Segoe UI", 12F)
        txb_paterno.Location = New Point(24, 203)
        txb_paterno.MaxLength = 50
        txb_paterno.Name = "txb_paterno"
        txb_paterno.Size = New Size(300, 29)
        txb_paterno.TabIndex = 1
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 12F)
        Label7.ForeColor = Color.Black
        Label7.ImeMode = ImeMode.NoControl
        Label7.Location = New Point(378, 104)
        Label7.Name = "Label7"
        Label7.Size = New Size(64, 21)
        Label7.TabIndex = 41
        Label7.Text = "Género:"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 12F)
        Label8.ForeColor = Color.Black
        Label8.ImeMode = ImeMode.NoControl
        Label8.Location = New Point(378, 179)
        Label8.Name = "Label8"
        Label8.Size = New Size(155, 21)
        Label8.TabIndex = 40
        Label8.Text = "Fecha de Nacimiento"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 12F)
        Label9.ForeColor = Color.Black
        Label9.ImeMode = ImeMode.NoControl
        Label9.Location = New Point(24, 179)
        Label9.Name = "Label9"
        Label9.Size = New Size(127, 21)
        Label9.TabIndex = 39
        Label9.Text = "Apellido Paterno:"
        ' 
        ' txb_email
        ' 
        txb_email.BackColor = Color.Silver
        txb_email.Font = New Font("Segoe UI", 12F)
        txb_email.Location = New Point(378, 284)
        txb_email.MaxLength = 50
        txb_email.Name = "txb_email"
        txb_email.ReadOnly = True
        txb_email.Size = New Size(300, 29)
        txb_email.TabIndex = 33
        ' 
        ' txb_telefono
        ' 
        txb_telefono.BackColor = Color.Silver
        txb_telefono.Font = New Font("Segoe UI", 12F)
        txb_telefono.Location = New Point(24, 359)
        txb_telefono.MaxLength = 10
        txb_telefono.Name = "txb_telefono"
        txb_telefono.Size = New Size(300, 29)
        txb_telefono.TabIndex = 3
        ' 
        ' txb_materno
        ' 
        txb_materno.BackColor = Color.Silver
        txb_materno.Font = New Font("Segoe UI", 12F)
        txb_materno.Location = New Point(24, 284)
        txb_materno.MaxLength = 50
        txb_materno.Name = "txb_materno"
        txb_materno.Size = New Size(300, 29)
        txb_materno.TabIndex = 2
        ' 
        ' txb_nombre
        ' 
        txb_nombre.BackColor = Color.Silver
        txb_nombre.Font = New Font("Segoe UI", 12F)
        txb_nombre.Location = New Point(24, 134)
        txb_nombre.MaxLength = 50
        txb_nombre.Name = "txb_nombre"
        txb_nombre.Size = New Size(300, 29)
        txb_nombre.TabIndex = 0
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F)
        Label5.ForeColor = Color.Black
        Label5.ImeMode = ImeMode.NoControl
        Label5.Location = New Point(378, 260)
        Label5.Name = "Label5"
        Label5.Size = New Size(141, 21)
        Label5.TabIndex = 36
        Label5.Text = "Correo Electrónico:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F)
        Label4.ForeColor = Color.Black
        Label4.ImeMode = ImeMode.NoControl
        Label4.Location = New Point(24, 335)
        Label4.Name = "Label4"
        Label4.Size = New Size(71, 21)
        Label4.TabIndex = 34
        Label4.Text = "Teléfono:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F)
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(24, 260)
        Label2.Name = "Label2"
        Label2.Size = New Size(133, 21)
        Label2.TabIndex = 32
        Label2.Text = "Apellido Materno:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F)
        Label3.ForeColor = Color.Black
        Label3.ImeMode = ImeMode.NoControl
        Label3.Location = New Point(24, 110)
        Label3.Name = "Label3"
        Label3.Size = New Size(71, 21)
        Label3.TabIndex = 30
        Label3.Text = "Nombre:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold)
        Label1.ForeColor = Color.Black
        Label1.ImeMode = ImeMode.NoControl
        Label1.Location = New Point(297, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(145, 37)
        Label1.TabIndex = 27
        Label1.Text = "MI PERFIL"
        ' 
        ' btn_guardar
        ' 
        btn_guardar.BackColor = SystemColors.HotTrack
        btn_guardar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_guardar.ForeColor = Color.Black
        btn_guardar.Location = New Point(457, 465)
        btn_guardar.Margin = New Padding(3, 2, 3, 2)
        btn_guardar.Name = "btn_guardar"
        btn_guardar.Size = New Size(116, 35)
        btn_guardar.TabIndex = 48
        btn_guardar.Text = "GUARDAR"
        btn_guardar.UseVisualStyleBackColor = False
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.ForeColor = Color.Black
        btn_cancelar.Location = New Point(579, 465)
        btn_cancelar.Margin = New Padding(3, 2, 3, 2)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(116, 35)
        btn_cancelar.TabIndex = 49
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' btn_actualizar_password
        ' 
        btn_actualizar_password.BackColor = SystemColors.HotTrack
        btn_actualizar_password.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_actualizar_password.ForeColor = Color.Black
        btn_actualizar_password.Location = New Point(24, 465)
        btn_actualizar_password.Margin = New Padding(3, 2, 3, 2)
        btn_actualizar_password.Name = "btn_actualizar_password"
        btn_actualizar_password.Size = New Size(177, 35)
        btn_actualizar_password.TabIndex = 50
        btn_actualizar_password.Text = "CAMBIAR CONTRASEÑA"
        btn_actualizar_password.UseVisualStyleBackColor = False
        ' 
        ' EditarPerfil
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(734, 511)
        ControlBox = False
        Controls.Add(btn_actualizar_password)
        Controls.Add(btn_cancelar)
        Controls.Add(btn_guardar)
        Controls.Add(cmb_genero)
        Controls.Add(dtpk_nacimiento)
        Controls.Add(txb_paterno)
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
        Name = "EditarPerfil"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents cmb_genero As ComboBox
    Friend WithEvents dtpk_nacimiento As DateTimePicker
    Friend WithEvents txb_paterno As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txb_email As TextBox
    Friend WithEvents txb_telefono As TextBox
    Friend WithEvents txb_materno As TextBox
    Friend WithEvents txb_nombre As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btn_guardar As Button
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents btn_actualizar_password As Button
End Class
