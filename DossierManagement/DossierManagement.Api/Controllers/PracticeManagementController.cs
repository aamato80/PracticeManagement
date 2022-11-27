using Microsoft.AspNetCore.Mvc;
using DossierManagement.Api.DTOs;
using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;
using DossierManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using DossierManagement.Api.Exceptions;
using DossierManagement.Api.Mappers;

namespace DossierManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/Dossier")]
    public class DossierManagementController : ControllerBase
    {
        private readonly ILogger<DossierManagementController> _logger;
        private readonly IDossierService _dossierService;
        private readonly IValidator<DossierDto> _dossierDtoValidator;

        public DossierManagementController(
            ILogger<DossierManagementController> logger,
            IDossierService DossierService,
            IValidator<DossierDto> dossierDtoValidator)
        {
            _logger = logger;
            _dossierService = DossierService;
            _dossierDtoValidator = dossierDtoValidator;
        }

        [HttpPost]
        public async Task<ActionResult<AddedDossierResponseDTO>> AddDossier([FromForm] DossierDto Dossier)
        {
            _logger.LogInformation("Add Dossier endpoint called");
            var validation = _dossierDtoValidator.Validate(Dossier);
            if (validation.IsValid)
            {
                var savedDossier = await _dossierService.Add(Dossier);
                var dossierResponseDto = DossierMapper.MapToAdd(savedDossier);

                _logger.LogInformation("Add Dossier endpoint executed");
                return Ok(dossierResponseDto);
            }
            else
            {
                _logger.LogInformation("Add Dossier endpoint bad request");
                return BadRequest(validation.Errors);
            }
        }

        [HttpPut("{dossierId}")]
        public async Task<ActionResult> UpdateDossier(int dossierId, [FromForm] DossierDto dossier)
        {
            var validation = _dossierDtoValidator.Validate(dossier);
            if (validation.IsValid)
            {
                try
                {
                    await _dossierService.Update(dossierId, dossier);
                    return Ok();
                }
                catch (DossierNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (IncongruentStatusForUpdateException ex)
                {
                    return Conflict(ex.Message);
                }
            }
            else
            {
                return BadRequest(validation.Errors);
            }
        }

        [HttpGet("{dossierId}")]
        public async Task<ActionResult<GetDossierResponseDTO>> GetDossier(int dossierId)
        {
            var dossier = await _dossierService.Get(dossierId);
            if (dossier != null)
            {
                var dossierResponse = DossierMapper.MapToGet(dossier);
                return Ok(dossierResponse);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut("{dossierId}/Status")]
        public async Task<ActionResult<DossierDto>> UpdateStatus(int dossierId, [FromBody] UpdateStatusDTO updateStatus)
        {
            try
            {
                await _dossierService.UpdateStatus(dossierId, updateStatus.Result);
            }
            catch (IncongruentStatusForUpdateException ex)
            {
                return Conflict(ex.Message);
            }

            return Ok();
        }


        [HttpGet("{dossierId}/Attachment")]
        public async Task<ActionResult> GetAttachment(int dossierId)
        {
            var attachment = await _dossierService.GetAttachment(dossierId);
            return File(attachment, "application/pdf");
        }

    }
}