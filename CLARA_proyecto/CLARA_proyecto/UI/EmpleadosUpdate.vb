Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class EmpleadosUpdate
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/empleados" ' Ajusta tu puerto

    ' VARIABLES PÚBLICAS PARA RECIBIR LOS DATOS DESDE LA TABLA
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property EmpleadoId As Integer

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property NombreActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property ApPaternoActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property ApMaternoActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property TelefonoActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property EmailActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property FechaNacActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property RolActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property GeneroActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property CedulaActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property EspecialidadActual As String

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property EstatusActual As String = "Activo"

    '  VARIABLE PARA RASTREAR LA CONTRASEÑA FALSA
    Private passwordEnmascarada As String = "sfjgdksaofhdd" ' Puede ser cualquier cosa, solo para llenar el campo

    ' --- AL CARGAR LA PANTALLA ---
    Private Async Sub EmpleadosUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' 1. Configurar los ojitos de la contraseña
        txt_password.UseSystemPasswordChar = True

        '  MODIFICACIÓN: Ponemos la contraseña falsa y bloqueamos los ojitos al inicio
        txt_password.Text = passwordEnmascarada
        pbx_ver.Visible = True
        pbx_ver.Enabled = False ' Bloqueamos el clic
        pbx_ocultar.Visible = False

        ' 2. Cargar los catálogos (Roles, Géneros, Estatus)
        Await CargarCatalogos()

        ' 3.  LLENAR LOS CAMPOS CON LOS DATOS DEL EMPLEADO
        txt_nombre.Text = NombreActual
        txt_apPaterno.Text = ApPaternoActual
        txt_apMaterno.Text = ApMaternoActual
        txt_telefono.Text = TelefonoActual
        txt_email.Text = EmailActual

        ' Intentamos convertir el texto de la fecha al DateTimePicker
        Try
            dtp_fechaNac.Value = DateTime.Parse(FechaNacActual)
        Catch ex As Exception
            dtp_fechaNac.Value = DateTime.Now
        End Try

        ' Seleccionamos los valores de los ComboBox según el texto que traemos de la tabla
        cmb_rol.SelectedIndex = cmb_rol.FindStringExact(RolActual)
        cmb_genero.SelectedIndex = cmb_genero.FindStringExact(GeneroActual)
        cmb_estatus.SelectedIndex = cmb_estatus.FindStringExact(EstatusActual) ' Por defecto asumimos activo
    End Sub

    '  NUEVO EVENTO: Detectar si borran la contraseña falsa
    Private Sub txt_password_TextChanged(sender As Object, e As EventArgs) Handles txt_password.TextChanged
        ' Si el campo queda totalmente en blanco (el usuario borró la contraseña falsa)
        If String.IsNullOrWhiteSpace(txt_password.Text) Then
            pbx_ver.Enabled = True ' Reactivamos el ojito para que funcione normal
        End If
    End Sub

    Private Async Function CargarCatalogos() As Task
        Try
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/catalogos")
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim catalogos = JsonSerializer.Deserialize(Of CatalogosResponseVB)(responseBody, opciones)

                cmb_rol.DataSource = catalogos.Roles
                cmb_rol.DisplayMember = "Nombre"
                cmb_rol.ValueMember = "Id"

                cmb_genero.DataSource = catalogos.Generos
                cmb_genero.DisplayMember = "Nombre"
                cmb_genero.ValueMember = "Id"

                cmb_estatus.DataSource = catalogos.Estatus
                cmb_estatus.DisplayMember = "Nombre"
                cmb_estatus.ValueMember = "Id"
            Else
                '  ATRAPAMOS EL ERROR DEL BACKEND
                Dim errorMsg As String = "Error desconocido del servidor."
                Try
                    Dim errorData = JsonDocument.Parse(responseBody).RootElement
                    If errorData.TryGetProperty("error", Nothing) Then errorMsg = errorData.GetProperty("error").GetString()
                Catch
                    errorMsg = responseBody
                End Try
                MessageBox.Show("No se pudieron cargar las listas desplegables." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' --- BOTÓN CANCELAR ---
    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    ' --- BOTÓN ACTUALIZAR ---
    Private Async Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click

        ' 1. Validaciones básicas
        If String.IsNullOrWhiteSpace(txt_nombre.Text) OrElse String.IsNullOrWhiteSpace(txt_apPaterno.Text) OrElse
           String.IsNullOrWhiteSpace(txt_email.Text) OrElse String.IsNullOrWhiteSpace(txt_telefono.Text) OrElse
           cmb_rol.SelectedIndex = -1 OrElse cmb_genero.SelectedIndex = -1 OrElse cmb_estatus.SelectedIndex = -1 Then

            MessageBox.Show("Por favor, llena todos los campos obligatorios (la contraseña es opcional si no deseas cambiarla).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim patron As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Dim regex As New Regex(patron)
        If Not regex.IsMatch(txt_email.Text) Then
            MessageBox.Show("Ingresa un formato de correo válido.", "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validar contraseña SOLO SI ESCRIBIERON ALGO NUEVO Y NO ES LA FALSA
        If Not String.IsNullOrWhiteSpace(txt_password.Text) AndAlso txt_password.Text <> passwordEnmascarada Then
            Dim regexSeguridad As New Regex("^(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$")
            If Not regexSeguridad.IsMatch(txt_password.Text) Then
                MessageBox.Show("La nueva contraseña debe tener mínimo 8 caracteres, una mayúscula y un número.", "Contraseña Débil", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        ' AGREGAMOS LA VALIDACIÓN DE EDAD Y FECHA QUE TENÍAMOS EN EL CREATE
        If dtp_fechaNac.Value.Date > DateTime.Now.Date Then
            MessageBox.Show("La fecha de nacimiento no puede estar en el futuro.", "Fecha no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim edad As Integer = DateTime.Now.Year - dtp_fechaNac.Value.Year
        If DateTime.Now.Date < dtp_fechaNac.Value.AddYears(edad) Then edad -= 1

        If edad < 18 Then
            MessageBox.Show("El empleado debe ser mayor de 18 años.", "Edad no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. INTERCEPTAR SI SE CONVIRTIÓ EN MÉDICO (o si ya lo era)
        Dim cedulaStr As String = ""
        Dim especialidadStr As String = ""

        If cmb_rol.Text.ToLower().Contains("medico") OrElse cmb_rol.Text.ToLower().Contains("médico") Then
            Dim formMedico As New DatosMedico()

            ' Precargamos los datos en la ventanita por si solo quiere editarlos
            formMedico.txt_cedula.Text = CedulaActual
            formMedico.txt_especialidad.Text = EspecialidadActual

            If formMedico.ShowDialog() = DialogResult.OK Then
                cedulaStr = formMedico.Cedula
                especialidadStr = formMedico.Especialidad
            Else
                Return ' Si cancela la ventanita, abortamos la actualización
            End If
        End If

        ' 3. GUARDAR LOS DATOS
        btn_guardar.Enabled = False
        btn_guardar.Text = "Actualizando..."

        Try
            '  LIMPIAR CONTRASEÑA: Si mandan la falsa, la borramos para que el backend no actualice nada
            Dim passParaEnviar As String = txt_password.Text.Trim()
            If passParaEnviar = passwordEnmascarada Then
                passParaEnviar = ""
            End If

            '  USAMOS TU MODELO FORMAL EN LUGAR DE UN OBJETO ANÓNIMO
            Dim empleadoActualizado As New UsuarioRequestVB() With {
                .IdEstatus = Convert.ToInt32(cmb_estatus.SelectedValue),
                .IdGenero = Convert.ToInt32(cmb_genero.SelectedValue),
                .IdRol = Convert.ToInt32(cmb_rol.SelectedValue),
                .Nombre = txt_nombre.Text.Trim(),
                .ApellidoPaterno = txt_apPaterno.Text.Trim(),
                .ApellidoMaterno = txt_apMaterno.Text.Trim(),
                .Email = txt_email.Text.Trim(),
                .Password = passParaEnviar, '  AQUÍ VA LA VALIDACIÓN ANTERIOR
                .Telefono = txt_telefono.Text.Trim(),
                .FechaNacimiento = dtp_fechaNac.Value.ToString("yyyy-MM-dd"),
                .CedulaProfesional = cedulaStr,
                .Especialidad = especialidadStr
            }

            Dim jsonString As String = JsonSerializer.Serialize(empleadoActualizado)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            '  USAMOS PUT Y LE MANDAMOS EL ID EN LA URL
            Dim response = Await clienteHttp.PutAsync($"{urlBase}/{EmpleadoId}", content)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Empleado actualizado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                MessageBox.Show("No se pudo actualizar el empleado." & vbCrLf & "Motivo: " & errorMsg, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el servidor." & vbCrLf & "Verifique su conexión o contacte a soporte." & vbCrLf & "Detalle técnico: " & ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_guardar.Enabled = True
            btn_guardar.Text = "ACTUALIZAR"
        End Try
    End Sub

    ' --- EVENTOS UX (Enter, Solo números, Ojitos) ---
    Private Sub txt_telefono_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_telefono.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then e.Handled = True
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub NavegarConEnter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nombre.KeyPress, txt_apPaterno.KeyPress, txt_apMaterno.KeyPress, txt_email.KeyPress, txt_password.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub pbx_ver_MouseDown(sender As Object, e As MouseEventArgs) Handles pbx_ver.MouseDown
        '  Solo funciona si está habilitado
        If pbx_ver.Enabled Then
            txt_password.UseSystemPasswordChar = False
            pbx_ver.Visible = False
            pbx_ocultar.Visible = True
        End If
    End Sub

    Private Sub pbx_ver_MouseUp(sender As Object, e As MouseEventArgs) Handles pbx_ver.MouseUp, pbx_ocultar.MouseUp
        If pbx_ver.Enabled Then
            txt_password.UseSystemPasswordChar = True
            pbx_ver.Visible = True
            pbx_ocultar.Visible = False
        End If
    End Sub
End Class