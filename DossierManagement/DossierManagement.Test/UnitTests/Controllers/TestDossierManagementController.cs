using Microsoft.Extensions.Logging;
using NSubstitute;
using DossierManagement.Api.Controllers;
using DossierManagement.Api.Utils;
using DossierManagement.Api.DTOs;
using DossierManagement.Dal.Enums;
using DossierManagement.Api.Services;
using DossierManagement.Test.Mocks;
using DossierManagement.Dal.Models;
using FluentValidation;
using DossierManagement.Api.Validators;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DossierManagement.Test.UnitTests.Controllers
{
    public class TestDossierManagementController
    {
        private readonly IDossierService _dossierService;
        private readonly ILogger<DossierManagementController> _logger;
        private readonly IValidator<DossierDto> _dossierDtoValidator;
        private DossierManagementController _controller;


        public TestDossierManagementController()
        {
            _dossierService = Substitute.For<IDossierService>();
            _logger = Substitute.For<ILogger<DossierManagementController>>();
            _dossierDtoValidator = new DossierDtoValidator();
            _controller = new DossierManagementController(_logger, _dossierService, _dossierDtoValidator);
        }

        [Fact]
        public async Task AddDossier_WrongRequest_ShouldFails()
        {
            var dto = DossierDtoMock.Create(id:100);
            dto.FirstName = null;
            var Dossier = new Dossier()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _dossierService.Add(Arg.Any<DossierDto>()).Returns(Dossier);


            var res = await _controller.AddDossier(dto);
            res.Result.Should().BeOfType<BadRequestObjectResult>();
            var result = (BadRequestObjectResult?)res.Result;
            result?.StatusCode.Should().Be(400);
            var errorList = (IEnumerable<FluentValidation.Results.ValidationFailure>?)result?.Value;
            errorList.Should().HaveCount(1);
            errorList?.FirstOrDefault()?.PropertyName.Should().Be(nameof(dto.FirstName));
            errorList?.FirstOrDefault()?.ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Fact]
        public async Task AddDossier_RightRequest_ShoulReturn200()
        {
            var dto = DossierDtoMock.Create(id: 100);
            var Dossier = new Dossier()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _dossierService.Add(Arg.Any<DossierDto>()).Returns(Dossier);

            var res = await _controller.AddDossier(dto);
            res.Result.Should().BeOfType<OkObjectResult>();
            var result = (OkObjectResult?)res.Result;
            result?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetDossier_RightRequest_ShoulReturn200()
        {
            var dto = DossierDtoMock.Create(id: 100);
            var Dossier = new Dossier()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _dossierService.Get(Arg.Any<int>()).Returns(Dossier);


            var res = await _controller.GetDossier(dto.Id.Value);
            res.Result.Should().BeOfType<OkObjectResult>();
            var result = (OkObjectResult?)res.Result;
            result?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetDossier_RequestNotExistingItem_ShoulReturn404()
        {
            var dto = DossierDtoMock.Create(id: 100);
            var Dossier = new Dossier()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _dossierService.Get(Arg.Any<int>()).Returns((Dossier?)null);


            var res = await _controller.AddDossier(dto);
            res.Result.Should().BeOfType<OkObjectResult>();
            var result = (OkObjectResult?)res.Result;
            result?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAttachment_RightRequest_ShoulReturnPdf()
        {
            var attachment = DossierDtoMock.CreateFakeFormFile();
           
            _dossierService.GetAttachment(Arg.Any<int>()).Returns(attachment.OpenReadStream());


            var res = (FileStreamResult)await _controller.GetAttachment(10);
            res.ContentType.Should().Be("application/pdf");
        }
    }
}