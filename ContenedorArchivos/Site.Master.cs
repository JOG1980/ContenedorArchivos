using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using System.Diagnostics;
using System.Drawing;

namespace ContenedorArchivos
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {

        protected void Page_init(object sender, EventArgs e)
        {

            Debug.WriteLine("MASTER - Page_Init");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            cargarDatos();
            habilitarMenu();
        }

        private void cargarDatos()
        {
            Configuracion configuracion = new Configuracion();

            Label_Titulo1.Text = configuracion.Titulo1;
            Label_Titulo1.ForeColor = ColorTranslator.FromHtml(configuracion.Titulo1Color);
            Label_Titulo1.Font.Size = int.Parse(configuracion.Titulo1Tamano);
            //Label_Titulo1.Style.Add("text-shadow", configuracion.Titulo1Color); 


            Label_Titulo2.Text = configuracion.Titulo2;
            Label_Titulo2.ForeColor = ColorTranslator.FromHtml(configuracion.Titulo2Color);
            Label_Titulo2.Font.Size = int.Parse(configuracion.Titulo2Tamano);
            //Label_Titulo2.Style.Add("text-shadow", configuracion.Titulo2Color);

            Label_Titulo3.Text = configuracion.Titulo3;
            Label_Titulo3.ForeColor = ColorTranslator.FromHtml(configuracion.Titulo3Color);
            Label_Titulo3.Font.Size = int.Parse(configuracion.Titulo3Tamano);
            //Label_Titulo3.Style.Add("text-shadow", configuracion.Titulo3Color);

            Panel_Titulo.BackColor = ColorTranslator.FromHtml(configuracion.TituloColorFondo);

        }


        private void habilitarMenu()
        {
            NavigationMenu.Visible = false;

            try
            {
                string[] rolesuser = Roles.GetRolesForUser();
                if (rolesuser.Length > 0)
                {
                    String rol = rolesuser[0];

                    if (rol.Equals("Admin"))
                    {
                        NavigationMenu.Visible = true;
                    }
                }
            }
            catch (Exception e)
            {
            }

        }
    }
}
