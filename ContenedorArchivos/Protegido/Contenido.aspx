<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contenido.aspx.cs" Inherits="ContenedorArchivos.Protegido.Contenido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../Styles/jquery-ui-1.11.0.css" rel="stylesheet" type="text/css" />
<link href="../Styles/waitMe.min.css" rel="stylesheet" type="text/css" />
<link href="../Styles/theme.blue.css" rel="stylesheet" type="text/css" />
<style rel="stylesheet" type="text/css">
.sel_op
{
    background-color: #AAAAAA; 
}
    .style1
    {
        width: 111px;
    }
    .style2
    {
        height: 26px;
    }
    .style3
    {
        width: 111px;
        height: 21px;
    }
    .style4
    {
        height: 21px;
    }
  
    
</style>

<script src="../Scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.11.0.min.js" type="text/javascript"></script>
<script src="../Scripts/waitMe.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.tablesorter.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.tablesorter.widgets.min.js" type="text/javascript"></script>

<script src="Scripts/Contenido.js" type="text/javascript"></script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<div id="DivContenedorPrincipal" style=" float: left; width: 100%; height: 100%; ">

<asp:HiddenField ID="HiddenField_TipoRol" runat="server" value=""/>

<asp:HiddenField ID="HiddenField_FirstTime" runat="server" value=""/>

<asp:HiddenField ID="HiddenField_NombreCarpeta" runat="server" value=""/>

<asp:HiddenField ID="HiddenField_NombreArchivo" runat="server" Value="" />


<asp:HiddenField ID="HiddenField_TipoVista" runat="server" />

<asp:HiddenField ID="HiddenField_HabilitarMensajeBienvenida" runat="server" />
<asp:HiddenField ID="HiddenField_TituloMensajeBienvenida" runat="server" />

<asp:HiddenField ID="HiddenField_FileExtensions" runat="server" />

<div id="dialogo_file_upload" title="Subir Archivo">

    <asp:FileUpload ID="FileUpload1" runat="server" Width="480px" />
    <br />
    <table style="width: 100%">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        
        <tr>
            <td class="style1"><span style=" font-size: 12px;">Fecha del Archivo</span><span style=" font-size: 10px;"><br />(AAAA-MM-DD):</span></td>
            <td>
            <asp:TextBox ID="TextBox_FechaArchivo" runat="server" Width="70px"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td style=" vertical-align: top;" class="style1">
                <span style=" font-size: 12px;">Descripción del Archivo:</span></td>
            <td>
                <asp:TextBox ID="TextBox_DescripcionArchivo" runat="server" Height="92px" 
                    TextMode="MultiLine" Width="376px" Font-Size="10px"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td style="text-align: right;">
                <asp:Button ID="Button_SubirArchivo" runat="server" Text="Subir Archivo" Height="26px" onclick="Button_SubirArchivo_Click" /></td>
            
        </tr>
    </table>
    <br />
    
   
</div>



<div id="dialogo_carpeta_opciones">
<div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoCrearCarpeta()"><img src="../Images/IconPack1/16x16/folder_add.png"  /><span style="font-size: 11px;">Crear carpeta</span></div>
<div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoBorrarCarpeta()"><img src="../Images/IconPack1/16x16/folder_remove.png" /><span style="font-size: 11px;">Eliminar carpeta</span></div>
<!--div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoEditarResumenCarpeta()"><img src="../Images/IconPack1/16x16/folder_edit.png" /><span style="font-size: 11px;">Editar carpeta</span></div -->
<div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoRenombrarCarpeta()"><img src="../Images/IconPack1/16x16/folder_process.png" /><span style="font-size: 11px;">Renombrar carpeta</span></div>
<hr style=" background-color: #AAAAAA; height: 1px;"/>
<div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoFileUpload()"><img src="../Images/IconPack1/16x16/page_up.png" /><span style="font-size: 11px;">Subir Archivo</span></div>
</div>



<div id="dialogo_interior_carpeta_view">
  <div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoCrearCarpeta()"><img src="../Images/IconPack1/16x16/folder_add.png"  /><span style="font-size: 11px;">Crear carpeta</span></div>
  <hr style=" background-color: #AAAAAA; height: 1px;"/>
  <div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoFileUpload()"><img src="../Images/IconPack1/16x16/page_up.png" /><span style="font-size: 11px;">Subir Archivo</span></div>
</div>


<div id="dialogo_archivo_opciones">
    <!--div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoEditarResumenArchivo()" ><img src="../Images/IconPack1/16x16/note_edit.png" /><span style="font-size: 11px;">Editar Resumen</span></div -->
     <div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoRenombrarArchivo()" ><img src="../Images/IconPack1/16x16/page_swap.png" /><span style="font-size: 11px;">Renombrar Archivo</span></div>
   <div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoBorrarArchivo()" ><img src="../Images/IconPack1/16x16/note_remove.png" /> <span style="font-size: 11px;">Eliminar Archivo</span></div>
</div>


<div id="dialogo_click_derecho_carpeta_opciones">
<div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoBorrarCarpeta()"><img src="../Images/IconPack1/16x16/folder_remove.png" /><span style="font-size: 11px;">Eliminar carpeta</span></div>
<!--div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoEditarResumenCarpeta()"><img src="../Images/IconPack1/16x16/folder_edit.png" /><span style="font-size: 11px;">Editar carpeta</span></div-->
<div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoRenombrarCarpeta()"><img src="../Images/IconPack1/16x16/folder_process.png" /><span style="font-size: 11px;">Renombrar carpeta</span></div>
<hr style=" background-color: #AAAAAA; height: 1px;"/>
<div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoFileUpload()"><img src="../Images/IconPack1/16x16/page_up.png" /><span style="font-size: 11px;">Subir Archivo</span></div>
</div>

<div id="dialogo_click_derecho_archivo_opciones">
    <!--div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoEditarResumenArchivo()" ><img src="../Images/IconPack1/16x16/note_edit.png" /><span style="font-size: 11px;">Editar Resumen</span></div-->
    <div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoRenombrarArchivo()" ><img src="../Images/IconPack1/16x16/page_rename.png" /><span style="font-size: 11px;">Renombrar Archivo</span></div>
    <div class="class_sel_op" style="cursor:pointer;" onclick="mostrarDialogoBorrarArchivo()" ><img src="../Images/IconPack1/16x16/note_remove.png" /><span style="font-size: 11px;">Eliminar Archivo</span></div>
</div>

<div id="dialogo_crear_carpeta" title="Crear Carpeta">
        <table><tr>
        <td class="style2">
            <span style="font-size: 12px;">Nombre de la Carpeta: </span>
        </td>
        <td class="style2">
            <asp:TextBox ID="TextBox_CrearCarpeta_Nombre" runat="server" Width="167px"></asp:TextBox>
        </td>
        </tr>
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
         <tr>
            <td class="style1"><span style=" font-size: 12px;">Fecha del Archivo</span><span style=" font-size: 10px;"><br />(AAAA-MM-DD):</span></td>
            <td>
            <asp:TextBox ID="TextBox_FechaCarpeta" runat="server" Width="70px"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td style=" vertical-align: top;" class="style1">
                <span style=" font-size: 12px;">Descripción del Archivo:</span></td>
            <td>
                <asp:TextBox ID="TextBox_DescripcionCarpeta" runat="server" Height="92px" 
                    TextMode="MultiLine" Width="376px" Font-Size="10px"></asp:TextBox>
            </td>
            
        </tr>
           <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td>&nbsp;</td><td style=" text-align: right;">
            <asp:Button ID="Button_CrearCarpeta" runat="server" Text="Crear Carpeta" 
        onclick="Button_CrearCarpeta_Click" />

        </td></tr>
        </table>


</div>

<div id="dialogo_borrar_carpeta" title="Borrar Carpeta">
    <p>Esta seguro que quiere borrar la carpeta:<br /> <span id="etiqueta_nombre_borrar_carpeta" style=" font-weight: bold;">Carpeta</span> ?</p><br />
    <div style=" text-align: right;">
    <asp:Button ID="Button_BorrarCarpeta" runat="server" Text="Borrar Carpeta" 
        onclick="Button_BorrarCarpeta_Click" />
    </div>    
</div>

<div id="dialogo_renombrar_carpeta" title="Renombrar Carpeta">
    <p>Renombrar la carpeta:<br /> <span id="etiqueta_nombre_renombrar_carpeta">Carpeta</span> ?</p>
    <p>Por: 
        <asp:TextBox ID="TextBox_NuevoNombreCarpeta" runat="server"></asp:TextBox></p>
    <asp:Button ID="Button_RenombrarCarpeta" runat="server" 
        Text="Renombrar Carpeta" onclick="Button_RenombrarCarpeta_Click"   />
</div>



<div id="dialogo_borrar_archivo" title="Borrar Archivo">
    <p>Esta seguro que quiere borrar el archivo: <span id="etiqueta_nombre_borrar_archivo">Archivo</span> ?</p>
    <asp:Button ID="Button_BorrarArchivo" runat="server" Text="Borrar Archivo" 
        onclick="Button_BorrarArchivo_Click" />
</div>


<div id="dialogo_renombrar_archivo" title="Renombrar Archivo">
    <p>Renombrar el archivo: <span id="etiqueta_nombre_renombrar_archivo">Archivo</span> ?</p>
    <p>Por: 
        <asp:TextBox ID="TextBox_NuevoNombreArchivo" runat="server"></asp:TextBox></p>
    <asp:Button ID="Button_RenombrarArchivo" runat="server" 
        Text="Renombrar Archivo" onclick="Button_RenombrarArchivo_Click"  />
</div>




<div id="dialogo_editar_descripcion_carpeta" title="Editar Carpeta">
<span id="etiqueta_nombre_editar_documentacion_carpeta"> Nombre Carpeta</span>
    <table style="width: 100%">
        <tr>
            <td class="style1" style="width: 140px;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        
        <tr>
            <td class="style1"><span style=" font-size: 12px;">Fecha de la Carpeta</span><span style=" font-size: 10px;"><br />(AAAA-MM-DD):</span></td>
            <td>
            <asp:TextBox ID="TextBox_EditarFechaCarpeta" runat="server" Width="70px"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td style=" vertical-align: top;" class="style1">
                <span style=" font-size: 12px;">Descripción de la Carpeta:</span></td>
            <td>
                <asp:TextBox ID="TextBox_EditarDescripcionCarpeta" runat="server" Height="92px" 
                    TextMode="MultiLine" Width="376px" Font-Size="10px"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td style="text-align: right;">
                <asp:Button ID="Button_EditarDescripcionCarpeta" runat="server" 
                    Text="Guardar Edición" Height="26px" 
                    onclick="Button_EditarDescripcionCarpeta_Click" Width="123px" /></td>
            
        </tr>
    </table>
    <br />
    
   
</div>



<div id="dialogo_editar_descripcion_archivo" title="Editar Archivo">
<span id="etiqueta_nombre_editar_documentacion_archivo"> Nombre Archivo</span>
    <table style="width: 100%">
        <tr>
            <td class="style3">
                </td>
            <td class="style4">
                </td>
            
        </tr>
        
        <tr>
            <td class="style1"><span style=" font-size: 12px;">Fecha del Archivo</span><span style=" font-size: 10px;"><br />(AAAA-MM-DD):</span></td>
            <td>
            <asp:TextBox ID="TextBox_EditarFechaArchivo" runat="server" Width="70px"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td style=" vertical-align: top;" class="style1">
                <span style=" font-size: 12px;">Descripción del Archivo:</span></td>
            <td>
                <asp:TextBox ID="TextBox_EditarDescripcionArchivo" runat="server" Height="92px" 
                    TextMode="MultiLine" Width="376px" Font-Size="10px"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td style="text-align: right;">
                <asp:Button ID="Button_EditarDescripcionArchivo" runat="server" 
                    Text="Guardar Edición" Height="26px" 
                    onclick="Button_EditarDescripcionArchivo_Click" Width="123px" /></td>
            
        </tr>
    </table>
    <br />
    
   
</div>




<div id="dialogo_mensaje">
    
</div>

<div style="width: 100%; float: left;">

<table style="width: 100%;">
<tr><td style="width: 114px; border-right: 1px solid #AAAAAA; ">
 <div>
     <div style="float: left; display: block; width: 35px; height: 35px;">
           <div style=" position:absolute; z-index: 2; float: left; width: 35px; height: 35px;"><asp:ImageButton ID="ImageButton_ViewTree" runat="server" 
                    ImageUrl="~/Images/icons/node-tree.png" 
                    onclick="ImageButton_ViewTree_Click" class="imgclick" />
            </div>
            <div id="treeview_button_selected" class="button_selected" style=" position:absolute; z-index: 3;  float: left;  width: 32px; height: 32px; background-color: rgba(0, 0, 0, 0.2);">
            </div>
            
     </div>
     <div style="float: left; display: block; width: 35px; height: 35px;">
            <div style=" position:absolute; z-index: 2;  float: left; width: 35px; height: 35px;"><asp:ImageButton ID="ImageButton_ViewIcons" runat="server" 
                ImageUrl="~/Images/icons/application_view_icons.png" 
                onclick="ImageButton_ViewIcons_Click" class="imgclick" />
            </div>
            <div  id="icons_button_selected" class="button_selected"  style=" position:absolute; z-index: 3;  float: left;  width: 32px; height: 32px; background-color: rgba(0, 0, 0, 0.2);">
             </div>        
     </div>
     <div style="float: left; display: block; width: 35px; height: 35px;">
       <div style="  position:absolute; z-index: 2;  float: left;  width: 35px; height: 35px;"><asp:ImageButton ID="ImageButton_ViewDetails" runat="server" 
                ImageUrl="~/Images/icons/application_view_detail.png" 
                onclick="ImageButton_ViewDetails_Click" class="imgclick" />
        </div>
    
        <div  id="details_button_selected" class="button_selected"  style=" position:absolute; z-index: 3;  float: left;  width: 32px; height: 32px; background-color: rgba(0, 0, 0, 0.2);">
         </div>
         
       </div>
 </div>
</td><td style="width: 128px; padding: 4px 0px 7px 4px;  border-right: 1px solid #AAAAAA;  ">
      
    <div>    <asp:CheckBox ID="CheckBox_TreeViewExpandAll" runat="server" 
            AutoPostBack="True" Font-Size="12px" 
            oncheckedchanged="CheckBox_TreeViewExpandAll_CheckedChanged" 
            Text="Expandir/Contraer" />
    </div>

</td><td style="width: 670px; padding: 4px 0px 7px 4px;  ">

     <div id="div_ruta_carpeta">
        <span style=" font-size: 12px; ">&nbsp;&nbsp;Ruta de la Carpeta &nbsp;&nbsp;</span><input id="Text_Vista_CarpetaActual" type="text" readonly style="font-size: 11px; width: 500px;" />
        <!--img src="../Images/icons/folder_add.png" style="cursor: pointer;" /-->
         <!--img src="../Images/IconPack1/24x24/folder_up.png" style="cursor: pointer;" onclick="subirNivelCarpeta()"  /-->
         <img src="../Images/icons/flecha_arriba.png" style="cursor: pointer;" onclick="subirNivelCarpeta()" />
    </div>
    </td></tr>
</table>
</div>


<div id="TreeDiagram" style="float: left; min-height: 400px; min-width: 140px; max-width: 900px; width: 247px; border-top: 1px solid #AAAAAA; overflow-x: hidden; overflow-y: auto; padding-left: 5px;">
   
        <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" 
            NodeIndent="15" LineImagesFolder="~/TreeLineImages" ShowLines="True" 
            Target="_blank">
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
                HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:TreeView>
   
</div>    
    <div id="div_vista_contenedor"  style=" float: left; display: block; border-top: 1px solid #AAAAAA;  width: 100%; height: 100%; " >
    


        <div id="div_vista" style=" padding-left: 13px;" >

            <table id="tbl_vista_details" class="tablesorter" style="width: 100%; "><thead>
            <tr>
            <th style=" width: 30px;">
                &nbsp;
            </th>
            <th>
                Nombre
            </th>
            <th>
                Tamaño
            </th>
            <th>
                Tipo
            </th>
            <th>
                Descripción
            </th>
            
            </tr>
            </thead>
            <tbody>
            </tbody>
            </table>
        </div>


        
    </div>


</div>
<br />


<div id="div_mensaje_bienvenida" style="text-align: justify;">
    <asp:Label ID="Label_MensajeBienvenida" runat="server" Text="Label"></asp:Label>
    
</div>


</asp:Content>
