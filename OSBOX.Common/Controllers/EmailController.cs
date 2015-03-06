using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace OSBOX.Common.Controllers
{
   public  class EmailController : IEmailController
    {
        public EmailController()
        {
                
        }

       //Gmail App password 
       const string fromPassword = "OpenGate#13";
        

        
        public bool SendEmailUsingGoogleSMTP(string EmailSubject,string EmailBody,string ToEmailAddress)
        {
            //try
            //{
            //    MailMessage mail = new MailMessage();
            //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            //    mail.From = new MailAddress("OSBOX@gmail.com");
            //    mail.To.Add(AdminEmail);
            //    mail.Subject = EmailSubject;
            //    mail.Body = EmailBody;

            //    SmtpServer.Port = 587;
            //    SmtpServer.Credentials = new System.Net.NetworkCredential("ouassim.sadellah@gmail.com", "DreamWalker@1981");
            //    SmtpServer.EnableSsl = true;

            //    SmtpServer.Send(mail);
            //    return ("mail Send");
            //}
            //catch (Exception ex)
            //{
            //    return ex.ToString();
            //}
            var fromAddress = new MailAddress("admin@viasoftech.com", "OSBOX");
            var toAddress = new MailAddress(ToEmailAddress);
            var smtp = new SmtpClient
            {
                Host = "smtpout.secureserver.net",
                //Port = 587,
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = EmailSubject,
                Body = EmailBody
            })
            {
                try
                {
                smtp.Send(message);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
       




       
    }
}
