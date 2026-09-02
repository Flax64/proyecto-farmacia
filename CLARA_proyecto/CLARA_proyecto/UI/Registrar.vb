Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class Registrar

    Private Sub lblk_login_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblk_login.LinkClicked
        Me.Close()
    End Sub

    Private Async Sub btn_registrar_Click(sender As Object, e As EventArgs) Handles btn_registrar.Click
        ' 1. Validaciones previas
        If String.IsNullOrWhiteSpace(txb_nombre.Text) OrElse
           String.IsNullOrWhiteSpace(txb_email.Text) OrElse
           String.IsNullOrWhiteSpace(txb_password.Text) OrElse
           String.IsNullOrWhiteSpace(txb_telefono.Text) OrElse
           String.IsNullOrWhiteSpace(txb_materno.Text) OrElse
           String.IsNullOrWhiteSpace(txb_paterno.Text) OrElse
           cbx_genero.SelectedIndex = -1 Then
            MessageBox.Show("Por favor, llena todos los campos obligatorios.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim patron As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Dim regex As New Regex(patron)
        If Not regex.IsMatch(txb_email.Text) Then
            MessageBox.Show("Por favor, ingresa un formato de correo válido (ejemplo@dominio.com).", "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If txb_password.Text.Length < 8 Then
            MessageBox.Show("La contraseña debe tener al menos 8 caracteres.", "Contraseña débil", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim edad As Integer = DateTime.Now.Year - dtpk_nacimiento.Value.Year
        If DateTime.Now.Date < dtpk_nacimiento.Value.AddYears(edad) Then edad -= 1

        If edad < 18 Then
            MessageBox.Show("Debes ser mayor de 18 años para registrarte en el sistema.", "Edad no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If dtpk_nacimiento.Value.Date > DateTime.Now.Date Then
            MessageBox.Show("La fecha de nacimiento no puede estar en el futuro.", "Fecha no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Empaquetar los datos
        Dim requestData As New RegistroRequestVB() With {
            .IdGenero = Convert.ToInt32(cbx_genero.SelectedValue),
            .IdEstatus = 1,
            .IdRol = 0,
            .Nombre = txb_nombre.Text.Trim(),
            .ApellidoPaterno = txb_paterno.Text.Trim(),
            .ApellidoMaterno = txb_materno.Text.Trim(),
            .Email = txb_email.Text.Trim(),
            .Password = txb_password.Text,
            .Telefono = txb_telefono.Text.Trim(),
            .FechaNacimiento = dtpk_nacimiento.Value.ToString("yyyy-MM-dd")
        }

        Dim jsonString As String = JsonSerializer.Serialize(requestData)
        Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

        ' 3. Enviar la petición a la API
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(sen, cert, chain, sslPolicyErrors) True

        ' IMPORTANTE: Le pasamos el manejador al cliente
        Using client As New HttpClient(manejador)
            Try
                btn_registrar.Enabled = False
                btn_registrar.Text = "Registrando..."

                Dim urlAPI As String = "http://54.89.200.65:5133/api/Registro/registar"
                Dim response As HttpResponseMessage = Await client.PostAsync(urlAPI, content)
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("¡Usuario registrado con éxito en el sistema!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
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
                        errorMsg = responseBody
                    End Try
                    MessageBox.Show("No se pudo completar el registro." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                btn_registrar.Enabled = True
                btn_registrar.Text = "Registrarse"
            End Try
        End Using
    End Sub

    Private Async Sub Registrar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txb_password.UseSystemPasswordChar = True
        pbx_ver.Visible = True
        pbx_ocultar.Visible = False

        cbx_genero.Enabled = False
        cbx_genero.Items.Clear()
        cbx_genero.Text = "Cargando..."

        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(sen, cert, chain, sslPolicyErrors) True

        Using client As New HttpClient(manejador)
            Try
                Dim urlAPI As String = "http://54.89.200.65:5133/api/Registro/generos"
                Dim response As HttpResponseMessage = Await client.GetAsync(urlAPI)
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                If response.IsSuccessStatusCode Then
                    Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                    Dim listaGeneros = JsonSerializer.Deserialize(Of List(Of GeneroVB))(responseBody, opciones)

                    cbx_genero.DataSource = listaGeneros
                    cbx_genero.DisplayMember = "NombreGenero"
                    cbx_genero.ValueMember = "IdGenero"
                    cbx_genero.SelectedIndex = -1
                    cbx_genero.Enabled = True
                Else
                    '  ATRAPAMOS EL ERROR DEL BACKEND AL CARGAR GÉNEROS
                    Dim errorMsg As String = "Error desconocido del servidor."
                    Try
                        Dim errorData = JsonDocument.Parse(responseBody).RootElement
                        If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                    Catch
                        errorMsg = responseBody
                    End Try
                    cbx_genero.Text = "Error al cargar"
                    MessageBox.Show("No se pudieron cargar los géneros." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                cbx_genero.Text = "Sin conexión"
                MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub txb_telefono_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_telefono.KeyPress
        ' Evaluamos si la tecla presionada NO es un dígito numérico (0-9) 
        ' Y también evaluamos si NO es una tecla de control (como el Retroceso/Backspace para borrar)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then

            ' Si entra aquí, significa que intentaron escribir una letra, un espacio o un símbolo.
            ' e.Handled = True le dice al sistema: "Yo ya me encargué de esto, cancela la tecla".
            e.Handled = True

        End If
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txb_nombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_nombre.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txb_paterno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_paterno.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txb_materno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_materno.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txb_email_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txb_email.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub pbx_ver_MouseDown(sender As Object, e As MouseEventArgs) Handles pbx_ver.MouseDown
        ' 1. REVELAMOS la contraseña
        txb_password.UseSystemPasswordChar = False

        ' 2. Intercambiamos los íconos
        pbx_ver.Visible = False
        pbx_ocultar.Visible = True
    End Sub

    ' --- 3. EVENTO AL SOLTAR EL CLIC SOBRE EL OJITO ABIERTO ---
    Private Sub pbx_ver_MouseUp(sender As Object, e As MouseEventArgs) Handles pbx_ver.MouseUp
        ' 1. VOLVEMOS A OCULTAR la contraseña
        txb_password.UseSystemPasswordChar = True

        ' 2. Restauramos los íconos originales
        pbx_ver.Visible = True
        pbx_ocultar.Visible = False
    End Sub
End Class