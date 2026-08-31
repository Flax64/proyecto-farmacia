Public Class UsuarioVB
    Public Property IdUsuario As Integer
    Public Property Nombre As String
    Public Property ApellidoPaterno As String
    Public Property ApellidoMaterno As String
    Public Property Email As String
    Public Property Rol As String
    Public Property Telefono As String
    Public Property FechaNacimiento As String
    Public Property Genero As String
    Public Property CedulaProfesional As String
    Public Property Especialidad As String
    Public Property Estatus As String

    ' Propiedad extra para el buscador
    Public ReadOnly Property NombreCompleto As String
        Get
            Return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}".Trim()
        End Get
    End Property
End Class