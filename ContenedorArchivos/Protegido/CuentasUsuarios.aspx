<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuentasUsuarios.aspx.cs" Inherits="ContenedorArchivos.Protegido.CuentasUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


<link href="../Styles/jquery-ui-1.11.0.css" rel="stylesheet" type="text/css" />
<link href="../Styles/theme.blue.css" rel="stylesheet" type="text/css" />

<script src="../Scripts/jquery-1.11.2.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.11.0.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.tablesorter.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.tablesorter.widgets.min.js" type="text/javascript"></script>

<script src="Scripts/CuentasUsuarios.js" type="text/javascript"></script>


    <style type="text/css">
        
        #Button1
        {
            width: 87px;
        }
        
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:HiddenField ID="HiddenField_UsuarioSeleccionado" runat="server" 
        Value="" />
<div style=" float:left; width: 100%; display:block;">



    <table style=" width: 100%;">
    
    <tr><td>
    
    <div style=" float: left; width: 300px;">
    <span style=" font-size: 14px;"> Lista de usuarios en esta aplicación</span>
    </div>
    <div style=" float: right; ">
        <input id="Button_AbrirPanel_GuardarPunto" type="button" value="Crear Usuario" 
                   onclick="mostrarDialogoCrearUsuario()"
                   style="width: 130px; "  />

       
    </div>
    
    </td></tr>
    
    <tr><td>
    <asp:Panel ID="Panel_Usuarios" runat="server" Height="400px" 
        ScrollBars="Vertical" Width="100%" BorderStyle="Outset">
    </asp:Panel>

     

    </td>
    </tr></table>
</div>
<div style="float: right;">
     <asp:Button ID="Button_ExportarUsuarios" runat="server" 
                    onclick="Button_ExportarUsuarios_Click" Text="Exportar Usuarios" 
                    Width="87px" Font-Size="10px" />
        <input id="Button1" type="button" style=" font-size: 10px;" value="Importar Usuarios" onclick="mostrarDialogoImportarUsuarios()" />
        
</div>
    <asp:Label ID="Label_Mensaje" runat="server" Text="Label"></asp:Label>
    <div id="Dialogo_CrearUsuario" title="Crear Nuevo Usuario">
        <table style="width: 100%;">
            <tr>
                <td style=" text-align: right;">
                   Usuario <span style=" font-size: 11px;">(Nikname)</span>:</td>
                <td>
                    <asp:TextBox ID="TextBox_NombreUsuario" runat="server" ToolTip="Este es el nombre con el que se accesará"  placeholder="ej: UsuarioZOT"></asp:TextBox>
                   *
                </td>
               
            </tr>
            <tr>
                <td style=" text-align: right;">
                    Password:</td>
                <td>
                    <asp:TextBox ID="TextBox_Password" runat="server"></asp:TextBox>
                     *
                </td>
                
            </tr>
            
             <tr>
                <td style=" text-align: right;">
                    Rol:</td>
                <td>
                   
                <asp:DropDownList ID="DropDownList_Roles" runat="server">
             <asp:ListItem Selected="True">User</asp:ListItem>
             <asp:ListItem>SuperUser</asp:ListItem>
             <asp:ListItem>Admin</asp:ListItem>
         </asp:DropDownList>
                </td>
               
            </tr>
            <tr>
                <td  style=" text-align: right;">Nombre:</td>
                <td>
                    <asp:TextBox ID="TextBox_Nombre" runat="server"></asp:TextBox>
                     
                </td>
               
            </tr>
            <tr>
                <td  style=" text-align: right;">Apellido Paterno:</td>
                <td>
                    <asp:TextBox ID="TextBox_ApellidoPaterno" runat="server" ></asp:TextBox>
                     
                </td>
               
            </tr>
            <tr>
                <td  style=" text-align: right;">Apellido Materno:</td>
                <td>
                    <asp:TextBox ID="TextBox_ApellidoMaterno" runat="server" ></asp:TextBox>
                     
                </td>
               
            </tr>
            <tr>
                <td  style=" text-align: right;">
                    Email:</td>
                <td>
                    <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
                     
                </td>
               
            </tr>
            <tr>
                <td  style=" text-align: right;">
                    Email de Aviso:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList_EnviarEmail" runat="server">
                        <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                        <asp:ListItem Value="True">Si</asp:ListItem>
                     </asp:DropDownList>
                </td>
                
            </tr>
            
            
        </table>
        <br />
        <br />
        <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;(*) Campos obligatorios
                </td>
                <td style="text-align: right;">
                    &nbsp;
                <asp:Button ID="Button_CrearUsuario" runat="server" Text="Crear" 
            onclick="Button_CrearUsuario_Click" CausesValidation="True" />
                </td>
               
            </tr>
            </table>
        </div>
    </div>

     <div id="Dialogo_EditarUsuario" title="Editar Usuario">
         
         <table style="width: 100%;">
            <tr><td style=" text-align: right;">Usuario <span style=" font-size: 11px;">(Nikname)</span>:</td>
            <td><span style=" font-size: 14px; font-weight:bolder" id="span_etiqueta_nombre_usuario">Usuario</span></td></tr>
            <tr><td></td><td></td></tr>
             <tr>
                 <td style=" text-align: right;">
                     Password:
                 </td>
                 <td>
                     <asp:TextBox ID="TextBox_Password2" runat="server"></asp:TextBox>
                     <asp:HiddenField ID="HiddenField_OldPassword" runat="server" Value="" />
                 </td>
                 
             </tr>

             <tr>
                 <td  style=" text-align: right;">
                     Rol:
                 </td>
                 <td>
                     <asp:DropDownList ID="DropDownList_Roles2" runat="server">
                     <asp:ListItem Selected="True">User</asp:ListItem>
                     <asp:ListItem>SuperUser</asp:ListItem>
                     <asp:ListItem>Admin</asp:ListItem>
                     </asp:DropDownList>
                     <asp:HiddenField ID="HiddenField_OldRol" runat="server" Value="" />
                 </td>
                 
             </tr>

                   <tr>
                <td  style=" text-align: right;">Nombre:</td>
                <td>
                    <asp:TextBox ID="TextBox_Nombre2" runat="server"></asp:TextBox>
                     
                </td>
               
            </tr>
            <tr>
                <td  style=" text-align: right;">Apellido Paterno:</td>
                <td>
                    <asp:TextBox ID="TextBox_ApellidoPaterno2" runat="server" ></asp:TextBox>
                     
                </td>
               
            </tr>
            <tr>
                <td  style=" text-align: right;">Apellido Materno:</td>
                <td>
                    <asp:TextBox ID="TextBox_ApellidoMaterno2" runat="server" ></asp:TextBox>
                     
                </td>
               
            </tr>




             <tr>
                 <td style=" text-align: right;">
                     Email:
                 </td>
                 <td>
                     <asp:TextBox ID="TextBox_Email2" runat="server" ></asp:TextBox>
                 </td>
                 
             </tr>
             <tr>
                <td  style=" text-align: right;">
                    Email de Aviso:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList_EnviarEmail2" runat="server">
                        <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                        <asp:ListItem Value="True">Si</asp:ListItem>
                     </asp:DropDownList>
                </td>
                
            </tr>
             
             <tr><td>&nbsp;</td><td style="text-align: right"><br /><br />
                 <asp:Button ID="Button_EditarUsuario" runat="server" Text="Editar" 
                     onclick="Button_EditarUsuario_Click" /></td></tr>
         </table>

     </div>
                              
         
     
     <div id="Dialogo_BorrarUsuario" title="Borar Usuario">
        <p>Esta seguro que desea borrar el usuario <span style=" font-size: 14px; font-weight: bold;" id="span_etiqueta_borrar_usuario">Usuario</span></p>
        <br /><br /><br />
        <div style="text-align: right;">
         <asp:Button ID="Button_BorrarUsuario" runat="server" Text="Borrar Usuario" 
             onclick="Button_BorrarUsuario_Click" Width="127px" />
        </div>
     </div>


     <div id="Dialogo_ImportarUsuarios">
         <asp:FileUpload ID="FileUpload1" runat="server" />
         <asp:Button ID="Button_ImportarUsuarios" runat="server" 
             onclick="Button_ImportarUsuarios_Click" Text="Importar" />
     </div>
     

</asp:Content>
