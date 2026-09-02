Imports System.Net.Http
Imports System.Text.Json
Imports System.ComponentModel
Imports System.Text
Imports System.Linq '  ¡ESTE ES EL QUE TE FALTA PARA QUE FUNCIONE EL FirstOrDefault!
Public Class ComprasUpdate
    Private idCompra As Integer
    Private clienteHttp As HttpClient
    Private carritoEdicion As New BindingList(Of FilaCarrito)

    Private jsonOpciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}

    Public Sub New(id As Integer)
        InitializeComponent()
        idCompra = id

        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
        clienteHttp.BaseAddress = New Uri("http://54.89.200.65:5133/")
    End Sub

    Private Async Sub ComprasEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lb_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy")

        dgv_Compra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_Compra.AllowUserToAddRows = False
        dgv_Compra.RowHeadersVisible = False
        dgv_Compra.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_Compra.DataSource = carritoEdicion
        dgv_Compra.BackgroundColor = Color.White

        cmb_Proveedores.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_Proveedores.AutoCompleteSource = AutoCompleteSource.ListItems
        cmb_Medicamentos.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_Medicamentos.AutoCompleteSource = AutoCompleteSource.ListItems

        If Not dgv_Compra.Columns.Contains("colEliminar") Then
            Dim btnDel As New DataGridViewButtonColumn() With {
                .Name = "colEliminar", .HeaderText = "", .Text = "🗑️",
                .UseColumnTextForButtonValue = True, .Width = 35, .FlatStyle = FlatStyle.Flat,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            }
            dgv_Compra.Columns.Add(btnDel)
        End If

        Await CargarProveedores()
        Await CargarMedicamentos()
        Await CargarDatosCompra()
    End Sub

    ' NUEVA FUNCIÓN PARA CALCULAR LOS TOTALES
    Private Sub ActualizarTotales()
        Dim sumaTotalPagar As Decimal = 0

        If carritoEdicion.Count > 0 Then
            sumaTotalPagar = carritoEdicion.Sum(Function(f) f.Subtotal)
        End If

        ' DESGLOSE INVERSO
        Dim subtotalBase As Decimal = sumaTotalPagar / 1.16D
        Dim ivaDesglosado As Decimal = sumaTotalPagar - subtotalBase

        ' Actualizamos los labels en pantalla
        lbl_SubtotalValue.Text = subtotalBase.ToString("C2")
        lbl_IVAValue.Text = ivaDesglosado.ToString("C2")
        lbl_TotalValue.Text = sumaTotalPagar.ToString("C2")
    End Sub

    ' --- EVENTOS DE TECLADO ---
    ' 1. De Medicamento salta a Precio
    Private Sub cmb_Medicamentos_KeyDown(sender As Object, e As KeyEventArgs) Handles cmb_Medicamentos.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            txt_Precio.Focus() '  Modificado: Ahora salta a la caja de precio
        End If
    End Sub

    ' 2. De Precio salta a Cantidad (NUEVO EVENTO)
    Private Sub txt_Precio_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_Precio.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            NumericUpDown1.Focus() '  Salta al control numérico (usando tu nombre actual)
        End If
    End Sub

    ' 3. De Cantidad agrega a la tabla y regresa a Medicamento
    Private Sub NumericUpDown1_KeyDown(sender As Object, e As KeyEventArgs) Handles NumericUpDown1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btn_Añadir.PerformClick() ' Da clic al botón de agregar
            cmb_Medicamentos.Focus() ' Regresa arriba para capturar el siguiente
        End If
    End Sub

    ' --- ELIMINAR DEL CARRITO ---
    Private Sub dgv_Compra_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Compra.CellContentClick
        If e.RowIndex < 0 Then Return

        If dgv_Compra.Columns(e.ColumnIndex).Name = "colEliminar" Then
            carritoEdicion.RemoveAt(e.RowIndex)
            ActualizarTotales() '  Recalcular al borrar
        End If
    End Sub

    ' --- CARGAS ---
    Private Async Function CargarProveedores() As Task
        Try
            Dim respuesta = Await clienteHttp.GetAsync("api/Proveedores/lista")
            Dim json As String = Await respuesta.Content.ReadAsStringAsync()

            If respuesta.IsSuccessStatusCode Then
                Dim lista = JsonSerializer.Deserialize(Of List(Of ProveedorDTO))(json, jsonOpciones)
                cmb_Proveedores.DataSource = lista
                cmb_Proveedores.DisplayMember = "nombre_Proveedor"
                cmb_Proveedores.ValueMember = "id_Proveedor"
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(json).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = json
                End Try
                MessageBox.Show("No se pudieron cargar los proveedores." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function CargarMedicamentos() As Task
        Try
            Dim respuesta = Await clienteHttp.GetAsync("api/Compras/medicamentos")
            Dim json As String = Await respuesta.Content.ReadAsStringAsync()

            If respuesta.IsSuccessStatusCode Then
                Dim lista = JsonSerializer.Deserialize(Of List(Of MedicamentoCombo))(json, jsonOpciones)
                cmb_Medicamentos.DataSource = lista
                cmb_Medicamentos.DisplayMember = "Nombre"
                cmb_Medicamentos.ValueMember = "Id"
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(json).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = json
                End Try
                MessageBox.Show("No se pudo cargar el catálogo de medicamentos." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function CargarDatosCompra() As Task
        Try
            Dim respuesta = Await clienteHttp.GetAsync($"api/Compras/{idCompra}")
            Dim json As String = Await respuesta.Content.ReadAsStringAsync()

            If respuesta.IsSuccessStatusCode Then
                Dim doc = JsonDocument.Parse(json).RootElement

                If doc.TryGetProperty("idProveedor", Nothing) Then
                    cmb_Proveedores.SelectedValue = doc.GetProperty("idProveedor").GetInt32()
                End If

                If doc.TryGetProperty("detalles", Nothing) Then
                    carritoEdicion.Clear()
                    Dim arrayDetalles = doc.GetProperty("detalles").EnumerateArray()

                    For Each item In arrayDetalles
                        Dim cant = item.GetProperty("cant").GetInt32()
                        Dim pUnit = item.GetProperty("p_Unit").GetDecimal()

                        carritoEdicion.Add(New FilaCarrito With {
                            .IdProducto = item.GetProperty("idProducto").GetInt32(),
                            .Producto = item.GetProperty("producto").GetString(),
                            .Cant = cant,
                            .P_Unit = pUnit,
                            .Subtotal = cant * pUnit
                        })
                    Next
                End If

                ActualizarTotales() '  Calcular totales iniciales
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "La compra no existe o fue eliminada."
                Try
                    Dim errorData = JsonDocument.Parse(json).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = json
                End Try
                MessageBox.Show("No se pudo cargar la información de esta compra." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Function

    '  FORMATO VISUAL DEL CARRITO (ComprasUpdate)
    Private Sub dgv_Compra_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_Compra.DataBindingComplete
        If dgv_Compra.Columns.Contains("IdProducto") Then
            dgv_Compra.Columns("IdProducto").Visible = False
        End If

        ' Le damos formato de moneda a las columnas de dinero
        If dgv_Compra.Columns.Contains("P_Unit") Then
            dgv_Compra.Columns("P_Unit").DefaultCellStyle.Format = "C2"
            dgv_Compra.Columns("P_Unit").HeaderText = "P. Unitario" ' Opcional: para que se vea mejor el título
        End If

        If dgv_Compra.Columns.Contains("Subtotal") Then
            dgv_Compra.Columns("Subtotal").DefaultCellStyle.Format = "C2"
        End If

        ' Asegurar posición del botón eliminar
        If dgv_Compra.Columns.Contains("colEliminar") Then
            dgv_Compra.Columns("colEliminar").DisplayIndex = dgv_Compra.Columns.Count - 1
        End If
    End Sub

    ' --- AÑADIR ---
    Private Sub btn_Añadir_Click(sender As Object, e As EventArgs) Handles btn_Añadir.Click
        If cmb_Medicamentos.SelectedItem Is Nothing Then
            MessageBox.Show("Por favor, selecciona un medicamento.")
            Return
        End If

        Dim cantidad = Convert.ToInt32(NumericUpDown1.Value)
        If cantidad <= 0 Then
            MessageBox.Show("La cantidad debe ser mayor a cero.")
            Return
        End If

        '  1. VALIDAMOS EL PRECIO DE COMPRA (TextBox)
        Dim precioIngresado As Decimal = 0
        If Not Decimal.TryParse(txt_Precio.Text, precioIngresado) OrElse precioIngresado <= 0 Then
            MessageBox.Show("Por favor, ingresa un precio de compra válido.", "Error de Precio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txt_Precio.Focus()
            Return
        End If

        Dim medSeleccionado = DirectCast(cmb_Medicamentos.SelectedItem, MedicamentoCombo)
        Dim itemExistente = carritoEdicion.FirstOrDefault(Function(x) x.IdProducto = medSeleccionado.Id)

        If itemExistente IsNot Nothing Then
            itemExistente.Cant += cantidad
            '  Actualizamos al nuevo precio ingresado
            itemExistente.P_Unit = precioIngresado
            itemExistente.Subtotal = itemExistente.Cant * itemExistente.P_Unit
            carritoEdicion.ResetBindings()
        Else
            Dim nuevaFila As New FilaCarrito With {
                .IdProducto = medSeleccionado.Id,
                .Producto = medSeleccionado.Nombre,
                .Cant = cantidad,
                .P_Unit = precioIngresado, '  Usamos el precio del TextBox
                .Subtotal = cantidad * precioIngresado
            }
            carritoEdicion.Add(nuevaFila)
        End If

        ActualizarTotales() ' Recalcular al añadir
        NumericUpDown1.Value = 1
        txt_Precio.Text = "" '  Limpiamos la caja de precio
        cmb_Medicamentos.SelectedIndex = -1
        cmb_Medicamentos.Text = ""
        cmb_Medicamentos.Focus() ' Regresa el cursor ahí automáticamente
    End Sub

    ' --- GUARDAR ---
    Private Async Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cmb_Proveedores.SelectedValue Is Nothing Then
            MessageBox.Show("Seleccione un proveedor válido.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If carritoEdicion.Count = 0 Then
            MessageBox.Show("El carrito no puede estar vacío. Agrega al menos un medicamento.", "Carrito Vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Calculamos el total real (el IVA ya está incluido en los precios unitarios)
        Dim sumaTotalPagar = carritoEdicion.Sum(Function(f) f.Subtotal)

        Dim request = New With {
            .IdProveedor = Convert.ToInt32(cmb_Proveedores.SelectedValue),
            .TotalCompra = sumaTotalPagar, ' Mandamos el total definitivo
            .Detalles = carritoEdicion.Select(Function(f) New With {
                .IdMedicamento = f.IdProducto,
                .Cantidad = f.Cant,
                .PrecioUnitario = f.P_Unit
            }).ToList
        }

        Try
            Dim jsonBody = JsonSerializer.Serialize(request)
            Dim contenido As New StringContent(jsonBody, Encoding.UTF8, "application/json")

            btnGuardar.Enabled = False
            btnGuardar.Text = "Actualizando..."

            Dim respuesta = Await clienteHttp.PutAsync($"api/Compras/{idCompra}", contenido)
            Dim responseBody = Await respuesta.Content.ReadAsStringAsync()

            If respuesta.IsSuccessStatusCode Then
                MessageBox.Show("¡Compra actualizada y stock ajustado con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DialogResult = DialogResult.OK
                Close()
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudo actualizar la compra." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnGuardar.Enabled = True
            btnGuardar.Text = "ACTUALIZAR COMPRA" ' Ajusta esto al texto original de tu botón
        End Try
    End Sub

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    ' EVENTO: AUTOCOMPLETAR EL ÚLTIMO PRECIO DE COMPRA
    Private Sub cmb_Medicamentos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Medicamentos.SelectedIndexChanged
        If cmb_Medicamentos.SelectedItem IsNot Nothing AndAlso TypeOf cmb_Medicamentos.SelectedItem Is MedicamentoCombo Then

            Dim medSeleccionado = DirectCast(cmb_Medicamentos.SelectedItem, MedicamentoCombo)

            ' Si el backend encontró un historial de compra (precio > 0), lo auto-escribe en la caja
            If medSeleccionado.Precio > 0 Then
                txt_Precio.Text = medSeleccionado.Precio.ToString("0.00")
            Else
                ' Si es un medicamento nuevo que jamás se ha comprado, lo dejamos en blanco
                txt_Precio.Text = ""
            End If

        End If
    End Sub
End Class