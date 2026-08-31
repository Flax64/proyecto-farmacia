<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CitasCreate
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
        cmb_Paciente = New ComboBox()
        Label3 = New Label()
        Label1 = New Label()
        Label2 = New Label()
        cmb_Medico = New ComboBox()
        dtp_Fecha = New DateTimePicker()
        Label4 = New Label()
        cmb_Hora = New ComboBox()
        Label5 = New Label()
        btn_cancelar = New Button()
        btn_guardar = New Button()
        SuspendLayout()
        ' 
        ' cmb_Paciente
        ' 
        cmb_Paciente.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_Paciente.AutoCompleteSource = AutoCompleteSource.ListItems
        cmb_Paciente.FormattingEnabled = True
        cmb_Paciente.Location = New Point(49, 98)
        cmb_Paciente.Name = "cmb_Paciente"
        cmb_Paciente.Size = New Size(308, 23)
        cmb_Paciente.TabIndex = 24
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(49, 74)
        Label3.Name = "Label3"
        Label3.Size = New Size(70, 21)
        Label3.TabIndex = 25
        Label3.Text = "Paciente:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(125, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(174, 37)
        Label1.TabIndex = 26
        Label1.Text = "NUEVA CITA"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(49, 143)
        Label2.Name = "Label2"
        Label2.Size = New Size(64, 21)
        Label2.TabIndex = 27
        Label2.Text = "Médico:"
        ' 
        ' cmb_Medico
        ' 
        cmb_Medico.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_Medico.AutoCompleteSource = AutoCompleteSource.ListItems
        cmb_Medico.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_Medico.FormattingEnabled = True
        cmb_Medico.Location = New Point(49, 167)
        cmb_Medico.Name = "cmb_Medico"
        cmb_Medico.Size = New Size(308, 23)
        cmb_Medico.TabIndex = 28
        ' 
        ' dtp_Fecha
        ' 
        dtp_Fecha.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtp_Fecha.Format = DateTimePickerFormat.Short
        dtp_Fecha.Location = New Point(49, 239)
        dtp_Fecha.MinDate = New Date(2026, 3, 29, 0, 0, 0, 0)
        dtp_Fecha.Name = "dtp_Fecha"
        dtp_Fecha.Size = New Size(124, 25)
        dtp_Fecha.TabIndex = 29
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(49, 215)
        Label4.Name = "Label4"
        Label4.Size = New Size(105, 21)
        Label4.TabIndex = 30
        Label4.Text = "Fecha de Cita:"
        ' 
        ' cmb_Hora
        ' 
        cmb_Hora.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_Hora.AutoCompleteSource = AutoCompleteSource.ListItems
        cmb_Hora.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_Hora.FormattingEnabled = True
        cmb_Hora.Location = New Point(231, 241)
        cmb_Hora.Name = "cmb_Hora"
        cmb_Hora.Size = New Size(126, 23)
        cmb_Hora.TabIndex = 31
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(231, 215)
        Label5.Name = "Label5"
        Label5.Size = New Size(99, 21)
        Label5.TabIndex = 32
        Label5.Text = "Hora de Cita:"
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.Location = New Point(205, 345)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(116, 35)
        btn_cancelar.TabIndex = 34
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' btn_guardar
        ' 
        btn_guardar.BackColor = SystemColors.HotTrack
        btn_guardar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_guardar.Location = New Point(83, 345)
        btn_guardar.Name = "btn_guardar"
        btn_guardar.Size = New Size(116, 35)
        btn_guardar.TabIndex = 33
        btn_guardar.Text = "AGENDAR"
        btn_guardar.UseVisualStyleBackColor = False
        ' 
        ' CitasCreate
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(423, 401)
        ControlBox = False
        Controls.Add(btn_cancelar)
        Controls.Add(btn_guardar)
        Controls.Add(Label5)
        Controls.Add(cmb_Hora)
        Controls.Add(Label4)
        Controls.Add(dtp_Fecha)
        Controls.Add(cmb_Medico)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Label3)
        Controls.Add(cmb_Paciente)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "CitasCreate"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cmb_Paciente As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmb_Medico As ComboBox
    Friend WithEvents dtp_Fecha As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents cmb_Hora As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents btn_guardar As Button
End Class
