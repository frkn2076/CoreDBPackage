using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreDBPackage.Notification {
    public interface IMailSender {
        Task SendMail(string mailValidatorKey, List<string> toList, List<string> ccList = null);
        Task SendMail(string mailValidatorKey, string to, string cc = null);

    }
}