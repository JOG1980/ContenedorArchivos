<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="ContenedorArchivos.Protegido.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


    <link href="../Styles/jquery-ui-1.11.0.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/colpick.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.11.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/colpick.js" type="text/javascript"></script>
    
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        .style2
        {
            height: 20px;
            width: 49px;
        }
        .style3
        {
            width: 49px;
        }
        .colorBoton {
	        margin:0;
	        padding:0;
	        border:0;
	        width:70px;
	        height:20px;
	        /*border-right:20px solid green;*/
	        border-right:20px solid;
	        line-height:20px;
        }
        .style4
        {
            height: 24px;
        }
    </style>

   
<script src="Scripts/Configuracion.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<div style=" float:left; width: 100%; display:block;">

<table style="width: 100%;">
    <tr>
    <td style="vertical-align: top;">
        <table>
        <tr><td>
             <asp:Panel ID="Panel1" runat="server" Width="500px" BorderStyle="Solid" style=" padding: 7px;">
    <div><span style=" font-weight: bolder;">Datos de los Titulos:</span></div>
    <br />
        <table style="width: 100%;">
            <thead>
            <tr style=" text-align: left; font-size: 11px; background-color: #666666; color: White; ">
            <th>&nbsp;</th>
            <th>Texto</th>
            <th>Color</th>
            <th>Tamaño</th>
            
            </tr>
            </thead>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Text="Titulo 1" Font-Size="10px"></asp:Label></td>
                <td class="style1">
                    <asp:TextBox ID="TextBox_Titulo1" runat="server" Font-Size="10px" Width="270px" MaxLength="80" ></asp:TextBox>
                </td>
                <td>
                <asp:TextBox ID="TextBox_Titulo1Color" runat="server" Font-Size="10px" Width="47px" 
                        CssClass="colorBoton" BorderColor="Black"></asp:TextBox>
                    
                </td>
                <td>
                    <asp:TextBox ID="TextBox_Titulo1_Tamano" runat="server" Font-Size="10px" 
                        MaxLength="2" Width="30px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="Label2" runat="server" Text="Titulo 2" Font-Size="10px"></asp:Label></td>
                <td>
                    <asp:TextBox ID="TextBox_Titulo2" runat="server" Font-Size="10px" Width="270px" MaxLength="80"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox ID="TextBox_Titulo2Color" runat="server" Font-Size="10px"  Width="47px" CssClass="colorBoton"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox_Titulo2_Tamano" runat="server" Font-Size="10px" 
                        MaxLength="2" Width="30px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="Label3" runat="server" Text="Titulo 3" Font-Size="10px"></asp:Label></td>
                
                <td>
                    <asp:TextBox ID="TextBox_Titulo3" runat="server" Font-Size="10px" Width="270px" MaxLength="80"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox ID="TextBox_Titulo3Color" runat="server" Font-Size="10px"  Width="47px" CssClass="colorBoton"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox_Titulo3_Tamano" runat="server" Font-Size="10px" 
                        MaxLength="2" Width="30px"></asp:TextBox>
                </td>
            </tr>
            </table>


            <table style="width: 100%;">
            <tr>
            <td style=" width: 80px;">
            <asp:Label ID="Label5" runat="server" Text="Color de Fondo:" Font-Size="10px"></asp:Label></td>
            
            <td>
                 <asp:TextBox ID="TextBox_FondoColor" runat="server" Font-Size="10px"  Width="47px" CssClass="colorBoton"></asp:TextBox>
                
              
            </td>
            <td class="style3">&nbsp;</td><td align="right">
                
                </td><td align="right">
                
                </td><td style=" text-align: right;">
                <asp:Button ID="Button_CargarColoresDefault" runat="server" Font-Size="9px" 
                    Text="Deafault" Width="45px" onclick="Button_CargarColoresDefault_Click" />
                </td></tr>
        </table>
        
      
    </asp:Panel>
        </td></tr>
        <tr><td>
                <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid"  style="padding: 7px;">
              <div><span style=" font-weight: bold;">Vista Inicial:</span>
              </div>
              <br />
              <table><tr><td>
              <span>Selecciona vista:</span>
              </td><td>  
                <asp:DropDownList ID="DropDownList_VistaInicial" runat="server">
                    <asp:ListItem Selected="True" Value="tree">Arbol</asp:ListItem>
                    <asp:ListItem Value="icons">Iconos</asp:ListItem>
                    <asp:ListItem Value="details">Detalles</asp:ListItem>
                </asp:DropDownList>
                </td></tr></table>
            </asp:Panel>
        </td></tr>
        <tr><td>            <asp:Panel ID="Panel3" runat="server" Width="500px " BorderStyle="Solid" style="padding: 7px;">
            <div><span style=" font-weight: bold;">Envio de avisos por email</span></div>
            <br />
            <table>
            <tr>
            <td>
                <asp:CheckBox ID="CheckBox_HabilitarEnvioEmailAviso" runat="server" 
                    Font-Size="10px" Text="Habilitar Envio" />
            </td>
            </tr>
            <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Direccion Ip del Servidor SMTP:" Font-Size="10px"></asp:Label>
                <asp:TextBox ID="TextBox_ServidorSmtpIp" runat="server" Font-Size="10px" 
                    Width="150px"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Email del Remitente" Font-Size="10px"></asp:Label>
                <asp:TextBox ID="TextBox_EmailRemitente" runat="server" Font-Size="10px" 
                    Width="195px"></asp:TextBox>
            </td>
            </tr>
            
            </table>
            </asp:Panel>
</td></tr>
<tr>
<td>
    <asp:Panel ID="Panel5" runat="server" Width="500" BorderStyle="Solid" style=" padding: 7px;">
    <table style="width: 100%;">
        <tr><td>Imagenes del Sistema</td><td>&nbsp;</td></tr>
        <tr>
        <td>
        <span style="font-size: 10px;">Logo 1: </span>
            <asp:Label ID="Label_ImagenLogo1" runat="server" Text="Logo 1" Font-Size="10px"></asp:Label>
        </td>
        <td style="text-align: right;">
            <input type="button" value="Seleccionar Imagen Logo 1" style="font-size: 10px;" onclick="mostrarVentanaSubirArchivo('logo1');" />
        </td>
        </tr>
        <tr><td>
        <span style="font-size: 10px;">Logo 2: </span>
            <asp:Label ID="Label_ImagenLogo2" runat="server" Text="Logo 2"  Font-Size="10px"></asp:Label>
        </td>
        <td  style="text-align: right;">
        <input type="button" value="Seleccionar Imagen Logo 2" style="font-size: 10px;" onclick="mostrarVentanaSubirArchivo('logo2');" />
        </td></tr>
        <tr><td>
        <span style="font-size: 10px;">Portada: </span>
            <asp:Label ID="Label_ImagenPortada" runat="server" Text="Portada"  Font-Size="10px"></asp:Label>
        </td>
        <td  style="text-align: right;">
        <input type="button" value="Seleccionar Imagen Portada" style="font-size: 10px;" onclick="mostrarVentanaSubirArchivo('portada');" />
        </td></tr>
    </table>
    </asp:Panel>
</td>
</tr>
        </table>

    </td>
    <td style="vertical-align: top;">
        <table >
        <tr><td>   <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid"  style="padding: 7px;">
              <div><span style=" font-weight: bold;">Mensaje Bienvenida:</span>
              </div>
              <br />
              <table>
              <tr><td class="style4">
                  <asp:CheckBox ID="CheckBox_HabilitarMensajeBienvenida" runat="server" 
                      Text="Habilitar Mensaje de Bienvenida" />
              </td></tr>
              <tr><td>
                  <span>Titulo: </span>&nbsp;
                  <asp:TextBox ID="TextBox_TituloMensajeBienvenida" runat="server" Width="290px" 
                      Font-Size="10px"></asp:TextBox>
              </td></tr>
              <tr><td class="style1">
              <span>Mensaje:</span>
                  <asp:TextBox ID="TextBox_MensajeBienvenida" runat="server" Height="181px" 
                      Width="348px" TextMode="MultiLine" Font-Size="10px"></asp:TextBox>


              </td></tr></table>
            </asp:Panel></td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td>&nbsp;</td></tr>
        </table>
    </td>
    </tr>
    <tr><td>&nbsp;</td>
    <td style="text-align: right">
    <asp:Button ID="Button_GuardarCambios" runat="server" Text="Guardar Cambios" onclick="Button_GuardarCambios_Click" />
        
    </td></tr>
</table>


</div>

<div id="dialog_subir_imagen_portada">

<label id="mensaje_subir_archivo" style="font-size: 12px;">Selecciona una imagen *.jpg (preferentemente de 800x600 pixeles) y despues preciona el boton subir</label>

    <asp:FileUpload ID="FileUpload_ImagenPortada" runat="server"/>
<br />
  <br />
    <asp:Button ID="Button_SubirImagenLogo1" runat="server" 
        Text="Subir Imagen Logo 1" onclick="Button_SubirImagenLogo1_Click" />
    <asp:Button ID="Button_SubirImagenLogo2" runat="server" 
        Text="Subir Imagen Logo 2" onclick="Button_SubirImagenLogo2_Click" />
    <asp:Button ID="Button_SubirImagenPortada" runat="server" 
        Text="Subir Imagen Portada" onclick="Button_SubirImagenLogoPortada_Click" />
        <br />
        <br />
        <span style=" font-size: 10px;">NOTA: Si el archivo no es una imagen JPG, esta no se sustituira.</span>
</div>


</asp:Content>
