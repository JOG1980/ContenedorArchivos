using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;

using System.Diagnostics;

//using System.ComponentModel.DataAnnotations;


namespace ContenedorArchivos
{
    public class EmailSMTP
    {

        public const bool SUCCESS = true;
        public const bool FAIL = false;

        public EmailSMTP()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public bool enviarCorreo(String servidor_smtp, String email_remitente, String emails_destinatarios, String sConCopia, String sTitulo, String sCuerpo)
        {
            //creamos el servidor ---------------------------------------------
            SmtpClient smtp = new SmtpClient();
            smtp.Host = servidor_smtp;
            smtp.Port = 25;
            smtp.EnableSsl = false;

            if (email_remitente == null || email_remitente.Length < 2)
            {
                Debug.WriteLine("Error: El emal del remitente no es valido..");
                return FAIL;
            }
            else if (emails_destinatarios == null || email_remitente.Length < 2)
            {
                return FAIL;
            }


            //creamos el mensaje ------------------------------------------
            MailMessage correo = new MailMessage();

            correo.From = new System.Net.Mail.MailAddress(email_remitente);
            correo.To.Add(emails_destinatarios);

            if (sConCopia != null && sConCopia.Length > 1)
                correo.CC.Add(sConCopia);

            correo.Subject = sTitulo;
            correo.Body = sCuerpo;
            correo.IsBodyHtml = true;


            //enviamos el mensaje -----------------------------------------
            try
            {
                smtp.Send(correo);
            }
            catch (ArgumentNullException an_ex)
            {
                Console.WriteLine("-> ERROR - EmailSMTP.enviarCorreo :: " + an_ex);
                return FAIL;
            }
            catch (InvalidOperationException io_ex)
            {
                Console.WriteLine("-> ERROR - EmailSMTP.enviarCorreo :: " + io_ex);
                return FAIL;
            }
            catch (SmtpException ex_smtp)
            {
                Console.WriteLine("-> ERROR - EmailSMTP.enviarCorreo :: " + ex_smtp);
                return FAIL;
            }
            catch (Exception ex)
            {
                Console.WriteLine("enviarCorreo Error por: " + ex);
                return FAIL;
            }
            return SUCCESS;
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

    



    }
}