Imports System.Net.Http
Imports System.Text.Json

Public Class EmpleadosRead
    Private clienteHttp As HttpClient
    Private todosLosEmpleados As New List(Of UsuarioVB)()
    Private listaFiltroActual As New List(Of UsuarioVB)()

    Private ReadOnly urlBase As String = "http://localhost:5133/api/empleados" ' Ajusta tu puerto

    Private Async Sub EmpleadosRead_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If SesionGlobal.Permisos IsNot Nothing Then
            btn_roles.Enabled = SesionGlobal.Permisos.Contains("CRUD de roles")
        End If
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        dgv_usuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_usuarios.AllowUserToAddRows = False
        dgv_usuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        dgv_usuarios.AllowUserToAddRows = False ' (Este ya lo tenías)
        dgv_usuarios.ReadOnly = True ' Bloquea todas las celdas para que no se pueda escribir
        dgv_usuarios.AllowUserToDeleteRows = False ' Evita que borren filas usando la tecla "Suprimir"

        cmb_buscar.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_buscar.AutoCompleteSource = AutoCompleteSource.ListItems
        dgv_usuarios.RowHeadersVisible = False

        LimpiarDetalles()
        Await CargarEmpleados()
    End Sub

    Private Async Function CargarEmpleados() As Task
        Try
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlBase)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}

                todosLosEmpleados = JsonSerializer.Deserialize(Of List(Of UsuarioVB))(responseBody, opciones)

                ' Llenamos el ComboBox
                cmb_buscar.DataSource = todosLosEmpleados.ToList()
                cmb_buscar.DisplayMember = "NombreCompleto"
                cmb_buscar.ValueMember = "IdUsuario"
                cmb_buscar.SelectedIndex = -1

                AplicarFiltros()
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudieron cargar los empleados." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub cmb_buscar_TextChanged(sender As Object, e As EventArgs) Handles cmb_buscar.TextChanged
        AplicarFiltros()
    End Sub


    Private Sub AplicarFiltros()
        If todosLosEmpleados Is Nothing OrElse todosLosEmpleados.Count = 0 Then Return

        Dim textoBusqueda As String = cmb_buscar.Text.Trim().ToLower()

        If String.IsNullOrWhiteSpace(textoBusqueda) Then
            listaFiltroActual = todosLosEmpleados.ToList()
        Else
            listaFiltroActual = todosLosEmpleados.Where(Function(u) _
                u.IdUsuario.ToString().Contains(textoBusqueda) OrElse
                (u.NombreCompleto IsNot Nothing AndAlso u.NombreCompleto.ToLower().Contains(textoBusqueda)) OrElse
                (u.Email IsNot Nothing AndAlso u.Email.ToLower().Contains(textoBusqueda))).ToList()
        End If

        dgv_usuarios.DataSource = Nothing
        dgv_usuarios.DataSource = listaFiltroActual

        ConfigurarColumnas()
    End Sub

    Private Sub ConfigurarColumnas()
        Dim columnasOcultas As String() = {"Telefono", "FechaNacimiento", "Genero", "NombreCompleto", "Email", "CedulaProfesional", "Especialidad"}
        For Each colName In columnasOcultas
            If dgv_usuarios.Columns.Contains(colName) Then
                dgv_usuarios.Columns(colName).Visible = False
            End If
        Next

        If dgv_usuarios.Columns.Contains("IdUsuario") Then dgv_usuarios.Columns("IdUsuario").HeaderText = "ID"
        If dgv_usuarios.Columns.Contains("ApellidoPaterno") Then dgv_usuarios.Columns("ApellidoPaterno").HeaderText = "Apellido Paterno"
        If dgv_usuarios.Columns.Contains("ApellidoMaterno") Then dgv_usuarios.Columns("ApellidoMaterno").HeaderText = "Apellido Materno"
        If dgv_usuarios.Columns.Contains("Estatus") Then dgv_usuarios.Columns("Estatus").HeaderText = "Estatus"

        If Not dgv_usuarios.Columns.Contains("colEditar") Then
            Dim btnEditar As New DataGridViewButtonColumn()
            btnEditar.Name = "colEditar"
            btnEditar.HeaderText = ""
            btnEditar.Text = "✏️"
            btnEditar.UseColumnTextForButtonValue = True
            btnEditar.FlatStyle = FlatStyle.Flat
            btnEditar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            btnEditar.Width = 35
            dgv_usuarios.Columns.Add(btnEditar)
        End If

        If Not dgv_usuarios.Columns.Contains("colEliminar") Then
            Dim btnEliminar As New DataGridViewButtonColumn()
            btnEliminar.Name = "colEliminar"
            btnEliminar.HeaderText = ""
            btnEliminar.Text = "🗑️"
            btnEliminar.UseColumnTextForButtonValue = True
            btnEliminar.FlatStyle = FlatStyle.Flat
            btnEliminar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            btnEliminar.Width = 35
            dgv_usuarios.Columns.Add(btnEliminar)
        End If

        If dgv_usuarios.Columns.Contains("colEditar") AndAlso dgv_usuarios.Columns.Contains("colEliminar") Then
            dgv_usuarios.Columns("colEditar").DisplayIndex = dgv_usuarios.Columns.Count - 1
            dgv_usuarios.Columns("colEliminar").DisplayIndex = dgv_usuarios.Columns.Count - 1
        End If
    End Sub

    Private Sub dgv_usuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_usuarios.CellClick
        ' Si hacen clic en una fila válida, extraemos al empleado de nuestra lista filtrada y actualizamos
        If e.RowIndex >= 0 AndAlso e.RowIndex < listaFiltroActual.Count Then
            Dim empleadoSeleccionado = listaFiltroActual(e.RowIndex)
            ActualizarEtiquetasDetalle(empleadoSeleccionado)
        End If
    End Sub

    Private Sub LimpiarDetalles()
        lbl_detalle_nombre.Text = "Nombre: "
        lbl_detalle_telefono.Text = "Teléfono: "
        lbl_detalle_fecha.Text = "Fecha Nacimiento: "
        lbl_detalle_genero.Text = "Genero: "
        lbl_detalle_email.Text = "Correo: "
        lbl_detalle_cedula.Text = "Cédula: "
        lbl_detalle_especialidad.Text = "Especialidad: "
        lbl_detalle_cedula.Visible = False
        lbl_detalle_especialidad.Visible = False
    End Sub

    Private Sub ActualizarEtiquetasDetalle(usuario As UsuarioVB)
        If usuario IsNot Nothing Then
            lbl_detalle_nombre.Text = $"Nombre: {usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}".Trim()
            lbl_detalle_telefono.Text = $"Teléfono: {usuario.Telefono}"
            lbl_detalle_fecha.Text = $"Fecha Nacimiento: {usuario.FechaNacimiento}"
            lbl_detalle_genero.Text = $"Genero: {usuario.Genero}"
            lbl_detalle_email.Text = $"Correo: {usuario.Email}"

            'VERIFICAMOS EL ROL PARA MOSTRAR U OCULTAR LOS LABELS MÉDICOS
            Dim esMedico As Boolean = usuario.Rol IsNot Nothing AndAlso (usuario.Rol.ToLower().Contains("medico") OrElse usuario.Rol.ToLower().Contains("médico"))

            If esMedico Then
                ' Si es médico, mostramos los labels y los llenamos
                lbl_detalle_cedula.Visible = True
                lbl_detalle_especialidad.Visible = True

                lbl_detalle_cedula.Text = $"Cédula: {usuario.CedulaProfesional}"
                lbl_detalle_especialidad.Text = $"Especialidad: {usuario.Especialidad}"
            Else
                ' Si es administrador o cajero, los hacemos invisibles
                lbl_detalle_cedula.Visible = False
                lbl_detalle_especialidad.Visible = False
            End If
        End If
    End Sub

    Private Async Sub dgv_usuarios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_usuarios.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim nombreColumna As String = dgv_usuarios.Columns(e.ColumnIndex).Name
        Dim row = dgv_usuarios.Rows(e.RowIndex)

        If nombreColumna = "colEditar" Then
            ' 1. Instanciamos el formulario vacío (sin nada en los paréntesis para que no marque error)
            Dim formEditar As New EmpleadosUpdate()

            ' 2. Le "inyectamos" todos los datos de la tabla directamente a sus variables
            formEditar.EmpleadoId = Convert.ToInt32(row.Cells("IdUsuario").Value)
            formEditar.NombreActual = row.Cells("Nombre").Value?.ToString()
            formEditar.ApPaternoActual = row.Cells("ApellidoPaterno").Value?.ToString()
            formEditar.ApMaternoActual = row.Cells("ApellidoMaterno").Value?.ToString()
            formEditar.EmailActual = row.Cells("Email").Value?.ToString()
            formEditar.TelefonoActual = row.Cells("Telefono").Value?.ToString()
            formEditar.FechaNacActual = row.Cells("FechaNacimiento").Value?.ToString()
            formEditar.RolActual = row.Cells("Rol").Value?.ToString()
            formEditar.GeneroActual = row.Cells("Genero").Value?.ToString()

            ' Ahora sí le pasamos el Estatus real que trajo de la BD
            If dgv_usuarios.Columns.Contains("Estatus") Then
                formEditar.EstatusActual = row.Cells("Estatus").Value?.ToString()
            End If
            ' Datos de médico (si es que existen en la tabla)
            If dgv_usuarios.Columns.Contains("CedulaProfesional") Then
                formEditar.CedulaActual = row.Cells("CedulaProfesional").Value?.ToString()
            End If
            If dgv_usuarios.Columns.Contains("Especialidad") Then
                formEditar.EspecialidadActual = row.Cells("Especialidad").Value?.ToString()
            End If

            ' 3. Abrimos la pantalla (¡Ahora sí aparecerá con todos los datos llenos!)
            formEditar.ShowDialog()

            ' 4. Al cerrar la ventana, recargamos la tabla para ver los cambios
            Await CargarEmpleados()

        ElseIf nombreColumna = "colEliminar" Then
            '  NUEVO: BLOQUEAMOS EL BOTÓN SI YA ESTÁ INACTIVO
            Dim estatusActual As String = row.Cells("Estatus").Value?.ToString()
            If estatusActual = "Inactivo" Then
                MessageBox.Show("Este empleado ya se encuentra inactivo.", "Acción Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' 1. Instanciamos la ventana de eliminar
            Dim formEliminar As New EmpleadosDelete()

            ' 2. Le pasamos el ID del empleado seleccionado
            formEliminar.EmpleadoId = Convert.ToInt32(row.Cells("IdUsuario").Value)

            ' 3. Mostramos la ventana y, si se eliminó con éxito (OK), recargamos la tabla
            If formEliminar.ShowDialog() = DialogResult.OK Then
                Await CargarEmpleados()
            End If
        End If
    End Sub

    Private Async Sub btn_nuevo_usuario_Click(sender As Object, e As EventArgs) Handles btn_nuevo_usuario.Click
        Dim formCrear As New EmpleadosCreate
        formCrear.ShowDialog
        Await CargarEmpleados
    End Sub

    ' 1. CUANDO SELECCIONAN UN NOMBRE CON EL MOUSE
    Private Sub cmb_buscar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_buscar.SelectedIndexChanged
        AplicarFiltros()

        '  Si seleccionaron a un empleado válido de la lista, actualizamos el recuadro azul
        If cmb_buscar.SelectedItem IsNot Nothing AndAlso TypeOf cmb_buscar.SelectedItem Is UsuarioVB Then
            Dim empleadoSeleccionado = CType(cmb_buscar.SelectedItem, UsuarioVB)
            ActualizarEtiquetasDetalle(empleadoSeleccionado)
        End If
    End Sub

    ' 2.  NUEVO: CUANDO PRESIONAN "ENTER" AL ESCRIBIR
    Private Sub cmb_buscar_KeyDown(sender As Object, e As KeyEventArgs) Handles cmb_buscar.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True ' Evita el molesto sonido de "beep" de Windows al dar Enter

            ' Si la tabla ya se filtró y encontró a alguien, mostramos al primer resultado
            If listaFiltroActual.Count > 0 Then
                Dim empleadoSeleccionado = listaFiltroActual(0)
                ActualizarEtiquetasDetalle(empleadoSeleccionado)

                ' Opcional: Seleccionamos visualmente la fila en la tabla de arriba
                If dgv_usuarios.Rows.Count > 0 Then
                    dgv_usuarios.ClearSelection()
                    dgv_usuarios.Rows(0).Selected = True
                End If
            Else
                LimpiarDetalles() ' Si escribieron algo que no existe, limpiamos el recuadro
            End If
        End If
    End Sub

    Private Sub btn_roles_Click(sender As Object, e As EventArgs) Handles btn_roles.Click
        Dim rolesForm As New RolesRead()
        rolesForm.ShowDialog()
    End Sub

    '  NUEVO: EVENTO QUE PINTA DE ROJO A LOS INACTIVOS
    Private Sub dgv_usuarios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv_usuarios.CellFormatting
        If e.RowIndex >= 0 AndAlso dgv_usuarios.Columns.Contains("Estatus") Then
            Dim estatus As String = dgv_usuarios.Rows(e.RowIndex).Cells("Estatus").Value?.ToString()

            If estatus = "Inactivo" Then
                ' Pintamos la fila de rojo claro (MistyRose) con texto rojo oscuro
                dgv_usuarios.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
                dgv_usuarios.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.DarkRed
            Else
                ' Pintamos la fila normal (blanco con letras negras)
                dgv_usuarios.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
                dgv_usuarios.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
            End If
        End If
    End Sub
End Class