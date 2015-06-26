Imports System.Drawing
Imports Ionic.Zip
Imports System.Net
Imports System.IO
Imports OfficeOpenXml.Style
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class Facturas
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
            che_nom.Checked = True
            NombreZip("cont_nombre", "Factura")
            PreventingDoubleSubmit(btn_cerarZip)
            PreventingDoubleSubmit(btn_repoeteexel)
            Permisos()
            oc_selecc.Text = "null"

            Grid_facturas.Columns(6).Visible = False
            Grid_facturas.Columns(5).Visible = True

            btn_gnerar_oc.Visible = False

        End If
        pintar()

        If che_oc.Checked Then
            pintarPagados()
        End If


    End Sub


    Sub Permisos()
        Try
            Dim ds As DataSet = dbt.GetDataSet("select * from usuario as u inner join permisos p on u.usua_id =p.usua_id where u.usua_id=" & Session("usua_id"))



            If ds.Tables(0).Rows(0).Item("baja") = 0 Then
                btn_eliminarFacturas.Visible = False
            Else

            End If
        Catch ex As Exception

        End Try
        

    End Sub
    Private Sub btn_buscar_Click(sender As Object, e As EventArgs) Handles btn_buscar.Click
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub btn_cerarZip_Click(sender As Object, e As EventArgs) Handles btn_cerarZip.Click
        crearZip()
        System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "downloadFile('./zip/" & txt_NombreZip.Text & ".zip');", True)
    End Sub

    Protected Sub btn_eliminarFacturas_Click(sender As Object, e As ImageClickEventArgs) Handles btn_eliminarFacturas.Click

        oc_selecc.Text = Seleccionados()

        If oc_selecc.Text = "null" Then
            mensajes.setMessage("Debe seleccionar como mínimo una factura.", 5000)
        Else
            MPE_confir.Show()
        End If
    End Sub

    Protected Sub btn_crearreporte_Click(sender As Object, e As ImageClickEventArgs) Handles btn_crearreporte.Click
        If Seleccionados() = "null" Then
            mensajes.setMessage("Debe seleccionar como mínimo una factura.", 5000)
        Else
            MPE_reporte.Show()
        End If
    End Sub

    Protected Sub btn_inicio_Click(sender As Object, e As ImageClickEventArgs) Handles btn_inicio.Click
        If Seleccionados() = "null" Then
            mensajes.setMessage("Debe seleccionar como mínimo una factura", 5000)
        Else
            MPEDescargas.Show()
        End If
    End Sub

    Protected Sub btn_cerrarConfir_Click(sender As Object, e As EventArgs) Handles btn_cerrarConfir.Click
        MPE_confir.Hide()
    End Sub

    Protected Sub che_Stodos_CheckedChanged(sender As Object, e As EventArgs) Handles che_Stodos.CheckedChanged
        If che_Stodos.Checked Then


            If che_oc.Checked Then
                seleccionarTodos(True, "Check_oc")

            Else
                seleccionarTodos(True, "CheckBox1")
            End If


        Else


            If che_oc.Checked Then
                seleccionarTodos(False, "Check_oc")
            Else
                seleccionarTodos(False, "CheckBox1")

            End If


        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ConsultarFacturas(Session("cuen_id"), False)
        che_nom.Checked = True
        NombreZip("cont_nombre", "Factura")
        pintar()
        mensajes.setMessage("PDF guardado con exito.", 5000)
        MPE_subirPDF.Hide()
    End Sub

    Protected Sub btn_repoeteexel_Click(sender As Object, e As EventArgs) Handles btn_repoeteexel.Click
        crearexcel()
    End Sub

    Protected Sub btn_eliminarFacturas2_Click(sender As Object, e As EventArgs) Handles btn_eliminarFacturas2.Click
        eliminarFacturas()
        MPE_confir.Hide()
    End Sub

    Protected Sub ImageButton4_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton4.Click
        txt_NombreArchivos.Text = ""
        txt_NombreArchivos2.Text = ""
        che_nom.Checked = False
        che_folio.Checked = False
        che_Fecha.Checked = False
        che_RFC_provee.Checked = False
        che_nom_provee.Checked = False
    End Sub

    Protected Sub Grid_facturas_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim fila1 As GridViewRow = Grid_facturas2.Rows(index)
       
        'Dim celda2 As TableCell = fila1.Cells(6)
        'Dim id_xml As String = celda2.Text


        Dim celda3 As TableCell = fila1.Cells(11)
        Dim id_pdf As String = celda3.Text
        Dim celda1 As TableCell = fila1.Cells(12)
        Dim id_comp As String = celda1.Text
        Dim celda4 As TableCell = fila1.Cells(13)
        Dim subido_por As String = celda4.Text
        Dim celda5 As TableCell = fila1.Cells(14)
        Dim id_corre As String = celda5.Text
        Dim celda6 As TableCell = fila1.Cells(15)
        Dim archivo As String = celda6.Text


        txt_idCRT.Text = id_comp

        If e.CommandName = "xml" Then

            If subido_por = "sat" Then
                Dim nombre_archivo As String = cadena_Random()
                CrearArchivo(id_comp, nombre_archivo)
                System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "window.open('Archivos\\" & nombre_archivo & ".xml" & "','CONTRATO','status = 1,width=600, resizable = 1');", True)
            Else
                System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "window.open('Archivos\\" & archivo & "','CONTRATO','status = 1,width=600, resizable = 1');", True)
            End If

        ElseIf e.CommandName = "pdf" Then


            'CrearArchivoqr(id_comp, "gatoooo")

            imprimir(id_comp)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "window.open('Ordene de Compra.pdf" & "','CONTRATO','status = 1,width=600, resizable = 1');", True)


            ''MsgBox("prooo")
            '
            '


            'mostrarPDF(id_pdf, id_comp, subido_por, id_corre)
        ElseIf e.CommandName = "conceptos" Then
            ConsultarConceptos(id_comp)
            mostarGrid(True, False, False)
        ElseIf e.CommandName = "retencion" Then
            ConsultarRetencion(id_comp)
            mostarGrid(False, True, False)
        ElseIf e.CommandName = "traslados" Then
            ConsultarTraslado(id_comp)
            mostarGrid(False, False, True)
        End If
    End Sub



    Sub imprimir(ByVal id As String)
        Dim rptdocument As New rpt_Factura
        Dim myda As SqlDataAdapter
        Dim ds As New ds_Factura


        'Dim str = "select * from qr  where comp_id= " & id

        Dim str = " select  *  " & _
                " from comprobante com " & _
                " inner join qr as qr on qr.comp_id=com.comp_id" & _
                " inner join Emisor e on e.emis_id=com.emis_id " & _
                " inner join Receptor r on r.rece_id=com.rece_id" & _
                " left join expedidoen expe on e.emis_id=expe.emis_id" & _
                " left join DomicilioFiscal domif on domif.domifical_id = e.domifical_id" & _
                " left join Domicilio d on d.domi_id = r.domi_id " & _
                " left join RegimenFiscal regi on regi.regi_id= e.regi_id" & _
                " where com.cuen_id =1 and  com.tipo= 'Ingreso' and subido_por='sat'and com.comp_id= " & id



        myda = dbt.GetDataAdapterRpt(str, "factura")
        ds.EnforceConstraints = False
        myda.Fill(ds, "factura")
        rptdocument.SetDataSource(ds)

        Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim o As CrystalDecisions.Shared.ExportOptions
        o = New CrystalDecisions.Shared.ExportOptions
        o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
        o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
        filedest.DiskFileName = Server.MapPath("Ordene de Compra.pdf")
        o.ExportDestinationOptions = filedest.Clone
        rptdocument.Export(o)
    End Sub

    Protected Sub CrearArchivoqr(ByVal dt As DataTable, ByVal nombre_archivo As String)
        Try
            Dim bytes() As Byte = CType(dt.Rows(0)("archivo"), Byte())
            Dim Ruta As String = Server.MapPath("/Archivos")
            File.WriteAllBytes(Ruta & "\" & nombre_archivo & ".png", bytes)
        Catch ex As Exception

        End Try

    End Sub


    Protected Sub CrearArchivo(ByVal dt As DataTable, ByVal nombre_archivo As String)
        Try
            Dim bytes() As Byte = CType(dt.Rows(0)("archivo"), Byte())
            Dim Ruta As String = Server.MapPath("/Archivos")
            File.WriteAllBytes(Ruta & "\" & nombre_archivo & ".xml", bytes)
        Catch ex As Exception

        End Try

    End Sub



    Sub CrearArchivoqr(ByVal id As String, ByVal nombre_archivo As String)
        Dim strQuery As String = "select * from qr  " & _
                                 " where comp_id=@comp_id "
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@comp_id", SqlDbType.Int).Value = id
        Dim dt As DataTable = GetData(cmd)
        If dt IsNot Nothing Then
            CrearArchivoqr(dt, nombre_archivo)
        End If
    End Sub




    Sub CrearArchivo(ByVal id As String, ByVal nombre_archivo As String)
        Dim strQuery As String = "select * from comprobante as com " & _
                                 " inner join xml_sat c_xml on c_xml.xml_sat_id=com.xml_id " & _
                                 " where comp_id=@comp_id "
        Dim cmd As SqlCommand = New SqlCommand(strQuery)
        cmd.Parameters.Add("@comp_id", SqlDbType.Int).Value = id
        Dim dt As DataTable = GetData(cmd)
        If dt IsNot Nothing Then
            CrearArchivo(dt, nombre_archivo)
        End If
    End Sub


    Function cadena_Random()
        Dim obj As New Random()

        Dim posibles As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"

        Dim longitud As Integer = posibles.Length

        Dim letra As Char

        Dim longitudnuevacadena As Integer = 15

        Dim nuevacadena As String = ""

        For i As Integer = 0 To longitudnuevacadena - 1

            letra = posibles(obj.[Next](longitud))

            nuevacadena += letra.ToString()

        Next

        Return nuevacadena
    End Function

    Public Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("currentConn").ConnectionString

        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception

            Return Nothing
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function


    Protected Sub che_nom_CheckedChanged(sender As Object, e As EventArgs) Handles che_nom.CheckedChanged
        If che_nom.Checked Then
            NombreZip("cont_nombre", "Factura")
        Else
            QuitarNombreZip("cont_nombre", "Factura")
        End If
    End Sub

    Protected Sub che_folio_CheckedChanged(sender As Object, e As EventArgs) Handles che_folio.CheckedChanged
        If che_folio.Checked Then
            NombreZip("folio", "Folio")
        Else
            QuitarNombreZip("folio", "Folio")
        End If
    End Sub

    Protected Sub che_Fecha_CheckedChanged(sender As Object, e As EventArgs) Handles che_Fecha.CheckedChanged
        If che_Fecha.Checked Then
            NombreZip("com.fecha", "Fecha")
        Else
            QuitarNombreZip("com.fecha", "Fecha")
        End If
    End Sub

    Protected Sub che_RFC_provee_CheckedChanged(sender As Object, e As EventArgs) Handles che_RFC_provee.CheckedChanged
        If che_RFC_provee.Checked Then
            NombreZip("e.rfc", "RFC Proveedor")
        Else
            QuitarNombreZip("e.rfc", "RFC Proveedor")
        End If
    End Sub

    Protected Sub che_nom_provee_CheckedChanged(sender As Object, e As EventArgs) Handles che_nom_provee.CheckedChanged
        If che_nom_provee.Checked Then
            NombreZip("e.nombre", "Nombre Proveedor")
        Else
            QuitarNombreZip("e.nombre", "Nombre Proveedor")
        End If
    End Sub

    Private Sub Grid_pdfEnCorreo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grid_pdfEnCorreo.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim fila1 As GridViewRow = Grid_pdfEnCorreo2.Rows(index)
            Dim celda1 As TableCell = fila1.Cells(0)
            Dim id_pdf As String = celda1.Text
            dbt.ExecuteNonQuery("update comprobante set pdf_id= " & id_pdf & " where comp_id= " & txt_idCRT.Text)
            ConsultarFacturas(Session("cuen_id"), False)
            mensajes.setMessage("Actualización de PDF correcta ", 5000)
            MPE_subirPDF.Hide()
        Catch ex As Exception
            mensajes.setError("Error al actulizar PDF", 5000)
        End Try
    End Sub

    Protected Sub Chec_version_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_version.CheckedChanged
        If Chec_version.Checked = True Then
            ColumnasExcel("version as Versión")
        Else
            QuitarColumnasExcel("version as Versión")
        End If
    End Sub

    Protected Sub Chec_serie_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_serie.CheckedChanged
        If Chec_serie.Checked Then
            ColumnasExcel("serie as Serie")
        Else
            QuitarColumnasExcel("serie as Serie")
        End If
    End Sub

    Protected Sub Chec_folio_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_folio.CheckedChanged
        If Chec_folio.Checked Then
            ColumnasExcel("Folio as Folio")
        Else
            QuitarColumnasExcel("Folio as Folio")
        End If
    End Sub

    Protected Sub Chec_fecha_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_fecha.CheckedChanged
        If Chec_fecha.Checked Then
            ColumnasExcel("fecha as Fecha")
        Else
            QuitarColumnasExcel("fecha as Fecha")
        End If
    End Sub

    Protected Sub Check_sello_CheckedChanged(sender As Object, e As EventArgs) Handles Check_sello.CheckedChanged
        If Check_sello.Checked Then
            ColumnasExcel("sello as Sello")
        Else
            QuitarColumnasExcel("sello as Sello")
        End If
    End Sub

    Protected Sub Chec_formaDePago_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_formaDePago.CheckedChanged
        If Chec_formaDePago.Checked Then
            ColumnasExcel("fomaDePago as 'Forma de pago'")
        Else
            QuitarColumnasExcel("fomaDePago as 'Forma de pago'")
        End If
    End Sub

    Protected Sub Chec_noCertificado_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_noCertificado.CheckedChanged
        If Chec_noCertificado.Checked Then
            ColumnasExcel("noCertificado as 'N. certificado'")
        Else
            QuitarColumnasExcel("noCertificado as 'N. certificado'")
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

    Protected Sub Chec_seleccionarColumnas_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_seleccionarColumnas.CheckedChanged
        If Chec_seleccionarColumnas.Checked Then
            seleccionarT(True)
            txt_ColumnasSel.Text = "version as Versión, serie as Serie,folio as Folio,fecha as Fecha,sello as Sello ,fomaDePago as 'Forma de pago',noCertificado as 'N. certificado',certificado as Certificado,condicionesDePago as 'Condiciones de pago',  Descuento as Descuento, MotivoDescuento as 'Motivo de descuento',TipoCambio as 'Tipo de cambio',Moneda as 'Moneda',TipoDeComprobante as 'Tipo de comprobante',MetodoDePago as 'Método de pago',LugarExpedicion 'Lugar de expredición',NumCtaPago as 'No CTA pago',e.rfc 'Proveedor RFC',e.nombre 'Proveedor Nombre' "
        Else
            seleccionarT(False)
            txt_ColumnasSel.Text = ""
        End If
    End Sub

    Protected Sub radio1__CheckedChanged(sender As Object, e As EventArgs) Handles Radio_examninados.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub
    Protected Sub radio2_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_correo.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Sub pintar()
        Try
            For i As Integer = 0 To Grid_facturas.Rows.Count - 1
                Dim fila1 As GridViewRow = Grid_facturas.Rows(i)
                Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
                Dim celda1 As TableCell = fila1.Cells(4)
                Dim celda2 As TableCell = fila2.Cells(7)
                Dim val As Integer = celda2.Text
                If val = 0 Then
                    celda1.BackColor = System.Drawing.Color.FromName("#E20F20")
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Sub crearZip()
        Try
            Dim c As Integer = 1
            Dim c2 As Integer = 1
            Using zip As ZipFile = New ZipFile()
                For i As Integer = 0 To Grid_facturas.Rows.Count - 1
                    Dim fila1 As GridViewRow = Grid_facturas.Rows(i)
                    Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
                    'Dim celda1 As TableCell = fila1.Cells(4)

                    Dim celda2 As TableCell = fila2.Cells(15)
                    Dim celda3 As TableCell = fila2.Cells(11)
                    Dim celdaID As TableCell = fila2.Cells(12)
                    Dim celda4 As TableCell = fila2.Cells(13)
                    Dim subido_por As String = celda4.Text
                    Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl("CheckBox1")
                    Dim ruta As String = Server.MapPath("/Archivos")
                    Dim Path_file As String = Server.MapPath("/Archivos")
                    Dim id_pdf As String = celda3.Text
                    Dim nom_pdf As String
                    Dim nom_pdfnew As String
                    Dim id_comp As String = celdaID.Text
                    Dim nom_xml As String


                    Dim celda6 As TableCell = fila2.Cells(6)
                    Dim nombre_proveedor As String = celda6.Text





                    If subido_por = "sat" Then
                        Dim nombre_archivo As String = cadena_Random()
                        CrearArchivo(id_comp, nombre_archivo)
                        nom_xml = nombre_archivo & ".xml"
                    Else
                        nom_xml = celda2.Text
                    End If



                    Dim nom_xmlnew As String

                    If ch.Checked = True Then
                        '-----------------------Agregar pdf-------------------------------------
                        If che_pdf.Checked Then
                            If id_pdf > 0 Then
                                Dim ds As DataSet = dbt.GetDataSet("select * from contenido where cont_id= " & id_pdf)
                                nom_pdf = ds.Tables(0).Rows(0).Item("cont_nombrefecha")
                                nom_pdfnew = CerarNombre(id_comp, subido_por) & ".pdf"
                                Try
                                    My.Computer.FileSystem.RenameFile(ruta & "\" & nom_pdf, nom_pdfnew)
                                Catch ex As Exception
                                End Try
                                If che_carpetas.Checked Then
                                    Try
                                        zip.AddFile(ruta & "\" & nom_pdfnew, "PDF")
                                    Catch ex As Exception
                                        Try
                                            nom_pdf = nom_pdfnew.Replace(".pdf", "")
                                            My.Computer.FileSystem.CopyFile( _
                                         Path_file & "\" & nom_pdfnew, _
                                         Path_file & "\" & nom_pdf & "(" & c & ").pdf", overwrite:=False)
                                        Catch ex2 As Exception
                                            'Ya existe el archivo
                                        End Try
                                        zip.AddFile(ruta & "\" & nom_pdf & "(" & c & ").pdf", "PDF")
                                        c += 1
                                    End Try
                                Else
                                    Try
                                        zip.AddFile(ruta & "\" & nom_pdfnew, "")
                                    Catch ex As Exception
                                        Try
                                            nom_pdf = nom_pdfnew.Replace(".pdf", "")
                                            My.Computer.FileSystem.CopyFile( _
                                         Path_file & "\" & nom_pdfnew, _
                                         Path_file & "\" & nom_pdf & "(" & c & ").pdf", overwrite:=False)

                                        Catch ex2 As Exception
                                            'Ya existe el archivo
                                        End Try
                                        zip.AddFile(ruta & "\" & nom_pdf & "(" & c & ").pdf", "")
                                        c += 1
                                    End Try
                                End If
                            End If
                        End If
                        '-----------------------Agregar xml-------------------------------------
                        If che_xml.Checked Then


                            nom_xmlnew = CerarNombre(id_comp, subido_por) & ".xml"
                            Try
                                My.Computer.FileSystem.RenameFile(ruta & "\" & nom_xml, nom_xmlnew)
                            Catch ex As Exception
                            End Try
                            If che_carpetas.Checked Then


                                Dim carpeta As String

                                If che_carpetas_clientes.Checked Then
                                    carpeta = nombre_proveedor

                                Else
                                    carpeta = "XML"
                                End If




                                Try
                                    zip.AddFile(ruta & "\" & nom_xmlnew, carpeta)
                                Catch ex As Exception
                                    'ya se guardo un archivo con el mismo nombre
                                    Try
                                        nom_xml = nom_xmlnew.Replace(".xml", "")
                                        My.Computer.FileSystem.CopyFile( _
                                     Path_file & "\" & nom_xmlnew, _
                                     Path_file & "\" & nom_xml & "(" & c2 & ").xml", overwrite:=False)
                                    Catch ex2 As Exception
                                        'Ya existe el archivo
                                    End Try
                                    zip.AddFile(ruta & "\" & nom_xml & "(" & c2 & ").xml", carpeta)
                                    c2 += 1
                                End Try
                            Else
                                Try
                                    zip.AddFile(ruta & "\" & nom_xmlnew, "")
                                Catch ex As Exception
                                    'ya se guardo un archivo con el mismo nombre
                                    Try
                                        nom_xml = nom_xmlnew.Replace(".xml", "")
                                        My.Computer.FileSystem.CopyFile( _
                                     Path_file & "\" & nom_xmlnew, _
                                     Path_file & "\" & nom_xml & "(" & c2 & ").xml", overwrite:=False)
                                    Catch ex2 As Exception
                                        'Ya existe el archivo
                                    End Try
                                    zip.AddFile(ruta & "\" & nom_xml & "(" & c2 & ").xml", "")
                                    c2 += 1
                                End Try
                            End If
                        End If
                    End If
                    c = 1
                    c2 = 1
                Next
                Dim rutaFolder As String = Server.MapPath("/zip")
                zip.Save(HttpContext.Current.Server.MapPath("zip\" & txt_NombreZip.Text & ".zip"))
                regresarNombrePDF()
                regresarNombreXML()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub regresarNombrePDF()
        Try
            For i As Integer = 0 To Grid_facturas.Rows.Count - 1
                Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
                Dim celda3 As TableCell = fila2.Cells(7)
                Dim celdaID As TableCell = fila2.Cells(5)
                Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl("CheckBox1")
                Dim ruta As String = Server.MapPath("/Archivos")
                Dim id_pdf As String = celda3.Text
                Dim nom_pdf As String
                Dim nom_pdfnew As String
                Dim id_comp As String = celdaID.Text
                If ch.Checked = True Then
                    If id_pdf > 0 Then
                        Dim ds As DataSet = dbt.GetDataSet("select * from contenido where cont_id= " & id_pdf)
                        nom_pdf = ds.Tables(0).Rows(0).Item("cont_nombrefecha")
                        nom_pdfnew = CerarNombre(id_comp, "") & ".pdf"
                        Try
                            My.Computer.FileSystem.RenameFile(ruta & "\" & nom_pdfnew, nom_pdf)
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Next
        Catch ex As Exception


        End Try
    End Sub
    Sub regresarNombreXML()
        Try
            For i As Integer = 0 To Grid_facturas.Rows.Count - 1
                Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
                Dim celda3 As TableCell = fila2.Cells(6)
                Dim celdaID As TableCell = fila2.Cells(5)
                Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl("CheckBox1")
                Dim ruta As String = Server.MapPath("/Archivos")
                Dim id_pdf As String = celda3.Text
                Dim nom_pdf As String
                Dim nom_pdfnew As String
                Dim id_comp As String = celdaID.Text
                If ch.Checked = True Then
                    Dim ds As DataSet = dbt.GetDataSet("select * from contenido where cont_id= " & id_pdf)
                    nom_pdf = ds.Tables(0).Rows(0).Item("cont_nombrefecha")
                    nom_pdfnew = CerarNombre(id_comp, "") & ".xml"
                    Try
                        My.Computer.FileSystem.RenameFile(ruta & "\" & nom_pdfnew, nom_pdf)
                    Catch ex As Exception
                    End Try
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Sub seleccionarTodos(ByVal val As Boolean, ByVal check As String)




        For i As Integer = 0 To Grid_facturas.Rows.Count - 1


            Dim fila1 As GridViewRow = Grid_facturas.Rows(i)
            Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)

          


            Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl(check)

            If che_oc.Checked Then
                Dim celda2 As TableCell = fila2.Cells(16)
                Dim val2 As String = celda2.Text



                If val2 = "Pagado" Then
                    ch.Checked = False
                Else
                    ch.Checked = val
                End If


            Else
                ch.Checked = val
            End If



        Next
    End Sub

    Sub QuitarNombreZip(ByVal texto As String, ByVal texto2 As String)
        txt_NombreArchivos.Text = txt_NombreArchivos.Text.Replace(texto & "-", "")
        txt_NombreArchivos.Text = txt_NombreArchivos.Text.Replace("-" & texto, "")
        txt_NombreArchivos.Text = txt_NombreArchivos.Text.Replace(texto, "")
        txt_NombreArchivos2.Text = txt_NombreArchivos2.Text.Replace(texto2 & "-", "")
        txt_NombreArchivos2.Text = txt_NombreArchivos2.Text.Replace("-" & texto2, "")
        txt_NombreArchivos2.Text = txt_NombreArchivos2.Text.Replace(texto2, "")
    End Sub

    Sub NombreZip(ByVal texto As String, ByVal texto2 As String)
        If txt_NombreArchivos.Text = "" Then
            txt_NombreArchivos.Text = texto
            txt_NombreArchivos2.Text = texto2
        Else
            txt_NombreArchivos.Text = txt_NombreArchivos.Text & "-" & texto
            txt_NombreArchivos2.Text = txt_NombreArchivos2.Text & "-" & texto2
        End If
    End Sub

    Sub QuitarColumnasExcel(ByVal texto As String)
        txt_ColumnasSel.Text = txt_ColumnasSel.Text.Replace(texto & ",", "")
        txt_ColumnasSel.Text = txt_ColumnasSel.Text.Replace("," & texto, "")
        txt_ColumnasSel.Text = txt_ColumnasSel.Text.Replace(texto, "")
    End Sub

    Sub ColumnasExcel(ByVal texto As String)
        If txt_ColumnasSel.Text = "" Then
            txt_ColumnasSel.Text = texto
        Else
            txt_ColumnasSel.Text = txt_ColumnasSel.Text & "," & texto
        End If
    End Sub

    Sub seleccionarT(ByVal t As Boolean)
        Chec_version.Checked = t
        Chec_serie.Checked = t
        Chec_folio.Checked = t
        Chec_fecha.Checked = t
        Check_sello.Checked = t
        Chec_formaDePago.Checked = t
        Chec_noCertificado.Checked = t
        Chec_concepto.Checked = t
        Chec_Traslado.Checked = t
        Chec_Retencio.Checked = t
        Chec_total_indivi.Checked = t
        Chec_total_total.Checked = t
        Chec_Certificado.Checked = t
        Chec_Condicionespago.Checked = t
        Chec_Descuento.Checked = t
        Chec_Motivodescuento.Checked = t
        Chec_Tipocambio.Checked = t
        Chec_Moneda.Checked = t
        Chec_Tipocomprobante.Checked = t
        Chec_Metodopago.Checked = t
        Chec_Lugarexpedicion.Checked = t
        Chec_Nuctapago.Checked = t
        chec_proveedor_rfc.Checked = t
        chec_proveedor_nombre.Checked = t
    End Sub

    Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)

        Try
            Dim fileOK As Boolean = False
            Dim fileExtension As String
            fileExtension = System.IO.Path. _
                GetExtension(AsyncFileUpload1.FileName).ToLower()
            Dim allowedExtensions As String() = _
                {".pdf"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    fileOK = True
                End If
            Next

            If fileOK = True Then
                Dim filename As String = System.IO.Path.GetFileName(AsyncFileUpload1.FileName)
                Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() & "\Archivos\"
                filename = CambiarCaracteres(DateTime.Now.ToString("yyyyMMdd") & "" & filename)
                AsyncFileUpload1.SaveAs(Server.MapPath("Archivos\") + filename)
                'Guardar en BD
                dbt.ExecuteNonQuery("insert into contenido values ('pdf','" & AsyncFileUpload1.FileName & "','" & filename & "','" & path & "','0')")
                Dim cont_id As String = ultimoID("contenido", "cont_id")
                dbt.ExecuteNonQuery("update comprobante set pdf_id=" & cont_id & " where comp_id= " & txt_idCRT.Text)
            Else
                mensajes.setError("Debe seleccionar un archivo .pdf", 5000)
            End If
        Catch ex As Exception
            mensajes.setError("Error al subir PDF.", 5000)
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

    Sub eliminarFacturas()
        'For i As Integer = 0 To Grid_facturas.Rows.Count - 1
        '    Dim fila1 As GridViewRow = Grid_facturas.Rows(i)
        '    Dim fila2 As GridViewRow = Grid_facturas2.Rows(i)
        '    Dim celda1 As TableCell = fila1.Cells(4)
        '    Dim celda2 As TableCell = fila2.Cells(5)
        '    Dim celda3 As TableCell = fila2.Cells(6)
        '    Dim celda4 As TableCell = fila2.Cells(7)
        '    Dim id_comp As String = celda2.Text
        '    Dim id_xml As String = celda3.Text
        '    Dim id_pdf As String = celda4.Text
        '    Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl("CheckBox1")

        '    If ch.Checked = True Then
        '        dbt.ExecuteNonQuery("delete contenido where cont_id  = " & id_xml)
        '        If id_pdf > 0 Then
        '            dbt.ExecuteNonQuery("delete contenido where cont_id  = " & id_pdf)
        '        End If
        '        dbt.ExecuteNonQuery("delete comprobante where comp_id  = " & id_comp)
        '    End If
        'Next
        'ConsultarFacturas(Session("cuen_id"), False)

        dbt.ExecuteNonQuery("update comprobante  set estatus='Cancelado' where comp_id in " & oc_selecc.Text)

        ConsultarFacturas(Session("cuen_id"), False)
        pintarPagados()
        mensajes.setMessage("Factura cancelada con éxito.", 5000)
        oc_selecc.Text = "null"


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

        Dim estatus As String = " , Estatus as 'ESTATUS' "


        Try
            Dim tipodeUsuario As String = TipoUsuario(Session("usua_id"))
            If tipodeUsuario = "Master" Then
                estatus = " , Estatus as 'ESTATUS' "
                che_oc.Visible = True
            ElseIf tipodeUsuario = "Administrador" Then
                che_oc.Visible = False
                estatus = " "
            End If
        Catch ex As Exception

        End Try



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
        Dim mostarar_nomFcat_sat As String = ""

        Dim mostarar_certNum As String = ""
        If Chec_horas.Checked Then
            mostarar_hms = ",CONVERT(VARCHAR, getdate(), 108) as 'Horas/min/seg' "
        Else
            mostarar_hms = " "
        End If
        If Chec_nomfact.Checked Then
            mostarar_nomFcat = "cont_nombre  'FACTURA', "
            mostarar_nomFcat_sat = "c_xml.nombre   'FACTURA', "
        Else
            mostarar_nomFcat = " "
            mostarar_nomFcat_sat = " "
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

        Dim consulta_correos As String = "    select   " & mostarar_nomFcat & "  e.rfc 'PROVEEDOR RFC'   ,e.nombre 'PROVEEDOR NOMBRE',folio 'FOLIO', " & mostarar_certNum & "Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'FECHA', Total as 'IMPORTE' " & estatus & mostarar_hms & _
                             " from comprobante com" & _
                             " inner join contenido c_xml on c_xml.cont_id=com.xml_id" & _
                             " inner join Emisor e on e.emis_id=com.emis_id" & _
                             " inner join Receptor r on r.rece_id=com.rece_id" & _
                             " inner join correos c on c.corre_id = com.corre_id " & _
                             " where com.cuen_id =" & Session("cuen_id") & _
                             filtros & filtrosfechas & tipo

        Dim consulta_examinados As String = "select  " & mostarar_nomFcat & "  e.rfc 'Proveedor RFC'   ,e.nombre 'Proveedor Nombre',folio 'Folio', " & mostarar_certNum & "Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'Fecha',Total as 'IMPORTE'  " & estatus & mostarar_hms & _
                            " from comprobante com" & _
                            " inner join contenido c_xml on c_xml.cont_id=com.xml_id" & _
                            " inner join Emisor e on e.emis_id=com.emis_id" & _
                            " inner join Receptor r on r.rece_id=com.rece_id" & _
                            " where com.cuen_id =" & Session("cuen_id") & _
                            filtros & filtrosfechas & tipo


        Dim consulta_sat As String = "select  " & mostarar_nomFcat_sat & "  e.rfc 'Proveedor RFC'   ,e.nombre 'Proveedor Nombre',folio 'Folio', " & mostarar_certNum & "Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'Fecha',Total as 'IMPORTE'  " & estatus & mostarar_hms & _
                            " from comprobante com" & _
                            " inner join xml_sat c_xml on c_xml.xml_sat_id=com.xml_id " & _
                            " inner join Emisor e on e.emis_id=com.emis_id" & _
                            " inner join Receptor r on r.rece_id=com.rece_id" & _
                            " where com.cuen_id =" & Session("cuen_id") & _
                            tipo
        'filtros & filtrosfechas & tipo


        Dim consulta_1 As String


        If Radio_correo.Checked Then
            consulta_1 = consulta_correos & " and subido_por='Correo' "
        ElseIf Radio_examninados.Checked Then
            consulta_1 = consulta_examinados & " and subido_por='Examinado'"
        Else
            consulta_1 = consulta_correos & "  and subido_por='Correo' union all " & consulta_examinados & " and subido_por='Examinado' union all " & consulta_sat & " and subido_por='sat'"
        End If



        ds2 = dbt.GetDataSet("select" & top & " a.* from (  " & consulta_1 & ") as a " & ordenar)
        Grid_facturas.DataSource = ds2
        Grid_facturas.DataBind()




        '-------------------consulta 2

        Dim consulta_correos_2 As String = "    select   " & mostarar_nomFcat & "  e.rfc 'PROVEEDOR RFC'   ,e.nombre 'PROVEEDOR NOMBRE',folio 'FOLIO', " & mostarar_certNum & "Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'FECHA', Total as 'IMPORTE' " & estatus & mostarar_hms & _
                            ",pdf_id,comp_id,subido_por,c.corre_id, cont_nombrefecha as 'archivo'" & _
                            " from comprobante com" & _
                            " inner join contenido c_xml on c_xml.cont_id=com.xml_id" & _
                            " inner join Emisor e on e.emis_id=com.emis_id" & _
                            " inner join Receptor r on r.rece_id=com.rece_id" & _
                            " inner join correos c on c.corre_id = com.corre_id " & _
                            " where com.cuen_id =" & Session("cuen_id") & _
                            filtros & filtrosfechas & tipo

        Dim consulta_examinados_2 As String = "select  " & mostarar_nomFcat & "  e.rfc 'Proveedor RFC'   ,e.nombre 'Proveedor Nombre',folio 'Folio', " & mostarar_certNum & "Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'Fecha',Total as 'IMPORTE'  " & estatus & mostarar_hms & _
                            " ,pdf_id,comp_id,subido_por,'0' as 'corre_id', cont_nombrefecha as 'archivo' " & _
                            " from comprobante com" & _
                            " inner join contenido c_xml on c_xml.cont_id=com.xml_id" & _
                            " inner join Emisor e on e.emis_id=com.emis_id" & _
                            " inner join Receptor r on r.rece_id=com.rece_id" & _
                            " where com.cuen_id =" & Session("cuen_id") & _
                            filtros & filtrosfechas & tipo


        Dim consulta_sat_2 As String = "select  " & mostarar_nomFcat_sat & "  e.rfc 'Proveedor RFC'   ,e.nombre 'Proveedor Nombre',folio 'Folio', " & mostarar_certNum & "Convert(varchar(10),CONVERT(date,com.fecha,106),103) 'Fecha',Total as 'IMPORTE'  " & estatus & mostarar_hms & _
                            " ,pdf_id,comp_id,subido_por,'0' as 'corre_id' ,archivo" & _
                            " from comprobante com" & _
                            " inner join xml_sat c_xml on c_xml.xml_sat_id=com.xml_id " & _
                            " inner join Emisor e on e.emis_id=com.emis_id" & _
                            " inner join Receptor r on r.rece_id=com.rece_id" & _
                            " where com.cuen_id =" & Session("cuen_id")
        'filtros & filtrosfechas & tipo



        Dim consulta_2 As String

        If Radio_correo.Checked Then
            consulta_2 = consulta_correos_2 & " and subido_por='Correo' "
        ElseIf Radio_examninados.Checked Then
            consulta_2 = consulta_examinados_2 & " and subido_por='Examinado'"
        Else
            consulta_2 = consulta_correos_2 & "  and subido_por='Correo' union all " & consulta_examinados_2 & " and subido_por='Examinado' union all " & consulta_sat_2 & " and subido_por='sat'"
        End If






        ds2 = dbt.GetDataSet("select" & top & " a.* from (  " & consulta_2 & ") as a " & ordenar)
        Grid_facturas2.DataSource = ds2
        Grid_facturas2.DataBind()
        pintar()
        che_Stodos.Checked = False
        Label17.Text = ds2.Tables(0).Rows.Count.ToString


        If che_oc.Checked Then
            pintarPagados()
        End If

    End Sub

    Sub crearexcel()
        If mostrarExcel() = True Then
            Dim ds As DataSet
            Dim id_comp As String = Seleccionados()
            If id_comp = "null" Then
                mensajes.setMessage("Dede seleccionar almenos  una factura", 5000)
            Else
                If txt_ColumnasSel.Text = "" Then
                    ds = dbt.GetDataSet("  select comp_id from comprobante where comp_id in " & id_comp)
                Else
                    ds = dbt.GetDataSet("  select comp_id," & txt_ColumnasSel.Text & " from comprobante c inner join Emisor e on c.emis_id=e.emis_id  where comp_id in " & id_comp)
                End If
                makeExcelReport(ds, "Reporte", id_comp)
                System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "downloadFile('Reporte.xlsx');", True)
                txt_ColumnasSel.Text = ""
                seleccionarT(False)
                Chec_seleccionarColumnas.Checked = False
            End If
        Else
            mensajes.setMessage("Debe seleccionar minimo un campo de la factura", 5000)
        End If
    End Sub

    Sub mostarGrid(ByVal t1 As Boolean, ByVal t2 As Boolean, ByVal t3 As Boolean)
        Grid_conceptos.Visible = t1
        Grid_Reten.Visible = t2
        Grid_Tras.Visible = t3
    End Sub

    Sub ConsultarRetencion(ByVal id As String)
        Try
            Dim ds2 = dbt.GetDataSet("select [impuesto] as Impuesrto ,[importe] Importe  from comprobante c inner join impuesto i on i.impu_id = c.impu_id inner join Retencion r on r.impu_id=i.impu_id where comp_id= " & id)
            Grid_Reten.DataSource = ds2
            Grid_Reten.DataBind()
            If ds2.Tables(0).Rows.Count > 0 Then
                tituloCR.Text = "Retenciones"
                MPE_conceptos.Show()
            Else
                mensajes.setMessage("La factura no tiene retenciones", 5000)
            End If
        Catch ex As Exception
            mensajes.setError("Error al consultar retenciones ", 5000)
        End Try
    End Sub

    Sub ConsultarTraslado(ByVal id As String)
        Try
            Dim ds2 = dbt.GetDataSet("select t.impuesto as  Impuesto,tasa as Tasa,t.Importe as Importe   from comprobante c inner join Traslado t on t.impu_id = c.impu_id  where comp_id= " & id)
            Grid_Tras.DataSource = ds2
            Grid_Tras.DataBind()

            If ds2.Tables(0).Rows.Count > 0 Then
                tituloCR.Text = "Traslados"
                MPE_conceptos.Show()
            Else
                mensajes.setMessage("La factura no tiene traslados", 5000)
            End If
        Catch ex As Exception
            mensajes.setError("Error al consultar retenciones ", 5000)
        End Try
    End Sub
    Sub ConsultarConceptos(ByVal id As String)
        Try
            Dim ds2 = dbt.GetDataSet(" select [Cantidad],[unidad],[noIdentificacion],[descripcion],[valorUnitario],[importe] from concepto where  comp_id=  " & id)
            Grid_conceptos.DataSource = ds2
            tituloCR.Text = "Conceptos"
            Grid_conceptos.DataBind()
            MPE_conceptos.Show()
        Catch ex As Exception
            mensajes.setError("Erro al consultar conceptos ", 5000)
        End Try

    End Sub

    Sub mostrarPDF(ByVal id As String, ByVal comp_id As String, ByVal tipo As String, ByVal id_corre As String)
        Try
            Dim ds_pdf As DataSet
            ds_pdf = dbt.GetDataSet("select * from contenido where   cont_id=" & id)
            Dim archivo As String = ds_pdf.Tables(0).Rows(0).Item("cont_nombreFecha")
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "window.open('Archivos\\" & archivo & "','CONTRATO','status = 1,width=600, resizable = 1');", True)
        Catch ex As Exception
            mensajes.setMessage("La factura no tiene  PDF", 5000)
            Dim ds As DataSet
            ds = dbt.GetDataSet("select cont_nombre from contenido where corre_id =" & id_corre & "and (cont_extencion = 'PDF' or cont_extencion = 'pdf')")
            Grid_pdfEnCorreo.DataSource = ds
            Grid_pdfEnCorreo.DataBind()
            ds = dbt.GetDataSet("select cont_id,cont_nombre from contenido where corre_id =" & id_corre & "and (cont_extencion = 'PDF' or cont_extencion = 'pdf')")
            Grid_pdfEnCorreo2.DataSource = ds
            Grid_pdfEnCorreo2.DataBind()
            Session("comp_id") = comp_id
            Session("corre_id") = id_corre
            MPE_subirPDF.Show()
        End Try

    End Sub

    Function CerarNombre(ByVal id As String, ByVal subido_por As String)

        Dim texto As String = ""
        Dim nombre As String = txt_NombreArchivos.Text
        Dim dt As New DataTable
        dt.Columns.Add("field1")
        For i As Integer = 0 To txt_NombreArchivos.Text.Length - 1
            If nombre(i) <> "-" Then
                texto = texto & nombre(i)
            Else
                Dim row1 As DataRow = dt.NewRow
                row1.Item("field1") = texto
                dt.Rows.Add(row1)
                texto = ""
            End If
        Next
        Dim rowf As DataRow = dt.NewRow
        rowf.Item("field1") = texto
        dt.Rows.Add(rowf)
        texto = ""
        Dim fin As String = ""
        GridView1.DataSource = dt
        GridView1.DataBind()
        Dim ds As DataSet
        Dim val As String = "cont_nombre"

        If subido_por = "sat" Then
            ds = dbt.GetDataSet(" select c_xml.nombre as'cont_nombre' ,folio,CONVERT(VARCHAR(10), com.fecha, 110)  as 'com.fecha'  ,e.rfc as 'e.rfc',e.nombre as 'e.nombre' " & _
                            " from comprobante com inner join xml_sat c_xml on c_xml.xml_sat_id=com.xml_id " & _
                            " inner join Emisor e on e.emis_id=com.emis_id " & _
                            " inner join Receptor r on r.rece_id=com.rece_id  " & _
                            " where comp_id =" & id)



        Else
            ds = dbt.GetDataSet(" select cont_nombre,folio,CONVERT(VARCHAR(10), com.fecha, 110)  as 'com.fecha'  ,e.rfc as 'e.rfc',e.nombre as 'e.nombre'" & _
                            " from comprobante com inner join contenido c_xml on c_xml.cont_id=com.xml_id " & _
                            " inner join Emisor e on e.emis_id=com.emis_id " & _
                            " inner join Receptor r on r.rece_id=com.rece_id  " & _
                            " where comp_id =" & id)

        End If

        
        Dim texto2 As String
        For c1 As Integer = 0 To GridView1.Rows.Count - 1
            If fin = "" Then





                If GridView1.Rows(c1).Cells(0).Text = val Then
                    Dim ext As String = Extencion(ds.Tables(0).Rows(0).Item(GridView1.Rows(c1).Cells(0).Text), ".")
                    If ext = "xml" Then
                        texto2 = ds.Tables(0).Rows(0).Item(GridView1.Rows(c1).Cells(0).Text).Replace(".xml", "")
                    Else
                        texto2 = ds.Tables(0).Rows(0).Item(GridView1.Rows(c1).Cells(0).Text).Replace(".pdf", "")
                    End If
                Else
                    texto2 = ds.Tables(0).Rows(0).Item(GridView1.Rows(c1).Cells(0).Text)
                End If
                fin = fin & texto2
            Else
                If GridView1.Rows(c1).Cells(0).Text = val Then
                    texto2 = GridView1.Rows(c1).Cells(0).Text.Replace(".xml", "")
                    texto2 = GridView1.Rows(c1).Cells(0).Text.Replace(".pdf", "")
                Else
                    texto2 = ds.Tables(0).Rows(0).Item(GridView1.Rows(c1).Cells(0).Text)
                End If
                fin = fin & "-" & texto2
            End If
        Next
        Return fin
    End Function

    Function Extencion(Path As String, Caracter As String) As String
        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        Extencion = ret
    End Function

    Function mostrarExcel() As Boolean

        Dim val As Boolean = False
        If Chec_version.Checked = True Then
            val = True
        ElseIf Chec_serie.Checked = True Then
            val = True
        ElseIf Chec_folio.Checked = True Then
            val = True
        ElseIf Chec_fecha.Checked = True Then
            val = True
        ElseIf Check_sello.Checked = True Then
            val = True
        ElseIf Chec_formaDePago.Checked = True Then
            val = True
        ElseIf Chec_noCertificado.Checked = True Then
            val = True
        End If
        Return val
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



    Sub Seleccionados_oc_Guardar()
        Try



            Dim fecha As String = Session("oc_fecha_soli")

            Dim fecha2 As String = Session("fecha_pago")

            For i As Integer = 0 To Grid_facturas.Rows.Count - 1
                Dim ch As CheckBox = Grid_facturas.Rows(i).FindControl("Check_oc")
                If ch.Checked Then

                    dbt.ExecuteNonQuery("insert into ordenesdecompra values (getdate(),'" & fecha & "','" & fecha2 & "'," & Grid_facturas2.Rows(i).Cells(5).Text & ",'Activo','" & Session("oc_obser") & "','" & Session("oc_repre") & "','" & Session("depto") & "'," & Session("cuen_id") & ",NULL,NULL)")
                End If
            Next


            Session("oc_fecha_soli") = ""
            Session("oc_obser") = ""
            Session("oc_repre") = ""
            Session("depto") = "COMPRAS"
            Session("fecha_pago") = ""







        Catch ex As Exception

        End Try




    End Sub



    'Crear Excel
    Function makeExcelReport(dataSet As DataSet, filename As String, ByVal id_comp As String) As String
        Try
            Dim comp_id As String = ""
            Dim package = New OfficeOpenXml.ExcelPackage(New FileInfo(HttpContext.Current.Server.MapPath("Reporte.xlsx")))
            Dim sheet = package.Workbook.Worksheets(1)
            Dim col = 0, row = 0
            Dim dataColumns As DataColumnCollection = dataSet.Tables(0).Columns
            Dim conce_Impuesto_total As Double = 0
            Dim conce_Impuesto_total_ind As Double = 0
            Dim conce_valorU_total As Double = 0
            Dim conce_valorU_total_ind As Double = 0
            Dim conce_Importe_total_reten As Double = 0
            Dim conce_Importe_total_ind_reten As Double = 0
            Dim conce_Importe_total_tras As Double = 0
            Dim conce_Importe_total_ind_tras As Double = 0
            Dim ImpuestosLocales_total_ind As Double = 0
            Dim ImpuestosLocales_total As Double = 0
            Dim total_descuentos As Double = 0
            Dim totalFactura As Double = 0
            Dim inicio, fin As Integer
            row = 3
            col = 1

            sheet.Cells.Clear()

            For Each dRow As DataRow In dataSet.Tables(0).Rows
                row += 1
                Dim dCol As DataColumn
                For Each dCol In dataColumns

                    If dataColumns.Count <= 7 Then
                        fin = 7
                    Else
                        fin = dataColumns.Count
                    End If

                    If dCol.ColumnName <> "comp_id" Then
                        sheet.Cells(row, col).Value = dCol.ColumnName
                        'Color
                        sheet.Cells(row, col).Style.Fill.PatternType = ExcelFillStyle.Solid
                        sheet.Cells(row, col).Style.Fill.BackgroundColor.SetColor(Color.DeepSkyBlue)
                        ''Bordes
                        sheet.Cells(row, col).Style.Border.Left.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, col).Style.Border.Right.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, col).Style.Border.Top.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, col).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, col).Style.Font.Size = 13
                        sheet.Cells(row, col).Style.Font.Bold = True
                        sheet.Cells(row, 2, row, fin).Style.Border.Top.Style = ExcelBorderStyle.Medium
                        inicio = row
                    Else
                        comp_id = dCol.ColumnName
                    End If
                    col += 1
                Next
                row += 1
                col = 1
                Dim id As String = ""
                Dim contador As Integer = 0
                For Each dCell As Object In dRow.ItemArray
                    contador += 1
                    dCol = dataColumns(col - 1)
                    If (dCol.DataType = GetType(Double) Or dCol.DataType = GetType(Decimal) Or dCol.DataType = GetType(Single)) Then
                        sheet.Cells(row, col).Style.Numberformat.Format = "$#,##0.00"
                    ElseIf (dCol.DataType = GetType(Date)) Then
                        sheet.Cells(row, col).Style.Numberformat.Format = "dd/MM/yyyy"
                    End If
                    If contador = 1 Then
                        id = dCell
                    Else
                        sheet.Cells(row, col).Value = dCell
                    End If
                    col += 1
                Next
                '----------------------------Conceptos-----------------------------------
                If Chec_concepto.Checked Then

                    row += 1
                    sheet.Cells(row, 2).Value = "Conceptos"
                    sheet.Cells(row, 2, row, fin).Merge = True
                    sheet.Cells(row, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                    sheet.Cells(row, 2).Style.Font.Size = 12
                    sheet.Cells(row, 2, row, fin).Style.Border.Left.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 2, row, fin).Style.Border.Right.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 2, row, fin).Style.Border.Top.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 2, row, fin).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 2, row, fin).Style.Fill.PatternType = ExcelFillStyle.Solid
                    sheet.Cells(row, 2, row, fin).Style.Fill.BackgroundColor.SetColor(Color.DeepSkyBlue)
                    Dim ds As DataSet = dbt.GetDataSet("select * from concepto where comp_id= " & id)
                    row += 1
                    sheet.Cells(row, 2).Value = ("Cantidad")
                    sheet.Cells(row, 3).Value = ("Unidad")
                    sheet.Cells(row, 4).Value = ("N. Identificación")
                    sheet.Cells(row, 5).Value = ("Valor Unitario")
                    sheet.Cells(row, 6).Value = ("Descripción")
                    sheet.Cells(row, 7).Value = ("Importe")
                    For c As Integer = 2 To 7
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
                        sheet.Cells(row, 2).Value = ds.Tables(0).Rows(c).Item("Cantidad")
                        sheet.Cells(row, 3).Value = ds.Tables(0).Rows(c).Item("unidad")
                        sheet.Cells(row, 4).Value = ds.Tables(0).Rows(c).Item("noIdentificacion")
                        sheet.Cells(row, 5).Value = ds.Tables(0).Rows(c).Item("ValorUnitario")
                        sheet.Cells(row, 6).Value = ds.Tables(0).Rows(c).Item("descripcion")
                        sheet.Cells(row, 7).Value = ds.Tables(0).Rows(c).Item("importe")
                        sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                        conce_Impuesto_total_ind += ds.Tables(0).Rows(c).Item("importe")
                        conce_valorU_total_ind += ds.Tables(0).Rows(c).Item("ValorUnitario")
                        conce_valorU_total += ds.Tables(0).Rows(c).Item("ValorUnitario")
                        conce_Impuesto_total += ds.Tables(0).Rows(c).Item("importe")
                    Next
                    'Totales conceptos
                    If Chec_total_indivi.Checked Then
                        row += 1
                        sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                        sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                        sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 6).Value = "Total impuestos conceptos:"
                        sheet.Cells(row, 7).Value = conce_Impuesto_total_ind
                        sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                        totalFactura += conce_Impuesto_total_ind
                        conce_Impuesto_total_ind = 0
                        row += -1
                        row += 1
                        sheet.Cells(row, 4).Style.Fill.PatternType = ExcelFillStyle.Solid
                        sheet.Cells(row, 4).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                        sheet.Cells(row, 4).Style.Border.Left.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 4).Style.Border.Right.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 4).Style.Border.Top.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 4).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 4).Value = "Total Valor unitario:"
                        sheet.Cells(row, 5).Value = conce_valorU_total_ind
                        sheet.Cells(row, 5).Style.Numberformat.Format = "$#,##0.00"
                        conce_valorU_total_ind = 0
                        row += 1
                    End If

                End If
                '--------------------------- Fin Conceptos-----------------------------------
                'Traslados
                If Chec_Traslado.Checked Then
                    Dim ds As DataSet = dbt.GetDataSet("select t.impuesto as  Impuesto,tasa as Tasa,t.Importe as Importe   from comprobante c inner join Traslado t on t.impu_id = c.impu_id  where comp_id= " & id)
                    If ds.Tables(0).Rows.Count > 0 Then

                        row += 1
                        sheet.Cells(row, 2).Value = "Traslados"
                        sheet.Cells(row, 2, row, fin).Merge = True
                        sheet.Cells(row, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        sheet.Cells(row, 2).Style.Font.Size = 12
                        sheet.Cells(row, 2, row, fin).Style.Border.Left.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Border.Right.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Border.Top.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Fill.PatternType = ExcelFillStyle.Solid
                        sheet.Cells(row, 2, row, fin).Style.Fill.BackgroundColor.SetColor(Color.DeepSkyBlue)

                        row += 1
                        sheet.Cells(row, 2).Value = ("Impuesto")
                        sheet.Cells(row, 3).Value = ("Tasa")
                        sheet.Cells(row, 4).Value = ("Importe")

                        For c As Integer = 2 To 4
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
                            sheet.Cells(row, 2).Value = ds.Tables(0).Rows(c).Item("impuesto")
                            sheet.Cells(row, 3).Value = ds.Tables(0).Rows(c).Item("tasa")
                            sheet.Cells(row, 4).Value = ds.Tables(0).Rows(c).Item("importe")
                            sheet.Cells(row, 4).Style.Numberformat.Format = "$#,##0.00"
                            conce_Importe_total_ind_tras += ds.Tables(0).Rows(c).Item("importe")
                            conce_Importe_total_tras += ds.Tables(0).Rows(c).Item("importe")
                        Next

                        'Totales traslados
                        If Chec_total_indivi.Checked Then

                            row += 1
                            sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                            sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                            sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, 6).Value = "Total Importe:"
                            sheet.Cells(row, 7).Value = conce_Importe_total_ind_tras
                            sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                            conce_Importe_total_ind_tras = 0
                            row += 1

                        End If
                        row += 1
                    End If
                End If
                '----------------------------Retenciones-----------------------------------
                If Chec_Retencio.Checked Then

                    Dim ds As DataSet = dbt.GetDataSet("select [impuesto]  ,[importe] from comprobante c " & _
                                                       " inner join impuesto i on i.impu_id = c.impu_id " & _
                                                        " inner join Retencion r on r.impu_id=i.impu_id where comp_id= " & id)
                    If ds.Tables(0).Rows.Count > 0 Then
                        row += 1
                        sheet.Cells(row, 2).Value = "Retenciones"
                        sheet.Cells(row, 2, row, fin).Merge = True
                        sheet.Cells(row, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        sheet.Cells(row, 2).Style.Font.Size = 12
                        sheet.Cells(row, 2, row, fin).Style.Border.Left.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Border.Right.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Border.Top.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Fill.PatternType = ExcelFillStyle.Solid
                        sheet.Cells(row, 2, row, fin).Style.Fill.BackgroundColor.SetColor(Color.DeepSkyBlue)
                        row += 1
                        sheet.Cells(row, 2).Value = ("Impuesto")
                        sheet.Cells(row, 3).Value = ("Importe")

                        For c As Integer = 2 To 3
                            'Color
                            sheet.Cells(row, c).Style.Fill.PatternType = ExcelFillStyle.Solid
                            sheet.Cells(row, c).Style.Fill.BackgroundColor.SetColor(Color.DeepSkyBlue)
                            'B0rdes
                            sheet.Cells(row, c).Style.Border.Left.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, c).Style.Border.Right.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, c).Style.Border.Top.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, c).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                        Next
                        For c As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            row += 1
                            sheet.Cells(row, 2).Value = ds.Tables(0).Rows(c).Item("impuesto")
                            sheet.Cells(row, 3).Value = ds.Tables(0).Rows(c).Item("importe")
                            sheet.Cells(row, 3).Style.Numberformat.Format = "$#,##0.00"
                            conce_Importe_total_ind_reten += ds.Tables(0).Rows(c).Item("importe")
                            conce_Importe_total_reten += ds.Tables(0).Rows(c).Item("importe")

                        Next
                        row += 1
                        'Totales retenciones
                        If Chec_total_indivi.Checked Then
                            row += 1
                            sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                            sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                            sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                            sheet.Cells(row, 6).Value = "Total Importe:"
                            sheet.Cells(row, 7).Value = conce_Importe_total_ind_reten
                            sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                            totalFactura -= conce_Importe_total_ind_reten
                            conce_Importe_total_ind_reten = 0
                            row += 1
                        End If
                    End If
                End If
                '----------------------------------------------------------------------
                If Chec_total_indivi.Checked Then
                    Dim ds As DataSet = dbt.GetDataSet("select implocRetenido as 'Importe local', tasaderetencion as 'Tasa retención', importe  as 'Importe' from ImpuestosLocales where comp_id= " & id)
                    If ds.Tables(0).Rows.Count > 0 Then

                        row += 1
                        sheet.Cells(row, 2).Value = "Impuestos Locales"
                        sheet.Cells(row, 2, row, fin).Merge = True
                        sheet.Cells(row, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        sheet.Cells(row, 2).Style.Font.Size = 12
                        sheet.Cells(row, 2, row, fin).Style.Border.Left.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Border.Right.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Border.Top.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                        sheet.Cells(row, 2, row, fin).Style.Fill.PatternType = ExcelFillStyle.Solid
                        sheet.Cells(row, 2, row, fin).Style.Fill.BackgroundColor.SetColor(Color.DeepSkyBlue)
                        If ds.Tables(0).Rows.Count > 0 Then
                            row += 1
                            sheet.Cells(row, 2).Value = ("Importe local")
                            sheet.Cells(row, 3).Value = ("Tasa retención")
                            sheet.Cells(row, 4).Value = ("Importe")

                            For c As Integer = 2 To 4
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
                                sheet.Cells(row, 2).Value = ds.Tables(0).Rows(c).Item("Importe local")
                                sheet.Cells(row, 3).Value = ds.Tables(0).Rows(c).Item("Tasa retención")
                                sheet.Cells(row, 4).Value = ds.Tables(0).Rows(c).Item("importe")
                                sheet.Cells(row, 4).Style.Numberformat.Format = "$#,##0.00"
                                ImpuestosLocales_total_ind = ds.Tables(0).Rows(c).Item("importe")
                            Next

                            'Totale Impuestos Locales
                            If Chec_total_indivi.Checked Then
                                row += 1
                                sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                                sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                                sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                                sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                                sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                                sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                                sheet.Cells(row, 6).Value = "Total Importe:"
                                sheet.Cells(row, 7).Value = ImpuestosLocales_total_ind
                                sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                                totalFactura -= ImpuestosLocales_total_ind
                                ImpuestosLocales_total_ind = 0
                                row += 1
                            End If
                        End If
                    End If
                    '-----------------------------------------------------

                    row += 1
                    sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                    sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                    sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Value = "Subtotal: "
                    sheet.Cells(row, 7).Value = conce_Impuesto_total_ind
                    sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                    row += 1
                    sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                    sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                    sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Value = "Descuento: "
                    sheet.Cells(row, 7).Value = optenerDescuento(id)
                    total_descuentos += optenerDescuento(id)
                    sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                    row += 1
                    sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                    sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                    sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Value = "IVA:"
                    sheet.Cells(row, 7).Value = optenerIVA(id)
                    sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                    row += 1
                    sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                    sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                    sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Value = "TOTAL: "
                    sheet.Cells(row, 7).Value = totalFactura - optenerDescuento(id) + optenerIVA(id)
                    sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                    totalFactura = 0
                    row += 1
                End If

                If txt_ColumnasSel.Text <> "" Then
                    col = 1

                End If
                'Bordes por factura
                sheet.Cells(row - 1, 2, row - 1, fin).Style.Border.Bottom.Style = ExcelBorderStyle.Medium
                sheet.Cells(inicio, 2, row - 1, 2).Style.Border.Left.Style = ExcelBorderStyle.Medium
                sheet.Cells(inicio, fin, row - 1, fin).Style.Border.Right.Style = ExcelBorderStyle.Medium

            Next

            If Chec_total_total.Checked Then
                row += 2
                sheet.Cells(row, 2).Value = "Totales"
                sheet.Cells(row, 2, row, 7).Merge = True
                sheet.Cells(row, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                sheet.Cells(row, 2).Style.Font.Bold = True
                sheet.Cells(row, 2).Style.Font.Size = 15
                If Chec_concepto.Checked Then

                    row += 2
                    sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                    sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                    sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Value = "Total concepto impuestos:"
                    sheet.Cells(row, 7).Value = conce_Impuesto_total
                    sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                    row += -2
                    row += 2
                    sheet.Cells(row, 4).Style.Fill.PatternType = ExcelFillStyle.Solid
                    sheet.Cells(row, 4).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                    sheet.Cells(row, 4).Style.Border.Left.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 4).Style.Border.Right.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 4).Style.Border.Top.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 4).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 4).Value = "Total valor unitario:"
                    sheet.Cells(row, 5).Value = conce_valorU_total
                End If
                If Chec_Retencio.Checked Then
                    row += 2
                    sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                    sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                    sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Value = "Total retención Importe:"
                    sheet.Cells(row, 7).Value = conce_Importe_total_reten
                    sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                End If

                If Chec_Traslado.Checked Then
                    row += 2
                    sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                    sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                    sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                    sheet.Cells(row, 6).Value = "Total traslado Importe:"
                    sheet.Cells(row, 7).Value = conce_Importe_total_tras
                    sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
                End If

                row += 2
                sheet.Cells(row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                sheet.Cells(row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
                sheet.Cells(row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                sheet.Cells(row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                sheet.Cells(row, 6).Style.Border.Top.Style = ExcelBorderStyle.Thin
                sheet.Cells(row, 6).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                sheet.Cells(row, 6).Value = "Total Descuentos:"
                sheet.Cells(row, 7).Value = total_descuentos
                sheet.Cells(row, 7).Style.Numberformat.Format = "$#,##0.00"
            End If
            Try
                For i As Integer = 1 To 20
                    sheet.Column(i).AutoFit()
                Next
            Catch ex As Exception
            End Try
            package.SaveAs(New FileInfo(HttpContext.Current.Server.MapPath(filename & ".xlsx")))
            Return filename & ".xlsx"
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub Radio_Recibido_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_Recibido.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

    Protected Sub Radio_emitido_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_emitido.CheckedChanged
        ConsultarFacturas(Session("cuen_id"), False)
    End Sub

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

    Protected Sub Chec_Certificado_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Certificado.CheckedChanged
        If Chec_Certificado.Checked = True Then
            ColumnasExcel("certificado as Certificado")
        Else
            QuitarColumnasExcel("certificado as Certificado")
        End If
    End Sub

    Protected Sub Chec_Condicionespago_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Condicionespago.CheckedChanged
        If Chec_Condicionespago.Checked = True Then
            ColumnasExcel("condicionesDePago as 'Condiciones de pago'")
        Else
            QuitarColumnasExcel("condicionesDePago as 'Condiciones de pago'")
        End If
    End Sub

    Protected Sub Chec_Descuento_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Descuento.CheckedChanged
        If Chec_Descuento.Checked = True Then
            ColumnasExcel("Descuento as Descuento")
        Else
            QuitarColumnasExcel("Descuento as Descuento")
        End If
    End Sub

    Protected Sub Chec_Motivodescuento_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Motivodescuento.CheckedChanged
        If Chec_Motivodescuento.Checked = True Then
            ColumnasExcel("MotivoDescuento as 'Motivo de descuento'")
        Else
            QuitarColumnasExcel("MotivoDescuento as 'Motivo de descuento'")
        End If
    End Sub

    Protected Sub Chec_Tipocambio_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Tipocambio.CheckedChanged
        If Chec_Tipocambio.Checked = True Then
            ColumnasExcel("TipoCambio as 'Tipo de cambio'")
        Else
            QuitarColumnasExcel("TipoCambio as 'Tipo de cambio'")
        End If
    End Sub

    Protected Sub Chec_Moneda_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Moneda.CheckedChanged
        If Chec_Moneda.Checked = True Then
            ColumnasExcel("Moneda as 'Moneda'")
        Else
            QuitarColumnasExcel("Moneda as 'Moneda'")
        End If
    End Sub

    Protected Sub Chec_Tipocomprobante_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Tipocomprobante.CheckedChanged
        If Chec_Tipocomprobante.Checked = True Then
            ColumnasExcel("TipoDeComprobante as 'Tipo de comprobante'")
        Else
            QuitarColumnasExcel("TipoDeComprobante as 'Tipo de comprobante'")
        End If
    End Sub

    Protected Sub Chec_Metodopago_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Metodopago.CheckedChanged
        If Chec_Metodopago.Checked = True Then
            ColumnasExcel("MetodoDePago as 'Método de pago'")
        Else
            QuitarColumnasExcel("MetodoDePago as 'Método de pago'")
        End If
    End Sub

    Protected Sub Chec_Luegarexpedicion_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Lugarexpedicion.CheckedChanged
        If Chec_Lugarexpedicion.Checked = True Then
            ColumnasExcel("LugarExpedicion 'Lugar de expredición'")
        Else
            QuitarColumnasExcel("LugarExpedicion 'Lugar de expredición'")
        End If
    End Sub

    Protected Sub Chec_Nuctapago_CheckedChanged(sender As Object, e As EventArgs) Handles Chec_Nuctapago.CheckedChanged
        If Chec_Nuctapago.Checked = True Then
            ColumnasExcel("NumCtaPago as 'No CTA pago'")
        Else
            QuitarColumnasExcel("NumCtaPago as 'No CTA pago'")
        End If
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

    Protected Sub btn_gnerar_oc_Click(sender As Object, e As ImageClickEventArgs) Handles btn_gnerar_oc.Click

        oc_selecc.Text = Seleccionados_oc()


        If oc_selecc.Text = "null" Then
            mensajes.setMessage("Debe seleccionar como mínimo una factura.", 5000)
        Else
            MPE_OC.Show()
            'CalendarExtender2.SelectedDate = DateTime.Now()
        End If
    End Sub




    Protected Sub btn_acep_oc_Click(sender As Object, e As ImageClickEventArgs) Handles btn_acep_oc.Click

        If Session("oc_fecha_soli") <> "" Then
            dbt.ExecuteNonQuery("update comprobante  set estatus='Pagado' where comp_id in " & oc_selecc.Text)

            Seleccionados_oc_Guardar()
            ConsultarFacturas(Session("cuen_id"), False)
            pintarPagados()
            MPE_OC.Hide()
            mensajes.setMessage("Orden de compra guardada con éxito.", 5000)
            oc_selecc.Text = "null"
        Else
            mensajes.setError("El campo de fecha estimada de entrega no puede estar vacío.", 5000)
        End If

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

    Protected Sub che_oc_CheckedChanged(sender As Object, e As EventArgs) Handles che_oc.CheckedChanged
        If che_oc.Checked Then
            pintarPagados()
            Grid_facturas.Columns(6).Visible = True
            Grid_facturas.Columns(5).Visible = False
            btn_gnerar_oc.Visible = True
            btn_cancelarFactura.Visible = True
        Else
            Limpiar()
            Grid_facturas.Columns(6).Visible = False
            Grid_facturas.Columns(5).Visible = True
            btn_gnerar_oc.Visible = False
            btn_cancelarFactura.Visible = False
        End If
        che_Stodos.Checked = False

    End Sub

    Protected Sub btn_cancel_oc_Click(sender As Object, e As ImageClickEventArgs) Handles btn_cancel_oc.Click
        MPE_OC.Hide()
    End Sub

    Protected Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid_facturas.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _row As System.Data.DataRowView = e.Row.DataItem
            Dim num As Integer
            If Chec_cerNum.Checked Then
                num = 13
            Else
                num = 12
            End If
            Dim x As String = e.Row.Cells(num).Text
            e.Row.Cells(num).Text = Format(CDec(x), "$##,##0.00")
            e.Row.Cells(num).HorizontalAlign = HorizontalAlign.Right

        End If
    End Sub

    Private Sub btn_cancelarFactura_Click(sender As Object, e As ImageClickEventArgs) Handles btn_cancelarFactura.Click
        oc_selecc.Text = Seleccionados_oc()
        If oc_selecc.Text <> "null" Then
            MPE_cancelar_oc.Show()
        Else
            mensajes.setError("Debe seleccionar como mínimo una factura.", 5000)
        End If
    End Sub

    Protected Sub btn_oc_cancelar_Click(sender As Object, e As ImageClickEventArgs) Handles btn_oc_cancelar.Click

        dbt.ExecuteNonQuery("update comprobante  set estatus='Cancelado' where comp_id in " & oc_selecc.Text)

        ConsultarFacturas(Session("cuen_id"), False)
        pintarPagados()
        mensajes.setMessage("Factura cancelada con éxito.", 5000)
        MPE_cancelar_oc.Hide()
        oc_selecc.Text = "null"
    End Sub

    Protected Sub btn_oc_salir_Click(sender As Object, e As ImageClickEventArgs) Handles btn_oc_salir.Click
        MPE_cancelar_oc.Hide()
    End Sub

    Protected Sub btn_eliminarFacturas2_Click(sender As Object, e As ImageClickEventArgs)

    End Sub


    Sub Permisos_oc()

    End Sub

    Function TipoUsuario(ByVal id As String)
        Dim val As String


        Dim ds As DataSet = dbt.GetDataSet("select * from usuario where usua_id =" & id)

        val = ds.Tables(0).Rows(0).Item("Tipo")


        Return val
    End Function


    Protected Sub chec_proveedor_rfc_CheckedChanged(sender As Object, e As EventArgs) Handles chec_proveedor_rfc.CheckedChanged
        If chec_proveedor_rfc.Checked = True Then
            ColumnasExcel("e.rfc 'Proveedor RFC'")
        Else
            QuitarColumnasExcel("e.rfc 'Proveedor RFC'")
        End If
    End Sub

    Protected Sub chec_proveedor_nombre_CheckedChanged(sender As Object, e As EventArgs) Handles chec_proveedor_nombre.CheckedChanged
        If chec_proveedor_nombre.Checked = True Then
            ColumnasExcel("e.nombre 'Proveedor Nombre'")
        Else
            QuitarColumnasExcel("e.nombre 'Proveedor Nombre'")
        End If
    End Sub

    Protected Sub che_carpetas_CheckedChanged(sender As Object, e As EventArgs) Handles che_carpetas.CheckedChanged
        che_carpetas_clientes.Visible = True
    End Sub
End Class
