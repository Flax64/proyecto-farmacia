<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RegistrarConsulta
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
        Label3 = New Label()
        cmb_cita = New ComboBox()
        Label2 = New Label()
        txt_sintomas = New TextBox()
        Label4 = New Label()
        txt_diagnostico = New TextBox()
        txt_observaciones = New TextBox()
        Label5 = New Label()
        num_peso = New NumericUpDown()
        Label6 = New Label()
        Label7 = New Label()
        num_altura = New NumericUpDown()
        cmb_medicamento = New ComboBox()
        Label8 = New Label()
        txt_dosis = New TextBox()
        Label9 = New Label()
        Label10 = New Label()
        txt_frecuencia = New TextBox()
        Label11 = New Label()
        txt_duracion = New TextBox()
        btn_agregar_medicamento = New Button()
        dgv_receta = New DataGridView()
        btn_guardar_consulta = New Button()
        Label12 = New Label()
        Label13 = New Label()
        CType(num_peso, ComponentModel.ISupportInitialize).BeginInit()
        CType(num_altura, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgv_receta, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(354, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(308, 37)
        Label1.TabIndex = 27
        Label1.Text = "REGISTRAR CONSULTA"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(33, 51)
        Label3.Name = "Label3"
        Label3.Size = New Size(217, 21)
        Label3.TabIndex = 29
        Label3.Text = "Seleccione la cita programada"
        ' 
        ' cmb_cita
        ' 
        cmb_cita.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_cita.AutoCompleteSource = AutoCompleteSource.ListItems
        cmb_cita.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_cita.FormattingEnabled = True
        cmb_cita.Location = New Point(33, 75)
        cmb_cita.Name = "cmb_cita"
        cmb_cita.Size = New Size(343, 23)
        cmb_cita.TabIndex = 0
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(33, 103)
        Label2.Name = "Label2"
        Label2.Size = New Size(162, 21)
        Label2.TabIndex = 30
        Label2.Text = "Sintomas del paciente"
        ' 
        ' txt_sintomas
        ' 
        txt_sintomas.BackColor = Color.Silver
        txt_sintomas.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_sintomas.Location = New Point(33, 127)
        txt_sintomas.MaxLength = 500
        txt_sintomas.Multiline = True
        txt_sintomas.Name = "txt_sintomas"
        txt_sintomas.Size = New Size(898, 60)
        txt_sintomas.TabIndex = 3
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(33, 195)
        Label4.Name = "Label4"
        Label4.Size = New Size(92, 21)
        Label4.TabIndex = 32
        Label4.Text = "Diagnóstico"
        ' 
        ' txt_diagnostico
        ' 
        txt_diagnostico.BackColor = Color.Silver
        txt_diagnostico.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_diagnostico.Location = New Point(33, 219)
        txt_diagnostico.MaxLength = 500
        txt_diagnostico.Multiline = True
        txt_diagnostico.Name = "txt_diagnostico"
        txt_diagnostico.Size = New Size(898, 60)
        txt_diagnostico.TabIndex = 4
        ' 
        ' txt_observaciones
        ' 
        txt_observaciones.BackColor = Color.Silver
        txt_observaciones.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_observaciones.Location = New Point(524, 313)
        txt_observaciones.MaxLength = 300
        txt_observaciones.Multiline = True
        txt_observaciones.Name = "txt_observaciones"
        txt_observaciones.Size = New Size(407, 140)
        txt_observaciones.TabIndex = 10
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(524, 289)
        Label5.Name = "Label5"
        Label5.Size = New Size(167, 21)
        Label5.TabIndex = 35
        Label5.Text = "Observaciones / Notas"
        ' 
        ' num_peso
        ' 
        num_peso.DecimalPlaces = 2
        num_peso.Location = New Point(690, 75)
        num_peso.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        num_peso.Name = "num_peso"
        num_peso.Size = New Size(58, 23)
        num_peso.TabIndex = 1
        num_peso.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(641, 75)
        Label6.Name = "Label6"
        Label6.Size = New Size(45, 21)
        Label6.TabIndex = 69
        Label6.Text = "Peso:"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(788, 74)
        Label7.Name = "Label7"
        Label7.Size = New Size(55, 21)
        Label7.TabIndex = 71
        Label7.Text = "Altura:"
        ' 
        ' num_altura
        ' 
        num_altura.DecimalPlaces = 2
        num_altura.Location = New Point(847, 75)
        num_altura.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        num_altura.Name = "num_altura"
        num_altura.Size = New Size(58, 23)
        num_altura.TabIndex = 2
        num_altura.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' cmb_medicamento
        ' 
        cmb_medicamento.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_medicamento.AutoCompleteSource = AutoCompleteSource.ListItems
        cmb_medicamento.FormattingEnabled = True
        cmb_medicamento.Location = New Point(150, 298)
        cmb_medicamento.Name = "cmb_medicamento"
        cmb_medicamento.Size = New Size(265, 23)
        cmb_medicamento.TabIndex = 5
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(36, 295)
        Label8.Name = "Label8"
        Label8.Size = New Size(108, 21)
        Label8.TabIndex = 73
        Label8.Text = "Medicamento:"
        ' 
        ' txt_dosis
        ' 
        txt_dosis.BackColor = Color.Silver
        txt_dosis.Font = New Font("Segoe UI", 12F)
        txt_dosis.Location = New Point(90, 327)
        txt_dosis.MaxLength = 30
        txt_dosis.Name = "txt_dosis"
        txt_dosis.Size = New Size(217, 29)
        txt_dosis.TabIndex = 6
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(36, 330)
        Label9.Name = "Label9"
        Label9.Size = New Size(51, 21)
        Label9.TabIndex = 75
        Label9.Text = "Dosís:"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(36, 368)
        Label10.Name = "Label10"
        Label10.Size = New Size(87, 21)
        Label10.TabIndex = 77
        Label10.Text = "Frecuencia:"
        ' 
        ' txt_frecuencia
        ' 
        txt_frecuencia.BackColor = Color.Silver
        txt_frecuencia.Font = New Font("Segoe UI", 12F)
        txt_frecuencia.Location = New Point(129, 365)
        txt_frecuencia.MaxLength = 30
        txt_frecuencia.Name = "txt_frecuencia"
        txt_frecuencia.Size = New Size(217, 29)
        txt_frecuencia.TabIndex = 7
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label11.Location = New Point(36, 403)
        Label11.Name = "Label11"
        Label11.Size = New Size(76, 21)
        Label11.TabIndex = 79
        Label11.Text = "Duración:"
        ' 
        ' txt_duracion
        ' 
        txt_duracion.BackColor = Color.Silver
        txt_duracion.Font = New Font("Segoe UI", 12F)
        txt_duracion.Location = New Point(129, 400)
        txt_duracion.MaxLength = 30
        txt_duracion.Name = "txt_duracion"
        txt_duracion.Size = New Size(217, 29)
        txt_duracion.TabIndex = 8
        ' 
        ' btn_agregar_medicamento
        ' 
        btn_agregar_medicamento.BackColor = SystemColors.HotTrack
        btn_agregar_medicamento.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_agregar_medicamento.ForeColor = Color.Black
        btn_agregar_medicamento.Location = New Point(389, 394)
        btn_agregar_medicamento.Margin = New Padding(3, 2, 3, 2)
        btn_agregar_medicamento.Name = "btn_agregar_medicamento"
        btn_agregar_medicamento.Size = New Size(95, 35)
        btn_agregar_medicamento.TabIndex = 9
        btn_agregar_medicamento.Text = "AGREGAR"
        btn_agregar_medicamento.UseVisualStyleBackColor = False
        ' 
        ' dgv_receta
        ' 
        dgv_receta.AllowUserToResizeColumns = False
        dgv_receta.AllowUserToResizeRows = False
        dgv_receta.BackgroundColor = Color.White
        dgv_receta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_receta.Location = New Point(36, 465)
        dgv_receta.Name = "dgv_receta"
        dgv_receta.ReadOnly = True
        dgv_receta.Size = New Size(599, 115)
        dgv_receta.TabIndex = 82
        ' 
        ' btn_guardar_consulta
        ' 
        btn_guardar_consulta.BackColor = SystemColors.HotTrack
        btn_guardar_consulta.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_guardar_consulta.ForeColor = Color.Black
        btn_guardar_consulta.Location = New Point(756, 545)
        btn_guardar_consulta.Margin = New Padding(3, 2, 3, 2)
        btn_guardar_consulta.Name = "btn_guardar_consulta"
        btn_guardar_consulta.Size = New Size(174, 35)
        btn_guardar_consulta.TabIndex = 11
        btn_guardar_consulta.Text = "REGISTRAR CONSULTA"
        btn_guardar_consulta.UseVisualStyleBackColor = False
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.Location = New Point(754, 75)
        Label12.Name = "Label12"
        Label12.Size = New Size(27, 21)
        Label12.TabIndex = 85
        Label12.Text = "kg"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label13.Location = New Point(907, 75)
        Label13.Name = "Label13"
        Label13.Size = New Size(24, 21)
        Label13.TabIndex = 86
        Label13.Text = "m"
        ' 
        ' RegistrarConsulta
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(959, 605)
        Controls.Add(Label13)
        Controls.Add(Label12)
        Controls.Add(btn_guardar_consulta)
        Controls.Add(dgv_receta)
        Controls.Add(btn_agregar_medicamento)
        Controls.Add(Label11)
        Controls.Add(txt_duracion)
        Controls.Add(Label10)
        Controls.Add(txt_frecuencia)
        Controls.Add(Label9)
        Controls.Add(txt_dosis)
        Controls.Add(Label8)
        Controls.Add(cmb_medicamento)
        Controls.Add(Label7)
        Controls.Add(num_altura)
        Controls.Add(Label6)
        Controls.Add(num_peso)
        Controls.Add(Label5)
        Controls.Add(txt_observaciones)
        Controls.Add(txt_diagnostico)
        Controls.Add(Label4)
        Controls.Add(txt_sintomas)
        Controls.Add(Label2)
        Controls.Add(Label3)
        Controls.Add(cmb_cita)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "RegistrarConsulta"
        StartPosition = FormStartPosition.CenterParent
        CType(num_peso, ComponentModel.ISupportInitialize).EndInit()
        CType(num_altura, ComponentModel.ISupportInitialize).EndInit()
        CType(dgv_receta, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmb_cita As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_sintomas As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_diagnostico As TextBox
    Friend WithEvents txt_observaciones As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents num_peso As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents num_altura As NumericUpDown
    Friend WithEvents cmb_medicamento As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_dosis As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_frecuencia As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txt_duracion As TextBox
    Friend WithEvents btn_agregar_medicamento As Button
    Friend WithEvents dgv_receta As DataGridView
    Friend WithEvents btn_guardar_consulta As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
End Class
