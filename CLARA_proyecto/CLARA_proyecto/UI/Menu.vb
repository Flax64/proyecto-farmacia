Imports System.Net.Http
Imports System.Reflection.Emit
Imports System.Text.Json

Public Class Menu
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/perfil"

    Private Async Sub Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' 1. Aplicamos el diseño sin bordes para que no te corte los textos
        AplicarEstiloModernoBotones()

        ' 2. Por seguridad, BLOQUEAMOS TODOS los módulos primero
        btn_ventas.Enabled = False
        btn_compras.Enabled = False
        btn_medicamentos.Enabled = False
        btn_empleados.Enabled = False
        btn_citas.Enabled = False
        btn_registrar_consulta.Enabled = False
        btn_horarios.Enabled = False
        btn_reportes.Enabled = False
        ' Bloqueamos tambien TODOS los menu item
        MiPerfilToolStripMenuItem.Visible = False
        MedicamentosToolStripMenuItem.Visible = False
        VentasToolStripMenuItem.Visible = False
        ComprasToolStripMenuItem.Visible = False
        ReportesToolStripMenuItem.Visible = False
        GestiónDeCitasToolStripMenuItem.Visible = False
        RegistrarConsultaToolStripMenuItem.Visible = False
        CitasToolStripMenuItem.Visible = False
        GestionDeEmpleadosToolStripMenuItem.Visible = False
        MedicamentosToolStripMenuItem.Visible = False
        HorarioDeMedicosToolStripMenuItem.Visible = False
        EmpleadosToolStripMenuItem.Visible = False

        ' 3. Vamos a C# a preguntar el Rol del usuario y encendemos lo necesario
        Await CargarPermisosDelRol()
    End Sub

    ' --- LÓGICA DE CONTROL DE ACCESO POR PERMISOS DINÁMICOS ---
    Private Async Function CargarPermisosDelRol() As Task
        Try
            Dim correo As String = SesionGlobal.correo
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/rol/{correo}")

            If response.IsSuccessStatusCode Then
                Dim responseBody = Await response.Content.ReadAsStringAsync()

                Using doc = JsonDocument.Parse(responseBody)
                    Dim root = doc.RootElement

                    Dim nombreUsuario As String = root.GetProperty("nombre").GetString()
                    Titulo.Text = $"Bienvenido {nombreUsuario}"

                    ' Limpiamos la memoria y guardamos los permisos nuevos
                    SesionGlobal.Permisos.Clear()
                    For Each perm In root.GetProperty("permisos").EnumerateArray()
                        SesionGlobal.Permisos.Add(perm.GetString())
                    Next
                End Using

                ' Encendemos los botones SOLO si tienen el texto exacto en su lista de permisos
                btn_ventas.Enabled = SesionGlobal.Permisos.Contains("CRUD de ventas")
                btn_empleados.Enabled = SesionGlobal.Permisos.Contains("CRUD de empleados")
                btn_citas.Enabled = SesionGlobal.Permisos.Contains("CRUD de citas")
                btn_medicamentos.Enabled = SesionGlobal.Permisos.Contains("CRUD de medicamentos")
                btn_compras.Enabled = SesionGlobal.Permisos.Contains("CRUD de compras")
                btn_registrar_consulta.Enabled = SesionGlobal.Permisos.Contains("Registrar consulta")
                btn_horarios.Enabled = SesionGlobal.Permisos.Contains("Horario de medicos")
                btn_reportes.Enabled = SesionGlobal.Permisos.Contains("Reportes")

                ' -------------------------------------------------------------
                '  2. MAGIA PARA LA BARRA DE MENÚ (Ocultar completamente)
                ' -------------------------------------------------------------

                ' A. Opciones directas
                MiPerfilToolStripMenuItem.Visible = SesionGlobal.Permisos.Contains("Editar perfil")
                MedicamentosToolStripMenuItem.Visible = SesionGlobal.Permisos.Contains("CRUD de medicamentos")
                VentasToolStripMenuItem.Visible = SesionGlobal.Permisos.Contains("CRUD de ventas")
                ComprasToolStripMenuItem.Visible = SesionGlobal.Permisos.Contains("CRUD de compras")
                ReportesToolStripMenuItem.Visible = SesionGlobal.Permisos.Contains("Reportes")

                ' B. Menú Desplegable: CITAS
                ' Primero evaluamos a los "hijos"
                Dim tieneGestionCitas As Boolean = SesionGlobal.Permisos.Contains("CRUD de citas")
                Dim tieneRegistrarConsulta As Boolean = SesionGlobal.Permisos.Contains("Registrar consulta")

                GestiónDeCitasToolStripMenuItem.Visible = tieneGestionCitas
                RegistrarConsultaToolStripMenuItem.Visible = tieneRegistrarConsulta

                ' CORRECCIÓN: Encendemos el "padre" si tiene al menos uno de los "hijos" encendidos
                CitasToolStripMenuItem.Visible = (tieneGestionCitas OrElse tieneRegistrarConsulta)

                ' C. Menú Desplegable: EMPLEADOS
                ' Primero evaluamos a los "hijos"
                Dim tieneGestionEmpleados As Boolean = SesionGlobal.Permisos.Contains("CRUD de empleados")
                Dim tieneRoles As Boolean = SesionGlobal.Permisos.Contains("CRUD de roles")
                Dim tieneHorarios As Boolean = SesionGlobal.Permisos.Contains("Horario de medicos")

                GestionDeEmpleadosToolStripMenuItem.Visible = tieneGestionEmpleados
                MedicamentosToolStripMenuItem.Visible = tieneRoles
                HorarioDeMedicosToolStripMenuItem.Visible = tieneHorarios

                ' CORRECCIÓN: Encendemos el "padre" si tiene al menos uno de los "hijos" encendidos
                EmpleadosToolStripMenuItem.Visible = (tieneGestionEmpleados OrElse tieneRoles OrElse tieneHorarios)
            Else
                MessageBox.Show("No se pudieron cargar los permisos de seguridad.", "Error de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor para validar los permisos.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- LÓGICA PARA NAVEGAR A LAS PANTALLAS ---
    Private Sub btn_ventas_Click(sender As Object, e As EventArgs) Handles btn_ventas.Click
        Dim CRUDVentas As New VentasRead()
        CRUDVentas.ShowDialog()
    End Sub

    Private Sub btn_medicamentos_Click(sender As Object, e As EventArgs) Handles btn_medicamentos.Click
        Dim CRUDMedicamentos As New MedicamentosRead()
        CRUDMedicamentos.ShowDialog()
    End Sub

    Private Sub btn_empleados_Click(sender As Object, e As EventArgs) Handles btn_empleados.Click
        Dim roles As New EmpleadosRead
        roles.ShowDialog()
    End Sub

    Private Sub btn_compras_Click(sender As Object, e As EventArgs) Handles btn_compras.Click
        Dim compras As New ComprasRead()
        compras.ShowDialog()
    End Sub

    Private Sub btn_citas_Click(sender As Object, e As EventArgs) Handles btn_citas.Click
        Dim citas As New CitasRead()
        citas.ShowDialog()
    End Sub

    Private Sub btn_registrar_consulta_Click(sender As Object, e As EventArgs) Handles btn_registrar_consulta.Click
        Dim consultas As New RegistrarConsulta()
        consultas.ShowDialog()
    End Sub

    Private Sub btn_horarios_Click(sender As Object, e As EventArgs) Handles btn_horarios.Click
        Dim horarios As New HorariosRead()
        horarios.ShowDialog()
    End Sub

    Private Sub btn_reportes_Click(sender As Object, e As EventArgs) Handles btn_reportes.Click
        Dim reportes As New Reportes()
        reportes.ShowDialog()
    End Sub

    ' =====================================================================
    ' CORRECCIÓN DEL TAMAÑO DE TEXTO Y EFECTO HOVER
    ' =====================================================================
    Private Sub AplicarEstiloModernoBotones()
        Dim listaBotones As Button() = {btn_ventas, btn_compras, btn_medicamentos,
            btn_empleados, btn_citas, btn_horarios, btn_registrar_consulta, btn_reportes}

        For Each btn In listaBotones
            btn.FlatStyle = FlatStyle.Flat
            ' Le quitamos el borde a todos para que no empuje tu texto hacia adentro
            btn.FlatAppearance.BorderSize = 0

            ' Color blanco base y le damos un toque en "Negrita" para que destaquen más
            btn.BackColor = Color.White
            btn.Cursor = Cursors.Hand
            btn.Font = New Font(btn.Font, FontStyle.Bold)

            ' Agregamos la animación
            AddHandler btn.MouseEnter, AddressOf EfectoBoton_EntraMouse
            AddHandler btn.MouseLeave, AddressOf EfectoBoton_SaleMouse
        Next
    End Sub

    Private Sub EfectoBoton_EntraMouse(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        If btn.Enabled Then
            ' Un azul muy moderno y suave al pasar el mouse
            btn.BackColor = Color.LightSkyBlue
        End If
    End Sub

    Private Sub EfectoBoton_SaleMouse(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        If btn.Enabled Then
            ' Regresa a blanco
            btn.BackColor = Color.White
        End If
    End Sub

    Private Sub MiPerfilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MiPerfilToolStripMenuItem.Click
        Dim editarPerfil As New EditarPerfil
        editarPerfil.ShowDialog()
    End Sub

    Private Sub GestionDeEmpleadosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GestionDeEmpleadosToolStripMenuItem.Click
        Dim empleados As New EmpleadosRead()
        empleados.ShowDialog()
    End Sub

    Private Sub RolesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RolesToolStripMenuItem1.Click
        Dim roles As New RolesRead()
        roles.ShowDialog()
    End Sub

    Private Sub HorarioDeMedicosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HorarioDeMedicosToolStripMenuItem.Click
        Dim horarios As New HorariosRead()
        horarios.ShowDialog()
    End Sub

    Private Sub MedicamentosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MedicamentosToolStripMenuItem.Click
        Dim medicamentos As New MedicamentosRead()
        medicamentos.ShowDialog()
    End Sub

    Private Sub GestiónDeCitasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GestiónDeCitasToolStripMenuItem.Click
        Dim citas As New CitasRead()
        citas.ShowDialog()
    End Sub

    Private Sub RegistrarConsultaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistrarConsultaToolStripMenuItem.Click
        Dim consultas As New RegistrarConsulta()
        consultas.ShowDialog()
    End Sub

    Private Sub VentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasToolStripMenuItem.Click
        Dim ventas As New VentasRead()
        ventas.ShowDialog()
    End Sub

    Private Sub ComprasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ComprasToolStripMenuItem.Click
        Dim compras As New ComprasRead()
        compras.ShowDialog()
    End Sub

    Private Sub ReportesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportesToolStripMenuItem.Click
        Dim reportes As New Reportes()
        reportes.ShowDialog()
    End Sub
End Class