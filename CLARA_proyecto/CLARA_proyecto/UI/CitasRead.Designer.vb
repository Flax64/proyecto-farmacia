<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CitasRead
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
        Label1 = New Label()
        txt_buscar = New TextBox()
        lb_right = New Label()
        lb_middle = New Label()
        lb_left = New Label()
        btn_siguiente = New LinkLabel()
        btn_anterior = New LinkLabel()
        dgv_citas = New DataGridView()
        Label2 = New Label()
        dtp_filtro_fecha = New DateTimePicker()
        Label3 = New Label()
        btn_nueva_cita = New Button()
        CType(dgv_citas, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(266, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(253, 37)
        Label1.TabIndex = 1
        Label1.Text = "GESTIÓN DE CITAS"
        ' 
        ' txt_buscar
        ' 
        txt_buscar.BackColor = Color.Silver
        txt_buscar.Font = New Font("Segoe UI", 12F)
        txt_buscar.Location = New Point(310, 70)
        txt_buscar.MaxLength = 50
        txt_buscar.Name = "txt_buscar"
        txt_buscar.Size = New Size(300, 29)
        txt_buscar.TabIndex = 60
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
        lb_right.TabIndex = 59
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
        lb_middle.TabIndex = 58
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
        lb_left.TabIndex = 57
        lb_left.Text = "1"
        ' 
        ' btn_siguiente
        ' 
        btn_siguiente.AutoSize = True
        btn_siguiente.ImeMode = ImeMode.NoControl
        btn_siguiente.LinkColor = Color.Black
        btn_siguiente.Location = New Point(435, 372)
        btn_siguiente.Name = "btn_siguiente"
        btn_siguiente.Size = New Size(67, 15)
        btn_siguiente.TabIndex = 56
        btn_siguiente.TabStop = True
        btn_siguiente.Text = "Siguiente >"
        btn_siguiente.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_anterior
        ' 
        btn_anterior.AutoSize = True
        btn_anterior.ImeMode = ImeMode.NoControl
        btn_anterior.LinkColor = Color.Black
        btn_anterior.Location = New Point(284, 372)
        btn_anterior.Name = "btn_anterior"
        btn_anterior.Size = New Size(61, 15)
        btn_anterior.TabIndex = 55
        btn_anterior.TabStop = True
        btn_anterior.Text = "< Anterior"
        btn_anterior.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dgv_citas
        ' 
        dgv_citas.AllowUserToResizeColumns = False
        dgv_citas.AllowUserToResizeRows = False
        dgv_citas.BackgroundColor = Color.White
        dgv_citas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_citas.Location = New Point(74, 172)
        dgv_citas.Name = "dgv_citas"
        dgv_citas.ReadOnly = True
        dgv_citas.Size = New Size(653, 185)
        dgv_citas.TabIndex = 54
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
        Label2.TabIndex = 53
        Label2.Text = "Fecha:"
        ' 
        ' dtp_filtro_fecha
        ' 
        dtp_filtro_fecha.CalendarMonthBackground = Color.Tan
        dtp_filtro_fecha.Location = New Point(159, 131)
        dtp_filtro_fecha.Name = "dtp_filtro_fecha"
        dtp_filtro_fecha.ShowCheckBox = True
        dtp_filtro_fecha.Size = New Size(258, 23)
        dtp_filtro_fecha.TabIndex = 52
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
        Label3.TabIndex = 51
        Label3.Text = "Buscar:"
        ' 
        ' btn_nueva_cita
        ' 
        btn_nueva_cita.BackColor = SystemColors.HotTrack
        btn_nueva_cita.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_nueva_cita.ForeColor = Color.Black
        btn_nueva_cita.ImeMode = ImeMode.NoControl
        btn_nueva_cita.Location = New Point(100, 64)
        btn_nueva_cita.Name = "btn_nueva_cita"
        btn_nueva_cita.Size = New Size(139, 35)
        btn_nueva_cita.TabIndex = 50
        btn_nueva_cita.Text = "NUEVA CITA"
        btn_nueva_cita.UseVisualStyleBackColor = False
        ' 
        ' CitasRead
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
        Controls.Add(dgv_citas)
        Controls.Add(Label2)
        Controls.Add(dtp_filtro_fecha)
        Controls.Add(Label3)
        Controls.Add(btn_nueva_cita)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "CitasRead"
        StartPosition = FormStartPosition.CenterParent
        CType(dgv_citas, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txt_buscar As TextBox
    Friend WithEvents lb_right As Label
    Friend WithEvents lb_middle As Label
    Friend WithEvents lb_left As Label
    Friend WithEvents btn_siguiente As LinkLabel
    Friend WithEvents btn_anterior As LinkLabel
    Friend WithEvents dgv_citas As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents dtp_filtro_fecha As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents btn_nueva_cita As Button
End Class
