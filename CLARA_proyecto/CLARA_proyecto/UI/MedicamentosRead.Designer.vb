<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MedicamentosRead
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
        txt_buscar = New TextBox()
        btn_nuevo_medicamento = New Button()
        dgv_medicamentos = New DataGridView()
        Label3 = New Label()
        lb_right = New Label()
        lb_middle = New Label()
        lb_left = New Label()
        btn_siguiente = New LinkLabel()
        btn_anterior = New LinkLabel()
        CType(dgv_medicamentos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(178, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(444, 37)
        Label1.TabIndex = 0
        Label1.Text = "INVENTARIO DE MEDICAMENTOS"
        ' 
        ' txt_buscar
        ' 
        txt_buscar.BackColor = Color.Silver
        txt_buscar.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_buscar.Location = New Point(305, 86)
        txt_buscar.Margin = New Padding(3, 2, 3, 2)
        txt_buscar.Name = "txt_buscar"
        txt_buscar.Size = New Size(300, 29)
        txt_buscar.TabIndex = 1
        ' 
        ' btn_nuevo_medicamento
        ' 
        btn_nuevo_medicamento.BackColor = SystemColors.HotTrack
        btn_nuevo_medicamento.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_nuevo_medicamento.Location = New Point(60, 80)
        btn_nuevo_medicamento.Margin = New Padding(3, 2, 3, 2)
        btn_nuevo_medicamento.Name = "btn_nuevo_medicamento"
        btn_nuevo_medicamento.Size = New Size(174, 35)
        btn_nuevo_medicamento.TabIndex = 2
        btn_nuevo_medicamento.Text = "NUEVO MEDICAMENTO"
        btn_nuevo_medicamento.UseVisualStyleBackColor = False
        ' 
        ' dgv_medicamentos
        ' 
        dgv_medicamentos.AllowUserToAddRows = False
        dgv_medicamentos.AllowUserToDeleteRows = False
        dgv_medicamentos.AllowUserToResizeColumns = False
        dgv_medicamentos.AllowUserToResizeRows = False
        dgv_medicamentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_medicamentos.BackgroundColor = Color.White
        dgv_medicamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_medicamentos.Location = New Point(69, 148)
        dgv_medicamentos.Margin = New Padding(3, 2, 3, 2)
        dgv_medicamentos.Name = "dgv_medicamentos"
        dgv_medicamentos.ReadOnly = True
        dgv_medicamentos.RowHeadersVisible = False
        dgv_medicamentos.RowHeadersWidth = 51
        dgv_medicamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_medicamentos.Size = New Size(679, 192)
        dgv_medicamentos.TabIndex = 3
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
        Label3.TabIndex = 29
        Label3.Text = "Buscar:"
        ' 
        ' lb_right
        ' 
        lb_right.AutoSize = True
        lb_right.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lb_right.ForeColor = Color.Black
        lb_right.ImeMode = ImeMode.NoControl
        lb_right.Location = New Point(415, 361)
        lb_right.Name = "lb_right"
        lb_right.Size = New Size(13, 15)
        lb_right.TabIndex = 42
        lb_right.Text = "3"
        ' 
        ' lb_middle
        ' 
        lb_middle.AutoSize = True
        lb_middle.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lb_middle.ForeColor = Color.Black
        lb_middle.ImeMode = ImeMode.NoControl
        lb_middle.Location = New Point(396, 361)
        lb_middle.Name = "lb_middle"
        lb_middle.Size = New Size(13, 15)
        lb_middle.TabIndex = 41
        lb_middle.Text = "2"
        ' 
        ' lb_left
        ' 
        lb_left.AutoSize = True
        lb_left.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lb_left.ForeColor = Color.Black
        lb_left.ImeMode = ImeMode.NoControl
        lb_left.Location = New Point(377, 361)
        lb_left.Name = "lb_left"
        lb_left.Size = New Size(14, 15)
        lb_left.TabIndex = 40
        lb_left.Text = "1"
        ' 
        ' btn_siguiente
        ' 
        btn_siguiente.AutoSize = True
        btn_siguiente.ImeMode = ImeMode.NoControl
        btn_siguiente.LinkColor = Color.Black
        btn_siguiente.Location = New Point(446, 361)
        btn_siguiente.Name = "btn_siguiente"
        btn_siguiente.Size = New Size(67, 15)
        btn_siguiente.TabIndex = 39
        btn_siguiente.TabStop = True
        btn_siguiente.Text = "Siguiente >"
        btn_siguiente.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_anterior
        ' 
        btn_anterior.AutoSize = True
        btn_anterior.ImeMode = ImeMode.NoControl
        btn_anterior.LinkColor = Color.Black
        btn_anterior.Location = New Point(295, 361)
        btn_anterior.Name = "btn_anterior"
        btn_anterior.Size = New Size(61, 15)
        btn_anterior.TabIndex = 38
        btn_anterior.TabStop = True
        btn_anterior.Text = "< Anterior"
        btn_anterior.TextAlign = ContentAlignment.TopCenter
        ' 
        ' MedicamentosRead
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(lb_right)
        Controls.Add(lb_middle)
        Controls.Add(lb_left)
        Controls.Add(btn_siguiente)
        Controls.Add(btn_anterior)
        Controls.Add(Label3)
        Controls.Add(dgv_medicamentos)
        Controls.Add(btn_nuevo_medicamento)
        Controls.Add(txt_buscar)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(3, 2, 3, 2)
        MaximizeBox = False
        Name = "MedicamentosRead"
        StartPosition = FormStartPosition.CenterParent
        CType(dgv_medicamentos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txt_buscar As TextBox
    Friend WithEvents btn_nuevo_medicamento As Button
    Friend WithEvents dgv_medicamentos As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents lb_right As Label
    Friend WithEvents lb_middle As Label
    Friend WithEvents lb_left As Label
    Friend WithEvents btn_siguiente As LinkLabel
    Friend WithEvents btn_anterior As LinkLabel
End Class
