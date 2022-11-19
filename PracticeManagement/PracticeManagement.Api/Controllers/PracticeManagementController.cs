using Microsoft.AspNetCore.Mvc;
using PracticeManagement.Api.Models;

namespace PracticeManagement.Api.Controllers
{
    [ApiController]
    [Route("/api/Practice")]
    public class PracticeManagementController : ControllerBase
    {
        private readonly ILogger<PracticeManagementController> _logger;

        public PracticeManagementController(ILogger<PracticeManagementController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async IEnumerable<WeatherForecast> AddPractice([FromForm] PracticeDTO practice)
        {
            
        }

        [HttpPut]
        public async IEnumerable<WeatherForecast> UpdatePractice([FromForm] PracticeDTO practice)
        {
           
        }

        [HttpGet("{practiceId}")]
        public async IEnumerable<WeatherForecast> GetPractice(int practiceId)
        {
            
        }
    }
}