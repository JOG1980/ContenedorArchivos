using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Xml;


namespace ContenedorArchivos
{
    public class Configuracion
    {
        private String nombre_archivo_configuracion = "configuracion.xml";

        private String ruta_nombre_archivo_configuracion = "";


        private String titulo1 = "";
        private String titulo1_default = "";
        private String titulo1_color = "";
        private String titulo1_color_default = "";
        private String titulo1_tamano = "";
        private String titulo1_tamano_default = "";
        private String titulo2 = "";
        private String titulo2_default = "";
        private String titulo2_color = "";
        private String titulo2_color_default = "";
        private String titulo2_tamano = "";
        private String titulo2_tamano_default = "";
        private String titulo3 = "";
        private String titulo3_default = "";
        private String titulo3_color = "";
        private String titulo3_color_default = "";
        private String titulo3_tamano = "";
        private String titulo3_tamano_default = "";

        private String imagen_logo1 = "";
        private String imagen_logo2 = "";
        private String imagen_portada = "";

        private String titulo_color_fondo = "";
        private String titulo_color_fondo_default = "";

        private String vista_inicial = "";

        private String servidor_smtp_ip = "";
        private String habilitar_envio_email_aviso = "";


        private String habilitar_mensaje_bienvenida = "";
        private String titulo_mensaje_bienvenida = "";
        private String mensaje_bienvenida = "";

        private String email_remitente = "";


        private string version = "";
        private string fecha_compilacion = "";

        public Configuracion()
        {
            init();
            LeerArchivoConfiguracion();
        }


        private void init()
        {
            this.ruta_nombre_archivo_configuracion = HttpContext.Current.Server.MapPath("~/" + nombre_archivo_configuracion);
        }


        //
        private void LeerArchivoConfiguracion()
        {
            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas
            //respecto del ejecutable!

            xDoc.Load(ruta_nombre_archivo_configuracion);

            XmlNodeList xml_configuracion = xDoc.GetElementsByTagName("configuration");

            //Interfaz grafica------------------------------------------------------------------------------------
            XmlNodeList xml_grupo_master_page = ((XmlElement)xDoc.GetElementsByTagName("configuration")[0]).GetElementsByTagName("master_page");

            XmlNodeList xml_titulo1 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo1");
            XmlNodeList xml_titulo1_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo1_default");
            XmlNodeList xml_titulo1_color = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo1_color");
            XmlNodeList xml_titulo1_color_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo1_color_default");
            XmlNodeList xml_titulo1_tamano = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo1_tamano");
            XmlNodeList xml_titulo1_tamano_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo1_tamano_default");

            XmlNodeList xml_titulo2 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo2");
            XmlNodeList xml_titulo2_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo2_default");
            XmlNodeList xml_titulo2_color = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo2_color");
            XmlNodeList xml_titulo2_color_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo2_color_default");
            XmlNodeList xml_titulo2_tamano = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo2_tamano");
            XmlNodeList xml_titulo2_tamano_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo2_tamano_default");

            XmlNodeList xml_titulo3 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo3");
            XmlNodeList xml_titulo3_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo3_default");
            XmlNodeList xml_titulo3_color = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo3_color");
            XmlNodeList xml_titulo3_color_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo3_color_default");
            XmlNodeList xml_titulo3_tamano = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo3_tamano");
            XmlNodeList xml_titulo3_tamano_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo3_tamano_default");

            XmlNodeList xml_titulo_color_fondo = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo_fondo_color");
            XmlNodeList xml_titulo_color_fondo_default = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo_fondo_color_default");

            XmlNodeList xml_imagen_logo1 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("imagen_logo1");
            XmlNodeList xml_imagen_logo2 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("imagen_logo2");
            XmlNodeList xml_imagen_portada = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("imagen_portada");

            //grupo de vista ---------------------------------------------------------------------
            XmlNodeList xml_grupo_vista = ((XmlElement)xDoc.GetElementsByTagName("configuration")[0]).GetElementsByTagName("vista");
            XmlNodeList xml_vista_inicial = ((XmlElement)xml_grupo_vista[0]).GetElementsByTagName("vista_inicial");
            XmlNodeList xml_habilitar_mensaje_bienvenida = ((XmlElement)xml_grupo_vista[0]).GetElementsByTagName("habilitar_mensaje_bienvenida");
            XmlNodeList xml_titulo_mensaje_bienvenida = ((XmlElement)xml_grupo_vista[0]).GetElementsByTagName("titulo_mensaje_bienvenida");
            XmlNodeList xml_mensaje_bienvenida = ((XmlElement)xml_grupo_vista[0]).GetElementsByTagName("mensaje_bienvenida");

            //grupo de email ----------------------------------------------------------------------
            XmlNodeList xml_grupo_email = ((XmlElement)xDoc.GetElementsByTagName("configuration")[0]).GetElementsByTagName("email");
            XmlNodeList xml_servidor_smtp_ip = ((XmlElement)xml_grupo_email[0]).GetElementsByTagName("servidor_smtp_ip");
            XmlNodeList xml_habilitar_envio_email_aviso = ((XmlElement)xml_grupo_email[0]).GetElementsByTagName("habilitar_envio_email_aviso");
            XmlNodeList xml_email_remitente = ((XmlElement)xml_grupo_email[0]).GetElementsByTagName("email_remitente");

            //grupo de acerca de ------------------------------------------------------------------
            XmlNodeList xml_grupo_acerca_de = ((XmlElement)xDoc.GetElementsByTagName("configuration")[0]).GetElementsByTagName("acerca_de");
            XmlNodeList xml_version = ((XmlElement)xml_grupo_acerca_de[0]).GetElementsByTagName("version");
            XmlNodeList xml_fecha_compilacion = ((XmlElement)xml_grupo_acerca_de[0]).GetElementsByTagName("fecha_compilacion");



            this.titulo1 = xml_titulo1[0].InnerText;
            this.titulo1_default = xml_titulo1_default[0].InnerText;
            this.titulo1_color = xml_titulo1_color[0].InnerText;
            this.titulo1_color_default = xml_titulo1_color_default[0].InnerText;
            this.titulo1_tamano = xml_titulo1_tamano[0].InnerText;
            this.titulo1_tamano_default = xml_titulo1_tamano_default[0].InnerText;

            this.titulo2 = xml_titulo2[0].InnerText;
            this.titulo2_default = xml_titulo2_default[0].InnerText;
            this.titulo2_color = xml_titulo2_color[0].InnerText;
            this.titulo2_color_default = xml_titulo2_color_default[0].InnerText;
            this.titulo2_tamano = xml_titulo2_tamano[0].InnerText;
            this.titulo2_tamano_default = xml_titulo2_tamano_default[0].InnerText;

            this.titulo3 = xml_titulo3[0].InnerText;
            this.titulo3_default = xml_titulo3_default[0].InnerText;
            this.titulo3_color = xml_titulo3_color[0].InnerText;
            this.titulo3_color_default = xml_titulo3_color_default[0].InnerText;
            this.titulo3_tamano = xml_titulo3_tamano[0].InnerText;
            this.titulo3_tamano_default = xml_titulo3_tamano_default[0].InnerText;

            this.titulo_color_fondo = xml_titulo_color_fondo[0].InnerText;
            this.titulo_color_fondo_default = xml_titulo_color_fondo_default[0].InnerText;

            this.imagen_logo1 = xml_imagen_logo1[0].InnerText;
            this.imagen_logo2 = xml_imagen_logo2[0].InnerText;
            this.imagen_portada = xml_imagen_portada[0].InnerText;


            this.vista_inicial = xml_vista_inicial[0].InnerText;

            this.habilitar_mensaje_bienvenida = xml_habilitar_mensaje_bienvenida[0].InnerText;
            this.titulo_mensaje_bienvenida = xml_titulo_mensaje_bienvenida[0].InnerText;
            this.mensaje_bienvenida = xml_mensaje_bienvenida[0].InnerText;


            this.servidor_smtp_ip = xml_servidor_smtp_ip[0].InnerText;
            this.habilitar_envio_email_aviso = xml_habilitar_envio_email_aviso[0].InnerText;
            this.email_remitente = xml_email_remitente[0].InnerText;

            this.version = xml_version[0].InnerText;
            this.fecha_compilacion = xml_fecha_compilacion[0].InnerText;

        }



        public void EscribirArchivoConfiguracion()
        {
            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas
            //respecto del ejecutable!

            xDoc.Load(ruta_nombre_archivo_configuracion);

            XmlNodeList xml_configuracion = xDoc.GetElementsByTagName("configuration");

            //Interfaz grafica------------------------------------------------------------------------------------
            XmlNodeList xml_grupo_master_page = ((XmlElement)xDoc.GetElementsByTagName("configuration")[0]).GetElementsByTagName("master_page");

            XmlNodeList xml_titulo1 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo1");
            XmlNodeList xml_titulo1_color = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo1_color");
            XmlNodeList xml_titulo1_tamano = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo1_tamano");

            XmlNodeList xml_titulo2 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo2");
            XmlNodeList xml_titulo2_color = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo2_color");
            XmlNodeList xml_titulo2_tamano = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo2_tamano");

            XmlNodeList xml_titulo3 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo3");
            XmlNodeList xml_titulo3_color = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo3_color");
            XmlNodeList xml_titulo3_tamano = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo3_tamano");

            XmlNodeList xml_titulo_fondo_color = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("titulo_fondo_color");

            XmlNodeList xml_imagen_logo1 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("imagen_logo1");
            XmlNodeList xml_imagen_logo2 = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("imagen_logo2");
            XmlNodeList xml_imagen_portada = ((XmlElement)xml_grupo_master_page[0]).GetElementsByTagName("imagen_portada");


            //vista -------------------------------------------------------------------------------------------------
            XmlNodeList xml_grupo_vista = ((XmlElement)xDoc.GetElementsByTagName("configuration")[0]).GetElementsByTagName("vista");
            XmlNodeList xml_vista_inicial = ((XmlElement)xml_grupo_vista[0]).GetElementsByTagName("vista_inicial");
            XmlNodeList xml_habilitar_mensaje_bienvenida = ((XmlElement)xml_grupo_vista[0]).GetElementsByTagName("habilitar_mensaje_bienvenida");
            XmlNodeList xml_titulo_mensaje_bienvenida = ((XmlElement)xml_grupo_vista[0]).GetElementsByTagName("titulo_mensaje_bienvenida");
            XmlNodeList xml_mensaje_bienvenida = ((XmlElement)xml_grupo_vista[0]).GetElementsByTagName("mensaje_bienvenida");

            XmlNodeList xml_grupo_email = ((XmlElement)xDoc.GetElementsByTagName("configuration")[0]).GetElementsByTagName("email");
            XmlNodeList xml_servidor_smtp_ip = ((XmlElement)xml_grupo_email[0]).GetElementsByTagName("servidor_smtp_ip");
            XmlNodeList xml_habilitar_envio_email_aviso = ((XmlElement)xml_grupo_email[0]).GetElementsByTagName("habilitar_envio_email_aviso");
            XmlNodeList xml_email_remitente = ((XmlElement)xml_grupo_email[0]).GetElementsByTagName("email_remitente");


            XmlNodeList xml_grupo_acerca_de = ((XmlElement)xDoc.GetElementsByTagName("configuration")[0]).GetElementsByTagName("acerca_de");
            XmlNodeList xml_version = ((XmlElement)xml_grupo_acerca_de[0]).GetElementsByTagName("version");
            XmlNodeList xml_fecha_compilacion = ((XmlElement)xml_grupo_acerca_de[0]).GetElementsByTagName("fecha_compilacion");

            xml_titulo1[0].InnerText = this.titulo1;
            xml_titulo1_color[0].InnerText = this.titulo1_color;
            xml_titulo1_tamano[0].InnerText = this.titulo1_tamano;

            xml_titulo2[0].InnerText = this.titulo2;
            xml_titulo2_color[0].InnerText = this.titulo2_color;
            xml_titulo2_tamano[0].InnerText = this.titulo2_tamano;

            xml_titulo3[0].InnerText = this.titulo3;
            xml_titulo3_color[0].InnerText = this.titulo3_color;
            xml_titulo3_tamano[0].InnerText = this.titulo3_tamano;

            xml_titulo_fondo_color[0].InnerText = this.titulo_color_fondo;

            xml_imagen_logo1[0].InnerText = this.imagen_logo1;
            xml_imagen_logo2[0].InnerText = this.imagen_logo2;
            xml_imagen_portada[0].InnerText = this.imagen_portada;

            xml_vista_inicial[0].InnerText = this.vista_inicial;


            xml_habilitar_mensaje_bienvenida[0].InnerText = this.habilitar_mensaje_bienvenida;
            xml_titulo_mensaje_bienvenida[0].InnerText = this.titulo_mensaje_bienvenida;
            xml_mensaje_bienvenida[0].InnerText = this.mensaje_bienvenida;


            xml_servidor_smtp_ip[0].InnerText = this.servidor_smtp_ip;
            xml_habilitar_envio_email_aviso[0].InnerText = this.habilitar_envio_email_aviso;
            xml_email_remitente[0].InnerText = this.email_remitente;


            xml_version[0].InnerText = this.version;
            xml_fecha_compilacion[0].InnerText = this.fecha_compilacion;

            xDoc.Save(ruta_nombre_archivo_configuracion);

        }



        //Titulo del sistema--------------------------------------------------


        public string Titulo1
        {
            get { return titulo1; }
            set { titulo1 = value; }
        }

        public string Titulo1Default
        {
            get { return titulo1_default; }
        }

        public string Titulo1Color
        {
            get { return titulo1_color; }
            set { titulo1_color = value; }
        }

        public string Titulo1ColorDefault
        {
            get { return titulo1_color_default; }
        }

        public string Titulo1Tamano
        {
            get { return titulo1_tamano; }
            set { titulo1_tamano = value; }
        }

        public string Titulo1TamanoDefault
        {
            get { return titulo1_tamano_default; }
        }



        public string Titulo2
        {
            get { return titulo2; }
            set { titulo2 = value; }
        }

        public string Titulo2Default
        {
            get { return titulo2_default; }
        }

        public string Titulo2Color
        {
            get { return titulo2_color; }
            set { titulo2_color = value; }
        }

        public string Titulo2ColorDefault
        {
            get { return titulo2_color_default; }
        }

        public string Titulo2Tamano
        {
            get { return titulo2_tamano; }
            set { titulo2_tamano = value; }
        }

        public string Titulo2TamanoDefault
        {
            get { return titulo2_tamano_default; }
        }


        public string Titulo3
        {
            get { return titulo3; }
            set { titulo3 = value; }
        }

        public string Titulo3Default
        {
            get { return titulo3_default; }
        }

        public string Titulo3Color
        {
            get { return titulo3_color; }
            set { titulo3_color = value; }
        }

        public string Titulo3ColorDefault
        {
            get { return titulo3_color_default; }
        }

        public string Titulo3Tamano
        {
            get { return titulo3_tamano; }
            set { titulo3_tamano = value; }
        }

        public string Titulo3TamanoDefault
        {
            get { return titulo3_tamano_default; }
        }

        public string TituloColorFondo
        {
            get { return titulo_color_fondo; }
            set { titulo_color_fondo = value; }
        }


        public string ImagenLogo1
        {
            get { return imagen_logo1; }
            set { imagen_logo1 = value; }
        }

        public string ImagenLogo2
        {
            get { return imagen_logo2; }
            set { imagen_logo2 = value; }
        }

        public string ImagenPortada
        {
            get { return imagen_portada; }
            set { imagen_portada = value; }
        }


        //vista inicial -----------------------
        public string VistaInicial
        {
            get { return vista_inicial; }
            set { vista_inicial = value; }
        }

        //Mensaje de bienvenida -----------------------
        public string HabilitarMensajeBienvenida
        {
            get { return habilitar_mensaje_bienvenida; }
            set { habilitar_mensaje_bienvenida = value; }
        }

        public string TituloMensajeBienvenida
        {
            get { return titulo_mensaje_bienvenida; }
            set { titulo_mensaje_bienvenida = value; }
        }

        public string MensajeBienvenida
        {
            get { return mensaje_bienvenida; }
            set { mensaje_bienvenida = value; }
        }


        //servidor de correo -----------------------

        public string ServidorSmtpIp
        {
            get { return servidor_smtp_ip; }
            set { servidor_smtp_ip = value; }
        }

        public string HabilitarEnvioEmailAviso
        {
            get { return habilitar_envio_email_aviso; }
            set { habilitar_envio_email_aviso = value; }
        }

        public string EmailRemitente
        {
            get { return email_remitente; }
            set { email_remitente = value; }
        }


        public string Version
        {
            get { return version; }
            set { version = value; }
        }


        public string FechaCompilacion
        {
            get { return fecha_compilacion; }
            set { fecha_compilacion = value; }
        }



    }
}