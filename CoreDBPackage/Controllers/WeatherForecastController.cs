using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreDBPackage.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase {
        private readonly AppDBContext _context;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDBContext context) {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public Users Get() {
            //_context.Users.Add(new Users() { id = 2, name = "asda", surname = "asdasd" });
            //_context.SaveChanges();

            try {
                var user =  _context.Users.First();
                return user;
            }
            catch(Exception ex) {
                return new Users() { id=1,name="dummy",surname="dummy"};
            }

        }
    }
}
