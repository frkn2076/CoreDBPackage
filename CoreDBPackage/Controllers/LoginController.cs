using CoreDBPackage.Config;
using CoreDBPackage.Crypto;
using CoreDBPackage.Exceptions;
using CoreDBPackage.Model;
using CoreDBPackage.Notification;
using CoreDBPackage.ViewModels.Model;
using CoreDBPackage.ViewModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CoreDBPackage.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase {
        private readonly AppDBContext context;
        private readonly IMailSender mailSender;
        private readonly IHttpContextAccessor accessor;

        public LoginController(AppDBContext context, IMailSender mailSender, IHttpContextAccessor accessor) {
            this.context = context;
            this.mailSender = mailSender;
            this.accessor = accessor;
        }

        //[ActionHandler(IdParamName = "fooId")]
        //[AllowAnonymous]
        [HttpPost("register1")]
        //[HttpGet]
        public BaseModel Register1(RegisterRequestViewModel1 request) {

            ////Retrieve the IP Address
            //accessor.HttpContext.Connection.RemoteIpAddress.ToString()


            var user = context.Login.AsNoTracking().FirstOrDefault(x => x.email == request.email);
            if (user != null) {
                throw new DefinedEmailException();
            }
            var cryptodPassword = Encryptor.MD5Hash(request?.password);
            var key = GenerateRandomKey();
            mailSender.SendMail(key, request?.email);

            HttpContext.Session.SetString("MailKey", key);
            HttpContext.Session.SetString("Mail", request?.email);
            HttpContext.Session.SetString("Password", cryptodPassword);

            return new BaseModel() {
                dialogBox = new DialogBoxModel() {
                    message = MyCache.getSetting("InfoHeader"),
                    subMessage = MyCache.getSetting("InfoDescription"),
                    hasTextBox = true,
                    button = new ButtonModel() {
                        type = ConfirmationType.Ok
                    }
                }
            };
        }

        [HttpPost("register2")]
        public BaseModel Register2(RegisterRequestViewModel2 request) {
            var mailKey = HttpContext.Session.GetString("MailKey");
            if(mailKey == request?.mailKey) {
                var login = new Login() {
                    email = HttpContext.Session.GetString("Mail"),
                    password = HttpContext.Session.GetString("Password")
                };
                context.Login.Add(login);
                context.SaveChanges();
                return null;
            }
            throw new WrongEmailKeyException();
        }

        #region Helper

        private string GenerateRandomKey() {
            Random random = new Random();
            int generatedNumber = random.Next(100000, 999999);
            var result = Convert.ToString(generatedNumber);
            return result;
        }

        #endregion
    }
}
