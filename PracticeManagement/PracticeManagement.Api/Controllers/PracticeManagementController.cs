using Microsoft.AspNetCore.Mvc;
using PracticeManagement.Api.DTOs;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;
using PracticeManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace PracticeManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/Practice")]
    public class PracticeManagementController : ControllerBase
    {
        private readonly ILogger<PracticeManagementController> _logger;
        private readonly IPracticeService _practiceService;
        private readonly IValidator<PracticeDTO> _practiceDtoValidator;

        public PracticeManagementController(
            ILogger<PracticeManagementController> logger,
            IPracticeService practiceService,
            IValidator<PracticeDTO> practiceDtoValidator)
        {
            _logger = logger;
            _practiceService = practiceService;
            _practiceDtoValidator = practiceDtoValidator;
        }

        [HttpPost]
        public async Task<ActionResult<PracticeDTO>> AddPractice([FromForm] PracticeDTO practice)
        {
            _logger.LogInformation("Add practice endpoint called");
            var validation = _practiceDtoValidator.Validate(practice);
            if (validation.IsValid)
            {
                var savedPractice = await _practiceService.Add(practice);
                _logger.LogInformation("Add practice endpoint executed");
                return Ok(savedPractice);
            }
            else
            {
                _logger.LogInformation("Add practice endpoint bad request");
                return BadRequest(validation.Errors);
            }
        }

        [HttpPut("{practiceId}")]
        public async Task<ActionResult> UpdatePractice(int practiceId, [FromForm] PracticeDTO practice)
        {
           var validation = _practiceDtoValidator.Validate(practice);
            if (validation.IsValid)
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
                return BadRequest(validation.Errors);
            }
        }

        [HttpGet("{practiceId}")]
        public async Task<ActionResult<Practice>> GetPractice(int practiceId)
        {
            var practice = await _practiceService.Get(practiceId);
            if (practice !=null)
            {
                return Ok(practiceId);
            }
            else
            {
                return NotFound();
            }
                
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


        [HttpGet("{practiceId}/Attachment")]
        public async Task<ActionResult> GetAttachment(int practiceId)
        {
          var attachment = await _practiceService.GetAttachment(practiceId);
            return File(attachment,"application/pdf");
        }

    }
}