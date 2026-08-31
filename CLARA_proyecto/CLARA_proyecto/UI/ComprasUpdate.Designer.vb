<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ComprasUpdate
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
        lbl_proveedores = New Label()
        Label1 = New Label()
        cmb_Proveedores = New ComboBox()
        cmb_Medicamentos = New ComboBox()
        lbl_medicamentos = New Label()
        NumericUpDown1 = New NumericUpDown()
        dgv_Compra = New DataGridView()
        btnGuardar = New Button()
        lbl_subtotal = New Label()
        lbl_iva = New Label()
        lbl_total = New Label()
        lbl_TotalValue = New Label()
        lbl_IVAValue = New Label()
        lbl_SubtotalValue = New Label()
        Lb3 = New Label()
        Lb2 = New Label()
        Lb1 = New Label()
        btn_cancelar = New Button()
        btn_Añadir = New Button()
        lb_fecha = New Label()
        txt_Precio = New TextBox()
        Label4 = New Label()
        Label3 = New Label()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgv_Compra, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lbl_proveedores
        ' 
        lbl_proveedores.AutoSize = True
        lbl_proveedores.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_proveedores.Location = New Point(12, 9)
        lbl_proveedores.Name = "lbl_proveedores"
        lbl_proveedores.Size = New Size(74, 21)
        lbl_proveedores.TabIndex = 0
        lbl_proveedores.Text = "Provedor"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(283, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(235, 37)
        Label1.TabIndex = 1
        Label1.Text = "EDITAR COMPRA"
        ' 
        ' cmb_Proveedores
        ' 
        cmb_Proveedores.FormattingEnabled = True
        cmb_Proveedores.Location = New Point(12, 33)
        cmb_Proveedores.Name = "cmb_Proveedores"
        cmb_Proveedores.Size = New Size(184, 23)
        cmb_Proveedores.TabIndex = 0
        ' 
        ' cmb_Medicamentos
        ' 
        cmb_Medicamentos.FormattingEnabled = True
        cmb_Medicamentos.Location = New Point(74, 112)
        cmb_Medicamentos.Name = "cmb_Medicamentos"
        cmb_Medicamentos.Size = New Size(363, 23)
        cmb_Medicamentos.TabIndex = 1
        ' 
        ' lbl_medicamentos
        ' 
        lbl_medicamentos.AutoSize = True
        lbl_medicamentos.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_medicamentos.Location = New Point(74, 88)
        lbl_medicamentos.Name = "lbl_medicamentos"
        lbl_medicamentos.Size = New Size(112, 21)
        lbl_medicamentos.TabIndex = 3
        lbl_medicamentos.Text = "Medicamentos"
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Location = New Point(568, 113)
        NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(70, 23)
        NumericUpDown1.TabIndex = 3
        NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' dgv_Compra
        ' 
        dgv_Compra.AllowUserToResizeColumns = False
        dgv_Compra.AllowUserToResizeRows = False
        dgv_Compra.BackgroundColor = Color.White
        dgv_Compra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_Compra.Location = New Point(74, 169)
        dgv_Compra.Name = "dgv_Compra"
        dgv_Compra.ReadOnly = True
        dgv_Compra.Size = New Size(633, 150)
        dgv_Compra.TabIndex = 6
        ' 
        ' btnGuardar
        ' 
        btnGuardar.BackColor = SystemColors.HotTrack
        btnGuardar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnGuardar.ForeColor = Color.Black
        btnGuardar.Location = New Point(539, 395)
        btnGuardar.Margin = New Padding(3, 2, 3, 2)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(116, 35)
        btnGuardar.TabIndex = 6
        btnGuardar.Text = "GUARDAR"
        btnGuardar.UseVisualStyleBackColor = False
        ' 
        ' lbl_subtotal
        ' 
        lbl_subtotal.AutoSize = True
        lbl_subtotal.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_subtotal.Location = New Point(74, 356)
        lbl_subtotal.Name = "lbl_subtotal"
        lbl_subtotal.Size = New Size(0, 21)
        lbl_subtotal.TabIndex = 21
        ' 
        ' lbl_iva
        ' 
        lbl_iva.AutoSize = True
        lbl_iva.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_iva.Location = New Point(215, 356)
        lbl_iva.Name = "lbl_iva"
        lbl_iva.Size = New Size(0, 21)
        lbl_iva.TabIndex = 22
        ' 
        ' lbl_total
        ' 
        lbl_total.AutoSize = True
        lbl_total.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_total.Location = New Point(421, 356)
        lbl_total.Name = "lbl_total"
        lbl_total.Size = New Size(0, 21)
        lbl_total.TabIndex = 23
        ' 
        ' lbl_TotalValue
        ' 
        lbl_TotalValue.AutoSize = True
        lbl_TotalValue.Font = New Font("Segoe UI", 12F)
        lbl_TotalValue.ForeColor = Color.Black
        lbl_TotalValue.ImeMode = ImeMode.NoControl
        lbl_TotalValue.Location = New Point(167, 387)
        lbl_TotalValue.Name = "lbl_TotalValue"
        lbl_TotalValue.Size = New Size(49, 21)
        lbl_TotalValue.TabIndex = 76
        lbl_TotalValue.Text = "$0.00"
        ' 
        ' lbl_IVAValue
        ' 
        lbl_IVAValue.AutoSize = True
        lbl_IVAValue.Font = New Font("Segoe UI", 12F)
        lbl_IVAValue.ForeColor = Color.Black
        lbl_IVAValue.ImeMode = ImeMode.NoControl
        lbl_IVAValue.Location = New Point(167, 356)
        lbl_IVAValue.Name = "lbl_IVAValue"
        lbl_IVAValue.Size = New Size(49, 21)
        lbl_IVAValue.TabIndex = 75
        lbl_IVAValue.Text = "$0.00"
        ' 
        ' lbl_SubtotalValue
        ' 
        lbl_SubtotalValue.AutoSize = True
        lbl_SubtotalValue.Font = New Font("Segoe UI", 12F)
        lbl_SubtotalValue.ForeColor = Color.Black
        lbl_SubtotalValue.ImeMode = ImeMode.NoControl
        lbl_SubtotalValue.Location = New Point(167, 325)
        lbl_SubtotalValue.Name = "lbl_SubtotalValue"
        lbl_SubtotalValue.Size = New Size(49, 21)
        lbl_SubtotalValue.TabIndex = 74
        lbl_SubtotalValue.Text = "$0.00"
        ' 
        ' Lb3
        ' 
        Lb3.AutoSize = True
        Lb3.Font = New Font("Segoe UI", 12F)
        Lb3.ForeColor = Color.Black
        Lb3.ImeMode = ImeMode.NoControl
        Lb3.Location = New Point(107, 387)
        Lb3.Name = "Lb3"
        Lb3.Size = New Size(45, 21)
        Lb3.TabIndex = 73
        Lb3.Text = "Total:"
        ' 
        ' Lb2
        ' 
        Lb2.AutoSize = True
        Lb2.Font = New Font("Segoe UI", 12F)
        Lb2.ForeColor = Color.Black
        Lb2.ImeMode = ImeMode.NoControl
        Lb2.Location = New Point(71, 356)
        Lb2.Name = "Lb2"
        Lb2.Size = New Size(81, 21)
        Lb2.TabIndex = 72
        Lb2.Text = "IVA (16%):" & vbCrLf
        ' 
        ' Lb1
        ' 
        Lb1.AutoSize = True
        Lb1.Font = New Font("Segoe UI", 12F)
        Lb1.ForeColor = Color.Black
        Lb1.ImeMode = ImeMode.NoControl
        Lb1.Location = New Point(81, 325)
        Lb1.Name = "Lb1"
        Lb1.Size = New Size(71, 21)
        Lb1.TabIndex = 71
        Lb1.Text = "Subtotal:"
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.ForeColor = Color.Black
        btn_cancelar.Location = New Point(661, 395)
        btn_cancelar.Margin = New Padding(3, 2, 3, 2)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(116, 35)
        btn_cancelar.TabIndex = 5
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' btn_Añadir
        ' 
        btn_Añadir.BackColor = SystemColors.HotTrack
        btn_Añadir.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_Añadir.ForeColor = Color.Black
        btn_Añadir.Location = New Point(644, 108)
        btn_Añadir.Margin = New Padding(3, 2, 3, 2)
        btn_Añadir.Name = "btn_Añadir"
        btn_Añadir.Size = New Size(95, 35)
        btn_Añadir.TabIndex = 4
        btn_Añadir.Text = "AGREGAR"
        btn_Añadir.UseVisualStyleBackColor = False
        ' 
        ' lb_fecha
        ' 
        lb_fecha.AutoSize = True
        lb_fecha.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lb_fecha.Location = New Point(661, 9)
        lb_fecha.Name = "lb_fecha"
        lb_fecha.Size = New Size(50, 21)
        lb_fecha.TabIndex = 84
        lb_fecha.Text = "Fecha"
        ' 
        ' txt_Precio
        ' 
        txt_Precio.BackColor = Color.White
        txt_Precio.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Precio.Location = New Point(451, 112)
        txt_Precio.MaxLength = 10
        txt_Precio.Name = "txt_Precio"
        txt_Precio.Size = New Size(105, 25)
        txt_Precio.TabIndex = 2
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(568, 88)
        Label4.Name = "Label4"
        Label4.Size = New Size(72, 21)
        Label4.TabIndex = 88
        Label4.Text = "Cantidad"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(451, 88)
        Label3.Name = "Label3"
        Label3.Size = New Size(53, 21)
        Label3.TabIndex = 87
        Label3.Text = "Precio"
        ' 
        ' ComprasUpdate
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(789, 441)
        ControlBox = False
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(txt_Precio)
        Controls.Add(lb_fecha)
        Controls.Add(btn_Añadir)
        Controls.Add(btn_cancelar)
        Controls.Add(lbl_TotalValue)
        Controls.Add(lbl_IVAValue)
        Controls.Add(lbl_SubtotalValue)
        Controls.Add(Lb3)
        Controls.Add(Lb2)
        Controls.Add(Lb1)
        Controls.Add(lbl_total)
        Controls.Add(lbl_iva)
        Controls.Add(lbl_subtotal)
        Controls.Add(btnGuardar)
        Controls.Add(dgv_Compra)
        Controls.Add(NumericUpDown1)
        Controls.Add(cmb_Medicamentos)
        Controls.Add(lbl_medicamentos)
        Controls.Add(cmb_Proveedores)
        Controls.Add(Label1)
        Controls.Add(lbl_proveedores)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "ComprasUpdate"
        StartPosition = FormStartPosition.CenterParent
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        CType(dgv_Compra, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lbl_proveedores As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmb_Proveedores As ComboBox
    Friend WithEvents cmb_Medicamentos As ComboBox
    Friend WithEvents lbl_medicamentos As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents dgv_Compra As DataGridView
    Friend WithEvents btnGuardar As Button
    Friend WithEvents lbl_subtotal As Label
    Friend WithEvents lbl_iva As Label
    Friend WithEvents lbl_total As Label
    Friend WithEvents lbl_TotalValue As Label
    Friend WithEvents lbl_IVAValue As Label
    Friend WithEvents lbl_SubtotalValue As Label
    Friend WithEvents Lb3 As Label
    Friend WithEvents Lb2 As Label
    Friend WithEvents Lb1 As Label
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents btn_Añadir As Button
    Friend WithEvents lb_fecha As Label
    Friend WithEvents txt_Precio As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
End Class
