using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Diagnostics;


namespace ContenedorArchivos.Protegido
{
    public partial class Configuration : System.Web.UI.Page
    {


        Configuracion configuracion = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            this.configuracion = new Configuracion();

            if (!IsPostBack)
            {
                cargarConfiguracion();
            }

        }



        private void cargarConfiguracion()
        {
            //Titulo 1 --------------------------------------
            TextBox_Titulo1.Text = this.configuracion.Titulo1;
            TextBox_Titulo1Color.Text = this.configuracion.Titulo1Color;
            TextBox_Titulo1_Tamano.Text = this.configuracion.Titulo1Tamano;

            //Titulo 2 --------------------------------------
            TextBox_Titulo2.Text = this.configuracion.Titulo2;
            TextBox_Titulo2Color.Text = this.configuracion.Titulo2Color;
            TextBox_Titulo2_Tamano.Text = this.configuracion.Titulo2Tamano;


            //Titulo 3 --------------------------------------
            TextBox_Titulo3.Text = this.configuracion.Titulo3;
            TextBox_Titulo3Color.Text = this.configuracion.Titulo3Color;
            TextBox_Titulo3_Tamano.Text = this.configuracion.Titulo3Tamano;

            //Titulo Color de Fondo
            TextBox_FondoColor.Text = this.configuracion.TituloColorFondo;

            //imagenes de la master page/portada
            Label_ImagenLogo1.Text = this.configuracion.ImagenLogo1;
            Label_ImagenLogo2.Text = this.configuracion.ImagenLogo2;
            Label_ImagenPortada.Text = this.configuracion.ImagenPortada;

            //servidor de correo smtp --------------------------------------
            CheckBox_HabilitarEnvioEmailAviso.Checked = bool.Parse(this.configuracion.HabilitarEnvioEmailAviso);
            TextBox_ServidorSmtpIp.Text = this.configuracion.ServidorSmtpIp;
            TextBox_EmailRemitente.Text = this.configuracion.EmailRemitente;


            //vista inicial --------------------------------------
            if (this.configuracion.VistaInicial.Equals("tree")) DropDownList_VistaInicial.SelectedIndex = 0;
            else if (this.configuracion.VistaInicial.Equals("icons")) DropDownList_VistaInicial.SelectedIndex = 1;
            else if (this.configuracion.VistaInicial.Equals("details")) DropDownList_VistaInicial.SelectedIndex = 2;

            //Mensaje de Bienvenida --------------------------------------
            CheckBox_HabilitarMensajeBienvenida.Checked = bool.Parse(this.configuracion.HabilitarMensajeBienvenida);
            TextBox_TituloMensajeBienvenida.Text = this.configuracion.TituloMensajeBienvenida;
            TextBox_MensajeBienvenida.Text = this.configuracion.MensajeBienvenida;

        }



        private void guardarConfiguracion()
        {
            //Titulo 1 ---------------------------------------------------
            this.configuracion.Titulo1 = TextBox_Titulo1.Text;
            this.configuracion.Titulo1Color = TextBox_Titulo1Color.Text;
            this.configuracion.Titulo1Tamano = TextBox_Titulo1_Tamano.Text;

            //Titulo 2 ---------------------------------------------------
            this.configuracion.Titulo2 = TextBox_Titulo2.Text;
            this.configuracion.Titulo2Color = TextBox_Titulo2Color.Text;
            this.configuracion.Titulo2Tamano = TextBox_Titulo2_Tamano.Text;

            //Titulo 3 ---------------------------------------------------
            this.configuracion.Titulo3 = TextBox_Titulo3.Text;
            this.configuracion.Titulo3Color = TextBox_Titulo3Color.Text;
            this.configuracion.Titulo3Tamano = TextBox_Titulo3_Tamano.Text;

            //titulo color de fondo ---------------------------------------
            this.configuracion.TituloColorFondo = TextBox_FondoColor.Text;


            //datos del servidor de correo
            this.configuracion.HabilitarEnvioEmailAviso = CheckBox_HabilitarEnvioEmailAviso.Checked.ToString();
            this.configuracion.ServidorSmtpIp = TextBox_ServidorSmtpIp.Text;
            this.configuracion.EmailRemitente = TextBox_EmailRemitente.Text;


            //asignamos la vista inicial
            this.configuracion.VistaInicial = DropDownList_VistaInicial.SelectedValue;


            //Mensaje de Bienvenida --------------------------------------
            this.configuracion.HabilitarMensajeBienvenida = CheckBox_HabilitarMensajeBienvenida.Checked.ToString();
            this.configuracion.TituloMensajeBienvenida = TextBox_TituloMensajeBienvenida.Text;
            this.configuracion.MensajeBienvenida = TextBox_MensajeBienvenida.Text;

            //guardamos los cambios
            this.configuracion.EscribirArchivoConfiguracion();

            //recargamos esta pagina, para visualizar los nuevos cambios---------------------------------
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }


        private void cargarDatosDefault()
        {
            TextBox_Titulo1.Text = this.configuracion.Titulo1Default;
            TextBox_Titulo2.Text = this.configuracion.Titulo2Default;
            TextBox_Titulo3.Text = this.configuracion.Titulo3Default;

            TextBox_Titulo1Color.Text = this.configuracion.Titulo1ColorDefault;
            TextBox_Titulo2Color.Text = this.configuracion.Titulo2ColorDefault;
            TextBox_Titulo3Color.Text = this.configuracion.Titulo3ColorDefault;

            TextBox_Titulo1_Tamano.Text = this.configuracion.Titulo1TamanoDefault;
            TextBox_Titulo2_Tamano.Text = this.configuracion.Titulo2TamanoDefault;
            TextBox_Titulo3_Tamano.Text = this.configuracion.Titulo3TamanoDefault;

            TextBox_FondoColor.Text = this.configuracion.TituloColorFondo;
        }


        private void subirImagenPresentacion(string tipo_imagen)
        {
            //Boolean fileOK = false;
            String base_path = Server.MapPath("~/Images");
            /*
                    string ext = Path.GetExtension(FileUpload_ImagenPortada.FileName).ToLower();
                    if (FileUpload_ImagenPortada.HasFile && (ext.Equals(".jpg") || ext.Equals(".gif") || ext.Equals(".png") || ext.Equals(".bmp") ))
                    {
                        string ruta_guardar_archivo = base_path + "\\portada.jpg";

                        FileUpload_ImagenPortada.PostedFile.SaveAs(ruta_guardar_archivo);

                    }*/

        }




        protected void Button_GuardarCambios_Click(object sender, EventArgs e)
        {
            guardarConfiguracion();
        }
        protected void Button_CargarColoresDefault_Click(object sender, EventArgs e)
        {
            cargarDatosDefault();
        }

        protected void Button_SubirImagenLogo1_Click(object sender, EventArgs e)
        {
            subirImagenPresentacion("logo1");
        }
        protected void Button_SubirImagenLogo2_Click(object sender, EventArgs e)
        {
            subirImagenPresentacion("logo2");
        }
        protected void Button_SubirImagenLogoPortada_Click(object sender, EventArgs e)
        {
            subirImagenPresentacion("portada");
        }



    }
}