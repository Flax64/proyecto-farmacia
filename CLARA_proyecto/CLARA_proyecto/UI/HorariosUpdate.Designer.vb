<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HorariosUpdate
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
        dtp_HoraSalida = New DateTimePicker()
        Label5 = New Label()
        btn_cancelar = New Button()
        btn_guardar = New Button()
        Label4 = New Label()
        dtp_HoraEntrada = New DateTimePicker()
        cmb_Medico = New ComboBox()
        Label2 = New Label()
        Label1 = New Label()
        Label3 = New Label()
        cmb_Dia = New ComboBox()
        SuspendLayout()
        ' 
        ' dtp_HoraSalida
        ' 
        dtp_HoraSalida.CustomFormat = "hh:mm tt"
        dtp_HoraSalida.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtp_HoraSalida.Format = DateTimePickerFormat.Custom
        dtp_HoraSalida.Location = New Point(52, 279)
        dtp_HoraSalida.MinDate = New Date(2026, 3, 29, 0, 0, 0, 0)
        dtp_HoraSalida.Name = "dtp_HoraSalida"
        dtp_HoraSalida.ShowUpDown = True
        dtp_HoraSalida.Size = New Size(124, 25)
        dtp_HoraSalida.TabIndex = 4
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(50, 255)
        Label5.Name = "Label5"
        Label5.Size = New Size(114, 21)
        Label5.TabIndex = 58
        Label5.Text = "Hora de Salida:"
        ' 
        ' btn_cancelar
        ' 
        btn_cancelar.BackColor = SystemColors.HotTrack
        btn_cancelar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_cancelar.Location = New Point(207, 345)
        btn_cancelar.Name = "btn_cancelar"
        btn_cancelar.Size = New Size(116, 35)
        btn_cancelar.TabIndex = 6
        btn_cancelar.Text = "CANCELAR"
        btn_cancelar.UseVisualStyleBackColor = False
        ' 
        ' btn_guardar
        ' 
        btn_guardar.BackColor = SystemColors.HotTrack
        btn_guardar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_guardar.Location = New Point(40, 345)
        btn_guardar.Name = "btn_guardar"
        btn_guardar.Size = New Size(161, 35)
        btn_guardar.TabIndex = 5
        btn_guardar.Text = "ACTUALIZAR HORARIO"
        btn_guardar.UseVisualStyleBackColor = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(51, 187)
        Label4.Name = "Label4"
        Label4.Size = New Size(125, 21)
        Label4.TabIndex = 55
        Label4.Text = "Hora de Entrada:"
        ' 
        ' dtp_HoraEntrada
        ' 
        dtp_HoraEntrada.CustomFormat = "hh:mm tt"
        dtp_HoraEntrada.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtp_HoraEntrada.Format = DateTimePickerFormat.Custom
        dtp_HoraEntrada.Location = New Point(51, 211)
        dtp_HoraEntrada.MinDate = New Date(2026, 3, 29, 0, 0, 0, 0)
        dtp_HoraEntrada.Name = "dtp_HoraEntrada"
        dtp_HoraEntrada.ShowUpDown = True
        dtp_HoraEntrada.Size = New Size(124, 25)
        dtp_HoraEntrada.TabIndex = 3
        ' 
        ' cmb_Medico
        ' 
        cmb_Medico.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_Medico.AutoCompleteSource = AutoCompleteSource.ListItems
        cmb_Medico.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_Medico.FormattingEnabled = True
        cmb_Medico.Location = New Point(51, 98)
        cmb_Medico.Name = "cmb_Medico"
        cmb_Medico.Size = New Size(308, 23)
        cmb_Medico.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(51, 74)
        Label2.Name = "Label2"
        Label2.Size = New Size(64, 21)
        Label2.TabIndex = 52
        Label2.Text = "Médico:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(72, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(243, 37)
        Label1.TabIndex = 51
        Label1.Text = "EDITAR HORARIO"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(51, 131)
        Label3.Name = "Label3"
        Label3.Size = New Size(36, 21)
        Label3.TabIndex = 50
        Label3.Text = "Día:"
        ' 
        ' cmb_Dia
        ' 
        cmb_Dia.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_Dia.AutoCompleteSource = AutoCompleteSource.ListItems
        cmb_Dia.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_Dia.FormattingEnabled = True
        cmb_Dia.Location = New Point(51, 155)
        cmb_Dia.Name = "cmb_Dia"
        cmb_Dia.Size = New Size(250, 23)
        cmb_Dia.TabIndex = 2
        ' 
        ' HorariosUpdate
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(423, 401)
        ControlBox = False
        Controls.Add(dtp_HoraSalida)
        Controls.Add(Label5)
        Controls.Add(btn_cancelar)
        Controls.Add(btn_guardar)
        Controls.Add(Label4)
        Controls.Add(dtp_HoraEntrada)
        Controls.Add(cmb_Medico)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Label3)
        Controls.Add(cmb_Dia)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "HorariosUpdate"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dtp_HoraSalida As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents btn_cancelar As Button
    Friend WithEvents btn_guardar As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents dtp_HoraEntrada As DateTimePicker
    Friend WithEvents cmb_Medico As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmb_Dia As ComboBox
End Class
