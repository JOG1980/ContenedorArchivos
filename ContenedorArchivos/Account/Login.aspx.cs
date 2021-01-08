using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Diagnostics;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Collections;

namespace ContenedorArchivos.Account
{
    public partial class Login : System.Web.UI.Page
    {
      

        protected void Page_Load(object sender, EventArgs e)
        {
            initFirstTime();
        }


        void redirectLogueo()
        {
            string[] roles = Roles.GetRolesForUser(this.User.Identity.Name);
            String rol = roles[0];
            String usuario = this.User.Identity.Name;

            Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Debug.WriteLine("rol: " + rol);
            Debug.WriteLine("usuario: " + usuario);

            if (rol.Equals("Admin")) Response.Redirect("~/Admin/AdminContent.aspx");
            else if (rol.Equals("SuperUser")) Response.Redirect("~/SuperUser/SuperUserContent.aspx");
            else if (rol.Equals("User")) Response.Redirect("~/User/UserContent.aspx");

        }

        //se valida la primera vez a partir de la pantalla de logueo. Esto se definio asi debido a que se considera que siempre
        //que se realiza un logueo es la primera vez que el usuario entra
        private void initFirstTime()
        {
            Session["FirstTime"] = true;
        }

    }
}
