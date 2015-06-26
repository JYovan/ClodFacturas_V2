<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OrdenesdeCompra.aspx.vb" Inherits="ClodFacturas_V2.OrdenesdeCompra" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/estilosOC.css" />
    <%-- Mensajes --%>
    <link href="~/css/Mensajes.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Mensajes.min.js"></script>
    <script type="text/javascript" src="js/jquery.Mensajes_minimized.js"></script>

    <%-- Descargas --%>
    <script type="text/javascript" src="js/download.js"></script>
    <script src="js/modernizr-1.5.min.js"></script>
    <link href="~/css/iframe.css" rel="stylesheet" type="text/css" />
    <link href="~/css/jquery.jgrowl.css" rel="stylesheet" type="text/css" />

    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 35px;
        }

        .auto-style2
        {
            width: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 30px">
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
                    <br />
                    <asp:Panel ID="panel_filtros" runat="server" CssClass="panel" DefaultButton="btn_buscar">
                        <asp:Button ID="btn_buscar" runat="server" BackColor="White" BorderStyle="None" CssClass="botoninv" Height="0" Text="Button" Width="0" />
                        <table>
                            <tr>
                                <td></td>
                                <td>Fecha estimada de pago:
                                </td>
                                <td style="text-align: right;" class="auto-style1">De:</td>
                                <td>
                                    <asp:TextBox ID="txt_fecha_soli_del" runat="server" class="TextBox" Style="width: 100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="externder" runat="server"
                                        __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_fecha_soli_del"></asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                        ControlToValidate="txt_fecha_soli_del" ErrorMessage="Mal"
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" CssClass="bubble">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td></td>
                                <td class="auto-style2"></td>
                                <td>Fecha de entrega:
                                </td>
                                <td style="text-align: right;" class="auto-style1">De:</td>
                                <td>
                                    <asp:TextBox ID="txt_fecha_entrega_del" runat="server" class="TextBox" Style="width: 100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                        __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_fecha_entrega_del"></asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txt_fecha_entrega_del" ErrorMessage="Mal"
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" CssClass="bubble">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td style="text-align: right" class="auto-style1">Al:</td>
                                <td>
                                    <asp:TextBox ID="txt_fecha_soli_al" runat="server" class="TextBox" Style="width: 100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                        __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_fecha_soli_al"></asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="txt_fecha_soli_al" ErrorMessage="Mal"
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" CssClass="bubble">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td></td>
                                <td class="auto-style2"></td>
                                <td></td>
                                <td style="text-align: right;" class="auto-style1">Al:</td>
                                <td>
                                    <asp:TextBox ID="txt_fecha_entrega_al" runat="server" class="TextBox" Style="width: 100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server"
                                        __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_fecha_entrega_al"></asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        ControlToValidate="txt_fecha_entrega_al" ErrorMessage="Mal"
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" CssClass="bubble">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">Proveedor:
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txt_proveedor" runat="server" class="TextBox" Style="width: 200px"></asp:TextBox>
                                </td>
                                <td class="auto-style2"></td>

                                <td colspan="2">Proveedor RFC:
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txt_proveedorRFC" runat="server" class="TextBox" Style="width: 150px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
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
                        </table>
                    </asp:Panel>
                    <br />
                    <asp:GridView ID="Grid_OC" runat="server" CssClass="Grid"
                        PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                        PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center"
                        AlternatingRowStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="Beige"
                        OnRowCommand="Grid_oc_RowCommand" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle Font-Bold="True" ForeColor="White" Height="50px" BackColor="#81BEF7" />
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="DESCARGAR" ImageUrl="/img/savezip.png" ControlStyle-Width="30px" HeaderText="DESCARGAR">
                                <ControlStyle Width="30px" />
                            </asp:ButtonField>
                        </Columns>
                        <PagerStyle BackColor="#81BEF7" BorderStyle="None" HorizontalAlign="Center" />
                    </asp:GridView>
                   <asp:GridView ID="Grid_OC_2" runat="server" Visible="false"></asp:GridView>
                    <br />
               </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
