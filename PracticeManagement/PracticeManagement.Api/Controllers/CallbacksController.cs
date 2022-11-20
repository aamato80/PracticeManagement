using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PracticeManagement.Api.Controllers
{
    [ApiController]
    [Route("/api/Callbacks")]
    public class CallbacksController : ControllerBase
    {
        private readonly ILogger<CallbacksController> _logger;

    }
}
