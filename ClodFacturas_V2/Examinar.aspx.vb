Imports OpenPop.Pop3
Imports OpenPop.Mime
Imports System.Linq
Imports System.IO
Imports System.Data.SqlClient
Imports System.Xml
Imports Ionic.Zip
Imports System.IO.Compression
Imports SharpCompress.Archive
Imports SharpCompress.Common
Imports System.Globalization



Public Class Examinar
    Inherits System.Web.UI.Page
    Dim dbt As New ToolsT.DbToolsT
    Dim mensajes As New messageTools(Me)
    ''Elementos 


    Dim uuid As String = ""
    Dim tipo_comprobante As String

    Dim Emisor, DomicilioFiscal, ExpedidoEn, RegimenFiscal, Receptor, Domicilio, Conceptos, InformacionAduanera, Retencion, Impuestos, Traslados As Boolean

    Dim comp_id As String = ""
    Dim version As String = ""
    Dim serie As String = ""
    Dim folio As String = ""
    Dim fecha As String = ""
    Dim sello As String = ""
    Dim formaDePago As String = ""
    Dim noCertificado As String = ""
    Dim certificado As String = ""
    Dim condicionesDePago As String = ""
    Dim Subtotal As String = ""
    Dim Descuento As String = ""
    Dim MotivoDescuento As String = ""
    Dim TipodeCambio As String = ""
    Dim Moneda As String = ""
    Dim Total As String = ""
    Dim TipoDeComprobante As String = ""
    Dim MetodoDePago As String = ""
    Dim LugarExpedicion As String = ""
    Dim NumCtaPago As String = ""
    Dim FolioFiscalOrig As String = ""
    Dim SerieFolioFiscalOrig As String = ""
    Dim FechaFolioFiscalOrig As String = ""
    Dim MontoFolioFiscalOrig As String = ""
    Dim emis_id As Integer = 0
    Dim rece_id As Integer = 0
    Dim impu_id As Integer = 0
    Dim comple_id As Integer = 0
    Dim adde_id As Integer = 0

    'Conceptos

    Dim conc_id As Integer = 0
    Dim Cantidad As String = ""
    Dim unidad As String = ""
    Dim noIdentificacion As String = ""
    Dim descripcion As String = ""
    Dim valorUnitario As String = ""
    Dim importe As String = ""
    Dim conceptocol As String = ""

    Dim conc_guardar As Boolean
    Dim InformacionAduanera_guardar As Boolean
    Dim Retencion_guardar As Boolean
    Dim conce_numero As Integer = 0




    'Emisor

    Dim rfc As String = ""
    Dim nombre As String = ""
    Dim domifical_id As String = ""
    Dim expe_id As String = ""
    Dim regi_id As String = ""


    Dim bandera_emisor As Boolean = True

    '' ExpedidoEn 

    Dim ExpedidoEn_Calle As String = ""
    Dim ExpedidoEn_noExterior As String = ""
    Dim ExpedidoEn_noInterior As String = ""
    Dim ExpedidoEn_colonia As String = ""
    Dim ExpedidoEn_localidad As String = ""
    Dim ExpedidoEn_referencia As String = ""
    Dim ExpedidoEn_municipio As String = ""
    Dim ExpedidoEn_estado As String = ""
    Dim ExpedidoEn_pais As String = ""
    Dim ExpedidoEn_codigoPostal As String = ""

    Dim bandera_ExpedidoEn As Boolean = True
    Dim bandera_DomicilioFiscal As Boolean = True

    'DomicilioFiscal

    Dim DomicilioFiscal_Calle As String = ""
    Dim DomicilioFiscal_noExterior As String = ""
    Dim DomicilioFiscal_noInterior As String = ""
    Dim DomicilioFiscal_Colonia As String = ""
    Dim DomicilioFiscal_Localidad As String = ""
    Dim DomicilioFiscal_referencia As String = ""
    Dim DomicilioFiscal_municipio As String = ""
    Dim DomicilioFiscal_estado As String = ""
    Dim DomicilioFiscal_pais As String = ""
    Dim DomicilioFiscal_codigoPostal As String = ""


    'RegimenFiscal

    Dim Regimen As String = ""



    ''Receptor
    Dim contadorrfc As Integer = 0
    Dim contadornombre As Integer = 0

    Dim receptor_rfc As String = ""
    Dim receptor_nombre As String = ""

    Dim domi_id As Integer = 0

    Dim calle As String

    Dim noExterior As String = ""
    Dim noInterior As String = ""
    Dim colonia As String = ""
    Dim localidad As String = ""
    Dim referencia As String = ""
    Dim municipio As String = ""
    Dim estado As String = ""
    Dim pais As String = ""
    Dim codigoPostal As String = ""

    Dim bandera_fechaComprobante = True



    Dim contadorCalle As Integer = 0
    Dim contadornoExterior As Integer = 0
    Dim contadornoInterior As Integer = 0
    Dim contadorcolonia As Integer = 0
    Dim contadorlocalidad As Integer = 0

    'impuesto 

    Dim totalImpuestosRetenidos As String = ""
    Dim totalImpuestosTrasladados As String = ""
    Dim reten_id As Integer = 0
    Dim trasla_id As Integer = 0

    'Traslado 
    Dim Traslado_impuesto As String = ""
    Dim Traslado_tasa As String = ""
    Dim Traslado_importe As String = ""


    'Retencion 
    Dim Retencion_impuesto As String = ""
    Dim Retencion_importe As String = ""

    'InformacionAduanera 
    Dim InformacionAduanera_numero As String = ""
    Dim InformacionAduanera_fecha As String = ""
    Dim InformacionAduanera_aduana As String = ""

    Dim impuesto As String = ""



    'ImpuestosLocales
    Dim ImpLocales_ImpLocRetenido As String
    Dim ImpLocales_TasadeRetencion As String
    Dim ImpLocales_Importe As String


    Dim valida_rfc1 As Boolean = False
    Dim valida_rfc2 As Boolean = False

    Dim ImpuestosLocales As Boolean = False


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MySecurity.checkValidSession(Me) = False Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "Cargar()", True)
        End If
        If IsPostBack Then

        End If
    End Sub


    Private Sub UploadButton_Click1(sender As Object, e As EventArgs) Handles UploadButton.Click
        Try
            Dim fileOK As Boolean = False
            If FileUpload1.HasFile Then
                Dim fileExtension As String
                fileExtension = System.IO.Path. _
                    GetExtension(FileUpload1.FileName).ToLower()
                Dim allowedExtensions As String() = _
                    {".xml"}
                For i As Integer = 0 To allowedExtensions.Length - 1
                    If fileExtension = allowedExtensions(i) Then
                        fileOK = True
                    End If
                Next

                If fileOK Then

                    Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() & "\Archivos\"
                    Dim fileName As String = FileUpload1.FileName

                    fileName = CambiarCaracteres(DateTime.Now.ToString("yyyyMMdd") & "" & fileName)

                    path += fileName
                    FileUpload1.SaveAs(path)




                    path = System.AppDomain.CurrentDomain.BaseDirectory() & "\Archivos\" & fileName

                    dbt.ExecuteNonQuery("insert into contenido values ('xml','" & FileUpload1.FileName & "','" & fileName & "','" & path & "','0')")

                    leerxml(path)




                    'UploadStatusLabel.Text = "Tu factura fue guardada"
                    mensajes.setMessage("xml guardado con exito.", 5000)
                    panel_pdf.Visible = True

                Else
                    'UploadStatusLabel.Text = "debe seleccionar un archivo xml"
                    mensajes.setMessage("debe seleccionar un archivo xml.", 5000)
                End If

            Else
                'UploadStatusLabel.Text = "debe seleccionar un archivo xml"
                mensajes.setMessage("debe seleccionar un archivo xml.", 5000)

            End If

        Catch ex As Exception
            mensajes.setError("Error al subir xml", 5000)
        End Try
    End Sub

    Sub cerarArchivo(ByVal Path As String, ByVal body As Object)

        If File.Exists(Path) Then
            'File.Delete(path)
        Else
            'Crear archivo
            Dim fs As FileStream = File.Create(Path, FileMode.Create)

            Dim BinaryStream = New BinaryWriter(fs)
            BinaryStream.Write(body)
            BinaryStream.Close()
        End If
    End Sub


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


    Function Extraer(Path As String, Caracter As String) As String
        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        Extraer = ret
    End Function



    Private Sub UploadButtonpdf_Click1(sender As Object, e As EventArgs) Handles UploadButtonpdf.Click
        Try
            Dim fileOK As Boolean = False
            If FileUpload2.HasFile Then
                Dim fileExtension As String
                fileExtension = System.IO.Path. _
                    GetExtension(FileUpload2.FileName).ToLower()
                Dim allowedExtensions As String() = _
                    {".pdf"}
                For i As Integer = 0 To allowedExtensions.Length - 1
                    If fileExtension = allowedExtensions(i) Then
                        fileOK = True
                    End If
                Next

                If fileOK Then

                    Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() & "\Archivos\"
                    Dim fileName As String = FileUpload2.FileName
                    path += fileName
                    FileUpload2.SaveAs(path)

                    fileName = CambiarCaracteres(DateTime.Now.ToString("yyyyMMdd") & "" & fileName)


                    dbt.ExecuteNonQuery("insert into contenido values ('pdf','" & FileUpload2.FileName & "','" & fileName & "','" & path & "','0')")

                    Dim cont_id As String = ultimoID("contenido", "cont_id")





                    dbt.ExecuteNonQuery("update comprobante set pdf_id=" & cont_id & " where comp_id= " & ultimoID("comprobante", "comp_id"))



                Else
                    'UploadStatusLabel.Text = "debe seleccionar un archivo xml"
                    mensajes.setMessage("debe seleccionar un archivo pdf.", 5000)
                End If

            Else
                'UploadStatusLabel.Text = "debe seleccionar un archivo xml"
                mensajes.setMessage("debe seleccionar un archivo pdf.", 5000)

            End If

        Catch ex As Exception
            mensajes.setError("Error al subir pdf", 5000)
        End Try
    End Sub





    Sub leerxml(ByVal Archivo As String)
        Dim contador As Integer = 1
        Dim contador2 As Integer = 1

        Dim xml As String = ""
        'Dim reader As XmlTextReader = New XmlTextReader("C:\Users\oskar\Documents\Visual Studio 2012\Projects\Facturas\Facturas\Archivos\Factura_RCO0708136F7_2117057B_2014-09-22.xml")
        Dim reader As XmlTextReader = New XmlTextReader(Archivo)


        Do While (reader.Read())
            Select Case reader.NodeType
                Case XmlNodeType.Element 'Mostrar comienzo del elemento.
                    xml = xml & ("<" + reader.Name)

                    If reader.Name = "cfdi:Emisor" Then
                        Emisor = True

                    ElseIf reader.Name = "cfdi:DomicilioFiscal" Then
                        DomicilioFiscal = True

                    ElseIf reader.Name = "cfdi:ExpedidoEn" Then
                        ExpedidoEn = True
                    ElseIf reader.Name = "cfdi:RegimenFiscal" Then
                        RegimenFiscal = True

                    ElseIf reader.Name = "cfdi:Receptor" Then
                        Receptor = True

                    ElseIf reader.Name = "cfdi:Domicilio" Then
                        Domicilio = True
                    ElseIf reader.Name = "cfdi:Conceptos" Then

                    ElseIf reader.Name = "cfdi:InformacionAduanera" Then
                        InformacionAduanera = True

                    ElseIf reader.Name = "cfdi:Retencion" Then
                        Retencion = True
                    ElseIf reader.Name = "cfdi:Traslado" Then
                        Traslados = True
                    End If



                    If reader.HasAttributes Then 'If attributes exist


                        xml = xml & ">"

                        While reader.MoveToNextAttribute()
                            'Mostrar valor y nombre del atributo
                            Console.Write(" {0}='{1}'", reader.Name, reader.Value)
                            Const quote As String = """"
                            xml = xml & reader.Name & " = " & quote & reader.Value & quote




                            If reader.Name = "cantidad" Then

                                conc_guardar = True
                            ElseIf reader.Name = "numero" Then
                                InformacionAduanera_guardar = True
                            ElseIf reader.Name = "impuesto" Then
                                Retencion_guardar = True
                            End If






                            Guardarxml(reader.Name, reader.Value)



                        End While



                    End If




                    xml = xml & (">")
                Case XmlNodeType.Text 'Mostrar el texto de cada elemento.
                    xml = xml & (reader.Value)
                Case XmlNodeType.EndElement 'Mostrar final del elemento.
                    xml = xml & ("</" + reader.Name)
                    xml = xml & (">")
            End Select
        Loop

        Try


            If Emisor = True Then
                dbt.ExecuteNonQuery("insert into emisor values ('" & rfc & "', '" & nombre & "','" & domifical_id & "','" & expe_id & "','" & regi_id & "')")
                Emisor = False
            End If

            If DomicilioFiscal = True Then
                dbt.ExecuteNonQuery("insert into DomicilioFiscal values ('" & DomicilioFiscal_Calle & "','" & DomicilioFiscal_noExterior & "','" & DomicilioFiscal_noInterior & "','" & DomicilioFiscal_Colonia & "','" & DomicilioFiscal_Localidad & "','" & DomicilioFiscal_referencia & "','" & DomicilioFiscal_municipio & "','" & DomicilioFiscal_estado & "','" & DomicilioFiscal_pais & "','" & DomicilioFiscal_codigoPostal & "')")
                DomicilioFiscal = False
                limpiarDomicilioFiscal()
            End If
            If ExpedidoEn = True Then
                dbt.ExecuteNonQuery("insert into ExpedidoEn values ('" & ExpedidoEn_Calle & "','" & ExpedidoEn_noExterior & "','" & ExpedidoEn_noInterior & "','" & ExpedidoEn_colonia & "','" & ExpedidoEn_localidad & "','" & ExpedidoEn_referencia & "','" & ExpedidoEn_municipio & "','" & ExpedidoEn_estado & "','" & ExpedidoEn_pais & "','" & ExpedidoEn_codigoPostal & "')")
                ExpedidoEn = False
                limpiarExpedidoEn()
            End If


            If RegimenFiscal = True Then
                dbt.ExecuteNonQuery("insert into RegimenFiscal values ('" & Regimen & "')")
                RegimenFiscal = False
            End If



            If Emisor = True Then
                dbt.ExecuteNonQuery("insert into emisor values ('" & rfc & "', '" & nombre & "','" & domifical_id & "','" & expe_id & "','" & regi_id & "')")
                Emisor = False
                limpiarEmisor()
            End If

            If DomicilioFiscal = True Then
                dbt.ExecuteNonQuery("insert into DomicilioFiscal values ('" & DomicilioFiscal_Calle & "','" & DomicilioFiscal_noExterior & "','" & DomicilioFiscal_noInterior & "','" & DomicilioFiscal_Colonia & "','" & DomicilioFiscal_Localidad & "','" & DomicilioFiscal_referencia & "','" & DomicilioFiscal_municipio & "','" & DomicilioFiscal_estado & "','" & DomicilioFiscal_pais & "','" & DomicilioFiscal_codigoPostal & "')")
                DomicilioFiscal = False
                limpiarDomicilioFiscal()
            End If
            If ExpedidoEn = True Then
                dbt.ExecuteNonQuery("insert into ExpedidoEn values ('" & ExpedidoEn_Calle & "','" & ExpedidoEn_noExterior & "','" & ExpedidoEn_noInterior & "','" & ExpedidoEn_colonia & "','" & ExpedidoEn_localidad & "','" & ExpedidoEn_referencia & "','" & ExpedidoEn_municipio & "','" & ExpedidoEn_estado & "','" & ExpedidoEn_pais & "','" & ExpedidoEn_codigoPostal & "')")
                ExpedidoEn = False
                limpiarExpedidoEn()
            End If


            If RegimenFiscal = True Then
                dbt.ExecuteNonQuery("insert into RegimenFiscal values ('" & Regimen & "')")
                RegimenFiscal = False
                limpiarRegimen()
            End If


            If Receptor = True Then

                If Domicilio = True Then
                    dbt.ExecuteNonQuery("insert into domicilio values ('" & calle & "','" & noExterior & "','" & noInterior & "','" & colonia & "','" & localidad & "','" & referencia & "','" & municipio & "','" & estado & "','" & pais & "','" & codigoPostal & "')")

                    Dim ds_domicilo As DataSet

                    ds_domicilo = dbt.GetDataSet("select top 1 * from domicilio order by domi_id desc")
                    domi_id = ds_domicilo.Tables(0).Rows(0).Item("domi_id")

                    dbt.ExecuteNonQuery("insert into Receptor values ('" & receptor_rfc & "','" & receptor_nombre & "','" & domi_id & "')")
                    Domicilio = False
                    Receptor = False


                    limpiarReceptor()
                End If
            End If


            emis_id = ultimoID("emisor", "emis_id")
            rece_id = ultimoID("receptor", "rece_id")
            impu_id = ultimoID("impuesto", "impu_id")

            domifical_id = ultimoID("DomicilioFiscal", "domifical_id")
            reten_id = ultimoID("Retencion", "reten_id")
            trasla_id = ultimoID("Traslado", "trasla_id")

            comple_id = 1
            adde_id = 1



            If validarUUid(uuid) = True Then


                If valida_rfc1 = True Or valida_rfc2 = True Then

                    Dim datee As Date = fecha
                    fecha = Format(datee, "dd-MM-yy H:mm:ss")

                    dbt.ExecuteNonQuery("insert into comprobante values('" & version & "','" & serie & "','" & folio & "','" & fecha & "','" & sello & "','" & formaDePago & "','" & noCertificado & "','" & certificado & _
                                                               "','" & condicionesDePago & "','" & Subtotal & "','" & Descuento & "','" & MotivoDescuento & "','" & TipodeCambio & "','" & Moneda & "','" & Total & "','" & TipoDeComprobante & _
                                                               "','" & MetodoDePago & "','" & LugarExpedicion & "','" & NumCtaPago & "','" & FolioFiscalOrig & "','" & SerieFolioFiscalOrig & _
                                                               "','" & FechaFolioFiscalOrig & "','" & MontoFolioFiscalOrig & "','" & emis_id & "','" & rece_id & "','" & impu_id & "','" & comple_id & "','" & adde_id & "'," & Session("cuen_id") & ",'Examinado'," & ultimoID("contenido", "cont_id") & ",0,'0','" & uuid & "','" & tipo_comprobante & "','Sin pagar')")


                    valida_rfc1 = False
                    valida_rfc2 = False
                End If



            End If




            conce_numero = 0
            bandera_emisor = True
            bandera_ExpedidoEn = True
            bandera_DomicilioFiscal = True
            bandera_fechaComprobante = True
            contadorrfc = 0
            contadornombre = 0
            contadorCalle = 0
            contadornoExterior = 0
            contadornoInterior = 0
            contadorcolonia = 0
            contadorlocalidad = 0
            Emisor = False
            DomicilioFiscal = False
            ExpedidoEn = False
            RegimenFiscal = False
            Receptor = False
            Domicilio = False
            Conceptos = False
            InformacionAduanera = False
            Retencion = False
            Retencion_guardar = False
        Catch ex As Exception

        End Try

    End Sub




    Function validarUUid(ByVal uuid As String) As Boolean

        Dim val As Boolean
        Try



            Dim ds As DataSet = dbt.GetDataSet("select * from comprobante where uuid='" & uuid & "' ")


            If ds.Tables(0).Rows.Count = 1 Then
                val = False
            Else
                val = True
            End If

        Catch ex As Exception
            val = True
        End Try

        Return val


    End Function


    Sub Guardarxml(ByVal Name As String, ByVal Value As String) ' vercion 3.2
        If Name = "UUID" Then
            uuid = Value
        End If
        If Name = "version" Then
            version = Value
        End If
        If Name = "version" Then 'si
            version = Value
        ElseIf Name = "serie" Then 'si ---no viene en la lista de orden
            serie = Value
        ElseIf Name = "folio" Then 'si  --no vbiene en la lista de orden
            folio = Value
        ElseIf Name = "fecha" Then 'si
            If bandera_fechaComprobante = True Then
                fecha = Value
                bandera_fechaComprobante = False
            End If
            If InformacionAduanera = True Then
                InformacionAduanera_fecha = Value
            End If
        ElseIf Name = "sello" Then 'si  --no viene en la lista de orden 
            sello = Value
        ElseIf Name = "tipoDeComprobante" Then 'falta en tabla
            TipoDeComprobante = Value
        ElseIf Name = "formaDePago" Then 'si
            formaDePago = Value
        ElseIf Name = "noCertificado" Then 'si --no viene en la lista de orden 
            noCertificado = Value
        ElseIf Name = "certificado" Then 'si --no viene en la lista de orden 
            certificado = Value
        ElseIf Name = "condicionesDePago" Then 'si 
            condicionesDePago = Value
        ElseIf Name = "subTotal" Then 'si
            Subtotal = Value
        ElseIf Name = "descuento" Then 'si
            Descuento = Value
        ElseIf Name = "MotivoDescuento" Then 'si -- no esta en lista de orden 
            MotivoDescuento = Value
        ElseIf Name = "TipoCambio" Then 'si
            TipodeCambio = Value
        ElseIf Name = "Moneda" Then 'si
            Moneda = Value
        ElseIf Name = "total" Then 'si
            Total = Value
        ElseIf Name = "TipoDeComprobante" Then 'si -no esta en lista de orden
            TipoDeComprobante = Value
        ElseIf Name = "metodoDePago" Then 'si
            MetodoDePago = Value
        ElseIf Name = "LugarExpedicion" Then 'si
            LugarExpedicion = Value
        ElseIf Name = "NumCtaPago" Then 'si
            NumCtaPago = Value
        ElseIf Name = "FolioFiscalOrig" Then 'si
            FolioFiscalOrig = Value
        ElseIf Name = "SerieFolioFiscalOrig" Then
            SerieFolioFiscalOrig = Value
        ElseIf Name = "FechaFolioFiscalOrig" Then
            FechaFolioFiscalOrig = Value
        ElseIf Name = "MontoFolioFiscalOrig" Then
            MontoFolioFiscalOrig = Value
            ' Información del nodo Emisor
        ElseIf Name = "rfc" Then
            If Emisor = True And Receptor = False Then
                rfc = Value


                Dim rfccc As String = Session("rfc")

                If Value = rfccc Then
                    valida_rfc2 = True
                    tipo_comprobante = "Egreso"
                Else

                    valida_rfc2 = False

                End If




            End If
            If Receptor = True Then
                receptor_rfc = Value

                Dim rfccc As String = Session("rfc")



                If Value = rfccc Then
                    valida_rfc1 = True
                    tipo_comprobante = "Ingreso"
                Else
                    valida_rfc1 = False
                End If

            End If
        ElseIf Name = "nombre" Then
            If Emisor = True And Receptor = False Then
                nombre = Value
            End If
            If Receptor = True Then
                receptor_nombre = Value
            End If






            ' Información del nodo DomicilioFiscal y Información del nodo ExpedidoEn
        ElseIf Name = "calle" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_Calle = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_Calle = Value
            End If
            If Domicilio = True Then
                calle = Value
            End If
        ElseIf Name = "noExterior" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_noExterior = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_noExterior = Value
            End If
            If Domicilio = True Then
                noExterior = Value
            End If
        ElseIf Name = "noInterior" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_noInterior = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_noInterior = Value
            End If
            If Domicilio = True Then
                noInterior = Value
            End If
        ElseIf Name = "colonia" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_Colonia = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_colonia = Value
            End If
            If Domicilio = True Then
                colonia = Value
            End If
        ElseIf Name = "localidad" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_Localidad = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_localidad = Value
            End If
            If Domicilio = True Then
                localidad = Value
            End If
        ElseIf Name = "referencia" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_referencia = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_referencia = Value
            End If
            If Domicilio = True Then
                referencia = Value
            End If
        ElseIf Name = "municipio" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_municipio = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_municipio = Value
            End If
            If Domicilio = True Then
                municipio = Value
            End If
        ElseIf Name = "estado" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_estado = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_estado = Value
            End If
            If Domicilio = True Then
                estado = Value
            End If
        ElseIf Name = "pais" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_pais = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_pais = Value
            End If
            If Domicilio = True Then
                pais = Value
            End If
        ElseIf Name = "codigoPostal" Then
            If DomicilioFiscal = True And ExpedidoEn = False And Domicilio = False Then
                DomicilioFiscal_codigoPostal = Value
            End If
            If ExpedidoEn = True And Domicilio = False Then
                ExpedidoEn_codigoPostal = Value
            End If
            If Domicilio = True Then
                codigoPostal = Value
            End If
            'Información del nodo RegimenFiscal
        ElseIf Name = "Regimen" Then
            Regimen = Value
            ' Información de cada nodo Concepto
        ElseIf Name = "tasa" Then
            Traslado_tasa = Value
        ElseIf Name = "cantidad" Then
            Cantidad = Value
        ElseIf Name = "unidad" Then
            unidad = Value
        ElseIf Name = "noIdentificacion" Then
            noIdentificacion = Value
        ElseIf Name = "descripcion" Then
            descripcion = Value
        ElseIf Name = "valorUnitario" Then
            valorUnitario = Value
        ElseIf Name = "Importe" Then
            If ImpuestosLocales = True Then
                ImpLocales_Importe = Value
            End If
        ElseIf Name = "importe" Then
            importe = Value
            If Traslados = True Then
                Traslado_importe = Value
            End If
            If conc_guardar = True Then
                dbt.ExecuteNonQuery("insert into concepto values ('" & Cantidad & "','" & unidad & "','" & noIdentificacion & "','" & descripcion & "','" & valorUnitario & "','" & importe & "','" & ultimoID("comprobante", "comp_id") + 1 & "')")
                conc_guardar = False
            End If
            If Retencion_guardar = True And Retencion = True And Traslados = False Then
                impu_id = ultimoID("impuesto", "impu_id")
                dbt.ExecuteNonQuery("insert into Retencion values ('" & impuesto & "','" & importe & "','" & impu_id & "'  ) ")
                Retencion_guardar = False
                Retencion = False
                impuesto = ""
                importe = ""
            End If
            'InformacionAduanera
        ElseIf Name = "numero" Then
            If InformacionAduanera = True Then
                InformacionAduanera_numero = Value
            End If
        ElseIf Name = "aduana" Then
            InformacionAduanera_aduana = Value
            Dim ds_conc As DataSet
            ds_conc = dbt.GetDataSet("select top 1 * from concepto order by conc_id desc")
            conc_id = ds_conc.Tables(0).Rows(0).Item("conc_id")
            dbt.ExecuteNonQuery("insert into InformacionAduanera  values ('" & InformacionAduanera_numero & "','" & InformacionAduanera_fecha & "','" & InformacionAduanera_aduana & "','" & conc_id & "')")
            'Información del nodo CuentaPredial
        ElseIf Name = "numero" Then
            ' Información de cada nodo Retencion
            ' nota: esta secuencia a, b, deberá ser repetida por cada nodo Retención relacionado, el total de impuestos retenidos no se repite.
        ElseIf Name = "impuesto" Then
            If Retencion = True Then
                impuesto = Value
            End If
            If Traslados = True Then
                Traslado_impuesto = Value
            End If
        ElseIf Name = "totalImpuestosRetenidos" Then
            totalImpuestosRetenidos = Value
        ElseIf Name = "totalImpuestosTrasladados" Then
            totalImpuestosTrasladados = Value
            dbt.ExecuteNonQuery("insert into impuesto values ('" & totalImpuestosRetenidos & "','" & totalImpuestosTrasladados & "')")
            totalImpuestosRetenidos = ""
            totalImpuestosTrasladados = ""
        ElseIf Name = "ImpLocRetenido" Then
            ImpLocales_ImpLocRetenido = Value
        ElseIf Name = "TasadeRetencion" Then
            ImpLocales_TasadeRetencion = Value
        End If
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



    Sub limpiarExpedidoEn()
        ExpedidoEn_Calle = ""
        ExpedidoEn_noExterior = ""
        ExpedidoEn_noInterior = ""
        ExpedidoEn_colonia = ""
        ExpedidoEn_localidad = ""
        ExpedidoEn_referencia = ""
        ExpedidoEn_municipio = ""
        ExpedidoEn_estado = ""
        ExpedidoEn_pais = ""
        ExpedidoEn_codigoPostal = ""

    End Sub


    Sub limpiarDomicilioFiscal()
        DomicilioFiscal_Calle = ""
        DomicilioFiscal_noExterior = ""
        DomicilioFiscal_noInterior = ""
        DomicilioFiscal_Colonia = ""
        DomicilioFiscal_Localidad = ""
        DomicilioFiscal_referencia = ""
        DomicilioFiscal_municipio = ""
        DomicilioFiscal_estado = ""
        DomicilioFiscal_pais = ""
        DomicilioFiscal_codigoPostal = ""
    End Sub


    Sub limpiarEmisor()
        rfc = ""
        nombre = ""
        domifical_id = 1
        expe_id = 1
        regi_id = 1
    End Sub

    Sub limpiarRegimen()
        Regimen = ""
    End Sub


    Sub limpiarReceptor()
        calle = ""
        noExterior = ""
        noInterior = ""
        colonia = ""
        localidad = ""
        referencia = ""
        municipio = ""
        estado = ""
        pais = ""
        codigoPostal = ""


        receptor_rfc = ""
        receptor_nombre = ""
        domi_id = 1
    End Sub




    Function obtenerConce_id() As Integer

        Dim ds As DataSet = dbt.GetDataSet("select top 1 * from comprobante order by comp_id desc")

        Try
            comp_id = ds.Tables(0).Rows(0).Item("comp_id") + 1
        Catch ex As Exception
            comp_id = 1
        End Try
        Return True
    End Function




End Class