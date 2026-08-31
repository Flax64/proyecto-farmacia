<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RolesCreate
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
        Label2 = New Label()
        Label1 = New Label()
        Lb1 = New Label()
        Label3 = New Label()
        txt_nombre = New TextBox()
        clb_permisos = New CheckedListBox()
        btn_crear = New Button()
        btn_cancelar = New Button()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold)
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(186, 9)
        Label2.Name = "Label2"
        Label2.Size = New Size(281, 37)
        Label2.TabIndex = 65
        Label2.Text = "CREACIÓN DE ROLES"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.ImeMode = ImeMode.NoControl
        Label1.Location = New Point(17, 89)
        Label1.Name = "Label1"
        Label1.Size = New Size(269, 30)
        Label1.TabIndex = 66
        Label1.Text = "Ingrese los datos para el rol"
        ' 
        ' Lb1
        ' 
        Lb1.AutoSize = True
        Lb1.Font = New Font("Segoe UI", 12F)
        Lb1.ForeColor = Color.Black
        Lb1.ImeMode = ImeMode.NoControl
        Lb1.Location = New Point(205, 174)
        Lb1.Name = "Lb1"
        Lb1.Size = New Size(119, 21)
        Lb1.TabIndex = 67
        Lb1.Text = "Nombre del rol:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F)
        Label3.ForeColor = Color.Black
        Label3.ImeMode = ImeMode.NoControl
        Label3.Location = New Point(248, 248)
        Label3.Name = "Label3"
        Label3.Size = New Size(76, 21)
        Label3.TabIndex = 68
        Label3.Text = "Permisos:"
        ' 
        ' txt_nombre
        ' 
        txt_nombre.BackColor = Color.Silver
        txt_nombre.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_nombre.Location = New Point(330, 171)
        txt_nombre.MaxLength = 50
        txt_nombre.Name = "txt_nombre"
        txt_nombre.Size = New Size(300, 29)
        txt_nombre.TabIndex = 69
        ' 
        ' clb_permisos
        ' 
        clb_permisos.FormattingEnabled = True
        clb_permisos.Location = New Point(330, 248)
        clb_permisos.Name = "clb_permisos"
        clb_permisos.Size = New Size(264, 220)
        clb_permisos.TabIndex = 70
        ' 
        ' btn_crear
        ' 
        btn_crear.BackColor = SystemColors.HotTrack
        btn_crear.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_crear.ForeColor = Color.Black
        btn_crear.Location = New Point(378, 515)
        btn_crear.Name = "btn_crear"
        btn_crear.Size = New Size(123, 37)
        btn_crear.TabIndex = 71
        btn_crear.Text = "CREAR ROL"
        btn_crear.UseVisualStyleBackColor = False
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.ForeColor = Color.Black
        btn_cancelar.Location = New Point(507, 515)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(123, 37)
        btn_cancelar.TabIndex = 72
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' RolesCreate
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(670, 585)
        ControlBox = False
        Controls.Add(btn_cancelar)
        Controls.Add(btn_crear)
        Controls.Add(clb_permisos)
        Controls.Add(txt_nombre)
        Controls.Add(Label3)
        Controls.Add(Lb1)
        Controls.Add(Label1)
        Controls.Add(Label2)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "RolesCreate"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Lb1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_nombre As TextBox
    Friend WithEvents clb_permisos As CheckedListBox
    Friend WithEvents btn_crear As Button
    Friend WithEvents btn_cancelar As Button
End Class
