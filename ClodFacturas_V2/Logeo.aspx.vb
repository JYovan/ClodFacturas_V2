Public Class Logeo
    Inherits System.Web.UI.Page

    Dim mensajes As New messageTools(Me)
    Dim dbt As New ToolsT.DbToolsT

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        keybox.Focus()
        If (Request.IsAuthenticated) Then
            Response.Redirect("index.aspx")
        End If
        Dim Name As String
        Dim Platform As String
        With Request.Browser
            Name = .Browser
            Platform = .Platform
        End With
        If Name <> "Chrome" Then
            If Platform <> "WinNT" Or Platform <> "UNIX" Then
                Response.Redirect("Logeo_02.aspx")
            End If
        End If
    End Sub

    Protected Sub btn_aceptar_Click(sender As Object, e As ImageClickEventArgs) Handles btn_inicio.Click
        Try
            Dim key = keybox.Text.Replace("'", "''")
            Dim pw = pwbox.Text.Replace("'", "''")
            Dim ds As DataSet = dbt.GetDataSet("select * from usuario as u inner join cuenta as c on u.cuen_id=c.cuen_id  where usuario ='" & key & "' and contrasenia='" & pw & "' ")
            Dim r As DataRow = ds.Tables(0).Rows(0)
            Dim clave As String = r("usuario")
            Dim password As String = r("contrasenia")
            Dim estado As String = r("Estado")
            If estado = "Inactivo" Then
                mensajes.setError("El usuario se encuentra Inactivo, contacte con su administrador", 5000)
                Exit Sub
            End If
            If ds.Tables(0).Rows.Count = 0 Or Not (clave = keybox.Text And password = pwbox.Text) Then
                mensajes.setError("No existe esa clave o password.", 5000)
                Exit Sub
            Else
                Dim tkt As FormsAuthenticationTicket
                Dim cookiestr As String
                Dim ck As HttpCookie
                Dim id As String = ds.Tables(0).Rows(0).Item("usua_id")
                tkt = New FormsAuthenticationTicket(1, key, DateTime.Now(), DateTime.Now.AddMinutes(60), True, id)
                cookiestr = FormsAuthentication.Encrypt(tkt)
                ck = New HttpCookie(FormsAuthentication.FormsCookieName(), cookiestr)
                ck.Expires = tkt.Expiration
                ck.Path = FormsAuthentication.FormsCookiePath()
                Response.Cookies.Add(ck)
                Session("usua_id") = ds.Tables(0).Rows(0).Item("usua_id")
                Session("nombre") = ds.Tables(0).Rows(0).Item("nombre") & " " + ds.Tables(0).Rows(0).Item("app") & " " + ds.Tables(0).Rows(0).Item("apm")
                Session("logeo") = True
                Session("cuen_id") = ds.Tables(0).Rows(0).Item("cuen_id")
                Dim aaa As String = ds.Tables(0).Rows(0).Item("cuen_id")
                Session("cuenta") = ds.Tables(0).Rows(0).Item("cuenta")
                Session("rfc") = ds.Tables(0).Rows(0).Item("rfc")
                Response.Redirect("index.aspx")
            End If
        Catch ex As Exception
            mensajes.setError("No existe esa clave o password.", 5000)
        End Try
    End Sub

End Class