using CoreDBPackage.Config;
using CoreDBPackage.ViewModels.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CoreDBPackage.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase {
        private readonly AppDBContext context;

        public LoginController(AppDBContext context) {
            this.context = context;
        }

        //[ActionHandler(IdParamName = "fooId")]
        //[AllowAnonymous]
        //[HttpPost("register")]
        [HttpGet]
        public string Register1(/*RegisterRequestViewModel request*/) {
            //try {
            //    var user = context.Login.AsNoTracking().FirstOrDefault(x => x.email == request.email);
            //    if (user != null) {
            //        throw new 
            //    }
            //    var cryptodPassword = encryptor.MD5Hash(password);
            //    var key = GenerateRandomKey();
            //    mailSender.SendMail(key, email);
            //    var login = new Login() {
            //        Email = email,
            //        Password = cryptodPassword,
            //        MailKey = key,
            //        IsActive = false
            //    };
            //    context.Login.Add(login);
            //    context.SaveChanges();
            //    return model;
            //}
            //catch (Exception ex) {
            //    model.isError = true;
            //    model.errorDescription = "Beklenmeyen hata";
            //    return model;
            //}
            MyCache.getSetting("ErrorHeader");
            return null;
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
