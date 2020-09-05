using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CoreDBPackage.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase {
        private readonly AppDBContext _context;


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDBContext context) {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public Users Get() {

            try {
                var user = _context.Users.FirstOrDefault();
                return user;
            }
            catch(Exception ex) {
                return new Users() { id=1,name="dummy",surname="dummy"};
            }

        }
    }
}
