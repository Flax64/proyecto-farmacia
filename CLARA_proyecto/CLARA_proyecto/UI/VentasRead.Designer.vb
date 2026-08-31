<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VentasRead
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        btn_create_venta = New Button()
        Label3 = New Label()
        txb_buscar_venta = New TextBox()
        dtpk_fecha_venta = New DateTimePicker()
        Label2 = New Label()
        dgv_ventas = New DataGridView()
        lblk_anterior = New LinkLabel()
        lblk_siguiente = New LinkLabel()
        lb_left = New Label()
        lb_middle = New Label()
        lb_right = New Label()
        CType(dgv_ventas, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold)
        Label1.ForeColor = Color.Black
        Label1.ImeMode = ImeMode.NoControl
        Label1.Location = New Point(316, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(118, 37)
        Label1.TabIndex = 2
        Label1.Text = "VENTAS"
        ' 
        ' btn_create_venta
        ' 
        btn_create_venta.BackColor = SystemColors.HotTrack
        btn_create_venta.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_create_venta.ForeColor = Color.Black
        btn_create_venta.ImeMode = ImeMode.NoControl
        btn_create_venta.Location = New Point(118, 80)
        btn_create_venta.Name = "btn_create_venta"
        btn_create_venta.Size = New Size(116, 35)
        btn_create_venta.TabIndex = 27
        btn_create_venta.Text = "NUEVA VENTA"
        btn_create_venta.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F)
        Label3.ForeColor = Color.Black
        Label3.ImeMode = ImeMode.NoControl
        Label3.Location = New Point(240, 86)
        Label3.Name = "Label3"
        Label3.Size = New Size(59, 21)
        Label3.TabIndex = 28
        Label3.Text = "Buscar:"
        ' 
        ' txb_buscar_venta
        ' 
        txb_buscar_venta.BackColor = Color.Silver
        txb_buscar_venta.Font = New Font("Segoe UI", 12F)
        txb_buscar_venta.Location = New Point(305, 86)
        txb_buscar_venta.MaxLength = 50
        txb_buscar_venta.Name = "txb_buscar_venta"
        txb_buscar_venta.Size = New Size(300, 29)
        txb_buscar_venta.TabIndex = 29
        ' 
        ' dtpk_fecha_venta
        ' 
        dtpk_fecha_venta.CalendarMonthBackground = Color.Tan
        dtpk_fecha_venta.Location = New Point(154, 147)
        dtpk_fecha_venta.Name = "dtpk_fecha_venta"
        dtpk_fecha_venta.ShowCheckBox = True
        dtpk_fecha_venta.Size = New Size(258, 23)
        dtpk_fecha_venta.TabIndex = 30
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F)
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(95, 147)
        Label2.Name = "Label2"
        Label2.Size = New Size(53, 21)
        Label2.TabIndex = 31
        Label2.Text = "Fecha:"
        ' 
        ' dgv_ventas
        ' 
        dgv_ventas.AllowUserToResizeColumns = False
        dgv_ventas.AllowUserToResizeRows = False
        dgv_ventas.BackgroundColor = Color.White
        dgv_ventas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_ventas.Location = New Point(69, 188)
        dgv_ventas.Name = "dgv_ventas"
        dgv_ventas.ReadOnly = True
        dgv_ventas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_ventas.Size = New Size(653, 185)
        dgv_ventas.TabIndex = 32
        ' 
        ' lblk_anterior
        ' 
        lblk_anterior.AutoSize = True
        lblk_anterior.ImeMode = ImeMode.NoControl
        lblk_anterior.LinkColor = Color.Black
        lblk_anterior.Location = New Point(279, 388)
        lblk_anterior.Name = "lblk_anterior"
        lblk_anterior.Size = New Size(61, 15)
        lblk_anterior.TabIndex = 33
        lblk_anterior.TabStop = True
        lblk_anterior.Text = "< Anterior"
        lblk_anterior.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lblk_siguiente
        ' 
        lblk_siguiente.AutoSize = True
        lblk_siguiente.ImeMode = ImeMode.NoControl
        lblk_siguiente.LinkColor = Color.Black
        lblk_siguiente.Location = New Point(430, 388)
        lblk_siguiente.Name = "lblk_siguiente"
        lblk_siguiente.Size = New Size(67, 15)
        lblk_siguiente.TabIndex = 34
        lblk_siguiente.TabStop = True
        lblk_siguiente.Text = "Siguiente >"
        lblk_siguiente.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lb_left
        ' 
        lb_left.AutoSize = True
        lb_left.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lb_left.ForeColor = Color.Black
        lb_left.ImeMode = ImeMode.NoControl
        lb_left.Location = New Point(361, 388)
        lb_left.Name = "lb_left"
        lb_left.Size = New Size(14, 15)
        lb_left.TabIndex = 35
        lb_left.Text = "1"
        ' 
        ' lb_middle
        ' 
        lb_middle.AutoSize = True
        lb_middle.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lb_middle.ForeColor = Color.Black
        lb_middle.ImeMode = ImeMode.NoControl
        lb_middle.Location = New Point(380, 388)
        lb_middle.Name = "lb_middle"
        lb_middle.Size = New Size(13, 15)
        lb_middle.TabIndex = 36
        lb_middle.Text = "2"
        ' 
        ' lb_right
        ' 
        lb_right.AutoSize = True
        lb_right.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lb_right.ForeColor = Color.Black
        lb_right.ImeMode = ImeMode.NoControl
        lb_right.Location = New Point(399, 388)
        lb_right.Name = "lb_right"
        lb_right.Size = New Size(13, 15)
        lb_right.TabIndex = 37
        lb_right.Text = "3"
        ' 
        ' VentasRead
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(lb_right)
        Controls.Add(lb_middle)
        Controls.Add(lb_left)
        Controls.Add(lblk_siguiente)
        Controls.Add(lblk_anterior)
        Controls.Add(dgv_ventas)
        Controls.Add(Label2)
        Controls.Add(dtpk_fecha_venta)
        Controls.Add(txb_buscar_venta)
        Controls.Add(Label3)
        Controls.Add(btn_create_venta)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "VentasRead"
        StartPosition = FormStartPosition.CenterParent
        CType(dgv_ventas, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btn_create_venta As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents txb_buscar_venta As TextBox
    Friend WithEvents dtpk_fecha_venta As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents dgv_ventas As DataGridView
    Friend WithEvents lblk_anterior As LinkLabel
    Friend WithEvents lblk_siguiente As LinkLabel
    Friend WithEvents lb_left As Label
    Friend WithEvents lb_middle As Label
    Friend WithEvents lb_right As Label
End Class
