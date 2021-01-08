using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContenedorArchivos.Protegido
{
    public partial class About : System.Web.UI.Page
    {
        private Configuracion configuracion = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.configuracion = new Configuracion();

            //Label_Version.Text = configuracion.Version;
            //Label_FechaCompilacion.Text = configuracion.FechaCompilacion;
        }
    }
}