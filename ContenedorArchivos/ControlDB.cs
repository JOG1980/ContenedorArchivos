using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;


namespace ContenedorArchivos
{
    public class ControlDB
    {

        public ControlDB()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public void complementarUsuario(string nombre_usuario, string nombre, string apellido_paterno, string apellido_materno, string enviar_email)
        {

            string connetionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            SqlConnection cnn;
            SqlCommand cmd;

            string sql = "UPDATE aspnet_Users " +
                         "SET nombre='" + nombre + "'," +
                         "apellido_paterno='" + apellido_paterno + "', " +
                         "apellido_materno='" + apellido_materno + "', " +
                         "enviar_email='" + enviar_email + "' " +
                         "WHERE UserName='" + nombre_usuario + "';";

            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cnn.Close();
                //Debug.WriteLine(" ExecuteNonQuery in SqlCommand executed !!");
            }
            catch (SqlException ex_sql)
            {
                Debug.WriteLine("ERROR SQL: " + ex_sql.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex.ToString());
            }

        }//end function



        public void obtenerDatosUsuario(string nombre_usuario, ref string nombre, ref string apellido_paterno, ref string apellido_materno, ref string enviar_email)
        {

            string connetionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataReader dataReader = null;

            string sql = "SELECT nombre, apellido_paterno, apellido_materno, enviar_email " +
                        "FROM aspnet_Users " +
                        "WHERE UserName='" + nombre_usuario + "';";


            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    nombre = dataReader["nombre"].ToString().Trim();
                    apellido_paterno = dataReader["apellido_paterno"].ToString().Trim();
                    apellido_materno = dataReader["apellido_materno"].ToString().Trim();
                    enviar_email = dataReader["enviar_email"].ToString().Trim();
                }


                cmd.Dispose();
                cnn.Close();
                //Debug.WriteLine(" ExecuteReader in SqlCommand executed !!");
            }
            catch (SqlException ex_sql)
            {
                Debug.WriteLine("ERROR SQL: " + ex_sql.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex.ToString());
            }

        }

    }

}