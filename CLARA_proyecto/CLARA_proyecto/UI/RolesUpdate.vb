Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class RolesUpdate
    Private idRolSeleccionado As Integer
    Private nombreOriginal As String
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://localhost:5133/api/roles" ' <-- Ajusta tu IP/Puerto

    ' --- CONSTRUCTOR ---
    ' Recibimos el ID y el Nombre desde la pantalla principal
    Public Sub New(idRol As Integer, nombre As String)
        InitializeComponent()
        idRolSeleccionado = idRol
        nombreOriginal = nombre
    End Sub

    ' --- AL ABRIR LA PANTALLA ---
    Private Async Sub RolesUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' 1. Ponemos el nombre actual en la cajita de texto
        txt_nombre.Text = nombreOriginal

        ' 2. Llenamos la cajita con todos los permisos
        Await CargarTodosLosPermisos()

        ' 3. Palomeamos solo los que tiene este rol actualmente
        Await CargarPermisosDelRol()
    End Sub

    Private Async Function CargarTodosLosPermisos() As Task
        Try
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/permisos")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

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

    Private Async Function CargarPermisosDelRol() As Task
        Try
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/{idRolSeleccionado}/permisos")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim permisosDelRol = JsonSerializer.Deserialize(Of List(Of Integer))(responseBody)

                ' Palomeamos las opciones que coincidan
                For i As Integer = 0 To clb_permisos.Items.Count - 1
                    Dim permisoItem As PermisoVB = CType(clb_permisos.Items(i), PermisoVB)
                    If permisosDelRol.Contains(permisoItem.IdPermiso) Then
                        clb_permisos.SetItemChecked(i, True)
                    End If
                Next
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudieron cargar los permisos actuales del rol." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    ' --- BOTÓN MODIFICAR ---
    Private Async Sub btn_modificar_Click(sender As Object, e As EventArgs) Handles btn_modificar.Click
        If String.IsNullOrWhiteSpace(txt_nombre.Text) Then
            MessageBox.Show("El nombre del rol no puede estar vacío.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        btn_modificar.Enabled = False
        btn_modificar.Text = "Guardando..."

        Try
            ' 1. Mandamos a actualizar el NOMBRE (Petición PUT)
            Dim rolUpdate = New With {.Nombre = txt_nombre.Text.Trim()}
            Dim jsonRol As String = JsonSerializer.Serialize(rolUpdate)
            Dim contentRol As New StringContent(jsonRol, Encoding.UTF8, "application/json")

            Dim responseRol = Await clienteHttp.PutAsync($"{urlBase}/{idRolSeleccionado}", contentRol)
            Dim responseBodyRol = Await responseRol.Content.ReadAsStringAsync()

            If responseRol.IsSuccessStatusCode Then

                ' 2. Si el nombre se guardó bien, mandamos a actualizar los PERMISOS (Petición POST)
                Dim permisosSeleccionados As New List(Of Integer)
                For Each itemChecked In clb_permisos.CheckedItems
                    Dim permiso As PermisoVB = CType(itemChecked, PermisoVB)
                    permisosSeleccionados.Add(permiso.IdPermiso)
                Next

                Dim requestPermisos = New With {.PermisosIds = permisosSeleccionados}
                Dim jsonPermisos = JsonSerializer.Serialize(requestPermisos)
                Dim contentPermisos = New StringContent(jsonPermisos, Encoding.UTF8, "application/json")

                Dim responsePermisos = Await clienteHttp.PostAsync($"{urlBase}/{idRolSeleccionado}/permisos", contentPermisos)
                Dim responseBodyPermisos = Await responsePermisos.Content.ReadAsStringAsync()

                If responsePermisos.IsSuccessStatusCode Then
                    MessageBox.Show("Rol modificado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close() ' Cerramos para que se recargue la tabla de atrás
                Else
                    '  ATRAPAMOS EL ERROR SI FALLA LA ASIGNACIÓN DE PERMISOS
                    Dim errorPermisosMsg As String = "Error desconocido al asignar permisos."
                    Try
                        Dim errorData = JsonDocument.Parse(responseBodyPermisos).RootElement
                        If errorData.TryGetProperty("error", Nothing) Then errorPermisosMsg = errorData.GetProperty("error").GetString()
                    Catch
                        errorPermisosMsg = responseBodyPermisos
                    End Try
                    MessageBox.Show("El nombre se actualizó, pero hubo un error al guardar los permisos." & vbCrLf & "Motivo: " & errorPermisosMsg, "Aviso Parcial", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            Else
                ' 3. Atrapamos el error del backend para la actualización del nombre (Ej. Nombre ya en uso)
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

                MessageBox.Show("No se pudo actualizar el rol." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_modificar.Enabled = True
            btn_modificar.Text = "MODIFICAR"
        End Try
    End Sub
End Class