using CoreDBPackage.CCC;
using CoreDBPackage.ViewModels.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreDBPackage.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Exception : ControllerBase {
        private readonly AppDBContext _context;
        private readonly ILogger<Login> _logger;

        public Exception(ILogger<Login> logger, AppDBContext context) {
            _logger = logger;
            _context = context;
        }

        [Route("error")]
        public BaseModel Error() {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception

            //logging

            Response.StatusCode = 400; // You can use HttpStatusCode enum instead

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
