Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class MedicamentosUpdate
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://localhost:5133/api/medicamentos"

    ' --- VARIABLES PÚBLICAS PARA RECIBIR LOS DATOS DE LA TABLA ---
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property MedicamentoId As Integer

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property NombrePuro As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property DescripcionPura As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property PrecioPuro As Decimal

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property ValorConcPuro As Decimal

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property UnidadConcPura As String

    ' Opcional: Recibir el stock actual solo para mostrarlo
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property StockActual As Integer

    ' --- AL ABRIR LA PANTALLA ---
    Private Sub MedicamentosUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' Llenamos las opciones del ComboBox si no lo hiciste en el diseño
        If cmb_unidad.Items.Count = 0 Then
            cmb_unidad.Items.AddRange(New String() {"mg", "g", "ml", "L", "UI", "mcg", "%"})
        End If

        '  1. LLENAMOS LOS CAMPOS CON LOS DATOS RECIBIDOS
        txt_nombre.Text = NombrePuro
        txt_concentracion.Text = ValorConcPuro.ToString("0.##") ' Evitamos ceros inútiles como 500.00
        cmb_unidad.Text = UnidadConcPura
        txt_descripcion.Text = DescripcionPura
        txt_precio.Text = PrecioPuro.ToString("0.00")
    End Sub

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    ' --- BOTÓN GUARDAR/ACTUALIZAR ---
    Private Async Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        ' 1. Validar campos vacíos
        If String.IsNullOrWhiteSpace(txt_nombre.Text) OrElse
           String.IsNullOrWhiteSpace(txt_concentracion.Text) OrElse
           String.IsNullOrWhiteSpace(cmb_unidad.Text) OrElse
           String.IsNullOrWhiteSpace(txt_precio.Text) OrElse
           String.IsNullOrWhiteSpace(txt_descripcion.Text) Then

            MessageBox.Show("Por favor, llena todos los campos.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Validar que la concentración y el precio sean números válidos
        Dim concentracion As Decimal = 0
        Dim precio As Decimal = 0

        ' Usamos InvariantCulture para que el punto (.) SIEMPRE sea detectado como decimal
        If Not Decimal.TryParse(txt_concentracion.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, concentracion) Then
            MessageBox.Show("La concentración debe ser un número válido (ej. 500 o 2.5).", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Decimal.TryParse(txt_precio.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, precio) Then
            MessageBox.Show("El precio debe ser un número válido.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Bloqueamos el botón
        btn_guardar.Enabled = False
        btn_guardar.Text = "Actualizando..."

        Try
            ' 3. Empaquetamos los datos (El Backend ignora el stock automáticamente)
            ' Asumimos que al editar, el estatus se mantiene como "Activo" (Id 1 por defecto en la BD o lo manejamos en el controlador)
            Dim medActualizado = New With {
                .IdEstatus = 1, ' Activo
                .Nombre = txt_nombre.Text.Trim(),
                .Descripcion = txt_descripcion.Text.Trim(),
                .Precio = precio,
                .ConcentracionValor = concentracion,
                .ConcentracionUnidad = cmb_unidad.Text.Trim()
            }

            Dim jsonString As String = JsonSerializer.Serialize(medActualizado)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            ' 4. Mandamos la petición PUT usando el ID del medicamento
            Dim response = Await clienteHttp.PutAsync($"{urlBase}/{MedicamentoId}", content)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            ' 5. Evaluamos la respuesta
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Medicamento actualizado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND (Ej. "Ya existe otro medicamento con ese nombre y concentración")
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then
                        errorMsg = errorData.GetProperty("error").GetString()
                    ElseIf errorData.TryGetProperty("message", Nothing) Then
                        errorMsg = errorData.GetProperty("message").GetString()
                    End If
                Catch
                    errorMsg = responseBody
                End Try

                MessageBox.Show(errorMsg, "No se pudo actualizar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_guardar.Enabled = True
            btn_guardar.Text = "GUARDAR"
        End Try
    End Sub

    ' --- UX: Evitar que escriban letras en el precio y concentración ---
    Private Sub txt_precio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_precio.KeyPress, txt_concentracion.KeyPress
        Dim cajaTexto As TextBox = CType(sender, TextBox)

        ' MEJORA DE UX: Si el usuario presiona una coma (,) por costumbre, la convertimos en punto (.) automáticamente
        If e.KeyChar = ","c Then
            e.KeyChar = "."c
        End If

        ' Permitir números, retroceso (borrar) y un solo punto decimal
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Evitar dos puntos decimales
        If e.KeyChar = "."c AndAlso cajaTexto.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub
End Class