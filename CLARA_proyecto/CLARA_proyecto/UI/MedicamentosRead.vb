Imports System.Net.Http
Imports System.Text.Json

Public Class MedicamentosRead
    Private clienteHttp As HttpClient
    Private todosLosMedicamentos As New List(Of MedicamentoReadVB)()
    Private listaFiltroActual As New List(Of MedicamentoReadVB)()

    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/medicamentos" ' <-- Ajusta tu puerto

    '  VARIABLES DE PAGINACIÓN
    Private paginaActual As Integer = 1
    Private ReadOnly elementosPorPagina As Integer = 6 ' <-- Cambia esto si quieres ver más filas a la vez
    Private totalPaginas As Integer = 1

    Private Async Sub MedicamentosRead_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' Diseño de la tabla
        dgv_medicamentos.AllowUserToAddRows = False
        dgv_medicamentos.AllowUserToDeleteRows = False
        dgv_medicamentos.ReadOnly = True
        dgv_medicamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_medicamentos.RowHeadersVisible = False
        dgv_medicamentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_medicamentos.BackgroundColor = Color.White

        Await CargarMedicamentos()
    End Sub

    ' --- CARGAR DATOS DESDE LA API ---
    Private Async Function CargarMedicamentos() As Task
        Try
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlBase)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                todosLosMedicamentos = JsonSerializer.Deserialize(Of List(Of MedicamentoReadVB))(responseBody, opciones)

                ' Forzamos a que inicie en la página 1 siempre que se recargan los datos
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
                MessageBox.Show("No se pudo cargar el inventario." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- BUSCADOR EN TIEMPO REAL ---
    Private Sub txt_buscar_TextChanged(sender As Object, e As EventArgs) Handles txt_buscar.TextChanged
        paginaActual = 1 ' Si buscan algo nuevo, los regresamos a la primera página de resultados
        AplicarFiltros()
    End Sub

    Private Sub AplicarFiltros()
        If todosLosMedicamentos Is Nothing OrElse todosLosMedicamentos.Count = 0 Then Return

        Dim textoBusqueda As String = txt_buscar.Text.Trim().ToLower()

        If String.IsNullOrWhiteSpace(textoBusqueda) Then
            listaFiltroActual = todosLosMedicamentos.ToList()
        Else
            listaFiltroActual = todosLosMedicamentos.Where(Function(m) _
                m.IdMedicamento.ToString().Contains(textoBusqueda) OrElse
                (m.NombreCompleto IsNot Nothing AndAlso m.NombreCompleto.ToLower().Contains(textoBusqueda)) OrElse
                (m.Descripcion IsNot Nothing AndAlso m.Descripcion.ToLower().Contains(textoBusqueda))).ToList()
        End If

        ' Calculamos cuántas páginas se necesitan en total
        If listaFiltroActual.Count = 0 Then
            totalPaginas = 1
        Else
            totalPaginas = Math.Ceiling(listaFiltroActual.Count / elementosPorPagina)
        End If

        MostrarPagina()
    End Sub

    '  MOSTRAR PÁGINA Y NÚMEROS DINÁMICOS
    Private Sub MostrarPagina()
        If listaFiltroActual Is Nothing OrElse listaFiltroActual.Count = 0 Then
            dgv_medicamentos.DataSource = Nothing
            btn_anterior.Enabled = True
            btn_siguiente.Enabled = True
            lb_left.Visible = False
            lb_middle.Visible = False
            lb_right.Visible = False
            Return
        End If

        ' Carga de datos de la página
        Dim listaPagina = listaFiltroActual.Skip((paginaActual - 1) * elementosPorPagina).Take(elementosPorPagina).ToList()
        dgv_medicamentos.DataSource = Nothing
        dgv_medicamentos.DataSource = listaPagina
        ConfigurarColumnas()

        btn_anterior.Enabled = True
        btn_siguiente.Enabled = True

        '  Limpiamos las negritas de todos los labels primero
        lb_left.Font = New Font(lb_left.Font, FontStyle.Regular)
        lb_middle.Font = New Font(lb_middle.Font, FontStyle.Regular)
        lb_right.Font = New Font(lb_right.Font, FontStyle.Regular)

        '  Lógica de visualización de números
        If totalPaginas = 1 Then
            lb_left.Visible = True
            lb_left.Text = "1"
            lb_left.Font = New Font(lb_left.Font, FontStyle.Bold) ' Negrita

            lb_middle.Visible = False
            lb_right.Visible = False

        ElseIf totalPaginas = 2 Then
            lb_left.Visible = True
            lb_left.Text = "1"
            lb_middle.Visible = True
            lb_middle.Text = "2"
            lb_right.Visible = False

            If paginaActual = 1 Then
                lb_left.Font = New Font(lb_left.Font, FontStyle.Bold)
            Else
                lb_middle.Font = New Font(lb_middle.Font, FontStyle.Bold)
            End If

        Else ' 3 páginas o más
            lb_left.Visible = True
            lb_middle.Visible = True
            lb_right.Visible = True

            If paginaActual = 1 Then
                lb_left.Text = "1"
                lb_middle.Text = "2"
                lb_right.Text = "3"
                lb_left.Font = New Font(lb_left.Font, FontStyle.Bold)
            ElseIf paginaActual = totalPaginas Then
                lb_left.Text = (totalPaginas - 2).ToString()
                lb_middle.Text = (totalPaginas - 1).ToString()
                lb_right.Text = totalPaginas.ToString()
                lb_right.Font = New Font(lb_right.Font, FontStyle.Bold)
            Else
                lb_left.Text = (paginaActual - 1).ToString()
                lb_middle.Text = paginaActual.ToString()
                lb_right.Text = (paginaActual + 1).ToString()
                lb_middle.Font = New Font(lb_middle.Font, FontStyle.Bold)
            End If
        End If
    End Sub

    '  EVENTOS DE LOS BOTONES DE PAGINACIÓN
    Private Sub btn_anterior_Click(sender As Object, e As EventArgs) Handles btn_anterior.Click
        If paginaActual > 1 Then
            paginaActual -= 1
            MostrarPagina()
        End If
    End Sub

    Private Sub btn_siguiente_Click(sender As Object, e As EventArgs) Handles btn_siguiente.Click
        If paginaActual < totalPaginas Then
            paginaActual += 1
            MostrarPagina()
        End If
    End Sub

    ' --- DISEÑO EXACTO DE LA TABLA ---
    Private Sub ConfigurarColumnas()
        If dgv_medicamentos.Columns.Count = 0 Then Return ' Evitar pánico si no hay columnas

        ' 1. Ocultamos las técnicas
        Dim columnasOcultas As String() = {"IdEstatus", "Nombre", "ConcentracionValor", "ConcentracionUnidad"}
        For Each colName In columnasOcultas
            If dgv_medicamentos.Columns.Contains(colName) Then dgv_medicamentos.Columns(colName).Visible = False
        Next

        ' 2. Medidas manuales seguras
        Dim colId As DataGridViewColumn = dgv_medicamentos.Columns("IdMedicamento")
        If colId IsNot Nothing Then
            colId.HeaderText = "ID"
            colId.Width = 40
        End If

        Dim colEstatus As DataGridViewColumn = dgv_medicamentos.Columns("Estatus")
        If colEstatus IsNot Nothing Then
            colEstatus.HeaderText = "Estatus"
            colEstatus.Width = 70
        End If

        Dim colNombre As DataGridViewColumn = dgv_medicamentos.Columns("NombreCompleto")
        If colNombre IsNot Nothing Then
            colNombre.HeaderText = "Nombre del Medicamento"
            colNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        Dim colDesc As DataGridViewColumn = dgv_medicamentos.Columns("Descripcion")
        If colDesc IsNot Nothing Then
            colDesc.HeaderText = "Descripción"
            colDesc.Width = 140
        End If

        Dim colPrecio As DataGridViewColumn = dgv_medicamentos.Columns("Precio")
        If colPrecio IsNot Nothing Then
            colPrecio.HeaderText = "Precio Unit."
            colPrecio.Width = 80
            colPrecio.DefaultCellStyle.Format = "C2"
        End If

        Dim colStock As DataGridViewColumn = dgv_medicamentos.Columns("Stock")
        If colStock IsNot Nothing Then
            colStock.HeaderText = "Stock Actual"
            colStock.Width = 70
            colStock.DefaultCellStyle.Format = "0 u."
            colStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        '  3. CREACIÓN Y CONFIGURACIÓN DE LOS BOTONES
        If Not dgv_medicamentos.Columns.Contains("colEditar") Then
            Dim btnEditar As New DataGridViewButtonColumn() With {
                .Name = "colEditar", .HeaderText = "", .Text = "✏️",
                .UseColumnTextForButtonValue = True, .Width = 40, .FlatStyle = FlatStyle.Flat,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            }
            dgv_medicamentos.Columns.Add(btnEditar)
        Else
            dgv_medicamentos.Columns("colEditar").Width = 40
            dgv_medicamentos.Columns("colEditar").HeaderText = ""
        End If

        If Not dgv_medicamentos.Columns.Contains("colEliminar") Then
            Dim btnEliminar As New DataGridViewButtonColumn() With {
                .Name = "colEliminar", .HeaderText = "", .Text = "🗑️",
                .UseColumnTextForButtonValue = True, .Width = 40, .FlatStyle = FlatStyle.Flat,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            }
            dgv_medicamentos.Columns.Add(btnEliminar)
        Else
            dgv_medicamentos.Columns("colEliminar").Width = 40
        End If

        '  4. ORDENAR BOTONES AL FINAL SIEMPRE
        If dgv_medicamentos.Columns.Contains("colEditar") AndAlso dgv_medicamentos.Columns.Contains("colEliminar") Then
            dgv_medicamentos.Columns("colEditar").DisplayIndex = dgv_medicamentos.Columns.Count - 1
            dgv_medicamentos.Columns("colEliminar").DisplayIndex = dgv_medicamentos.Columns.Count - 1
        End If
    End Sub

    ' LÓGICA DE COLORES: INACTIVOS (Fila Roja), STOCK 0 (Celda Roja), STOCK 1-5 (Celda Amarilla)
    Private Sub dgv_medicamentos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv_medicamentos.CellFormatting
        If e.RowIndex >= 0 Then
            Dim estatus As String = dgv_medicamentos.Rows(e.RowIndex).Cells("Estatus").Value?.ToString()
            Dim stockActual As Integer = 0
            Integer.TryParse(dgv_medicamentos.Rows(e.RowIndex).Cells("Stock").Value?.ToString(), stockActual)

            If estatus = "Inactivo" Then
                ' 🔴 Toda la fila roja si está inactivo
                dgv_medicamentos.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
                dgv_medicamentos.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.DarkRed
            Else
                ' ⚪ Restauramos el color normal de la fila por defecto
                dgv_medicamentos.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
                dgv_medicamentos.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black

                ' Evaluamos los colores ÚNICAMENTE para la celda de la columna "Stock"
                If dgv_medicamentos.Columns(e.ColumnIndex).Name = "Stock" Then

                    If stockActual = 0 Then
                        ' 🔴 AGOTADO: Fondo rojo claro, letras blancas y en negrita
                        e.CellStyle.BackColor = Color.LightCoral
                        e.CellStyle.ForeColor = Color.White
                        e.CellStyle.Font = New Font(dgv_medicamentos.Font, FontStyle.Bold)

                    ElseIf stockActual >= 1 AndAlso stockActual <= 5 Then
                        ' 🟡 STOCK BAJO: Fondo amarillo, letras naranjas y en negrita
                        e.CellStyle.BackColor = Color.LightGoldenrodYellow
                        e.CellStyle.ForeColor = Color.DarkOrange
                        e.CellStyle.Font = New Font(dgv_medicamentos.Font, FontStyle.Bold)
                    End If

                End If
            End If
        End If
    End Sub

    ' --- ACCIONES AL HACER CLIC EN LOS BOTONES ---
    Private Async Sub dgv_medicamentos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_medicamentos.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim nombreColumna As String = dgv_medicamentos.Columns(e.ColumnIndex).Name
        Dim row = dgv_medicamentos.Rows(e.RowIndex)

        If nombreColumna = "colEditar" OrElse nombreColumna = "colEliminar" Then
            ' SEGURIDAD: Bloqueamos acciones si el medicamento está inactivo
            Dim estatusActual As String = row.Cells("Estatus").Value?.ToString()
            If estatusActual = "Inactivo" Then
                MessageBox.Show("Este medicamento se encuentra Inactivo y no puede ser modificado ni eliminado.", "Acción Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim idMed As Integer = Convert.ToInt32(row.Cells("IdMedicamento").Value)

            If nombreColumna = "colEditar" Then
                Dim formEditar As New MedicamentosUpdate()
                formEditar.MedicamentoId = idMed
                formEditar.NombrePuro = row.Cells("Nombre").Value?.ToString()
                formEditar.DescripcionPura = row.Cells("Descripcion").Value?.ToString()
                formEditar.PrecioPuro = Convert.ToDecimal(row.Cells("Precio").Value)
                formEditar.ValorConcPuro = Convert.ToDecimal(row.Cells("ConcentracionValor").Value)
                formEditar.UnidadConcPura = row.Cells("ConcentracionUnidad").Value?.ToString()

                formEditar.ShowDialog()
                Await CargarMedicamentos()

            ElseIf nombreColumna = "colEliminar" Then
                Dim formEliminar As New MedicamentosDelete(idMed)
                If formEliminar.ShowDialog() = DialogResult.OK Then
                    Await CargarMedicamentos()
                End If
            End If
        End If
    End Sub

    Private Async Sub btn_nuevo_medicamento_Click(sender As Object, e As EventArgs) Handles btn_nuevo_medicamento.Click
        Dim formCrear As New MedicamentosCreate()
        formCrear.ShowDialog()
        Await CargarMedicamentos()
    End Sub

    '  EVENTO UNIFICADO: Lee qué número presionaste y te lleva directo ahí
    Private Sub NumerosPaginacion_Click(sender As Object, e As EventArgs) Handles lb_left.Click, lb_middle.Click, lb_right.Click
        Dim lblClickeado As Label = CType(sender, Label) ' Si usas LinkLabel, cambia la palabra Label por LinkLabel
        Dim pagSeleccionada As Integer

        If Integer.TryParse(lblClickeado.Text, pagSeleccionada) Then
            paginaActual = pagSeleccionada
            MostrarPagina()
        End If
    End Sub
End Class