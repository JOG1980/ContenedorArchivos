using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Diagnostics;
using System.Web.Security;
using System.Collections;


namespace ContenedorArchivos
{
    public class EnviarCorreo
    {

        private string autor = "";
        private string email = "";
        private bool habilitar_envio_email_aviso = false;

        private Configuracion configuracion = null;
        private EmailSMTP email_smtp = null;
        private ControlDB control_db = null;



        private List<String> lista_emails_destinatarios = null;
        private string lista_envio = "";


        public EnviarCorreo(string usuario)
        {

            this.configuracion = new Configuracion();
            this.email_smtp = new EmailSMTP();
            this.control_db = new ControlDB();

            this.habilitar_envio_email_aviso = bool.Parse(this.configuracion.HabilitarEnvioEmailAviso);

            this.lista_emails_destinatarios = new List<String>();

            //obtenemos los datos del autor
            obtenerDatosAutor(usuario);

            //obtenemos la lista de destinatarios
            obtenerListaDestinatarios();
        }


        //obtenemos los datos del autor (la persona que esta logueda haciendo los movimientos en el sistema del contenedor).
        public void obtenerDatosAutor(string usuario)
        {
            MembershipUser ms_user = Membership.GetUser(usuario);
            this.email = ms_user.Email;



            string nombre = "";
            string apellido_paterno = "";
            string apellido_materno = "";
            string enviar_email = "";

            this.control_db.obtenerDatosUsuario(usuario, ref  nombre, ref apellido_paterno, ref apellido_materno, ref enviar_email);

            //definimos el nombre del autor del archivo (quien esta logueado en esta session)
            this.autor = nombre + " " + apellido_paterno + " " + apellido_materno;
            if (!(autor.Trim().Length > 0)) autor = usuario; //si no tiene nombre entonces icupamos el nickname

        }


        //con esta funcion obtenemos la lista de destinatarios
        public void obtenerListaDestinatarios()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            IEnumerator lista_usuarios = users.GetEnumerator();

            while (lista_usuarios.MoveNext())
            {

                try
                {
                    //optenemos el usuario actual
                    string d_usuario = lista_usuarios.Current.ToString();

                    MembershipUser d_ms_user = Membership.GetUser(d_usuario);

                    string d_email = d_ms_user.Email;

                    string d_nombre = "";
                    string d_apellido_paterno = "";
                    string d_apellido_materno = "";
                    string d_enviar_email = "";

                    this.control_db.obtenerDatosUsuario(d_usuario, ref d_nombre, ref d_apellido_paterno, ref d_apellido_materno, ref d_enviar_email);

                    //si esta activado el envio de notificaciones
                    if (bool.Parse(d_enviar_email))
                    {
                        this.lista_emails_destinatarios.Add(d_email);
                        this.lista_envio += d_email + ",";
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine("ERROR: " + e.ToString());
                }
            }

            if (this.lista_envio.Length > 0)
            {
                this.lista_envio = this.lista_envio.Substring(0, this.lista_envio.Length - 1);
            }
        }




        public void enviarCorreoConfirmacion(string tipo, string nombre_archivo, string nuevo_nombre_archivo, string ruta_archivo)
        {
            if (this.habilitar_envio_email_aviso)
            {
                //ajustamos la ruta
                ruta_archivo = ruta_archivo + "/";

                string web_path = HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/Protegido/Contenedor" + ruta_archivo + nombre_archivo;


                web_path = web_path.Replace("//", "/");
                web_path = "http://" + web_path;

                //armamos <a href>
                web_path = "<a href='" + web_path + "'>" + web_path + "</a>";

                string servidor_smtp_ip = this.configuracion.ServidorSmtpIp;
                string remitente = this.configuracion.EmailRemitente;
                string subject = "";
                string msg = "";


                mensajeEmail(ref subject, ref msg, tipo, nombre_archivo, nuevo_nombre_archivo, ruta_archivo, autor, email, web_path);

                this.email_smtp.enviarCorreo(servidor_smtp_ip, remitente, this.lista_envio, "", subject, msg);

            }
        }





        public void enviarCorreoConfirmacionSubirNuevoArchivo(string nombre_archivo, string ruta_archivo)
        {
            enviarCorreoConfirmacion("NUEVO_ARCHIVO", nombre_archivo, "", ruta_archivo);
        }//end function

        public void enviarCorreoConfirmacionSubirActualizacionArchivo(string nombre_archivo, string ruta_archivo)
        {
            enviarCorreoConfirmacion("ACTUALIZAR_ARCHIVO", nombre_archivo, "", ruta_archivo);
        }//end function

        public void enviarCorreoConfirmacionRenombrarArchivo(string ruta_archivos, string nombre_anterior_archivo, string nombre_archivo)
        {
            enviarCorreoConfirmacion("RENOMBRAR_ARCHIVO", nombre_archivo, nombre_anterior_archivo, ruta_archivos);

        }//end function

        public void enviarCorreoConfirmacionEliminarArchivo(string ruta_archivos, string nombre_archivo)
        {
            enviarCorreoConfirmacion("ELIMINAR_ARCHIVO", nombre_archivo, "", ruta_archivos);
        }//end function

        public void enviarCorreoConfirmacionCrearCarpeta(string ruta_carpeta, string nombre_carpeta)
        {
            enviarCorreoConfirmacion("CREAR_CARPETA", nombre_carpeta, "", ruta_carpeta);
        }//end function

        public void enviarCorreoConfirmacionRenombrarCarpeta(string ruta, string nombre_anterior_carpeta, string nombre_carpeta)
        {
            enviarCorreoConfirmacion("RENOMBRAR_CARPETA", nombre_carpeta, nombre_anterior_carpeta, ruta);
        }//end function

        public void enviarCorreoConfirmacionEliminarCarpeta(string ruta_carpeta, string nombre_carpeta)
        {
            enviarCorreoConfirmacion("ELIMINAR_CARPETA", nombre_carpeta, "", ruta_carpeta);
        }//end function



        private string mensajeEmail(ref string subject, ref string msg, string tipo, string nombre_elemento, string nombre_anterior_elemento, string ruta_elemento, string autor, string email, string web_path)
        {


            if (tipo.Equals("NUEVO_ARCHIVO"))
            {
                subject = "Se ha subido un nuevo archivo en el Contenedor \"" + this.configuracion.Titulo3 + "\"";

                msg = "<span style='font-weight: bold;'>" +
                           "Se ha subido un nuevo archivo en el Contenedor <span style='color:#2E64FE;'>\"" + this.configuracion.Titulo3 + "\"</span>" +
                           ", los datos del archivo son los siguientes: </span> <br /><br />" +
                           "Nombre del archivo: <span style='color:#2E64FE; font-weight: bold;'>" + nombre_elemento + "</span><br />" +
                           "Ubicación en el contenedor: <span style='color:#2E64FE; font-weight: bold;'>" + ruta_elemento + "</span><br />" +
                           "Enlace directo a: <span style='color:#2E64FE; font-weight: bold;'>" + web_path + "</span><br />" +
                           "Subido por: <span style='color:#2E64FE; font-weight: bold;'>" + autor + " &lt;" + email + " &gt;</span>";

            }
            else if (tipo.Equals("ACTUALIZAR_ARCHIVO"))
            {
                subject = "Se ha actualizado un archivo en el Contenedor \"" + this.configuracion.Titulo3 + "\"";

                msg = "<span style='font-weight: bold;'>" +
                                "Se ha actualizado un archivo en el Contenedor <span style='color:#2E64FE; font-weight: bold;'>\"" + this.configuracion.Titulo3 + "\"</span>" +
                                ", los datos son los siguientes: </span> <br /><br />" +
                                "Nombre del archivo: <span style='color:#2E64FE; font-weight: bold;'>" + nombre_elemento + "</span><br />" +
                                "Ubicación en el contenedor: <span style='color:#2E64FE; font-weight: bold;'>" + ruta_elemento + "</span><br />" +
                                "Enlace directo a: <span style='color:#2E64FE; font-weight: bold;'>" + web_path + "</span><br />" +
                                "Actualizado por: <span style='color:#2E64FE; font-weight: bold;'>" + autor + " &lt;" + email + " &gt;</span>";

            }
            else if (tipo.Equals("RENOMBRAR_ARCHIVO"))
            {
                subject = "Se ha renombrado un archivo en el Contenedor  \"" + this.configuracion.Titulo3 + "\"";

                msg = "<span style='font-weight: bold;'>" +
                             "Se ha renombrado un archivo en el Contenedor <span style='color:#2E64FE; font-weight: bold;'>\"" + this.configuracion.Titulo3 + "\"</span>" +
                             ", los datos son los siguientes: </span> <br /><br />" +
                             "Nombre anterior del archivo: <span style='color:#2E64FE; font-weight: bold;'>" + nombre_anterior_elemento + "</span><br />" +
                             "Nombre actual del archivo: <span style='color:#2E64FE; font-weight: bold;'>" + nombre_elemento + "</span><br />" +
                             "Ubicación en el contenedor: <span style='color:#2E64FE; font-weight: bold;'>" + ruta_elemento + "</span><br />" +
                             "Enlace directo a: <span style='color:#2E64FE; font-weight: bold;'>" + web_path + "</span><br />" +
                             "Renombrado por: <span style='color:#2E64FE; font-weight: bold;'>" + autor + " &lt;" + email + " &gt;</span>";

            }
            else if (tipo.Equals("ELIMINAR_ARCHIVO"))
            {
                subject = "Se ha eliminado un archivo en el Contenedor \"" + this.configuracion.Titulo3 + "\"";


                msg = "<span style='font-weight: bold;'>" +
                             "Se ha eliminado un archivo en el Contenedor <span style='color:#2E64FE; font-weight: bold;'>\"" + this.configuracion.Titulo3 + "\"</span>" +
                             ", los datos son los siguientes: </span> <br /><br />" +
                             "Nombre del archivo: <span style='color:#2E64FE; font-weight: bold;'>" + nombre_elemento + "</span><br />" +
                             "Ubicación en el contenedor: <span style='color:#2E64FE; font-weight: bold;'>" + ruta_elemento + "</span><br />" +
                             "Eliminado por: <span style='color:#2E64FE; font-weight: bold;'>" + autor + " &lt;" + email + " &gt;</span>";
            }
            else if (tipo.Equals("CREAR_CARPETA"))
            {
                subject = "Se ha creado una carpeta en el Contenedor \"" + this.configuracion.Titulo3 + "\"";
                msg = "<span style='font-weight: bold;'>" +
                             "Se ha creado una carpeta en el Contenedor <span style='color:#2E64FE; font-weight: bold;'>\"" + this.configuracion.Titulo3 + "\"</span>" +
                             ", los datos son los siguientes: </span> <br /><br />" +
                             "Nombre de la carpeta: <span style='color:#2E64FE; font-weight: bold;'>" + nombre_elemento + "</span><br />" +
                             "Ubicación en el contenedor: <span style='color:#2E64FE; font-weight: bold;'>" + ruta_elemento + "</span><br />" +
                             "Enlace directo a: <span style='color:#2E64FE; font-weight: bold;'>" + web_path + "</span><br />" +
                             "Creada por: <span style='color:#2E64FE; font-weight: bold;'>" + autor + " &lt;" + email + " &gt;</span>";
            }
            else if (tipo.Equals("ELIMINAR_CARPETA"))
            {
                subject = "Se ha eliminado una carpeta en el Contenedor \"" + this.configuracion.Titulo3 + "\"";

                msg = "<span style='font-weight: bold;'>" +
                           "Se ha eliminado una carpeta en el Contenedor <span style='color:#2E64FE; font-weight: bold;'>\"" + this.configuracion.Titulo3 + "\"</span>" +
                           ", los datos son los siguientes: </span> <br /><br />" +
                           "Nombre de la carpeta: <span style='color:#2E64FE; font-weight: bold;'>" + nombre_elemento + "</span><br />" +
                           "Ubicación en el contenedor: <span style='color:#2E64FE; font-weight: bold;'>" + ruta_elemento + "</span><br />" +
                           "Eliminada por: <span style='color:#2E64FE; font-weight: bold;'>" + autor + " &lt;" + email + " &gt;</span>";
            }
            else if (tipo.Equals("RENOMBRAR_CARPETA"))
            {
                subject = "Se ha renombrado una carpeta en el Contenedor \"" + this.configuracion.Titulo3 + "\"";

                msg = "<span style='font-weight: bold;'>" +
                             "Se ha renombrado una carpeta en el Contenedor <span style='color:#2E64FE; font-weight: bold;'>\"" + this.configuracion.Titulo3 + "\"</span>" +
                             ", los datos son los siguientes: </span> <br /><br />" +
                             "Nombre anterior de la carpeta: <span style='color:#2E64FE; font-weight: bold;'>" + nombre_anterior_elemento + "</span><br />" +
                             "Nombre actual de la carpeta: <span style='color:#2E64FE; font-weight: bold;'>" + nombre_elemento + "</span><br />" +
                             "Ubicación en el contenedor: <span style='color:#2E64FE; font-weight: bold;'>" + ruta_elemento + "</span><br />" +
                             "Enlace directo a: <span style='color:#2E64FE; font-weight: bold;'>" + web_path + "</span><br />" +
                             "Renombrada por: <span style='color:#2E64FE; font-weight: bold;'>" + autor + " &lt;" + email + " &gt;</span>";

            }

            return msg;
        }

    }
}