<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RolesUpdate
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
        btn_modificar = New Button()
        btn_cancelar = New Button()
        clb_permisos = New CheckedListBox()
        txt_nombre = New TextBox()
        Label3 = New Label()
        Lb1 = New Label()
        Label2 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(162, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(347, 37)
        Label1.TabIndex = 0
        Label1.Text = "MODIFICACIÓN DE ROLES"
        ' 
        ' btn_modificar
        ' 
        btn_modificar.BackColor = SystemColors.HotTrack
        btn_modificar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_modificar.Location = New Point(367, 513)
        btn_modificar.Name = "btn_modificar"
        btn_modificar.Size = New Size(116, 35)
        btn_modificar.TabIndex = 6
        btn_modificar.Text = "MODIFICAR"
        btn_modificar.UseVisualStyleBackColor = False
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.Location = New Point(489, 513)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(116, 35)
        btn_cancelar.TabIndex = 7
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' clb_permisos
        ' 
        clb_permisos.FormattingEnabled = True
        clb_permisos.Location = New Point(305, 242)
        clb_permisos.Name = "clb_permisos"
        clb_permisos.Size = New Size(264, 220)
        clb_permisos.TabIndex = 75
        ' 
        ' txt_nombre
        ' 
        txt_nombre.BackColor = Color.Silver
        txt_nombre.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_nombre.Location = New Point(305, 173)
        txt_nombre.MaxLength = 50
        txt_nombre.Name = "txt_nombre"
        txt_nombre.Size = New Size(300, 29)
        txt_nombre.TabIndex = 74
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F)
        Label3.ForeColor = Color.Black
        Label3.ImeMode = ImeMode.NoControl
        Label3.Location = New Point(223, 242)
        Label3.Name = "Label3"
        Label3.Size = New Size(76, 21)
        Label3.TabIndex = 73
        Label3.Text = "Permisos:"
        ' 
        ' Lb1
        ' 
        Lb1.AutoSize = True
        Lb1.Font = New Font("Segoe UI", 12F)
        Lb1.ForeColor = Color.Black
        Lb1.ImeMode = ImeMode.NoControl
        Lb1.Location = New Point(180, 176)
        Lb1.Name = "Lb1"
        Lb1.Size = New Size(119, 21)
        Lb1.TabIndex = 72
        Lb1.Text = "Nombre del rol:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(35, 103)
        Label2.Name = "Label2"
        Label2.Size = New Size(269, 30)
        Label2.TabIndex = 71
        Label2.Text = "Ingrese los datos para el rol"
        ' 
        ' RolesUpdate
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(670, 585)
        ControlBox = False
        Controls.Add(clb_permisos)
        Controls.Add(txt_nombre)
        Controls.Add(Label3)
        Controls.Add(Lb1)
        Controls.Add(Label2)
        Controls.Add(btn_cancelar)
        Controls.Add(btn_modificar)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "RolesUpdate"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btn_modificar As Button
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents clb_permisos As CheckedListBox
    Friend WithEvents txt_nombre As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Lb1 As Label
    Friend WithEvents Label2 As Label
End Class
