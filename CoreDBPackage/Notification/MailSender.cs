using CoreDBPackage.Config;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CoreDBPackage.Notification {
    public class MailSender : IMailSender {
        private /*async Task*/ void SendMail(string mailValidatorKey, List<string> toList, List<string> ccList = null) {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("FindYourSoulMatee@gmail.com");
            foreach (var to in toList) {
                email.To.Add(MailboxAddress.Parse(to));
            }
            if (ccList != null) {
                foreach (var cc in ccList) {
                    email.Cc.Add(MailboxAddress.Parse(cc));
                }
            }
            email.Subject = MyCache.getSetting("MAILSUBJECT");

            string path = "Notification//MailValidator.html";
            var tempBody = File.ReadAllText(path);
            tempBody = tempBody.Replace("MAILHEADER", MyCache.getSetting("MAILHEADER"));
            tempBody = tempBody.Replace("MAILTEXT", MyCache.getSetting("MAILTEXT"));
            var body = tempBody.Replace("MAILKEY", mailValidatorKey);

            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("FindYourSoulMatee@gmail.com", "fbr01994");
            smtp.Send(email);
            smtp.Disconnect(true);

        }

        private /*async Task*/ void SendMail(string mailValidatorKey, string to, string cc = null) {
            var toList = new List<string>() { to };
            var ccList = cc == null ? null : new List<string>() { cc };
            SendMail(mailValidatorKey, toList, ccList);
        }


        async Task IMailSender.SendMail(string mailValidatorKey, List<string> toList, List<string> ccList) => SendMail(mailValidatorKey, toList, ccList);
        async Task IMailSender.SendMail(string mailValidatorKey, string to, string cc) => SendMail(mailValidatorKey, to, cc);

    }
}
