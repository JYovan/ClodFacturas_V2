<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Cuentas.aspx.vb" Inherits="ClodFacturas_V2.Cuentas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Facturas</title>

    <script type="text/javascript" src="js/jquery.min.js"></script>
    <link href="~/css/jquery.jgrowl.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/EstilosCuentas.css" />

    <%-- Mensajes --%>
    <link href="~/css/Mensajes.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Mensajes.min.js"></script>
    <script type="text/javascript" src="js/jquery.Mensajes_minimized.js"></script>
</head>

<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ScriptManager>
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
                <asp:Accordion ID="Accordion1" runat="server"
                    HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" Width="97%" CssClass="borde">
                    <Panes>
                        <asp:AccordionPane ID="pane1" runat="server">
                            <Header>Cuenta</Header>
                            <Content>
                                <asp:Panel ID="Panel3" runat="server" class="panel" DefaultButton="btn_cuenta" Height="160px">
                                    <section id="contenido">
                                        <article id="filtros">
                                            <div3>
                                                    <div1>
                                                            <asp:Label ID="Label15" runat="server" Text="Cuenta:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_Cuenta" runat="server" Width="100%" class="form-control" Font-Size="12pt"></asp:TextBox>            
                                                    </div2>
                                                    <div4>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Cuenta"
                                                             CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                            <br />
                                            <div3>
                                                    <div1>
                                                            <asp:Label ID="Label1" runat="server" Text="Razón social:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_RazonSocial" runat="server" Width="100%" class="form-control" Font-Size="12pt"></asp:TextBox>                                                
                                                    </div2>
                                                    <div4>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txt_RazonSocial"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                            <br />
                                            <div3>
                                                    <div1>
                                                            <asp:Label ID="Label11" runat="server" Text="RFC:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_RFC" runat="server" Width="100%" class="form-control" Font-Size="12pt" ></asp:TextBox>                                                               
                                                    </div2>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                                runat="server"
                                                                ControlToValidate="txt_RFC"
                                                                CssClass="bubble"
                                                                ValidationGroup="guardar"
                                                                ValidationExpression="^([A-Z&Ññ]{3}|[A-Z][AEIOU][A-Z]{2})\d{2}((01|03|05|07|08|10|12)(0[1-9]|[12]\d|3[01])|02(0[1-9]|[12]\d)|(04|06|09|11)(0[1-9]|[12]\d|30))([A-Z0-9]{2}[0-9A])?$">
                                                                    Incorrecto
                                                            </asp:RegularExpressionValidator>
                                                    <div4>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txt_RFC"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                            <br />
                                            <div3>
                                                    <div1>
                                                            <asp:Label ID="Label12" runat="server" Text="CURP:" ></asp:Label>
                                                    </div1>
                                                    <div2>
                                                         <asp:TextBox ID="txt_CURP" runat="server" Width="100%" class="form-control" Font-Size="12pt" ></asp:TextBox>                                                       
                                                    </div2>
                                                    <div4>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                              runat="server"
                                                                ControlToValidate="txt_CURP"
                                                                CssClass="bubble"
                                                                ValidationGroup="guardar"
                                                                ValidationExpression="^[a-zA-Z]{4}\d{6}[a-zA-Z]{6}\d{2}$">                                           
                                                                 Incorrecto
                                                            </asp:RegularExpressionValidator>          
                                                    </div4>
                                                </div3>
                                            <div3>
                                                <div5>
                                                  <asp:ImageButton ID="btn_cuenta" runat="server" ImageUrl="/img/save2.png" CssClass="boton" BackColor="Green" ValidationGroup="guardar" />       

                                                </div5>
                                            </div3>
                                        </article>
                                    </section>
                                </asp:Panel>
                            </Content>
                        </asp:AccordionPane>
                        <asp:AccordionPane ID="pane2" runat="server">
                            <Header>Correos</Header>
                            <Content>
                                <asp:Panel ID="Panel1" runat="server" class="panel" DefaultButton="btn_correo">
                                    <div3>
                                                    <div1>
                                                            <asp:Label ID="Label2" runat="server" Text="Correo:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_Correo" runat="server" Width="100%" class="form-control" Font-Size="12pt" placeholder="ejemplo@gmail.com"></asp:TextBox>
                                                    </div2>
                                                    <div4>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                                runat="server"
                                                                ControlToValidate="txt_Correo"
                                                                SetFocusOnError="True"
                                                                CssClass="bubble"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                                Incorrecto
                                                            </asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                ControlToValidate="txt_Correo"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarCorreo"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                    <br />
                                    <div3>
                                                    <div1>
                                                            <asp:Label ID="Label9" runat="server" Text="Contraseña:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_contrasenia" runat="server" TextMode="Password" Width="100%" class="form-control" Font-Size="12pt"></asp:TextBox>
                                                    </div2>
                                                    <div4>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                                ControlToValidate="txt_contrasenia"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarCorreo"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                    <br />
                                    <div3>
                                                    <div1>
                                                            <asp:Label ID="Label3" runat="server" Text="Tipo configuración:"></asp:Label>
                                                    </div1>
                                                    <div7>
                                                            <asp:TextBox ID="txt_Tipo" runat="server" Width="100%" class="form-control" Font-Size="12pt" placeholder="Pop3" Text="Pop3"  ></asp:TextBox>
                                                    </div7>
                                                    <div8>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                ControlToValidate="txt_Tipo"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarCorreo"></asp:RequiredFieldValidator>
                                                    </div8>
                                                </div3>
                                    <br />
                                    <div3>
                                                    
                                                        <div1>
                                                                <asp:Label ID="Label4" runat="server" Text="Servidor:"></asp:Label>
                                                        </div1>
                                                        <div7>
                                                                <asp:TextBox ID="txt_Servidor" runat="server" Width="100%" class="form-control" Font-Size="12pt" placeholder="pop.gmail.com"></asp:TextBox>
                                                        </div7>
                                                        <div8>
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                    ControlToValidate="txt_Servidor"
                                                                    CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarCorreo"></asp:RequiredFieldValidator>
                                                        </div8>      
                                                </div3>
                                    <br />
                                    <div3>
                                                    
                                                        <div1>
                                                            <asp:Label ID="Label8" runat="server" Text="Puerto:"></asp:Label>

                                                        </div1>
                                                        <div7>
                                                            <asp:TextBox ID="txt_Puerto" runat="server" Width="100%" class="form-control" Font-Size="12pt" placeholder="995"></asp:TextBox>

                                                        </div7>
                                                        <div8>
                                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                ControlToValidate="txt_Puerto"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarCorreo"></asp:RequiredFieldValidator>
                                                             <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_Puerto" Type="Integer" Operator="DataTypeCheck"                                             ErrorMessage="Incorrecto" CssClass="bubble"  />
                                                        </div8>                           
                                                </div3>
                                    <div3>
                                                    <div1>
                                                        <asp:Label ID="Label17" runat="server" Text="SSL:"> </asp:Label>
                                                    </div1>
                                                        <div7>
                                                            <asp:RadioButton ID="Radio_ssl_si" runat="server" GroupName="ssl" Checked="true" Text="Si" />
                                                     <asp:RadioButton ID="Radio_ssl_no" runat="server" GroupName="ssl" Text="No" />
                                                        </div7>                                 
                                                 </div3>
                                    <div3>
                                                    <div5>
                                                        <table style="text-align: right; float: right;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btn_correo" runat="server" ImageUrl="/img/save2.png" CssClass="boton" BackColor="Green" ValidationGroup="guardarCorreo" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="btn_correo_modificar" runat="server" ImageUrl="/img/modificar.png" CssClass="boton" BackColor="#FAAC58" ValidationGroup="guardarCorreo" />
                                                                        </td>
                                                                        <td>
                                                                              <asp:ImageButton ID="btn_limpiar_correo" runat="server" ImageUrl="/img/limpiar.png" CssClass="boton"  />       
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                    </div5>
                                                    
                                                </div3>
                                    <br />
                                    <table style="width: 100%; text-align: left; border-spacing: 7px; color: black">
                                        <tr>
                                            <center>                                                                             
                                        <td style="width:100%;"  >
                                            <div style="  width:100%; margin:auto;">                                            
                                           <asp:GridView ID="Grid_correos" runat="server" CssClass="Grid"
                                                    PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                                    PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center"
                                                    AlternatingRowStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="Beige"
                                                   HeaderStyle-HorizontalAlign="Left"  >
                                                    <HeaderStyle  Font-Bold="True" ForeColor="White" Height="50px" BackColor="#81BEF7"  />
                                                    <Columns>
                                                    <asp:ButtonField  ButtonType="Image"  CommandName="probar" ImageUrl="/img/mailB.png"  ControlStyle-Width="30px"  HeaderText="PROBAR" >
                                                              <ControlStyle Width="30px" />
                                                    </asp:ButtonField>
                                                </Columns>
                                                <Columns  >
                                                  <asp:ButtonField  ButtonType="Image"  CommandName="modificar" ImageUrl="/img/edit.png"  ControlStyle-Width="30px"  HeaderText="EDITAR" >
                                                              <ControlStyle Width="30px" />
                                                    </asp:ButtonField>  
                                                    <asp:ButtonField  ButtonType="Image"  CommandName="Eliminar" ImageUrl="/img/delete.png"  ControlStyle-Width="30px"  HeaderText="ELIMINAR" >
                                                              <ControlStyle Width="30px" />
                                                    </asp:ButtonField>                                                                                                                
                                                </Columns>
                                            </asp:GridView>                                                                               
                                                <asp:GridView ID="grid_correos2" runat="server"
                                                Font-Size="12px"
                                                 Width="100%"
                                                 BorderColor="Black"
                                                 BorderStyle="Solid"
                                               Visible="false">                                             
                                                 <Columns  >
                                                    <asp:ButtonField CommandName="probar" Text="PROBAR" ControlStyle-ForeColor="Black" ItemStyle-Width="100" ControlStyle-CssClass="ajustar2" />
                                                </Columns>
                                                <Columns  >
                                                    <asp:ButtonField CommandName="modificar" Text="Modificar" ControlStyle-ForeColor="Black" ItemStyle-Width="100" ControlStyle-CssClass="ajustar2" />
                                                </Columns>
                                            </asp:GridView>
                                       </div>
                                      </td>
                                    </center>
                                        </tr>
                                    </table>
                                    <br />
                                </asp:Panel>
                            </Content>
                        </asp:AccordionPane>
                        <asp:AccordionPane ID="pane3" runat="server">
                            <Header>Usuarios</Header>
                            <Content>
                                <asp:Panel ID="Panel2" runat="server" DefaultButton="btn_usuario">
                                    <asp:TextBox runat="server" ID="cuentaModi" Visible="false"></asp:TextBox>
                                    <div3>
                                                    <div1>
                                                            <asp:Label ID="Label6" runat="server" Text="Usuario:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_Usuario" runat="server" Width="100%" class="form-control" Font-Size="12pt"></asp:TextBox>       
                                                    </div2>
                                                    <div4>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                ControlToValidate="txt_Usuario"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarUsuario"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                    <br />
                                    <div3>
                                                    <div1>
                                                        <asp:Label ID="Label7" runat="server" Text="Contraseña:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_Contrasena" runat="server" TextMode="Password" Width="100%" class="form-control" Font-Size="12pt"></asp:TextBox>   
                                                    </div2>
                                                    <div4>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                                ControlToValidate="txt_Contrasena"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarUsuario"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                    <br />
                                    <div3>
                                                    <div1>
                                                            <asp:Label ID="Label10" runat="server" Text="Nombre:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_nombre" runat="server" Width="100%" class="form-control" Font-Size="12pt"></asp:TextBox>                                   
                                                    </div2>
                                                    <div4>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                                ControlToValidate="txt_nombre"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarUsuario"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                    <br />
                                    <div3>
                                                    <div1>
                                                            <asp:Label ID="Label13" runat="server" Text="Apellido Paterno:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_app" runat="server" Width="100%" class="form-control" Font-Size="12pt"></asp:TextBox>                                                          
                                                    </div2>
                                                    <div4>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                                ControlToValidate="txt_app"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarUsuario"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                    <br />
                                    <div3>
                                                    <div1>
                                                            <asp:Label ID="txt_label" runat="server" Text="Apellido Materno:"></asp:Label>
                                                    </div1>
                                                    <div2>
                                                            <asp:TextBox ID="txt_apma" runat="server" Width="100%" class="form-control" Font-Size="12pt"></asp:TextBox>  
                                                    </div2>
                                                    <div4>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                                                ControlToValidate="txt_apma"
                                                                CssClass="bubble" ErrorMessage="Falta" ValidationGroup="guardarUsuario"></asp:RequiredFieldValidator>
                                                    </div4>
                                                </div3>
                                    <br />
                                    <div3>
                                                     <div5>
                                                         <table style="text-align: right; float: right;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btn_usuario" runat="server" ImageUrl="/img/save2.png" ValidationGroup="guardarUsuario" CssClass="boton" BackColor="Green" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="btn_usuario_modificar" runat="server" ImageUrl="/img/modificar.png" CssClass="boton" BackColor="#FAAC58" ValidationGroup="guardarUsuario" />
                                                                        </td>
                                                                          <td>
                                                                              <asp:ImageButton ID="btn_limpiar_usuario" runat="server" ImageUrl="/img/limpiar.png" CssClass="boton" />       
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                     </div5>
                                                 </div3>
                                    <table style="width: 100%; text-align: left; border-spacing: 7px; color: black">
                                        <tr>
                                            <center>                                                                             
                                                            <td style="width:100%;"  >
                                                                <div style="  width:100%; margin:auto;">  
                                                                    <asp:GridView ID="Grid_Usuarios"    runat="server" CssClass="Grid"
                                                                                    PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                                                                                    PagerStyle-BorderStyle="None" PagerStyle-HorizontalAlign="Center"
                                                                                    AlternatingRowStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="Beige"
                                                                                    HeaderStyle-HorizontalAlign="Left"  >
                                                                                    <HeaderStyle  Font-Bold="True" ForeColor="White" Height="50px" BackColor="#81BEF7"  />
                                                                                    <Columns>
                                                                        <asp:ButtonField  ButtonType="Image"  CommandName="modificar" ImageUrl="/img/edit.png"  ControlStyle-Width="30px"  HeaderText="EDITAR" >
                                                                              <ControlStyle Width="30px" />
                                                                         </asp:ButtonField>     
                                                                            <asp:ButtonField  ButtonType="Image"  CommandName="Eliminar" ImageUrl="/img/delete.png"  ControlStyle-Width="30px"  HeaderText="ELIMINAR">
                                                              <ControlStyle Width="30px" />
                                                    </asp:ButtonField>   
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="Grid_Usuarios2" runat="server"
                                                                        CssClass="mGrid"
                                                                        PagerStyle-CssClass="pgr"
                                                                        AlternatingRowStyle-CssClass="alt"
                                                                        Font-Size="10px" Width="100%" Visible="false">
                                                                        <Columns>
                                                                            <asp:ButtonField CommandName="modificar" Text="Modificar" ControlStyle-ForeColor="Black" ItemStyle-Width="100" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                           </center>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <br />
                                <br />
                            </Content>
                        </asp:AccordionPane>
                    </Panes>
                </asp:Accordion>
                <asp:Label ID="Label14" runat="server" Text="id" Visible="false"></asp:Label>
                <asp:TextBox ID="txt_id_correo" runat="server" Visible="false" class="form-control" Font-Size="12pt"></asp:TextBox>
                <asp:Label ID="Label16" runat="server" Text="id usuario:" Visible="false"></asp:Label>
                <asp:TextBox ID="txt_id_Usuario" runat="server" Visible="false" Width="100%" class="form-control" Font-Size="12pt"></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div3 style="visibility: hidden">
           <div1>
               <asp:Label ID="Label5" runat="server" Text="Host:" visible="false"></asp:Label>
           </div1>
           <div7>
                <asp:TextBox ID="txt_Host" runat="server" Width="100%" class="form-control" Font-Size="12pt" placeholder="gmail.com" visible="false"></asp:TextBox>
            </div7>
            <div8>
            </div8>
          </div3>
        <%-- confirmacion --%>
        <asp:ModalPopupExtender ID="MPE_confir" runat="server"
            TargetControlID="Button1"
            PopupControlID="panel_confir2"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="Button1">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel_confir2" BackColor="White" CssClass="ventana" Style="display: none">
            <div class="titulo" style="background-color: #2e8bcc; height: 30px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label18" runat="server" Text="Eliminar"></asp:Label>
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

                                <asp:Panel ID="panel4" runat="server">
                                    <center>
                                         ¿Está seguro que desea eliminar el usuario?  
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
        <%-- confirmacion --%>
        <asp:ModalPopupExtender ID="MPE_confir_correo" runat="server"
            TargetControlID="Button2"
            PopupControlID="panel5"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="Button2">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel5" BackColor="White" CssClass="ventana" Style="display: none">
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
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                                <asp:Panel ID="panel6" runat="server">
                                    <center>
                                         ¿Está seguro que desea eliminar el correo?  
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
                            <asp:Button runat="server" ID="Button2" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>