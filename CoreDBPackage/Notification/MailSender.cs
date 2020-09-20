using CoreDBPackage.Config;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreDBPackage.Notification {
    public class MailSender : IMailSender {
        private async Task SendMail(string mailValidatorKey, List<string> toList, List<string> ccList = null) {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;

            sc.Credentials = new NetworkCredential("FindYourSoulMatee@gmail.com", "ccbsdjwtwmrsmxcx");

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("ozturkfurkan1994@gmail.com", "Ekranda Görünecek İsim");


            foreach (var to in toList) {
                mail.To.Add(to);
            }
            if (ccList != null) {
                foreach (var cc in ccList) {
                    mail.CC.Add(cc);
                }
            }

            mail.Subject = "SOULMATEE";
            mail.IsBodyHtml = true;
            string path = "Notification//MailValidator.html";
            var tempBody = File.ReadAllText(path);
            tempBody = tempBody.Replace("MAILHEADER", MyCache.getSetting("MAILHEADER"));
            tempBody = tempBody.Replace("MAILTEXT", MyCache.getSetting("MAILTEXT"));
            var body = tempBody.Replace("MAILKEY", mailValidatorKey);
            mail.Body = body;

            //mail.Attachments.Add(new Attachment(@"C:\Rapor.xlsx"));
            //mail.Attachments.Add(new Attachment(@"C:\Sonuc.pptx"));

            sc.Send(mail);
        }

        private async Task SendMail(string mailValidatorKey, string to, string cc = null) {
            var toList = new List<string>() { to };
            var ccList = cc == null ? null : new List<string>() { cc };
            SendMail(mailValidatorKey, toList, ccList);
        }


        async Task IMailSender.SendMail(string mailValidatorKey, List<string> toList, List<string> ccList) => SendMail(mailValidatorKey, toList, ccList);
        async Task IMailSender.SendMail(string mailValidatorKey, string to, string cc) => SendMail(mailValidatorKey, to, cc);

    }
}
