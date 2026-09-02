Imports System.ComponentModel
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class VentasUpdate
    Private clienteHttp As HttpClient
    Private carritoCompras As New BindingList(Of FilaCarrito)
    Private idVentaActual As Integer ' Aquí guardamos el ID que vamos a editar
    Private listaVirtualMedicamentos As New BindingList(Of MedicamentoVB)
    Private idConsultaCargada As Integer? = Nothing

    ' =====================================================================
    ' 1. CONSTRUCTOR (Recibe el ID de la tabla principal)
    ' =====================================================================
    Public Sub New(idVenta As Integer)
        InitializeComponent()
        idVentaActual = idVenta
        Label2.Text = "Venta " & idVentaActual ' Cambiamos el título como en tu imagen
    End Sub

    ' =====================================================================
    ' 2. AL CARGAR (Descargamos los datos desde C#)
    ' =====================================================================
    Private Async Sub VentasUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy")

        ' 1. PRIMERO DESCARGAMOS TODOS LOS CATÁLOGOS (LAS LISTAS)
        Await CargarMetodosDePagoBD()
        Await CargarBuscadorMedicamentos()
        Await CargarBuscadorPacientes()

        dgv_carrito.DataSource = carritoCompras
        dgv_carrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_carrito.AllowUserToAddRows = False
        dgv_carrito.BackgroundColor = Color.White
        dgv_carrito.RowHeadersVisible = False

        If dgv_carrito.Columns.Contains("P_Unit") Then dgv_carrito.Columns("P_Unit").DefaultCellStyle.Format = "C2"
        If dgv_carrito.Columns.Contains("Subtotal") Then dgv_carrito.Columns("Subtotal").DefaultCellStyle.Format = "C2"

        AgregarColumnaEliminar()

        ' 2. AL FINAL DESCARGAMOS EL TICKET Y LLENAMOS LAS CAJAS
        Await CargarDatosDeLaVenta()
    End Sub

    Private Async Function CargarDatosDeLaVenta() As Task
        Try
            Dim urlAPI As String = $"http://54.89.200.65:5133/api/ventas/{idVentaActual}"
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlAPI)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim ventaRecibida = JsonSerializer.Deserialize(Of VentaCompletaVB)(responseBody, opciones)

                cmb_cliente.Text = ventaRecibida.NombreCliente
                cmb_metodo_pago.SelectedValue = ventaRecibida.IdMetodoPago

                For Each item In ventaRecibida.Detalles
                    carritoCompras.Add(item)
                Next

                ActualizarTotales()
            Else
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudo cargar la información de esta venta." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Function


    ' =====================================================================
    ' EL RESTO DEL CÓDIGO ES IDÉNTICO AL DE "CREAR"
    ' =====================================================================
    Private Sub AgregarColumnaEliminar()
        If Not dgv_carrito.Columns.Contains("colEliminar") Then
            Dim btnEliminar As New DataGridViewButtonColumn()
            btnEliminar.Name = "colEliminar"
            btnEliminar.HeaderText = ""
            btnEliminar.Text = "🗑️"
            btnEliminar.UseColumnTextForButtonValue = True
            btnEliminar.FlatStyle = FlatStyle.Flat
            btnEliminar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            btnEliminar.Width = 35
            dgv_carrito.Columns.Add(btnEliminar)
            dgv_carrito.Columns("colEliminar").DisplayIndex = dgv_carrito.Columns.Count - 1
        End If
    End Sub

    Private Sub btn_agregar_Click(sender As Object, e As EventArgs) Handles btn_agregar.Click
        If cmb_buscar_producto.SelectedItem Is Nothing Then
            MessageBox.Show("Por favor, selecciona un producto válido de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim medSeleccionado As MedicamentoVB = CType(cmb_buscar_producto.SelectedItem, MedicamentoVB)
        Dim cantidadAgregar As Integer = Convert.ToInt32(nud_cantidad.Value)

        '  3. VALIDAMOS CONTRA EL STOCK VIRTUAL (Lo que hay en la memoria)
        If cantidadAgregar > medSeleccionado.Stock Then
            MessageBox.Show($"¡No hay suficiente stock! Solo quedan {medSeleccionado.Stock} disponibles.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        '  4. DESCONTAMOS DEL INVENTARIO VIRTUAL INMEDIATAMENTE
        medSeleccionado.Stock -= cantidadAgregar

        ' Agrupamos o agregamos al carrito
        Dim articuloExistente = carritoCompras.FirstOrDefault(Function(x) x.IdProducto = medSeleccionado.Id)

        If articuloExistente IsNot Nothing Then
            articuloExistente.Cant += cantidadAgregar
            articuloExistente.Subtotal = articuloExistente.Cant * articuloExistente.P_Unit
            carritoCompras.ResetBindings()
        Else
            carritoCompras.Add(New FilaCarrito() With {
                .IdProducto = medSeleccionado.Id,
                .Producto = medSeleccionado.Nombre,
                .Cant = cantidadAgregar,
                .P_Unit = medSeleccionado.Precio,
                .Subtotal = cantidadAgregar * medSeleccionado.Precio
            })
        End If

        ' Limpiamos controles
        cmb_buscar_producto.SelectedIndex = -1
        cmb_buscar_producto.Text = ""
        nud_cantidad.Value = 1
        cmb_buscar_producto.Focus()

        ActualizarTotales()
    End Sub

    Private Sub dgv_carrito_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_carrito.CellContentClick
        If e.RowIndex >= 0 AndAlso dgv_carrito.Columns(e.ColumnIndex).Name = "colEliminar" Then

            '  5. ATRAPAMOS LOS DATOS DE LA FILA ANTES DE DESTRUIRLA
            Dim filaBorranda = carritoCompras(e.RowIndex)

            ' Buscamos si el medicamento aún existe en el ComboBox
            Dim medVirtual = listaVirtualMedicamentos.FirstOrDefault(Function(m) m.Id = filaBorranda.IdProducto)

            If medVirtual IsNot Nothing Then
                ' Si existe (porque había más stock), le DEVOLVEMOS lo que tenía el carrito
                medVirtual.Stock += filaBorranda.Cant
            Else
                ' Si NO existe (porque nos habíamos acabado el stock original), LO REVIVIMOS
                listaVirtualMedicamentos.Add(New MedicamentoVB() With {
                    .Id = filaBorranda.IdProducto,
                    .Nombre = filaBorranda.Producto,
                    .Precio = filaBorranda.P_Unit,
                    .Stock = filaBorranda.Cant
                })
            End If

            ' Refrescamos la memoria visual del ComboBox
            listaVirtualMedicamentos.ResetBindings()

            '  6. AHORA SÍ, BORRAMOS DEL CARRITO
            carritoCompras.RemoveAt(e.RowIndex)
            ActualizarTotales()
        End If
    End Sub

    ' =====================================================================
    ' MATEMÁTICAS (SUBTOTAL BASE, IVA DESGLOSADO, TOTAL)
    ' =====================================================================
    Private Sub ActualizarTotales()
        Dim sumaTotalPagar As Decimal = 0

        ' 1. Sumamos el total neto que el cliente va a pagar (precio de etiqueta * cantidad)
        For Each item In carritoCompras
            sumaTotalPagar += item.Subtotal
        Next

        ' 2. DESGLOSE INVERSO (Asumiendo que el IVA en México es del 16%)
        ' El subtotal base se obtiene dividiendo el total entre 1.16
        Dim subtotalBase As Decimal = sumaTotalPagar / 1.16D

        ' El IVA es la diferencia entre el Total final y el Subtotal base
        Dim ivaDesglosado As Decimal = sumaTotalPagar - subtotalBase

        ' 3. Mostramos en pantalla
        lbl_subtotal_valor.Text = subtotalBase.ToString("C2")
        lbl_iva_valor.Text = ivaDesglosado.ToString("C2")
        lbl_total_valor.Text = sumaTotalPagar.ToString("C2")
    End Sub

    ' =====================================================================
    ' BOTÓN FINALIZAR (AQUÍ HACEMOS EL PUT EN VEZ DE POST)
    ' =====================================================================
    Private Async Sub btn_finalizar_Click(sender As Object, e As EventArgs) Handles btn_finalizar.Click
        If carritoCompras.Count = 0 Then
            MessageBox.Show("El carrito está vacío. Agrega al menos un medicamento para poder actualizar la venta.", "Carrito Vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmb_buscar_producto.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(cmb_cliente.Text) Then
            MessageBox.Show("Por favor, ingresa el nombre del cliente.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmb_cliente.Focus()
            Return
        End If

        If cmb_metodo_pago.SelectedIndex = -1 OrElse cmb_metodo_pago.SelectedItem Is Nothing Then
            MessageBox.Show("Por favor, selecciona un método de pago válido.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmb_metodo_pago.Focus()
            Return
        End If

        btn_finalizar.Enabled = False
        btn_finalizar.Text = "Actualizando..."

        Try
            Dim detallesList As New List(Of Object)
            Dim sumaParaTotal As Decimal = 0

            For Each item In carritoCompras
                detallesList.Add(New With {.IdMedicamento = item.IdProducto, .Cantidad = item.Cant})
                sumaParaTotal += item.Subtotal
            Next

            Dim idMetodo As Integer = Convert.ToInt32(cmb_metodo_pago.SelectedValue)

            Dim datosActualizados = New With {
                .IdUsuario = SesionGlobal.idUsuario,
                .IdMetodoPago = idMetodo,
                .NombreCliente = cmb_cliente.Text.Trim(),
                .TotalVenta = sumaParaTotal,
                .Detalles = detallesList
            }

            Dim jsonString As String = JsonSerializer.Serialize(datosActualizados)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            Dim urlAPI As String = $"http://54.89.200.65:5133/api/ventas/{idVentaActual}"
            Dim response As HttpResponseMessage = Await clienteHttp.PutAsync(urlAPI, content)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Venta actualizada con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudo actualizar la venta." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_finalizar.Enabled = True
            btn_finalizar.Text = "FINALIZAR VENTA"
        End Try
    End Sub

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub
    Private Async Function CargarMetodosDePagoBD() As Task
        Try
            Dim urlAPI As String = "http://54.89.200.65:5133/api/ventas/metodos-pago"
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlAPI)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim listaMetodos = JsonSerializer.Deserialize(Of List(Of MetodoPagoVB))(responseBody, opciones)

                cmb_metodo_pago.Items.Clear()
                cmb_metodo_pago.DataSource = listaMetodos
                cmb_metodo_pago.DisplayMember = "Nombre"
                cmb_metodo_pago.ValueMember = "Id"
            Else
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudieron cargar los métodos de pago." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Private Async Function CargarBuscadorMedicamentos() As Task
        Try
            Dim urlAPI As String = "http://54.89.200.65:5133/api/ventas/medicamentos"
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlAPI)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim listaMedicamentos = JsonSerializer.Deserialize(Of List(Of MedicamentoVB))(responseBody, opciones)

                listaVirtualMedicamentos = New BindingList(Of MedicamentoVB)(listaMedicamentos)

                cmb_buscar_producto.DataSource = listaVirtualMedicamentos
                cmb_buscar_producto.DisplayMember = "Nombre"
                cmb_buscar_producto.ValueMember = "Id"
                cmb_buscar_producto.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                cmb_buscar_producto.AutoCompleteSource = AutoCompleteSource.ListItems
                cmb_buscar_producto.SelectedIndex = -1
            Else
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudo cargar el catálogo de medicamentos." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Private Async Function CargarBuscadorPacientes() As Task
        Try
            Dim urlAPI As String = "http://54.89.200.65:5133/api/ventas/consultas-pendientes"
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlAPI)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim listaConsultas = JsonSerializer.Deserialize(Of List(Of ConsultaVB))(responseBody, opciones)

                cmb_cliente.DataSource = listaConsultas
                cmb_cliente.DisplayMember = "Nombre"
                cmb_cliente.ValueMember = "IdConsulta"
                cmb_cliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                cmb_cliente.AutoCompleteSource = AutoCompleteSource.ListItems
                cmb_cliente.SelectedIndex = -1
            Else
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudo cargar la lista de pacientes en espera." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    ' Esta función centralizada se encarga de todo el proceso de la receta
    Private Async Function ImportarRecetaAlCarrito(consultaSeleccionada As ConsultaVB) As Task
        Dim respuesta = MessageBox.Show($"El paciente {consultaSeleccionada.Nombre} tiene una receta médica reciente." & vbCrLf & "¿Deseas cargar los medicamentos recetados a esta venta?", "Receta Encontrada", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If respuesta = DialogResult.Yes Then
            Try
                Dim urlAPI As String = $"http://54.89.200.65:5133/api/ventas/receta/{consultaSeleccionada.IdConsulta}"
                Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlAPI)
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                If response.IsSuccessStatusCode Then
                    Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                    Dim recetaList = JsonSerializer.Deserialize(Of List(Of MedicamentoRecetaVB))(responseBody, opciones)

                    If recetaList.Count = 0 Then
                        MessageBox.Show("Esta consulta no tiene medicamentos recetados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                    '  1. CREAMOS UNA LISTA PARA MEMORIZAR LOS QUE NO ALCANZAN
                    Dim medicamentosOmitidos As New List(Of String)()

                    For Each med In recetaList
                        '  2. VALIDAMOS: ¿El stock es menor a la cantidad que pide el doctor?
                        If med.Stock < med.Cantidad Then
                            ' Lo anotamos en nuestra libreta virtual y saltamos al siguiente
                            medicamentosOmitidos.Add($"- {med.Nombre} (Recetado: {med.Cantidad}, Stock actual: {med.Stock})")
                            Continue For
                        End If

                        ' Si sí hay stock, lo agregamos al carrito normal
                        Dim articuloExistente = carritoCompras.FirstOrDefault(Function(x) x.IdProducto = med.Id)
                        If articuloExistente IsNot Nothing Then
                            articuloExistente.Cant += med.Cantidad
                            articuloExistente.Subtotal = articuloExistente.Cant * articuloExistente.P_Unit
                        Else
                            carritoCompras.Add(New FilaCarrito() With {
                                .IdProducto = med.Id,
                                .Producto = med.Nombre,
                                .Cant = med.Cantidad,
                                .P_Unit = med.Precio,
                                .Subtotal = med.Cantidad * med.Precio
                            })
                        End If
                    Next

                    carritoCompras.ResetBindings()
                    ActualizarTotales()
                    cmb_metodo_pago.Focus()

                    '  3. MOSTRAMOS EL REPORTE SI HUBO MEDICAMENTOS FALTANTES
                    If medicamentosOmitidos.Count > 0 Then
                        Dim mensaje As String = "La receta se cargó parcialmente. Los siguientes medicamentos quedaron PENDIENTES por falta de stock:" & vbCrLf & vbCrLf
                        mensaje &= String.Join(vbCrLf, medicamentosOmitidos)
                        MessageBox.Show(mensaje, "Medicamentos Pendientes / Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                Else
                    Dim errorMsg As String = "Error desconocido del servidor."
                    Try
                        Dim errorData = JsonDocument.Parse(responseBody).RootElement
                        If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                    Catch
                        errorMsg = responseBody
                    End Try
                    MessageBox.Show("No se pudo cargar la receta del paciente." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            ' SI EL USUARIO DICE QUE NO, DESVINCULAMOS LA RECETA PARA QUE SEA VENTA LIBRE
            idConsultaCargada = Nothing
        End If
    End Function
    ' OPCIÓN A: Cuando el usuario usa el RATÓN y hace clic en un nombre
    Private Async Sub cmb_cliente_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmb_cliente.SelectionChangeCommitted
        If cmb_cliente.SelectedItem Is Nothing Then Return
        Dim consulta As ConsultaVB = CType(cmb_cliente.SelectedItem, ConsultaVB)

        ' VALIDACIÓN: Si el ID es mayor a 0, tiene receta. Si es 0, es venta libre.
        If consulta.IdConsulta > 0 Then
            Await ImportarRecetaAlCarrito(consulta)
        Else
            idConsultaCargada = Nothing ' Desvinculamos cualquier receta anterior
        End If
    End Sub

    ' OPCIÓN B: Cuando el usuario usa el TECLADO y presiona ENTER
    Private Async Sub cmb_cliente_KeyDown(sender As Object, e As KeyEventArgs) Handles cmb_cliente.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            e.Handled = True

            If cmb_cliente.SelectedItem Is Nothing Then Return
            Dim consulta As ConsultaVB = CType(cmb_cliente.SelectedItem, ConsultaVB)

            ' Misma validación para el teclado
            If consulta.IdConsulta > 0 Then
                Await ImportarRecetaAlCarrito(consulta)
            Else
                idConsultaCargada = Nothing ' Desvinculamos cualquier receta anterior
                cmb_buscar_producto.Focus() ' Saltamos directo a los medicamentos
            End If
        End If
    End Sub
    ' EVENTO MAESTRO: Ocultar columnas técnicas automáticamente
    Private Sub dgv_carrito_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_carrito.DataBindingComplete
        ' 1. Ocultar IdProducto
        If dgv_carrito.Columns.Contains("IdProducto") Then
            dgv_carrito.Columns("IdProducto").Visible = False
        End If

        ' 2. Asegurar que el botón de eliminar (basurero) siempre esté al final a la derecha
        If dgv_carrito.Columns.Contains("colEliminar") Then
            dgv_carrito.Columns("colEliminar").DisplayIndex = dgv_carrito.Columns.Count - 1
        End If
    End Sub
End Class