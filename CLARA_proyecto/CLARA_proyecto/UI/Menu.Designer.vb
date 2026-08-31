<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Menu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Menu))
        btn_ventas = New Button()
        Titulo = New Label()
        Ventas_PB = New PictureBox()
        Medicamentos_PB = New PictureBox()
        btn_medicamentos = New Button()
        Empleados_PB = New PictureBox()
        btn_empleados = New Button()
        btn_citas = New Button()
        Citas_PB = New PictureBox()
        btn_compras = New Button()
        btn_horarios = New Button()
        btn_registrar_consulta = New Button()
        menu_principal = New MenuStrip()
        MiPerfilToolStripMenuItem = New ToolStripMenuItem()
        EmpleadosToolStripMenuItem = New ToolStripMenuItem()
        GestionDeEmpleadosToolStripMenuItem = New ToolStripMenuItem()
        RolesToolStripMenuItem1 = New ToolStripMenuItem()
        HorarioDeMedicosToolStripMenuItem = New ToolStripMenuItem()
        MedicamentosToolStripMenuItem = New ToolStripMenuItem()
        CitasToolStripMenuItem = New ToolStripMenuItem()
        GestiónDeCitasToolStripMenuItem = New ToolStripMenuItem()
        RegistrarConsultaToolStripMenuItem = New ToolStripMenuItem()
        VentasToolStripMenuItem = New ToolStripMenuItem()
        ComprasToolStripMenuItem = New ToolStripMenuItem()
        ReportesToolStripMenuItem = New ToolStripMenuItem()
        PictureBox1 = New PictureBox()
        btn_reportes = New Button()
        CType(Ventas_PB, ComponentModel.ISupportInitialize).BeginInit()
        CType(Medicamentos_PB, ComponentModel.ISupportInitialize).BeginInit()
        CType(Empleados_PB, ComponentModel.ISupportInitialize).BeginInit()
        CType(Citas_PB, ComponentModel.ISupportInitialize).BeginInit()
        menu_principal.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_ventas
        ' 
        btn_ventas.Location = New Point(103, 196)
        btn_ventas.Name = "btn_ventas"
        btn_ventas.Size = New Size(75, 23)
        btn_ventas.TabIndex = 0
        btn_ventas.Text = "Ventas"
        btn_ventas.UseVisualStyleBackColor = True
        ' 
        ' Titulo
        ' 
        Titulo.AutoSize = True
        Titulo.Font = New Font("Segoe UI Historic", 18F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Titulo.Location = New Point(224, 29)
        Titulo.Name = "Titulo"
        Titulo.Size = New Size(144, 32)
        Titulo.TabIndex = 1
        Titulo.Text = "Bienvenido"
        ' 
        ' Ventas_PB
        ' 
        Ventas_PB.BackColor = SystemColors.Control
        Ventas_PB.Image = CType(resources.GetObject("Ventas_PB.Image"), Image)
        Ventas_PB.Location = New Point(61, 82)
        Ventas_PB.Name = "Ventas_PB"
        Ventas_PB.Size = New Size(236, 108)
        Ventas_PB.SizeMode = PictureBoxSizeMode.StretchImage
        Ventas_PB.TabIndex = 2
        Ventas_PB.TabStop = False
        ' 
        ' Medicamentos_PB
        ' 
        Medicamentos_PB.Image = CType(resources.GetObject("Medicamentos_PB.Image"), Image)
        Medicamentos_PB.Location = New Point(490, 82)
        Medicamentos_PB.Name = "Medicamentos_PB"
        Medicamentos_PB.Size = New Size(236, 108)
        Medicamentos_PB.SizeMode = PictureBoxSizeMode.StretchImage
        Medicamentos_PB.TabIndex = 3
        Medicamentos_PB.TabStop = False
        ' 
        ' btn_medicamentos
        ' 
        btn_medicamentos.Location = New Point(559, 196)
        btn_medicamentos.Name = "btn_medicamentos"
        btn_medicamentos.Size = New Size(102, 23)
        btn_medicamentos.TabIndex = 4
        btn_medicamentos.Text = "Medicamentos"
        btn_medicamentos.UseVisualStyleBackColor = True
        ' 
        ' Empleados_PB
        ' 
        Empleados_PB.Image = CType(resources.GetObject("Empleados_PB.Image"), Image)
        Empleados_PB.InitialImage = Nothing
        Empleados_PB.Location = New Point(35, 248)
        Empleados_PB.Name = "Empleados_PB"
        Empleados_PB.Size = New Size(172, 126)
        Empleados_PB.SizeMode = PictureBoxSizeMode.StretchImage
        Empleados_PB.TabIndex = 5
        Empleados_PB.TabStop = False
        ' 
        ' btn_empleados
        ' 
        btn_empleados.Location = New Point(83, 380)
        btn_empleados.Name = "btn_empleados"
        btn_empleados.Size = New Size(75, 23)
        btn_empleados.TabIndex = 6
        btn_empleados.Text = "Empleados"
        btn_empleados.UseVisualStyleBackColor = True
        ' 
        ' btn_citas
        ' 
        btn_citas.Location = New Point(636, 380)
        btn_citas.Name = "btn_citas"
        btn_citas.Size = New Size(75, 23)
        btn_citas.TabIndex = 7
        btn_citas.Text = "Citas"
        btn_citas.UseVisualStyleBackColor = True
        ' 
        ' Citas_PB
        ' 
        Citas_PB.Image = CType(resources.GetObject("Citas_PB.Image"), Image)
        Citas_PB.Location = New Point(584, 248)
        Citas_PB.Name = "Citas_PB"
        Citas_PB.Size = New Size(172, 126)
        Citas_PB.SizeMode = PictureBoxSizeMode.StretchImage
        Citas_PB.TabIndex = 8
        Citas_PB.TabStop = False
        ' 
        ' btn_compras
        ' 
        btn_compras.Location = New Point(184, 196)
        btn_compras.Name = "btn_compras"
        btn_compras.Size = New Size(75, 23)
        btn_compras.TabIndex = 10
        btn_compras.Text = "Compras"
        btn_compras.UseVisualStyleBackColor = True
        ' 
        ' btn_horarios
        ' 
        btn_horarios.Location = New Point(117, 409)
        btn_horarios.Name = "btn_horarios"
        btn_horarios.Size = New Size(108, 23)
        btn_horarios.TabIndex = 11
        btn_horarios.Text = "Horarios Medicos"
        btn_horarios.UseVisualStyleBackColor = True
        ' 
        ' btn_registrar_consulta
        ' 
        btn_registrar_consulta.Location = New Point(559, 409)
        btn_registrar_consulta.Name = "btn_registrar_consulta"
        btn_registrar_consulta.Size = New Size(120, 23)
        btn_registrar_consulta.TabIndex = 13
        btn_registrar_consulta.Text = "Registrar consulta"
        btn_registrar_consulta.UseVisualStyleBackColor = True
        ' 
        ' menu_principal
        ' 
        menu_principal.Items.AddRange(New ToolStripItem() {MiPerfilToolStripMenuItem, EmpleadosToolStripMenuItem, MedicamentosToolStripMenuItem, CitasToolStripMenuItem, VentasToolStripMenuItem, ComprasToolStripMenuItem, ReportesToolStripMenuItem})
        menu_principal.Location = New Point(0, 0)
        menu_principal.Name = "menu_principal"
        menu_principal.Size = New Size(800, 24)
        menu_principal.TabIndex = 14
        menu_principal.Text = "MenuStrip1"
        ' 
        ' MiPerfilToolStripMenuItem
        ' 
        MiPerfilToolStripMenuItem.Name = "MiPerfilToolStripMenuItem"
        MiPerfilToolStripMenuItem.Size = New Size(63, 20)
        MiPerfilToolStripMenuItem.Text = "Mi perfil"
        ' 
        ' EmpleadosToolStripMenuItem
        ' 
        EmpleadosToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {GestionDeEmpleadosToolStripMenuItem, RolesToolStripMenuItem1, HorarioDeMedicosToolStripMenuItem})
        EmpleadosToolStripMenuItem.Name = "EmpleadosToolStripMenuItem"
        EmpleadosToolStripMenuItem.Size = New Size(77, 20)
        EmpleadosToolStripMenuItem.Text = "Empleados"
        ' 
        ' GestionDeEmpleadosToolStripMenuItem
        ' 
        GestionDeEmpleadosToolStripMenuItem.Name = "GestionDeEmpleadosToolStripMenuItem"
        GestionDeEmpleadosToolStripMenuItem.Size = New Size(191, 22)
        GestionDeEmpleadosToolStripMenuItem.Text = "Gestión de empleados"
        ' 
        ' RolesToolStripMenuItem1
        ' 
        RolesToolStripMenuItem1.Name = "RolesToolStripMenuItem1"
        RolesToolStripMenuItem1.Size = New Size(191, 22)
        RolesToolStripMenuItem1.Text = "Roles"
        ' 
        ' HorarioDeMedicosToolStripMenuItem
        ' 
        HorarioDeMedicosToolStripMenuItem.Name = "HorarioDeMedicosToolStripMenuItem"
        HorarioDeMedicosToolStripMenuItem.Size = New Size(191, 22)
        HorarioDeMedicosToolStripMenuItem.Text = "Horario de medicos"
        ' 
        ' MedicamentosToolStripMenuItem
        ' 
        MedicamentosToolStripMenuItem.Name = "MedicamentosToolStripMenuItem"
        MedicamentosToolStripMenuItem.Size = New Size(98, 20)
        MedicamentosToolStripMenuItem.Text = "Medicamentos"
        ' 
        ' CitasToolStripMenuItem
        ' 
        CitasToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {GestiónDeCitasToolStripMenuItem, RegistrarConsultaToolStripMenuItem})
        CitasToolStripMenuItem.Name = "CitasToolStripMenuItem"
        CitasToolStripMenuItem.Size = New Size(45, 20)
        CitasToolStripMenuItem.Text = "Citas"
        ' 
        ' GestiónDeCitasToolStripMenuItem
        ' 
        GestiónDeCitasToolStripMenuItem.Name = "GestiónDeCitasToolStripMenuItem"
        GestiónDeCitasToolStripMenuItem.Size = New Size(168, 22)
        GestiónDeCitasToolStripMenuItem.Text = "Gestión de citas"
        ' 
        ' RegistrarConsultaToolStripMenuItem
        ' 
        RegistrarConsultaToolStripMenuItem.Name = "RegistrarConsultaToolStripMenuItem"
        RegistrarConsultaToolStripMenuItem.Size = New Size(168, 22)
        RegistrarConsultaToolStripMenuItem.Text = "Registrar consulta"
        ' 
        ' VentasToolStripMenuItem
        ' 
        VentasToolStripMenuItem.Name = "VentasToolStripMenuItem"
        VentasToolStripMenuItem.Size = New Size(53, 20)
        VentasToolStripMenuItem.Text = "Ventas"
        ' 
        ' ComprasToolStripMenuItem
        ' 
        ComprasToolStripMenuItem.Name = "ComprasToolStripMenuItem"
        ComprasToolStripMenuItem.Size = New Size(67, 20)
        ComprasToolStripMenuItem.Text = "Compras"
        ' 
        ' ReportesToolStripMenuItem
        ' 
        ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
        ReportesToolStripMenuItem.Size = New Size(65, 20)
        ReportesToolStripMenuItem.Text = "Reportes"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(324, 248)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(144, 126)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 15
        PictureBox1.TabStop = False
        ' 
        ' btn_reportes
        ' 
        btn_reportes.Location = New Point(358, 380)
        btn_reportes.Name = "btn_reportes"
        btn_reportes.Size = New Size(75, 23)
        btn_reportes.TabIndex = 16
        btn_reportes.Text = "Reportes"
        btn_reportes.UseVisualStyleBackColor = True
        ' 
        ' Menu
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btn_reportes)
        Controls.Add(PictureBox1)
        Controls.Add(btn_registrar_consulta)
        Controls.Add(btn_horarios)
        Controls.Add(btn_compras)
        Controls.Add(Citas_PB)
        Controls.Add(btn_citas)
        Controls.Add(btn_empleados)
        Controls.Add(Empleados_PB)
        Controls.Add(btn_medicamentos)
        Controls.Add(Medicamentos_PB)
        Controls.Add(Ventas_PB)
        Controls.Add(Titulo)
        Controls.Add(btn_ventas)
        Controls.Add(menu_principal)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MainMenuStrip = menu_principal
        MaximizeBox = False
        Name = "Menu"
        StartPosition = FormStartPosition.CenterParent
        CType(Ventas_PB, ComponentModel.ISupportInitialize).EndInit()
        CType(Medicamentos_PB, ComponentModel.ISupportInitialize).EndInit()
        CType(Empleados_PB, ComponentModel.ISupportInitialize).EndInit()
        CType(Citas_PB, ComponentModel.ISupportInitialize).EndInit()
        menu_principal.ResumeLayout(False)
        menu_principal.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btn_ventas As Button
    Friend WithEvents Titulo As Label
    Friend WithEvents Ventas_PB As PictureBox
    Friend WithEvents Medicamentos_PB As PictureBox
    Friend WithEvents btn_medicamentos As Button
    Friend WithEvents Empleados_PB As PictureBox
    Friend WithEvents btn_empleados As Button
    Friend WithEvents btn_citas As Button
    Friend WithEvents Citas_PB As PictureBox
    Friend WithEvents btn_compras As Button
    Friend WithEvents btn_horarios As Button
    Friend WithEvents btn_registrar_consulta As Button
    Friend WithEvents menu_principal As MenuStrip
    Friend WithEvents MiPerfilToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EmpleadosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GestionDeEmpleadosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RolesToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents HorarioDeMedicosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MedicamentosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CitasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GestiónDeCitasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RegistrarConsultaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VentasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ComprasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btn_reportes As Button
    Friend WithEvents ReportesToolStripMenuItem As ToolStripMenuItem
End Class
