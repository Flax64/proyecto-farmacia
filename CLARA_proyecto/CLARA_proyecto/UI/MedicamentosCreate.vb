Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class MedicamentosCreate
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/medicamentos"

    Private Sub MedicamentosCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' Blindamos el stock visualmente
        txt_stock.Text = "0"
        txt_stock.ReadOnly = True

        ' Si no agregaste items al combo en el diseño, los agregamos aquí por defecto
        If cmb_unidad.Items.Count = 0 Then
            cmb_unidad.Items.AddRange(New String() {"mg", "g", "ml", "L"})
        End If
    End Sub

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

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
        btn_guardar.Text = "Guardando..."

        Try
            ' 3. Empaquetamos los datos (Fíjate que NO enviamos el stock, el backend lo pone en 0)
            Dim nuevoMed = New With {
                .Nombre = txt_nombre.Text.Trim(),
                .Descripcion = txt_descripcion.Text.Trim(),
                .Precio = precio,
                .ConcentracionValor = concentracion,
                .ConcentracionUnidad = cmb_unidad.Text.Trim()
            }

            Dim jsonString As String = JsonSerializer.Serialize(nuevoMed)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            ' 4. Mandamos la petición
            Dim response = Await clienteHttp.PostAsync(urlBase, content)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            ' 5. Evaluamos la respuesta
            If response.IsSuccessStatusCode Then
                ' Si se creó O si se reactivó uno viejo, C# nos manda un mensaje de éxito
                Dim msjExito As String = "Operación completada."
                Try
                    Using doc = JsonDocument.Parse(responseBody)
                        msjExito = doc.RootElement.GetProperty("message").GetString()
                    End Using
                Catch
                End Try

                MessageBox.Show(msjExito, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND (Ej. "Ya existe este medicamento")
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then
                        errorMsg = errorData.GetProperty("error").GetString()
                    End If
                Catch
                    errorMsg = responseBody
                End Try

                MessageBox.Show(errorMsg, "No se pudo registrar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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