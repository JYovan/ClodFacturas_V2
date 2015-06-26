Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Data


Public Class MySecurity


    Public Shared Function checkValidSession(c As Control) As Boolean

        Dim logeo As Boolean = True

        Try
            Dim ident As FormsIdentity = CType(c.Page.User.Identity, FormsIdentity)
            Dim ticket As FormsAuthenticationTicket = ident.Ticket

            If ident.Ticket Is Nothing Then
                c.Page.Response.Redirect("logeo.aspx")

            End If



            If (c.Page.Session("usua_id") Is Nothing) Then
                Dim dbtools As New ToolsT.DbToolsT

                Dim ds As DataSet = dbtools.GetDataSet("select * from usuario u inner join cuenta c on u.cuen_id=c.cuen_id  where usua_id='" & ticket.UserData & "'")
                c.Page.Session("usua_id") = ds.Tables(0).Rows(0).Item("usua_id")
                c.Page.Session("nombre") = ds.Tables(0).Rows(0).Item("nombre") & " " + ds.Tables(0).Rows(0).Item("app") & " " + ds.Tables(0).Rows(0).Item("apm")
                c.Page.Session("cuen_id") = ds.Tables(0).Rows(0).Item("cuen_id")
                c.Page.Session("cuenta") = ds.Tables(0).Rows(0).Item("cuenta")
                c.Page.Session("rfc") = ds.Tables(0).Rows(0).Item("rfc")

                dbtools.Disconnect()
            End If
        Catch ex As Exception
            c.Page.Response.Redirect("logeo.aspx")
        End Try
        Return logeo
    End Function



End Class
