Public Class FilaHistorial
    Public Property IdConsulta As Integer
    Public Property Fecha As String
    Public Property Hora As String
    Public Property Medico As String
    Public Property Sintomas As String
    Public Property Diagnostico As String
    Public Property Observaciones As String
    Public Property Peso As Double
    Public Property Altura As Double
    Public Property Receta As List(Of ExpedienteRecetaItem)
End Class
