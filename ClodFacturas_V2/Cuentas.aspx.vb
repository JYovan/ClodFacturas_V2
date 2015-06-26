Imports OpenPop.Pop3
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Public Class Cuentas
    Inherits System.Web.UI.Page

    Dim mensajes As New messageTools(Me)
    Dim dbt As New ToolsT.DbToolsT
    Dim bandera_modificar As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mostrarBotones(True, False, False, False, False)
            consultarCuenta(Cuen)
            txt_Tipo.Enabled = False
        End If
    End Sub

    Function Cuen()
        Dim val As String = ""
        If Session("usua_id") = Nothing Then
        Else
            Dim ds As DataSet = dbt.GetDataSet("select * from usuario where usua_id=" & Session("usua_id"))
            val = ds.Tables(0).Rows(0).Item("cuen_id")
        End If
        Return val
    End Function

    Private Sub btn_cuenta_Click(sender As Object, e As ImageClickEventArgs) Handles btn_cuenta.Click
        insertarCuenta()
    End Sub

    Private Sub btn_correo_Click(sender As Object, e As ImageClickEventArgs) Handles btn_correo.Click
        InsertarCorreo()
    End Sub

    Private Sub btn_usuario_Click(sender As Object, e As ImageClickEventArgs) Handles btn_usuario.Click
        If cuentaModi.Text = "T" Then
            ModificarUsuario()
            cuentaModi.Text = "F"
        Else
            insertarUsiario()
        End If
    End Sub

    Private Sub btn_correo_modificar_Click(sender As Object, e As ImageClickEventArgs) Handles btn_correo_modificar.Click
        ModificarCorreo()
    End Sub

    Protected Sub grid1_usuariosClick(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid_Usuarios.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim fila1 As GridViewRow = Grid_Usuarios2.Rows(index)
        Dim celda As TableCell = fila1.Cells(1)
        Dim id As String = celda.Text
        If e.CommandName = "modificar" Then
            cuentaModi.Text = "T"
            Dim ds As DataSet = dbt.GetDataSet("select * from usuario where usua_id= " & id)
            txt_id_Usuario.Text = ds.Tables(0).Rows(0).Item("usua_id")
            txt_nombre.Text = ds.Tables(0).Rows(0).Item("nombre")
            txt_app.Text = ds.Tables(0).Rows(0).Item("app")
            txt_apma.Text = ds.Tables(0).Rows(0).Item("apm")
            txt_Usuario.Text = ds.Tables(0).Rows(0).Item("usuario")
            txt_Contrasena.Text = ds.Tables(0).Rows(0).Item("contrasenia")
            mostrarBotones(False, True, True, False, False)
        Else
            MPE_confir.Show()
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "document.getElementById('panel_confir2').style.display = 'block';", True)
            txt_id_Usuario.Text = id
        End If
    End Sub

    Sub eliminaUsuario(ByVal id As String)
        Try
            dbt.ExecuteNonQuery("delete usuario where usua_id= " & id)
            ConsultarUsuarios(Session("cuen_id"))
            MPE_confir.Hide()
        Catch ex As Exception
            mensajes.setError("Error al eliminar el usuario", 5000)
        End Try
    End Sub

    Protected Sub grid1Click(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid_correos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim fila1 As GridViewRow = grid_correos2.Rows(index)
        Dim celda As TableCell = fila1.Cells(2)
        Dim id As String = celda.Text
        If e.CommandName = "probar" Then
            ProbarCorreo(id)
        ElseIf e.CommandName = "modificar" Then
            Dim ds As DataSet = dbt.GetDataSet("select * from cuenta_correos where cuen_corre_id= " & id)

            txt_Correo.Text = ds.Tables(0).Rows(0).Item("cuenta")
            txt_contrasenia.Text = ds.Tables(0).Rows(0).Item("contrasenia")
            txt_Tipo.Text = ds.Tables(0).Rows(0).Item("tipo")
            txt_Servidor.Text = ds.Tables(0).Rows(0).Item("servidor")
            txt_Host.Text = ds.Tables(0).Rows(0).Item("host")
            txt_Puerto.Text = ds.Tables(0).Rows(0).Item("puerto")
            txt_id_correo.Text = ds.Tables(0).Rows(0).Item("cuen_corre_id")
            mostrarBotones(False, False, True, True, False)
            ConsultarCorreosCuenta(Session("cuen_id"))
        Else
            MPE_confir_correo.Show()
            txt_id_correo.Text = id
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "document.getElementById('panel5').style.display = 'block';", True)
        End If
    End Sub

    Sub EliminarCorreo(ByVal id As String)
        Try
            dbt.ExecuteNonQuery("delete cuenta_correos where cuen_corre_id= " & id)
            MPE_confir_correo.Hide()
            ConsultarCorreosCuenta(Session("cuen_id"))
        Catch ex As Exception
            mensajes.setError("Error al eliminar el correo", 5000)
        End Try
    End Sub

    Sub ProbarCorreo(ByVal id As String)
        Try
            Dim pop3Client As Pop3Client
            Dim p_ds As DataSet = dbt.GetDataSet("select * from cuenta_correos where cuen_corre_id=" & id)
            Dim p_cuenta As String = p_ds.Tables(0).Rows(0).Item("cuenta")
            Dim p_contrasenia As String = p_ds.Tables(0).Rows(0).Item("contrasenia")
            Dim p_puerto As String = p_ds.Tables(0).Rows(0).Item("puerto")
            Dim p_servior As String = p_ds.Tables(0).Rows(0).Item("servidor")
            Dim ssl As Boolean
            Dim Callback As Security.RemoteCertificateValidationCallback = Function(s As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True
            If p_ds.Tables(0).Rows(0).Item("ssl") = 1 Then
                ssl = True
            Else
                ssl = False
            End If
            pop3Client = New Pop3Client()
            pop3Client.Connect(p_servior, p_puerto, ssl, 8000, 8000, Callback)
            pop3Client.Authenticate(p_cuenta, p_contrasenia, AuthenticationMethod.TryBoth)
            Session("Pop3Client") = pop3Client
            dbt.ExecuteNonQuery("update cuenta_correos  set estado='Correcto' where cuen_corre_id= " & id)
        Catch ex As Exception
            mensajes.setError(ex.Message, 5000)
            dbt.ExecuteNonQuery("update cuenta_correos  set estado='No identificado' where cuen_corre_id= " & id)
            dbt.ExecuteNonQuery("insert into errores values ('" + ex.Message + "',getdate(),'Consultar correos',0)")
        End Try
        If Session("cuen_id") = Nothing Then
            ConsultarCorreosCuenta(ultimoID("cuenta", "cuen_id"))
        Else
            ConsultarCorreosCuenta(Session("cuen_id"))
        End If

    End Sub

    Protected Sub Traspaso_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid_correos.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _row As System.Data.DataRowView = e.Row.DataItem

            If _row.Item("Estado") = "Correcto" Then
                e.Row.Cells(5).BackColor = System.Drawing.Color.FromName("#088A08")
            ElseIf _row.Item("Estado") = "No identificado" Then
                e.Row.Cells(5).BackColor = System.Drawing.Color.FromName("#ffb800")
            Else
                e.Row.Cells(5).BackColor = System.Drawing.Color.FromName("#DF0101")
            End If
        End If
    End Sub

    Sub InsertarCorreo()
        Try

            Dim ssl As String
            If Radio_ssl_si.Checked Then
                ssl = 1
            Else
                ssl = 0
            End If
            Dim idC As String = FidC()
            'If Validar3Correos(idC) Then
            dbt.ExecuteNonQuery(" insert into cuenta_correos  values ( '" & txt_Correo.Text & "','" & txt_contrasenia.Text & "','" & txt_Tipo.Text & "','" & txt_Servidor.Text & "','" & txt_Host.Text & "','" & txt_Puerto.Text & "','Inactivo','" & idC & "'," & ssl & ")")
            'End If
            ConsultarCorreosCuenta(idC)
            LimpiarCorreo()
            mensajes.setMessage("Correo guardado con éxito", 5000)
        Catch ex As Exception
            mensajes.setError("Error al guardar correo", 5000)
        End Try

    End Sub

    Sub insertarCuenta()
        Try
            dbt.ExecuteNonQuery("insert into cuenta values('" & txt_Cuenta.Text & "','" & txt_RazonSocial.Text & "','" & txt_RFC.Text & "','" & txt_CURP.Text & "','" & txt_contraseniaSat.Text & "') ")
            BloquearCuenta(False)
            mostrarBotones(False, True, True, False, False)
            txt_Correo.Focus()
            mensajes.setMessage("Cuenta guardada con exito", 5000)
        Catch ex As Exception
            mensajes.setError("Error al guardar cuenta", 5000)
        End Try
    End Sub

    Sub insertarUsiario()
        Try
            Dim idC As String = FidC()
            Dim ds As DataSet
            ds = dbt.GetDataSet("select * from usuario where cuen_id = " & idC)
            Dim tipo As String
            If ds.Tables(0).Rows.Count <= 0 Then
                tipo = "Administrador"
            Else
                tipo = "Usuario"
            End If
            dbt.ExecuteNonQuery("insert into usuario values ('" & txt_nombre.Text & "','" & txt_app.Text & "','" & txt_apma.Text & "','" & txt_Usuario.Text & "','" & txt_Contrasena.Text & "','" & idC & "','" & tipo & "','Inactivo')")
            Dim usua_id As String = ultimoID("usuario", "usua_id")
            dbt.ExecuteNonQuery("insert into permisos values ('" & usua_id & "',1,1,1,1,1,1,1,1,1,0,0,0)")
            ConsultarUsuarios(idC)
            limpiarUsuario()
            mensajes.setMessage("Usuario guardado correctamente", 5000)
        Catch ex As Exception
            mensajes.setError("Error al insertar el nuevo usuario", 5000)
        End Try
    End Sub
    Function FidC()
        Dim val As String
        If Session("cuen_id") = Nothing Then
            val = ultimoID("cuenta", "cuen_id")
        Else
            val = Session("cuen_id")
        End If
        Return val
    End Function

    Sub ConsultarUsuarios(ByVal id As String)
        Try
            Dim ds2 = dbt.GetDataSet("select   (Nombre +' '+ app+' '+apm) as NOMBRE,usuario as USUARIO  from usuario where cuen_id= " & id)
            Grid_Usuarios.DataSource = ds2
            Grid_Usuarios.DataBind()

            ds2 = dbt.GetDataSet("select * from usuario where cuen_id=  " & id)
            Grid_Usuarios2.DataSource = ds2
            Grid_Usuarios2.DataBind()
        Catch ex As Exception
            mensajes.setError("Error al consultar usuarios", 5000)
        End Try
    End Sub

    Sub ConsultarCorreosCuenta(ByVal id As String)
        Dim ds_correos As DataSet
        ds_correos = dbt.GetDataSet("select cuenta as 'CUENTA',servidor as 'SERVIDOR' , estado as 'ESTADO' from cuenta_correos where cuen_id=  " & id)
        Grid_correos.DataSource = ds_correos
        Grid_correos.DataBind()
        ds_correos = dbt.GetDataSet("select * from cuenta_correos where cuen_id=  " & id)
        grid_correos2.DataSource = ds_correos
        grid_correos2.DataBind()
    End Sub

    Sub consultarCuenta(ByVal id As String)
        Try
            If Session("usua_id") = Nothing Then

            Else
                Dim ds As DataSet
                ds = dbt.GetDataSet("select * from cuenta where cuen_id =" & id)
                txt_Cuenta.Text = ds.Tables(0).Rows(0).Item("cuenta")
                txt_RazonSocial.Text = ds.Tables(0).Rows(0).Item("razon_social")
                txt_RFC.Text = ds.Tables(0).Rows(0).Item("RFC")
                txt_CURP.Text = ds.Tables(0).Rows(0).Item("CURP")
                ConsultarCorreosCuenta(ds.Tables(0).Rows(0).Item("cuen_id"))
                ConsultarUsuarios(ds.Tables(0).Rows(0).Item("cuen_id"))
                BloquearCuenta(False)
                mostrarBotones(False, True, True, False, False)
            End If

        Catch ex As Exception
            mensajes.setMessage("Error el consultar cuenta", 5000)
        End Try

    End Sub

    Sub ModificarUsuario()
        Try
            Dim idC As String = FidC()
            dbt.ExecuteNonQuery("update usuario set nombre='" & txt_nombre.Text & "', app='" & txt_app.Text & "',apm= '" & txt_apma.Text & "', usuario= '" & txt_Usuario.Text & "', contrasenia='" & txt_Contrasena.Text & "' where usua_id= " & txt_id_Usuario.Text)
            limpiarUsuario()
            mostrarBotones(False, True, True, False, False)
            ConsultarUsuarios(idC)
            mensajes.setMessage("Usuario modificado con éxito", 5000)
        Catch ex As Exception
            mensajes.setError("Error al modificar usuario", 5000)
        End Try
    End Sub

    Sub ModificarCorreo()
        Try
            Dim idC As String = FidC()

            dbt.ExecuteNonQuery("update cuenta_correos set cuenta='" & txt_Correo.Text & "', contrasenia='" & txt_contrasenia.Text & "',tipo = '" & txt_Tipo.Text & "', servidor = '" & txt_Servidor.Text & "', host= '" & txt_Host.Text & "',puerto='" & txt_Puerto.Text & "' where cuen_corre_id = " & txt_id_correo.Text)
            LimpiarCorreo()
            mostrarBotones(False, True, True, False, False)
            ConsultarCorreosCuenta(idC)
            mensajes.setMessage("Correo modificado con éxito", 5000)
        Catch ex As Exception
            mensajes.setError("Error al modificar correo", 5000)
        End Try
    End Sub

    Sub LimpiarCuenta()
        txt_Cuenta.Text = Nothing
        txt_RazonSocial.Text = Nothing
        txt_RFC.Text = Nothing
        txt_CURP.Text = Nothing
    End Sub

    Sub LimpiarCorreo()
        txt_Correo.Text = Nothing
        txt_contrasenia.Text = Nothing
        txt_Tipo.Text = Nothing
        txt_Servidor.Text = Nothing
        txt_Host.Text = Nothing
        txt_Puerto.Text = Nothing
    End Sub

    Sub limpiarUsuario()
        txt_Usuario.Text = Nothing
        txt_Contrasena.Text = Nothing
        txt_nombre.Text = Nothing
        txt_app.Text = Nothing
        txt_apma.Text = Nothing
        cuentaModi.Text = "F"
    End Sub

    Sub BloquearCuenta(ByVal t As Boolean)
        txt_Cuenta.Enabled = t
        txt_RazonSocial.Enabled = t
        txt_RFC.Enabled = t
        txt_CURP.Enabled = t
    End Sub

    Sub mostrarBotones(ByVal t1 As Boolean, ByVal t2 As Boolean, ByVal t3 As Boolean, ByVal t4 As Boolean, ByVal t5 As Boolean)
        btn_cuenta.Visible = t1
        btn_correo.Visible = t2
        btn_usuario.Visible = t3
        btn_correo_modificar.Visible = t4
        btn_usuario_modificar.Visible = t5
    End Sub

    Function Validar3Correos(ByVal id As String) As Boolean
        Dim val As Boolean = False
        Try
            Dim ds As DataSet = dbt.GetDataSet("select * from cuenta_correos  where cuen_id =" & id)
            If ds.Tables(0).Rows.Count < 3 Then
                val = True
            Else
                mensajes.setError("La cuenta solo puede contar con 3 correos", 5000)
                val = False
            End If
        Catch ex As Exception
            mensajes.setError("Error al validar correos", 5000)
        End Try
        Return val
    End Function

    Function ultimoID(ByVal tabla As String, ByVal elemento As String) As Integer
        Dim id As Integer
        Dim ds As DataSet = dbt.GetDataSet("select top 1 * from " & tabla & " order by " & elemento & " desc")
        Try
            id = ds.Tables(0).Rows(0).Item(elemento)
        Catch ex As Exception
            id = 1
        End Try
        Return id
    End Function

    Private Sub btn_limpiar_correo_Click(sender As Object, e As ImageClickEventArgs) Handles btn_limpiar_correo.Click
        LimpiarCorreo()
        btn_correo_modificar.Visible = False
        btn_correo.Visible = True
    End Sub

    Private Sub btn_limpiar_usuario_Click(sender As Object, e As ImageClickEventArgs) Handles btn_limpiar_usuario.Click
        limpiarUsuario()
        btn_usuario_modificar.Visible = False
        btn_usuario.Visible = True
    End Sub

    Protected Sub btn_eliminarFacturas2_Click(sender As Object, e As ImageClickEventArgs) Handles btn_eliminarFacturas2.Click
        eliminaUsuario(txt_id_Usuario.Text)
    End Sub

    Protected Sub btn_cerrarConfir_Click(sender As Object, e As ImageClickEventArgs) Handles btn_cerrarConfir.Click
        MPE_confir.Hide()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        EliminarCorreo(txt_id_correo.Text)
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        MPE_confir_correo.Hide()
    End Sub
End Class