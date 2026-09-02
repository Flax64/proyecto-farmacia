Imports System.Net.Http
Imports System.Text.Json

Public Class VentasRead
    ' Usamos el mismo HttpClient global que ya sabes usar
    Private clienteHttp As HttpClient
    ' Aquí guardaremos la lista original para poder filtrarla
    Private todasLasVentas As New List(Of VentaVB)()
    Private registrosPorPagina As Integer = 6 ' Cambia este número según cuántas filas quepan en tu tabla
    Private paginaActual As Integer = 1
    Private listaFiltroActual As New List(Of VentaVB)() ' Aquí guardaremos el resultado del buscador antes de paginarlo

    Private Async Sub VentasRead_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuramos el cliente para ignorar el certificado local (como lo hicimos antes)
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        '  EL TRUCO DEL CALENDARIO: Activamos el CheckBox y lo empezamos apagado
        dtpk_fecha_venta.ShowCheckBox = True
        dtpk_fecha_venta.Checked = False

        ' Configuramos la tabla para que se vea bonita y ocupe todo el espacio
        dgv_ventas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_ventas.AllowUserToAddRows = False ' Quitamos la fila vacía del final
        dgv_ventas.BackgroundColor = Color.White
        dgv_ventas.RowHeadersVisible = False

        ' Llamamos a la función que trae los datos
        Await CargarVentas()
    End Sub

    ' La función que va a C#, pide el JSON y llena la tabla
    Private Async Function CargarVentas() As Task
        Try
            Dim urlAPI As String = "http://54.89.200.65:5133/api/ventas/lista"
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlAPI)

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                todasLasVentas = JsonSerializer.Deserialize(Of List(Of VentaVB))(responseBody, opciones)
                AplicarFiltros()
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then
                        errorMsg = errorData.GetProperty("error").GetString()
                    ElseIf errorData.TryGetProperty("message", Nothing) Then
                        errorMsg = errorData.GetProperty("message").GetString()
                    End If
                Catch
                    errorMsg = responseBody ' Si no es JSON, mostramos el texto crudo
                End Try

                MessageBox.Show("No se pudieron cargar las ventas." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub pbx_ver_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Async Sub btn_create_venta_Click(sender As Object, e As EventArgs) Handles btn_create_venta.Click
        ' 1. Preparamos y abrimos la ventana sobrepuesta
        Dim CreateVenta As New VentasCreate()
        CreateVenta.ShowDialog() ' El programa se queda "pausado" aquí esperando al cajero

        ' 2. Cuando el cajero cierra la ventana de cobro, el código continúa aquí.
        ' En lugar de Me.Hide(), mandamos a recargar la base de datos para ver la nueva venta
        Await CargarVentas()
    End Sub

    Private Sub lblk_anterior_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblk_anterior.LinkClicked
        anterior()
    End Sub

    Private Sub anterior()
        ' Solo retrocedemos si no estamos en la primera página
        If paginaActual > 1 Then
            paginaActual -= 1
            MostrarPagina()
        End If
    End Sub

    Private Sub lblk_siguiente_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblk_siguiente.LinkClicked
        siguiente()
    End Sub

    Public Sub siguiente()
        ' Calculamos cuántas páginas hay en total (Ej. si hay 12 registros y caben 5, son 3 páginas)
        Dim totalPaginas As Integer = Math.Ceiling(listaFiltroActual.Count / registrosPorPagina)

        ' Solo avanzamos si no hemos llegado a la última página
        If paginaActual < totalPaginas Then
            paginaActual += 1
            MostrarPagina()
        End If
    End Sub

    Private Sub txb_buscar_venta_TextChanged(sender As Object, e As EventArgs) Handles txb_buscar_venta.TextChanged
        AplicarFiltros()
    End Sub

    Private Sub dtpk_fecha_venta_ValueChanged(sender As Object, e As EventArgs) Handles dtpk_fecha_venta.ValueChanged
        AplicarFiltros()
    End Sub

    Private Sub AplicarFiltros()
        ' 1. Si la lista original está vacía 
        If todasLasVentas Is Nothing OrElse todasLasVentas.Count = 0 Then
            listaFiltroActual = New List(Of VentaVB)() ' Vaciamos el filtro
            MostrarPagina() ' Mandamos a limpiar la tabla
            Return
        End If

        Dim listaFiltrada = todasLasVentas

        ' --- FILTRO 1: Por Texto ---
        Dim textoBusqueda As String = txb_buscar_venta.Text.Trim().ToLower()
        If Not String.IsNullOrWhiteSpace(textoBusqueda) Then
            ' Le agregamos "IsNot Nothing" para que no se trabe si una venta no tiene cliente
            listaFiltrada = listaFiltrada.Where(Function(v) _
            (v.Cliente IsNot Nothing AndAlso v.Cliente.ToLower().Contains(textoBusqueda)) OrElse
            (v.Vendedor IsNot Nothing AndAlso v.Vendedor.ToLower().Contains(textoBusqueda))).ToList()
        End If

        ' --- FILTRO 2: Por Fecha (Controlado por el CheckBox) ---
        If dtpk_fecha_venta.Checked Then
            Dim fechaSeleccionada As String = dtpk_fecha_venta.Value.ToString("dd/MM/yyyy")
            ' Cambiamos FechaHora por Fecha. 
            ' Como ya no trae la hora pegada, podemos usar una igualdad exacta (=)
            listaFiltrada = listaFiltrada.Where(Function(v) _
            v.Fecha IsNot Nothing AndAlso v.Fecha = fechaSeleccionada).ToList()
        End If

        ' --- NUEVO FINAL ---
        ' Guardamos la lista ya filtrada (para que la paginación sepa qué cortar)
        listaFiltroActual = listaFiltrada

        ' Reiniciamos a la página 1 cada vez que el usuario usa el buscador o el calendario
        paginaActual = 1

        ' Mandamos a dibujar la página
        MostrarPagina()
    End Sub

    Private Sub MostrarPagina()
        ' Si no hay nada que mostrar, pasamos una lista vacía para NO borrar los títulos
        If listaFiltroActual Is Nothing OrElse listaFiltroActual.Count = 0 Then
            dgv_ventas.DataSource = New List(Of VentaVB)() ' Lista vacía en vez de Nothing
            ActualizarLabelsPaginacion() ' Ocultamos los números (1, 2, 3)
            Return
        End If

        ' ¡LA MAGIA DE LA PAGINACIÓN!
        ' Calculamos cuántos registros debemos saltar
        Dim registrosASaltar As Integer = (paginaActual - 1) * registrosPorPagina

        ' Cortamos el pedacito exacto que queremos ver
        Dim pedacitoPagina = listaFiltroActual.Skip(registrosASaltar).Take(registrosPorPagina).ToList()

        ' Mostramos ese pedacito en la tabla
        dgv_ventas.DataSource = Nothing
        dgv_ventas.DataSource = pedacitoPagina

        ' Revisamos que la columna exista antes de formatearla para evitar errores
        If dgv_ventas.Columns.Contains("Total") Then
            dgv_ventas.Columns("Total").DefaultCellStyle.Format = "C2"
        End If

        ' --- NUEVO: COLUMNAS DE ACCIONES (CORREGIDAS) ---
        ' 1. Columna de Editar
        If Not dgv_ventas.Columns.Contains("colEditar") Then
            Dim btnEditar As New DataGridViewButtonColumn()
            btnEditar.Name = "colEditar"
            btnEditar.HeaderText = "" ' Lo dejamos vacío para que se vea más limpio
            btnEditar.Text = "✏️"
            btnEditar.UseColumnTextForButtonValue = True
            btnEditar.FlatStyle = FlatStyle.Flat

            '  MAGIA 1: Le decimos que NO se estire y le damos un ancho fijo en píxeles
            btnEditar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            btnEditar.Width = 35

            dgv_ventas.Columns.Add(btnEditar)
        End If

        ' 2. Columna de Eliminar
        If Not dgv_ventas.Columns.Contains("colEliminar") Then
            Dim btnEliminar As New DataGridViewButtonColumn()
            btnEliminar.Name = "colEliminar"
            btnEliminar.HeaderText = ""
            btnEliminar.Text = "🗑️"
            btnEliminar.UseColumnTextForButtonValue = True
            btnEliminar.FlatStyle = FlatStyle.Flat

            '  MAGIA 1: Le decimos que NO se estire y le damos un ancho fijo en píxeles
            btnEliminar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            btnEliminar.Width = 35

            dgv_ventas.Columns.Add(btnEliminar)
        End If

        '  MAGIA 2: Obligamos a ambas columnas a irse hasta la derecha de la tabla
        dgv_ventas.Columns("colEditar").DisplayIndex = dgv_ventas.Columns.Count - 1
        dgv_ventas.Columns("colEliminar").DisplayIndex = dgv_ventas.Columns.Count - 1
        '  Ocultar el estatus para que quede limpio
        If dgv_ventas.Columns.Contains("Estatus") Then dgv_ventas.Columns("Estatus").Visible = False

        ActualizarLabelsPaginacion()
    End Sub

    Private Sub ActualizarLabelsPaginacion()
        ' 1. Calculamos el total de páginas reales
        Dim totalPaginas As Integer = Math.Ceiling(listaFiltroActual.Count / registrosPorPagina)

        ' Si no hay registros (0 páginas), escondemos los números y salimos
        If totalPaginas = 0 Then
            lb_left.Visible = False
            lb_middle.Visible = False
            lb_right.Visible = False
            Return
        End If

        ' 2. Calculamos con qué número debe empezar el primer label (lb_left)
        Dim numeroInicio As Integer
        If totalPaginas <= 3 Then
            ' Si hay 3 páginas o menos, siempre empezamos en el 1
            numeroInicio = 1
        ElseIf paginaActual = 1 Then
            ' Si estamos al inicio, mostramos 1, 2, 3
            numeroInicio = 1
        ElseIf paginaActual = totalPaginas Then
            ' Si estamos al final (Ej. pag 10), mostramos 8, 9, 10
            numeroInicio = totalPaginas - 2
        Else
            ' Si estamos en medio (Ej. pag 5), mostramos uno antes (4, 5, 6)
            numeroInicio = paginaActual - 1
        End If

        ' 3. Asignamos los textos y mostramos/ocultamos según corresponda
        lb_left.Text = numeroInicio.ToString()
        lb_left.Visible = (numeroInicio <= totalPaginas)

        lb_middle.Text = (numeroInicio + 1).ToString()
        lb_middle.Visible = ((numeroInicio + 1) <= totalPaginas)

        lb_right.Text = (numeroInicio + 2).ToString()
        lb_right.Visible = ((numeroInicio + 2) <= totalPaginas)

        ' 4. ¡LA MAGIA DE LAS NEGRITAS!
        ' Primero le quitamos las negritas a todos (los hacemos normales)
        lb_left.Font = New Font(lb_left.Font, FontStyle.Regular)
        lb_middle.Font = New Font(lb_middle.Font, FontStyle.Regular)
        lb_right.Font = New Font(lb_right.Font, FontStyle.Regular)

        ' Luego, buscamos cuál coincide con la página actual y lo ponemos en negritas
        If lb_left.Text = paginaActual.ToString() AndAlso lb_left.Visible Then
            lb_left.Font = New Font(lb_left.Font, FontStyle.Bold)
        ElseIf lb_middle.Text = paginaActual.ToString() AndAlso lb_middle.Visible Then
            lb_middle.Font = New Font(lb_middle.Font, FontStyle.Bold)
        ElseIf lb_right.Text = paginaActual.ToString() AndAlso lb_right.Visible Then
            lb_right.Font = New Font(lb_right.Font, FontStyle.Bold)
        End If
    End Sub

    Private Sub Numeros_Click(sender As Object, e As EventArgs) Handles lb_left.Click, lb_middle.Click, lb_right.Click
        ' Averiguamos a qué label le dieron clic
        Dim labelClickeado As Label = CType(sender, Label)

        ' Cambiamos la página actual al número que dice el label
        paginaActual = Convert.ToInt32(labelClickeado.Text)

        ' Mandamos a dibujar la nueva página (esto automáticamente moverá las negritas)
        MostrarPagina()
    End Sub

    Private Async Sub dgv_ventas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_ventas.CellContentClick
        ' Ignoramos los clics en los encabezados de la tabla
        If e.RowIndex < 0 Then Return

        ' Averiguamos el nombre de la columna a la que le dieron clic
        Dim nombreColumna As String = dgv_ventas.Columns(e.ColumnIndex).Name

        ' Si no es Editar ni Eliminar, no hacemos nada
        If nombreColumna <> "colEditar" AndAlso nombreColumna <> "colEliminar" Then Return

        '  EL CANDADO DEBE IR AQUÍ (Antes de preguntar qué botón presionó)
        If dgv_ventas.Columns.Contains("Estatus") AndAlso dgv_ventas.Rows(e.RowIndex).Cells("Estatus").Value IsNot Nothing Then
            Dim estatusFila As String = dgv_ventas.Rows(e.RowIndex).Cells("Estatus").Value.ToString().ToLower()
            If estatusFila.Contains("cancelada") Then
                MessageBox.Show("Esta venta ya se encuentra cancelada. No se puede editar ni borrar de nuevo.", "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return ' 🛑 Cortamos la ejecución aquí para AMBOS botones
            End If
        End If

        ' Capturamos el ID de la venta seleccionada en esa fila
        Dim idVentaSeleccionada As Integer = Convert.ToInt32(dgv_ventas.Rows(e.RowIndex).Cells("Id").Value)

        ' --- LÓGICA DE EDITAR ---
        If nombreColumna = "colEditar" Then
            ' Abrimos la pantalla de actualizar y le pasamos el ID por parámetro
            Dim formEditar As New VentasUpdate(idVentaSeleccionada)
            formEditar.ShowDialog()

            ' Cuando el usuario cierre la ventana de editar, recargamos la tabla para ver los cambios
            Await CargarVentas()

            ' --- LÓGICA DE ELIMINAR ---
        ElseIf nombreColumna = "colEliminar" Then
            ' Abrimos la pantalla de eliminar y le pasamos el ID por parámetro
            Dim formEliminar As New VentasDelete(idVentaSeleccionada)
            formEliminar.ShowDialog()

            ' Cuando el usuario cierre la ventana de eliminar, recargamos la tabla por si la borró
            Await CargarVentas()
        End If
    End Sub

    ' ---  EVENTO PARA PINTAR DE ROJO LAS VENTAS CANCELADAS ---
    Private Sub dgv_ventas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv_ventas.CellFormatting
        If e.RowIndex >= 0 AndAlso dgv_ventas.Columns.Contains("Estatus") Then
            If dgv_ventas.Rows(e.RowIndex).Cells("Estatus").Value IsNot Nothing Then
                Dim estatusValor As String = dgv_ventas.Rows(e.RowIndex).Cells("Estatus").Value.ToString().ToLower()

                If estatusValor.Contains("cancelad") Then
                    dgv_ventas.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightCoral
                    dgv_ventas.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
                    dgv_ventas.Rows(e.RowIndex).DefaultCellStyle.SelectionBackColor = Color.Red
                End If
            End If
        End If
    End Sub
End Class