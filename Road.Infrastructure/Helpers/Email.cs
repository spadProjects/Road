﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Road.Infrastructure.Helpers
{
    public class Email
    {
        public class EmailFormModel
        {
            public string FromName { get; set; }
            public string FromEmail { get; set; }
            public string ToEmail { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
        }


        public static async Task SendEmail(EmailFormModel emailForm)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(emailForm.ToEmail));  // replace with valid value 
            message.From = new MailAddress("RoadWayTeam@gmail.com");  // replace with valid value
            message.Subject = emailForm.Subject;

            message.Body = string.Format(body, emailForm.FromName, emailForm.FromEmail, emailForm.Message);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "RoadWayTeam@gmail.com", // replace with valid value
                    Password = "@Spad$123456&" // replace with valid value
                };
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);

            }

        }
    }

}