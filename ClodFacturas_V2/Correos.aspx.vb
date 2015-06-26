Imports System.Net
Imports OfficeOpenXml.Style
Imports System.IO
Imports System.Drawing

Public Class Correos
    Inherits System.Web.UI.Page
    Dim dbt As New ToolsT.DbToolsT
    Dim mensajes As New messageTools(Me)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MySecurity.checkValidSession(Me) = False Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "Cargar()", True)
        End If
        If Not IsPostBack Then
            ConsultarCorreos(Session("cuen_id"), False)
        End If
    End Sub

    Protected Sub btn_buscar_Click(sender As Object, e As EventArgs) Handles btn_buscar.Click
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Sub ConsultarCorreos(ByVal id As String, ByVal b As Boolean)
        Try
            Dim ds2 As DataSet
            Dim filtros As String = " where cuen_id =" & Session("cuen_id") & " and corre_proveedor like '%" & txt_CorreProvvedor.Text & "%' and corre_motivo like '%" & txt_CorreMotivo.Text & "%' and estado='Activo' and corre_correo_proveedor like '%" & txt_correoProveedor.Text & "%'"
            Dim filtrosfechas As String = " "
            If txt_CorreFechaAl.Text = "" Or txt_CorreFechaDe.Text = "" Then
                b = False
            Else
                b = True
            End If
            If b = True Then
                filtrosfechas = " and corre_fecha between CONVERT(datetime,'" & txt_CorreFechaDe.Text.ToString.Substring(0, 10) & " 01:00:00.000') and CONVERT(datetime,'" & txt_CorreFechaAl.Text.ToString.Substring(0, 10) & " 23:59:59.000') "
            End If
            Dim por As String = ""
            If Radio_por_proveedor.Checked = True Then
                por = "corre_proveedor"
            ElseIf Radio_por_motivo.Checked = True Then
                por = "corre_motivo"
            ElseIf Radio_por_fecha.Checked Then
                por = "corre_fecha"
            End If
            Dim orden As String
            If Radio_orden_desc.Checked = True Then
                orden = " order by " & por & " desc"
            Else
                orden = " order by " & por & " asc"
            End If
            Dim mostrar As String = ""
            If Radio_mostrar_10.Checked = True Then
                mostrar = "top 10"
            ElseIf Radio_mostrar_50.Checked = True Then
                mostrar = " top 100"
            ElseIf Radio_mostrar_500.Checked = True Then
                mostrar = " top 500"
            ElseIf Radio_mostrar_todos.Checked = True Then
                mostrar = " "
            End If
            Dim mostarar_hms As String = ""
            If Chec_horas.Checked Then
                mostarar_hms = ",CONVERT(VARCHAR, getdate(), 108) as 'H/MIN/SEG' "
            Else
                mostarar_hms = " "
            End If
            ds2 = dbt.GetDataSet("select " & mostrar & " [corre_proveedor] as 'PROVEEDOR', [corre_motivo] as 'MOTIVO', Convert(varchar(10),CONVERT(date,corre_fecha,106),103) as 'FECHA'" & mostarar_hms & " from correos " & filtros & filtrosfechas & orden)
            Grid_CorreosRecibidos.DataSource = ds2
            Grid_CorreosRecibidos.DataBind()

            ds2 = dbt.GetDataSet("select " & mostrar & " * from correos" & filtros & filtrosfechas & orden)
            Grid_CorreosRecibidos2.DataSource = ds2
            Grid_CorreosRecibidos2.DataBind()
            txt_total.Text = ds2.Tables(0).Rows.Count.ToString
        Catch ex As Exception
            mensajes.setError("Erro al consultar los correos:  " & ex.Message, 5000)
        End Try
        che_Stodos.Checked = False
    End Sub

    Protected Sub Grid_facturas_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim fila1 As GridViewRow = Grid_CorreosRecibidos2.Rows(index)
            Dim celda As TableCell = fila1.Cells(1)
            Dim id As String = celda.Text
            If e.CommandName = "mostrar" Then
                ConsultarArchivos(id)
                MPE.Show()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub ConsultarArchivos(ByVal id As String)
        Dim ds2 As DataSet
        ds2 = dbt.GetDataSet("select cont_extencion as EXTENSIÓN, cont_nombre as ARCHIVO  from contenido where corre_id =  " & id)
        Grid_Archivos.DataSource = ds2
        Grid_Archivos.DataBind()
        ds2 = dbt.GetDataSet("select cont_extencion as Extencion, cont_nombreFecha as Archivo  from contenido where corre_id =  " & id)
        Grid_Archivos2.DataSource = ds2
        Grid_Archivos2.DataBind()
    End Sub

    Protected Sub grid2Click(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid_Archivos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim fila1 As GridViewRow = Grid_Archivos2.Rows(index)
        Dim celda As TableCell = fila1.Cells(3)
        Dim archivo As String = celda.Text
        If e.CommandName = "mostrar" Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "window.open('Archivos\\" & archivo & "','CONTRATO','status = 1,width=600, resizable = 1');", True)
        ElseIf e.CommandName = "descargar" Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "downloadFile('Archivos\\" & archivo & "');", True)
        End If
    End Sub

    Protected Sub Radio_por_proveedor_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_proveedor.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub Radio_por_motivo_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_motivo.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub Radio_por_fecha_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_por_fecha.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub Radio_mostrar_todos_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_mostrar_todos.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub Radio_mostrar_10_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_mostrar_10.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub Radio_mostrar_50_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_mostrar_50.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub Radio_orden_acs_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_orden_acs.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub Radio_orden_desc_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_orden_desc.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub Chec_horas_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_horas.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub Radio_mostrar_500_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_mostrar_500.CheckedChanged
        ConsultarCorreos(Session("cuen_id"), True)
    End Sub

    Protected Sub btn_crearreporte_Click(sender As Object, e As ImageClickEventArgs) Handles btn_crearreporte.Click
        Dim ds As DataSet
        Dim id_corre As String = Seleccionados()
        If id_corre = "null" Then
            mensajes.setMessage("Debe seleccionar como mínimo un correo.", 5000)
        Else
            ds = dbt.GetDataSet("  select * from correos where corre_id in " & id_corre)
        End If
        If Seleccionados() = "null" Then
            mensajes.setMessage("Debe seleccionar como mínimo un correo.", 5000)
        Else
            makeExcelReport(ds, "Reporte", id_corre)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "downloadFile('Reporte.xlsx');", True)
        End If
    End Sub

    Function Seleccionados() As String
        Dim val As String = "("
        For i As Integer = 0 To Grid_CorreosRecibidos.Rows.Count - 1
            Dim ch As CheckBox = Grid_CorreosRecibidos.Rows(i).FindControl("CheckBox1")
            If ch.Checked Then
                If val = "(" Then
                    val = val & Grid_CorreosRecibidos2.Rows(i).Cells(1).Text
                Else
                    val = val & "," & Grid_CorreosRecibidos2.Rows(i).Cells(1).Text
                End If
            End If
        Next
        val = val & ")"
        If val = "()" Then
            val = "null"
        End If
        Return val
    End Function

    Function makeExcelReport(ds As DataSet, filename As String, ByVal id_comp As String) As String
        Try
            Dim package = New OfficeOpenXml.ExcelPackage(New FileInfo(HttpContext.Current.Server.MapPath("Reporte.xlsx")))
            Dim sheet = package.Workbook.Worksheets(1)
            Dim col = 0, row = 0
            row = 3
            col = 1
            sheet.Cells.Clear()
            sheet.Cells(row, 2).Value = ("Proveedor")
            sheet.Cells(row, 3).Value = ("Correo")
            sheet.Cells(row, 4).Value = ("Motivo")
            sheet.Cells(row, 5).Value = ("Cuerpo")
            sheet.Cells(row, 6).Value = ("Fecha")
            For c As Integer = 2 To 6
                'Color
                sheet.Cells(row, c).Style.Fill.PatternType = ExcelFillStyle.Solid
                sheet.Cells(row, c).Style.Fill.BackgroundColor.SetColor(Color.DeepSkyBlue)
                'Bordes
                sheet.Cells(row, c).Style.Border.Left.Style = ExcelBorderStyle.Thin
                sheet.Cells(row, c).Style.Border.Right.Style = ExcelBorderStyle.Thin
                sheet.Cells(row, c).Style.Border.Top.Style = ExcelBorderStyle.Thin
                sheet.Cells(row, c).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
            Next
            For c As Integer = 0 To ds.Tables(0).Rows.Count - 1
                row += 1
                sheet.Cells(row, 2).Value = ds.Tables(0).Rows(c).Item("corre_proveedor")
                sheet.Cells(row, 3).Value = ds.Tables(0).Rows(c).Item("corre_correo_proveedor")
                sheet.Cells(row, 4).Value = ds.Tables(0).Rows(c).Item("corre_motivo")
                sheet.Cells(row, 5).Value = ds.Tables(0).Rows(c).Item("corre_cuerpo")
                sheet.Cells(row, 6).Value = ds.Tables(0).Rows(c).Item("corre_fecha")
            Next
            For i As Integer = 1 To 14
                sheet.Column(i).AutoFit()
            Next
            seleccionarTodos(False)
            package.SaveAs(New FileInfo(HttpContext.Current.Server.MapPath(filename & ".xlsx")))
            Return filename & ".xlsx"
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub che_Stodos_CheckedChanged(sender As Object, e As EventArgs) Handles che_Stodos.CheckedChanged
        If che_Stodos.Checked Then
            seleccionarTodos(True)
        Else
            seleccionarTodos(False)
        End If
    End Sub

    Sub seleccionarTodos(ByVal val As Boolean)
        For i As Integer = 0 To Grid_CorreosRecibidos.Rows.Count - 1
            Dim ch As CheckBox = Grid_CorreosRecibidos.Rows(i).FindControl("CheckBox1")
            ch.Checked = val
        Next
    End Sub

    Private Sub btn_eliminarFacturas_Click(sender As Object, e As ImageClickEventArgs) Handles btn_eliminarFacturas.Click
        If Seleccionados() = "null" Then
            mensajes.setMessage("Debe seleccionar como mínimo un correo.", 5000)
        Else
            MPE_confir.Show()
        End If
    End Sub

    Protected Sub btn_eliminarFacturas2_Click(sender As Object, e As EventArgs) Handles btn_eliminarFacturas2.Click
        eliminarFacturas()
        MPE_confir.Hide()
        ConsultarCorreos(Session("cuen_id"), False)
    End Sub

    Sub eliminarFacturas()
        For i As Integer = 0 To Grid_CorreosRecibidos.Rows.Count - 1
            Dim fila1 As GridViewRow = Grid_CorreosRecibidos.Rows(i)
            Dim fila2 As GridViewRow = Grid_CorreosRecibidos2.Rows(i)
            Dim celda3 As TableCell = fila2.Cells(1)
            Dim id_corre As String = celda3.Text
            Dim ch As CheckBox = Grid_CorreosRecibidos.Rows(i).FindControl("CheckBox1")
            If ch.Checked = True Then
                dbt.ExecuteNonQuery("update correos set estado= 'Eliminado' where corre_id = " & id_corre)
            End If
        Next
    End Sub

    Private Sub btn_cerrarConfir_Click(sender As Object, e As ImageClickEventArgs) Handles btn_cerrarConfir.Click
        MPE_confir.Hide()
    End Sub

End Class