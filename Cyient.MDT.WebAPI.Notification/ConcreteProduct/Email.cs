using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cyient.MDT.WebAPI.Notification.Interface;
using Cyient.MDT.WebAPI.Notification.Product;
using System.Net.Mail;
using System.Net.Http;
using System.Net;
using System.Configuration;
namespace Cyient.MDT.WebAPI.Notification.ConcreteProduct
{
    public class Email : IMessager
    {

        public Email()
        {

        }
        /// <summary>
        /// This method will send the email notification to recipient
        /// </summary>
        /// <param name="sendMailRequest"> Pass SendMailRequest type object to process the request</param>
        /// <returns></returns>
        public string SendNotification(SendMailRequest sendMailRequest)
        {
            MailMessage mailMessage = new MailMessage();
            try
            {
                // Setting To recipient
                string[] emailAddress = sendMailRequest.recipient.Split(',');
                foreach (var email in emailAddress)
                {
                    mailMessage.To.Add(email);
                }

                // Separate the cc array , if not null
                //if (sendMailRequest.cc != null)
                //{
                //    string[] cc_emailAddress = sendMailRequest.cc.Split(',');
                //    foreach (var email in cc_emailAddress)
                //    {
                //        mailMessage.CC.Add(email);
                //    }
                //}

                //// Include the reply to if not null
                //if (sendMailRequest.replyto != null)
                //{
                //    mailMessage.ReplyToList.Add(new MailAddress(sendMailRequest.replyto));
                //}

                // Include the file attachment if the filename is not null
                //if (sendMailRequest.filename != null)
                //{
                //    // Declare a temp file path where we can assemble our file
                //    string tempPath = Properties.Settings.Default["TempFile"].ToString();

                //    string filePath = Path.Combine(tempPath, sendMailRequest.filename);

                //    using (System.IO.FileStream reader = System.IO.File.Create(filePath))
                //    {
                //        byte[] buffer = Convert.FromBase64String(sendMailRequest.filecontent);
                //        reader.Write(buffer, 0, buffer.Length);
                //        reader.Dispose();
                //    }

                //msg.Attachments.Add(new Attachment(filePath));

                //}

                string sendFromEmail = Properties.Settings.Default["SendFromEmail"].ToString();
                string sendFromName = Properties.Settings.Default["SendFromName"].ToString();
                mailMessage.From = new MailAddress(sendFromEmail, sendFromName);
                mailMessage.Subject = sendMailRequest.subject;
                mailMessage.Body = sendMailRequest.body;
                mailMessage.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPName"].ToString());
                client.Port = 25;
                //client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential nCred = new System.Net.NetworkCredential(sendFromEmail, "Jun201812#");
                client.Credentials = nCred;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mailMessage);
                return "Email sent successfully to " + sendMailRequest.recipient.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                mailMessage.Dispose();
            }
        }
    }
}