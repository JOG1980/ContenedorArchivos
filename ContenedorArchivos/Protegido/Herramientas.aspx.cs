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
    public partial class Herramientas : System.Web.UI.Page
    {

        String ruta_principal = "";
        String ruta_relativa_base = "Protegido\\Contenedor";

        protected void Page_Load(object sender, EventArgs e)
        {
            initVariables();
        }


        private void initVariables()
        {
            this.ruta_principal = Server.MapPath("~/");
        }





        //Esta es la funcion recursiva del armado del treeview -----------------------------------------
        private void listarArchivos(String ruta_relativa_actual)
        {
            string ruta_actual = ruta_principal + ruta_relativa_base + ruta_relativa_actual;

            //listamos todos los directorios----------------------
            DirectoryInfo dir_info = new DirectoryInfo(ruta_actual);


            if (dir_info.Exists)
            {

                dir_info.Refresh();
                try
                {
                    FileInfo[] lista_archivos = dir_info.GetFiles();
                    for (int i = 0; i < lista_archivos.Length; i++)
                    {

                        //String nueva_ruta_relativa_actual = ruta_relativa_actual + "\\" + lista_directorios[i].Name;
                        string nom_arch = lista_archivos[i].Name;

                        //empezamos a remplazar el nombre, por uno correcto
                        nom_arch = nom_arch.Replace("&", "_AND_");

                        Debug.WriteLine("> " + ruta_actual + "\\" + nom_arch);

                        //lista_archivos[i].MoveTo(ruta_relativa_actual + "\\" + nom_arch);
                    }

                    DirectoryInfo[] lista_directorios = dir_info.GetDirectories();
                    for (int i = 0; i < lista_directorios.Length; i++)
                    {

                        String nueva_ruta_relativa_actual = ruta_relativa_actual + "\\" + lista_directorios[i].Name;


                        listarArchivos(nueva_ruta_relativa_actual);
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine("ERROR:" + e.ToString());
                }
            }
        }






        protected void Button_ArreglarNombreArchivos_Click(object sender, EventArgs e)
        {
            listarArchivos("");
        }
    }
}