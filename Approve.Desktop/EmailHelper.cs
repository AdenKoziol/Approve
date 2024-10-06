using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Approve.Desktop
{
    internal class EmailHelper
    {
        public static void SendEmail(string toEmail, string toName, string subject, string body)
        {
            try
            {
                SmtpClient mySmtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("adenkoziol@outlook.com", "underarmour7"),
                    EnableSsl = true
                };

                // Add from, to mail addresses
                MailAddress from = new MailAddress("adenkoziol@outlook.com", "Do Not Reply");
                MailAddress to = new MailAddress(toEmail, toName);
                MailMessage myMail = new MailMessage(from, to);

                // Add ReplyTo
                MailAddress replyTo = new MailAddress("reply@example.com");
                myMail.ReplyToList.Add(replyTo);

                // Set subject and encoding
                myMail.Subject = subject;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // Set body-message and encoding
                myMail.Body = body;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                myMail.IsBodyHtml = true;

                // Send the email
                mySmtpClient.Send(myMail);
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException("SmtpException has occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
