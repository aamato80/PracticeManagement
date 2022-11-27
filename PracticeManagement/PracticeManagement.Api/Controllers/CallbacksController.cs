using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeManagement.Api.DTOs;

namespace PracticeManagement.Api.Controllers
{
    [ApiController]
    [Route("/api/Callbacks")]
    public class CallbacksController : ControllerBase
    {
        private readonly ILogger<CallbacksController> _logger;

        [HttpPost]
        public async Task<ActionResult> ReceiveCallback([FromBody] CallbackDTO callback)
        {
            return Ok(callback);
        }
    }
}
