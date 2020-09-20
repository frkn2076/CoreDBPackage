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

        /// <summary>Sets profile photo.
        /// <para></para>
        /// <seealso cref="ProfileController"/>
        /// </summary>
        [HttpPost("profile1")]
        public async Task Profile1(IFormFile file) {
            var user = HttpContext.Session.GetString("User");
            var uploads = Path.Combine("wwwroot", "..//Uploads");
            if (file.Length > 0) {
                using (var fileStream = new FileStream(Path.Combine(uploads, string.Concat(user, ".jpg")), FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

        /// <summary>Gets profile photo.
        /// <para></para>
        /// <seealso cref="ProfileController"/>
        /// </summary>
        [HttpGet("profile2")]
        public IActionResult Profile2() {
            var user = HttpContext.Session.GetString("User");
            var uploads = Path.Combine("wwwroot", "..//Uploads");
            var image = System.IO.File.ReadAllBytes(Path.Combine(uploads, string.Concat(user, ".jpg")));
            return File(image, "image/jpeg");
        }
    }
}
