<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatosMedico
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
        txt_cedula = New TextBox()
        txt_especialidad = New TextBox()
        Label8 = New Label()
        Label1 = New Label()
        btn_aceptar = New Button()
        btn_cancelar = New Button()
        Label2 = New Label()
        SuspendLayout()
        ' 
        ' txt_cedula
        ' 
        txt_cedula.BackColor = Color.Silver
        txt_cedula.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_cedula.Location = New Point(167, 65)
        txt_cedula.MaxLength = 8
        txt_cedula.Name = "txt_cedula"
        txt_cedula.Size = New Size(284, 29)
        txt_cedula.TabIndex = 11
        ' 
        ' txt_especialidad
        ' 
        txt_especialidad.BackColor = Color.Silver
        txt_especialidad.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_especialidad.Location = New Point(167, 131)
        txt_especialidad.MaxLength = 50
        txt_especialidad.Name = "txt_especialidad"
        txt_especialidad.Size = New Size(284, 29)
        txt_especialidad.TabIndex = 12
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(12, 73)
        Label8.Name = "Label8"
        Label8.Size = New Size(143, 21)
        Label8.TabIndex = 13
        Label8.Text = "Cédula Profesional:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(57, 139)
        Label1.Name = "Label1"
        Label1.Size = New Size(98, 21)
        Label1.TabIndex = 14
        Label1.Text = "Especialidad:"
        ' 
        ' btn_aceptar
        ' 
        btn_aceptar.BackColor = SystemColors.HotTrack
        btn_aceptar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_aceptar.Location = New Point(213, 214)
        btn_aceptar.Name = "btn_aceptar"
        btn_aceptar.Size = New Size(116, 35)
        btn_aceptar.TabIndex = 19
        btn_aceptar.Text = "ACEPTAR"
        btn_aceptar.UseVisualStyleBackColor = False
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.Location = New Point(335, 214)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(116, 35)
        btn_cancelar.TabIndex = 20
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold)
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(126, 9)
        Label2.Name = "Label2"
        Label2.Size = New Size(272, 37)
        Label2.TabIndex = 28
        Label2.Text = "DATOS DEL MÉDICO"
        ' 
        ' DatosMedico
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(533, 275)
        ControlBox = False
        Controls.Add(Label2)
        Controls.Add(btn_cancelar)
        Controls.Add(btn_aceptar)
        Controls.Add(Label1)
        Controls.Add(Label8)
        Controls.Add(txt_especialidad)
        Controls.Add(txt_cedula)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "DatosMedico"
        StartPosition = FormStartPosition.CenterScreen
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txt_cedula As TextBox
    Friend WithEvents txt_especialidad As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btn_aceptar As Button
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents Label2 As Label
End Class
