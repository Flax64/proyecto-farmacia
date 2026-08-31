<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReporteInventario
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReporteInventario))
        Label4 = New Label()
        dgv_Inventario = New DataGridView()
        txt_Buscar = New TextBox()
        btn_refrescar = New Button()
        btn_Descargar = New Button()
        Label1 = New Label()
        PrintPreviewDialog1 = New PrintPreviewDialog()
        PrintDocument1 = New Printing.PrintDocument()
        CType(dgv_Inventario, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(250, 24)
        Label4.Name = "Label4"
        Label4.Size = New Size(354, 37)
        Label4.TabIndex = 90
        Label4.Text = "CONTROL DE INVENTARIO"
        ' 
        ' dgv_Inventario
        ' 
        dgv_Inventario.AllowUserToResizeColumns = False
        dgv_Inventario.AllowUserToResizeRows = False
        dgv_Inventario.BackgroundColor = Color.White
        dgv_Inventario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_Inventario.Location = New Point(68, 145)
        dgv_Inventario.Name = "dgv_Inventario"
        dgv_Inventario.ReadOnly = True
        dgv_Inventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_Inventario.Size = New Size(667, 150)
        dgv_Inventario.TabIndex = 91
        ' 
        ' txt_Buscar
        ' 
        txt_Buscar.Location = New Point(182, 84)
        txt_Buscar.Name = "txt_Buscar"
        txt_Buscar.Size = New Size(298, 23)
        txt_Buscar.TabIndex = 92
        ' 
        ' btn_refrescar
        ' 
        btn_refrescar.BackColor = SystemColors.HotTrack
        btn_refrescar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_refrescar.ForeColor = Color.Black
        btn_refrescar.Location = New Point(616, 72)
        btn_refrescar.Margin = New Padding(3, 2, 3, 2)
        btn_refrescar.Name = "btn_refrescar"
        btn_refrescar.Size = New Size(119, 35)
        btn_refrescar.TabIndex = 94
        btn_refrescar.Text = "ACTUALIZAR"
        btn_refrescar.UseVisualStyleBackColor = False
        ' 
        ' btn_Descargar
        ' 
        btn_Descargar.BackColor = SystemColors.HotTrack
        btn_Descargar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_Descargar.ForeColor = Color.Black
        btn_Descargar.Location = New Point(616, 361)
        btn_Descargar.Margin = New Padding(3, 2, 3, 2)
        btn_Descargar.Name = "btn_Descargar"
        btn_Descargar.Size = New Size(119, 35)
        btn_Descargar.TabIndex = 95
        btn_Descargar.Text = "DESCARGAR"
        btn_Descargar.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(68, 86)
        Label1.Name = "Label1"
        Label1.Size = New Size(108, 21)
        Label1.TabIndex = 96
        Label1.Text = "Medicamento:"
        ' 
        ' PrintPreviewDialog1
        ' 
        PrintPreviewDialog1.AutoScrollMargin = New Size(0, 0)
        PrintPreviewDialog1.AutoScrollMinSize = New Size(0, 0)
        PrintPreviewDialog1.ClientSize = New Size(400, 300)
        PrintPreviewDialog1.Enabled = True
        PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), Icon)
        PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        PrintPreviewDialog1.Visible = False
        ' 
        ' ReporteInventario
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Label1)
        Controls.Add(btn_Descargar)
        Controls.Add(btn_refrescar)
        Controls.Add(txt_Buscar)
        Controls.Add(dgv_Inventario)
        Controls.Add(Label4)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "ReporteInventario"
        StartPosition = FormStartPosition.CenterParent
        CType(dgv_Inventario, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label4 As Label
    Friend WithEvents dgv_Inventario As DataGridView
    Friend WithEvents txt_Buscar As TextBox
    Friend WithEvents btn_refrescar As Button
    Friend WithEvents btn_Descargar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
End Class
