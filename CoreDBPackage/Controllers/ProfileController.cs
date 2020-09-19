using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CoreDBPackage.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase {
        private readonly AppDBContext context;
        private readonly IHttpContextAccessor accessor;
        private readonly IWebHostEnvironment environment;

        public ProfileController(AppDBContext context, IHttpContextAccessor accessor, IWebHostEnvironment environment) {
            this.context = context;
            this.accessor = accessor;
            this.environment = environment;
        }


        //[ActionHandler(IdParamName = "fooId")]
        //[AllowAnonymous]
        [HttpPost("profile1")]
        //[HttpGet]
        public async Task Profile1(IFormFile file) {
            var uploads = Path.Combine("wwwroot", "..//Uploads");
            if (file.Length > 0) {
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }
            }
        }
    }
}
