<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Correos.aspx.vb" Inherits="ClodFacturas_V2.Correos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link rel="stylesheet" href="css/EstilosCorreosV2.css" />
    <link rel="stylesheet" href="css/Estilos.css" />
    <%-- Mensajes --%>
    <link href="~/css/Mensajes.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Mensajes.min.js"></script>
    <script type="text/javascript" src="js/jquery.Mensajes_minimized.js"></script>
    <script type="text/javascript" src="js/download.js"></script>

    <script type="text/javascript">
        function Cargar() {

            parent.location.reload();
        }

        function Forzar() {
            __doPostBack('', '');
        }
    </script>

</head>
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
                                     <asp:Label ID="Label7" runat="server" Text="Proveedor:"></asp:Label>
                                </div1>
                                <div2>
                                    <asp:TextBox ID="txt_CorreProvvedor" runat="server"  class="TextBox" AutoPostBack="false"></asp:TextBox>
                                </div2> 
                            </div3>
                            <div3>
                                 <div1>
                                     <asp:Label ID="Label1" runat="server" Text="Correo:"></asp:Label>
                                </div1>
                                <div2>
                                    <asp:TextBox ID="txt_correoProveedor" runat="server"  class="TextBox"></asp:TextBox>
                                </div2> 
                            </div3>
                            <div3>
                                 <div1>
                                    <asp:Label ID="Label2" runat="server" Text="Motivo:"></asp:Label>
                                </div1>
                                <div2>
                                    <asp:TextBox ID="txt_CorreMotivo" runat="server"  class="TextBox"></asp:TextBox>
                                </div2> 
                            </div3>
                            <br />
                            <div3>
                                <div1>
                                    <asp:Label ID="label10" runat="server" Text="De:"></asp:Label>                        
                                </div1>
                                <div2>
                                    <asp:TextBox ID="txt_CorreFechaDe" runat="server" class="TextBox" style="width:100px"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtFechaDel_CalendarExtender" runat="server"
                                                    __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_CorreFechaDe">
                                                </asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                    ControlToValidate="txt_CorreFechaDe" ErrorMessage="Mal"
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
                                    <asp:TextBox ID="txt_CorreFechaAl" runat="server" class="TextBox"  style="width:100px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_CorreFechaAl">
                                                </asp:CalendarExtender>
                                </div2>
                            </div3>
                            <br />
                            <br />

                            <asp:Panel ID="panelfil" runat="server" GroupingText="Ordenar" Width="550px">
                                <table>
                                    <tr>
                                        <td>Ordenar por:
                                        </td>
                                        <td style="width: 2px"></td>
                                        <td style="width: 140px">
                                            <asp:RadioButton ID="Radio_por_proveedor" runat="server" GroupName="por" Text="Proveedor" AutoPostBack="true" />
                                        </td>
                                        <td style="width: 120px">
                                            <asp:RadioButton ID="Radio_por_motivo" runat="server" GroupName="por" Text="Motivo" AutoPostBack="true" />
                                        </td>
                                        <td style="width: 120px">
                                            <asp:RadioButton ID="Radio_por_fecha" runat="server" GroupName="por" Text="Fecha" Checked="true" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Mostrar:
                                        </td>
                                        <td style="width: 2px"></td>
                                        <td>
                                            <asp:RadioButton ID="Radio_mostrar_10" runat="server" GroupName="mostrar" Text="10" Checked="true" AutoPostBack="true" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="Radio_mostrar_50" runat="server" GroupName="mostrar" Text="100" AutoPostBack="true" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="Radio_mostrar_500" runat="server" GroupName="mostrar" Text="500" AutoPostBack="true" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="Radio_mostrar_todos" runat="server" GroupName="mostrar" Text="Todos" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Ordenar de:
                                        </td>
                                        <td style="width: 2px"></td>
                                        <td>
                                            <asp:RadioButton ID="Radio_orden_acs" runat="server" GroupName="orden" Text="Ascendente" Checked="true" AutoPostBack="true" />
                                        </td>

                                        <td>
                                            <asp:RadioButton ID="Radio_orden_desc" runat="server" GroupName="orden" Text="Descendente" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Mostar :
                                        </td>
                                        <td style="width: 2px"></td>
                                        <td>
                                            <asp:CheckBox ID="Chec_horas" runat="server" Text="H/min/seg" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="width: 8px"></td>
                                        <td>
                                            <asp:CheckBox ID="che_Stodos" runat="server" AutoPostBack="true" Text="Seleccionar todos" CssClass="abajo"></asp:CheckBox></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                        <br />
                    </article>
                    <article id="Botones2">
                        <div4>                  
                          <asp:Label ID="aaa" runat="server" Text="Resultados: "></asp:Label>  <asp:Label ID="txt_total" runat="server" Text="0"></asp:Label>                           
                        </div4>
                        <br />
                        <divb2>
                            <a href="#" alt="Crear reporte" class="tooltip">  
                                 <asp:ImageButton ID="btn_crearreporte" runat="server" ImageUrl="/img/reporte.png" CssClass="boton" BackColor="Green" ValidationGroup="inicio" />                         
                            </a>
                        </divb2>
                        <divb3>
                            <a href="#" alt="Eliminar facturas" class="tooltip">  
                                  <asp:ImageButton ID="btn_eliminarFacturas" runat="server" ImageUrl="/img/eliminar.png" CssClass="boton" BackColor="Green" ValidationGroup="inicio" /> 
                            </a>
                       </divb3>
                    </article>
                    <article id="tabla" style="text-align: left;">
                        <div id="Layer1" style="width: 100%; min-height: 200px; overflow: scroll;">
                            <asp:GridView ID="Grid_CorreosRecibidos" runat="server"
                                PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center"
                                AlternatingRowStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="Beige"
                                OnRowCommand="Grid_facturas_RowCommand" HeaderStyle-HorizontalAlign="Left" CssClass="Grid">
                                <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" Height="40px" />
                                <Columns>
                                    <asp:ButtonField ButtonType="Image" CommandName="mostrar" ImageUrl="/img/open.png" ControlStyle-Width="30px" HeaderText="CONTENIDO" ControlStyle-CssClass="ajustar2">
                                        <ControlStyle Width="20px" />
                                    </asp:ButtonField>
                                    <asp:TemplateField FooterText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#81BEF7" BorderStyle="None" HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </article>
                </section>
                <asp:GridView ID="Grid_CorreosRecibidos2" runat="server"
                    PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt"
                    Font-Size="11px"
                    autopostback="false" ControlStyle-ForeColor="Black"
                    Visible="false">
                    <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="mostrar" Text="Archivos" ControlStyle-ForeColor="Black" ControlStyle-CssClass="ajustar" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalPopupExtender ID="MPE" runat="server"
            TargetControlID="Button4"
            PopupControlID="panel_confir"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="Button4">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel_confir" BackColor="White" CssClass="ventana">
            <div class="titulo" style="background-color: #2e8bcc; height: 30px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label19" runat="server" Text="Contenido"></asp:Label>
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
                                    <table style="width: 100%; text-align: left">
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:GridView ID="Grid_Archivos" runat="server"
                                                    PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                                    PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center"
                                                    AlternatingRowStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="Beige"
                                                    OnRowCommand="Grid_facturas_RowCommand" HeaderStyle-HorizontalAlign="Left" CssClass="Grid">
                                                    <HeaderStyle BackColor="#81BEF7" Font-Bold="True" ForeColor="White" Height="40px" />
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="mostrar" ImageUrl="/img/open.png" ControlStyle-Width="30px" HeaderText="ABRIR" ControlStyle-CssClass="ajustar">
                                                            <ControlStyle Width="20px" />
                                                        </asp:ButtonField>
                                                    </Columns>
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="descargar" ImageUrl="/img/dow.png" ControlStyle-Width="30px" HeaderText="DESCARGAR" ControlStyle-CssClass="ajustar2">
                                                            <ControlStyle Width="40px" />
                                                        </asp:ButtonField>
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
                                                <div class="close-button2 ">
                                                    <asp:Button runat="server" ID="btn_cierrate" Text="X" class="btn_cierrate" />
                                                </div>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
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
            <asp:Button ID="btn_buscar" runat="server" Text="Button" CssClass="btn_invi" />
        </asp:Panel>

        <%-- confirmacion --%>
        <asp:ModalPopupExtender ID="MPE_confir" runat="server"
            TargetControlID="Button1"
            PopupControlID="panel_confir2"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="Button1">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel_confir2" BackColor="White" CssClass="ventana">
            <div class="titulo" style="background-color: #2e8bcc; height: 30px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label3" runat="server" Text="Eliminar"></asp:Label>
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
                                         ¿Está seguro que desea eliminar los correos?  
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
                            <asp:Button runat="server" ID="Button1" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
