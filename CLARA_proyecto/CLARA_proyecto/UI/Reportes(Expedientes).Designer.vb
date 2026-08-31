<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reportes_Expedientes_
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
        tb_Buscar = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        dgv_Expedientes = New DataGridView()
        btn_detalle = New Button()
        dgv_Historial = New DataGridView()
        btn_Descargar = New Button()
        CType(dgv_Expedientes, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgv_Historial, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' tb_Buscar
        ' 
        tb_Buscar.Location = New Point(185, 96)
        tb_Buscar.Name = "tb_Buscar"
        tb_Buscar.Size = New Size(264, 23)
        tb_Buscar.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(304, 25)
        Label1.Name = "Label1"
        Label1.Size = New Size(190, 37)
        Label1.TabIndex = 36
        Label1.Text = "EXPEDIENTES"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(47, 98)
        Label2.Name = "Label2"
        Label2.Size = New Size(132, 21)
        Label2.TabIndex = 37
        Label2.Text = "Nombre Paciente:"
        ' 
        ' dgv_Expedientes
        ' 
        dgv_Expedientes.AllowUserToAddRows = False
        dgv_Expedientes.AllowUserToDeleteRows = False
        dgv_Expedientes.AllowUserToResizeColumns = False
        dgv_Expedientes.AllowUserToResizeRows = False
        dgv_Expedientes.BackgroundColor = Color.White
        dgv_Expedientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_Expedientes.Location = New Point(47, 181)
        dgv_Expedientes.Name = "dgv_Expedientes"
        dgv_Expedientes.ReadOnly = True
        dgv_Expedientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_Expedientes.Size = New Size(695, 150)
        dgv_Expedientes.TabIndex = 38
        ' 
        ' btn_detalle
        ' 
        btn_detalle.BackColor = SystemColors.HotTrack
        btn_detalle.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_detalle.ForeColor = Color.Black
        btn_detalle.Location = New Point(623, 96)
        btn_detalle.Margin = New Padding(3, 2, 3, 2)
        btn_detalle.Name = "btn_detalle"
        btn_detalle.Size = New Size(119, 35)
        btn_detalle.TabIndex = 82
        btn_detalle.Text = "VER DETALLE"
        btn_detalle.UseVisualStyleBackColor = False
        ' 
        ' dgv_Historial
        ' 
        dgv_Historial.AllowUserToAddRows = False
        dgv_Historial.AllowUserToDeleteRows = False
        dgv_Historial.AllowUserToResizeColumns = False
        dgv_Historial.AllowUserToResizeRows = False
        dgv_Historial.BackgroundColor = Color.White
        dgv_Historial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_Historial.Location = New Point(47, 346)
        dgv_Historial.Name = "dgv_Historial"
        dgv_Historial.ReadOnly = True
        dgv_Historial.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_Historial.Size = New Size(695, 137)
        dgv_Historial.TabIndex = 83
        ' 
        ' btn_Descargar
        ' 
        btn_Descargar.BackColor = SystemColors.HotTrack
        btn_Descargar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_Descargar.ForeColor = Color.Black
        btn_Descargar.Location = New Point(623, 493)
        btn_Descargar.Margin = New Padding(3, 2, 3, 2)
        btn_Descargar.Name = "btn_Descargar"
        btn_Descargar.Size = New Size(119, 35)
        btn_Descargar.TabIndex = 96
        btn_Descargar.Text = "DESCARGAR"
        btn_Descargar.UseVisualStyleBackColor = False
        ' 
        ' Reportes_Expedientes_
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 539)
        Controls.Add(btn_Descargar)
        Controls.Add(dgv_Historial)
        Controls.Add(btn_detalle)
        Controls.Add(dgv_Expedientes)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(tb_Buscar)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "Reportes_Expedientes_"
        StartPosition = FormStartPosition.CenterParent
        CType(dgv_Expedientes, ComponentModel.ISupportInitialize).EndInit()
        CType(dgv_Historial, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tb_Buscar As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents dgv_Expedientes As DataGridView
    Friend WithEvents btn_detalle As Button
    Friend WithEvents dgv_Historial As DataGridView
    Friend WithEvents btn_Descargar As Button
End Class
