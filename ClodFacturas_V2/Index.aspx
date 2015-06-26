<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Index.aspx.vb" Inherits="ClodFacturas_V2.Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cloud Facturas</title>

    <link rel="icon" type="image/png" href="/img/xml_favi.png" />
    <link rel="stylesheet" href="css/EstilosIndex.css" />
    <link rel="stylesheet" href="css/Estilos.css" />

    <%-- Ventanas --%>
    <link rel="stylesheet" type="text/css" href="css/default.css" />
    <link rel="stylesheet" type="text/css" href="css/component.css" />
    <script src="js/modernizr.custom.js"></script>

    <%-- Mensajes --%>
    <link href="~/css/Mensajes.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Mensajes.min.js"></script>
    <script type="text/javascript" src="js/jquery.Mensajes_minimized.js"></script>

    <%-- iFrame--%>
    <link href="~/css/iframe.css" rel="stylesheet" type="text/css" />
    <script>
        function Actualizar(id) {
            document.getElementById(id).contentWindow.location.reload(true);
        }

        function ocultar(correos, facturas, usuarios, subirF, admin, actualizar, PagosCancelados, firmas, OC, load,Manual) {

            if (correos == 0) {
                document.getElementById('divCorreos').style.display = 'none';
            } else {
                document.getElementById('divCorreos').style.display = 'block';
            }

            if (facturas == 0) {
                document.getElementById('divFacturas').style.display = 'none';
            }
            if (usuarios == 0) {
                document.getElementById('divUsuarios').style.display = 'none';
            }
            if (subirF == 0) {
                document.getElementById('divsubirF').style.display = 'none';
            }
            if (admin == 0) {
                document.getElementById('divAdministracion').style.display = 'none';
            }
            if (actualizar == 0) {
                document.getElementById('divActualizar').style.display = 'none';
            }
            if (PagosCancelados == 0) {
                document.getElementById('divPagosCancelados').style.display = 'none';
            } else {
                document.getElementById('divPagosCancelados').style.display = 'block';
            }
            if (firmas == 0) {
                document.getElementById('divFirmas').style.display = 'none';
            } else {
                document.getElementById('divFirmas').style.display = 'block';
            }
            if (OC == 0) {
                document.getElementById('divOC').style.display = 'none';
            } else {
                document.getElementById('divOC').style.display = 'block';
            }
            if (load == 0) {
                document.getElementById('cargando').style.display = 'none';
            } else {
                document.getElementById('cargando').style.display = 'block';
            }
         
            if (Manual == 0) {
                document.getElementById('div1M').style.display = 'none';
                document.getElementById('div2M').style.display = 'block';
                
            } else {
                document.getElementById('div1M').style.display = 'block';
                document.getElementById('div2M').style.display = 'none';
            }                
        }
        //function mostrar() {
        //    document.getElementById('divCorreos').style.display = 'block';
        //    document.getElementById('divFacturas').style.display = 'block';
        //    document.getElementById('divUsuarios').style.display = 'block';

        //}
    </script>

</head>
<body>
    <section id="Principal">
        <section id="contenedor">
            <div class="logo">
                <img class="imglogo" src="/img/CloudFacturas.png" />
            </div>  
            <div class="md-modal md-effect-2" id="modal-1">
                <div class="md-content">
                    <h3>Correos<button class="md-close" id="btn1">X</button>
                    </h3>
                    <div class="demo-wrapper">
                        <div>
                            <iframe id="if_cor" src="Correos.aspx"></iframe>
                        </div>
                    </div>
                </div>
            </div>
            <div class="md-modal md-effect-2" id="modal-2">
                <div class="md-content">
                    <h3>Facturas
                        <button class="md-close" id="Button1">X</button>
                    </h3>
                    <div class="demo-wrapper">
                        <div>
                            <iframe id="if_fac" src="Facturas.aspx"></iframe>
                        </div>
                    </div>
                </div>
            </div>
            <div class="md-modal md-effect-2" id="modal-3">
                <div class="md-content">
                    <h3>Ordenes de Compra
                        <button class="md-close" id="Button2">X</button>
                    </h3>
                    <div class="demo-wrapper">
                        <div>
                            <iframe id="if_oc" src="OrdenesdeCompra.aspx"></iframe>
                        </div>
                    </div>
                </div>
            </div>
            <div class="md-modal md-effect-2" id="modal-4">
                <div class="md-content">
                    <h3>Subir Factura
                        <button class="md-close" id="Button3">X</button>
                    </h3>
                    <div class="demo-wrapper">
                        <div>
                            <iframe id="if_exa" src="Examinar.aspx"></iframe>
                        </div>
                    </div>
                </div>
            </div>
            <div class="md-modal md-effect-2" id="modal-5">
                <div class="md-content">
                    <h3>Administración
                        <button class="md-close" id="Button4">X</button>
                    </h3>
                    <div class="demo-wrapper">
                        <div>
                            <iframe id="if_adm" src="Administracion.aspx"></iframe>
                        </div>
                    </div>
                </div>
            </div>
            <div class="md-modal md-effect-2" id="modal-6">
                <div class="md-content">
                    <h3>Firmas
                        <button class="md-close" id="Button5">X</button>
                    </h3>
                    <div class="demo-wrapper">
                        <div>
                            <iframe id="if_fir" src="Firmas.aspx"></iframe>
                        </div>
                    </div>
                </div>
            </div>
            <div class="md-modal md-effect-2" id="modal-7">
                <div class="md-content">
                    <h3>Pagos/Cancelaciones
                        <button class="md-close" id="Button6">X</button>
                    </h3>
                    <div class="demo-wrapper">
                        <div>
                            <iframe id="if_pc" src="PagosCancelaciones.aspx"></iframe>
                        </div>
                    </div>
                </div>
            </div>
            <div class="md-modal md-effect-2" id="modal-8">
                <div class="md-content">
                    <h3>Cuentas
                        <button class="md-close" id="Button7">X</button>
                    </h3>
                    <div class="demo-wrapper">
                        <div>
                            <iframe id="if_cue" src="Cuentas.aspx"></iframe>
                        </div>
                    </div>
                </div>
            </div>        
            <%--Botones--%>
            <section id="cargando" class="cargandocss" style="display:none">
                <section id="contenedor2">
                    <div>
                        <asp:Image ID="Image1" Width="150px" runat="server" ImageUrl="~/img/loader2.gif" />
                    </div>
                </section>
            </section>
            <div class="col1">            
                <div class="tile-big" id="divCorreos" onclick="Actualizar('if_cor');">
                    <button class="md-trigger btn1" data-modal="modal-1">
                        <img src="img/email.png" class="imagenes" /></button>
                    <figure>
                        <figcaption class="caption-left">
                            <p>Correos Recibidos</p>
                        </figcaption>
                    </figure>
                </div>
                <div class="tile-big" id="divFacturas" onclick="Actualizar('if_fac');">
                    <button class="md-trigger btn2" data-modal="modal-2">
                        <img src="img/factura3.png" class="imagenes" /></button>
                    <figure>
                        <figcaption class="caption-left">
                            <p>Facturas recibidas </p>
                        </figcaption>
                    </figure>
                </div>
                <div class="tile-big" id="divOC" onclick="Actualizar('if_oc'); "style="display:none">
                    <button class="md-trigger btn3" data-modal="modal-3">
                        <img src="img/OC.png" class="imagenes" /></button>
                    <figure>
                        <figcaption class="caption-left">
                            <p>Orden de Compra</p>
                        </figcaption>
                    </figure>
                </div>
                <div class="tile-big" id="divsubirF" onclick="Actualizar('if_exa');">
                    <button class="md-trigger btn4" data-modal="modal-4">
                        <img src="img/xml.png" class="imagenes" /></button>
                    <figure>
                        <figcaption class="caption-left">
                            <p>Subir Facturas</p>
                        </figcaption>
                    </figure>

                </div>
            </div>
            <div class="col2">
                <div class="tile-big" id="divUsuarios" onclick="Actualizar('if_cue');">
                                <button class="md-trigger btn8" data-modal="modal-8" >
                                    <img src="img/user.png" class="imagenes" /></button>
                                <figure>
                                    <figcaption class="caption-left">
                                        <p>Cuentas</p>
                                    </figcaption>
                                </figure>
                            </div>
                <div class="divdividido" id="divAdministracion">
                    <div class="tile-big" onclick="Actualizar('if_adm');">
                        <button class="md-trigger btn5" data-modal="modal-5">
                            <img src="img/admin4.png" class="imagenes" /></button>
                        <figure>
                            <figcaption class="caption-left">
                                <p>Administración</p>
                            </figcaption>
                        </figure>
                    </div>
                </div>
                <div class="divdividido" id="divFirmas" style="display:none">
                    <div class="tile-big" onclick="Actualizar('if_fir');">
                        <button class="md-trigger btn5" data-modal="modal-6">
                            <img src="img/firma2.png" class="imagenes" /></button>
                        <figure>
                            <figcaption class="caption-left">
                                <p>Firmas OC</p>
                            </figcaption>
                        </figure>
                    </div>
                </div>
                <div class="divdividido">
                    <div class="tile-big" id="divPagosCancelados" onclick="Actualizar('if_pc');" style="display:none">
                        <button class="md-trigger btn6" data-modal="modal-7">
                            <img src="img/PagCan.png" class="imagenes" /></button>
                        <figure>
                            <figcaption class="caption-left">
                                <p>Pagos/Cancelacione</p>
                            </figcaption>
                        </figure>
                    </div>
                </div>
                 <div class="divdividido">
                    <div class="tile-big btnmanual" id="div1M"  onclick="window.open('Manual.pdf','CONTRATO','status = 1,width=600, resizable = 1');">
                        
                            <img src="img/manual.png" class="imagenes" />
                        <figure>
                            <figcaption class="caption-left">
                                <p>Manual</p>
                            </figcaption>
                        </figure>

                    </div>
                     <div class="tile-big btnmanual" id="div2M"  onclick="window.open('Manual2.pdf','CONTRATO','status = 1,width=600, resizable = 1');">
                        
                            <img src="img/manual.png" class="imagenes" />
                        <figure>
                            <figcaption class="caption-left">
                                <p>Manual</p>
                            </figcaption>
                        </figure>
                    </div>
                </div>                 
            </div>
            <div class="col3" style="float: left">

                <form id="form1" runat="server" style="float: left;width:100%">
                    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
                    

                            <div class="tile-big" id="divActualizar">
                                <asp:Button ID="btn_actualizar" runat="server" Text="Actualizar" class="tileActualizar btn7" />
                                <figure>
                                    <figcaption class="caption-left">
                                        <p>Actualizar Facturas </p>
                                    </figcaption>
                                </figure>
                            </div>                                   
                            <div class="divSalir" style="background-color: navajowhite;">
                                <asp:ImageButton ID="ImageButton1" ImageUrl="/img/exit.png" runat="server" CssClass="btnSalir" />
                                <figure>
                                    <figcaption class="caption-left">
                                        <p>Salir</p>
                                    </figcaption>
                                </figure>
                            </div>                                 
       <asp:ModalPopupExtender ID="MPE_confir" runat="server"
            TargetControlID="Button8"
            PopupControlID="panel_confir"
            BackgroundCssClass="modalBackground"
            DropShadow="FALSE"
            OkControlID="Button8">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="panel_confir" BackColor="White" CssClass="ventana" style="display:none">
            <div class="titulo" style="background-color: #2e8bcc; height: 30px; width: 100%">
                <h2 style="color: white">
                    <asp:Label ID="Label19" runat="server" Text="Resumen"></asp:Label>
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
                                       <table style="margin-left:10px; text-align:left; width:300px" >
                                            <tr>
                                                <td> Cuentas de correo leídos: </td>
                                                <td style="text-align:right" ><asp:Label  id="lb_cuentas" runat="server" ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Correos leídos: </td>
                                                <td style="text-align:right"><asp:Label  id="lb_correos_leidos" runat="server" ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Correos guardados:  </td>
                                                <td style="text-align:right"><asp:Label  id="lb_correos_guardados" runat="server" ></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td>Archivos comprimidos: </td>
                                                <td style="text-align:right"><asp:Label  id="lb_archivos_comp" runat="server" ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Facturas guardadas: </td>
                                                <td style="text-align:right"><asp:Label  id="lb_facturas_gua" runat="server" ></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td>Facturas repetidas: </td>
                                                <td style="text-align:right"><asp:Label  id="lb_fact_repetidas" runat="server" ></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td>Facturas no pertenecen al RFC: </td>
                                                <td style="text-align:right"><asp:Label  id="lb_noperteneceRFC" runat="server" ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td> Facturas no guardadas: </td>
                                                <td style="text-align:right" ><asp:Label  id="lb_factutas_nogua" runat="server" ></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td>PDF guardados: </td>
                                                <td style="text-align:right" ><asp:Label  id="lb_pdf_guar" runat="server" ></asp:Label></td>
                                            </tr>                                          
                                        </table>
                                   </center>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <div>
                            <asp:Button runat="server" ID="Button8" Text="X" class="btn_cierrate" />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>   
            </form>
            </div>
            <script src="js/classie.js"></script>
            <script src="js/modalEffects.js"></script>
            <script>
                var polyfilter_scriptpath = '/js/';
            </script>
            <script src="js/cssParser.js"></script>
            <script src="js/css-filters-polyfill.js"></script>
        </section>
    </section>
</body>
</html>
