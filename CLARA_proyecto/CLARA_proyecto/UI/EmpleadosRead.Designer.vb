<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EmpleadosRead
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
        Label1 = New Label()
        Label2 = New Label()
        btn_nuevo_usuario = New Button()
        dgv_usuarios = New DataGridView()
        cmb_buscar = New ComboBox()
        Panel1 = New Panel()
        lbl_detalle_especialidad = New Label()
        lbl_detalle_cedula = New Label()
        lbl_detalle_email = New Label()
        lbl_detalle_genero = New Label()
        lbl_detalle_fecha = New Label()
        lbl_detalle_telefono = New Label()
        lbl_detalle_nombre = New Label()
        btn_roles = New Button()
        CType(dgv_usuarios, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(239, 33)
        Label1.Name = "Label1"
        Label1.Size = New Size(301, 37)
        Label1.TabIndex = 0
        Label1.Text = "Gestión de Empleados"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(28, 387)
        Label2.Name = "Label2"
        Label2.Size = New Size(177, 21)
        Label2.TabIndex = 4
        Label2.Text = "Empleado Seleccionado:"
        ' 
        ' btn_nuevo_usuario
        ' 
        btn_nuevo_usuario.BackColor = SystemColors.HotTrack
        btn_nuevo_usuario.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_nuevo_usuario.Location = New Point(631, 604)
        btn_nuevo_usuario.Name = "btn_nuevo_usuario"
        btn_nuevo_usuario.Size = New Size(144, 35)
        btn_nuevo_usuario.TabIndex = 7
        btn_nuevo_usuario.Text = "NUEVO USUARIO"
        btn_nuevo_usuario.UseVisualStyleBackColor = False
        ' 
        ' dgv_usuarios
        ' 
        dgv_usuarios.AllowUserToResizeColumns = False
        dgv_usuarios.AllowUserToResizeRows = False
        dgv_usuarios.BackgroundColor = Color.White
        dgv_usuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_usuarios.Location = New Point(28, 162)
        dgv_usuarios.Name = "dgv_usuarios"
        dgv_usuarios.Size = New Size(747, 206)
        dgv_usuarios.TabIndex = 8
        ' 
        ' cmb_buscar
        ' 
        cmb_buscar.FormattingEnabled = True
        cmb_buscar.Location = New Point(30, 111)
        cmb_buscar.Name = "cmb_buscar"
        cmb_buscar.Size = New Size(454, 23)
        cmb_buscar.TabIndex = 9
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = SystemColors.ActiveCaption
        Panel1.Controls.Add(lbl_detalle_especialidad)
        Panel1.Controls.Add(lbl_detalle_cedula)
        Panel1.Controls.Add(lbl_detalle_email)
        Panel1.Controls.Add(lbl_detalle_genero)
        Panel1.Controls.Add(lbl_detalle_fecha)
        Panel1.Controls.Add(lbl_detalle_telefono)
        Panel1.Controls.Add(lbl_detalle_nombre)
        Panel1.Location = New Point(41, 423)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(734, 160)
        Panel1.TabIndex = 10
        ' 
        ' lbl_detalle_especialidad
        ' 
        lbl_detalle_especialidad.AutoSize = True
        lbl_detalle_especialidad.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_detalle_especialidad.Location = New Point(436, 40)
        lbl_detalle_especialidad.Name = "lbl_detalle_especialidad"
        lbl_detalle_especialidad.Size = New Size(98, 21)
        lbl_detalle_especialidad.TabIndex = 17
        lbl_detalle_especialidad.Text = "Especialidad:"
        ' 
        ' lbl_detalle_cedula
        ' 
        lbl_detalle_cedula.AutoSize = True
        lbl_detalle_cedula.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_detalle_cedula.Location = New Point(438, 10)
        lbl_detalle_cedula.Name = "lbl_detalle_cedula"
        lbl_detalle_cedula.Size = New Size(61, 21)
        lbl_detalle_cedula.TabIndex = 16
        lbl_detalle_cedula.Text = "Cedula:"
        ' 
        ' lbl_detalle_email
        ' 
        lbl_detalle_email.AutoSize = True
        lbl_detalle_email.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_detalle_email.Location = New Point(7, 71)
        lbl_detalle_email.Name = "lbl_detalle_email"
        lbl_detalle_email.Size = New Size(65, 21)
        lbl_detalle_email.TabIndex = 15
        lbl_detalle_email.Text = "Correo: "
        ' 
        ' lbl_detalle_genero
        ' 
        lbl_detalle_genero.AutoSize = True
        lbl_detalle_genero.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_detalle_genero.Location = New Point(10, 127)
        lbl_detalle_genero.Name = "lbl_detalle_genero"
        lbl_detalle_genero.Size = New Size(68, 21)
        lbl_detalle_genero.TabIndex = 14
        lbl_detalle_genero.Text = "Genero: "
        ' 
        ' lbl_detalle_fecha
        ' 
        lbl_detalle_fecha.AutoSize = True
        lbl_detalle_fecha.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_detalle_fecha.Location = New Point(7, 99)
        lbl_detalle_fecha.Name = "lbl_detalle_fecha"
        lbl_detalle_fecha.Size = New Size(141, 21)
        lbl_detalle_fecha.TabIndex = 13
        lbl_detalle_fecha.Text = "Fecha Nacimiento: "
        ' 
        ' lbl_detalle_telefono
        ' 
        lbl_detalle_telefono.AutoSize = True
        lbl_detalle_telefono.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_detalle_telefono.Location = New Point(7, 40)
        lbl_detalle_telefono.Name = "lbl_detalle_telefono"
        lbl_detalle_telefono.Size = New Size(71, 21)
        lbl_detalle_telefono.TabIndex = 12
        lbl_detalle_telefono.Text = "Teléfono:"
        ' 
        ' lbl_detalle_nombre
        ' 
        lbl_detalle_nombre.AutoSize = True
        lbl_detalle_nombre.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_detalle_nombre.Location = New Point(7, 10)
        lbl_detalle_nombre.Name = "lbl_detalle_nombre"
        lbl_detalle_nombre.Size = New Size(75, 21)
        lbl_detalle_nombre.TabIndex = 11
        lbl_detalle_nombre.Text = "Nombre: "
        ' 
        ' btn_roles
        ' 
        btn_roles.BackColor = SystemColors.HotTrack
        btn_roles.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_roles.Location = New Point(646, 12)
        btn_roles.Name = "btn_roles"
        btn_roles.Size = New Size(144, 35)
        btn_roles.TabIndex = 11
        btn_roles.Text = "GESTIONAR ROLES"
        btn_roles.UseVisualStyleBackColor = False
        ' 
        ' EmpleadosRead
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(802, 651)
        Controls.Add(btn_roles)
        Controls.Add(Panel1)
        Controls.Add(cmb_buscar)
        Controls.Add(dgv_usuarios)
        Controls.Add(btn_nuevo_usuario)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "EmpleadosRead"
        StartPosition = FormStartPosition.CenterParent
        CType(dgv_usuarios, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_nuevo_usuario As Button
    Friend WithEvents dgv_usuarios As DataGridView
    Friend WithEvents cmb_buscar As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_detalle_nombre As Label
    Friend WithEvents lbl_detalle_telefono As Label
    Friend WithEvents lbl_detalle_fecha As Label
    Friend WithEvents lbl_detalle_genero As Label
    Friend WithEvents lbl_detalle_email As Label
    Friend WithEvents btn_roles As Button
    Friend WithEvents lbl_detalle_cedula As Label
    Friend WithEvents lbl_detalle_especialidad As Label
End Class
