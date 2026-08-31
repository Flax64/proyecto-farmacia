Imports System.Net.Http
Imports System.Text.Json
Imports System.ComponentModel

Public Class ComprasRead
    Private clienteHttp As HttpClient
    Private todasLasCompras As New List(Of CompraListado)()
    Private listaFiltroActual As New List(Of CompraListado)()

    Private registrosPorPagina As Integer = 6
    Private paginaActual As Integer = 1

    Private Async Sub Compras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Configuración de Red
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
        clienteHttp.BaseAddress = New Uri("http://localhost:5133/")

        '  EL TRUCO DEL CALENDARIO: Activamos el CheckBox y lo empezamos apagado
        dtpk_fecha.ShowCheckBox = True
        dtpk_fecha.Checked = False

        ' 2. Estilo Base
        dgv_Compras.BackgroundColor = Color.White
        dgv_Compras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_Compras.AllowUserToAddRows = False
        dgv_Compras.RowHeadersVisible = False
        dgv_Compras.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ' 3. Cargar Datos (Ya no necesitamos cargar el combo, solo las compras)
        Await CargarCompras()
    End Sub

    ' --- CARGAR COMPRAS ---
    Private Async Function CargarCompras() As Task
        Try
            Dim respuesta = Await clienteHttp.GetAsync("api/Compras/lista")
            Dim json As String = Await respuesta.Content.ReadAsStringAsync()

            If respuesta.IsSuccessStatusCode Then
                Dim opciones = New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                todasLasCompras = JsonSerializer.Deserialize(Of List(Of CompraListado))(json, opciones)
                AplicarFiltros()
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(json).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = json
                End Try
                MessageBox.Show("No se pudieron cargar las compras." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- EVENTOS DE FILTROS ---
    '  Este es tu nuevo buscador estilo Ventas
    Private Sub txb_buscar_compra_TextChanged(sender As Object, e As EventArgs) Handles txb_buscar_compra.TextChanged
        AplicarFiltros()
    End Sub

    Private Sub dtpk_fecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpk_fecha.ValueChanged
        AplicarFiltros()
    End Sub

    Private Sub AplicarFiltros()
        If todasLasCompras Is Nothing OrElse todasLasCompras.Count = 0 Then
            listaFiltroActual = New List(Of CompraListado)()
            MostrarPagina()
            Return
        End If

        Dim listaFiltrada = todasLasCompras

        '  FILTRO 1: Por Texto Libre (Buscador)
        Dim textoBusqueda As String = txb_buscar_compra.Text.Trim().ToLower()
        If Not String.IsNullOrWhiteSpace(textoBusqueda) Then
            listaFiltrada = listaFiltrada.Where(Function(c) c.Proveedor IsNot Nothing AndAlso c.Proveedor.ToLower().Contains(textoBusqueda)).ToList()
        End If

        '  FILTRO 2: Por Fecha (Controlado por el CheckBox)
        If dtpk_fecha.Checked Then
            Dim fechaB = dtpk_fecha.Value.ToString("dd/MM/yyyy")
            listaFiltrada = listaFiltrada.Where(Function(c) c.Fecha.ToString("dd/MM/yyyy") = fechaB).ToList()
        End If

        listaFiltroActual = listaFiltrada
        paginaActual = 1 ' Reiniciamos a la página 1 al buscar
        MostrarPagina()
    End Sub

    ' --- MOSTRAR PÁGINA ---
    Private Sub MostrarPagina()
        If listaFiltroActual Is Nothing OrElse listaFiltroActual.Count = 0 Then
            dgv_Compras.DataSource = New List(Of CompraListado)()
            ActualizarLabelsPaginacion()
            Return
        End If

        Dim saltar = (paginaActual - 1) * registrosPorPagina
        Dim pedacitoPagina = listaFiltroActual.Skip(saltar).Take(registrosPorPagina).ToList()

        dgv_Compras.DataSource = Nothing
        dgv_Compras.DataSource = pedacitoPagina

        AjustarBotonesFijos()
        ActualizarLabelsPaginacion()
    End Sub

    '  EVENTO MAESTRO: Se ejecuta cuando la tabla termina de cargar los datos
    Private Sub dgv_Compras_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_Compras.DataBindingComplete

        ' 1. OCULTAR LAS COLUMNAS INTERNAS
        If dgv_Compras.Columns.Contains("IdProveedor") Then dgv_Compras.Columns("IdProveedor").Visible = False
        If dgv_Compras.Columns.Contains("Estatus") Then dgv_Compras.Columns("Estatus").Visible = False

        ' 2. CAMBIAR LOS NOMBRES DE ENCABEZADO Y FORMATO
        If dgv_Compras.Columns.Contains("IdCompra") Then dgv_Compras.Columns("IdCompra").HeaderText = "Id"
        If dgv_Compras.Columns.Contains("Total") Then dgv_Compras.Columns("Total").DefaultCellStyle.Format = "C2"

        ' 3. FORZAR EL ORDENAMIENTO DE LAS COLUMNAS
        Try
            If dgv_Compras.Columns.Contains("IdCompra") Then dgv_Compras.Columns("IdCompra").DisplayIndex = 0
            If dgv_Compras.Columns.Contains("Proveedor") Then dgv_Compras.Columns("Proveedor").DisplayIndex = 1
            If dgv_Compras.Columns.Contains("Fecha") Then dgv_Compras.Columns("Fecha").DisplayIndex = 2
            If dgv_Compras.Columns.Contains("Hora") Then dgv_Compras.Columns("Hora").DisplayIndex = 3
            If dgv_Compras.Columns.Contains("Total") Then dgv_Compras.Columns("Total").DisplayIndex = 4

            ' Los botones siempre hasta la derecha
            If dgv_Compras.Columns.Contains("colEditar") Then dgv_Compras.Columns("colEditar").DisplayIndex = dgv_Compras.Columns.Count - 2
            If dgv_Compras.Columns.Contains("colEliminar") Then dgv_Compras.Columns("colEliminar").DisplayIndex = dgv_Compras.Columns.Count - 1
        Catch ex As Exception
            ' Ignoramos si alguna columna falta temporalmente
        End Try
    End Sub

    Private Sub AjustarBotonesFijos()
        If Not dgv_Compras.Columns.Contains("colEditar") Then
            Dim btnEdit As New DataGridViewButtonColumn() With {
                .Name = "colEditar", .HeaderText = "", .Text = "✏️",
                .UseColumnTextForButtonValue = True, .Width = 30, .FlatStyle = FlatStyle.Flat,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            }
            dgv_Compras.Columns.Add(btnEdit)
        End If

        If Not dgv_Compras.Columns.Contains("colEliminar") Then
            Dim btnDel As New DataGridViewButtonColumn() With {
                .Name = "colEliminar", .HeaderText = "", .Text = "🗑️",
                .UseColumnTextForButtonValue = True, .Width = 30, .FlatStyle = FlatStyle.Flat,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            }
            dgv_Compras.Columns.Add(btnDel)
        End If
    End Sub

    ' --- EVENTOS DE CLIC EN ICONOS ---
    Private Async Sub dgv_Compras_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Compras.CellContentClick
        If e.RowIndex < 0 Then Return
        Dim nombreCol = dgv_Compras.Columns(e.ColumnIndex).Name
        If nombreCol <> "colEditar" AndAlso nombreCol <> "colEliminar" Then Return

        '  NUEVO BLOQUEO (Protege a ambos botones)
        Dim estatusFila As String = dgv_Compras.Rows(e.RowIndex).Cells("Estatus").Value.ToString().ToLower()
        If estatusFila.Contains("cancelada") Then
            MessageBox.Show("Esta compra ya se encuentra cancelada. No es posible editarla ni cancelarla de nuevo.", "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim idCompra = Convert.ToInt32(dgv_Compras.Rows(e.RowIndex).Cells("IdCompra").Value)

        ' --- LÓGICA DE EDITAR ---
        If nombreCol = "colEditar" Then
            Dim frm As New ComprasUpdate(idCompra)
            If frm.ShowDialog() = DialogResult.OK Then
                Await CargarCompras()
            End If

            ' --- LÓGICA DE ELIMINAR ---
        ElseIf nombreCol = "colEliminar" Then
            Dim frmBorrar As New ComprasDelete(idCompra) ' Asumo que crearemos un ComprasDelete igual a VentasDelete
            If frmBorrar.ShowDialog() = DialogResult.Yes Then
                Try
                    Dim resp = Await clienteHttp.DeleteAsync($"api/Compras/{idCompra}")
                    Dim responseBody = Await resp.Content.ReadAsStringAsync()

                    If resp.IsSuccessStatusCode Then
                        MessageBox.Show("¡Compra cancelada y stock revertido con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Await CargarCompras()
                    Else
                        '  ATRAPAMOS EL ERROR AL ELIMINAR
                        Dim errorMsg As String = "Error desconocido del servidor."
                        Try
                            Dim errorData = JsonDocument.Parse(responseBody).RootElement
                            If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                        Catch
                            errorMsg = responseBody
                        End Try
                        MessageBox.Show("No se pudo cancelar la compra." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    ' ---  PAGINACIÓN MEJORADA (ESTILO VENTAS) ---
    Private Sub ActualizarLabelsPaginacion()
        Dim totalPaginas As Integer = Math.Ceiling(listaFiltroActual.Count / registrosPorPagina)

        If totalPaginas = 0 Then
            lb_left.Visible = False : lb_middle.Visible = False : lb_right.Visible = False
            Return
        End If

        Dim numeroInicio As Integer
        If totalPaginas <= 3 Then
            numeroInicio = 1
        ElseIf paginaActual = 1 Then
            numeroInicio = 1
        ElseIf paginaActual = totalPaginas Then
            numeroInicio = totalPaginas - 2
        Else
            numeroInicio = paginaActual - 1
        End If

        lb_left.Text = numeroInicio.ToString()
        lb_left.Visible = (numeroInicio <= totalPaginas)

        lb_middle.Text = (numeroInicio + 1).ToString()
        lb_middle.Visible = ((numeroInicio + 1) <= totalPaginas)

        lb_right.Text = (numeroInicio + 2).ToString()
        lb_right.Visible = ((numeroInicio + 2) <= totalPaginas)

        ' Magia de las Negritas
        lb_left.Font = New Font(lb_left.Font, FontStyle.Regular)
        lb_middle.Font = New Font(lb_middle.Font, FontStyle.Regular)
        lb_right.Font = New Font(lb_right.Font, FontStyle.Regular)

        If lb_left.Text = paginaActual.ToString() AndAlso lb_left.Visible Then
            lb_left.Font = New Font(lb_left.Font, FontStyle.Bold)
        ElseIf lb_middle.Text = paginaActual.ToString() AndAlso lb_middle.Visible Then
            lb_middle.Font = New Font(lb_middle.Font, FontStyle.Bold)
        ElseIf lb_right.Text = paginaActual.ToString() AndAlso lb_right.Visible Then
            lb_right.Font = New Font(lb_right.Font, FontStyle.Bold)
        End If
    End Sub

    Private Sub Numeros_Click(sender As Object, e As EventArgs) Handles lb_left.Click, lb_middle.Click, lb_right.Click
        Dim labelClickeado As Label = CType(sender, Label)
        paginaActual = Convert.ToInt32(labelClickeado.Text)
        MostrarPagina()
    End Sub

    Private Sub lblk_anterior_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblk_anterior.LinkClicked
        If paginaActual > 1 Then
            paginaActual -= 1
            MostrarPagina()
        End If
    End Sub

    Private Sub lblk_siguiente_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblk_siguiente.LinkClicked
        Dim totalPaginas As Integer = Math.Ceiling(listaFiltroActual.Count / registrosPorPagina)
        If paginaActual < totalPaginas Then
            paginaActual += 1
            MostrarPagina()
        End If
    End Sub

    Private Async Sub btn_create_compra_Click(sender As Object, e As EventArgs) Handles btn_create_compra.Click
        Dim frm As New ComprasCreate()
        If frm.ShowDialog() = DialogResult.OK Then
            Await CargarCompras()
        End If
    End Sub

    ' ---  EVENTO PARA PINTAR DE ROJO LAS COMPRAS CANCELADAS ---
    Private Sub dgv_Compras_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv_Compras.CellFormatting
        If e.RowIndex >= 0 AndAlso dgv_Compras.Columns.Contains("Estatus") Then

            ' Obtenemos el texto del estatus de esa fila
            Dim estatusValor As String = dgv_Compras.Rows(e.RowIndex).Cells("Estatus").Value.ToString().ToLower()

            ' Si dice "cancelada" o "cancelado" (dependiendo de cómo esté en tu BD)
            If estatusValor.Contains("cancelad") Then
                ' Pintamos el fondo rojo claro y la letra blanca o negra
                dgv_Compras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightCoral
                dgv_Compras.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
                dgv_Compras.Rows(e.RowIndex).DefaultCellStyle.SelectionBackColor = Color.Red
            End If
        End If
    End Sub
End Class