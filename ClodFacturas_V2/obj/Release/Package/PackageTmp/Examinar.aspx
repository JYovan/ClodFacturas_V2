<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Examinar.aspx.vb" Inherits="ClodFacturas_V2.Examinar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

     <%-- Mensajes --%>
    <link href="~/css/Mensajes.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Mensajes.min.js"></script>
    <script type="text/javascript" src="js/jquery.Mensajes_minimized.js"></script>
    <%--  --%>
    <link rel="stylesheet" href="css/EstilosExaminar.css" />

</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="contenido">
          
          
            <br />
            
            <div>
              
                    
                            <asp:Panel id="panel_xml" runat="server" GroupingText="Subir XML" Width="50%" CssClass="panel">

                                <table>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="FileUpload1" EnableTheming="true" runat="server"  >


                                            </asp:FileUpload>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">            
                                           <%--  <asp:Button  ID="UploadButton"                                           
                                            runat="server" CssClass="botonExa"   ></asp:Button>--%>

                                            <asp:ImageButton ID="UploadButton" runat="server" ImageUrl="/img/save2.png" CssClass="boton" BackColor="Green"  />    

                                        </td>
                                    </tr>
                                </table>

                                

                            </asp:Panel>
                            <asp:Panel id="panel_pdf" runat="server" GroupingText="Subir PDF" Width="50%" CssClass="panel" Visible="false" >

                                <table>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="FileUpload2"  runat="server" ></asp:FileUpload>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">            
                                           <%--  <asp:Button ID="UploadButtonpdf"
                                       
                                            runat="server"  CssClass="botonExa"  ></asp:Button>--%>

                                            <asp:ImageButton ID="UploadButtonpdf" runat="server" ImageUrl="/img/save2.png" CssClass="boton" BackColor="Green"  />    




                                        </td>
                                    </tr>
                                </table>

                                

                            </asp:Panel>
                      
                    <br />

                   

                <asp:Label ID="UploadStatusLabel"
                    runat="server">
                </asp:Label>

           





                <br />
                <br />

                
            </div>
        </div>


    </form>
</body>
</html>
