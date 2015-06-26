Imports System.IO
Imports System.Net.Mail
Imports OpenPop.Pop3
Imports OpenPop.Mime
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Xml
Imports Ionic.Zip
Imports System.IO.Compression
Imports SharpCompress.Archive
Imports SharpCompress.Common
Imports System.Globalization
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports org.pdfbox.util
Imports org.pdfbox.pdmodel
Imports System.TypeInitializationException



Public Class Index
    Inherits System.Web.UI.Page
    Dim dbt As New ToolsT.DbToolsT
    Dim mensajes As New messageTools(Me)

    Dim consultaFinal As String
    Dim errorTotal As Boolean = True

    Dim consulta As String
    Dim comp_id As Integer
    Dim conc_id As Integer = 0
    Dim guardar_correo, valida_rfc1, valida_rfc2 As Boolean
    Dim validaUuid, Emisor, DomicilioFiscal, ExpedidoEn, RegimenFiscal, Receptor, Domicilio, Conceptos, InformacionAduanera, Retencion, Impuestos, Traslados, ImpuestosLocales As Boolean
    Dim correos_tipo, uuid, version, serie, folio, fecha, sello, formaDePago, noCertificado As String
    Dim certificado, condicionesDePago, Subtotal, Descuento, MotivoDescuento, TipodeCambio, Moneda As String
    Dim Total, TipoDeComprobante, MetodoDePago, LugarExpedicion, NumCtaPago, FolioFiscalOrig, SerieFolioFiscalOrig As String
    Dim FechaFolioFiscalOrig, MontoFolioFiscalOrig, emis_id, rece_id, impu_id, comple_id, adde_id, tipo_comprobante As String
    Dim numfacturas_Repetidas As Integer = 0
    Dim num_facturas_noperteneceRFC As Integer = 0
    'Conceptos
    Dim Cantidad As String, unidad, noIdentificacion, descripcion, valorUnitario, importe, conceptocol As String
    Dim conc_guardar, InformacionAduanera_guardar, Retencion_guardar, traslado_guardar As Boolean
    Dim conce_numero As Integer = 0
    'Emisor
    Dim rfc, nombre, domifical_id, expe_id, regi_id As String
    Dim bandera_emisor As Boolean = True
    'ExpedidoEn 
    Dim ExpedidoEn_Calle, ExpedidoEn_noExterior, ExpedidoEn_noInterior, ExpedidoEn_colonia, ExpedidoEn_localidad, ExpedidoEn_referencia, ExpedidoEn_municipio, ExpedidoEn_estado, ExpedidoEn_pais, ExpedidoEn_codigoPostal As String
    Dim bandera_ExpedidoEn As Boolean = True
    Dim bandera_DomicilioFiscal As Boolean = True
    'DomicilioFiscal
    Dim DomicilioFiscal_Calle, DomicilioFiscal_noExterior, DomicilioFiscal_noInterior, DomicilioFiscal_Colonia, DomicilioFiscal_Localidad, DomicilioFiscal_referencia, DomicilioFiscal_municipio, DomicilioFiscal_estado, DomicilioFiscal_pais, DomicilioFiscal_codigoPostal As String
    'RegimenFiscal
    Dim Regimen As String = ""
    'Receptor
    Dim calle, receptor_rfc, receptor_nombre, noExterior, noInterior, colonia, localidad, referencia, municipio, estado, pais, codigoPostal As String
    Dim domi_id, contadorCalle, contadornoExterior, contadornoInterior, contadorcolonia, contadorlocalidad, contadorrfc, contadornombre As Integer
    Dim bandera_fechaComprobante = True
    'impuesto 
    Dim totalImpuestosRetenidos, totalImpuestosTrasladados As String
    Dim reten_id, trasla_id As Integer
    'Traslado 
    Dim Traslado_impuesto, Traslado_tasa, Traslado_importe As String
    'Retencion 
    Dim Retencion_impuesto, Retencion_importe As String
    'InformacionAduanera 
    Dim InformacionAduanera_numero, InformacionAduanera_fecha, InformacionAduanera_aduana, impuesto As String
    'ImpuestosLocales
    Dim ImpLocales_ImpLocRetenido As String = "0"
    Dim ImpLocales_TasadeRetencion As String = "0"
    Dim ImpLocales_Importe As String = "0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MySecurity.checkValidSession(Me) = False Then
            Response.Redirect("Logeo.aspx")
        End If
        If Not IsPostBack Then
            MensajeBienvenida()
            PreventingDoubleSubmit(btn_actualizar)
            Permisos()
        End If
    End Sub

    Sub Permisos()
        Dim ds As DataSet = dbt.GetDataSet("select * from usuario as u inner join permisos p on u.usua_id =p.usua_id where u.usua_id=" & Session("usua_id"))
        Dim correos, facturas, cuentas, subir, actulizar, administracion, alta, baja, modificar As Integer
        Dim pagosCancelaciones, firmas, OC, manual As Integer

        If ds.Tables(0).Rows(0).Item("correos") = 0 Then
            correos = 0
        Else
            correos = 1
        End If
        If ds.Tables(0).Rows(0).Item("facturas") = 0 Then
            facturas = 0
        Else
            facturas = 1
        End If
        If ds.Tables(0).Rows(0).Item("cuentas") = 0 Then
            cuentas = 0
        Else
            cuentas = 1
        End If
        If ds.Tables(0).Rows(0).Item("subir") = 0 Then
            subir = 0
        Else
            subir = 1
        End If
        If ds.Tables(0).Rows(0).Item("actualizar") = 0 Then
            actulizar = 0
        Else
            actulizar = 1
        End If
        If ds.Tables(0).Rows(0).Item("administracion") = 0 Then
            administracion = 0
        Else
            administracion = 1
        End If
        If ds.Tables(0).Rows(0).Item("alta") = 0 Then
            alta = 0
        Else
            alta = 1
        End If
        If ds.Tables(0).Rows(0).Item("baja") = 0 Then
            baja = 0
        Else
            baja = 1
        End If
        If ds.Tables(0).Rows(0).Item("modificar") = 0 Then
            modificar = 0
        Else
            modificar = 1
        End If
        If ds.Tables(0).Rows(0).Item("PagosCancelaciones") = 0 Then
            pagosCancelaciones = 0
        Else
            pagosCancelaciones = 1
        End If
        If ds.Tables(0).Rows(0).Item("firmas") = 0 Then
            firmas = 0
        Else
            firmas = 1
        End If
        If ds.Tables(0).Rows(0).Item("OC") = 0 Then
            OC = 0
        Else
            OC = 1
        End If

        Dim tipo_cuenta As DataSet

        tipo_cuenta = dbt.GetDataSet("select * from cuenta where cuen_id= " & Session("cuen_id"))

        If tipo_cuenta.Tables(0).Rows(0).Item("cuenta") = "Hospital Siena del Moral" Then
            manual = 1
        Else
            manual = 0
        End If
        System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "ocultar(" & correos & "," & facturas & "," & cuentas & "," & subir & "," & administracion & "," & actulizar & " ," & pagosCancelaciones & " ," & firmas & " ," & OC & ",0," & manual & ");", True)

    End Sub

    'Boton Actualizar , mensaje de procesando y bloqueo de botones
    Sub PreventingDoubleSubmit(ByVal button As Button)
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append(" document.getElementById('cargando').style.display = 'block'; ")
        sb.Append("if (typeof(Page_ClientValidate) == ' ') { ")
        sb.Append("var oldPage_IsValid = Page_IsValid; var oldPage_BlockSubmit = Page_BlockSubmit;")
        sb.Append("if (Page_ClientValidate('" + button.ValidationGroup + "') == false) {")
        sb.Append(" Page_IsValid = oldPage_IsValid; Page_BlockSubmit = oldPage_BlockSubmit; return false; }} ")
        sb.Append("this.disabled = true;")
        sb.Append(ClientScript.GetPostBackEventReference(button, Nothing) + ";")
        sb.Append("return true;  ")
        Dim submit_Button_onclick_js As String = sb.ToString()
        button.Attributes.Add("onclick", submit_Button_onclick_js)

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_actualizar.Click
        System.Threading.Thread.Sleep(5000)
        Me.Read_Emails()   
        MPE_confir.Show()
        'System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "document.getElementById('panel_confir').style.display = 'block';", True)
        Permisos()
    End Sub

    Private Sub Read_Emails()
        Try


            Dim num_correos_vacios As Integer = 0
            Dim num_c As Integer
            Dim num_Total_correos As Integer = 0 'Total de correos leidos
            Dim num_Total_facturas As Integer = 0
            Dim num_errores_correos As Integer = 0
            Dim p_ds As DataSet = dbt.GetDataSet("select * from cuenta_correos where estado='Correcto' and cuen_id=" & Session("cuen_id"))
            Dim num_CuentasdeCorreos As Integer = p_ds.Tables(0).Rows.Count
            Dim num_total_zip As Integer = 0
            Dim num_total_pdf As Integer = 0

            For c As Integer = num_CuentasdeCorreos - 1 To 0 Step -1
                Try
                    Dim pop3Client As Pop3Client
                    'Extencion
                    Dim ext As String
                    Dim path As String
                    Dim p_cuenta As String = p_ds.Tables(0).Rows(c).Item("cuenta")
                    Dim p_contrasenia As String = p_ds.Tables(0).Rows(c).Item("contrasenia")
                    Dim p_puerto As String = p_ds.Tables(0).Rows(c).Item("puerto")
                    Dim p_servidor As String = p_ds.Tables(0).Rows(c).Item("servidor")
                    Dim id_c As String = p_ds.Tables(0).Rows(c).Item("cuen_id")
                    Dim ssl As Boolean
                    If p_ds.Tables(0).Rows(c).Item("ssl") = 1 Then
                        ssl = True
                    Else
                        ssl = False
                    End If
                    If Session("Pop3Client") Is Nothing Then
                        Dim Callback As Security.RemoteCertificateValidationCallback = Function(s As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True
                        pop3Client = New Pop3Client()
                        pop3Client.Connect(p_servidor, p_puerto, ssl, 8000, 8000, Callback)
                        pop3Client.Authenticate(p_cuenta, p_contrasenia, AuthenticationMethod.TryBoth)
                        Session("Pop3Client") = pop3Client
                    Else
                        pop3Client = DirectCast(Session("Pop3Client"), Pop3Client)
                    End If
                    Dim count As Integer = pop3Client.GetMessageCount()
                    num_Total_correos = num_Total_correos + count
                    Me.Emails = New List(Of Email)()
                    Dim fecha_ultimo_correo As Date = ultimaFechaCorreo(p_cuenta, id_c)
                    For i As Integer = 1 To count
                        Try
                            Dim message As Message = pop3Client.GetMessage(i)
                            Dim email As New Email()
                            Dim fechacorreo As Date
                            Dim fechacorreo2 As String
                            fechacorreo = message.Headers.DateSent
                            fechacorreo2 = Format(fechacorreo, "dd/MM/yyyy HH:mm:ss")
                            If fechacorreo2 = "01/01/0001 00:00:00" Then
                                fechacorreo2 = "01/01/1900 00:00:00"
                            End If
                            If validarFecha(fechacorreo, fecha_ultimo_correo) Then
                                If validarCorreosSpam(message.Headers.From.DisplayName) Then
                                    email.MessageNumb = i
                                    'Motivo
                                    email.Subject = message.Headers.Subject
                                    'Fecha
                                    email.DateSent = message.Headers.DateSent
                                    'Nombre provedor
                                    email.From = String.Format(message.Headers.From.DisplayName, message.Headers.From.Address)
                                    Dim body As MessagePart = message.FindFirstPlainTextVersion()
                                    If body IsNot Nothing Then
                                        email.Body = body.GetBodyAsText()
                                    End If
                                    Dim attachments As List(Of MessagePart) = message.FindAllAttachments()

                                    If attachments.Count = 0 Then
                                        num_correos_vacios += 1
                                    End If

                                    For Each attachment As MessagePart In attachments
                                        Dim attachmentObj As New Attachment
                                        attachmentObj.FileName = attachment.FileName 'Nombre archivo
                                        attachmentObj.ContentType = attachment.ContentType.MediaType
                                        attachmentObj.Content = attachment.Body
                                        email.Attachments.Add(attachmentObj)
                                        ext = Extencion(attachment.FileName, ".")
                                        If ext = "pdf" Or ext = "xml" Or ext = "rar" Or ext = "zip" Or ext = "PDF" Or ext = "XML" Or ext = "RAR" Or ext = "ZIP" Then
                                            If ext = "rar" Then
                                                ExtraerRar(attachment.FileName, attachment.Body)
                                                num_total_zip += 1
                                            ElseIf ext = "zip" Then
                                                ExtraerZip(attachment.FileName, attachment.Body)
                                                num_total_zip += 1
                                            Else
                                                If ext = "pdf" Then
                                                    num_total_pdf += 1
                                                End If

                                                'If errorTotal = False Then
                                                '    dbt.ExecuteNonQuery(consultaFinal)
                                                'End If

                                                'consultaFinal = ""
                                                'errorTotal = False


                                                consultaFinal = consultaFinal + "insert into contenido values ('" & ext & "','" & attachment.FileName & "' ,  '" & CambiarCaracteres(DateTime.Now.ToString("yyyyMMdd") & "" & attachment.FileName) & "','" & path & "'," & ultimoID("correos", "corre_id", True) + 1 & " )"

                                                path = System.AppDomain.CurrentDomain.BaseDirectory() & "\Archivos\" & CambiarCaracteres(DateTime.Now.ToString("yyyyMMdd") & "" & attachment.FileName)
                                                dbt.ExecuteNonQuery("insert into contenido values ('" & ext & "','" & attachment.FileName & "' ,  '" & CambiarCaracteres(DateTime.Now.ToString("yyyyMMdd") & "" & attachment.FileName) & "','" & path & "'," & ultimoID("correos", "corre_id", True) + 1 & " )")
                                                cerarArchivo(path, attachment.Body)
                                                If ext = "xml" Then
                                                    leerxml(path)
                                                    'guardar_correo = True
                                                    num_Total_facturas += 1
                                                End If
                                            End If
                                        End If
                                    Next
                                    Try
                                        'If guardar_correo = True Then
                                        num_c = num_c + 1


                                        'consultaFinal = consultaFinal + "insert into correos values ('" & email.From & "','" & message.Headers.From.Address & "','" & CambiarCaracteres2(message.Headers.Subject, "'") & "','" & CambiarCaracteres2(email.Body, "'") & "','" & fechacorreo2 & "'," & Session("cuen_id") & ",'" & correos_tipo & "','" & p_cuenta & "','Activo') "


                                        correos_tipo = validarTipoDecoreo(p_cuenta, message.Headers.From.Address, message.Headers.To(0).Address)
                                        dbt.ExecuteNonQuery("insert into correos values ('" & email.From & "','" & message.Headers.From.Address & "','" & CambiarCaracteres2(message.Headers.Subject, "'") & "','" & CambiarCaracteres2(email.Body, "'") & "','" & fechacorreo2 & "'," & Session("cuen_id") & ",'" & correos_tipo & "','" & p_cuenta & "','Activo') ")
                                        guardar_correo = False
                                        ValidarPDF()

                                        'End If
                                    Catch ex As Exception
                                        dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() ,'guardar correo'," & count & ")")
                                    End Try
                                End If 'Fin validar correos spam
                            End If
                        Catch ex As Exception
                            dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() ,'guardar correo'," & count & ")")
                        End Try
                    Next
                    Session("Pop3Client") = Nothing
                    dbt.ExecuteNonQuery("insert into lecturaCorreos values ('" & count & "', '" & Session("cuen_id") & "', '" & num_c & "')")

                Catch ex As Exception
                    dbt.ExecuteNonQuery("insert into errores values('" & ex.Message & "',GETDATE() ,'aaaaaa'," & c & ")")
                    num_errores_correos += 1
                    'dbt.Disconnect()
                    Session("Pop3Client") = Nothing
                End Try
            Next
            lb_cuentas.Text = num_CuentasdeCorreos
            lb_correos_leidos.Text = num_Total_correos
            lb_correos_guardados.Text = num_c
            lb_archivos_comp.Text = num_total_zip
            lb_facturas_gua.Text = num_Total_facturas
            lb_factutas_nogua.Text = num_errores_correos
            lb_pdf_guar.Text = num_total_pdf
            lb_fact_repetidas.Text = numfacturas_Repetidas
            lb_noperteneceRFC.Text = num_facturas_noperteneceRFC


            'MsgBox(num_correos_vacios)


        Catch ex As Exception
            dbt.ExecuteNonQuery("insert into errores values('" & ex.Message & "',GETDATE() ,'Read_Emails()',0)")
            dbt.Disconnect()
            Session("Pop3Client") = Nothing
        End Try
    End Sub
    Sub ValidarPDF()
        Try
            Dim dsxml As DataSet
            Dim dspfd As DataSet
            Dim xml As String
            Dim pdf_id As String
            Dim id_comp As String
            Dim id As String = ultimoID("correos", "corre_id", False)
            dsxml = dbt.GetDataSet("select * from comprobante com inner join contenido con on com.xml_id = con.cont_id where com.corre_id=" & id & " and cont_extencion='xml'")
            For c As Integer = 0 To dsxml.Tables(0).Rows.Count - 1
                xml = dsxml.Tables(0).Rows(c).Item("cont_nombre")
                id_comp = dsxml.Tables(0).Rows(c).Item("comp_id")
                Dim result As String = System.IO.Path.GetFileNameWithoutExtension(xml)
                dspfd = dbt.GetDataSet("select cont_id,cont_nombre" & _
                                        " from correos cor inner join contenido con on cor.corre_id=con.corre_id " & _
                                        " where cor.corre_id=" & id & " and cont_extencion='pdf' and cont_nombre like '%" & result & "%'")
                If dspfd.Tables(0).Rows.Count = 1 Then
                    pdf_id = dspfd.Tables(0).Rows(0).Item("cont_id")
                    dbt.ExecuteNonQuery("update comprobante set pdf_id =" & pdf_id & "where comp_id= " & id_comp)
                End If
            Next
        Catch ex As Exception
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
    Protected Property Emails() As List(Of Email)
        Get
            Return DirectCast(ViewState("Emails"), List(Of Email))
        End Get
        Set(value As List(Of Email))
            ViewState("Emails") = value
        End Set
    End Property

    Sub MensajeBienvenida()
        Try
            Dim ds As DataSet = dbt.GetDataSet("select (u.Nombre+' '+u.app+' '+apm) as 'Nombre', cuenta as 'Cuenta' from usuario as u inner join cuenta as c on u.cuen_id=c.cuen_id where c.cuen_id=" & Session("cuen_id"))
            mensajes.setMessage("Bienvenido " & ds.Tables(0).Rows(0).Item("Nombre") & ".", 10000)
        Catch ex As Exception
        End Try
    End Sub

    Sub leerxml(ByVal Archivo As String)
        Try
            Dim contador As Integer = 1
            Dim contador2 As Integer = 1
            Dim xml As String = ""
            Dim reader As XmlTextReader = New XmlTextReader(Archivo)

            Try

                If reader.Name = "tfd:NomSucEmpresa" Then
                    'MsgBox("asdadas")
                End If
                Dim validador As Boolean
                Try
                    reader.Read()
                    validador = True

                Catch ex As Exception
                    validador = False
                End Try

                If validador = True Then

                    Do While (reader.Read())
                        xml = xml & ("<" + reader.Name)
                        Try
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
                            ElseIf reader.Name = "implocal:ImpuestosLocales" Then
                                ImpuestosLocales = True
                            End If
                        Catch ex As Exception
                            dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() , '1 -" & folio & "',0)")

                        End Try
                        Try

                            If reader.HasAttributes Then 'If attributes exist
                                xml = xml & ">"
                                While reader.MoveToNextAttribute()
                                    'Mostrar valor y nombre del atributo
                                    Const quote As String = """"
                                    xml = xml & reader.Name & " = " & quote & reader.Value & quote
                                    If reader.Name = "cantidad" Then
                                        conc_guardar = True
                                    ElseIf reader.Name = "numero" Then
                                        InformacionAduanera_guardar = True
                                    ElseIf reader.Name = "impuesto" Then
                                        If Traslados = True Then
                                            traslado_guardar = True
                                        End If
                                        If Retencion = True Then
                                            Retencion_guardar = True
                                        End If
                                    End If
                                    Guardarxml(reader.Name, reader.Value)
                                End While
                            End If

                        Catch ex As Exception
                            dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() , '2 -" & folio & "',0)")

                        End Try
                        Try

                            xml = xml & (">")

                            If traslado_guardar = True And Traslados = True And Retencion = False Then
                                impu_id = ultimoID("impuesto", "impu_id", False)

                                consultaFinal = consultaFinal + "insert into Traslado values ('" & Traslado_impuesto & "','" & Traslado_tasa & "','" & Traslado_importe & "','" & impu_id & "')"


                                dbt.ExecuteNonQuery("insert into Traslado values ('" & Traslado_impuesto & "','" & Traslado_tasa & "','" & Traslado_importe & "','" & impu_id & "')")
                                traslado_guardar = False
                                Traslados = False
                                Traslado_impuesto = ""
                                Traslado_tasa = ""
                                Traslado_importe = ""
                            End If
                            If ImpuestosLocales = True Then
                                If ImpLocales_Importe <> "0" Then

                                    consultaFinal = consultaFinal + "insert into ImpuestosLocales values ('" & ImpLocales_ImpLocRetenido & "'," & ImpLocales_TasadeRetencion & "," & ImpLocales_Importe & "," & ultimoID("comprobante", "comp_id", False) + 1 & ")"

                                    dbt.ExecuteNonQuery("insert into ImpuestosLocales values ('" & ImpLocales_ImpLocRetenido & "'," & ImpLocales_TasadeRetencion & "," & ImpLocales_Importe & "," & ultimoID("comprobante", "comp_id", False) + 1 & ")")
                                    ImpuestosLocales = False
                                    ImpLocales_Importe = "0"
                                    ImpLocales_ImpLocRetenido = "0"
                                    ImpLocales_TasadeRetencion = "0"
                                End If
                            End If

                        Catch ex As Exception
                            dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() , '3 -" & folio & "',0)")

                        End Try


                    Loop

                End If

            Catch ex As Exception
                dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() , 'qwwewqewqeqw -" & folio & "',0)")

            End Try

            Try
                If Emisor = True Then

                    consultaFinal = consultaFinal + "insert into emisor values ('" & rfc & "', '" & nombre & "','" & domifical_id & "','" & expe_id & "','" & regi_id & "')"

                    dbt.ExecuteNonQuery("insert into emisor values ('" & rfc & "', '" & nombre & "','" & domifical_id & "','" & expe_id & "','" & regi_id & "')")
                    Emisor = False
                End If
                If DomicilioFiscal = True Then


                    consultaFinal = consultaFinal + "insert into DomicilioFiscal values ('" & DomicilioFiscal_Calle & "','" & DomicilioFiscal_noExterior & "','" & DomicilioFiscal_noInterior & "','" & DomicilioFiscal_Colonia & "','" & DomicilioFiscal_Localidad & "','" & DomicilioFiscal_referencia & "','" & DomicilioFiscal_municipio & "','" & DomicilioFiscal_estado & "','" & DomicilioFiscal_pais & "','" & DomicilioFiscal_codigoPostal & "')"

                    dbt.ExecuteNonQuery("insert into DomicilioFiscal values ('" & DomicilioFiscal_Calle & "','" & DomicilioFiscal_noExterior & "','" & DomicilioFiscal_noInterior & "','" & DomicilioFiscal_Colonia & "','" & DomicilioFiscal_Localidad & "','" & DomicilioFiscal_referencia & "','" & DomicilioFiscal_municipio & "','" & DomicilioFiscal_estado & "','" & DomicilioFiscal_pais & "','" & DomicilioFiscal_codigoPostal & "')")
                    DomicilioFiscal = False
                    limpiarDomicilioFiscal()
                End If
                If ExpedidoEn = True Then


                    consultaFinal = consultaFinal + "insert into ExpedidoEn values ('" & ExpedidoEn_Calle & "','" & ExpedidoEn_noExterior & "','" & ExpedidoEn_noInterior & "','" & ExpedidoEn_colonia & "','" & ExpedidoEn_localidad & "','" & ExpedidoEn_referencia & "','" & ExpedidoEn_municipio & "','" & ExpedidoEn_estado & "','" & ExpedidoEn_pais & "','" & ExpedidoEn_codigoPostal & "')"

                    dbt.ExecuteNonQuery("insert into ExpedidoEn values ('" & ExpedidoEn_Calle & "','" & ExpedidoEn_noExterior & "','" & ExpedidoEn_noInterior & "','" & ExpedidoEn_colonia & "','" & ExpedidoEn_localidad & "','" & ExpedidoEn_referencia & "','" & ExpedidoEn_municipio & "','" & ExpedidoEn_estado & "','" & ExpedidoEn_pais & "','" & ExpedidoEn_codigoPostal & "')")
                    ExpedidoEn = False
                    limpiarExpedidoEn()
                End If
                If RegimenFiscal = True Then

                    consultaFinal = consultaFinal + "insert into RegimenFiscal values ('" & Regimen & "')"


                    dbt.ExecuteNonQuery("insert into RegimenFiscal values ('" & Regimen & "')")
                    RegimenFiscal = False
                End If
                If Receptor = True Then
                    If Domicilio = True Then

                        consultaFinal = consultaFinal + "insert into domicilio values ('" & calle & "','" & noExterior & "','" & noInterior & "','" & colonia & "','" & localidad & "','" & referencia & "','" & municipio & "','" & estado & "','" & pais & "','" & codigoPostal & "')"

                        dbt.ExecuteNonQuery("insert into domicilio values ('" & calle & "','" & noExterior & "','" & noInterior & "','" & colonia & "','" & localidad & "','" & referencia & "','" & municipio & "','" & estado & "','" & pais & "','" & codigoPostal & "')")
                        Dim ds_domicilo As DataSet
                        ds_domicilo = dbt.GetDataSet("select top 1 * from domicilio order by domi_id desc")


                        domi_id = ds_domicilo.Tables(0).Rows(0).Item("domi_id")

                        'domi_id = ultimoID("domicilio", "domi_id", False)


                        consultaFinal = consultaFinal + "insert into Receptor values ('" & receptor_rfc & "','" & receptor_nombre & "','" & domi_id & "')"

                        dbt.ExecuteNonQuery("insert into Receptor values ('" & receptor_rfc & "','" & receptor_nombre & "','" & domi_id & "')")
                        Domicilio = False
                        Receptor = False
                        limpiarReceptor()
                    End If
                End If


            Catch ex As Exception
                dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() , '5 -" & folio & "',0)")

            End Try

            Try
                emis_id = ultimoID("emisor", "emis_id", False)
                rece_id = ultimoID("receptor", "rece_id", False)
                impu_id = ultimoID("impuesto", "impu_id", False)
                domifical_id = ultimoID("DomicilioFiscal", "domifical_id", False)
                reten_id = ultimoID("Retencion", "reten_id", False)
                trasla_id = ultimoID("Traslado", "trasla_id", False)
                comple_id = 1
                adde_id = 1
                Dim conte_id As String
                conte_id = ultimoID("contenido", "cont_id", False)
                Dim corre_id As String
                corre_id = ultimoID("correos", "corre_id", True) + 1
                Try


                    If validarUUid(uuid) = True Then


                    End If


                    Try
                        Dim datee As Date = fecha
                        fecha = Format(datee, "dd-MM-yy H:mm:ss")

                        If tipo_comprobante = "No pertenece" Then
                            num_facturas_noperteneceRFC += 1
                        End If
                    Catch ex As Exception
                        fecha = "17-05-1999"
                    End Try

                    consultaFinal = consultaFinal + "insert into comprobante values('" & version & "','" & serie & "','" & folio & "','" & fecha & "','" & sello & "','" & formaDePago & "','" & noCertificado & "','" & certificado & _
                                                                     "','" & condicionesDePago & "','" & Subtotal & "','" & Descuento & "','" & MotivoDescuento & "','" & TipodeCambio & "','" & Moneda & "','" & CambiarCaracteres3(Total, ",", "$") & "','" & TipoDeComprobante & _
                                                                     "','" & MetodoDePago & "','" & LugarExpedicion & "','" & NumCtaPago & "','" & FolioFiscalOrig & "','" & SerieFolioFiscalOrig & _
                                                                     "','" & FechaFolioFiscalOrig & "','" & MontoFolioFiscalOrig & "','" & emis_id & "','" & rece_id & "','" & impu_id & "','" & comple_id & "','" & adde_id & "'," & Session("cuen_id") & ",'Correo'," & ultimoID("contenido", "cont_id", False) & ",0,'" & corre_id & "','" & uuid & "','" & tipo_comprobante & "','Sin pagar')"


                    dbt.ExecuteNonQuery("insert into comprobante values('" & version & "','" & serie & "','" & folio & "','" & fecha & "','" & sello & "','" & formaDePago & "','" & noCertificado & "','" & certificado & _
                                                                         "','" & condicionesDePago & "','" & Subtotal & "','" & Descuento & "','" & MotivoDescuento & "','" & TipodeCambio & "','" & Moneda & "','" & CambiarCaracteres3(Total, ",", "$") & "','" & TipoDeComprobante & _
                                                                         "','" & MetodoDePago & "','" & LugarExpedicion & "','" & NumCtaPago & "','" & FolioFiscalOrig & "','" & SerieFolioFiscalOrig & _
                                                                         "','" & FechaFolioFiscalOrig & "','" & MontoFolioFiscalOrig & "','" & emis_id & "','" & rece_id & "','" & impu_id & "','" & comple_id & "','" & adde_id & "'," & Session("cuen_id") & ",'Correo'," & ultimoID("contenido", "cont_id", False) & ",0,'" & corre_id & "','" & uuid & "','" & tipo_comprobante & "','Sin pagar')")
                Catch ex As Exception
                    dbt.ExecuteNonQuery("insert into errores values('" & ex.Message & "',GETDATE() , 'aaaaaaaaaa',0")
                End Try
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
                Descuento = ""
            Catch ex As Exception
                dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() , '6 -" & folio & "',0)")

            End Try

        Catch ex As Exception
            dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() , 'leer factura-" & folio & "',0)")

            errorTotal = True

            'emis_id = ultimoID("emisor", "emis_id", False) + 1
            'dbt.ExecuteNonQuery("delete Emisor where emis_id=" & emis_id)


        End Try
    End Sub

    Sub Guardarxml(ByVal Name As String, ByVal Value As String) ' vercion 3.2

        Try
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
                    tipo_comprobante = "No pertenece"

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
                    tipo_comprobante = "No pertenece"
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
                If descripcion = "MERREM 1 GR." Then
                End If

                consultaFinal = consultaFinal + "insert into concepto values ('" & Cantidad & "','" & unidad & "','" & noIdentificacion & "','" & CambiarCaracteres2(descripcion, "'") & "','" & valorUnitario & "','" & importe & "','" & ultimoID("comprobante", "comp_id", True) + 1 & "')"

                    dbt.ExecuteNonQuery("insert into concepto values ('" & Cantidad & "','" & unidad & "','" & noIdentificacion & "','" & CambiarCaracteres2(descripcion, "'") & "','" & valorUnitario & "','" & importe & "','" & ultimoID("comprobante", "comp_id", True) + 1 & "')")
                noIdentificacion = ""
                conc_guardar = False
            End If
            If Retencion_guardar = True And Retencion = True And Traslados = False Then
                impu_id = ultimoID("impuesto", "impu_id", False)

                consultaFinal = consultaFinal + "insert into Retencion values ('" & impuesto & "','" & importe & "','" & impu_id & "'  ) "

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
            'ds_conc = dbt.GetDataSet("select top 1 * from concepto order by conc_id desc")
            'conc_id = ds_conc.Tables(0).Rows(0).Item("conc_id")



            conc_id = ultimoID("concepto", "conc_id", False)

            consultaFinal = consultaFinal + "insert into InformacionAduanera  values ('" & InformacionAduanera_numero & "','" & InformacionAduanera_fecha & "','" & InformacionAduanera_aduana & "','" & conc_id & "')"




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


            consultaFinal = consultaFinal + "insert into impuesto values ('" & totalImpuestosRetenidos & "','" & totalImpuestosTrasladados & "')"

                dbt.ExecuteNonQuery("insert into impuesto values ('" & totalImpuestosRetenidos & "','" & totalImpuestosTrasladados & "')")
            totalImpuestosRetenidos = ""
            totalImpuestosTrasladados = ""
        ElseIf Name = "ImpLocRetenido" Then
            ImpLocales_ImpLocRetenido = Value
        ElseIf Name = "TasadeRetencion" Then
            ImpLocales_TasadeRetencion = Value
            End If

        Catch ex As Exception
            dbt.ExecuteNonQuery("insert into errores values('" & CambiarCaracteres2(ex.Message, "'") & "',GETDATE() , 'guardar factura-" & folio & "',0)")
        End Try


    End Sub

    Function ultimoID(ByVal tabla As String, ByVal elemento As String, ByVal sum As Boolean) As Integer
        Dim id As Integer
        Dim ds As DataSet = dbt.GetDataSet("select top 1 * from " & tabla & " order by " & elemento & " desc")
        Try
            id = ds.Tables(0).Rows(0).Item(elemento)
        Catch ex As Exception
            'If sum Then
            '    id = 0
            'Else
            id = 1
            'End If
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

    Sub salir()
        Session.Abandon()
        Session.Clear()
        FormsAuthentication.SignOut()
        Response.Redirect("logeo.aspx")
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

    Function CambiarCaracteres2(ByVal archivo As String, ByVal Caracter As String)
        Dim res As String = ""
        Try
            For i As Integer = 0 To archivo.Length - 1
                Dim val As String = archivo(i)
                If val <> Caracter Then
                    res = res & val
                End If
            Next
        Catch ex As Exception
        End Try
        Return res
    End Function

    Function CambiarCaracteres3(ByVal archivo As String, ByVal Caracter1 As String, ByVal Caracter2 As String)
        Dim res As String = ""
        Try
            For i As Integer = 0 To archivo.Length - 1
                Dim val As String = archivo(i)
                If val <> Caracter1 And val <> Caracter2 Then
                    res = res & val
                End If
            Next
        Catch ex As Exception
        End Try
        Return res
    End Function

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        salir()
    End Sub


    Private Sub ExtraerZip(ByVal Archivo As String, ByVal body As Object)
        Try
            Dim Path As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Archivos\" & Archivo
            Dim DirectorioExtraccion As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Archivos\"
            cerarArchivo(Path, body)
            Using zip1 As ZipFile = ZipFile.Read(Path)
                Dim e As ZipEntry
                For Each e In zip1
                    Dim ext As String = Extencion(e.FileName, ".")
                    If ext = "pdf" Or ext = "xml" Then
                        'Extraer archivo 
                        e.Extract(DirectorioExtraccion, ExtractExistingFileAction.OverwriteSilently)
                        'Guardar en base de datos 
                        guardar_correo = True

                        consultaFinal = consultaFinal = "insert into contenido values ('" & ext & "','" & e.FileName & "','" & DateTime.Now.ToString("yyyyMMdd") & "" & e.FileName & "','" & Path & "'," & ultimoID("correos", "corre_id", True) + 1 & ")"

                        dbt.ExecuteNonQuery("insert into contenido values ('" & ext & "','" & e.FileName & "','" & DateTime.Now.ToString("yyyyMMdd") & "" & e.FileName & "','" & Path & "'," & ultimoID("correos", "corre_id", True) + 1 & ")")
                    End If
                Next
            End Using
        Catch ex As Exception
            dbt.ExecuteNonQuery("insert into errores values('" & ex.Message & "',GETDATE(),'ExtraerZip',0)")
        End Try
    End Sub

    Sub ExtraerRar(ByVal Archivo As String, ByVal body As Object)
        Try
            Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Archivos\" & Archivo
            Dim a As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Archivos\ "
            cerarArchivo(path, body)
            Dim archive As IArchive = ArchiveFactory.Open(path)
            For Each entry In archive.Entries
                If Not entry.IsDirectory Then
                    Dim ext As String = Extencion(entry.FilePath, ".")
                    If ext = "pdf" Or ext = "xml" Then
                        'Extraer archivo 
                        entry.WriteToDirectory(a, ExtractOptions.ExtractFullPath Or ExtractOptions.Overwrite)
                        guardar_correo = True
                        'Guardar en base de datos 

                        consultaFinal = consultaFinal + "insert into contenido values ('" & ext & "','" & entry.FilePath & "','" & DateTime.Now.ToString("yyyyMMdd") & "" & entry.FilePath & "','" & path & "'," & ultimoID("correos", "corre_id", True) + 1 & ")"


                        dbt.ExecuteNonQuery("insert into contenido values ('" & ext & "','" & entry.FilePath & "','" & DateTime.Now.ToString("yyyyMMdd") & "" & entry.FilePath & "','" & path & "'," & ultimoID("correos", "corre_id", True) + 1 & ")")
                    End If
                End If
            Next
        Catch ex As Exception
            dbt.ExecuteNonQuery("insert into errores values('" & ex.Message & "',GETDATE() ,'ExtraerRar',0)")
        End Try
    End Sub

    'Validar Estado del correo
    Function validarCorreosEstado(ByVal cuen_id As String) As Boolean
        Dim val As Boolean = True
        Try
            Dim ds As DataSet
            ds = dbt.GetDataSet("select * from cuenta_correos where cuen_corre_id='" & cuen_id & "'")
            If ds.Tables(0).Rows(0).Item("estado") = "Correcto" Then
                val = True
            Else
                val = False
            End If
        Catch ex As Exception
            dbt.ExecuteNonQuery("insert into errores values('" & ex.Message & "',GETDATE() ,'validarCorreosEstado',0)")
        End Try
        Return val
    End Function
    'Validar Correos spam
    Function validarCorreosSpam(ByVal DisplayName As String) As Boolean
        Dim val As Boolean = True
        Try
            Dim ds As DataSet = dbt.GetDataSet("select * from correosBloqueados where DisplayName like '%" & DisplayName & "%' ")
            If ds.Tables(0).Rows.Count >= 1 Then
                val = False
            End If
        Catch ex As Exception
            val = True
        End Try
        Return val
    End Function

    Function validarTipoDecoreo(ByVal correo As String, ByVal correoF As String, ByVal correoT As String)
        Dim val As String = ""
        If correo = correoF Then
            val = "Emitido"
            If correoF = correoT Then
                val = "Auto"
            End If
        ElseIf correo <> correoF Then
            val = "Recibido"
        End If
        Return val
    End Function

    Function validarFecha(ByVal fecha As Date, ByVal fecha_ultimo_correo As Date) As Boolean
        Dim val As Boolean = True
        Try
            If fecha > fecha_ultimo_correo Then
                val = True
            Else
                val = False
            End If
        Catch ex As Exception
        End Try
        Return val
    End Function

    Function ultimaFechaCorreo(ByVal p_cuenta As String, ByVal id_c As String) As Date
        Dim val As Date
        Try
            Dim ds As DataSet
            ds = dbt.GetDataSet("select * from correos where cuen_id =" & Session("cuen_id") & " and corre_receptor='" & p_cuenta & "' and cuen_id =" & id_c & " and estado='Activo' order by corre_fecha  desc")
            val = ds.Tables(0).Rows(0).Item("corre_fecha")
        Catch ex As Exception
        End Try
        Return val
    End Function

    Function Extencion(Path As String, Caracter As String) As String
        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        Extencion = ret
    End Function

    Function validarUUid(ByVal uuid As String) As Boolean
        Dim val As Boolean
        Try
            Dim ds As DataSet = dbt.GetDataSet("select * from comprobante where uuid='" & uuid & "'   and cuen_id= " & Session("cuen_id"))
            If ds.Tables(0).Rows.Count = 1 Then
                val = False
                tipo_comprobante = "uudiRepetido"
                numfacturas_Repetidas += 1
            Else
                val = True
            End If
        Catch ex As Exception
            val = True
        End Try
        Return val
    End Function
End Class



