<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RolesRead
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
        dgv_roles = New DataGridView()
        lbl_rolSeleccionado = New Label()
        btn_nuevo_rol = New Button()
        cmb_buscar = New ComboBox()
        clb_permisos = New CheckedListBox()
        Label2 = New Label()
        CType(dgv_roles, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(224, 27)
        Label1.Name = "Label1"
        Label1.Size = New Size(260, 37)
        Label1.TabIndex = 0
        Label1.Text = "GESTIÓN DE ROLES"
        ' 
        ' dgv_roles
        ' 
        dgv_roles.AllowUserToAddRows = False
        dgv_roles.AllowUserToResizeColumns = False
        dgv_roles.AllowUserToResizeRows = False
        dgv_roles.BackgroundColor = Color.White
        dgv_roles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_roles.Location = New Point(84, 164)
        dgv_roles.Name = "dgv_roles"
        dgv_roles.Size = New Size(554, 150)
        dgv_roles.TabIndex = 3
        ' 
        ' lbl_rolSeleccionado
        ' 
        lbl_rolSeleccionado.AutoSize = True
        lbl_rolSeleccionado.Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_rolSeleccionado.Location = New Point(84, 317)
        lbl_rolSeleccionado.Name = "lbl_rolSeleccionado"
        lbl_rolSeleccionado.Size = New Size(127, 20)
        lbl_rolSeleccionado.TabIndex = 5
        lbl_rolSeleccionado.Text = "Rol Seleccionado:"
        ' 
        ' btn_nuevo_rol
        ' 
        btn_nuevo_rol.BackColor = SystemColors.HotTrack
        btn_nuevo_rol.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_nuevo_rol.Location = New Point(562, 561)
        btn_nuevo_rol.Name = "btn_nuevo_rol"
        btn_nuevo_rol.Size = New Size(116, 35)
        btn_nuevo_rol.TabIndex = 7
        btn_nuevo_rol.Text = "NUEVO ROL"
        btn_nuevo_rol.UseVisualStyleBackColor = False
        ' 
        ' cmb_buscar
        ' 
        cmb_buscar.FormattingEnabled = True
        cmb_buscar.Location = New Point(84, 104)
        cmb_buscar.Name = "cmb_buscar"
        cmb_buscar.Size = New Size(319, 23)
        cmb_buscar.TabIndex = 8
        ' 
        ' clb_permisos
        ' 
        clb_permisos.FormattingEnabled = True
        clb_permisos.Location = New Point(160, 355)
        clb_permisos.Name = "clb_permisos"
        clb_permisos.Size = New Size(264, 220)
        clb_permisos.TabIndex = 9
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(84, 355)
        Label2.Name = "Label2"
        Label2.Size = New Size(70, 20)
        Label2.TabIndex = 10
        Label2.Text = "Permisos:"
        ' 
        ' RolesRead
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(709, 626)
        Controls.Add(Label2)
        Controls.Add(clb_permisos)
        Controls.Add(cmb_buscar)
        Controls.Add(btn_nuevo_rol)
        Controls.Add(lbl_rolSeleccionado)
        Controls.Add(dgv_roles)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "RolesRead"
        StartPosition = FormStartPosition.CenterParent
        CType(dgv_roles, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents dgv_roles As DataGridView
    Friend WithEvents lbl_rolSeleccionado As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btn_nuevo_rol As Button
    Friend WithEvents cmb_buscar As ComboBox
    Friend WithEvents clb_permisos As CheckedListBox
    Friend WithEvents Label2 As Label
End Class
