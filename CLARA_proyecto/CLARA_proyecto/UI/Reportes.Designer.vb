<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reportes
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
        btnExpedientes = New Button()
        btn_Reporte_Ventas = New Button()
        btn_Reporte_Inventario = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(164, 31)
        Label1.Name = "Label1"
        Label1.Size = New Size(148, 37)
        Label1.TabIndex = 35
        Label1.Text = "REPORTES"
        ' 
        ' btnExpedientes
        ' 
        btnExpedientes.BackColor = SystemColors.HotTrack
        btnExpedientes.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnExpedientes.ForeColor = Color.Black
        btnExpedientes.Location = New Point(61, 146)
        btnExpedientes.Margin = New Padding(3, 2, 3, 2)
        btnExpedientes.Name = "btnExpedientes"
        btnExpedientes.Size = New Size(377, 35)
        btnExpedientes.TabIndex = 41
        btnExpedientes.Text = "1.- EXPEDIENTES"
        btnExpedientes.UseVisualStyleBackColor = False
        ' 
        ' btn_Reporte_Ventas
        ' 
        btn_Reporte_Ventas.BackColor = SystemColors.HotTrack
        btn_Reporte_Ventas.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_Reporte_Ventas.ForeColor = Color.Black
        btn_Reporte_Ventas.Location = New Point(61, 214)
        btn_Reporte_Ventas.Margin = New Padding(3, 2, 3, 2)
        btn_Reporte_Ventas.Name = "btn_Reporte_Ventas"
        btn_Reporte_Ventas.Size = New Size(377, 35)
        btn_Reporte_Ventas.TabIndex = 42
        btn_Reporte_Ventas.Text = "2.- REPORTE DE VENTAS"
        btn_Reporte_Ventas.UseVisualStyleBackColor = False
        ' 
        ' btn_Reporte_Inventario
        ' 
        btn_Reporte_Inventario.BackColor = SystemColors.HotTrack
        btn_Reporte_Inventario.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_Reporte_Inventario.ForeColor = Color.Black
        btn_Reporte_Inventario.Location = New Point(61, 285)
        btn_Reporte_Inventario.Margin = New Padding(3, 2, 3, 2)
        btn_Reporte_Inventario.Name = "btn_Reporte_Inventario"
        btn_Reporte_Inventario.Size = New Size(377, 35)
        btn_Reporte_Inventario.TabIndex = 43
        btn_Reporte_Inventario.Text = "3.- REPORTE DE INVENTARIO"
        btn_Reporte_Inventario.UseVisualStyleBackColor = False
        ' 
        ' Reportes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(514, 450)
        Controls.Add(btn_Reporte_Inventario)
        Controls.Add(btn_Reporte_Ventas)
        Controls.Add(btnExpedientes)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "Reportes"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btnExpedientes As Button
    Friend WithEvents btn_Reporte_Ventas As Button
    Friend WithEvents btn_Reporte_Inventario As Button
End Class
