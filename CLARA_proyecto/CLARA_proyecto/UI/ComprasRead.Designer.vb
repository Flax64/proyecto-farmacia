<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ComprasRead
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
        lb_right = New Label()
        lb_middle = New Label()
        lb_left = New Label()
        lblk_siguiente = New LinkLabel()
        lblk_anterior = New LinkLabel()
        dgv_Compras = New DataGridView()
        Label2 = New Label()
        dtpk_fecha = New DateTimePicker()
        Label3 = New Label()
        btn_create_compra = New Button()
        txb_buscar_compra = New TextBox()
        CType(dgv_Compras, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(332, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(149, 37)
        Label1.TabIndex = 0
        Label1.Text = "COMPRAS"
        ' 
        ' lb_right
        ' 
        lb_right.AutoSize = True
        lb_right.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lb_right.ForeColor = Color.Black
        lb_right.ImeMode = ImeMode.NoControl
        lb_right.Location = New Point(404, 372)
        lb_right.Name = "lb_right"
        lb_right.Size = New Size(13, 15)
        lb_right.TabIndex = 48
        lb_right.Text = "3"
        ' 
        ' lb_middle
        ' 
        lb_middle.AutoSize = True
        lb_middle.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lb_middle.ForeColor = Color.Black
        lb_middle.ImeMode = ImeMode.NoControl
        lb_middle.Location = New Point(385, 372)
        lb_middle.Name = "lb_middle"
        lb_middle.Size = New Size(13, 15)
        lb_middle.TabIndex = 47
        lb_middle.Text = "2"
        ' 
        ' lb_left
        ' 
        lb_left.AutoSize = True
        lb_left.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lb_left.ForeColor = Color.Black
        lb_left.ImeMode = ImeMode.NoControl
        lb_left.Location = New Point(366, 372)
        lb_left.Name = "lb_left"
        lb_left.Size = New Size(14, 15)
        lb_left.TabIndex = 46
        lb_left.Text = "1"
        ' 
        ' lblk_siguiente
        ' 
        lblk_siguiente.AutoSize = True
        lblk_siguiente.ImeMode = ImeMode.NoControl
        lblk_siguiente.LinkColor = Color.Black
        lblk_siguiente.Location = New Point(435, 372)
        lblk_siguiente.Name = "lblk_siguiente"
        lblk_siguiente.Size = New Size(67, 15)
        lblk_siguiente.TabIndex = 45
        lblk_siguiente.TabStop = True
        lblk_siguiente.Text = "Siguiente >"
        lblk_siguiente.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lblk_anterior
        ' 
        lblk_anterior.AutoSize = True
        lblk_anterior.ImeMode = ImeMode.NoControl
        lblk_anterior.LinkColor = Color.Black
        lblk_anterior.Location = New Point(284, 372)
        lblk_anterior.Name = "lblk_anterior"
        lblk_anterior.Size = New Size(61, 15)
        lblk_anterior.TabIndex = 44
        lblk_anterior.TabStop = True
        lblk_anterior.Text = "< Anterior"
        lblk_anterior.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dgv_Compras
        ' 
        dgv_Compras.AllowUserToResizeColumns = False
        dgv_Compras.AllowUserToResizeRows = False
        dgv_Compras.BackgroundColor = Color.White
        dgv_Compras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_Compras.Location = New Point(74, 172)
        dgv_Compras.Name = "dgv_Compras"
        dgv_Compras.ReadOnly = True
        dgv_Compras.Size = New Size(653, 185)
        dgv_Compras.TabIndex = 43
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F)
        Label2.ForeColor = Color.Black
        Label2.ImeMode = ImeMode.NoControl
        Label2.Location = New Point(100, 131)
        Label2.Name = "Label2"
        Label2.Size = New Size(53, 21)
        Label2.TabIndex = 42
        Label2.Text = "Fecha:"
        ' 
        ' dtpk_fecha
        ' 
        dtpk_fecha.CalendarMonthBackground = Color.Tan
        dtpk_fecha.Location = New Point(159, 131)
        dtpk_fecha.Name = "dtpk_fecha"
        dtpk_fecha.ShowCheckBox = True
        dtpk_fecha.Size = New Size(258, 23)
        dtpk_fecha.TabIndex = 41
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F)
        Label3.ForeColor = Color.Black
        Label3.ImeMode = ImeMode.NoControl
        Label3.Location = New Point(245, 70)
        Label3.Name = "Label3"
        Label3.Size = New Size(59, 21)
        Label3.TabIndex = 39
        Label3.Text = "Buscar:"
        ' 
        ' btn_create_compra
        ' 
        btn_create_compra.BackColor = SystemColors.HotTrack
        btn_create_compra.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_create_compra.ForeColor = Color.Black
        btn_create_compra.ImeMode = ImeMode.NoControl
        btn_create_compra.Location = New Point(100, 64)
        btn_create_compra.Name = "btn_create_compra"
        btn_create_compra.Size = New Size(139, 35)
        btn_create_compra.TabIndex = 38
        btn_create_compra.Text = "NUEVA COMPRA"
        btn_create_compra.UseVisualStyleBackColor = False
        ' 
        ' txb_buscar_compra
        ' 
        txb_buscar_compra.BackColor = Color.Silver
        txb_buscar_compra.Font = New Font("Segoe UI", 12F)
        txb_buscar_compra.Location = New Point(310, 70)
        txb_buscar_compra.MaxLength = 50
        txb_buscar_compra.Name = "txb_buscar_compra"
        txb_buscar_compra.Size = New Size(300, 29)
        txb_buscar_compra.TabIndex = 49
        ' 
        ' ComprasRead
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(txb_buscar_compra)
        Controls.Add(lb_right)
        Controls.Add(lb_middle)
        Controls.Add(lb_left)
        Controls.Add(lblk_siguiente)
        Controls.Add(lblk_anterior)
        Controls.Add(dgv_Compras)
        Controls.Add(Label2)
        Controls.Add(dtpk_fecha)
        Controls.Add(Label3)
        Controls.Add(btn_create_compra)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "ComprasRead"
        StartPosition = FormStartPosition.CenterParent
        CType(dgv_Compras, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents lb_right As Label
    Friend WithEvents lb_middle As Label
    Friend WithEvents lb_left As Label
    Friend WithEvents lblk_siguiente As LinkLabel
    Friend WithEvents lblk_anterior As LinkLabel
    Friend WithEvents dgv_Compras As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents dtpk_fecha As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents btn_create_compra As Button
    Friend WithEvents txb_buscar_compra As TextBox
End Class
