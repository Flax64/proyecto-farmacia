Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Public Class RegistrarConsulta
    Private clienteHttp As HttpClient
    Private ReadOnly urlBase As String = "http://54.89.200.65:5133/api/consultas"

    '  Fíjate que aquí ya dice RegistrarConsulta_Load
    Private Async Sub RegistrarConsulta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim manejador As New HttpClientHandler()
        manejador.ServerCertificateCustomValidationCallback = Function(s, cert, chain, sslPolicyErrors) True
        clienteHttp = New HttpClient(manejador)

        ' 1. Preparamos la tabla visual de la receta
        ConfigurarTablaReceta()

        ' 2. Descargamos catálogos
        Await CargarCitasDelDia()
        Await CargarMedicamentos()
    End Sub

    ' --- PREPARAR TABLA RECETA ---
    Private Sub ConfigurarTablaReceta()
        dgv_receta.AllowUserToAddRows = False
        dgv_receta.AllowUserToDeleteRows = False
        dgv_receta.ReadOnly = True
        dgv_receta.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_receta.RowHeadersVisible = False
        dgv_receta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        dgv_receta.Columns.Add("IdMedicamento", "ID")
        dgv_receta.Columns("IdMedicamento").Visible = False ' Oculto, pero lo usamos para la BD

        dgv_receta.Columns.Add("NombreMedicamento", "Medicamento")
        '  LE DAMOS MÁS PRIORIDAD DE TAMAÑO A ESTA COLUMNA (Por defecto es 100)
        dgv_receta.Columns("NombreMedicamento").FillWeight = 200

        dgv_receta.Columns.Add("Dosis", "Dosis")
        dgv_receta.Columns.Add("Frecuencia", "Frecuencia")
        dgv_receta.Columns.Add("Duracion", "Duración")

        ' Botón para quitar el medicamento de la lista si el doctor se equivoca
        Dim btnEliminar As New DataGridViewButtonColumn() With {
            .Name = "colEliminar", .HeaderText = "", .Text = "🗑️",
            .UseColumnTextForButtonValue = True, .Width = 35, .FlatStyle = FlatStyle.Flat,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None '  EVITA QUE EL BOTÓN SE ESTIRE
        }
        dgv_receta.Columns.Add(btnEliminar)
    End Sub

    ' --- DESCARGAR CITAS DE "HOY" (Que no tengan consulta) ---
    Private Async Function CargarCitasDelDia() As Task
        Try
            Dim correo As String = SesionGlobal.correo
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/citas-disponibles?correoMedico={correo}")
            If response.IsSuccessStatusCode Then
                Dim json = Await response.Content.ReadAsStringAsync()
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim citas = JsonSerializer.Deserialize(Of List(Of CitaComboDTO))(json, opciones)

                cmb_cita.DataSource = citas
                cmb_cita.DisplayMember = "TextoCombo"
                cmb_cita.ValueMember = "IdCita"
                cmb_cita.SelectedIndex = -1
            End If
        Catch ex As Exception
        End Try
    End Function

    ' --- DESCARGAR MEDICINAS CON STOCK ---
    Private Async Function CargarMedicamentos() As Task
        Try
            Dim response = Await clienteHttp.GetAsync($"{urlBase}/medicamentos")
            If response.IsSuccessStatusCode Then
                Dim json = Await response.Content.ReadAsStringAsync()
                Dim opciones As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                Dim meds = JsonSerializer.Deserialize(Of List(Of MedComboDTO))(json, opciones)

                cmb_medicamento.DataSource = meds
                cmb_medicamento.DisplayMember = "Nombre"
                cmb_medicamento.ValueMember = "IdMedicamento"
                cmb_medicamento.SelectedIndex = -1
            End If
        Catch ex As Exception
        End Try
    End Function

    ' --- BOTÓN: AGREGAR A LA RECETA VISUAL ---
    Private Sub btn_agregar_medicamento_Click(sender As Object, e As EventArgs) Handles btn_agregar_medicamento.Click
        If cmb_medicamento.SelectedIndex = -1 Then
            MessageBox.Show("Selecciona un medicamento del catálogo primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrWhiteSpace(txt_dosis.Text) OrElse String.IsNullOrWhiteSpace(txt_frecuencia.Text) OrElse String.IsNullOrWhiteSpace(txt_duracion.Text) Then
            MessageBox.Show("Debes indicar la dosis, frecuencia y duración para recetar el medicamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim idMed As Integer = Convert.ToInt32(cmb_medicamento.SelectedValue)
        Dim nombreMed As String = cmb_medicamento.Text

        ' Validación: Evitar recetar el mismo medicamento 2 veces en la misma consulta
        For Each row As DataGridViewRow In dgv_receta.Rows
            If Convert.ToInt32(row.Cells("IdMedicamento").Value) = idMed Then
                MessageBox.Show("Este medicamento ya está en la receta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        ' Agregamos el renglón a la tabla
        dgv_receta.Rows.Add(idMed, nombreMed, txt_dosis.Text.Trim(), txt_frecuencia.Text.Trim(), txt_duracion.Text.Trim())

        ' Limpiamos las cajitas para recetar el siguiente
        cmb_medicamento.SelectedIndex = -1
        txt_dosis.Clear()
        txt_frecuencia.Clear()
        txt_duracion.Clear()
    End Sub

    ' --- CLIC EN LA TABLA RECETA (Botón Eliminar ❌) ---
    Private Sub dgv_receta_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_receta.CellContentClick
        If e.RowIndex >= 0 AndAlso dgv_receta.Columns(e.ColumnIndex).Name = "colEliminar" Then
            dgv_receta.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    ' --- BOTÓN PRINCIPAL: GUARDAR TODO EL EXPEDIENTE ---
    Private Async Sub btn_guardar_consulta_Click(sender As Object, e As EventArgs) Handles btn_guardar_consulta.Click
        ' 1. Validaciones
        If cmb_cita.SelectedIndex = -1 Then
            MessageBox.Show("Seleccione la cita para registrar la consulta.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrWhiteSpace(txt_sintomas.Text) OrElse String.IsNullOrWhiteSpace(txt_diagnostico.Text) Then
            MessageBox.Show("Los campos de Síntomas y Diagnóstico son obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If num_peso.Value <= 0 OrElse num_altura.Value <= 0 Then
            MessageBox.Show("El peso y la altura son datos obligatorios (mayores a cero).", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        btn_guardar_consulta.Enabled = False
        btn_guardar_consulta.Text = "Guardando expediente..."

        Try
            ' 2. Extraer los medicamentos de la tabla visual para empaquetarlos
            Dim listaReceta As New List(Of RecetaItemDTO)()
            For Each row As DataGridViewRow In dgv_receta.Rows
                listaReceta.Add(New RecetaItemDTO With {
                    .IdMedicamento = Convert.ToInt32(row.Cells("IdMedicamento").Value),
                    .Dosis = row.Cells("Dosis").Value.ToString(),
                    .Frecuencia = row.Cells("Frecuencia").Value.ToString(),
                    .Duracion = row.Cells("Duracion").Value.ToString()
                })
            Next

            ' 3. Empaquetar todo el JSON general
            Dim requestData = New With {
                .IdCita = Convert.ToInt32(cmb_cita.SelectedValue),
                .Sintomas = txt_sintomas.Text.Trim(),
                .Diagnostico = txt_diagnostico.Text.Trim(),
                .Observaciones = txt_observaciones.Text.Trim(),
                .Peso = num_peso.Value,
                .Altura = num_altura.Value,
                .Receta = listaReceta
            }

            Dim jsonString = JsonSerializer.Serialize(requestData)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            ' 4. Enviar a C#
            Dim response = Await clienteHttp.PostAsync(urlBase, content)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Consulta y receta registradas exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close() ' Regresamos al menú o pantalla anterior
            Else
                Dim errorMsg As String = "Error desconocido."
                Try
                    errorMsg = JsonDocument.Parse(responseBody).RootElement.GetProperty("error").GetString()
                Catch
                End Try
                MessageBox.Show(errorMsg, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error de conexión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_guardar_consulta.Enabled = True
            btn_guardar_consulta.Text = "GUARDAR REGISTRO DE CONSULTA"
        End Try
    End Sub

    Private Sub NavegarConEnter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_cita.KeyPress, txt_sintomas.KeyPress, txt_diagnostico.KeyPress, num_peso.KeyPress, num_altura.KeyPress, cmb_medicamento.KeyPress, txt_dosis.KeyPress, txt_frecuencia.KeyPress, txt_duracion.KeyPress, txt_observaciones.KeyPress
        ' Si presiona Enter, simulamos un "Tab" para pasar al siguiente campo
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class