using Microsoft.Extensions.Logging;
using NSubstitute;
using PracticeManagement.Api.Controllers;
using PracticeManagement.Api.Utils;
using PracticeManagement.Api.DTOs;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Api.Services;
using PracticeManagement.Test.Mocks;
using PracticeManagement.Dal.Models;
using FluentValidation;
using PracticeManagement.Api.Validators;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace PracticeManagement.Test.UnitTests.Controllers
{
    public class TestPracticeManagementController
    {
        private readonly IPracticeService _practiceService;
        private readonly ILogger<PracticeManagementController> _logger;
        private readonly IValidator<PracticeDTO> _practiceDtoValidator;
        private PracticeManagementController _controller;


        public TestPracticeManagementController()
        {
            _practiceService = Substitute.For<IPracticeService>();
            _logger = Substitute.For<ILogger<PracticeManagementController>>();
            _practiceDtoValidator = new PracticeDTOValidator();
            _controller = new PracticeManagementController(_logger, _practiceService, _practiceDtoValidator);
        }

        [Fact]
        public async Task AddPractice_WrongRequest_ShouldFails()
        {
            var dto = PracticeDTOMock.Create(id:100);
            dto.FirstName = null;
            var practice = new Practice()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _practiceService.Add(Arg.Any<PracticeDTO>()).Returns(practice);


            var res = await _controller.AddPractice(dto);
            res.Result.Should().BeOfType<BadRequestObjectResult>();
            var result = (BadRequestObjectResult?)res.Result;
            result?.StatusCode.Should().Be(400);
            var errorList = (IEnumerable<FluentValidation.Results.ValidationFailure>?)result?.Value;
            errorList.Should().HaveCount(1);
            errorList?.FirstOrDefault()?.PropertyName.Should().Be(nameof(dto.FirstName));
            errorList?.FirstOrDefault()?.ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Fact]
        public async Task AddPractice_RightRequest_ShoulReturn200()
        {
            var dto = PracticeDTOMock.Create(id: 100);
            var practice = new Practice()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _practiceService.Add(Arg.Any<PracticeDTO>()).Returns(practice);

            var res = await _controller.AddPractice(dto);
            res.Result.Should().BeOfType<OkObjectResult>();
            var result = (OkObjectResult?)res.Result;
            result?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetPractice_RightRequest_ShoulReturn200()
        {
            var dto = PracticeDTOMock.Create(id: 100);
            var practice = new Practice()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _practiceService.Get(Arg.Any<int>()).Returns(practice);


            var res = await _controller.GetPractice(dto.Id.Value);
            res.Result.Should().BeOfType<OkObjectResult>();
            var result = (OkObjectResult?)res.Result;
            result?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetPractice_RequestNotExistingItem_ShoulReturn404()
        {
            var dto = PracticeDTOMock.Create(id: 100);
            var practice = new Practice()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _practiceService.Get(Arg.Any<int>()).Returns((Practice?)null);


            var res = await _controller.AddPractice(dto);
            res.Result.Should().BeOfType<OkObjectResult>();
            var result = (OkObjectResult?)res.Result;
            result?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAttachment_RightRequest_ShoulReturnPdf()
        {
            var attachment = PracticeDTOMock.CreateFakeFormFile();
           
            _practiceService.GetAttachment(Arg.Any<int>()).Returns(attachment.OpenReadStream());


            var res = (FileStreamResult)await _controller.GetAttachment(10);
            res.ContentType.Should().Be("application/pdf");
        }
    }
}