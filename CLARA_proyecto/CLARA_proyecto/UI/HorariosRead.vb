Imports System.Net.Http
Imports System.Text.Json

Public Class HorariosRead
    Private clienteHttp As HttpClient
    Private todosLosHorarios As New List(Of HorarioReadVB)()
    Private listaFiltroActual As New List(Of HorarioReadVB)()

    Private ReadOnly urlBase As String = "http://localhost:5133/api/horarios"

    Private paginaActual As Integer = 1
    Private ReadOnly elementosPorPagina As Integer = 6
    Private totalPaginas As Integer = 1

    Private Async Sub HorariosRead_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        dgv_horarios.AllowUserToAddRows = False
        dgv_horarios.AllowUserToDeleteRows = False
        dgv_horarios.ReadOnly = True
        dgv_horarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_horarios.RowHeadersVisible = False
        dgv_horarios.BackgroundColor = Color.White

        Await CargarHorarios()
    End Sub

    ' --- CARGAR DATOS ---
    Private Async Function CargarHorarios() As Task
        Try
            Dim response = Await clienteHttp.GetAsync(urlBase)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                todosLosHorarios = JsonSerializer.Deserialize(Of List(Of HorarioReadVB))(responseBody, opciones)
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
                MessageBox.Show("No se pudo cargar el registro de horarios." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- BUSCADOR ---
    Private Sub txt_buscar_TextChanged(sender As Object, e As EventArgs) Handles txt_buscar.TextChanged
        paginaActual = 1
        AplicarFiltros()
    End Sub

    Private Sub AplicarFiltros()
        If todosLosHorarios Is Nothing Then todosLosHorarios = New List(Of HorarioReadVB)()

        Dim textoBusqueda As String = txt_buscar.Text.Trim().ToLower()

        If todosLosHorarios.Count = 0 Then
            listaFiltroActual = New List(Of HorarioReadVB)()
        Else
            '  ORDENAMIENTO APLICADO: Primero por Médico, luego por el número del Día
            listaFiltroActual = todosLosHorarios.Where(Function(h)
                                                           If String.IsNullOrWhiteSpace(textoBusqueda) Then Return True

                                                           Return h.IdHorario.ToString().Contains(textoBusqueda) OrElse
                                                                  (h.Medico IsNot Nothing AndAlso h.Medico.ToLower().Contains(textoBusqueda)) OrElse
                                                                  (h.Dia IsNot Nothing AndAlso h.Dia.ToLower().Contains(textoBusqueda))
                                                       End Function).
                                                       OrderBy(Function(h) h.Medico).
                                                       ThenBy(Function(h) ObtenerNumeroDia(h.Dia)).
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
        If dgv_horarios.Columns.Contains("IdHorario") Then
            dgv_horarios.Columns("IdHorario").Visible = False
        End If
        If dgv_horarios.Columns.Contains("Medico") Then dgv_horarios.Columns("Medico").HeaderText = "Médico"
        If dgv_horarios.Columns.Contains("Medico") Then dgv_horarios.Columns("Medico").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        If dgv_horarios.Columns.Contains("Dia") Then dgv_horarios.Columns("Dia").HeaderText = "Día"
        If dgv_horarios.Columns.Contains("Dia") Then dgv_horarios.Columns("Dia").Width = 90
        If dgv_horarios.Columns.Contains("Entrada") Then dgv_horarios.Columns("Entrada").HeaderText = "Hora Entrada"
        If dgv_horarios.Columns.Contains("Entrada") Then dgv_horarios.Columns("Entrada").Width = 100
        If dgv_horarios.Columns.Contains("Salida") Then dgv_horarios.Columns("Salida").HeaderText = "Hora Salida"
        If dgv_horarios.Columns.Contains("Salida") Then dgv_horarios.Columns("Salida").Width = 100

        '  BOTÓN EDITAR (Lápiz)
        If Not dgv_horarios.Columns.Contains("colEditar") Then
            Dim btnEditar As New DataGridViewButtonColumn() With {
                .Name = "colEditar", .HeaderText = "", .Text = "✏️",
                .UseColumnTextForButtonValue = True, .Width = 40, .FlatStyle = FlatStyle.Flat
            }
            dgv_horarios.Columns.Add(btnEditar)
        Else
            dgv_horarios.Columns("colEditar").HeaderText = ""
        End If

        '  BOTÓN ELIMINAR (Basura)
        If Not dgv_horarios.Columns.Contains("colEliminar") Then
            Dim btnEliminar As New DataGridViewButtonColumn() With {
                .Name = "colEliminar", .HeaderText = "", .Text = "🗑️",
                .UseColumnTextForButtonValue = True, .Width = 40, .FlatStyle = FlatStyle.Flat
            }
            dgv_horarios.Columns.Add(btnEliminar)
        Else
            dgv_horarios.Columns("colEliminar").HeaderText = ""
        End If

        '  ORDEN ESTRICTO
        Dim indice As Integer = 0
        If dgv_horarios.Columns.Contains("IdHorario") Then dgv_horarios.Columns("IdHorario").DisplayIndex = indice : indice += 1
        If dgv_horarios.Columns.Contains("Medico") Then dgv_horarios.Columns("Medico").DisplayIndex = indice : indice += 1
        If dgv_horarios.Columns.Contains("Dia") Then dgv_horarios.Columns("Dia").DisplayIndex = indice : indice += 1
        If dgv_horarios.Columns.Contains("Entrada") Then dgv_horarios.Columns("Entrada").DisplayIndex = indice : indice += 1
        If dgv_horarios.Columns.Contains("Salida") Then dgv_horarios.Columns("Salida").DisplayIndex = indice : indice += 1

        If dgv_horarios.Columns.Contains("colEditar") Then dgv_horarios.Columns("colEditar").DisplayIndex = indice : indice += 1
        If dgv_horarios.Columns.Contains("colEliminar") Then dgv_horarios.Columns("colEliminar").DisplayIndex = indice : indice += 1
    End Sub

    ' --- FORMATO VISUAL (Poner Día en negritas como en tu diseño) ---
    Private Sub dgv_horarios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv_horarios.CellFormatting
        If e.RowIndex < 0 Then Return
        If dgv_horarios.Columns(e.ColumnIndex).Name = "Dia" Then
            e.CellStyle.Font = New Font(dgv_horarios.Font, FontStyle.Bold)
        End If
    End Sub

    ' --- ACCIONES: EDITAR Y ELIMINAR ---
    Private Async Sub dgv_horarios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_horarios.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim nombreColumna As String = dgv_horarios.Columns(e.ColumnIndex).Name
        Dim idHorario As Integer = Convert.ToInt32(dgv_horarios.Rows(e.RowIndex).Cells("IdHorario").Value)

        If nombreColumna = "colEditar" Then
            Dim formEd As New HorariosUpdate(idHorario)
            formEd.ShowDialog()
            Await CargarHorarios()
        ElseIf nombreColumna = "colEliminar" Then
            Dim formDel As New HorariosDelete(idHorario)
            If formDel.ShowDialog() = DialogResult.OK Then Await CargarHorarios()
        End If
    End Sub

    ' --- PAGINACIÓN ---
    Private Sub MostrarPagina()
        If listaFiltroActual Is Nothing OrElse listaFiltroActual.Count = 0 Then
            dgv_horarios.DataSource = New List(Of HorarioReadVB)()
            ConfigurarColumnas()
            btn_anterior.Enabled = True
            btn_siguiente.Enabled = True
            lb_left.Visible = False : lb_middle.Visible = False : lb_right.Visible = False
            Return
        End If

        Dim listaPagina = listaFiltroActual.Skip((paginaActual - 1) * elementosPorPagina).Take(elementosPorPagina).ToList()
        dgv_horarios.DataSource = Nothing
        dgv_horarios.DataSource = listaPagina
        ConfigurarColumnas()

        btn_anterior.Enabled = True
        btn_siguiente.Enabled = True

        lb_left.Font = New Font(lb_left.Font, FontStyle.Regular)
        lb_middle.Font = New Font(lb_middle.Font, FontStyle.Regular)
        lb_right.Font = New Font(lb_right.Font, FontStyle.Regular)

        If totalPaginas = 1 Then
            lb_left.Visible = True : lb_left.Text = "1" : lb_left.Font = New Font(lb_left.Font, FontStyle.Bold)
            lb_middle.Visible = False : lb_right.Visible = False
        ElseIf totalPaginas = 2 Then
            lb_left.Visible = True : lb_left.Text = "1"
            lb_middle.Visible = True : lb_middle.Text = "2"
            lb_right.Visible = False
            If paginaActual = 1 Then lb_left.Font = New Font(lb_left.Font, FontStyle.Bold) Else lb_middle.Font = New Font(lb_middle.Font, FontStyle.Bold)
        Else
            lb_left.Visible = True : lb_middle.Visible = True : lb_right.Visible = True
            If paginaActual = 1 Then
                lb_left.Text = "1" : lb_middle.Text = "2" : lb_right.Text = "3"
                lb_left.Font = New Font(lb_left.Font, FontStyle.Bold)
            ElseIf paginaActual = totalPaginas Then
                lb_left.Text = (totalPaginas - 2).ToString() : lb_middle.Text = (totalPaginas - 1).ToString() : lb_right.Text = totalPaginas.ToString()
                lb_right.Font = New Font(lb_right.Font, FontStyle.Bold)
            Else
                lb_left.Text = (paginaActual - 1).ToString() : lb_middle.Text = paginaActual.ToString() : lb_right.Text = (paginaActual + 1).ToString()
                lb_middle.Font = New Font(lb_middle.Font, FontStyle.Bold)
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

    Private Sub btn_nuevo_horario_Click(sender As Object, e As EventArgs) Handles btn_nuevo_horario.Click
        Dim formCrear As New HorariosCreate()
        formCrear.ShowDialog()
        CargarHorarios()
    End Sub

    '  NUEVA FUNCIÓN AUXILIAR PARA ORDENAR LOS DÍAS
    Private Function ObtenerNumeroDia(dia As String) As Integer
        If String.IsNullOrWhiteSpace(dia) Then Return 8
        Select Case dia.Trim().ToLower()
            Case "domingo" : Return 1
            Case "lunes" : Return 2
            Case "martes" : Return 3
            Case "miércoles", "miercoles" : Return 4
            Case "jueves" : Return 5
            Case "viernes" : Return 6
            Case "sábado", "sabado" : Return 7
            Case Else : Return 8
        End Select
    End Function

End Class