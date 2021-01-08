using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Diagnostics;
using System.Web.Security;
using System.Collections;
using System.Xml;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Net;

namespace ContenedorArchivos.Protegido
{
    public partial class Contenido : System.Web.UI.Page
    {
       

        public class TreeViewExpandStatus
        {
            public String name { get; set; }
            public String status { get; set; }
        }

        bool firstTime = false;

        string tipo_vista = "icons";
        string habilitar_mensaje_bienvenida = "";
        string titulo_mensaje_bienvenida = "";
        string mensaje_bienvenida = "";

        int elementos_id = 1;

        String rol = "";

        String ruta_principal = "";
        String ruta_relativa_base = "Protegido\\Contenedor";

        DescripcionArchivosCarpetas descripcion_archivos_carpetas = null;

        Dictionary<string, string> treeviewstatus_expand = null;

        MedirTiempos mt = new MedirTiempos("mt");

        Configuracion configuracion = null;

        EmailSMTP email_smtp = null;
        ControlDB control_db = null;
        EnviarCorreo enviar_correo = null;


        string servidor_smtp_ip = "";
        bool habilitar_envio_email_aviso = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            //inicializamos algunos elementos
            init();


            if (!IsPostBack)
            {
                repaintTreeView();
            }

            initTreeViewStatus();
            validateFirstTime();
        }



        //esta funcion se inicializan algunos valores ------------------------------------------------
        private void init()
        {

            string[] roles = Roles.GetRolesForUser(this.User.Identity.Name);
            this.rol = roles[0];
            //Debug.WriteLine("rol: "+rol);

            HiddenField_TipoRol.Value = this.rol;

            this.configuracion = new Configuracion();
            this.email_smtp = new EmailSMTP();
            this.control_db = new ControlDB();

            this.enviar_correo = new EnviarCorreo(this.User.Identity.Name);

            this.elementos_id = 1;

            this.ruta_principal = Server.MapPath("~/");


            this.habilitar_envio_email_aviso = bool.Parse(configuracion.HabilitarEnvioEmailAviso);
            this.servidor_smtp_ip = configuracion.ServidorSmtpIp;

            cargarDescripcion();

            listarExtenciones();


        }

        private void validateFirstTime()
        {
            if (Session["FirstTime"] == null)
            {
                firstTime = true;
                Session["FirstTime"] = true;
            }

            firstTime = (bool)Session["FirstTime"];


            if (firstTime)
            {
                Session["FirstTime"] = false;

                this.tipo_vista = this.configuracion.VistaInicial;
                HiddenField_TipoVista.Value = this.tipo_vista;

                //mensaje de bienvenida ----------------------
                this.habilitar_mensaje_bienvenida = this.configuracion.HabilitarMensajeBienvenida;
                HiddenField_HabilitarMensajeBienvenida.Value = this.habilitar_mensaje_bienvenida;

                this.titulo_mensaje_bienvenida = this.configuracion.TituloMensajeBienvenida;
                HiddenField_TituloMensajeBienvenida.Value = this.titulo_mensaje_bienvenida;

                this.mensaje_bienvenida = this.configuracion.MensajeBienvenida;
                Label_MensajeBienvenida.Text = this.mensaje_bienvenida;


            }

            HiddenField_FirstTime.Value = firstTime.ToString();
        }


        //esta funcion inicializa algunas variables solo la primera ves de la carga ------------------
        private void initFirstTime()
        {


        }

        private void initNoFirstTime()
        {
            //mensaje de bienvenida ----------------------
            HiddenField_FirstTime.Value = "False";
        }

        //repintamos el contenido del treeview -----------------------------------------------
        private void repaintTreeView()
        {

            mt.start();
            removerTodoContenido();
            cargarDescripcion();
            listarTodoContenido();
            mt.end();
        }

        private void cargarDescripcion()
        {
            this.descripcion_archivos_carpetas = new DescripcionArchivosCarpetas();
            Session["descripcion_archivos_carpetas"] = this.descripcion_archivos_carpetas;
        }


        private void initTreeViewStatus()
        {

            if (Session["treeviewstatus_expand"] != null)
            {
                this.treeviewstatus_expand = new Dictionary<string, string>();

                string s_treeviewstatus_expand = (string)Session["treeviewstatus_expand"];

                JavaScriptSerializer js = new JavaScriptSerializer();
                TreeViewExpandStatus[] elem = js.Deserialize<TreeViewExpandStatus[]>(s_treeviewstatus_expand);
                for (int i = 0; i < elem.Length; i++)
                {
                    this.treeviewstatus_expand.Add(elem[i].name, elem[i].status);
                }
            }


        }

        private string obtenerTreeViewExpandStatus(string name)
        {
            string res = "E";
            if (this.treeviewstatus_expand != null)
            {
                if (this.treeviewstatus_expand.ContainsKey(name))
                {
                    res = this.treeviewstatus_expand[name];
                }

            }

            return res;
        }


        //esta es la funcion principal del listado y creacion de treeview -----------------------------
        private void listarTodoContenido()
        {
            TreeNode tree_node = new TreeNode();
            tree_node.Text = "Archivos";
            tree_node.Target = "";

            //tree_node.NavigateUrl = "javascript:mostrarDialogoOpcionesCarpeta|" + this.elementos_id + "||raiz";
            if (this.rol.Equals("Admin") || this.rol.Equals("SuperUser"))
            {
                tree_node.NavigateUrl = "javascript:mostrarDialogoOpcionesCarpeta|" + this.elementos_id + "||raiz";
            }
            else
            {
                tree_node.NavigateUrl = "";
            }


            tree_node.Expand();

            this.elementos_id++;
            listarContenidoEnTreeView("", tree_node);
            TreeView1.Nodes.Add(tree_node);

        }





        //Esta es la funcion recursiva del armado del treeview -----------------------------------------
        private void listarContenidoEnTreeView(String ruta_relativa_actual, TreeNode tree_node)
        {
            string ruta_actual = ruta_principal + ruta_relativa_base + ruta_relativa_actual;

            //listamos todos los directorios----------------------
            DirectoryInfo dir_info = new DirectoryInfo(ruta_actual);


            if (dir_info.Exists)
            {

                dir_info.Refresh();
                try
                {
                    DirectoryInfo[] lista_directorios = dir_info.GetDirectories();
                    for (int i = 0; i < lista_directorios.Length; i++)
                    {

                        String nueva_ruta_relativa_actual = ruta_relativa_actual + "\\" + lista_directorios[i].Name;
                        String nueva_ruta_web_actual = ruta_relativa_actual + "\\" + lista_directorios[i].Name;
                        nueva_ruta_web_actual = nueva_ruta_web_actual.Replace(@"\", "/");

                        //obtenemos los datos del archivo que estan guardados en el xml
                        //String[] datos_carpeta = this.descripcion_archivos_carpetas.obtenerDescripcion(nueva_ruta_relativa_actual);
                        String[] datos_carpeta = this.descripcion_archivos_carpetas.obtenerDescripcionCarpetaVar(nueva_ruta_relativa_actual);


                        TreeNode tree_node_hijo = new TreeNode();

                        if (this.rol.Equals("Admin") || this.rol.Equals("SuperUser"))
                        {
                            //tree_node_hijo.NavigateUrl = "javascript:mostrarDialogoOpcionesCarpeta('" + nueva_ruta_web_actual + "')";
                            tree_node_hijo.NavigateUrl = "javascript:mostrarDialogoOpcionesCarpeta|" + this.elementos_id + "|" + nueva_ruta_web_actual + "|" + datos_carpeta[1];
                        }
                        else
                        {
                            tree_node_hijo.NavigateUrl = "";
                        }


                        tree_node_hijo.ImageUrl = "~/Images/IconPack1/16x16/folder.png";

                        tree_node_hijo.Text = lista_directorios[i].Name;
                        tree_node_hijo.Target = "";

                        tree_node_hijo.ToolTip = datos_carpeta[0];

                        string expand = obtenerTreeViewExpandStatus(nueva_ruta_web_actual);
                        if (expand.Equals("C"))
                            tree_node_hijo.Expand();
                        else
                            tree_node_hijo.Collapse();

                        this.elementos_id++;

                        tree_node.ChildNodes.Add(tree_node_hijo);

                        listarContenidoEnTreeView(nueva_ruta_relativa_actual, tree_node_hijo);



                    }

                    if (this.tipo_vista.Equals("tree"))
                    {
                        FileInfo[] lista_archivos = dir_info.GetFiles();

                        for (int i = 0; i < lista_archivos.Length; i++)
                        {


                            /*string cpath = HttpContext.Current.Request.Url.ToString();
                            string ruta_descarga = "..\\" + ruta_relativa_base +  ruta_relativa_actual + "\\" + lista_archivos[i].Name;
                            ruta_descarga = ruta_descarga.Replace(@"\", "/");*/

                            String nueva_ruta_web_actual = ruta_relativa_actual + "\\" + lista_archivos[i].Name;
                            nueva_ruta_web_actual = nueva_ruta_web_actual.Replace(@"\", "/");

                            //obtenemos los datos del archivo que estan guardados en el xml
                            //String[] datos_archivo = this.descripcion_archivos_carpetas.obtenerDescripcion(lista_archivos[i].Name, ruta_relativa_actual);
                            String[] datos_archivo = this.descripcion_archivos_carpetas.obtenerDescripcionArchivoVar(lista_archivos[i].Name, ruta_relativa_actual);

                            TreeNode tree_node_hijo = new TreeNode();

                            if (this.rol.Equals("Admin") || this.rol.Equals("SuperUser"))
                            {
                                tree_node_hijo.NavigateUrl = "javascript:mostrarDialogoOpcionesArchivo|" + this.elementos_id + "|" + nueva_ruta_web_actual + "|" + lista_archivos[i].Name + "|" + datos_archivo[1];
                            }
                            else
                            {
                                string ruta_descarga = "..\\" + ruta_relativa_base + ruta_relativa_actual + "\\" + lista_archivos[i].Name;
                                ruta_descarga = ruta_descarga.Replace(@"\", "/");
                                tree_node_hijo.NavigateUrl = ruta_descarga;
                            }

                            tree_node_hijo.Text = lista_archivos[i].Name;

                            tree_node_hijo.ImageUrl = "~/Images/IconPack1/16x16/page.png";
                            tree_node_hijo.Target = "";

                            //tree_node_hijo.ToolTip = datos_archivo[1] + "\n"+datos_archivo[0]; 
                            tree_node_hijo.ToolTip = datos_archivo[0];



                            tree_node.ChildNodes.Add(tree_node_hijo);

                            this.elementos_id++;
                        }
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine("ERROR:" + e.ToString());
                }
            }
        }


        private void removerTodoContenido()
        {


            for (int i = TreeView1.Nodes.Count - 1; i >= 0; i--)
            {
                removerContenidoEnTreeView(TreeView1.Nodes[i]);
                TreeView1.Nodes.RemoveAt(i);
            }

        }

        private void removerContenidoEnTreeView(TreeNode tree_node)
        {
            if (tree_node.ChildNodes.Count > 0)
            {
                for (int i = tree_node.ChildNodes.Count - 1; i >= 0; i--)
                {
                    removerContenidoEnTreeView(tree_node.ChildNodes[i]);
                    tree_node.ChildNodes.RemoveAt(i);

                }
            }
        }


        private void listarExtenciones()
        {
            string ruta_actual_16 = ruta_principal + "\\Images\\icons_ext\\16";
            string ruta_actual_32 = ruta_principal + "\\Images\\icons_ext\\32";

            string ext_16 = "";
            string ext_32 = "";

            //listamos todos los archivos de 16----------------------
            DirectoryInfo dir_info_16 = new DirectoryInfo(ruta_actual_16);

            if (dir_info_16.Exists)
            {
                dir_info_16.Refresh();
                try
                {
                    FileInfo[] lista_archivos_16 = dir_info_16.GetFiles();
                    for (int i = 0; i < lista_archivos_16.Length; i++)
                    {
                        string ext = lista_archivos_16[i].Name.Replace("file_extension_", "");
                        ext = ext.Replace(".png", "");
                        ext_16 += ext + ",";
                    }

                    ext_16 = ext_16.Substring(0, ext_16.Length - 1);
                }
                catch (Exception e)
                {
                }
            }//end if


            //listamos todos los archivos de 16----------------------
            DirectoryInfo dir_info_32 = new DirectoryInfo(ruta_actual_32);

            if (dir_info_32.Exists)
            {
                dir_info_32.Refresh();
                try
                {
                    FileInfo[] lista_archivos_32 = dir_info_32.GetFiles();
                    for (int i = 0; i < lista_archivos_32.Length; i++)
                    {
                        string ext = lista_archivos_32[i].Name.Replace("file_extension_", "");
                        ext = ext.Replace(".png", "");
                        ext_32 += ext + ",";

                    }

                    ext_32 = ext_32.Substring(0, ext_32.Length - 1);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("ERROR: " + e.ToString());
                }
            }//end if


            HiddenField_FileExtensions.Value = ext_16 + "|" + ext_32;
        }



        private void subirArchivo(string ruta_carpeta_web, string fecha, string descripcion)
        {
            //Boolean fileOK = false;
            String base_path = Server.MapPath("~/Protegido/Contenedor");
            string ruta_carpeta = ruta_carpeta_web.Replace("/", @"\");

            if (FileUpload1.HasFile)
            {
                try
                {
                    //string ruta_guardar_archivo = base_path + "\\" + nombre_carpeta + "\\" + FileUpload1.FileName;
                    string ruta_guardar_archivo = base_path + ruta_carpeta + "\\" + FileUpload1.FileName;

                    var fileInfo = new FileInfo(ruta_guardar_archivo);
                    if (!fileInfo.Exists)
                    {
                        FileUpload1.PostedFile.SaveAs(ruta_guardar_archivo);

                        //this.descripcion_archivos_carpetas.guardarDescripcionArchivo(FileUpload1.FileName, ruta_carpeta, fecha, descripcion);

                        enviar_correo.enviarCorreoConfirmacionSubirNuevoArchivo(FileUpload1.FileName, ruta_carpeta_web);
                    }
                    else
                    {
                        FileUpload1.PostedFile.SaveAs(ruta_guardar_archivo);


                        enviar_correo.enviarCorreoConfirmacionSubirActualizacionArchivo(FileUpload1.FileName, ruta_carpeta_web);
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ERROR: No se pudo subir el archivo: " + FileUpload1.FileName);
                }
            }
            else
            {
                Debug.WriteLine("No se ha seleccionado ningún archivo.");
            }

            repaintTreeView();
        }



        private void crearCarpeta(String nombre_carpeta_padre, String nombre_carpeta_crear, String descripcion_carpeta, String fecha_carpeta)
        {
            String nombre_completo_carpeta_crear = ruta_principal + ruta_relativa_base + nombre_carpeta_padre + "\\" + nombre_carpeta_crear;
            nombre_completo_carpeta_crear = nombre_completo_carpeta_crear.Replace("/", @"\");

            try
            {
                var dirInfo = new DirectoryInfo(nombre_completo_carpeta_crear);
                if (!dirInfo.Exists)
                {
                    Directory.CreateDirectory(nombre_completo_carpeta_crear);

                    String ruta_carpeta = nombre_carpeta_padre + "\\" + nombre_carpeta_crear;
                    ruta_carpeta = ruta_carpeta.Replace("/", @"\");

                    this.enviar_correo.enviarCorreoConfirmacionCrearCarpeta(nombre_carpeta_padre, nombre_carpeta_crear);
                    //this.descripcion_archivos_carpetas.guardarDescripcionCarpeta(ruta_carpeta, fecha_carpeta, descripcion_carpeta);

                }
                //repintamos el treeview
                repaintTreeView();
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR CREAR CARPETA: " + e.ToString());
            }
            //this.descripcion_archivos_carpetas.borrarDescripcion(nombre_archivo_borrar, path_archivo_borrar);

        }


        private void borrarCarpeta(String nombre_carpeta_borrar)
        {
            String nombre_completo_carpeta_borrar = ruta_principal + ruta_relativa_base + nombre_carpeta_borrar;
            nombre_completo_carpeta_borrar = nombre_completo_carpeta_borrar.Replace("/", @"\");

            try
            {
                Directory.Delete(nombre_completo_carpeta_borrar, false);

                var dirInfo = new DirectoryInfo(nombre_completo_carpeta_borrar);
                if (!dirInfo.Exists)
                {

                    int pos = nombre_carpeta_borrar.LastIndexOf('/');
                    string nombre = nombre_carpeta_borrar.Substring(pos + 1);
                    string ruta = nombre_carpeta_borrar.Substring(0, pos);
                    this.enviar_correo.enviarCorreoConfirmacionEliminarCarpeta(ruta, nombre);
                }


            }
            catch (IOException ioex)
            {
                String msg = ioex.ToString();
                Debug.WriteLine(msg);
                mensajeHTML(msg);
            }

            //repintamos el treeview
            repaintTreeView();
        }

        private void renombrarCarpeta(String nombre_carpeta, String nuevo_nombre_carpeta)
        {
            int pos = nombre_carpeta.LastIndexOf('/');
            string path = nombre_carpeta.Substring(0, pos);
            string nombre_original = nombre_carpeta.Substring(pos + 1);


            String nombre_completo_carpeta = ruta_principal + ruta_relativa_base + nombre_carpeta;
            nombre_completo_carpeta = nombre_completo_carpeta.Replace("/", @"\");



            String nombre_completo_carpeta_nuevo = ruta_principal + ruta_relativa_base + path + "/" + nuevo_nombre_carpeta;
            nombre_completo_carpeta_nuevo = nombre_completo_carpeta_nuevo.Replace("/", @"\");

            try
            {
                var dirInfo = new DirectoryInfo(nombre_completo_carpeta);
                if (dirInfo.Exists)
                {
                    Directory.Move(nombre_completo_carpeta, nombre_completo_carpeta_nuevo);

                    this.enviar_correo.enviarCorreoConfirmacionRenombrarCarpeta(path, nombre_original, nuevo_nombre_carpeta);
                }

            }
            catch (IOException ioex)
            {
                String msg = ioex.ToString();
                Debug.WriteLine(msg);
                mensajeHTML(msg);
            }

            //repintamos el treeview
            repaintTreeView();
        }


        private void borrarArchivo(String path_archivo_borrar, String nombre_archivo_borrar)
        {
            //String nombre_completo_archivo_borrar = ruta_principal + ruta_relativa_base + nombre_archivo_borrar;
            String nombre_completo_archivo_borrar = ruta_principal + ruta_relativa_base + path_archivo_borrar;

            nombre_completo_archivo_borrar = nombre_completo_archivo_borrar.Replace("/", @"\");

            try
            {
                File.Delete(nombre_completo_archivo_borrar);

                path_archivo_borrar = path_archivo_borrar.Replace("/" + nombre_archivo_borrar, "");
                path_archivo_borrar = path_archivo_borrar.Replace("/", @"\");
                //this.descripcion_archivos_carpetas.borrarDescripcionArchivo(nombre_archivo_borrar, path_archivo_borrar);
                this.enviar_correo.enviarCorreoConfirmacionEliminarArchivo(path_archivo_borrar, nombre_archivo_borrar);
            }
            catch (IOException ioex)
            {
                String msg = ioex.ToString();
                Debug.WriteLine(msg);
                mensajeHTML(msg);
            }

            //repintamos el treeview
            repaintTreeView();
        }

        private void renombrarArchivo(String path_archivo, String nombre_archivo, String nuevo_nombre_archivo)
        {

            path_archivo = path_archivo.Replace(nombre_archivo, "");

            String nombre_completo_archivo = ruta_principal + ruta_relativa_base + path_archivo + nombre_archivo;
            String nombre_completo_archivo_nuevo = ruta_principal + ruta_relativa_base + path_archivo + nuevo_nombre_archivo;

            nombre_completo_archivo = nombre_completo_archivo.Replace("/", @"\");
            nombre_completo_archivo_nuevo = nombre_completo_archivo_nuevo.Replace("/", @"\");

            try
            {
                File.Move(nombre_completo_archivo, nombre_completo_archivo_nuevo);

                this.enviar_correo.enviarCorreoConfirmacionRenombrarArchivo(path_archivo, nombre_archivo, nuevo_nombre_archivo);

            }
            catch (IOException ioex)
            {
                String msg = ioex.ToString();
                Debug.WriteLine(msg);
                mensajeHTML(msg);
            }

            //repintamos el treeview
            repaintTreeView();
        }



        private void editarCarpeta(string nombre_carpeta, string fecha, string descripcion)
        {

            nombre_carpeta = nombre_carpeta.Replace("/", @"\");

            this.descripcion_archivos_carpetas.editarDescripcionCarpeta(nombre_carpeta, fecha, descripcion);

            repaintTreeView();
        }


        private void editarArchivo(string nombre_archivo, string fecha, string descripcion)
        {

            String[] param_nombre_archivo = nombre_archivo.Split('|');

            String path = param_nombre_archivo[0];
            String name = param_nombre_archivo[1];

            string ruta = path.Replace("/" + name, "");
            ruta = ruta.Replace("/", @"\");

            this.descripcion_archivos_carpetas.editarDescripcionArchivo(name, ruta, fecha, descripcion);

            repaintTreeView();
        }


        private void changeExpandCollapseAll()
        {
            //repaintTreeView();

            if (CheckBox_TreeViewExpandAll.Checked)
                TreeView1.ExpandAll();
            else
                TreeView1.CollapseAll();

            //si se colapsan topdos, solo q ueda la raiz, para evitar esto expandemos el nodo raiz
            TreeView1.Nodes[0].Expand();

        }

        //cambia la vista
        private void showTipoVista(String tipo_vista)
        {
            this.tipo_vista = tipo_vista;
            HiddenField_TipoVista.Value = this.tipo_vista;
            Debug.WriteLine("tipo_vista: " + this.tipo_vista);
            repaintTreeView();
        }


        //## METODOS WEB ###################################################################################

        //Catalogo de tipos de equipos con subequipos
        [WebMethod(EnableSession = true)]
        public static string actualizarTreeViewStatusExpand(string treeviewstatus_expand)
        {
            string msg = "ok";
            try
            {
                HttpContext.Current.Session["treeviewstatus_expand"] = treeviewstatus_expand;
                //Debug.WriteLine("> expname: " + expname + " - expand: " + expand);
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            return msg;
        }


        [WebMethod(EnableSession = true)]
        public static string listarArchivosContenidos(string nombre_carpeta)
        {

            DescripcionArchivosCarpetas descripcion_archivos_carpetas = (DescripcionArchivosCarpetas)HttpContext.Current.Session["descripcion_archivos_carpetas"];

            string resultado = "";

            string ruta_principal = HttpContext.Current.Server.MapPath("~/");
            string ruta_relativa_base = "Protegido\\Contenedor";

            string ruta_actual = ruta_principal + ruta_relativa_base + nombre_carpeta;

            ruta_actual = ruta_actual.Replace("/", @"\");

            ////refefrenciamos el directorio (carpeta)----------------------
            DirectoryInfo dir_info = new DirectoryInfo(ruta_actual);


            if (dir_info.Exists)
            {

                dir_info.Refresh();



                DirectoryInfo[] lista_directorios = dir_info.GetDirectories();

                for (int i = 0; i < lista_directorios.Length; i++)
                {
                    var ruta_web = "Contenedor" + nombre_carpeta + "/" + lista_directorios[i].Name;

                    var nombre_carpeta_convertida = nombre_carpeta.Replace("/", @"\");
                    String[] datos_carpeta = descripcion_archivos_carpetas.obtenerDescripcionCarpetaVar(nombre_carpeta_convertida);


                    //verificamos si tiene contenido: archivos o carpetas
                    bool tiene_archivos = false;
                    DirectoryInfo[] ldir = lista_directorios[i].GetDirectories();
                    FileInfo[] lfile = lista_directorios[i].GetFiles();
                    if (ldir.Length > 0 || lfile.Length > 0)
                    {
                        tiene_archivos = true;
                    }



                    string res = "{";
                    //res += "\"nombre_completo\":\"" + lista_archivos[i].FullName + "\",";
                    res += "\"tipo\":\"carpeta\",";
                    res += "\"tiene_contenido\":\"" + tiene_archivos + "\",";
                    res += "\"rutaweb\":\"" + ruta_web + "\",";
                    res += "\"nombre\":\"" + lista_directorios[i].Name + "\",";
                    res += "\"ultima_mod\":\"" + lista_directorios[i].LastWriteTime + "\",";
                    res += "\"desc\":\"" + datos_carpeta[0] + "\",";
                    res += "\"fecha_desc\":\"" + datos_carpeta[1] + "\"";
                    res += "}";

                    resultado += res + ",";
                }




                FileInfo[] lista_archivos = dir_info.GetFiles();

                for (int i = 0; i < lista_archivos.Length; i++)
                {
                    var ruta_web = "Contenedor" + nombre_carpeta + "/" + lista_archivos[i].Name;

                    var nombre_carpeta_convertida = nombre_carpeta.Replace("/", @"\");
                    String[] datos_archivos = descripcion_archivos_carpetas.obtenerDescripcionArchivoVar(lista_archivos[i].Name, nombre_carpeta_convertida);

                    //try
                    //{

                    string ext = (lista_archivos[i].Extension.Length >= 1) ? lista_archivos[i].Extension.Substring(1) : "";
                    string res = "{";
                    //res += "\"nombre_completo\":\"" + lista_archivos[i].FullName + "\",";
                    res += "\"tipo\":\"archivo\",";
                    res += "\"rutaweb\":\"" + ruta_web + "\",";
                    res += "\"nombre\":\"" + lista_archivos[i].Name + "\",";
                    res += "\"ext\":\"" + ext + "\",";
                    res += "\"ultima_mod\":\"" + lista_archivos[i].LastWriteTime + "\",";
                    res += "\"size\":\"" + lista_archivos[i].Length + "\",";
                    res += "\"desc\":\"" + datos_archivos[0] + "\",";
                    res += "\"fecha_desc\":\"" + datos_archivos[1] + "\"";
                    res += "}";

                    resultado += res + ",";
                    //}
                    //catch(Exception e){
                    //    Debug.WriteLine("ERROR :: listarArchivosContenidos :: " + e.ToString());
                    //}
                }

                //si se encontraron archivos
                if (resultado.Length > 0)
                    resultado = resultado.Substring(0, resultado.Length - 1);

            }

            //regresamos todos los archivos y sis datos en una estructura llamada carpeta
            resultado = "{\"carpeta\":[" + resultado + "]}";


            return resultado;
        }


        private void enviarCorreoConfirmacion(string accion, string nombre_archivo, string ruta_archivo)
        {
            if (this.habilitar_envio_email_aviso)
            {

                string usuario = this.User.Identity.Name;
                MembershipUser ms_user = Membership.GetUser(usuario);
                string email = ms_user.Email;


                string web_path = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/Protegido/Contenedor" + ruta_archivo + "/" + nombre_archivo;

                string nombre = "";
                string apellido_paterno = "";
                string apellido_materno = "";
                string enviar_email = "";

                this.control_db.obtenerDatosUsuario(usuario, ref  nombre, ref apellido_paterno, ref apellido_materno, ref enviar_email);

                //definimos el nombre del autor del archivo (quien esta logueado en esta session)
                string autor = nombre + " " + apellido_paterno + " " + apellido_materno;
                if (!(autor.Trim().Length > 0)) autor = usuario; //si no tiene nombre entonces icupamos el nickname

                string servidor_smtp_ip = this.configuracion.ServidorSmtpIp;
                string remitente = "unifilares_zotgm@cfe.gob.mx"; //this.configuracion.Titulo3
                string subject = "";
                string msg = "";

                if (accion.Equals("subir_archivo"))
                {
                    subject = "Se subió un archivo en el Contenedor \"" + this.configuracion.Titulo3 + "\"";


                    msg = "<span style='font-size: 12px;'>" +
                            "Se subió un archivo en el Contenedor <b>\"" + this.configuracion.Titulo3 + "\"</b>" +
                            "</span>" +
                            "\" <br /><br />" +
                            "<span style='font-size: 12px;'>Los datos del archivo son los siguientes: </span> <br />" +
                            "Nombre del archivo: <b>" + nombre_archivo + "</b><br />" +
                            "Ubicacion en el contenedor: <b>" + ruta_archivo + "</b><br />" +
                            "Enlace directo a: <b>" + web_path + "</b><br />" +
                            "Subido Por: <b>" + autor + " &lt;" + email + " &gt;</b>";
                }
                else if (accion.Equals("renombrar_archivo"))
                {
                    subject = "Se renombro un archivo en el Contenedor \"" + this.configuracion.Titulo3 + "\"";


                    msg = "<span style='font-size: 12px;'>" +
                            "Se renombro un archivo en el Contenedor <b>\"" + this.configuracion.Titulo3 + "\"</b>" +
                            "</span>" +
                            "\" <br /><br />" +
                            "<span style='font-size: 12px;'>Los datos del archivo son los siguientes: </span> <br />" +
                            "Nombre del archivo: <b>" + nombre_archivo + "</b><br />" +
                            "Ubicacion en el contenedor: <b>" + ruta_archivo + "</b><br />" +
                            "Enlace directo a: <b>" + web_path + "</b><br />" +
                            "Subido Por: <b>" + autor + " &lt;" + email + " &gt;</b>";
                }
                else if (accion.Equals("eliminar_archivo"))
                {
                    subject = "Se elimino un archivo en el Contenedor \"" + this.configuracion.Titulo3 + "\"";


                    msg = "<span style='font-size: 12px;'>" +
                            "Se elimino un archivo en el Contenedor <b>\"" + this.configuracion.Titulo3 + "\"</b>" +
                            "</span>" +
                            "\" <br /><br />" +
                            "Nombre del archivo: <b>" + nombre_archivo + "</b><br />" +
                            "Ubicacion en el contenedor: <b>" + ruta_archivo + "</b><br />" +
                            "Eliminado Por: <b>" + autor + " &lt;" + email + " &gt;</b>";
                }

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
                            this.email_smtp.enviarCorreo(servidor_smtp_ip, remitente, d_email, "", subject, msg);

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("ERROR: " + e.ToString());
                    }
                }

            }
        }


        //## MENSAJES #######################################################################################
        private void mensajeHTML(string msg)
        {
            string funcion_javascript = "mostrarMensaje(" + msg + "')";
            //ScriptManager.RegisterStartupScript(this.GetType(), typeof(Page), "mostrarMensaje('"+msj+"')", script, true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mostrarMensaje", funcion_javascript, true);

        }



        //## EVENTOS SISTEMA #######################################################################################
        protected void Button_SubirArchivo_Click(object sender, EventArgs e)
        {
            string nombre_carpeta = this.HiddenField_NombreCarpeta.Value;
            string fecha = this.TextBox_FechaArchivo.Text;
            string descripcion = TextBox_DescripcionArchivo.Text;
            subirArchivo(nombre_carpeta, fecha, descripcion);
        }


        protected void Button_CrearCarpeta_Click(object sender, EventArgs e)
        {
            string nombre_carpeta_padre = this.HiddenField_NombreCarpeta.Value;
            string nombre_carpeta_crear = TextBox_CrearCarpeta_Nombre.Text;

            string fecha_carpeta = TextBox_FechaCarpeta.Text;
            string descripcion_carpeta = TextBox_DescripcionCarpeta.Text;

            crearCarpeta(nombre_carpeta_padre, nombre_carpeta_crear, descripcion_carpeta, fecha_carpeta);
        }


        protected void Button_BorrarCarpeta_Click(object sender, EventArgs e)
        {
            string nombre_carpeta_borrar = this.HiddenField_NombreCarpeta.Value;

            //reajustamos la ruta donde se va a visualizar el contenido
            int pos = nombre_carpeta_borrar.LastIndexOf("/");
            this.HiddenField_NombreCarpeta.Value = nombre_carpeta_borrar.Substring(0, pos);

            borrarCarpeta(nombre_carpeta_borrar);
        }

        protected void Button_RenombrarCarpeta_Click(object sender, EventArgs e)
        {
            string nombre_carpeta = this.HiddenField_NombreCarpeta.Value;

            string nuevo_nombre_carpeta = TextBox_NuevoNombreCarpeta.Text;

            //reajustamos la ruta donde se va a visualizar el contenido
            int pos = nombre_carpeta.LastIndexOf("/");
            this.HiddenField_NombreCarpeta.Value = nombre_carpeta.Substring(0, pos);

            renombrarCarpeta(nombre_carpeta, nuevo_nombre_carpeta);
        }

        protected void Button_BorrarArchivo_Click(object sender, EventArgs e)
        {
            String nombre_archivo_borrar = this.HiddenField_NombreArchivo.Value;
            String[] param_nombre_archivo_borrar = nombre_archivo_borrar.Split('|');

            String path = param_nombre_archivo_borrar[0];
            String name = param_nombre_archivo_borrar[1];

            borrarArchivo(path, name);
        }
        protected void Button_RenombrarArchivo_Click(object sender, EventArgs e)
        {
            String nombre_archivo = this.HiddenField_NombreArchivo.Value;
            String[] param_nombre_archivo = nombre_archivo.Split('|');

            String path = param_nombre_archivo[0];
            String name = param_nombre_archivo[1];

            string nuevo_nombre = TextBox_NuevoNombreArchivo.Text;

            renombrarArchivo(path, name, nuevo_nombre);
        }


        protected void CheckBox_TreeViewExpandAll_CheckedChanged(object sender, EventArgs e)
        {
            changeExpandCollapseAll();
        }


        protected void Button_EditarDescripcionArchivo_Click(object sender, EventArgs e)
        {

            string nombre_archivo = this.HiddenField_NombreArchivo.Value;
            string nueva_descripcion = TextBox_EditarDescripcionArchivo.Text;
            string nueva_fecha_archivo = TextBox_EditarFechaArchivo.Text;


            editarArchivo(nombre_archivo, nueva_fecha_archivo, nueva_descripcion);
        }


        protected void Button_EditarDescripcionCarpeta_Click(object sender, EventArgs e)
        {
            string nombre_carpeta = this.HiddenField_NombreCarpeta.Value;
            string nueva_descripcion = TextBox_EditarDescripcionCarpeta.Text;
            string nueva_fecha_carpeta = TextBox_EditarFechaCarpeta.Text;


            editarCarpeta(nombre_carpeta, nueva_fecha_carpeta, nueva_descripcion);
        }





        protected void ImageButton_ViewTree_Click(object sender, ImageClickEventArgs e)
        {
            showTipoVista("tree");
        }
        protected void ImageButton_ViewIcons_Click(object sender, ImageClickEventArgs e)
        {
            showTipoVista("icons");
        }
        protected void ImageButton_ViewDetails_Click(object sender, ImageClickEventArgs e)
        {
            showTipoVista("details");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //enviarCorreoConfirmacion("mi_archivo_pueba.txt", "http://localhost:29289/ContenedorArchivosWebSite/Protegido/Contenido.aspx");
        }




    }
}