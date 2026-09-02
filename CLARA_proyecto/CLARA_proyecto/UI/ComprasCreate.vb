Imports System.Net.Http
Imports System.Text.Json
Imports System.ComponentModel
Imports System.Text
Imports System.Linq '  ¡MUY IMPORTANTE para que funcione el Sum y el FirstOrDefault!

Public Class ComprasCreate
    Private clienteHttp As HttpClient
    Private carritoCompras As New BindingList(Of FilaCarrito)
    Private jsonOpciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}

    Private Async Sub ComprasCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lb_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy")
        ' 1. CONFIGURACIÓN DEL CLIENTE
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)
        clienteHttp.BaseAddress = New Uri("http://54.89.200.65:5133/")

        ' 2. DISEÑO Y ESTILO DE LA TABLA
        dgv_Carrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_Carrito.AllowUserToAddRows = False
        dgv_Carrito.RowHeadersVisible = False
        dgv_Carrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_Carrito.DataSource = carritoCompras
        dgv_Carrito.BackgroundColor = Color.White

        ' 3. CONFIGURACIÓN DE AUTOCOMPLETADO
        cmb_Proveedores.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_Proveedores.AutoCompleteSource = AutoCompleteSource.ListItems
        cmb_Medicamentos.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmb_Medicamentos.AutoCompleteSource = AutoCompleteSource.ListItems

        ' 4. AGREGAR BOTÓN DE ELIMINAR (Basurero)
        If Not dgv_Carrito.Columns.Contains("colEliminar") Then
            Dim btnDel As New DataGridViewButtonColumn() With {
                .Name = "colEliminar", .HeaderText = "", .Text = "🗑️",
                .UseColumnTextForButtonValue = True, .Width = 35, .FlatStyle = FlatStyle.Flat,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            }
            dgv_Carrito.Columns.Add(btnDel)
        End If

        ' 5. CARGAR CATÁLOGOS
        Await CargarProveedores()
        Await CargarBuscadorMedicamentos()
    End Sub

    ' --- CARGAS DE CATÁLOGOS ---
    Private Async Function CargarProveedores() As Task
        Try
            Dim respuesta = Await clienteHttp.GetAsync("api/Proveedores/lista")
            Dim json As String = Await respuesta.Content.ReadAsStringAsync()

            If respuesta.IsSuccessStatusCode Then
                Dim listaProveedores = JsonSerializer.Deserialize(Of List(Of ProveedorDTO))(json, jsonOpciones)

                cmb_Proveedores.DataSource = listaProveedores
                cmb_Proveedores.DisplayMember = "nombre_Proveedor"
                cmb_Proveedores.ValueMember = "id_Proveedor"
                cmb_Proveedores.SelectedIndex = -1 ' Empieza vacío para forzar selección
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

    Private Async Function CargarBuscadorMedicamentos() As Task
        Try
            Dim respuesta = Await clienteHttp.GetAsync("api/Compras/medicamentos")
            Dim json As String = Await respuesta.Content.ReadAsStringAsync()

            If respuesta.IsSuccessStatusCode Then
                Dim listaMed = JsonSerializer.Deserialize(Of List(Of MedicamentoCombo))(json, jsonOpciones)

                cmb_Medicamentos.DataSource = listaMed
                cmb_Medicamentos.DisplayMember = "Nombre"
                cmb_Medicamentos.ValueMember = "Id"
                cmb_Medicamentos.SelectedIndex = -1
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

    ' --- EVENTOS DE TECLADO (CAPTURAR CON ENTER) ---
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
            nud_cantidad.Focus() '  Salta al control numérico
        End If
    End Sub

    ' 3. De Cantidad agrega a la tabla y regresa a Medicamento
    Private Sub nud_cantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles nud_cantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btn_Añadir.PerformClick() ' Da clic al botón de agregar
            cmb_Medicamentos.Focus() ' Regresa arriba para capturar el siguiente
        End If
    End Sub

    ' --- LÓGICA DEL CARRITO ---
    Private Sub btn_Añadir_Click(sender As Object, e As EventArgs) Handles btn_Añadir.Click
        If cmb_Medicamentos.SelectedItem Is Nothing Then
            MessageBox.Show("Selecciona un producto válido.", "Aviso")
            Return
        End If

        Dim cantidad = Convert.ToInt32(nud_cantidad.Value)
        If cantidad <= 0 Then
            MessageBox.Show("La cantidad debe ser mayor a cero.")
            Return
        End If

        '  VALIDAMOS EL PRECIO QUE ESCRIBIÓ EL USUARIO
        Dim precioIngresado As Decimal = 0
        If Not Decimal.TryParse(txt_Precio.Text, precioIngresado) OrElse precioIngresado <= 0 Then
            MessageBox.Show("Por favor, ingresa un precio de compra válido.", "Error de Precio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txt_Precio.Focus()
            Return
        End If

        Dim medSeleccionado = DirectCast(cmb_Medicamentos.SelectedItem, MedicamentoCombo)
        Dim existente = carritoCompras.FirstOrDefault(Function(x) x.IdProducto = medSeleccionado.Id)

        If existente IsNot Nothing Then
            existente.Cant += cantidad
            '  El subtotal usa el precio ingresado, o actualiza el precio si cambió
            existente.P_Unit = precioIngresado
            existente.Subtotal = existente.Cant * existente.P_Unit
            carritoCompras.ResetBindings()
        Else
            carritoCompras.Add(New FilaCarrito With {
                .IdProducto = medSeleccionado.Id,
                .Producto = medSeleccionado.Nombre,
                .Cant = cantidad,
                .P_Unit = precioIngresado, '  Usamos el precio del TextBox
                .Subtotal = cantidad * precioIngresado
            })
        End If

        ActualizarTotales()

        ' Limpiamos para el siguiente producto
        nud_cantidad.Value = 1
        txt_Precio.Text = ""
        cmb_Medicamentos.SelectedIndex = -1
        cmb_Medicamentos.Text = ""
        cmb_Medicamentos.Focus()
    End Sub

    '  FORMATO VISUAL DEL CARRITO (ComprasCreate)
    Private Sub dgv_Carrito_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgv_Carrito.DataBindingComplete
        If dgv_Carrito.Columns.Contains("IdProducto") Then
            dgv_Carrito.Columns("IdProducto").Visible = False
        End If

        ' Le damos formato de moneda a las columnas de dinero
        If dgv_Carrito.Columns.Contains("P_Unit") Then
            dgv_Carrito.Columns("P_Unit").DefaultCellStyle.Format = "C2"
            dgv_Carrito.Columns("P_Unit").HeaderText = "P. Unitario" ' Opcional: para que se vea mejor el título
        End If

        If dgv_Carrito.Columns.Contains("Subtotal") Then
            dgv_Carrito.Columns("Subtotal").DefaultCellStyle.Format = "C2"
        End If

        ' Aprovechamos para asegurar que el basurero siempre esté al final
        If dgv_Carrito.Columns.Contains("colEliminar") Then
            dgv_Carrito.Columns("colEliminar").DisplayIndex = dgv_Carrito.Columns.Count - 1
        End If
    End Sub

    Private Sub dgv_Carrito_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Carrito.CellContentClick
        If e.RowIndex < 0 Then Return
        If dgv_Carrito.Columns(e.ColumnIndex).Name = "colEliminar" Then
            carritoCompras.RemoveAt(e.RowIndex)
            ActualizarTotales()
        End If
    End Sub

    Private Sub ActualizarTotales()
        Dim sumaTotalPagar As Decimal = 0

        If carritoCompras.Count > 0 Then
            sumaTotalPagar = carritoCompras.Sum(Function(x) x.Subtotal)
        End If

        ' DESGLOSE INVERSO: El subtotal base dividiendo entre 1.16
        Dim subtotalBase As Decimal = sumaTotalPagar / 1.16D
        Dim ivaDesglosado As Decimal = sumaTotalPagar - subtotalBase

        lbl_subtotal.Text = subtotalBase.ToString("C2")
        lbl_iva.Text = ivaDesglosado.ToString("C2")
        lbl_total.Text = sumaTotalPagar.ToString("C2")
    End Sub

    ' --- GUARDAR COMPRA ---
    Private Async Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If carritoCompras.Count = 0 OrElse cmb_Proveedores.SelectedValue Is Nothing Then
            MessageBox.Show("Verifica que haya productos y un proveedor seleccionado.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Desactivar botón para no guardar dos veces
        btnGuardar.Enabled = False
        btnGuardar.Text = "Guardando..."

        ' El total final (Subtotal + IVA)
        Dim sumaTotalPagar = carritoCompras.Sum(Function(x) x.Subtotal)

        Dim datosCompra = New With {
            .IdProveedor = Convert.ToInt32(cmb_Proveedores.SelectedValue),
            .TotalCompra = sumaTotalPagar,
            .Detalles = carritoCompras.Select(Function(x) New With {
                .IdMedicamento = x.IdProducto,
                .Cantidad = x.Cant,
                .PrecioUnitario = x.P_Unit
            }).ToList
        }

        Try
            Dim jsonString = JsonSerializer.Serialize(datosCompra)
            Dim contenido As New StringContent(jsonString, Encoding.UTF8, "application/json")

            Dim respuesta = Await clienteHttp.PostAsync("api/Compras/registrar", contenido)
            Dim responseBody = Await respuesta.Content.ReadAsStringAsync()

            If respuesta.IsSuccessStatusCode Then
                MessageBox.Show("¡Compra registrada y stock actualizado con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '  CRÍTICO: Avisarle a Compras.vb que todo salió bien
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
                MessageBox.Show("No se pudo guardar la compra." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnGuardar.Enabled = True
            btnGuardar.Text = "GUARDAR COMPRA" ' Cambia esto si tu botón originalmente decía otra cosa
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