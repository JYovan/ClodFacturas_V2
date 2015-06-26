<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AceptarOC.aspx.vb" Inherits="ClodFacturas_V2.AceptarOC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="css/estolosPrincipal.css" />
    <%-- Mensajes --%>
    <link href="~/css/jquery.jgrowl.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="jquery.min.js"></script>
    <script type="text/javascript" src="jquery.jgrowl_minimized.js"></script>
    <%-- Descargas --%>
    <script type="text/javascript" src="js/download.js"></script>

    <script src="js/modernizr-1.5.min.js"></script>
    <link href="~/css/iframe.css" rel="stylesheet" type="text/css" />
    <link href="~/css/jquery.jgrowl.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .auto-style1
        {
            width: 340px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panel7" runat="server">
                        <center>
                           <table>
                               <tr>
                                   <td>
                                       <asp:TextBox ID="oc_selecc" runat="server" class="TextBox" style="width:100px " visible="false" ></asp:TextBox>
                                       Fecha estimada de entrega:
                                   </td>
                                   <td>
                                        <asp:TextBox ID="txt_fecha_oc" runat="server" class="TextBox" style="width:100px" AutoPostBack="true"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                                __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_fecha_oc">
                                            </asp:CalendarExtender>
                                   </td>
                                   <td>
                                   </td>
                               </tr>
                               <tr>
                                   <td>
                                          Fecha estimada de pago:
                                   </td>
                                   <td>
                                        <asp:TextBox ID="txt_fecha_pago" runat="server" class="TextBox" style="width:100px" AutoPostBack="true"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                                __designer:wfdid="w18" Format="dd/MM/yyyy" TargetControlID="txt_fecha_pago">
                                            </asp:CalendarExtender>
                                   </td>
                                   <td>
                                   </td>
                               </tr>
                               <tr>                        
                                   <td>
                                       Observaciones:
                                   </td>
                                   <td colspan="2">
                                       <asp:TextBox ID="txt_Observaciones" runat="server" class="TextBox" style="width:340px" AutoPostBack="true"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td>
                                       Representante:
                                   </td>
                                   <td colspan="2">
                                       <asp:TextBox ID="txt_Representante" runat="server" class="TextBox" style="width:100px" AutoPostBack="true"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td>
                                       Depto:
                                   </td>
                                   <td colspan="2">
                                       <asp:TextBox ID="txt_Depto" runat="server" class="TextBox" style="width:100px" AutoPostBack="true" Text="COMPRAS"></asp:TextBox>
                                   </td>
                               </tr>
                           </table>                               
                        </center>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
