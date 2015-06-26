Public Class Administracion
    Inherits System.Web.UI.Page
    Dim dbt As New ToolsT.DbToolsT
    Dim mensajes As New messageTools(Me)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MySecurity.checkValidSession(Me) = False Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "Cargar()", True)
        End If
        If Not IsPostBack Then
            ConsultarUsuarios(Session("cuen_id"))
            Permisos()
        End If
    End Sub

    Protected Sub btn_cuenta_Click(sender As Object, e As ImageClickEventArgs) Handles btn_cuenta.Click
        If Seleccionados() <> "null" Then
            Actulaizar(Seleccionados())
        Else
            mensajes.setMessage("Debe seleccionar como mínimo un usuario.", 5000)
        End If
        ConsultarUsuarios(Session("cuen_id"))
    End Sub

    Protected Sub btn_eliminarFacturas2_Click(sender As Object, e As ImageClickEventArgs) Handles btn_eliminarFacturas2.Click
        System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "Cargar()", True)
    End Sub

    Protected Sub btn_cerrarConfir_Click(sender As Object, e As ImageClickEventArgs) Handles btn_cerrarConfir.Click
        MPE_confir.Hide()
    End Sub


    Sub Permisos()
        Try
            Dim tipodeUsuario As String = TipoUsuario(Session("usua_id"))
            If tipodeUsuario = "Master" Then
                Check_PagosCancelaciones.Visible = True
                Check_Firmas.Visible = True
                Check_OC.Visible = True
            ElseIf tipodeUsuario = "Administrador" Then
                Check_PagosCancelaciones.Visible = False
                Check_Firmas.Visible = False
                Check_OC.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub ConsultarUsuarios(ByVal id As String)
        Try
            Dim ds2 = dbt.GetDataSet("select   (Nombre +' '+ app+' '+apm) as NOMBRE,usuario as USUARIO , tipo as 'TIPO', Estado as 'ESTADO'  from usuario where cuen_id= " & id)
            Grid_usuarios.DataSource = ds2
            Grid_usuarios.DataBind()

            ds2 = dbt.GetDataSet("select * from usuario where cuen_id=  " & id)
            Grid_Usuarios2.DataSource = ds2
            Grid_Usuarios2.DataBind()
        Catch ex As Exception
            mensajes.setError("Error al consultar usuarios", 5000)
        End Try
    End Sub

    Protected Sub grid2Click(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid_usuarios.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim fila1 As GridViewRow = Grid_Usuarios2.Rows(index)
            Dim celda As TableCell = fila1.Cells(1)
            Dim id As String = celda.Text
            If e.CommandName = "Ver" Then
                Dim ds As DataSet
                ds = dbt.GetDataSet("select * from usuario as u inner join permisos p on u.usua_id =p.usua_id where u.usua_id=" & id)
                Dim estado As String = ds.Tables(0).Rows(0).Item("Estado")

                If estado = "Activo" Then
                    Radio_activo.Checked = True
                    Radio_inactivo.Checked = False
                ElseIf estado = "Inactivo" Then
                    Radio_inactivo.Checked = True
                End If

                If ds.Tables(0).Rows(0).Item("correos") = 1 Then
                    Check_correos.Checked = True
                Else
                    Check_correos.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("facturas") = 1 Then
                    Check_facturas.Checked = True
                Else
                    Check_facturas.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("cuentas") = 1 Then
                    Check_cuentas.Checked = True
                Else
                    Check_cuentas.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("subir") = 1 Then
                    Check_subirF.Checked = True
                Else
                    Check_subirF.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("actualizar") = 1 Then
                    Check_Actilizar.Checked = True
                Else
                    Check_Actilizar.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("administracion") = 1 Then
                    Check_administracion.Checked = True
                Else
                    Check_administracion.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("alta") = 1 Then
                    Check_alta.Checked = True
                Else
                    Check_alta.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("baja") = 1 Then
                    Check_baja.Checked = True
                Else
                    Check_baja.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("modificar") = 1 Then
                    Check_modificar.Checked = True
                Else
                    Check_modificar.Checked = False
                End If

                If ds.Tables(0).Rows(0).Item("PagosCancelaciones") = 1 Then
                    Check_PagosCancelaciones.Checked = True
                Else
                    Check_PagosCancelaciones.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("firmas") = 1 Then
                    Check_Firmas.Checked = True
                Else
                    Check_Firmas.Checked = False
                End If
                If ds.Tables(0).Rows(0).Item("OC") = 1 Then
                    Check_OC.Checked = True
                Else
                    Check_OC.Checked = False
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub Actulaizar(ByVal id As String)
        Try
            Dim correos, facturas, cuentas, subir, actulizar, administracion, alta, baja, modificar, estado As String
            Dim PagosCancelaciones, firmas, OC As Integer

            If Check_correos.Checked = True Then
                correos = 1
            Else
                correos = 0
            End If
            If Check_facturas.Checked = True Then
                facturas = 1
            Else
                facturas = 0
            End If
            If Check_cuentas.Checked = True Then
                cuentas = 1
            Else
                cuentas = 0
            End If
            If Check_subirF.Checked = True Then
                subir = 1
            Else
                subir = 0
            End If
            If Check_Actilizar.Checked = True Then
                actulizar = 1
            Else
                actulizar = 0
            End If
            If Check_administracion.Checked = True Then
                administracion = 1
            Else
                administracion = 0
            End If
            If Check_alta.Checked = True Then
                alta = 1
            Else
                alta = 0
            End If
            If Check_baja.Checked = True Then
                baja = 1
            Else
                baja = 0
            End If
            If Check_modificar.Checked = True Then
                modificar = 1
            Else
                modificar = 0
            End If
            If Radio_activo.Checked = True Then
                estado = "Activo"
            Else
                estado = "Inactivo"
            End If
            If Check_PagosCancelaciones.Checked = True Then
                PagosCancelaciones = 1
            Else
                PagosCancelaciones = 0
            End If
            If Check_Firmas.Checked = True Then
                firmas = 1
            Else
                firmas = 0
            End If
            If Check_OC.Checked = True Then
                OC = 1
            Else
                OC = 0
            End If

            dbt.ExecuteNonQuery("update permisos set correos=" & correos & ", " & _
                                "facturas =" & facturas & ", " & _
                                "cuentas= " & cuentas & ", " & _
                                "subir=" & subir & " , " & _
                                "actualizar= " & actulizar & " , " & _
                                "alta=" & alta & ", " & _
                                "baja=" & baja & ", " & _
                                "modificar=" & modificar & ",  " & _
                                "PagosCancelaciones=" & PagosCancelaciones & ", " & _
                                "firmas=" & firmas & ", " & _
                                "OC=" & OC & "  " & _
                                "where usua_id in " & id)

            If validarTipoUsuario(id) = True Then
                dbt.ExecuteNonQuery("update permisos set administracion= " & administracion & " " & _
                             "where usua_id in " & id)
                dbt.ExecuteNonQuery("update usuario set estado ='" & estado & "' where usua_id in " & id)

            Else
                If Check_administracion.Checked = False Or Radio_activo.Checked = False Then
                    mensajes.setError("No puede desactivar el usuario de administrador ni bloquear la entrada a este modulo.", 5000)
                    Exit Sub
                End If

            End If
            mensajes.setMessage("Permisos modificados con exito.", 5000)
            MPE_confir.Show()
        Catch ex As Exception
            mensajes.setError("Error al actualizar permisos", 5000)
        End Try
    End Sub

    Function validarTipoUsuario(ByVal id As String)
        Dim val As Boolean
        Dim ds As DataSet = dbt.GetDataSet("select * from usuario where usua_id in " & id)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            If ds.Tables(0).Rows(i).Item("Tipo") = "Administrador" Or ds.Tables(0).Rows(i).Item("Tipo") = "Administrador Siena" Then
                val = False
                Return val
            Else
                val = True
            End If
        Next
        Return val
    End Function

    Function Seleccionados() As String
        Dim val As String = "("
        For i As Integer = 0 To Grid_usuarios.Rows.Count - 1
            Dim ch As CheckBox = Grid_usuarios.Rows(i).FindControl("CheckBox1")
            If ch.Checked Then
                If val = "(" Then
                    Dim c As String = Grid_Usuarios2.Rows(i).Cells(1).Text
                    val = val & Grid_Usuarios2.Rows(i).Cells(1).Text
                Else
                    val = val & "," & Grid_Usuarios2.Rows(i).Cells(1).Text
                End If
            End If
        Next
        val = val & ")"
        If val = "()" Then
            val = "null"
        End If
        Return val
    End Function

    Function TipoUsuario(ByVal id As String)
        Dim val As String
        Dim ds As DataSet = dbt.GetDataSet("select * from usuario where usua_id =" & id)
        val = ds.Tables(0).Rows(0).Item("Tipo")
        Return val
    End Function

End Class