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

namespace ContenedorArchivos.Protegido
{
    public partial class CuentasUsuarios : System.Web.UI.Page
    {
       

        string ruta_base = "";

        ControlDB control_db = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            init();

            ListarUsuarios();
        }


        private void init()
        {

            this.control_db = new ControlDB();
            this.ruta_base = Server.MapPath("~");
        }


        private void ListarUsuarios()
        {
            Table tabla = new Table();

            tabla.Attributes.Add("class", "tabla_lista_usuarios");

            TableHeaderCell h_celda_nombre_usuario = new TableHeaderCell();
            h_celda_nombre_usuario.Text = "Usuario";

            TableHeaderCell h_celda_password_usuario = new TableHeaderCell();
            h_celda_password_usuario.Text = "Password";


            TableCell h_celda_nombre = new TableCell();
            h_celda_nombre.Text = "Nombre";

            TableCell h_celda_apellido_paterno = new TableCell();
            h_celda_apellido_paterno.Text = "Apellido Paterno";

            TableCell h_celda_apellido_materno = new TableCell();
            h_celda_apellido_materno.Text = "Apellido Materno";


            TableHeaderCell h_celda_email = new TableHeaderCell();
            h_celda_email.Text = "Email";

            TableCell h_celda_enviar_email = new TableCell();
            h_celda_enviar_email.Attributes.Add("data-sorter", "false");

            h_celda_enviar_email.Text = "Enviar Email";

            TableHeaderCell h_celda_rol = new TableHeaderCell();
            h_celda_rol.Text = "Rol";

            TableHeaderCell h_celda_boton_borrar = new TableHeaderCell();
            //h_celda_boton_borrar.Attributes.Add("data-sorter", "false");
            h_celda_boton_borrar.Text = "<span title='Editar'>E</span>";

            TableHeaderCell h_celda_boton_editar = new TableHeaderCell();
            //h_celda_boton_editar.Attributes.Add("data-sorter", "false");
            h_celda_boton_editar.Text = "<span title='Borrar'>B</span>";

            TableHeaderRow hrow = new TableHeaderRow();
            hrow.TableSection = TableRowSection.TableHeader;
            hrow.Cells.Add(h_celda_nombre_usuario);
            hrow.Cells.Add(h_celda_password_usuario);

            hrow.Cells.Add(h_celda_nombre);
            hrow.Cells.Add(h_celda_apellido_paterno);
            hrow.Cells.Add(h_celda_apellido_materno);


            hrow.Cells.Add(h_celda_email);
            hrow.Cells.Add(h_celda_enviar_email);

            hrow.Cells.Add(h_celda_rol);
            hrow.Cells.Add(h_celda_boton_editar);
            hrow.Cells.Add(h_celda_boton_borrar);

            tabla.Rows.Add(hrow);



            MembershipUserCollection users = Membership.GetAllUsers();
            IEnumerator lista_usuarios = users.GetEnumerator();

            while (lista_usuarios.MoveNext())
            {

                try
                {
                    //optenemos el usuario actual
                    string user = lista_usuarios.Current.ToString();

                    MembershipUser ms_user = Membership.GetUser(user);


                    string password = ms_user.GetPassword();
                    string email = ms_user.Email;

                    string rol = "";
                    //obtenemos el rol (o roles) del usuario. enb este caso solo debe de ser uno
                    string[] rolesuser = Roles.GetRolesForUser(user);
                    if (rolesuser.Length > 0) rol = rolesuser[0];

                    string nombre = "";
                    string apellido_paterno = "";
                    string apellido_materno = "";
                    string enviar_email = "";

                    control_db.obtenerDatosUsuario(user, ref  nombre, ref apellido_paterno, ref apellido_materno, ref enviar_email);


                    TableCell celda_nombre_usuario = new TableCell();
                    celda_nombre_usuario.Text = user;

                    TableCell celda_password_usuario = new TableCell();
                    celda_password_usuario.Text = password;

                    TableCell celda_nombre = new TableCell();
                    celda_nombre.Text = nombre;

                    TableCell celda_apellido_paterno = new TableCell();
                    celda_apellido_paterno.Text = apellido_paterno;

                    TableCell celda_apellido_materno = new TableCell();
                    celda_apellido_materno.Text = apellido_materno;

                    TableCell celda_email = new TableCell();
                    celda_email.Text = email;

                    TableCell celda_enviar_email = new TableCell();
                    celda_enviar_email.Text = (enviar_email.Equals("True")) ? "SI" : "";


                    TableCell celda_rol = new TableCell();
                    celda_rol.Text = rol;

                    TableCell celda_boton_borrar = new TableCell();
                    celda_boton_borrar.Text = "<img src='../Images/IconPack1/16x16/remove.png' style='cursor:pointer;' onclick='mostrarDialogoBorrarUsuario(\"" + user + "\")' />";

                    TableCell celda_boton_editar = new TableCell();
                    celda_boton_editar.Text = "<img src='../Images/IconPack1/16x16/notes_edit.png' style='cursor:pointer;' onclick='mostrarDialogoEditarUsuario(\"" + user + "\",\"" + password + "\",\"" + rol + "\",\"" + nombre + "\",\"" + apellido_paterno + "\",\"" + apellido_materno + "\",\"" + email + "\",\"" + enviar_email + "\")' />";


                    TableRow renglon = new TableRow();
                    renglon.TableSection = TableRowSection.TableBody;
                    renglon.Cells.Add(celda_nombre_usuario);
                    renglon.Cells.Add(celda_password_usuario);

                    renglon.Cells.Add(celda_nombre);
                    renglon.Cells.Add(celda_apellido_paterno);
                    renglon.Cells.Add(celda_apellido_materno);

                    renglon.Cells.Add(celda_email);
                    renglon.Cells.Add(celda_enviar_email);

                    renglon.Cells.Add(celda_rol);
                    renglon.Cells.Add(celda_boton_editar);
                    renglon.Cells.Add(celda_boton_borrar);

                    tabla.Rows.Add(renglon);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("ERROR: " + e.ToString());
                }
            }

            Panel_Usuarios.Controls.Add(tabla);

        }

        private void crearUsuario(string nombre_usuario, string password, string rol, string nombre, string apellido_paterno, string apellido_materno, string email, string enviar_email)
        {

            nombre_usuario = nombre_usuario.Trim();
            if (nombre_usuario == null || !(nombre_usuario.Length > 1)) return;

            password = password.Trim();
            if (password == null || !(password.Length > 1)) return;


            if (Membership.GetUser(nombre_usuario) == null)
            {
                email = validarEmail(email);

                Membership.CreateUser(nombre_usuario, password, email);
                Roles.AddUserToRole(nombre_usuario, rol);


                //completamos los campos que faltan al crear un usuario
                this.control_db.complementarUsuario(nombre_usuario, nombre, apellido_paterno, apellido_materno, enviar_email);


                //recargamos esta pagina, para visualizar los nuevos cambios---------------------------------
                //Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }


        private void editarUsuario(string nombre_usuario, string old_password, string new_password, string old_rol, string new_rol, string nombre, string apellido_paterno, string apellido_materno, string email, string enviar_email)
        {

            nombre_usuario = nombre_usuario.Trim();
            if (nombre_usuario == null || !(nombre_usuario.Length > 1)) return;

            new_password = new_password.Trim();
            if (new_password == null || !(new_password.Length > 1)) return;

            old_password = old_password.Trim();
            if (old_password == null || !(old_password.Length > 1)) return;

            email = validarEmail(email);

            if (!new_rol.Equals("Admin") && !new_rol.Equals("SuperUser") && !new_rol.Equals("User")) return;
            if (!old_rol.Equals("Admin") && !old_rol.Equals("SuperUser") && !old_rol.Equals("User")) return;

            if (!enviar_email.Equals("True") && !enviar_email.Equals("False")) return;


            MembershipUser ms_user = Membership.GetUser(nombre_usuario);
            ms_user.ChangePassword(old_password, new_password);
            ms_user.Email = email;
            Roles.RemoveUserFromRole(nombre_usuario, old_rol);
            Roles.AddUserToRole(nombre_usuario, new_rol);

            Membership.UpdateUser(ms_user);

            //completamos los campos que faltan al crear un usuario
            this.control_db.complementarUsuario(nombre_usuario, nombre, apellido_paterno, apellido_materno, enviar_email);


            //recargamos esta pagina, para visualizar los nuevos cambios---------------------------------
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        //borramos un usuario
        private void borrarUsuario(String usuario)
        {
            try
            {
                Membership.DeleteUser(usuario);
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR" + e.ToString());
            }

            //recargamos esta pagina, para visualizar los nuevos cambios---------------------------------
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }



        //exportamos a los usuarios =====================================================================================================
        private void exportarUsuarios()
        {

            string ruta_web_archivo_usuarios_exportar = "ArchivosImportadosExportados/UsuariosExportados.csv";
            string ruta_completa_archivo_usuarios_exportar = this.ruta_base + "\\Protegido\\ArchivosImportadosExportados\\UsuariosExportados.csv";
            try
            {

                FileStream stream = new FileStream(ruta_completa_archivo_usuarios_exportar, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream);

                MembershipUserCollection users = Membership.GetAllUsers();
                IEnumerator lista_usuarios = users.GetEnumerator();


                while (lista_usuarios.MoveNext())
                {

                    try
                    {
                        //optenemos el usuario actual
                        string user = lista_usuarios.Current.ToString();

                        MembershipUser ms_user = Membership.GetUser(user);

                        string password = ms_user.GetPassword();

                        string nombre = "";
                        string apellido_paterno = "";
                        string apellido_materno = "";
                        string enviar_email = "";

                        string email = ms_user.Email;

                        string rol = "";
                        //obtenemos el rol (o roles) del usuario. enb este caso solo debe de ser uno
                        string[] rolesuser = Roles.GetRolesForUser(user);
                        if (rolesuser.Length > 0) rol = rolesuser[0];

                        this.control_db.obtenerDatosUsuario(user, ref nombre, ref apellido_paterno, ref apellido_materno, ref enviar_email);

                        string new_line = user + "," + password + "," + rol + "," + nombre + "," + apellido_paterno + "," + apellido_materno + "," + email + "," + enviar_email;

                        writer.WriteLine(new_line);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("ERROR: " + e.ToString());
                    }//try del Membership


                }//end del while

                //cerramos el archivo
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            //descargamos el archivo
            Response.Redirect(ruta_web_archivo_usuarios_exportar);

        }



        //exportamos a los usuarios
        private void importarUsuarios()
        {

            if (FileUpload1.HasFile)
            {
                try
                {
                    string ruta_archivo_importar = this.ruta_base + "\\Protegido\\ArchivosImportadosExportados\\UsuariosImportados.csv";

                    FileUpload1.PostedFile.SaveAs(ruta_archivo_importar);

                    try
                    {

                        string[] lines = System.IO.File.ReadAllLines(ruta_archivo_importar);

                        foreach (string line in lines)
                        {

                            try
                            {
                                // Use a tab to indent each line of the file.
                                //Debug.WriteLine("> " + line);
                                string[] elementos_linea = line.Split(',');
                                string nombre_usuario = elementos_linea[0];
                                string password = elementos_linea[1];

                                string rol = elementos_linea[2];

                                string nombre = elementos_linea[3];
                                string apellido_paterno = elementos_linea[4];
                                string apellido_materno = elementos_linea[5];

                                string email = elementos_linea[6];
                                string enviar_email = elementos_linea[7];


                                email = validarEmail(email);

                                crearUsuario(nombre_usuario, password, rol, nombre, apellido_paterno, apellido_materno, email, enviar_email);

                            }
                            catch (Exception ex3)
                            {
                                string error_msg = "ERROR: No se pudo crear el usuario ." + line + "\n" + ex3.ToString();
                                Label_Mensaje.Text = error_msg;
                                Debug.WriteLine(error_msg);
                            }

                        }



                    }
                    catch (Exception ex2)
                    {
                        Debug.WriteLine("ERROR: " + ex2.ToString());
                        Label_Mensaje.Text = "ERROR: No se pudo Leer el archivo.";
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ERROR: " + ex.ToString());
                    Label_Mensaje.Text = "ERROR: No se pudo subir el archivo: " + FileUpload1.FileName;
                }
            }
            else
            {
                Label_Mensaje.Text = "No se ha seleccionado ningún archivo.";
            }

            //recargamos esta pagina, para visualizar los nuevos cambios---------------------------------
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        private string validarEmail(string email)
        {
            if (IsValidEmail(email))
            {
                return email;
            }

            return "";
        }


        //## EVENTOS GENERADOS POR EL IDE ###########################################

        protected void Button_BorrarUsuario_Click(object sender, EventArgs e)
        {
            String usurio = HiddenField_UsuarioSeleccionado.Value;
            borrarUsuario(usurio);
            //recargamos esta pagina, para visualizar los nuevos cambios---------------------------------
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }


        protected void Button_CrearUsuario_Click(object sender, EventArgs e)
        {
            string nombre_usuario = TextBox_NombreUsuario.Text;
            string password = TextBox_Password.Text;

            string rol = DropDownList_Roles.SelectedValue;

            string nombre = TextBox_Nombre.Text;
            string apellido_paterno = TextBox_ApellidoPaterno.Text;
            string apellido_materno = TextBox_ApellidoMaterno.Text;

            string email = TextBox_Email.Text;
            string enviar_email = DropDownList_EnviarEmail.SelectedItem.Value;

            crearUsuario(nombre_usuario, password, rol, nombre, apellido_paterno, apellido_materno, email, enviar_email);
            //recargamos esta pagina, para visualizar los nuevos cambios---------------------------------
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }



        protected void Button_EditarUsuario_Click(object sender, EventArgs e)
        {
            string nombre_usuario = HiddenField_UsuarioSeleccionado.Value;

            string old_password = HiddenField_OldPassword.Value;
            string new_password = TextBox_Password2.Text;

            string old_rol = HiddenField_OldRol.Value;
            string new_rol = DropDownList_Roles2.SelectedValue;

            string nombre = TextBox_Nombre2.Text;
            string apellido_paterno = TextBox_ApellidoPaterno2.Text;
            string apellido_materno = TextBox_ApellidoMaterno2.Text;

            string email = TextBox_Email2.Text;
            string enviar_email = DropDownList_EnviarEmail2.SelectedItem.Value;

            editarUsuario(nombre_usuario, old_password, new_password, old_rol, new_rol, nombre, apellido_paterno, apellido_materno, email, enviar_email);
            //recargamos esta pagina, para visualizar los nuevos cambios---------------------------------
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }



        protected void Button_ExportarUsuarios_Click(object sender, EventArgs e)
        {
            exportarUsuarios();
        }

        protected void Button_ImportarUsuarios_Click(object sender, EventArgs e)
        {
            importarUsuarios();
        }
    }
}