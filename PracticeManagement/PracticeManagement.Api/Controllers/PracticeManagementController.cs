using Microsoft.AspNetCore.Mvc;
using PracticeManagement.Api.DTOs;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;
using PracticeManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace PracticeManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/Practice")]
    public class PracticeManagementController : ControllerBase
    {
        private readonly ILogger<PracticeManagementController> _logger;
        private readonly IPracticeService _practiceService;

        public PracticeManagementController(
            ILogger<PracticeManagementController> logger,
            IPracticeService practiceService)
        {
            _logger = logger;
            _practiceService = practiceService;
        }

        [HttpPost]
        public async Task<ActionResult<PracticeDTO>> AddPractice([FromForm] PracticeDTO practice)
        {
            if (ModelState.IsValid)
            {
                var savedPractice = await _practiceService.Add(practice);
                return Ok(savedPractice);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{practiceId}")]
        public async Task<ActionResult> UpdatePractice(int practiceId, [FromForm] PracticeDTO practice)
        {
            if (ModelState.IsValid)
            {
                if ((await _practiceService.GetStatus(practiceId)) != PracticeStatus.Created)
                {
                    return Conflict("Cannot modify a Practice in Status different from Created") ;
                }

                var modified = await _practiceService.Update(practiceId, practice);
                if (modified > 0)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{practiceId}")]
        public async Task<ActionResult<PracticeDTO>> GetPractice(int practiceId)
        {
            return Ok();
        }

        [HttpPut("{practiceId}/Status")]
        public async Task<ActionResult<PracticeDTO>> UpdateStatus(int practiceId, [FromBody] UpdateStatusDTO updateStatus)
        {
            try
            {
                await _practiceService.UpdateStatus(practiceId, updateStatus.Result);
            }
            catch (Exception)
            {

                throw;
            }
            
            return Ok();
        }
    }
}