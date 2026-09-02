Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class RolesCreate
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/roles" ' <-- Ajusta tu IP/Puerto si es necesario

    ' --- AL ABRIR LA PANTALLA ---
    Private Async Sub RolesCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' Llenamos la cajita con todos los permisos disponibles
        Await CargarTodosLosPermisos()
    End Sub

    Private Async Function CargarTodosLosPermisos() As Task
        Try
            Dim response As HttpResponseMessage = Await clienteHttp.GetAsync($"{urlBase}/permisos")
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim permisos = JsonSerializer.Deserialize(Of List(Of PermisoVB))(responseBody, opciones)

                clb_permisos.DataSource = permisos
                clb_permisos.DisplayMember = "Nombre"
                clb_permisos.ValueMember = "IdPermiso"
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudo cargar el catálogo de permisos." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close() ' Solo cerramos la ventana
    End Sub

    ' --- BOTÓN CREAR ROL ---
    Private Async Sub btn_crear_Click(sender As Object, e As EventArgs) Handles btn_crear.Click
        ' 1. Validamos que el nombre no esté vacío
        If String.IsNullOrWhiteSpace(txt_nombre.Text) Then
            MessageBox.Show("Por favor ingresa un nombre para el rol.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        btn_crear.Enabled = False
        btn_crear.Text = "Guardando..."

        Try
            ' 2. Mandamos a crear el Rol (Solo el nombre)
            Dim rolNuevo = New With {.Nombre = txt_nombre.Text.Trim()}
            Dim jsonRol As String = JsonSerializer.Serialize(rolNuevo)
            Dim contentRol As New StringContent(jsonRol, Encoding.UTF8, "application/json")

            Dim responseRol = Await clienteHttp.PostAsync(urlBase, contentRol)
            Dim responseBodyRol = Await responseRol.Content.ReadAsStringAsync()

            If responseRol.IsSuccessStatusCode Then
                ' 3. Extraemos el ID del nuevo rol que nos devolvió C#
                Dim nuevoIdRol As Integer = 0
                Using doc = JsonDocument.Parse(responseBodyRol)
                    nuevoIdRol = doc.RootElement.GetProperty("idRol").GetInt32()
                End Using

                ' 4. Recolectamos las palomitas seleccionadas
                Dim permisosSeleccionados As New List(Of Integer)
                For Each itemChecked In clb_permisos.CheckedItems
                    Dim permiso As PermisoVB = CType(itemChecked, PermisoVB)
                    permisosSeleccionados.Add(permiso.IdPermiso)
                Next

                ' 5. Le asignamos esos permisos al NUEVO ID (si es que seleccionó alguno)
                If permisosSeleccionados.Count > 0 Then
                    Dim requestPermisos = New With {.PermisosIds = permisosSeleccionados}
                    Dim jsonPermisos = JsonSerializer.Serialize(requestPermisos)
                    Dim contentPermisos = New StringContent(jsonPermisos, Encoding.UTF8, "application/json")

                    Dim responsePermisos = Await clienteHttp.PostAsync($"{urlBase}/{nuevoIdRol}/permisos", contentPermisos)
                    Dim responseBodyPermisos = Await responsePermisos.Content.ReadAsStringAsync()

                    If Not responsePermisos.IsSuccessStatusCode Then
                        '  ATRAPAMOS EL ERROR SI FALLA LA ASIGNACIÓN DE PERMISOS
                        Dim errorPermisosMsg As String = "Error desconocido al asignar permisos."
                        Try
                            Dim errorData = JsonDocument.Parse(responseBodyPermisos).RootElement
                            If errorData.TryGetProperty("error", Nothing) Then errorPermisosMsg = errorData.GetProperty("error").GetString()
                        Catch
                            errorPermisosMsg = responseBodyPermisos
                        End Try
                        MessageBox.Show("El rol se creó, pero hubo un error al asignar los permisos." & vbCrLf & "Motivo: " & errorPermisosMsg, "Aviso Parcial", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If

                MessageBox.Show("Rol creado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close() ' Cerramos la pantalla
            Else
                '  ATRAPAMOS EL ERROR AL CREAR EL ROL (Ej. "Ya existe un rol con este nombre")
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBodyRol).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then
                        errorMsg = errorData.GetProperty("error").GetString()
                    ElseIf errorData.TryGetProperty("message", Nothing) Then
                        errorMsg = errorData.GetProperty("message").GetString()
                    End If
                Catch
                    errorMsg = responseBodyRol
                End Try
                MessageBox.Show("No se pudo crear el rol." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_crear.Enabled = True
            btn_crear.Text = "CREAR ROL"
        End Try
    End Sub
End Class