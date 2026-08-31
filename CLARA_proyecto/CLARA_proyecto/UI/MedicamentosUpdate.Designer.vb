<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MedicamentosUpdate
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        btn_cancelar = New Button()
        cmb_unidad = New ComboBox()
        Label7 = New Label()
        txt_concentracion = New TextBox()
        Label6 = New Label()
        btn_guardar = New Button()
        txt_precio = New TextBox()
        txt_descripcion = New TextBox()
        txt_nombre = New TextBox()
        Label5 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(135, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(319, 37)
        Label1.TabIndex = 10
        Label1.Text = "EDITAR MEDICAMENTO"
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.Location = New Point(460, 346)
        btn_cancelar.Margin = New Padding(3, 2, 3, 2)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(116, 35)
        btn_cancelar.TabIndex = 6
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' cmb_unidad
        ' 
        cmb_unidad.FormattingEnabled = True
        cmb_unidad.Items.AddRange(New Object() {"mg", "g", "ml", "L"})
        cmb_unidad.Location = New Point(455, 131)
        cmb_unidad.Name = "cmb_unidad"
        cmb_unidad.Size = New Size(121, 23)
        cmb_unidad.TabIndex = 2
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(386, 131)
        Label7.Name = "Label7"
        Label7.Size = New Size(63, 21)
        Label7.TabIndex = 26
        Label7.Text = "Unidad:"
        ' 
        ' txt_concentracion
        ' 
        txt_concentracion.BackColor = Color.Silver
        txt_concentracion.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_concentracion.Location = New Point(143, 130)
        txt_concentracion.Margin = New Padding(3, 2, 3, 2)
        txt_concentracion.MaxLength = 6
        txt_concentracion.Name = "txt_concentracion"
        txt_concentracion.Size = New Size(106, 29)
        txt_concentracion.TabIndex = 1
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(24, 133)
        Label6.Name = "Label6"
        Label6.Size = New Size(113, 21)
        Label6.TabIndex = 24
        Label6.Text = "Concentración:"
        ' 
        ' btn_guardar
        ' 
        btn_guardar.BackColor = SystemColors.HotTrack
        btn_guardar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_guardar.Location = New Point(338, 346)
        btn_guardar.Margin = New Padding(3, 2, 3, 2)
        btn_guardar.Name = "btn_guardar"
        btn_guardar.Size = New Size(116, 35)
        btn_guardar.TabIndex = 5
        btn_guardar.Text = "GUARDAR"
        btn_guardar.UseVisualStyleBackColor = False
        ' 
        ' txt_precio
        ' 
        txt_precio.BackColor = Color.Silver
        txt_precio.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_precio.Location = New Point(24, 294)
        txt_precio.Margin = New Padding(3, 2, 3, 2)
        txt_precio.MaxLength = 15
        txt_precio.Name = "txt_precio"
        txt_precio.Size = New Size(132, 29)
        txt_precio.TabIndex = 4
        ' 
        ' txt_descripcion
        ' 
        txt_descripcion.BackColor = Color.Silver
        txt_descripcion.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_descripcion.Location = New Point(24, 196)
        txt_descripcion.Margin = New Padding(3, 2, 3, 2)
        txt_descripcion.MaxLength = 100
        txt_descripcion.Multiline = True
        txt_descripcion.Name = "txt_descripcion"
        txt_descripcion.Size = New Size(552, 62)
        txt_descripcion.TabIndex = 3
        ' 
        ' txt_nombre
        ' 
        txt_nombre.BackColor = Color.Silver
        txt_nombre.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_nombre.Location = New Point(24, 85)
        txt_nombre.Margin = New Padding(3, 2, 3, 2)
        txt_nombre.MaxLength = 50
        txt_nombre.Name = "txt_nombre"
        txt_nombre.Size = New Size(552, 29)
        txt_nombre.TabIndex = 0
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(26, 271)
        Label5.Name = "Label5"
        Label5.Size = New Size(53, 21)
        Label5.TabIndex = 18
        Label5.Text = "Precio"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(24, 173)
        Label3.Name = "Label3"
        Label3.Size = New Size(94, 21)
        Label3.TabIndex = 16
        Label3.Text = "Descripcion:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(24, 62)
        Label2.Name = "Label2"
        Label2.Size = New Size(195, 21)
        Label2.TabIndex = 15
        Label2.Text = "Nombre del medicamento:"
        ' 
        ' MedicamentosUpdate
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(588, 399)
        ControlBox = False
        Controls.Add(btn_cancelar)
        Controls.Add(cmb_unidad)
        Controls.Add(Label7)
        Controls.Add(txt_concentracion)
        Controls.Add(Label6)
        Controls.Add(btn_guardar)
        Controls.Add(txt_precio)
        Controls.Add(txt_descripcion)
        Controls.Add(txt_nombre)
        Controls.Add(Label5)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(3, 2, 3, 2)
        Name = "MedicamentosUpdate"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents cmb_unidad As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_concentracion As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btn_guardar As Button
    Friend WithEvents txt_precio As TextBox
    Friend WithEvents txt_descripcion As TextBox
    Friend WithEvents txt_nombre As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
End Class
