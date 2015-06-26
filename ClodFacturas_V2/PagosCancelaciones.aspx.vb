Imports System.Drawing
Imports Ionic.Zip
Imports System.Net
Imports System.IO
Imports OfficeOpenXml.Style
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class PagosCancelaciones
    Inherits System.Web.UI.Page
    Dim dbt As New ToolsT.DbToolsT
    Dim mensajes As New messageTools(Me)
    Dim mostrar As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MySecurity.checkValidSession(Me) = False Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "Cargar()", True)
        End If
        If Not IsPostBack Then
            ConsultarFacturas(Session("cuen_id"), False)
            Permisos()
        End If
        pintar()
    End Sub
    Private Sub btn_buscar_Click(sender As Object, e As EventArgs) Handles btn_buscar.Click
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub
    Protected Sub btn_cerrarConfir_Click(sender As Object, e As EventArgs) Handles btn_cerrarConfir.Click
        MPE_confir.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ConsultarFacturas(Session("cuen_id"), False)
        pintar()
        mensajes.setMessage("PDF guardado con exito.", 5000)
    End Sub

    Protected Sub Grid_facturas_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim fila1 As GridViewRow = Grid_facturas2.Rows(index)
        Dim celda1 As TableCell = fila1.Cells(5)
        Dim id_comp As String = celda1.Text
        Dim celda2 As TableCell = fila1.Cells(6)
        Dim id_xml As String = celda2.Text
        Dim celda3 As TableCell = fila1.Cells(7)
        Dim id_pdf As String = celda3.Text
        Dim celda4 As TableCell = fila1.Cells(8)
        Dim subido_por As String = celda4.Text
        Dim celda5 As TableCell = fila1.Cells(9)
        Dim id_corre As String = celda5.Text
        Dim celda6 As TableCell = fila1.Cells(10)
        Dim archivo As String = celda6.Text
        txt_idCRT.Text = id_comp
        If e.CommandName = "Restaurar" Then
            ValidarRestauracion(id_comp)
            txt_id.Text = id_comp
        End If
    End Sub

    Protected Sub Radio_top_todos_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_top_todos.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_top_50_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_top_50.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_top_20_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_top_20.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_top_10_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_top_10.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_asc_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_asc.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_desc_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_desc.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_por_folio_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_folio.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_por_factura_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_factura.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_por_proveedor_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_proveedor.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_por_fecha_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_fecha.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub txtNombreFactura_TextChanged(sender As Object, e As EventArgs) Handles txtNombreFactura.TextChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_todos_e_c_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_e_c.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub radio1__CheckedChanged(sender As Object, e As EventArgs) Handles Radio_examninados.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub
    Protected Sub radio2_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_correo.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Chec_nomfact_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_nomfact.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Chec_horas_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_horas.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Chec_cerNum_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_cerNum.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub
    Protected Sub Radio_Recibido_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_Recibido.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_emitido_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_emitido.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub btn_restaurar_Click(sender As Object, e As ImageClickEventArgs) Handles btn_restaurar.Click
        restaurarCancel(txt_id.Text)
    End Sub

    Protected Sub btn_cerrarConfir_Click(sender As Object, e As ImageClickEventArgs)
        MPE_confir.Hide()
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        MPE_confir.Hide()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        cancelarOc(txt_id.Text)
    End Sub

    Sub Permisos()
        Dim ds As DataSet = dbt.GetDataSet("select * from usuario as u inner join permisos p on u.usua_id =p.usua_id where u.usua_id=" & Session("usua_id"))
        If ds.Tables(0).Rows(0).Item("baja") = 0 Then
        Else
        End If
    End Sub

    Sub pintar()
        Try
            For i As Integer = 0 To Grid_facturas.Rows.Count - 1
                Dim fila1 As GridViewRow = Grid_facturas.Rows(i)
                Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
                Dim celda1 As TableCell = fila1.Cells(7)
                Dim celda2 As TableCell = fila2.Cells(7)
                Dim val As String = celda1.Text
                If val = "Pagado" Then
                    celda1.BackColor = System.Drawing.Color.FromName("#81F7F3")
                ElseIf val = "Sin pagar" Then
                    celda1.BackColor = System.Drawing.Color.FromName("#58FA58")
                Else
                    celda1.BackColor = System.Drawing.Color.FromName("#E20F20")
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
    Sub pintarPagados()
        Try
            Dim num As Integer
            If Chec_cerNum.Checked Then
                num = 14
            Else
                num = 13
            End If
            For i As Integer = 0 To Grid_facturas.Rows.Count - 1
                Dim fila1 As GridViewRow = Grid_facturas.Rows(i)
                Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
                Dim celda1 As TableCell = fila1.Cells(num)
                Dim celda2 As TableCell = fila2.Cells(16)
                Dim val As String = celda2.Text
                If val = "Pagado" Then
                    Grid_facturas.Rows(i).Enabled = False
                    celda1.BackColor = System.Drawing.Color.FromName("#81F7F3")
                ElseIf val = "Sin pagar" Then
                    celda1.BackColor = System.Drawing.Color.FromName("#58FA58")
                Else
                    Grid_facturas.Rows(i).Enabled = False
                    celda1.BackColor = System.Drawing.Color.FromName("#E20F20")
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Sub Limpiar()
        Try
            For i As Integer = 0 To Grid_facturas.Rows.Count - 1
                Dim fila1 As GridViewRow = Grid_facturas.Rows(i)
                Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
                Dim celda1 As TableCell = fila1.Cells(13)
                Dim celda2 As TableCell = fila2.Cells(16)
                Dim val As String = celda2.Text
                Grid_facturas.Rows(i).Enabled = True
            Next
        Catch ex As Exception
        End Try
    End Sub

    Sub restaurarCancel(ByVal id As String)
        dbt.ExecuteNonQuery("update comprobante set estatus='Sin pagar' where comp_id=" & id)
        MPE_confir.Hide()
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Sub cancelarOc(ByVal id As String)
        dbt.ExecuteNonQuery("update comprobante set estatus='Sin pagar' where comp_id=" & id)
        dbt.ExecuteNonQuery("update OrdenesDeCompra set estatus='Cancelado' ,fecha_Canecelacion=GETDATE(), usua_id_cancelacion=" & Session("usua_id") & " where comp_id= " & id)
        MPE_confirmar2.Hide()
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Sub ValidarRestauracion(ByVal id As String)
        Dim ds As DataSet
        ds = dbt.GetDataSet("select * from comprobante where comp_id =" & id)
        Dim estatus As String = ds.Tables(0).Rows(0).Item("Estatus")
        If estatus = "Cancelado" Then
            MPE_confir.Show()
        ElseIf estatus = "Pagado" Then
            MPE_confirmar2.Show()
        End If
    End Sub

    Sub seleccionarTodos(ByVal val As Boolean, ByVal check As String)
        For i As Integer = 0 To Grid_facturas.Rows.Count - 1
            Dim fila1 As GridViewRow = Grid_facturas.Rows(i)
            Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
            Dim celda1 As TableCell = fila1.Cells(13)
            Dim celda2 As TableCell = fila2.Cells(14)
            Dim val2 As String = celda2.Text
            Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl(check)
            If val2 = "Pagado" Then
                ch.Checked = False
            Else
                ch.Checked = val
            End If
        Next
    End Sub

    Sub eliminarFacturas()
        For i As Integer = 0 To Grid_facturas.Rows.Count - 1
            Dim fila1 As GridViewRow = Grid_facturas.Rows(i)
            Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
            Dim celda1 As TableCell = fila1.Cells(4)
            Dim celda2 As TableCell = fila2.Cells(5)
            Dim celda3 As TableCell = fila2.Cells(6)
            Dim celda4 As TableCell = fila2.Cells(7)
            Dim id_comp As String = celda2.Text
            Dim id_xml As String = celda3.Text
            Dim id_pdf As String = celda4.Text
            Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl("CheckBox1")
            If ch.Checked = True Then
                dbt.ExecuteNonQuery("delete contenido where cont_id  = " & id_xml)
                If id_pdf > 0 Then
                    dbt.ExecuteNonQuery("delete contenido where cont_id  = " & id_pdf)
                End If
                dbt.ExecuteNonQuery("delete comprobante where comp_id  = " & id_comp)
            End If
        Next
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Sub PreventingDoubleSubmit(ByVal button As Button)
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append("if (typeof(Page_ClientValidate) == ' ') { ")
        sb.Append("var oldPage_IsValid = Page_IsValid; var oldPage_BlockSubmit = Page_BlockSubmit;")
        sb.Append("if (Page_ClientValidate('" + button.ValidationGroup + "') == false) {")
        sb.Append(" Page_IsValid = oldPage_IsValid; Page_BlockSubmit = oldPage_BlockSubmit; return false; }} ")
        sb.Append("this.value = 'Procesando...';")
        sb.Append("this.disabled = true;")
        sb.Append(ClientScript.GetPostBackEventReference(button, Nothing) + ";")
        sb.Append("return true;")
        Dim submit_Button_onclick_js As String = sb.ToString()
        button.Attributes.Add("onclick", submit_Button_onclick_js)
    End Sub

    Sub ConsultarFacturas(ByVal id As String, ByVal b As Boolean)
        Dim ds2 As DataSet
        Dim filtros As String = ""
        Dim por As String = ""
        Dim ordenar As String = ""
        Dim top As String = ""
        Dim filtrosfechas As String = " "
        If Radio_por_fecha.Checked = True Then
            por = "fecha,factura"
        ElseIf Radio_por_proveedor.Checked = True Then
            por = "'Proveedor Nombre' ,fecha,factura"
        ElseIf Radio_por_factura.Checked = True Then
            por = "Factura,fecha"
        ElseIf Radio_por_folio.Checked = True Then
            por = "folio,fecha,factura"
        End If
        If Radio_desc.Checked Then
            ordenar = "order by " & por & " desc"
        ElseIf Radio_asc.Checked Then
            ordenar = "order by " & por & " asc"
        End If
        If Radio_top_10.Checked Then
            top = " top 10   "
        ElseIf Radio_top_20.Checked Then
            top = "  top 20 "
        ElseIf Radio_top_50.Checked Then
            top = "  top 50 "
        ElseIf Radio_top_todos.Checked Then
            top = " "
        End If
        Dim tipo As String = ""

        If Radio_emitido.Checked Then
            tipo = "and  com.tipo='Egreso'"
        ElseIf Radio_Recibido.Checked Then
            tipo = "and  com.tipo= 'Ingreso'"
        End If
        Dim mostarar_hms As String = ""
        Dim mostarar_nomFcat As String = ""
        Dim mostarar_certNum As String = ""
        If Chec_horas.Checked Then
            mostarar_hms = ",CONVERT(VARCHAR, getdate(), 108) as 'Horas/min/seg' "
        Else
            mostarar_hms = " "
        End If
        If Chec_nomfact.Checked Then
            mostarar_nomFcat = "cont_nombre  'FACTURA', "
        Else
            mostarar_nomFcat = " "
        End If
        If Chec_cerNum.Checked Then
            mostarar_certNum = "noCertificado 'N. CERTIFICADO' , "
        Else
            mostarar_certNum = " "
        End If
        filtros = " and e.rfc like '%" & txt_rfc_proveedor.Text & "%'" & _
                  " and e.nombre like '%" & txt_nombre_proveedor.Text & "%'" & _
                  " and folio like '%" & txt_folio.Text & "%' " & _
                  " and noCertificado like '%" & txt_numCertificado.Text & "%' " & _
                  " and  cont_nombre like '%" & txtNombreFactura.Text & "%'"
        If txt_FechaAl.Text = "" Or txt_FechaDe.Text = "" Then
            filtrosfechas = " "
        Else
            filtrosfechas = " and com.fecha between CONVERT(datetime,'" & txt_FechaDe.Text.ToString.Substring(0, 10) & " 01:00:00.000') and CONVERT(datetime,'" & txt_FechaAl.Text.ToString.Substring(0, 10) & " 23:59:59.000') "
        End If
        Dim consulta As String = "    select   " & mostarar_nomFcat & "  e.rfc 'PROVEEDOR RFC'   ,e.nombre 'PROVEEDOR NOMBRE',folio 'FOLIO', " & mostarar_certNum & "Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'FECHA', Total as 'IMPORTE', Estatus as 'ESTATUS'   " & mostarar_hms & _
                             " from comprobante com" & _
                             " inner join contenido c_xml on c_xml.cont_id=com.xml_id" & _
                             " inner join Emisor e on e.emis_id=com.emis_id" & _
                             " inner join Receptor r on r.rece_id=com.rece_id" & _
                             " inner join correos c on c.corre_id = com.corre_id " & _
                             " where Estatus <> 'Sin pagar' and  com.cuen_id =" & Session("cuen_id") & _
                             filtros & filtrosfechas & tipo
        Dim consulta2 As String = "select  " & mostarar_nomFcat & "  e.rfc 'Proveedor RFC'   ,e.nombre 'Proveedor Nombre',folio 'Folio', " & mostarar_certNum & "Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'Fecha',Total as 'IMPORTE'  ,Estatus as 'ESTATUS'   " & mostarar_hms & _
                            " from comprobante com" & _
                            " inner join contenido c_xml on c_xml.cont_id=com.xml_id" & _
                            " inner join Emisor e on e.emis_id=com.emis_id" & _
                            " inner join Receptor r on r.rece_id=com.rece_id" & _
                            " where Estatus <> 'Sin pagar' and com.cuen_id =" & Session("cuen_id") & _
                            filtros & filtrosfechas
        If Radio_correo.Checked Then
            consulta = consulta & " and subido_por='Correo' "
        ElseIf Radio_examninados.Checked Then
            consulta = consulta2 & " and subido_por='Examinado'"
        Else
            consulta = consulta & "  and subido_por='Correo' union all " & consulta2 & " and subido_por='Examinado'"
        End If
        ds2 = dbt.GetDataSet("select" & top & " a.* from (  " & consulta & ") as a " & ordenar)
        Grid_facturas.DataSource = ds2
        Grid_facturas.DataBind()
        Dim consulta3 As String = "select com.cuen_id,[comp_id],[xml_id], [pdf_id],subido_por, com.corre_id,cont_nombreFecha  ,   e.rfc 'Proveedor RFC'   ,e.nombre 'Proveedor Nombre',folio 'Folio',  Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'Fecha' ,cont_nombre 'Factura',Estatus as 'ESTATUS'  " & _
                          " from comprobante com" & _
                          " inner join contenido c_xml on c_xml.cont_id=com.xml_id" & _
                          " inner join Emisor e on e.emis_id=com.emis_id" & _
                          " inner join Receptor r on r.rece_id=com.rece_id" & _
                          " inner join correos c on c.corre_id = com.corre_id " & _
                          " where  Estatus <> 'Sin pagar'and  com.cuen_id =" & Session("cuen_id") & _
                          filtros & filtrosfechas & tipo
        Dim consulta4 As String = "select com.cuen_id,[comp_id],[xml_id], [pdf_id],subido_por, com.corre_id,cont_nombreFecha  ,   e.rfc 'Proveedor RFC'   ,e.nombre 'Proveedor Nombre',folio 'Folio',  Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'Fecha',cont_nombre  'Factura',Estatus as 'ESTATUS' " & _
                          " from comprobante com" & _
                          " inner join contenido c_xml on c_xml.cont_id=com.xml_id" & _
                          " inner join Emisor e on e.emis_id=com.emis_id" & _
                          " inner join Receptor r on r.rece_id=com.rece_id" & _
                          " where Estatus <> 'Sin pagar' and  com.cuen_id =" & Session("cuen_id") & _
                          filtros & filtrosfechas
        If Radio_correo.Checked Then
            consulta3 = consulta3 & " and subido_por='Correo' "
        ElseIf Radio_examninados.Checked Then
            consulta3 = consulta4 & " and subido_por='Examinado'"
        Else
            consulta3 = consulta3 & "  and subido_por='Correo' union all " & consulta4 & " and subido_por='Examinado'"
        End If
        ds2 = dbt.GetDataSet("select" & top & " a.* from (  " & consulta3 & ") as a " & ordenar)
        Grid_facturas2.DataSource = ds2
        Grid_facturas2.DataBind()
        pintar()
        Label17.Text = ds2.Tables(0).Rows.Count.ToString
    End Sub
    Sub Seleccionados_oc_Guardar()
        Try
            Dim fecha As String = Session("oc_fecha_soli")
            For i As Integer = 0 To Grid_facturas.Rows.Count - 1
                Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl("Check_oc")
                If ch.Checked Then
                    dbt.ExecuteNonQuery("insert into ordenesdecompra values (getdate(),'" & fecha & "'," & Grid_facturas2.Rows(i).Cells(5).Text & ",'Activo','" & Session("oc_obser") & "','" & Session("oc_repre") & "','" & Session("depto") & "'," & Session("cuen_id") & ")")
                End If
            Next
            Session("oc_fecha_soli") = ""
            Session("oc_obser") = ""
            Session("oc_repre") = ""
            Session("depto") = "COMPRAS"
        Catch ex As Exception
        End Try
    End Sub

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

    Function CambiarCaracteres(ByVal archivo As String)
        Dim res As String = ""
        For i As Integer = 0 To archivo.Length - 1
            Dim val As String = archivo(i)
            If val <> "&" Then
                res = res & val
            End If
        Next
        Return res
    End Function

    Function Extencion(Path As String, Caracter As String) As String
        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        Extencion = ret
    End Function

    Function Seleccionados() As String
        Dim val As String = "("
        For i As Integer = 0 To Grid_facturas.Rows.Count - 1
            Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl("CheckBox1")
            If ch.Checked Then
                If val = "(" Then
                    val = val & Grid_facturas2.Rows(i).Cells(5).Text
                Else
                    val = val & "," & Grid_facturas2.Rows(i).Cells(5).Text
                End If
            End If
        Next
        val = val & ")"
        If val = "()" Then
            val = "null"
        End If
        Return val
    End Function

    Function Seleccionados_oc() As String
        Dim val As String = "("
        For i As Integer = 0 To Grid_facturas.Rows.Count - 1
            Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl("Check_oc")
            If ch.Checked Then
                If val = "(" Then
                    val = val & Grid_facturas2.Rows(i).Cells(5).Text
                Else
                    val = val & "," & Grid_facturas2.Rows(i).Cells(5).Text
                End If
            End If
        Next
        val = val & ")"
        If val = "()" Then
            val = "null"
        End If
        Return val
    End Function

    Function optenerDescuento(ByVal id As String)
        Dim val As Double = 0.0
        Try
            Dim ds As DataSet
            ds = dbt.GetDataSet("select * from comprobante where comp_id =" & id)
            val = ds.Tables(0).Rows(0).Item("Descuento")
        Catch ex As Exception
        End Try
        Return val
    End Function

    Function optenerIVA(ByVal id As String)
        Dim val As Double = 0.0
        Try
            Dim ds As DataSet
            ds = dbt.GetDataSet("select totalImpuestosTrasladados from comprobante c inner join impuesto i on c.impu_id= i.impu_id where comp_id =" & id)
            val = ds.Tables(0).Rows(0).Item("totalImpuestosTrasladados")
        Catch ex As Exception
        End Try
        Return val
    End Function
End Class
