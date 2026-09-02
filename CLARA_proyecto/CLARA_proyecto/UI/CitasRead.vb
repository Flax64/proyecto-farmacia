Imports System.Net.Http
Imports System.Text.Json

Public Class CitasRead
    Private clienteHttp As HttpClient
    Private todasLasCitas As New List(Of CitaReadVB)()
    Private listaFiltroActual As New List(Of CitaReadVB)()

    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/citas"

    Private paginaActual As Integer = 1
    Private ReadOnly elementosPorPagina As Integer = 6
    Private totalPaginas As Integer = 1

    Private Async Sub CitasRead_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        dtp_filtro_fecha.ShowCheckBox = True
        dtp_filtro_fecha.Checked = False
        dgv_citas.AllowUserToAddRows = False
        dgv_citas.AllowUserToDeleteRows = False
        dgv_citas.ReadOnly = True
        dgv_citas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_citas.RowHeadersVisible = False
        dgv_citas.BackgroundColor = Color.White

        Await CargarCitas()
    End Sub

    ' --- CARGAR DATOS ---
    Private Async Function CargarCitas() As Task
        Try
            '  NUEVO: 1. Llamamos al detector inteligente de alertas
            Try
                Dim resAlertas = Await clienteHttp.GetAsync($"{urlBase}/validar-huerfanas")
                If resAlertas.IsSuccessStatusCode Then
                    Dim jsonAlertas = Await resAlertas.Content.ReadAsStringAsync()
                    Dim doc = JsonDocument.Parse(jsonAlertas)
                    For Each alerta In doc.RootElement.GetProperty("alertas").EnumerateArray()
                        MessageBox.Show(alerta.GetString(), "Atención Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Next
                End If
            Catch ex As Exception
                ' Ignoramos errores silenciosos del detector para que la tabla cargue de todas formas
            End Try

            ' 2. Le enviamos el correo de la sesión actual a la API
            Dim correo As String = SesionGlobal.correo
            Dim urlConCorreo As String = $"{urlBase}?correo={correo}"

            Dim response = Await clienteHttp.GetAsync(urlConCorreo)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                todasLasCitas = JsonSerializer.Deserialize(Of List(Of CitaReadVB))(responseBody, opciones)
                paginaActual = 1
                AplicarFiltros()
            Else
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudo cargar el registro de citas." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- BUSCADOR, FILTRO Y ORDENAMIENTO (MÁS CERCANA PRIMERO) ---
    Private Sub AplicarFiltros()
        If todasLasCitas Is Nothing Then todasLasCitas = New List(Of CitaReadVB)()

        Dim textoBusqueda As String = txt_buscar.Text.Trim().ToLower()
        Dim fechaSeleccionada As String = dtp_filtro_fecha.Value.ToString("dd/MM/yyyy")
        Dim filtrarPorFecha As Boolean = dtp_filtro_fecha.Checked

        If todasLasCitas.Count = 0 Then
            listaFiltroActual = New List(Of CitaReadVB)()
        Else
            listaFiltroActual = todasLasCitas.Where(Function(c)
                                                        Dim coincideFecha = Not filtrarPorFecha OrElse c.Fecha = fechaSeleccionada
                                                        Dim coincideTexto As Boolean

                                                        If String.IsNullOrWhiteSpace(textoBusqueda) Then
                                                            coincideTexto = True
                                                        Else
                                                            Dim contieneTexto = c.IdCita.ToString().Contains(textoBusqueda) OrElse
                                                                                (c.Paciente IsNot Nothing AndAlso c.Paciente.ToLower().Contains(textoBusqueda)) OrElse
                                                                                (c.Medico IsNot Nothing AndAlso c.Medico.ToLower().Contains(textoBusqueda))

                                                            Dim estadoValido = (c.Estado = "Confirmada" OrElse c.Estado = "Pendiente")
                                                            coincideTexto = contieneTexto AndAlso estadoValido
                                                        End If

                                                        Return coincideTexto AndAlso coincideFecha
                                                    End Function).
            OrderBy(Function(c) DateTime.ParseExact(c.Fecha, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture)).
            ThenBy(Function(c) DateTime.ParseExact(c.Hora, "hh:mm tt", Globalization.CultureInfo.InvariantCulture)).
            ThenBy(Function(c) c.IdCita).
            ToList()
        End If

        If listaFiltroActual.Count = 0 Then
            totalPaginas = 1
        Else
            totalPaginas = Math.Ceiling(listaFiltroActual.Count / elementosPorPagina)
        End If

        MostrarPagina()
    End Sub

    ' --- CONFIGURACIÓN DE TABLA ---
    Private Sub ConfigurarColumnas()
        If dgv_citas.Columns.Contains("IdCita") Then
            dgv_citas.Columns("IdCita").HeaderText = "Id"
            dgv_citas.Columns("IdCita").Width = 40
        End If
        If dgv_citas.Columns.Contains("Paciente") Then dgv_citas.Columns("Paciente").HeaderText = "Paciente"
        If dgv_citas.Columns.Contains("Medico") Then dgv_citas.Columns("Medico").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        If dgv_citas.Columns.Contains("Fecha") Then dgv_citas.Columns("Fecha").Width = 90
        If dgv_citas.Columns.Contains("Hora") Then dgv_citas.Columns("Hora").Width = 80
        If dgv_citas.Columns.Contains("Estado") Then dgv_citas.Columns("Estado").Width = 100

        If Not dgv_citas.Columns.Contains("colConfirmar") Then
            Dim btnConfirmar As New DataGridViewButtonColumn() With {
                .Name = "colConfirmar", .HeaderText = "", .Text = "✔️",
                .UseColumnTextForButtonValue = True, .Width = 40, .FlatStyle = FlatStyle.Flat
            }
            dgv_citas.Columns.Add(btnConfirmar)
        Else
            dgv_citas.Columns("colConfirmar").HeaderText = ""
        End If

        If Not dgv_citas.Columns.Contains("colEditar") Then
            Dim btnEditar As New DataGridViewButtonColumn() With {
                .Name = "colEditar", .HeaderText = "", .Text = "✏️",
                .UseColumnTextForButtonValue = True, .Width = 40, .FlatStyle = FlatStyle.Flat
            }
            dgv_citas.Columns.Add(btnEditar)
        Else
            dgv_citas.Columns("colEditar").HeaderText = ""
        End If

        If Not dgv_citas.Columns.Contains("colEliminar") Then
            Dim btnEliminar As New DataGridViewButtonColumn() With {
                .Name = "colEliminar", .HeaderText = "", .Text = "🗑️",
                .UseColumnTextForButtonValue = True, .Width = 40, .FlatStyle = FlatStyle.Flat
            }
            dgv_citas.Columns.Add(btnEliminar)
        Else
            dgv_citas.Columns("colEliminar").HeaderText = ""
        End If

        Dim indice As Integer = 0
        If dgv_citas.Columns.Contains("IdCita") Then dgv_citas.Columns("IdCita").DisplayIndex = indice : indice += 1
        If dgv_citas.Columns.Contains("Paciente") Then dgv_citas.Columns("Paciente").DisplayIndex = indice : indice += 1
        If dgv_citas.Columns.Contains("Medico") Then dgv_citas.Columns("Medico").DisplayIndex = indice : indice += 1
        If dgv_citas.Columns.Contains("Fecha") Then dgv_citas.Columns("Fecha").DisplayIndex = indice : indice += 1
        If dgv_citas.Columns.Contains("Hora") Then dgv_citas.Columns("Hora").DisplayIndex = indice : indice += 1
        If dgv_citas.Columns.Contains("Estado") Then dgv_citas.Columns("Estado").DisplayIndex = indice : indice += 1

        If dgv_citas.Columns.Contains("colConfirmar") Then dgv_citas.Columns("colConfirmar").DisplayIndex = indice : indice += 1
        If dgv_citas.Columns.Contains("colEditar") Then dgv_citas.Columns("colEditar").DisplayIndex = indice : indice += 1
        If dgv_citas.Columns.Contains("colEliminar") Then dgv_citas.Columns("colEliminar").DisplayIndex = indice : indice += 1
    End Sub

    ' --- FORMATO DINÁMICO ---
    Private Sub dgv_citas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv_citas.CellFormatting
        If e.RowIndex < 0 Then Return

        Dim estadoFila As String = dgv_citas.Rows(e.RowIndex).Cells("Estado").Value?.ToString()

        '  AGREGAMOS COLOR VERDE PARA "COMPLETADA"
        If estadoFila = "Cancelada" Then
            dgv_citas.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Red
            dgv_citas.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
        ElseIf estadoFila = "Completada" Then
            dgv_citas.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.MediumSeaGreen
            dgv_citas.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
        Else
            dgv_citas.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
            dgv_citas.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
        End If

        If dgv_citas.Columns(e.ColumnIndex).Name = "Estado" Then
            e.CellStyle.Font = New Font(dgv_citas.Font, FontStyle.Bold)
            If estadoFila = "Confirmada" Then
                e.CellStyle.BackColor = Color.LightGreen : e.CellStyle.ForeColor = Color.DarkGreen
            ElseIf estadoFila = "Pendiente" Then
                e.CellStyle.BackColor = Color.LightYellow : e.CellStyle.ForeColor = Color.DarkOrange
            End If
        End If
    End Sub

    ' --- ACCIONES ---
    Private Async Sub dgv_citas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_citas.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim nombreColumna As String = dgv_citas.Columns(e.ColumnIndex).Name
        Dim row = dgv_citas.Rows(e.RowIndex)
        Dim estadoActual = row.Cells("Estado").Value?.ToString()
        Dim idCita As Integer = Convert.ToInt32(row.Cells("IdCita").Value)

        If nombreColumna = "colConfirmar" Then
            If estadoActual = "Confirmada" Then
                MessageBox.Show("Esta cita ya se encuentra confirmada. No es posible confirmarla de nuevo.", "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            ElseIf estadoActual = "Cancelada" Then
                MessageBox.Show("Esta cita se encuentra cancelada. No es posible confirmarla.", "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            ElseIf estadoActual = "Completada" Then
                '  BLOQUEO SI ESTÁ COMPLETADA
                MessageBox.Show("Esta cita ya fue completada y cuenta con historial de consulta. No es posible confirmarla de nuevo.", "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim formConf As New CitasConfirmar(idCita)
            If formConf.ShowDialog() = DialogResult.OK Then Await CargarCitas()
            Return
        End If

        If nombreColumna = "colEditar" OrElse nombreColumna = "colEliminar" Then
            If estadoActual = "Cancelada" Then
                MessageBox.Show("Esta cita ya se encuentra cancelada. No es posible editarla ni cancelarla de nuevo.", "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If estadoActual = "Confirmada" Then
                MessageBox.Show("Esta cita ya se encuentra confirmada. No es posible editarla ni cancelarla.", "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            '  BLOQUEO SI ESTÁ COMPLETADA
            If estadoActual = "Completada" Then
                MessageBox.Show("Esta cita ya fue completada y cuenta con historial de consulta. No es posible editarla ni cancelarla.", "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If nombreColumna = "colEditar" Then
                Dim formEd As New CitasUpdate(idCita)
                formEd.ShowDialog()
                Await CargarCitas()
            ElseIf nombreColumna = "colEliminar" Then
                Dim formDel As New CitasDelete(idCita)
                If formDel.ShowDialog() = DialogResult.OK Then
                    Await CargarCitas()
                End If
            End If
        End If
    End Sub

    Private Sub txt_buscar_TextChanged(sender As Object, e As EventArgs) Handles txt_buscar.TextChanged
        paginaActual = 1 : AplicarFiltros()
    End Sub

    Private Sub dtp_filtro_fecha_ValueChanged(sender As Object, e As EventArgs) Handles dtp_filtro_fecha.ValueChanged
        paginaActual = 1 : AplicarFiltros()
    End Sub

    ' --- PAGINACIÓN ---
    Private Sub MostrarPagina()
        If listaFiltroActual Is Nothing OrElse listaFiltroActual.Count = 0 Then
            dgv_citas.DataSource = New List(Of CitaReadVB)()
            ConfigurarColumnas()
            btn_anterior.Enabled = True : btn_siguiente.Enabled = True
            lb_left.Visible = False : lb_middle.Visible = False : lb_right.Visible = False
            Return
        End If

        Dim listaPagina = listaFiltroActual.Skip((paginaActual - 1) * elementosPorPagina).Take(elementosPorPagina).ToList()
        dgv_citas.DataSource = Nothing
        dgv_citas.DataSource = listaPagina
        ConfigurarColumnas()

        btn_anterior.Enabled = True : btn_siguiente.Enabled = True
        lb_left.Font = New Font(lb_left.Font, FontStyle.Regular)
        lb_middle.Font = New Font(lb_middle.Font, FontStyle.Regular)
        lb_right.Font = New Font(lb_right.Font, FontStyle.Regular)

        If totalPaginas = 1 Then
            lb_left.Visible = True : lb_left.Text = "1" : lb_left.Font = New Font(lb_left.Font, FontStyle.Bold)
            lb_middle.Visible = False : lb_right.Visible = False
        ElseIf totalPaginas = 2 Then
            lb_left.Visible = True : lb_left.Text = "1"
            lb_middle.Visible = True : lb_middle.Text = "2" : lb_right.Visible = False
            If paginaActual = 1 Then lb_left.Font = New Font(lb_left.Font, FontStyle.Bold) Else lb_middle.Font = New Font(lb_middle.Font, FontStyle.Bold)
        Else
            lb_left.Visible = True : lb_middle.Visible = True : lb_right.Visible = True
            If paginaActual = 1 Then
                lb_left.Text = "1" : lb_middle.Text = "2" : lb_right.Text = "3" : lb_left.Font = New Font(lb_left.Font, FontStyle.Bold)
            ElseIf paginaActual = totalPaginas Then
                lb_left.Text = (totalPaginas - 2).ToString() : lb_middle.Text = (totalPaginas - 1).ToString() : lb_right.Text = totalPaginas.ToString() : lb_right.Font = New Font(lb_right.Font, FontStyle.Bold)
            Else
                lb_left.Text = (paginaActual - 1).ToString() : lb_middle.Text = paginaActual.ToString() : lb_right.Text = (paginaActual + 1).ToString() : lb_middle.Font = New Font(lb_middle.Font, FontStyle.Bold)
            End If
        End If
    End Sub

    Private Sub btn_anterior_Click(sender As Object, e As EventArgs) Handles btn_anterior.Click
        If paginaActual > 1 Then paginaActual -= 1 : MostrarPagina()
    End Sub

    Private Sub btn_siguiente_Click(sender As Object, e As EventArgs) Handles btn_siguiente.Click
        If paginaActual < totalPaginas Then paginaActual += 1 : MostrarPagina()
    End Sub

    Private Sub NumerosPaginacion_Click(sender As Object, e As EventArgs) Handles lb_left.Click, lb_middle.Click, lb_right.Click
        Dim lbl = CType(sender, Label)
        Dim pag As Integer
        If Integer.TryParse(lbl.Text, pag) Then paginaActual = pag : MostrarPagina()
    End Sub

    Private Sub btn_nueva_cita_Click(sender As Object, e As EventArgs) Handles btn_nueva_cita.Click
        Dim formCrear As New CitasCreate()
        formCrear.ShowDialog()
        CargarCitas()
    End Sub
End Class