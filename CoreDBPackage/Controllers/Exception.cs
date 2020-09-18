using CoreDBPackage.CCC;
using CoreDBPackage.Exceptions;
using CoreDBPackage.ViewModels.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CoreDBPackage.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Exception : ControllerBase {
        private readonly AppDBContext _context;
        private readonly ILogger<LoginController> _logger;

        public Exception(ILogger<LoginController> logger, AppDBContext context) {
            _logger = logger;
            _context = context;
        }

        [Route("error")]
        public BaseModel Error() {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception

            if(exception is DefinedException) {
                _logger.Log(LogLevel.Warning, exception.Message);

                return new BaseModel() {
                    isError = true,
                    error = new DialogBoxModel() {
                        message = "HATA",
                        subMessage = exception.Message
                    }
                };
            }

            _logger.Log(LogLevel.Error,string.Concat(exception.Message, Environment.NewLine));

            //Response.StatusCode = 400; // You can use HttpStatusCode enum instead

            return new BaseModel() {
                isError = true,
                error = new DialogBoxModel() {
                    message = "HATA",
                    subMessage = "Geçici süre işleminizi gerçekleştiremiyoruz."
                }
            };
        }
    }
}
