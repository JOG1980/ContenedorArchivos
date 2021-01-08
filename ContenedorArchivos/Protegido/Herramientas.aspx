<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Herramientas.aspx.cs" Inherits="ContenedorArchivos.Protegido.Herramientas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="300px" Height="200px">
                
                  <span>Arreglar nombres de los archivos</span>  
                    <asp:Button ID="Button_ArreglarNombreArchivos" runat="server" 
                        Text="Arreglar Nombres" Height="38px" Width="147px" 
                        onclick="Button_ArreglarNombreArchivos_Click" />
                </asp:Panel>
            </td>
            <td>
                &nbsp;
            </td>
           
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            
        </tr>
    </table>

</asp:Content>
