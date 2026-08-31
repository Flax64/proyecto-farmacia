Public Class Reportes
    Private Sub btnExpedientes_Click(sender As Object, e As EventArgs) Handles btnExpedientes.Click
        Dim frmExpedientes As New Reportes_Expedientes_()
        frmExpedientes.ShowDialog()

    End Sub

    Private Sub btn_Reporte_Ventas_Click(sender As Object, e As EventArgs) Handles btn_Reporte_Ventas.Click
        Dim frmVentas As New ReporteVentas()
        frmVentas.ShowDialog()
    End Sub

    Private Sub btn_Reporte_Inventario_Click(sender As Object, e As EventArgs) Handles btn_Reporte_Inventario.Click
        Dim frmInventario As New ReporteInventario()
        frmInventario.ShowDialog()
    End Sub
End Class