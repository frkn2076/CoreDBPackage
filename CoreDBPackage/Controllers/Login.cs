using CoreDBPackage.CCC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreDBPackage.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class Login : ControllerBase {
        private readonly AppDBContext _context;


        private readonly ILogger<Login> _logger;

        public Login(ILogger<Login> logger, AppDBContext context) {
            _logger = logger;
            _context = context;
        }

        //[ActionHandler(IdParamName = "fooId")]
        //[AllowAnonymous]
        //[HttpPost("register")]
        [HttpGet]
        public string Register1(/*RegisterRequestViewModel request*/) {
            var a = new Users();
            a = null;
            return a.name;

        }
    }
}
