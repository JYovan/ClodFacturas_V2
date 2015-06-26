<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Logeo.aspx.vb" Inherits="ClodFacturas_V2.Logeo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>Facturas</title>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <link rel="stylesheet" href="css/EstilosIndex.css" />
    <link href="~/css/jquery.jgrowl.css" rel="stylesheet" type="text/css" />
    <link href="~/css/iframe.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="logo" style="width:93%; margin-left:40px ; margin-top:25px" >
            <section>
                <img class="fade" src="/img/CloudFacturas.png" width="170" height="120" />
            </section>
            <section class="tile  tile-nuevousuario fig-tile"  data-page-name="usuarios" style="background-color:#088A08; color:white; height:25px; width:100px">
                Regístrate
            </section>
        </div>    
        <div class="page-content">
            <div class="usuarios">
                <div class="titulo" style="background-color: #2e8bcc; height: 45px; width: 100%">
                    <h2 class="page-title">Cuentas</h2>
                </div>
                <br />
                <br />
                <div class="demo-wrapper">
                    <iframe src="cuentas.aspx"></iframe>
                </div>
                <div class="close-button r-close-button">x</div>
            </div>
        </div>
        <asp:UpdatePanel ID="principal" runat="server">
            <ContentTemplate>
                <%-- Logeo --%>
                <div style="min-height: 80px;">
                </div>
                <table class="loginBody" style="text-align: left" >
                    <tr>
                        <td>
                            <asp:Label ID="Bienve" runat="server" Font-Size="23px">Bienvenido: </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top; margin-top:1.5px" ">
                            <p>
                                <img src="/img/user-login.png" width="130" height="130" /></p>
                            <div class="tiles">
                                <div>
                                    
                                </div>
                            </div>
                        </td>
                        <td style="vertical-align:bottom; border-color:red" >
                            <table>
                                <tr>
                                    <td class="loginBody" style="height: 100px">
                                        <asp:Panel ID="Panel1" runat="server" class="panel" DefaultButton="btn_inicio">
                                            <table style="text-align: left; width: 250px;" id="loginBox">
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:TextBox ID="keybox" runat="server" class="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="keybox"
                                                            ErrorMessage="*" ForeColor="Red" ValidationGroup="wasd" Font-Size="19pt"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:TextBox ID="pwbox" runat="server" TextMode="Password" class="form-control" Font-Size="12pt"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="pwbox"
                                                            ErrorMessage="*" ForeColor="Red" ValidationGroup="wasd" Font-Size="19pt"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="height: 40px; float: right; margin-right: 20px">
                                                        <asp:ImageButton ID="btn_inicio" runat="server" ImageUrl="/img/save.png" CssClass="boton" BackColor="Green" ValidationGroup="inicio" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 50px"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <%--  --%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--win8  --%>
        <script src="js/jquery-1.8.2.min.js"></script>
        <script src="js/scripts.js"></script>
        <%--  --%>
        <%-- Mensajes --%>
        <script type="text/javascript" src="jquery.min.js"></script>
        <script type="text/javascript" src="jquery.jgrowl_minimized.js"></script>
        <%--  --%>
    </form>
</body>
</html>