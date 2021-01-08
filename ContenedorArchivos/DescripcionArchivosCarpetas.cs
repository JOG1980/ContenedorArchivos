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

    class _DatosArchivoCarpeta
    {
        public string descripcion;
        public string fecha;
    }

    public class DescripcionArchivosCarpetas
    {

        private Dictionary<String, _DatosArchivoCarpeta> descripciones_archivos = null;
        private Dictionary<String, _DatosArchivoCarpeta> descripciones_carpetas = null;

        private String nombre_archivo_descipcion = "descripcion_archivos.xml";
        private String ruta_nombre_archivo_descipcion = "";

        private String nombre_carpeta_descipcion = "descripcion_carpetas.xml";
        private String ruta_nombre_carpeta_descipcion = "";

        public DescripcionArchivosCarpetas()
        {
            init();
            generarMapaCarpetas();
            generarMapaArchivos();
        }


        private void init()
        {
            this.ruta_nombre_archivo_descipcion = HttpContext.Current.Server.MapPath("~/Protegido/Configuracion" + "\\" + nombre_archivo_descipcion);
            this.ruta_nombre_carpeta_descipcion = HttpContext.Current.Server.MapPath("~/Protegido/Configuracion" + "\\" + nombre_carpeta_descipcion);

        }


        private void generarMapaArchivos()
        {
            this.descripciones_archivos = new Dictionary<String, _DatosArchivoCarpeta>();

            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas
            xDoc.Load(this.ruta_nombre_archivo_descipcion);

            XmlNodeList xml_archivos_descripcion = xDoc.GetElementsByTagName("archivo");

            for (int i = 0; i < xml_archivos_descripcion.Count; i++)
            {
                XmlNode current_xml_node = xml_archivos_descripcion[i];

                string current_file_name = current_xml_node.Attributes["fname"].Value;
                string current_file_path = current_xml_node.Attributes["fpath"].Value;

                String full_name = current_file_path + "\\" + current_file_name;

                _DatosArchivoCarpeta da = new _DatosArchivoCarpeta();
                da.descripcion = current_xml_node.ChildNodes[0].InnerText; //descripcion
                da.fecha = current_xml_node.ChildNodes[1].InnerText;       //fecha
                this.descripciones_archivos.Add(full_name, da);
            }

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }


        private void generarMapaCarpetas()
        {
            this.descripciones_carpetas = new Dictionary<String, _DatosArchivoCarpeta>();

            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas
            xDoc.Load(this.ruta_nombre_carpeta_descipcion);

            XmlNodeList xml_archivos_descripcion = xDoc.GetElementsByTagName("carpeta");

            for (int i = 0; i < xml_archivos_descripcion.Count; i++)
            {
                XmlNode current_xml_node = xml_archivos_descripcion[i];

                string current_file_path = current_xml_node.Attributes["fpath"].Value;

                _DatosArchivoCarpeta da = new _DatosArchivoCarpeta();
                da.descripcion = current_xml_node.ChildNodes[0].InnerText; //descripcion
                da.fecha = current_xml_node.ChildNodes[1].InnerText;       //fecha
                this.descripciones_carpetas.Add(current_file_path, da);
            }

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        //valida si el archivo existe
        public bool existeDescripcionArchivo(string file_name, string file_path)
        {
            bool archivo_existe = false;

            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas
            //respecto del ejecutable!
            xDoc.Load(this.ruta_nombre_archivo_descipcion);

            XmlNodeList xml_archivos_descripcion = xDoc.GetElementsByTagName("archivo");

            for (int i = 0; i < xml_archivos_descripcion.Count; i++)
            {

                XmlNode current_xml_node = xml_archivos_descripcion[i];

                string current_file_name = current_xml_node.Attributes["fname"].Value;
                string current_file_path = current_xml_node.Attributes["fpath"].Value;

                if (file_name.Equals(current_file_name) && file_path.Equals(current_file_path))
                {
                    archivo_existe = true;
                    break;
                }
            }

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return archivo_existe;
        }


        //valida si el carpeta existe
        public bool existeDescripcionCarpeta(string folder_path)
        {
            bool carpeta_existe = false;

            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas
            //respecto del ejecutable!
            xDoc.Load(this.ruta_nombre_carpeta_descipcion);

            XmlNodeList xml_carpeta_descripcion = xDoc.GetElementsByTagName("carpeta");

            for (int i = 0; i < xml_carpeta_descripcion.Count; i++)
            {

                XmlNode current_xml_node = xml_carpeta_descripcion[i];


                string current_folder_path = current_xml_node.Attributes["fpath"].Value;

                if (folder_path.Equals(current_folder_path))
                {
                    carpeta_existe = true;
                    break;
                }
            }

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return carpeta_existe;
        }


        public bool existeDescripcionArchivoVar(string file_name, string file_path)
        {
            string full_name = file_name + "\\" + file_path;

            return this.descripciones_archivos.ContainsKey(full_name);
        }



        public bool existeDescripcionCarpetaVar(string file_path)
        {
            return this.descripciones_carpetas.ContainsKey(file_path);
        }




        public String[] obtenerDescripcionArchivo(string file_name, string file_path)
        {

            XmlDocument xDoc = new XmlDocument();

            String[] res = new String[2] { "", "" };

            //La ruta del documento XML permite rutas relativas
            //respecto del ejecutable!

            xDoc.Load(this.ruta_nombre_archivo_descipcion);

            XmlNodeList xml_archivos_descripcion = xDoc.GetElementsByTagName("archivo");

            for (int i = 0; i < xml_archivos_descripcion.Count; i++)
            {

                XmlNode current_xml_node = xml_archivos_descripcion[i];

                string current_file_name = current_xml_node.Attributes["fname"].Value;
                string current_file_path = current_xml_node.Attributes["fpath"].Value;

                if (file_name.Equals(current_file_name) && file_path.Equals(current_file_path))
                {
                    res[0] = current_xml_node.ChildNodes[0].InnerText; //descripcion
                    res[1] = current_xml_node.ChildNodes[1].InnerText;  //fecha
                }
            }

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return res;
        }


        public String[] obtenerDescripcionCarpeta(string folder_path)
        {

            XmlDocument xDoc = new XmlDocument();

            String[] res = new String[2] { "", "" };

            //La ruta del documento XML permite rutas relativas
            //respecto del ejecutable!

            xDoc.Load(this.ruta_nombre_carpeta_descipcion);

            XmlNodeList xml_carpetas_descripcion = xDoc.GetElementsByTagName("carpeta");

            for (int i = 0; i < xml_carpetas_descripcion.Count; i++)
            {

                XmlNode current_xml_node = xml_carpetas_descripcion[i];

                string current_folder_path = current_xml_node.Attributes["fpath"].Value;

                if (folder_path.Equals(current_folder_path))
                {
                    res[0] = current_xml_node.ChildNodes[0].InnerText; //descripcion
                    res[1] = current_xml_node.ChildNodes[1].InnerText;  //fecha
                    break;
                }
            }

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return res;
        }





        public String[] obtenerDescripcionArchivoVar(string file_name, string file_path)
        {
            String[] res = new String[2] { "", "" };

            string full_name = file_path + "\\" + file_name;

            if (this.descripciones_archivos.ContainsKey(full_name))
            {

                res[0] = this.descripciones_archivos[full_name].descripcion;
                res[1] = this.descripciones_archivos[full_name].fecha;
            }
            else
            {
                res[0] = res[1] = "";
            }

            return res;
        }


        public String[] obtenerDescripcionCarpetaVar(string folder_path)
        {
            String[] res = new String[2] { "", "" };

            if (this.descripciones_carpetas.ContainsKey(folder_path))
            {

                res[0] = this.descripciones_carpetas[folder_path].descripcion;
                res[1] = this.descripciones_carpetas[folder_path].fecha;
            }
            else
            {
                res[0] = res[1] = "";
            }

            return res;
        }



        public void guardarDescripcionArchivo(string nombre_archivo, string ruta_archivo, string fecha, string descripcion)
        {


            if (!existeDescripcionArchivo(nombre_archivo, ruta_archivo))
            {

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(this.ruta_nombre_archivo_descipcion);

                XmlNode root = xDoc.DocumentElement;

                XmlElement elem_descripcion = xDoc.CreateElement("desc");
                elem_descripcion.InnerText = descripcion;

                XmlElement elem_fecha = xDoc.CreateElement("fecha");
                elem_fecha.InnerText = fecha;

                XmlElement elem_archivo = xDoc.CreateElement("archivo");
                elem_archivo.AppendChild(elem_descripcion);
                elem_archivo.AppendChild(elem_fecha);

                elem_archivo.SetAttribute("fname", nombre_archivo);
                elem_archivo.SetAttribute("fpath", ruta_archivo);

                root.AppendChild(elem_archivo);

                xDoc.Save(this.ruta_nombre_archivo_descipcion);
                xDoc = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        public void guardarDescripcionCarpeta(string ruta_carpeta, string fecha, string descripcion)
        {


            if (!existeDescripcionCarpeta(ruta_carpeta))
            {

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(this.ruta_nombre_carpeta_descipcion);

                XmlNode root = xDoc.DocumentElement;


                XmlElement elem_descripcion = xDoc.CreateElement("desc");
                elem_descripcion.InnerText = descripcion;

                XmlElement elem_fecha = xDoc.CreateElement("fecha");
                elem_fecha.InnerText = fecha;

                XmlElement elem_carpeta = xDoc.CreateElement("carpeta");
                elem_carpeta.AppendChild(elem_descripcion);
                elem_carpeta.AppendChild(elem_fecha);

                elem_carpeta.SetAttribute("fpath", ruta_carpeta);

                root.AppendChild(elem_carpeta);

                xDoc.Save(this.ruta_nombre_carpeta_descipcion);
                xDoc = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }




        public void borrarDescripcionArchivo(string file_name, string file_path)
        {

            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas
            xDoc.Load(this.ruta_nombre_archivo_descipcion);

            XmlNode root = xDoc.DocumentElement;

            XmlNodeList xml_archivos_descripcion = xDoc.GetElementsByTagName("archivo");

            //int indice = 0;
            for (int i = 0; i < xml_archivos_descripcion.Count; i++)
            {

                XmlNode current_xml_node = xml_archivos_descripcion[i];

                string current_file_name = current_xml_node.Attributes["fname"].Value;
                string current_file_path = current_xml_node.Attributes["fpath"].Value;

                if (file_name.Equals(current_file_name) && file_path.Equals(current_file_path))
                {
                    //current_xml_node.RemoveAll();
                    //indice = i;
                    root.RemoveChild(current_xml_node);
                    break;
                }
            }

            //xml_archivos_descripcion[indice].RemoveChild(
            xDoc.Save(this.ruta_nombre_archivo_descipcion);

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }






        public void borrarDescripcionCarpeta(string folder_path)
        {

            XmlDocument xDoc = new XmlDocument();


            //La ruta del documento XML permite rutas relativas
            //respecto del ejecutable!

            xDoc.Load(this.ruta_nombre_carpeta_descipcion);

            XmlNode root = xDoc.DocumentElement;


            XmlNodeList xml_carpetas_descripcion = xDoc.GetElementsByTagName("carpeta");

            //int indice = 0;
            for (int i = 0; i < xml_carpetas_descripcion.Count; i++)
            {

                XmlNode current_xml_node = xml_carpetas_descripcion[i];

                string current_folder_path = current_xml_node.Attributes["fpath"].Value;

                if (folder_path.Equals(current_folder_path))
                {
                    root.RemoveChild(current_xml_node);
                    break;
                }
            }

            xDoc.Save(this.ruta_nombre_carpeta_descipcion);

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }





        public void editarDescripcionArchivo(string file_name, string file_path, string fecha, string descripcion)
        {
            bool archivo_encontrado = false;

            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas

            xDoc.Load(this.ruta_nombre_archivo_descipcion);

            XmlNodeList xml_archivos_descripcion = xDoc.GetElementsByTagName("archivo");

            for (int i = 0; i < xml_archivos_descripcion.Count; i++)
            {

                XmlNode current_xml_node = xml_archivos_descripcion[i];

                string current_file_name = current_xml_node.Attributes["fname"].Value;
                string current_file_path = current_xml_node.Attributes["fpath"].Value;

                if (file_name.Equals(current_file_name) && file_path.Equals(current_file_path))
                {
                    archivo_encontrado = true;

                    current_xml_node.ChildNodes[0].InnerText = descripcion;
                    current_xml_node.ChildNodes[1].InnerText = fecha;


                    break;
                }
            }

            //xml_archivos_descripcion[indice].RemoveChild(
            xDoc.Save(this.ruta_nombre_archivo_descipcion);

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //en el caso de que no exista descripcion se agrega
            //esto sucede por que se agregan archivos sinb la interfaz
            if (!archivo_encontrado)
            {
                guardarDescripcionArchivo(file_name, file_path, fecha, descripcion);
            }
        }


        public void editarDescripcionCarpeta(string folder_path, string fecha, string descripcion)
        {
            bool carpeta_encontrado = false;

            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas

            xDoc.Load(this.ruta_nombre_carpeta_descipcion);

            XmlNodeList xml_carpetas_descripcion = xDoc.GetElementsByTagName("carpeta");

            for (int i = 0; i < xml_carpetas_descripcion.Count; i++)
            {

                XmlNode current_xml_node = xml_carpetas_descripcion[i];

                string current_folder_path = current_xml_node.Attributes["fpath"].Value;

                if (folder_path.Equals(current_folder_path))
                {
                    carpeta_encontrado = true;

                    current_xml_node.ChildNodes[0].InnerText = descripcion;
                    current_xml_node.ChildNodes[1].InnerText = fecha;

                    break;
                }
            }

            //xml_carpetas_descripcion[indice].RemoveChild(
            xDoc.Save(this.ruta_nombre_carpeta_descipcion);

            xDoc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //en el caso de que no exista descripcion se agrega
            //esto sucede por que se agregan carpetas sinb la interfaz
            if (!carpeta_encontrado)
            {
                guardarDescripcionCarpeta(folder_path, fecha, descripcion);
            }
        }

   


    }
}