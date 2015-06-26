<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Administracion.aspx.vb" Inherits="ClodFacturas_V2.Administracion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <link rel="stylesheet" href="css/EstilosAdministracion.css" />

     <%-- Mensajes --%>
    <link href="~/css/Mensajes.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Mensajes.min.js"></script>
    <script type="text/javascript" src="js/jquery.Mensajes_minimized.js"></script>

    <script type="text/javascript">
        function Cargar() {

            parent.location.reload();
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
    <div style="padding-left:30px ; padding-top:30px">

        <div>
            <asp:GridView ID="Grid_usuarios" runat="server" CssClass="Grid"
                                PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center"
                                AlternatingRowStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="Beige"
                                HeaderStyle-HorizontalAlign="Left" Width="620px"  >
                                <HeaderStyle  Font-Bold="True" ForeColor="White" Height="50px" BackColor="#81BEF7"  />
                                <Columns>
                                    <asp:ButtonField ButtonType="Image"  CommandName="Ver" ImageUrl="/img/open.png" ControlStyle-Width="30px" HeaderText="VER">
                                        <ControlStyle Width="20px" />
                                        </asp:ButtonField>
                                    <asp:TemplateField FooterText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server"  CssClass="check"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>                             
        </asp:GridView>
        </div> 
        <asp:GridView ID="Grid_Usuarios2" runat="server" Visible="false">
                                <Columns>
                                    <asp:ButtonField ButtonType="Image" Visible="false" CommandName="conceptos" ImageUrl="/img/open.png" ControlStyle-Width="30px" HeaderText="Conceptos">
                                        <ControlStyle Width="20px" />
                                        </asp:ButtonField>
                                </Columns>         
        </asp:GridView>
        <br />
         <asp:Panel ID="panelfil" runat="server" GroupingText="Permisos Módulos" Width="620px">
                  <table>
                      <tr>
                          <td></td>
                          <td><asp:CheckBox ID="Check_correos" runat="server" Text="Correos"/></td>
                          <td style="width:5px"></td>
                          <td><asp:CheckBox ID="Check_facturas" runat="server" Text="Facturas"/></td>
                          <td style="width:5px"></td>   
                          <td><asp:CheckBox ID="Check_cuentas" runat="server" Text="Cuentas"/></td>
                          <td style="width:5px"></td>
                          <td><asp:CheckBox ID="Check_subirF" runat="server" Text="Subir Facturas"/></td>
                          <td></td>                    
                      </tr>
                      <tr>
                          <td></td>
                          <td><asp:CheckBox ID="Check_Actilizar" runat="server" Text="Actualizar"/></td>
                          <td></td>
                          <td><asp:CheckBox ID="Check_administracion" runat="server" Text="Administración"/></td>
                          <td ></td>    
                          <td><asp:CheckBox ID="Check_PagosCancelaciones" runat="server" Text="Pagos/Cancelaciones"/></td>
                          <td></td>
                          <td><asp:CheckBox ID="Check_Firmas" runat="server" Text="Firmas OC"/></td>
                          <td ></td>                                                  
                      </tr>
                      <tr>
                          <td ></td> 
                          <td><asp:CheckBox ID="Check_OC" runat="server" Text="Ordenes de Compra"/></td>
                          <td ></td>  
                      </tr>
                  </table>
              </asp:Panel>
        <br />
            <asp:Panel ID="panel1" runat="server" GroupingText="Permisos" Width="620px">
                  <table>
                      <tr>
                          <td></td>
                          <td><asp:CheckBox ID="Check_alta" runat="server" Text="Dar de alta"/></td>
                          <td style="width:5px"></td>
                          <td><asp:CheckBox ID="Check_baja" runat="server" Text="Dar de baja"/></td>
                          <td style="width:5px"></td>   
                          <td><asp:CheckBox ID="Check_modificar" runat="server" Text="Modificar"/></td>                                     
                      </tr>
                  </table>
              </asp:Panel>
        <br />
        <asp:Panel ID="panel2" runat="server" GroupingText="Estado" Width="620px">
            <table>
                      <tr>
                          <td></td>
                            <td><asp:RadioButton ID="Radio_activo" runat="server" GroupName="estado" Text="Activo" AutoPostBack="true"/></td>
                          <td style="width:15px">&nbsp;</td>
                          <td><asp:RadioButton ID="Radio_inactivo" runat="server" GroupName="estado" Text="Inactivo" AutoPostBack="true"/></td>                                                                   
                      </tr>
                  </table>

        </asp:Panel>
        <asp:ImageButton ID="btn_cuenta" runat="server" ImageUrl="/img/save2.png" CssClass="boton"/>       
    </div>
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
                    <asp:Label ID="Label19" runat="server" Text="Cambios"></asp:Label>
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
                                         ¿Desea aplicar los cambios? 
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
    </form>
</body>
</html>
