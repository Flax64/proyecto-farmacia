Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions
Public Class EditarPerfil
    Private clienteHttp As HttpClient
    Private idUsuario As Integer

    Private nombreActual As String
    Private paternoActual As String
    Private maternoActual As String
    Private telefonoActual As String
    Private emailActual As String
    Private fechaNacimientoActual As Date
    Private generoActual As String

    Private Async Sub EditarPerfil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        Await CargarGeneros()
        Await CargarPerfilUsuario()
    End Sub
    Private Sub btn_actualizar_password_Click(sender As Object, e As EventArgs) Handles btn_actualizar_password.Click
        Dim cambiarPassword As New CambiarPassword3()
        cambiarPassword.ShowDialog()
    End Sub

    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    ' =======================================================
    ' 1. BOTÓN GUARDAR (ACTUALIZAR PERFIL)
    ' =======================================================
    Private Async Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        If String.IsNullOrWhiteSpace(txb_nombre.Text) OrElse
           String.IsNullOrWhiteSpace(txb_paterno.Text) OrElse
           String.IsNullOrWhiteSpace(txb_telefono.Text) OrElse
           cmb_genero.SelectedIndex = -1 Then

            MessageBox.Show("Por favor, llena todos los campos obligatorios (Nombre, Apellido Paterno, Teléfono y Género).", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not validarCambios() Then
            MessageBox.Show("No has realizado ningún cambio en tu perfil.", "Sin Cambios", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Return
        End If

        btn_guardar.Enabled = False
        btn_guardar.Text = "Guardando..."

        Try
            Dim idGeneroSeleccionado As Integer = Convert.ToInt32(cmb_genero.SelectedValue)

            Dim datosActualizados = New With {
                .Nombre = txb_nombre.Text.Trim(),
                .ApellidoP = txb_paterno.Text.Trim(),
                .ApellidoM = txb_materno.Text.Trim(),
                .Telefono = txb_telefono.Text.Trim(),
                .FechaNacimiento = dtpk_nacimiento.Value.ToString("yyyy-MM-dd"),
                .IdGenero = idGeneroSeleccionado
            }

            Dim jsonString As String = JsonSerializer.Serialize(datosActualizados)
            Dim content As New StringContent(jsonString, System.Text.Encoding.UTF8, "application/json")

            Dim urlAPI As String = $"http://localhost:5133/api/perfil/actualizar/{SesionGlobal.correo}"
            Dim response As HttpResponseMessage = Await clienteHttp.PutAsync(urlAPI, content)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("¡Tu perfil ha sido actualizado con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                MessageBox.Show("No se pudo actualizar el perfil." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_guardar.Enabled = True
            btn_guardar.Text = "GUARDAR"
        End Try
    End Sub

    ' =======================================================
    ' 2. CARGAR GÉNEROS
    ' =======================================================
    Private Async Function CargarGeneros() As Task
        Try
            Dim urlAPI As String = "http://localhost:5133/api/registro/generos"
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlAPI)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim listaGeneros = JsonSerializer.Deserialize(Of List(Of GeneroVB))(responseBody, opciones)

                cmb_genero.DataSource = listaGeneros
                cmb_genero.DisplayMember = "NombreGenero"
                cmb_genero.ValueMember = "IdGenero"
                cmb_genero.SelectedIndex = -1
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudieron cargar los géneros." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' =======================================================
    ' 3. CARGAR PERFIL DEL USUARIO
    ' =======================================================
    Private Async Function CargarPerfilUsuario() As Task
        Try
            Dim urlAPI As String = $"http://localhost:5133/api/perfil/{correo}"
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync(urlAPI)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim perfil = JsonSerializer.Deserialize(Of PerfilResponseVB)(responseBody, opciones)

                txb_nombre.Text = perfil.Nombre
                txb_paterno.Text = perfil.ApellidoP
                txb_materno.Text = perfil.ApellidoM
                txb_telefono.Text = perfil.Telefono
                txb_email.Text = perfil.Correo
                dtpk_nacimiento.Value = perfil.FechaNacimiento
                cmb_genero.Text = perfil.Genero

                nombreActual = perfil.Nombre
                paternoActual = perfil.ApellidoP
                maternoActual = perfil.ApellidoM
                telefonoActual = perfil.Telefono
                emailActual = perfil.Correo
                fechaNacimientoActual = perfil.FechaNacimiento
                generoActual = perfil.Genero
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Usuario no encontrado."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudo cargar la información del perfil." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Private Function validarCambios() As Boolean
        If nombreActual.Trim = txb_nombre.Text.Trim() AndAlso
           paternoActual.Trim = txb_paterno.Text.Trim() AndAlso
           maternoActual.Trim = txb_materno.Text.Trim() AndAlso
           telefonoActual.Trim = txb_telefono.Text.Trim() AndAlso
           emailActual.Trim = txb_email.Text.Trim() AndAlso
           fechaNacimientoActual = dtpk_nacimiento.Value.Date AndAlso
           generoActual.Trim = cmb_genero.Text Then
            Return False
        End If
        Return True
    End Function

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
End Class