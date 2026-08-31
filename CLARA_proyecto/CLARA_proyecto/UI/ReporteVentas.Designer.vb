<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReporteVentas
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
        dtp_inicio = New DateTimePicker()
        dtp_final = New DateTimePicker()
        btn_generar = New Button()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        lbl_granTotal = New Label()
        dvg_Ventas = New DataGridView()
        Label4 = New Label()
        btn_Descargar = New Button()
        CType(dvg_Ventas, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dtp_inicio
        ' 
        dtp_inicio.Location = New Point(89, 115)
        dtp_inicio.Name = "dtp_inicio"
        dtp_inicio.Size = New Size(200, 23)
        dtp_inicio.TabIndex = 0
        ' 
        ' dtp_final
        ' 
        dtp_final.Location = New Point(418, 115)
        dtp_final.Name = "dtp_final"
        dtp_final.Size = New Size(200, 23)
        dtp_final.TabIndex = 1
        ' 
        ' btn_generar
        ' 
        btn_generar.BackColor = SystemColors.HotTrack
        btn_generar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_generar.ForeColor = Color.Black
        btn_generar.Location = New Point(648, 110)
        btn_generar.Margin = New Padding(3, 2, 3, 2)
        btn_generar.Name = "btn_generar"
        btn_generar.Size = New Size(119, 35)
        btn_generar.TabIndex = 83
        btn_generar.Text = "VER DETALLE"
        btn_generar.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(27, 117)
        Label1.Name = "Label1"
        Label1.Size = New Size(50, 21)
        Label1.TabIndex = 84
        Label1.Text = "Inicio:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(362, 117)
        Label2.Name = "Label2"
        Label2.Size = New Size(46, 21)
        Label2.TabIndex = 85
        Label2.Text = "Final:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(544, 389)
        Label3.Name = "Label3"
        Label3.Size = New Size(0, 21)
        Label3.TabIndex = 86
        ' 
        ' lbl_granTotal
        ' 
        lbl_granTotal.AutoSize = True
        lbl_granTotal.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_granTotal.Location = New Point(580, 389)
        lbl_granTotal.Name = "lbl_granTotal"
        lbl_granTotal.Size = New Size(100, 21)
        lbl_granTotal.TabIndex = 87
        lbl_granTotal.Text = "Total en Caja:"
        ' 
        ' dvg_Ventas
        ' 
        dvg_Ventas.AllowUserToResizeColumns = False
        dvg_Ventas.AllowUserToResizeRows = False
        dvg_Ventas.BackgroundColor = Color.White
        dvg_Ventas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dvg_Ventas.Location = New Point(89, 208)
        dvg_Ventas.Name = "dvg_Ventas"
        dvg_Ventas.ReadOnly = True
        dvg_Ventas.Size = New Size(615, 150)
        dvg_Ventas.TabIndex = 88
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(345, 24)
        Label4.Name = "Label4"
        Label4.Size = New Size(118, 37)
        Label4.TabIndex = 89
        Label4.Text = "VENTAS"
        ' 
        ' btn_Descargar
        ' 
        btn_Descargar.BackColor = SystemColors.HotTrack
        btn_Descargar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btn_Descargar.ForeColor = Color.Black
        btn_Descargar.Location = New Point(89, 389)
        btn_Descargar.Margin = New Padding(3, 2, 3, 2)
        btn_Descargar.Name = "btn_Descargar"
        btn_Descargar.Size = New Size(119, 35)
        btn_Descargar.TabIndex = 96
        btn_Descargar.Text = "DESCARGAR"
        btn_Descargar.UseVisualStyleBackColor = False
        ' 
        ' ReporteVentas
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btn_Descargar)
        Controls.Add(Label4)
        Controls.Add(dvg_Ventas)
        Controls.Add(lbl_granTotal)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(btn_generar)
        Controls.Add(dtp_final)
        Controls.Add(dtp_inicio)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "ReporteVentas"
        StartPosition = FormStartPosition.CenterParent
        CType(dvg_Ventas, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dtp_inicio As DateTimePicker
    Friend WithEvents dtp_final As DateTimePicker
    Friend WithEvents btn_generar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lbl_granTotal As Label
    Friend WithEvents dvg_Ventas As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents btn_Descargar As Button
End Class
