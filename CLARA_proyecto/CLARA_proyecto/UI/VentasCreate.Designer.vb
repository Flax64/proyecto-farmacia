<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VentasCreate
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
        lbl_fecha = New Label()
        lbl_total_valor = New Label()
        lbl_iva_valor = New Label()
        lbl_subtotal_valor = New Label()
        nud_cantidad = New NumericUpDown()
        btn_agregar = New Button()
        Label2 = New Label()
        btn_cancelar = New Button()
        btn_finalizar = New Button()
        Label1 = New Label()
        cmb_metodo_pago = New ComboBox()
        Label6 = New Label()
        Lb3 = New Label()
        Lb2 = New Label()
        Lb1 = New Label()
        dgv_carrito = New DataGridView()
        cmb_buscar_producto = New ComboBox()
        cmb_cliente = New ComboBox()
        CType(nud_cantidad, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgv_carrito, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lbl_fecha
        ' 
        lbl_fecha.AutoSize = True
        lbl_fecha.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_fecha.ForeColor = Color.Black
        lbl_fecha.ImeMode = ImeMode.NoControl
        lbl_fecha.Location = New Point(659, 9)
        lbl_fecha.Name = "lbl_fecha"
        lbl_fecha.Size = New Size(61, 25)
        lbl_fecha.TabIndex = 71
        lbl_fecha.Text = "Fecha"
        ' 
        ' lbl_total_valor
        ' 
        lbl_total_valor.AutoSize = True
        lbl_total_valor.Font = New Font("Segoe UI", 12F)
        lbl_total_valor.ForeColor = Color.Black
        lbl_total_valor.ImeMode = ImeMode.NoControl
        lbl_total_valor.Location = New Point(551, 407)
        lbl_total_valor.Name = "lbl_total_valor"
        lbl_total_valor.Size = New Size(49, 21)
        lbl_total_valor.TabIndex = 70
        lbl_total_valor.Text = "$0.00"
        ' 
        ' lbl_iva_valor
        ' 
        lbl_iva_valor.AutoSize = True
        lbl_iva_valor.Font = New Font("Segoe UI", 12F)
        lbl_iva_valor.ForeColor = Color.Black
        lbl_iva_valor.ImeMode = ImeMode.NoControl
        lbl_iva_valor.Location = New Point(551, 376)
        lbl_iva_valor.Name = "lbl_iva_valor"
        lbl_iva_valor.Size = New Size(49, 21)
        lbl_iva_valor.TabIndex = 69
        lbl_iva_valor.Text = "$0.00"
        ' 
        ' lbl_subtotal_valor
        ' 
        lbl_subtotal_valor.AutoSize = True
        lbl_subtotal_valor.Font = New Font("Segoe UI", 12F)
        lbl_subtotal_valor.ForeColor = Color.Black
        lbl_subtotal_valor.ImeMode = ImeMode.NoControl
        lbl_subtotal_valor.Location = New Point(551, 345)
        lbl_subtotal_valor.Name = "lbl_subtotal_valor"
        lbl_subtotal_valor.Size = New Size(49, 21)
        lbl_subtotal_valor.TabIndex = 68
        lbl_subtotal_valor.Text = "$0.00"
        ' 
        ' nud_cantidad
        ' 
        nud_cantidad.Location = New Point(520, 81)
        nud_cantidad.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nud_cantidad.Name = "nud_cantidad"
        nud_cantidad.Size = New Size(58, 23)
        nud_cantidad.TabIndex = 2
        nud_cantidad.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' btn_agregar
        ' 
        btn_agregar.BackColor = SystemColors.HotTrack
        btn_agregar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_agregar.ForeColor = Color.Black
        btn_agregar.ImeMode = ImeMode.NoControl
        btn_agregar.Location = New Point(604, 72)
        btn_agregar.Name = "btn_agregar"
        btn_agregar.Size = New Size(116, 35)
        btn_agregar.TabIndex = 3
        btn_agregar.Text = "AGREGAR"
        btn_agregar.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold)
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(306, 9)
        Label2.Name = "Label2"
        Label2.Size = New Size(201, 37)
        Label2.TabIndex = 64
        Label2.Text = "NUEVA VENTA"
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.ForeColor = Color.Black
        btn_cancelar.ImeMode = ImeMode.NoControl
        btn_cancelar.Location = New Point(592, 518)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(136, 37)
        btn_cancelar.TabIndex = 4
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' btn_finalizar
        ' 
        btn_finalizar.BackColor = SystemColors.HotTrack
        btn_finalizar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_finalizar.ForeColor = Color.Black
        btn_finalizar.ImeMode = ImeMode.NoControl
        btn_finalizar.Location = New Point(397, 518)
        btn_finalizar.Name = "btn_finalizar"
        btn_finalizar.Size = New Size(172, 37)
        btn_finalizar.TabIndex = 5
        btn_finalizar.Text = "FINALIZAR VENTA"
        btn_finalizar.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 12F)
        Label1.ForeColor = Color.Black
        Label1.ImeMode = ImeMode.NoControl
        Label1.Location = New Point(0, 41)
        Label1.Name = "Label1"
        Label1.Size = New Size(61, 21)
        Label1.TabIndex = 61
        Label1.Text = "Cliente:" & vbCrLf
        ' 
        ' cmb_metodo_pago
        ' 
        cmb_metodo_pago.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_metodo_pago.FormattingEnabled = True
        cmb_metodo_pago.Location = New Point(542, 454)
        cmb_metodo_pago.Name = "cmb_metodo_pago"
        cmb_metodo_pago.Size = New Size(186, 23)
        cmb_metodo_pago.TabIndex = 40
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 12F)
        Label6.ForeColor = Color.Black
        Label6.ImeMode = ImeMode.NoControl
        Label6.Location = New Point(414, 456)
        Label6.Name = "Label6"
        Label6.Size = New Size(127, 21)
        Label6.TabIndex = 58
        Label6.Text = "Método de pago:" & vbCrLf
        ' 
        ' Lb3
        ' 
        Lb3.AutoSize = True
        Lb3.Font = New Font("Segoe UI", 12F)
        Lb3.ForeColor = Color.Black
        Lb3.ImeMode = ImeMode.NoControl
        Lb3.Location = New Point(491, 407)
        Lb3.Name = "Lb3"
        Lb3.Size = New Size(45, 21)
        Lb3.TabIndex = 57
        Lb3.Text = "Total:"
        ' 
        ' Lb2
        ' 
        Lb2.AutoSize = True
        Lb2.Font = New Font("Segoe UI", 12F)
        Lb2.ForeColor = Color.Black
        Lb2.ImeMode = ImeMode.NoControl
        Lb2.Location = New Point(455, 376)
        Lb2.Name = "Lb2"
        Lb2.Size = New Size(81, 21)
        Lb2.TabIndex = 56
        Lb2.Text = "IVA (16%):" & vbCrLf
        ' 
        ' Lb1
        ' 
        Lb1.AutoSize = True
        Lb1.Font = New Font("Segoe UI", 12F)
        Lb1.ForeColor = Color.Black
        Lb1.ImeMode = ImeMode.NoControl
        Lb1.Location = New Point(465, 345)
        Lb1.Name = "Lb1"
        Lb1.Size = New Size(71, 21)
        Lb1.TabIndex = 55
        Lb1.Text = "Subtotal:"
        ' 
        ' dgv_carrito
        ' 
        dgv_carrito.AllowUserToResizeColumns = False
        dgv_carrito.AllowUserToResizeRows = False
        dgv_carrito.BackgroundColor = Color.White
        dgv_carrito.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_carrito.Location = New Point(67, 132)
        dgv_carrito.Name = "dgv_carrito"
        dgv_carrito.ReadOnly = True
        dgv_carrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_carrito.Size = New Size(653, 185)
        dgv_carrito.TabIndex = 54
        ' 
        ' cmb_buscar_producto
        ' 
        cmb_buscar_producto.FormattingEnabled = True
        cmb_buscar_producto.Location = New Point(67, 80)
        cmb_buscar_producto.Name = "cmb_buscar_producto"
        cmb_buscar_producto.Size = New Size(415, 23)
        cmb_buscar_producto.TabIndex = 1
        ' 
        ' cmb_cliente
        ' 
        cmb_cliente.FormattingEnabled = True
        cmb_cliente.Location = New Point(67, 43)
        cmb_cliente.Name = "cmb_cliente"
        cmb_cliente.Size = New Size(233, 23)
        cmb_cliente.TabIndex = 0
        ' 
        ' VentasCreate
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 572)
        ControlBox = False
        Controls.Add(cmb_cliente)
        Controls.Add(cmb_buscar_producto)
        Controls.Add(lbl_fecha)
        Controls.Add(lbl_total_valor)
        Controls.Add(lbl_iva_valor)
        Controls.Add(lbl_subtotal_valor)
        Controls.Add(nud_cantidad)
        Controls.Add(btn_agregar)
        Controls.Add(Label2)
        Controls.Add(btn_cancelar)
        Controls.Add(btn_finalizar)
        Controls.Add(Label1)
        Controls.Add(cmb_metodo_pago)
        Controls.Add(Label6)
        Controls.Add(Lb3)
        Controls.Add(Lb2)
        Controls.Add(Lb1)
        Controls.Add(dgv_carrito)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "VentasCreate"
        StartPosition = FormStartPosition.CenterParent
        CType(nud_cantidad, ComponentModel.ISupportInitialize).EndInit()
        CType(dgv_carrito, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lbl_fecha As Label
    Friend WithEvents lbl_total_valor As Label
    Friend WithEvents lbl_iva_valor As Label
    Friend WithEvents lbl_subtotal_valor As Label
    Friend WithEvents nud_cantidad As NumericUpDown
    Friend WithEvents btn_agregar As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents btn_finalizar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cmb_metodo_pago As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Lb3 As Label
    Friend WithEvents Lb2 As Label
    Friend WithEvents Lb1 As Label
    Friend WithEvents dgv_carrito As DataGridView
    Friend WithEvents cmb_buscar_producto As ComboBox
    Friend WithEvents cmb_cliente As ComboBox
End Class
