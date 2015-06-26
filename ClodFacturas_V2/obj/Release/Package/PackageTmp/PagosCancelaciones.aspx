<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PagosCancelaciones.aspx.vb" Inherits="ClodFacturas_V2.PagosCancelaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="css/EstilosAdministracion.css" />
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
                    <div2>
                        <asp:TextBox ID="txt_FechaAl" runat="server" class="TextBox"  style="width:100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                        __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_FechaAl">
                                    </asp:CalendarExtender>
                    </div2>
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
                                    <asp:ButtonField ButtonType="Image" CommandName="Restaurar" HeaderText="RESTAURAR" ImageUrl="/img/Actualizar.png">
                                        <ControlStyle Width="20px" />
                                    </asp:ButtonField>
                                </Columns>
                                <PagerStyle BackColor="#81BEF7" BorderStyle="None" HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </article>
                    <asp:GridView ID="Grid_facturas2" runat="server" AlternatingRowStyle-BackColor="#BCC98E"
                        CssClass="gridviewmediaforms" EditRowStyle-BackColor="#EDE059" EmptyDataRowStyle-BackColor="Red"
                        HeaderStyle-BackColor="Beige" HorizontalAlign="Center" OnRowCommand="Grid_facturas_RowCommand"
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
                    <asp:Label ID="Label19" runat="server" Text="Confirmar"></asp:Label>
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
                                        ¿Está  seguro que desea restaurar la factura cancelada?
                                        
                                       <br />
                                       <br />
                                          <asp:ImageButton ID="btn_restaurar" runat="server" ImageUrl="/img/Guardar.png" CssClass="boton"     />
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
        <%-- confirmacion --%>
        <asp:ModalPopupExtender ID="MPE_confirmar2" runat="server"
            TargetControlID="btn_cancelar2"
            PopupControlID="panel1"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="btn_cancelar2">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel1" BackColor="White" CssClass="ventana">
            <div class="titulo" style="background-color: #2e8bcc; height: 30px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label13" runat="server" Text="Confirmar"></asp:Label>
                </h2>
            </div>
            <br />
            <br />
            <br />
            <table style="width: 95%; text-align: left; margin-left: 15px;">
                <tr>
                    <td></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panel2" runat="server">

                                    <center>
                                        ¿Está seguro que desea cancelar la orden de compra?
                                       <br />
                                       <br />
                                          <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/img/Guardar.png" CssClass="boton"     />
                                          <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="/img/Eliminar.png" CssClass="boton"     />
                                   </center>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <div>
                            <asp:Button runat="server" ID="btn_cancelar2" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
         <asp:TextBox ID="txt_id" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txt_idCRT" runat="server" Visible="false"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="btn_invi" Width="0px" Height="0px" BorderColor="White" BorderStyle="None" BackColor="White"></asp:Button>
    </form>
</body>
</html>
