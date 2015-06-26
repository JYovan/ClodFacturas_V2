Public Class AceptarOC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("depto") = "COMPRAS"
    End Sub

    Protected Sub txt_fecha_oc_TextChanged(sender As Object, e As EventArgs) Handles txt_fecha_oc.TextChanged
        Session("oc_fecha_soli") = txt_fecha_oc.Text
    End Sub

    Private Sub txt_Observaciones_TextChanged(sender As Object, e As EventArgs) Handles txt_Observaciones.TextChanged
        Session("oc_obser") = txt_Observaciones.Text
    End Sub

    Private Sub txt_Representante_TextChanged(sender As Object, e As EventArgs) Handles txt_Representante.TextChanged
        Session("oc_repre") = txt_Representante.Text
    End Sub

    Private Sub txt_Depto_TextChanged(sender As Object, e As EventArgs) Handles txt_Depto.TextChanged
        Session("depto") = txt_Depto.Text
    End Sub

    Protected Sub txt_fecha_pago_TextChanged(sender As Object, e As EventArgs) Handles txt_fecha_pago.TextChanged
        Session("fecha_pago") = txt_fecha_pago.Text
    End Sub
End Class