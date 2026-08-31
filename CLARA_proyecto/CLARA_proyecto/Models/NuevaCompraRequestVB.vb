' Este objeto es el que viaja por HttpClient hacia el Backend
Public Class NuevaCompraRequestVB
    Public Property IdProveedor As Integer
    Public Property TotalCompra As Decimal
    Public Property Detalles As List(Of DetalleNuevaCompraVB)
End Class

' Este es cada renglón del carrito de compras
Public Class DetalleNuevaCompraVB
    Public Property IdMedicamento As Integer

    Public Property Cantidad As Integer
End Class
