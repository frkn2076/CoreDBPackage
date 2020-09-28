using CoreDBPackage.DTO;
using CoreDBPackage.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CoreDBPackage.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase {

        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext) {
            _hubContext = hubContext;
        }

        [Route("send")]
        [HttpPost]
        public IActionResult SendRequest([FromBody] MessageDto msg) {
            _hubContext.Clients.All.SendAsync("SendNoticeEventToClient", msg.msgText);

            return Ok();
        }
    }
}
