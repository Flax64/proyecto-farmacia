' Modelo para recibir los datos del Proveedor
Public Class ProveedorDTO
    Public Property id_Proveedor As Integer
    Public Property nombre_Proveedor As String
End Class

' Modelo para los Medicamentos (Asegúrate que coincida con lo que manda C#)
Public Class Medicamento
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Precio As Decimal
    Public Property Stock As Integer

End Class
