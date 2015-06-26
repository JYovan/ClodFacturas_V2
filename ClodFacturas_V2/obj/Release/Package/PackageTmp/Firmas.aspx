<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Firmas.aspx.vb" Inherits="ClodFacturas_V2.Firmas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/estilosFirmas.css" />
    <%-- Mensajes --%>
    <link href="~/css/Mensajes.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Mensajes.min.js"></script>
    <script type="text/javascript" src="js/jquery.Mensajes_minimized.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <section>
            <br />
            <asp:Panel ID="f1" runat="server" GroupingText="Firma 1"  CssClass="panel_firmas">
               
          <table>
            <tr>
                <td  >           
                </td>
                <td >
                    <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_nombre_1" runat="server" Width="150px" CssClass="TextBox" ></asp:TextBox>
                </td>
                <td >
                    <asp:Label ID="Label4" runat="server" Text="Apellido paterno:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_app_1" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td >
                    <asp:Label ID="Label5" runat="server" Text="Apellido materno:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_apm_1" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td >
                    <asp:Label ID="Label10" runat="server" Text="Departamento:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_departamento_1" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                </tr>
            </table>
                    <br />           
            </asp:Panel>
            <asp:Panel ID="Panel1" runat="server" GroupingText="Firma 2" CssClass="panel_firmas">   
                 <table>
                     <tr>
                <td  >
                </td>
                <td >
                    <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_nombre_2" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td >
                    <asp:Label ID="Label3" runat="server" Text="Apellido paterno:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_app_2" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td >
                    <asp:Label ID="Label6" runat="server" Text="Apellido materno:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_apm_2" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                         <td >
                    <asp:Label ID="Label11" runat="server" Text="Departamento:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_departamento_2" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>

                </tr>
                </table>
                <br />  
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" GroupingText="Firma 2"  CssClass="panel_firmas">                         
                <table>
                      <tr>
                <td  >
                </td>
                <td >
                    <asp:Label ID="Label7" runat="server" Text="Nombre:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_nombre_3" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td >
                    <asp:Label ID="Label8" runat="server" Text="Apellido paterno:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_app_3" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td >
                    <asp:Label ID="Label9" runat="server" Text="Apellido materno:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_apm_3" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td >
                    <asp:Label ID="Label12" runat="server" Text="Departamento:"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_departamento_3" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                </tr>    
                </table>
                <br />         
            </asp:Panel>
            <asp:ImageButton ID="btn_inicio" runat="server" ImageUrl="/img/save2.png" CssClass="boton" BackColor="Green" ValidationGroup="inicio" />
        </section>
    </form>
</body>
</html>
