Imports System.Net.Http
Imports System.Text.Json

Public Class RolesRead
    Private clienteHttp As HttpClient
    Private todosLosRoles As New List(Of RolVB)()
    Private listaFiltroActual As New List(Of RolVB)()
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/roles" ' <-- Ajusta tu IP/Puerto

    Private Async Sub RolesRead_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        dgv_roles.ReadOnly = True
        dgv_roles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_roles.AllowUserToAddRows = False

        ' Configuramos el autocompletado
        cmb_buscar.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_buscar.AutoCompleteSource = AutoCompleteSource.ListItems
        dgv_roles.RowHeadersVisible = False

        Await CargarTodosLosPermisos()
        Await CargarRoles()
    End Sub

    ' --- CARGA DE DATOS ---
    Private Async Function CargarRoles() As Task
        Try
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlBase)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                todosLosRoles = JsonSerializer.Deserialize(Of List(Of RolVB))(responseBody, opciones)

                ' Llenamos el ComboBox con los roles
                cmb_buscar.DataSource = todosLosRoles.ToList()
                cmb_buscar.DisplayMember = "Nombre"
                cmb_buscar.ValueMember = "IdRol"
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
                MessageBox.Show("No se pudieron cargar los roles." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function CargarTodosLosPermisos() As Task
        Try
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync($"{urlBase}/permisos")
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim permisos = JsonSerializer.Deserialize(Of List(Of PermisoVB))(responseBody, opciones)

                clb_permisos.DataSource = permisos
                clb_permisos.DisplayMember = "Nombre"
                clb_permisos.ValueMember = "IdPermiso"

                ' Hacemos que la lista sea de "Solo Lectura" visualmente (opcional)
                clb_permisos.SelectionMode = SelectionMode.None
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudieron cargar los permisos." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- VER DETALLES DE PERMISOS (SOLO LECTURA) ---
    Private Async Sub CargarPermisosDelRol(id As Integer, nombre As String)
        lbl_rolSeleccionado.Text = "Rol Seleccionado: " & nombre

        For i As Integer = 0 To clb_permisos.Items.Count - 1
            clb_permisos.SetItemChecked(i, False)
        Next

        Try
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/{id}/permisos")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim permisosDelRol = JsonSerializer.Deserialize(Of List(Of Integer))(responseBody)

                For i As Integer = 0 To clb_permisos.Items.Count - 1
                    Dim permisoItem As PermisoVB = CType(clb_permisos.Items(i), PermisoVB)
                    If permisosDelRol.Contains(permisoItem.IdPermiso) Then
                        clb_permisos.SetItemChecked(i, True)
                    End If
                Next
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudieron cargar los detalles del rol." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- BUSCADOR ---
    Private Sub cmb_buscar_TextChanged(sender As Object, e As EventArgs) Handles cmb_buscar.TextChanged
        AplicarFiltros()
    End Sub

    Private Sub AplicarFiltros()
        If todosLosRoles Is Nothing OrElse todosLosRoles.Count = 0 Then Return

        Dim textoBusqueda As String = cmb_buscar.Text.Trim().ToLower()

        If String.IsNullOrWhiteSpace(textoBusqueda) Then
            listaFiltroActual = todosLosRoles
        Else
            listaFiltroActual = todosLosRoles.Where(Function(r) _
                r.IdRol.ToString().Contains(textoBusqueda) OrElse
                (r.Nombre IsNot Nothing AndAlso r.Nombre.ToLower().Contains(textoBusqueda)) OrElse
                (r.Permisos IsNot Nothing AndAlso r.Permisos.ToLower().Contains(textoBusqueda))).ToList()
        End If

        dgv_roles.DataSource = Nothing
        dgv_roles.DataSource = listaFiltroActual

        If dgv_roles.Columns.Contains("IdRol") Then dgv_roles.Columns("IdRol").Visible = False

        If dgv_roles.Columns.Contains("Permisos") Then
            dgv_roles.Columns("Permisos").HeaderText = "Permisos Asignados"
            dgv_roles.Columns("Permisos").FillWeight = 200
            ' Activamos el salto de línea (Wrap) solo para esta columna
            dgv_roles.Columns("Permisos").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        End If
        ' Le decimos a toda la tabla que ajuste la altura de sus filas automáticamente
        dgv_roles.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        ' --- COLUMNAS DE ACCIONES ---
        If Not dgv_roles.Columns.Contains("colEditar") Then
            Dim btnEditar As New DataGridViewButtonColumn()
            btnEditar.Name = "colEditar"
            btnEditar.HeaderText = ""
            btnEditar.Text = "✏️"
            btnEditar.UseColumnTextForButtonValue = True
            btnEditar.FlatStyle = FlatStyle.Flat
            btnEditar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            btnEditar.Width = 35
            dgv_roles.Columns.Add(btnEditar)
        End If

        If Not dgv_roles.Columns.Contains("colEliminar") Then
            Dim btnEliminar As New DataGridViewButtonColumn()
            btnEliminar.Name = "colEliminar"
            btnEliminar.HeaderText = ""
            btnEliminar.Text = "🗑️"
            btnEliminar.UseColumnTextForButtonValue = True
            btnEliminar.FlatStyle = FlatStyle.Flat
            btnEliminar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            btnEliminar.Width = 35
            dgv_roles.Columns.Add(btnEliminar)
        End If

        If dgv_roles.Columns.Contains("colEditar") AndAlso dgv_roles.Columns.Contains("colEliminar") Then
            dgv_roles.Columns("colEditar").DisplayIndex = dgv_roles.Columns.Count - 1
            dgv_roles.Columns("colEliminar").DisplayIndex = dgv_roles.Columns.Count - 1
        End If
    End Sub

    Private Sub dgv_roles_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_roles.CellClick
        ' Si hizo clic en una fila normal (no en los botones)
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 AndAlso dgv_roles.Columns(e.ColumnIndex).Name <> "colEditar" AndAlso dgv_roles.Columns(e.ColumnIndex).Name <> "colEliminar" Then
            Dim id As Integer = Convert.ToInt32(dgv_roles.Rows(e.RowIndex).Cells("IdRol").Value)
            Dim nombre As String = dgv_roles.Rows(e.RowIndex).Cells("Nombre").Value.ToString()
            CargarPermisosDelRol(id, nombre)
        End If
    End Sub

    Private Sub cmb_buscar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_buscar.SelectedIndexChanged
        If cmb_buscar.SelectedIndex >= 0 AndAlso TypeOf cmb_buscar.SelectedItem Is RolVB Then
            Dim rolSeleccionado As RolVB = CType(cmb_buscar.SelectedItem, RolVB)
            CargarPermisosDelRol(rolSeleccionado.IdRol, rolSeleccionado.Nombre)
        End If
    End Sub

    ' --- NAVEGACIÓN A OTRAS PANTALLAS ---

    Private Async Sub btn_nuevo_rol_Click(sender As Object, e As EventArgs) Handles btn_nuevo_rol.Click
        Dim formCrear As New RolesCreate()
        formCrear.ShowDialog()
        Await CargarRoles()
    End Sub

    Private Async Sub dgv_roles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_roles.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim nombreColumna As String = dgv_roles.Columns(e.ColumnIndex).Name

        If nombreColumna = "colEditar" OrElse nombreColumna = "colEliminar" Then
            Dim idRolAccion As Integer = Convert.ToInt32(dgv_roles.Rows(e.RowIndex).Cells("IdRol").Value)

            If nombreColumna = "colEditar" Then
                ' Obtenemos el nombre del rol desde la tabla
                Dim nombreRolAccion As String = dgv_roles.Rows(e.RowIndex).Cells("Nombre").Value.ToString()
                Dim formEditar As New RolesUpdate(idRolAccion, nombreRolAccion)
                formEditar.ShowDialog()
                Await CargarRoles()
            ElseIf nombreColumna = "colEliminar" Then
                Dim formEliminar As New RolesDelete(idRolAccion)
                formEliminar.ShowDialog()
                Await CargarRoles()
            End If
        End If
    End Sub
End Class