Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports Ionic.Zip

Public Class OrdenesdeCompra
    Inherits System.Web.UI.Page
    Dim mensajes As New messageTools(Me)
    Dim dbt As New ToolsT.DbToolsT

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MySecurity.checkValidSession(Me) = False Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "Cargar()", True)
        End If
        If Not IsPostBack Then
            ConsultarOrdenes()
        End If
    End Sub

    Sub ConsultarOrdenes()
        Dim filtros As String = ""
        Dim por As String = ""
        Dim ordenar As String = ""
        Dim top As String = ""
        Dim filtrosfechas1 As String = " "
        Dim filtrosfechas2 As String = " "

        If Radio_por_fecha.Checked = True Then
            por = "fecha_de_solicitud"
        ElseIf Radio_por_proveedor.Checked = True Then
            por = "'Proveedor Nombre' "
        ElseIf Radio_por_folio.Checked = True Then
            por = "folio"
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
        filtros = " and nombre like '%" & txt_proveedor.Text & "%'" & _
                  " and rfc like '%" & txt_proveedorRFC.Text & "%' "
        If txt_fecha_soli_del.Text = "" Or txt_fecha_soli_al.Text = "" Then
            filtrosfechas1 = " "
        Else
            filtrosfechas1 = " and fecha_estimada_pago between CONVERT(datetime,'" & txt_fecha_soli_del.Text.ToString.Substring(0, 10) & " 00:00:00.000') and CONVERT(datetime,'" & txt_fecha_soli_al.Text.ToString.Substring(0, 10) & " 23:59:59.000')  "
        End If
        If txt_fecha_entrega_del.Text = "" Or txt_fecha_entrega_al.Text = "" Then
            filtrosfechas2 = " "
        Else
            filtrosfechas2 = " and fecha_de_entrega between CONVERT(datetime,'" & txt_fecha_entrega_del.Text.ToString.Substring(0, 10) & " 00:00:00.000') and CONVERT(datetime,'" & txt_fecha_entrega_al.Text.ToString.Substring(0, 10) & " 23:59:59.000')  "
        End If
        Dim consulta As String = " Select  " & top & "  Convert(varchar(10),CONVERT(date,fecha_estimada_pago,106),103)   as 'FECHA ESTIMADA DE PAGO',  Convert(varchar(10),CONVERT(date,fecha_de_entrega,106),103)  'FECHA DE ENTREGA',rfc 'PROVEEDOR RFC', nombre 'PROVEEDOR NOMBRE' ,folio as 'FOLIO', total as 'IMPORTE' " & _
                                 " from ordenesdecompra oc " & _
                                 " inner join comprobante c on oc.comp_id=c.comp_id" & _
                                 " inner join emisor e on e.emis_id = c.emis_id" & _
                                 " where oc.estatus ='Activo'and c.cuen_id =" & Session("cuen_id") & filtrosfechas1 & filtrosfechas2 & filtros & ordenar
        Dim ds As DataSet
        ds = dbt.GetDataSet(consulta)
        Grid_OC.DataSource = ds
        Grid_OC.DataBind()
        Dim consulta2 As String = "Select * from ordenesdecompra oc " & _
                                  " inner join comprobante c on oc.comp_id=c.comp_id" & _
                                  " inner join emisor e on e.emis_id = c.emis_id" & _
                                  " where  oc.estatus ='Activo'and  c.cuen_id =" & Session("cuen_id") & filtrosfechas1 & filtrosfechas2 & filtros & ordenar
        Dim ds2 As DataSet
        ds2 = dbt.GetDataSet(consulta2)
        Grid_OC_2.DataSource = ds2
        Grid_OC_2.DataBind()
    End Sub

    Protected Sub Grid_oc_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim fila1 As GridViewRow = Grid_OC_2.Rows(index)
        Dim celda1 As TableCell = fila1.Cells(4)
        Dim id_comp As String = celda1.Text
        If e.CommandName = "DESCARGAR" Then
            Dim fecha As String = DateTime.Now.ToString("yyyyMMdd HHmmss")
            imprimir(id_comp)
            crearZip("Ordene de Compra.pdf", archivo(id_comp), fecha)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "downloadFile('./zip/OC " & fecha & ".zip');", True)
        End If
    End Sub

    Sub crearZip(ByVal OC As String, ByVal factura As String, ByVal fecha As String)
        Try
            Dim c As Integer = 1
            Dim c2 As Integer = 1
            Using zip As ZipFile = New ZipFile()
                Dim ruta As String = Server.MapPath(OC)
                Dim ruta1 As String = Server.MapPath("/Archivos")
                Try
                    If factura <> "null" Then
                        zip.AddFile(ruta1 & "\" & factura, "PDF")
                    End If
                    zip.AddFile(ruta, "PDF")
                Catch ex As Exception
                End Try
                Dim rutaFolder As String = Server.MapPath("/zip")
                zip.Save(HttpContext.Current.Server.MapPath("zip\OC " & fecha & ".zip"))
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Function archivo(ByVal id As String)
        Dim val As String
        Try
            Dim ds As DataSet = dbt.GetDataSet("select * from contenido con inner join  comprobante com  on con.corre_id= com.corre_id where cont_extencion='PDF' and com.comp_id= " & id)
            val = ds.Tables(0).Rows(0).Item("cont_nombreFecha").ToString
        Catch ex As Exception
            val = "null"
        End Try
        Return val
    End Function

    Function firmas(ByVal num As String)
        Dim val As String
        Dim ds As DataSet
        ds = dbt.GetDataSet("select * from firmas where firm_id= " & num)
        val = ds.Tables(0).Rows(0).Item("Nombre").ToString & " " & ds.Tables(0).Rows(0).Item("app").ToString & " " & ds.Tables(0).Rows(0).Item("apm").ToString
        Return val
    End Function

    Sub imprimir(ByVal id As String)
        Dim rptdocument As New rpt_OC
        Dim myda As SqlDataAdapter
        Dim ds As New ds_OC
        Dim str = "select * from ordenesdecompra oc  " & _
                  " inner join comprobante c on oc.comp_id=c.comp_id" & _
                    " inner join Emisor e on e.emis_id=c.emis_id " & _
                    " inner join Receptor r on r.rece_id=c.rece_id " & _
                  " inner join  concepto con on c.comp_id = con.comp_id where c.comp_id= " & id

        myda = dbt.GetDataAdapterRpt(str, "ordenesdecambio")
        ds.EnforceConstraints = False
        myda.Fill(ds, "ordenesdecambio")
        rptdocument.SetDataSource(ds)
        rptdocument.SetParameterValue("Nombref1", firmas(1))
        rptdocument.SetParameterValue("Nombref2", firmas(2))
        rptdocument.SetParameterValue("Nombref3", firmas(3))
        Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim o As CrystalDecisions.Shared.ExportOptions
        o = New CrystalDecisions.Shared.ExportOptions
        o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
        o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
        filedest.DiskFileName = Server.MapPath("Ordene de Compra.pdf")
        o.ExportDestinationOptions = filedest.Clone
        rptdocument.Export(o)
    End Sub

    Protected Sub Radio_por_fecha_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_fecha.CheckedChanged
        ConsultarOrdenes()
    End Sub

    Protected Sub Radio_por_proveedor_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_proveedor.CheckedChanged
        ConsultarOrdenes()
    End Sub

    Private Sub Radio_por_folio_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_folio.CheckedChanged
        ConsultarOrdenes()
    End Sub

    Private Sub Radio_top_10_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_top_10.CheckedChanged
        ConsultarOrdenes()
    End Sub

    Private Sub Radio_top_20_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_top_20.CheckedChanged
        ConsultarOrdenes()
    End Sub

    Private Sub Radio_top_50_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_top_50.CheckedChanged
        ConsultarOrdenes()
    End Sub

    Private Sub Radio_top_todos_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_top_todos.CheckedChanged
        ConsultarOrdenes()
    End Sub

    Private Sub Radio_desc_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_desc.CheckedChanged
        ConsultarOrdenes()
    End Sub

    Private Sub Radio_asc_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_asc.CheckedChanged
        ConsultarOrdenes()
    End Sub

    Protected Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid_OC.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _row As System.Data.DataRowView = e.Row.DataItem
            Dim num As Integer
            num = 6
            Dim x As String = e.Row.Cells(num).Text
            e.Row.Cells(num).Text = Format(CDec(x), "$##,##0.00")
            e.Row.Cells(num).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

    Protected Sub btn_buscar_Click(sender As Object, e As EventArgs) Handles btn_buscar.Click
        ConsultarOrdenes()
    End Sub
End Class