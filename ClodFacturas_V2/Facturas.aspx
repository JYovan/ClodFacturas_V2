<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Facturas.aspx.vb" Inherits="ClodFacturas_V2.Facturas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="css/EstilosFacturas.css" />
    <%-- Mensajes --%>
    <link href="~/css/Mensajes.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Mensajes.min.js"></script>
    <script type="text/javascript" src="js/jquery.Mensajes_minimized.js"></script>
    <%-- Descargas --%>
    <script type="text/javascript" src="js/download.js"></script>


    <script src="js/modernizr-1.5.min.js"></script>
    <link href="~/css/iframe.css" rel="stylesheet" type="text/css" />
    <link href="~/css/jquery.jgrowl.css" rel="stylesheet" type="text/css" />


</head>

<script type="text/javascript">
    function uploadComplete(sender) {

        document.getElementById('<%= Button1.ClientID%>').click();

    }
    function uploadError(sender) {

    }

    function Cargar() {

        parent.location.reload();
    }


</script>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="principal" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                    AssociatedUpdatePanelID="principal" DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="loadingAnimation">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/img/spinner.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <section id="contenido">
                    <article id="filtros">
                        <asp:Panel ID="panel_filtros" runat="server" CssClass="panel" DefaultButton="btn_buscar">
                            <div3>
                    <div1>
                        <asp:Label ID="Label7" runat="server" Text="Factura:"></asp:Label>
                    </div1>
                   <div2>
                        <asp:TextBox ID="txtNombreFactura" runat="server"  class="TextBox"></asp:TextBox>
                               
                                       <asp:AutoCompleteExtender ID="tx_buscar_AutoCompleteExtender" runat="server" enabled="True"
                                        servicepath="buscar.asmx" minimumprefixlength="2" servicemethod="ObtListaPeliculas"
                                        enablecaching="true" targetcontrolid="txtNombreFactura" usecontextkey="True" completionsetcount="10"
                                        completioninterval="200"
                                           CompletionListCssClass="completionList"
                                         CompletionListItemCssClass="listItem"
                                         CompletionListHighlightedItemCssClass="itemHighlighted"> 
                                    </asp:AutoCompleteExtender>
                    </div2>
                </div3>
                            <div3>
                    <div1>
                        <asp:Label ID="Label1" runat="server" Text="Proveedor RFC:"></asp:Label>
                    </div1>
                    <div2>
                        <asp:TextBox ID="txt_rfc_proveedor" runat="server" class="TextBox" ></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" enabled="True"
                                        servicepath="buscar.asmx" minimumprefixlength="2" servicemethod="ListaRFC"
                                        enablecaching="true" targetcontrolid="txt_rfc_proveedor" usecontextkey="True" completionsetcount="10"
                                        completioninterval="200"
                                           CompletionListCssClass="completionList"
                                         CompletionListItemCssClass="listItem"
                                         CompletionListHighlightedItemCssClass="itemHighlighted"> 
                                    </asp:AutoCompleteExtender>
                    </div2>
                </div3>
                <div3>
                    <div1>
                        <asp:Label ID="label11" runat="server" Text="Proveedor Nombre:"></asp:Label>                                     
                    </div1>
                    <div2>
                        <asp:TextBox ID="txt_nombre_proveedor" runat="server" class="TextBox"></asp:TextBox>
                    </div2>
                </div3>
                
                
                            <div3>
                    <div1>
                        <asp:Label ID="label8" runat="server" Text="Folio:"></asp:Label>                            
                    </div1>
                    <div2>
                        <asp:TextBox ID="txt_folio" runat="server" class="TextBox"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" enabled="True"
                                        servicepath="buscar.asmx" minimumprefixlength="1" servicemethod="ListaFolios"
                                        enablecaching="true" targetcontrolid="txt_folio" usecontextkey="True" completionsetcount="10"
                                        completioninterval="200"
                                           CompletionListCssClass="completionList"
                                         CompletionListItemCssClass="listItem"
                                         CompletionListHighlightedItemCssClass="itemHighlighted"> 
                                    </asp:AutoCompleteExtender>
                    </div2>
                </div3>
                            <div3>
                    <div1>
                         <asp:Label ID="label9" runat="server" Text="N° Certificado:"></asp:Label>                     
                    </div1>
                    <div2>
                        <asp:TextBox ID="txt_numCertificado" runat="server" class="TextBox"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" enabled="True"
                                        servicepath="buscar.asmx" minimumprefixlength="1" servicemethod="ListaNCertificado"
                                        enablecaching="true" targetcontrolid="txt_numCertificado" usecontextkey="True" completionsetcount="10"
                                        completioninterval="200"
                                           CompletionListCssClass="completionList"
                                         CompletionListItemCssClass="listItem"
                                         CompletionListHighlightedItemCssClass="itemHighlighted"> 
                                    </asp:AutoCompleteExtender>
                    </div2>
                </div3>
                <br />
                <div3>
                    <div1>
                        <asp:Label ID="label10" runat="server" Text="De:"></asp:Label>                        
                    </div1>
                    <div2>
                        <asp:TextBox ID="txt_FechaDe" runat="server" class="TextBox" style="width:100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtFechaDel_CalendarExtender" runat="server"
                                        __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_FechaDe">
                                    </asp:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                        ControlToValidate="txt_FechaDe" ErrorMessage="Mal"
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" CssClass="bubble">
                                    </asp:RegularExpressionValidator>
                    </div2>
                </div3>
                <br />
                <div3>
                    <div1>
                           <asp:Label ID="label12" runat="server" Text="Al:"></asp:Label>                         
                    </div1>
                    <dv2>
                        <asp:TextBox ID="txt_FechaAl" runat="server" class="TextBox"  style="width:100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                        __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_FechaAl">
                                    </asp:CalendarExtender>
                    </dv2>
                </div3>
                            <br />
                            <br />
                            <asp:Panel ID="panelfil" runat="server" GroupingText="Ordenar" Width="650px">
                                <table id="radioB" style="width: 100%">
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Ordenar por:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_por_fecha" runat="server" GroupName="por" Text="Fecha" Checked="true" AutoPostBack="true" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_por_proveedor" runat="server" GroupName="por" Text="Proveedor" AutoPostBack="true" />
                                        </td>
                                        <td></td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_por_factura" runat="server" GroupName="por" Text="Factura" AutoPostBack="true" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_por_folio" runat="server" GroupName="por" Text="Folio" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Mostrar:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_top_10" runat="server" GroupName="top" Text="10" Checked="true" AutoPostBack="true" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_top_20" runat="server" GroupName="top" Text="20" AutoPostBack="true" />
                                        </td>
                                        <td></td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_top_50" runat="server" GroupName="top" Text="50" AutoPostBack="true" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_top_todos" runat="server" GroupName="top" Text="Todos" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text="Subido por:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="Radio_correo" runat="server" GroupName="subido_por" Text="Por correo" AutoPostBack="true" Checked="true" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="Radio_examninados" runat="server" GroupName="subido_por" Text="Examinados" AutoPostBack="true" />
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:RadioButton ID="Radio_e_c" runat="server" GroupName="subido_por" Text="Todos" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Ordenar de:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_desc" runat="server" GroupName="orden" Text="Descendente" Checked="true" AutoPostBack="true" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_asc" runat="server" GroupName="orden" Text="Ascendente" AutoPostBack="true" />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Tipo:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_Recibido" runat="server" GroupName="tipo" Text="Ingreso" Checked="true" AutoPostBack="true" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RadioButton ID="Radio_emitido" runat="server" GroupName="tipo" Text="Egreso" AutoPostBack="true" />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Mostrar :
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="Chec_nomfact" runat="server" Text="Nombre factura" AutoPostBack="true" Checked="true" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="Chec_horas" runat="server" Text="Hora/min/seg" AutoPostBack="true" />
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="Chec_cerNum" runat="server" Text="N. Certificado" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="che_Stodos" runat="server" AutoPostBack="true" Text="Seleccionar todos" CssClass="abajo"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="che_oc" runat="server" AutoPostBack="true" Text="Ordenes de compra" CssClass="abajo"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btn_buscar" runat="server" BackColor="White" BorderStyle="None" CssClass="botoninv" Height="0" Text="Button" Width="0" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </article>
                    <article id="Botones">
                    </article>
                    <article id="Botones2">
                        <div4>                  
                          <asp:Label ID="Label18" runat="server" Text="Resultados: "></asp:Label>  <asp:Label ID="Label17" runat="server" Text="0"></asp:Label>                              
                        </div4>
                        <br />
                        <divb1>
                             <a href="#" alt="Descargar zip" class="tooltip">  
                                  <asp:ImageButton ID="btn_inicio" runat="server" ImageUrl="/img/zip.png" CssClass="boton" BackColor="Green" ValidationGroup="inicio"    />
                             </a>
                        </divb1>
                        <divb2>
                            <a href="#" alt="Crear reporte" class="tooltip">  
                                 <asp:ImageButton ID="btn_crearreporte" runat="server" ImageUrl="/img/reporte.png" CssClass="boton" BackColor="Green" ValidationGroup="inicio" />                         
                            </a>
                        </divb2>
                        <divb3>
                            <a href="#" alt="Eliminar facturas" class="tooltip" style="visibility:hidden">  
                                  <asp:ImageButton ID="btn_eliminarFacturas" runat="server" ImageUrl="/img/eliminar.png" CssClass="boton" BackColor="Green" ValidationGroup="inicio" /> 
                            </a>
                       </divb3>
                        <divb3>
                            <a href="#" alt="Generar orden de compra" class="tooltip">  
                                  <asp:ImageButton ID="btn_gnerar_oc" runat="server" ImageUrl="/img/oc.png" CssClass="boton" BackColor="Violet" ValidationGroup="inicio" /> 
                            </a>
                       </divb3>
                        <divb3>
                            <a href="#" alt="Cancelar factura" class="tooltip">  
                                  <asp:ImageButton ID="btn_cancelarFactura" runat="server" ImageUrl="/img/off.png" CssClass="boton" BackColor="red" ValidationGroup="inicio" visible="false"/> 
                            </a>
                       </divb3>

                    </article>
                    <article id="tabla" style="text-align: left;">
                        <div id="Layer1" style="width: 100%; min-height: 200px; overflow: scroll;">
                            <asp:GridView ID="Grid_facturas" runat="server" CssClass="Grid"
                                PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center"
                                AlternatingRowStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="Beige"
                                OnRowCommand="Grid_facturas_RowCommand" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Font-Bold="True" ForeColor="White" Height="50px" BackColor="#81BEF7" />
                                <Columns>
                                    <asp:ButtonField ButtonType="Image" Visible="false" CommandName="conceptos" ImageUrl="/img/open.png" ControlStyle-Width="30px" HeaderText="Conceptos">
                                        <ControlStyle Width="20px" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" Visible="false" CommandName="retencion" HeaderText="Retencion" ImageUrl="/img/open.png">
                                        <ControlStyle Width="20px" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" Visible="false" CommandName="traslados" HeaderText="Traslados" ImageUrl="/img/open.png" FooterText="Selec">
                                        <ControlStyle Width="20px" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" CommandName="xml" HeaderText="XML" ImageUrl="/img/xml2.png">
                                        <ControlStyle Width="20px" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" CommandName="pdf" HeaderText="PDF" ImageUrl="/img/pdf.png">
                                        <ControlStyle Width="20px" />
                                    </asp:ButtonField>
                                    <asp:TemplateField FooterText="Seleccionar" HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="check" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField FooterText="Generar OC" HeaderText="Generar OC">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Check_oc" runat="server" CssClass="check" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#81BEF7" BorderStyle="None" HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </article>
                    <asp:GridView ID="Grid_facturas2" runat="server" AlternatingRowStyle-BackColor="#BCC98E"
                        CssClass="gridviewmediaforms" EditRowStyle-BackColor="#EDE059" EmptyDataRowStyle-BackColor="Red"
                        HeaderStyle-BackColor="Beige" HorizontalAlign="Center" OnRowCommand="Grid_facturas_RowCommand"
                        PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom" PagerStyle-BackColor="#81BEF7"
                        PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center" Visible="false">
                        <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="4" Position="Top" PreviousPageText="Previus" />
                        <HeaderStyle BackColor="#81BEF7" Font-Bold="True" Font-Size="12" ForeColor="White" />
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="mostrar" ControlStyle-ForeColor="Black" Text="concepto" />
                        </Columns>
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="mostrar" ControlStyle-ForeColor="Black" Text="concepto" />
                        </Columns>
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="mostrarRetencion" ControlStyle-ForeColor="Black" Text="Retencion" />
                        </Columns>
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="mostrarArchivos" ControlStyle-ForeColor="Black" Text="Archivos" />
                        </Columns>
                    </asp:GridView>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- MPE Arcivos --%>
        <asp:Panel runat="server" ID="pnl_detalles" BackColor="White" CssClass="ventana">
            <div class="titulo" style="background-color: #3EC7F3; height: 45px; width: 100%">
                <h2 style="color: white">Archivos</h2>
            </div>
            <br />
            <br />
            <br />
            <table style="width: 100%; text-align: left">
                <tr>
                    <td></td>
                    <td>
                        <asp:GridView ID="Grid_Archivos" runat="server"
                            Width="100% "
                            Font-Size="12px">
                            <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="mostrar" Text="Abrir" ControlStyle-ForeColor="Black" ControlStyle-Width="100px" ControlStyle-CssClass="botonGrid2" />
                            </Columns>
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="descargar" Text="Descargar" ControlStyle-ForeColor="Black" ControlStyle-Width="100px" ControlStyle-CssClass="botonGrid2" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="Grid_Archivos2" runat="server"
                            Width="100% "
                            Font-Size="12px"
                            Visible="false">
                            <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="mostrar" Text="Abrir" ControlStyle-ForeColor="Black" ControlStyle-Width="100px" ControlStyle-CssClass="botonGrid2" />
                            </Columns>
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="descargar" Text="Descargar" ControlStyle-ForeColor="Black" ControlStyle-Width="100px" ControlStyle-CssClass="botonGrid2" />
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                        <div>
                            <asp:Button runat="server" ID="btn_cierrate" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="MPE" runat="server"
            TargetControlID="btn_cierrate"
            PopupControlID="pnl_detalles"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="btn_cierrate">
        </asp:ModalPopupExtender>
        </ContentTemplate>
        <%-- MPE conceptos --%>
        <asp:ModalPopupExtender ID="MPE_conceptos" runat="server"
            TargetControlID="btn_cierrateconceptos"
            PopupControlID="panel_conceptos"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="btn_cierrateconceptos">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel_conceptos" BackColor="White" CssClass="ventana">
            <div class="titulo" style="background-color: #3EC7F3; height: 45px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="tituloCR" runat="server" Text="Conceptos"></asp:Label>
                </h2>
            </div>
            <br />
            <br />
            <br />
            <table style="width: 95%; text-align: left; margin-left: 15px;">
                <tr>
                    <td></td>
                    <td>
                        <asp:UpdatePanel ID="aaa" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Grid_conceptos" runat="server"
                                    AllowPaging="true" PageSize="10"
                                    CssClass="gridviewmediaforms"
                                    EmptyDataRowStyle-BackColor="Red" EditRowStyle-BackColor="#EDE059"
                                    PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                    PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center" HorizontalAlign="Center"
                                    AlternatingRowStyle-BackColor="#BCC98E" HeaderStyle-BackColor="Beige" PagerStyle-BackColor="#81BEF7">
                                    <PagerSettings PageButtonCount="4" FirstPageText="First" LastPageText="Last" Position="Top" PreviousPageText="Previus" />
                                    <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" Font-Size="12" />
                                </asp:GridView>
                                <asp:GridView ID="Grid_Reten" runat="server"
                                    AllowPaging="true" PageSize="10"
                                    CssClass="gridviewmediaforms"
                                    EmptyDataRowStyle-BackColor="Red" EditRowStyle-BackColor="#EDE059"
                                    PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                    PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center" HorizontalAlign="Center"
                                    AlternatingRowStyle-BackColor="#BCC98E" HeaderStyle-BackColor="Beige" PagerStyle-BackColor="#81BEF7">
                                    <PagerSettings PageButtonCount="4" FirstPageText="First" LastPageText="Last" Position="Top" PreviousPageText="Previus" />
                                    <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" Font-Size="12" />
                                </asp:GridView>
                                <asp:GridView ID="Grid_Tras" runat="server"
                                    AllowPaging="true" PageSize="10"
                                    CssClass="gridviewmediaforms"
                                    EmptyDataRowStyle-BackColor="Red" EditRowStyle-BackColor="#EDE059"
                                    PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                    PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center" HorizontalAlign="Center"
                                    AlternatingRowStyle-BackColor="#BCC98E" HeaderStyle-BackColor="Beige" PagerStyle-BackColor="#81BEF7">
                                    <PagerSettings PageButtonCount="4" FirstPageText="First" LastPageText="Last" Position="Top" PreviousPageText="Previus" />
                                    <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" Font-Size="12" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <div>
                            <asp:Button runat="server" ID="btn_cierrateconceptos" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <%-- MPE subir pdf --%>
        <asp:ModalPopupExtender ID="MPE_subirPDF" runat="server"
            TargetControlID="btn_cierraPDF"
            PopupControlID="panel_pdf"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="btn_cierraPDF">
        </asp:ModalPopupExtender>

        <asp:Panel runat="server" ID="panel_pdf" BackColor="White" CssClass="ventana">
            <div class="titulo" style="background-color: #3EC7F3; height: 45px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label13" runat="server" Text="PDF"></asp:Label>
                </h2>
            </div>
            <br />
            <br />
            <br />
            <center>
                <asp:Accordion ID="Accordion1" runat="server"
                                HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" Width="97%">
                                <Panes>
                                    <asp:AccordionPane ID="pane1" runat="server">
                                        <Header>PDF en correo</Header>
                                        <Content>
                                            <asp:Panel runat="server" ID="seleccionar">
                 <table style="width: 95%; text-align: left; margin-left: 15px;">
                     <tr>
                         <td>
                         </td>
                         <td>
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="Grid_pdfEnCorreo" runat="server"
                                    AllowPaging="true" PageSize="10"
                                    CssClass="gridviewmediaforms"
                                    EmptyDataRowStyle-BackColor="Red" EditRowStyle-BackColor="#EDE059"
                                    PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                    PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center" HorizontalAlign="Center"
                                    AlternatingRowStyle-BackColor="#BCC98E" HeaderStyle-BackColor="Beige" PagerStyle-BackColor="#81BEF7">
                                    <PagerSettings PageButtonCount="4" FirstPageText="First" LastPageText="Last" Position="Top" PreviousPageText="Previus" />
                                    <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" Font-Size="12" />
                                <Columns>
                                      <asp:ButtonField  ButtonType="Image"  CommandName="Actualizar" ImageUrl="/img/Actualizar.png"  ControlStyle-Width="30px"  HeaderText="Actualizar"></asp:ButtonField>
                                   </Columns>                                 
                                    </asp:GridView>
                                    <asp:GridView ID="Grid_pdfEnCorreo2" runat="server"
                                    AllowPaging="true" PageSize="10"
                                    CssClass="gridviewmediaforms"
                                    EmptyDataRowStyle-BackColor="Red" EditRowStyle-BackColor="#EDE059"
                                    PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                    PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center" HorizontalAlign="Center"
                                    AlternatingRowStyle-BackColor="#BCC98E" HeaderStyle-BackColor="Beige" PagerStyle-BackColor="#81BEF7"
                                        Visible="false">
                                    <PagerSettings PageButtonCount="4" FirstPageText="First" LastPageText="Last" Position="Top" PreviousPageText="Previus" />
                                    <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" Font-Size="12"                   />
                                </asp:GridView>
                                </ContentTemplate>
                                </asp:UpdatePanel>                        
                         </td>
                     </tr>
                     </table>
            </asp:Panel>
                                         </Content>
                                        </asp:AccordionPane>
                                </Panes>
                                 <Panes>
                                    <asp:AccordionPane ID="AccordionPane1" runat="server">
                                        <Header>Subir pdf</Header>
                                        <Content>
                                            <asp:Panel runat="server" ID="subir">
                <table style="width: 95%; text-align: left; margin-left: 15px;">
                <tr>
                    <td></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>       
                             <br />         
                                <asp:AsyncFileUpload  OnClientUploadError="uploadError" OnClientUploadComplete="uploadComplete"
                                    runat="server" ID="AsyncFileUpload1" Width="400px" UploaderStyle="Modern" CompleteBackColor="White"
                                    UploadingBackColor="#CCFFFF" ThrobberID="imgLoader" OnUploadedComplete="AsyncFileUpload1_UploadedComplete" />              
                                <br />
                             </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>    
                        <br />
                    </td>
                </tr>
            </table>
            </asp:Panel>
                                         </Content>
                                        </asp:AccordionPane>
                                </Panes>
                </asp:Accordion>

                </center>
            <div>
                <asp:Button runat="server" ID="btn_cierraPDF" Text="X" class="btn_cierrate" />
            </div>
        </asp:Panel>

        <%-- MPE Descargar --%>
        <asp:ModalPopupExtender ID="MPEDescargas" runat="server"
            TargetControlID="btn_cerrarDescarga"
            PopupControlID="panel_Descargas"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="btn_cerrarDescarga">
        </asp:ModalPopupExtender>

        <asp:Panel runat="server" ID="panel_Descargas" BackColor="White" CssClass="ventana" Width="700px">
            <div class="titulo" style="background-color: #2e8bcc; height: 45px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label14" runat="server" Text="Descargas"></asp:Label>
                </h2>
            </div>
            <br />
            <br />
            <br />
            <table style="width: 95%; text-align: left; margin-left: 15px;">
                <tr>
                    <td></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panelnom" runat="server" GroupingText="Nombre de los archivos">
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox ID="che_nom" runat="server" Text="Factura" AutoPostBack="True" />
                                                <asp:CheckBox ID="che_folio" runat="server" Text="Folio" AutoPostBack="True" />
                                                <asp:CheckBox ID="che_Fecha" runat="server" Text="Fecha" AutoPostBack="True" />
                                                <asp:CheckBox ID="che_RFC_provee" runat="server" Text="RFC Proveedor" AutoPostBack="True" />
                                                <asp:CheckBox ID="che_nom_provee" runat="server" Text="Nombre Proveedor" AutoPostBack="True" />
                                            </td>
                                            <tr>
                                                <td style="vertical-align: top">
                                                    <asp:TextBox ID="txt_NombreArchivos2" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txt_NombreArchivos" runat="server" Width="100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <a href="#" alt="Limpiar Nombre del los archivos" class="tooltipDemo">
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="/img/limpiar.png" CssClass="boton" />
                                                    </a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView1" runat="server" Visible="false"></asp:GridView>
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="panel1" runat="server" GroupingText="zip">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="che_xml" runat="server" Text="XML" Checked="true" />
                                                <asp:CheckBox ID="che_pdf" runat="server" Text="PDF" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="che_carpetas" runat="server" Text="Ordenar por carpetas" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="che_carpetas_clientes" runat="server" Text="Ordenar por carpetas de clientes" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">
                                                <asp:Label ID="Label15" runat="server" Text="Nombre:"></asp:Label>
                                                <asp:TextBox ID="txt_NombreZip" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="width: 200px">
                                                <a href="#" alt="Descargar Zip" class="tooltipDemo">
                                                    <asp:Button ID="btn_cerarZip" runat="server" Text="Descargar" CssClass="botonZip" />
                                                </a>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <div>
                            <asp:Button runat="server" ID="btn_cerrarDescarga" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="MPE_reporte" runat="server"
            TargetControlID="btn_cerrarReporte"
            PopupControlID="panel_Reporte"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="btn_cerrarReporte">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel_Reporte" BackColor="White" CssClass="ventana" Width="80%">
            <div class="titulo" style="background-color: #2e8bcc; height: 45px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label16" runat="server" Text="Reporte"></asp:Label>
                </h2>
            </div>
            <br />
            <br />
            <br />
            <table style="width: 95%; text-align: left; margin-left: 15px;">
                <tr>
                    <td></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="Chec_seleccionarColumnas" runat="server" AutoPostBack="true" Text="Seleccionar Todo" />
                                <asp:Panel ID="panel3" runat="server" GroupingText="Columnas">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="Chec_version" runat="server" AutoPostBack="true" Text="Versión" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_serie" runat="server" AutoPostBack="true" Text="Serie" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_folio" runat="server" AutoPostBack="true" Text="Folio" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_fecha" runat="server" AutoPostBack="true" Text="Fecha" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Check_sello" runat="server" AutoPostBack="true" Text="Sello" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_formaDePago" runat="server" AutoPostBack="true" Text="Forma de pago" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="Chec_noCertificado" runat="server" AutoPostBack="true" Text="N. certificado" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_Certificado" runat="server" AutoPostBack="true" Text="Certificado" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_Condicionespago" runat="server" AutoPostBack="true" Text="Condiciones de pago" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_Descuento" runat="server" AutoPostBack="true" Text="Descuento" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_Motivodescuento" runat="server" AutoPostBack="true" Text="Motivo de descuento" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_Tipocambio" runat="server" AutoPostBack="true" Text="Tipo de cambio" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="Chec_Moneda" runat="server" AutoPostBack="true" Text="Moneda" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_Tipocomprobante" runat="server" AutoPostBack="true" Text="Tipo de comprobante" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_Metodopago" runat="server" AutoPostBack="true" Text="Método de pago" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_Lugarexpedicion" runat="server" AutoPostBack="true" Text="Lugar de expedición" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Chec_Nuctapago" runat="server" AutoPostBack="true" Text="Nu cta pago" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chec_proveedor_rfc" runat="server" AutoPostBack="true" Text="Proveedor RFC" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chec_proveedor_nombre" runat="server" AutoPostBack="true" Text="Proveedor Nombre" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:TextBox ID="txt_ColumnasSel" runat="server" Visible="false"></asp:TextBox>
                                </asp:Panel>
                                <asp:Panel ID="panel2" runat="server" GroupingText="Mostrar">
                                    <asp:CheckBox ID="Chec_concepto" runat="server" AutoPostBack="true" Text=" Mostrar Concepto" />
                                    <asp:CheckBox ID="Chec_Traslado" runat="server" AutoPostBack="true" Text="Mostar Traslado" />
                                    <asp:CheckBox ID="Chec_Retencio" runat="server" AutoPostBack="true" Text="Mostrar Retención" />
                                </asp:Panel>
                                <asp:Panel ID="panel4" runat="server" GroupingText="Totales">
                                    <asp:CheckBox ID="Chec_total_indivi" runat="server" AutoPostBack="true" Text="Total individual" />
                                    <asp:CheckBox ID="Chec_total_total" runat="server" AutoPostBack="true" Text="Total" />
                                </asp:Panel>
                                <br />
                                <a href="#" alt="Crear reporte" class="tooltipDemo">
                                    <asp:Button ID="btn_repoeteexel" runat="server" Text="Exportar" CssClass="botonExcel" />
                                </a>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <div class="close-button2 ">
                            <asp:Button runat="server" ID="btn_cerrarReporte" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%-- confirmacion --%>
        <asp:ModalPopupExtender ID="MPE_confir" runat="server"
            TargetControlID="Button4"
            PopupControlID="panel_confir"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="Button4">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel_confir" BackColor="White" CssClass="ventana">
            <div class="titulo" style="background-color: #2e8bcc; height: 30px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label19" runat="server" Text="Eliminar"></asp:Label>
                </h2>
            </div>
            <br />
            <br />
            <br />
            <table style="width: 95%; text-align: left; margin-left: 15px;">
                <tr>
                    <td></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panel5" runat="server">
                                    <center>
                                         ¿Está seguro que desea eliminar las facturas?  
                                       <br />
                                       <br />
                                          <asp:ImageButton ID="btn_eliminarFacturas2" runat="server" ImageUrl="/img/Guardar.png" CssClass="boton"     />
                                          <asp:ImageButton ID="btn_cerrarConfir" runat="server" ImageUrl="/img/Eliminar.png" CssClass="boton"     />
                                   </center>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <div>
                            <asp:Button runat="server" ID="Button4" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>



        <%-- confirmacion oc --%>
        <asp:ModalPopupExtender ID="MPE_cancelar_oc" runat="server"
            TargetControlID="btn_salir_cancelaroc"
            PopupControlID="panel_cancelar_oc"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="btn_salir_cancelaroc">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel_cancelar_oc" BackColor="White" CssClass="ventana">
            <div class="titulo" style="background-color: #2e8bcc; height: 30px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label23" runat="server" Text="Eliminar"></asp:Label>
                </h2>
            </div>
            <br />
            <br />
            <br />
            <table style="width: 95%; text-align: left; margin-left: 15px;">
                <tr>
                    <td></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panel8" runat="server">
                                    <center>
                                         ¿Está seguro que desea cancelar las facturas?  
                                       <br />
                                       <br />
                                          <asp:ImageButton ID="btn_oc_cancelar" runat="server" ImageUrl="/img/Guardar.png" CssClass="boton"     />
                                         <asp:ImageButton ID="btn_oc_salir" runat="server" ImageUrl="/img/Eliminar.png" CssClass="boton"     />
                                   </center>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <div>
                            <asp:Button runat="server" ID="btn_salir_cancelaroc" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>






        <%-- OC --%>


        <asp:ModalPopupExtender ID="MPE_OC" runat="server"
            TargetControlID="btn_cerrar_oc"
            PopupControlID="panel_oc"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="btn_cerrar_oc">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel_oc" BackColor="White" CssClass="ventana">
            <div class="titulo" style="background-color: #2e8bcc; height: 30px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label22" runat="server" Text="Orden de Compra"></asp:Label>
                </h2>
            </div>
            <br />
            <br />
            <br />
            <table style="width: 95%; text-align: left; margin-left: 15px;">
                <tr>
                    <td></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panel7" runat="server" CssClass="alinear">
                                    <center>

              

                          
                                    <iframe src="AceptarOC.aspx" style="display:inline-block; width: 591px;  " ></iframe>
                                     
                                       <div style="display:inline-block;  height:100px; margin-bottom:15px;" >
                                             <asp:TextBox ID="oc_selecc" runat="server" class="TextBox" style="width:100px " visible="false" ></asp:TextBox>
                                              <asp:ImageButton ID="btn_acep_oc" runat="server" ImageUrl="/img/Guardar.png" CssClass="boton"     />
                                              <asp:ImageButton ID="btn_cancel_oc" runat="server" ImageUrl="/img/Eliminar.png" CssClass="boton"     />
                         
                                       </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <div>
                            <asp:Button runat="server" ID="btn_cerrar_oc" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>







        <asp:TextBox ID="txt_idCRT" runat="server" Visible="false"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="btn_invi" Width="0px" Height="0px" BorderColor="White" BorderStyle="None" BackColor="White"></asp:Button>
    </form>
</body>
</html>
