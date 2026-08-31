<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HorariosRead
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
        txt_buscar = New TextBox()
        lb_right = New Label()
        lb_middle = New Label()
        lb_left = New Label()
        btn_siguiente = New LinkLabel()
        btn_anterior = New LinkLabel()
        dgv_horarios = New DataGridView()
        Label3 = New Label()
        btn_nuevo_horario = New Button()
        Label1 = New Label()
        CType(dgv_horarios, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' txt_buscar
        ' 
        txt_buscar.BackColor = Color.Silver
        txt_buscar.Font = New Font("Segoe UI", 12F)
        txt_buscar.Location = New Point(301, 100)
        txt_buscar.MaxLength = 50
        txt_buscar.Name = "txt_buscar"
        txt_buscar.Size = New Size(300, 29)
        txt_buscar.TabIndex = 72
        ' 
        ' lb_right
        ' 
        lb_right.AutoSize = True
        lb_right.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lb_right.ForeColor = Color.Black
        lb_right.ImeMode = ImeMode.NoControl
        lb_right.Location = New Point(395, 372)
        lb_right.Name = "lb_right"
        lb_right.Size = New Size(13, 15)
        lb_right.TabIndex = 71
        lb_right.Text = "3"
        ' 
        ' lb_middle
        ' 
        lb_middle.AutoSize = True
        lb_middle.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lb_middle.ForeColor = Color.Black
        lb_middle.ImeMode = ImeMode.NoControl
        lb_middle.Location = New Point(376, 372)
        lb_middle.Name = "lb_middle"
        lb_middle.Size = New Size(13, 15)
        lb_middle.TabIndex = 70
        lb_middle.Text = "2"
        ' 
        ' lb_left
        ' 
        lb_left.AutoSize = True
        lb_left.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lb_left.ForeColor = Color.Black
        lb_left.ImeMode = ImeMode.NoControl
        lb_left.Location = New Point(357, 372)
        lb_left.Name = "lb_left"
        lb_left.Size = New Size(14, 15)
        lb_left.TabIndex = 69
        lb_left.Text = "1"
        ' 
        ' btn_siguiente
        ' 
        btn_siguiente.AutoSize = True
        btn_siguiente.ImeMode = ImeMode.NoControl
        btn_siguiente.LinkColor = Color.Black
        btn_siguiente.Location = New Point(426, 372)
        btn_siguiente.Name = "btn_siguiente"
        btn_siguiente.Size = New Size(67, 15)
        btn_siguiente.TabIndex = 68
        btn_siguiente.TabStop = True
        btn_siguiente.Text = "Siguiente >"
        btn_siguiente.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_anterior
        ' 
        btn_anterior.AutoSize = True
        btn_anterior.ImeMode = ImeMode.NoControl
        btn_anterior.LinkColor = Color.Black
        btn_anterior.Location = New Point(275, 372)
        btn_anterior.Name = "btn_anterior"
        btn_anterior.Size = New Size(61, 15)
        btn_anterior.TabIndex = 67
        btn_anterior.TabStop = True
        btn_anterior.Text = "< Anterior"
        btn_anterior.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dgv_horarios
        ' 
        dgv_horarios.AllowUserToResizeColumns = False
        dgv_horarios.AllowUserToResizeRows = False
        dgv_horarios.BackgroundColor = Color.White
        dgv_horarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_horarios.Location = New Point(65, 172)
        dgv_horarios.Name = "dgv_horarios"
        dgv_horarios.ReadOnly = True
        dgv_horarios.Size = New Size(653, 185)
        dgv_horarios.TabIndex = 66
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F)
        Label3.ForeColor = Color.Black
        Label3.ImeMode = ImeMode.NoControl
        Label3.Location = New Point(236, 100)
        Label3.Name = "Label3"
        Label3.Size = New Size(59, 21)
        Label3.TabIndex = 63
        Label3.Text = "Buscar:"
        ' 
        ' btn_nuevo_horario
        ' 
        btn_nuevo_horario.BackColor = SystemColors.HotTrack
        btn_nuevo_horario.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_nuevo_horario.ForeColor = Color.Black
        btn_nuevo_horario.ImeMode = ImeMode.NoControl
        btn_nuevo_horario.Location = New Point(91, 94)
        btn_nuevo_horario.Name = "btn_nuevo_horario"
        btn_nuevo_horario.Size = New Size(139, 35)
        btn_nuevo_horario.TabIndex = 62
        btn_nuevo_horario.Text = "NUEVO HORARIO"
        btn_nuevo_horario.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(198, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(447, 37)
        Label1.TabIndex = 61
        Label1.Text = "GESTIÓN DE HORARIOS MEDICOS"
        ' 
        ' HorariosRead
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(txt_buscar)
        Controls.Add(lb_right)
        Controls.Add(lb_middle)
        Controls.Add(lb_left)
        Controls.Add(btn_siguiente)
        Controls.Add(btn_anterior)
        Controls.Add(dgv_horarios)
        Controls.Add(Label3)
        Controls.Add(btn_nuevo_horario)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "HorariosRead"
        StartPosition = FormStartPosition.CenterParent
        CType(dgv_horarios, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txt_buscar As TextBox
    Friend WithEvents lb_right As Label
    Friend WithEvents lb_middle As Label
    Friend WithEvents lb_left As Label
    Friend WithEvents btn_siguiente As LinkLabel
    Friend WithEvents btn_anterior As LinkLabel
    Friend WithEvents dgv_horarios As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents btn_nuevo_horario As Button
    Friend WithEvents Label1 As Label
End Class
