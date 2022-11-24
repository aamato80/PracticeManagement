using Microsoft.Extensions.Logging;
using NSubstitute;
using PracticeManagement.Api.Controllers;
using PracticeManagement.Api.Utils;
using PracticeManagement.Api.DTOs;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Api.Services;
using PracticeManagement.Test.Mocks;

namespace PracticeManagement.Test.UnitTests
{
    public class TestPracticeManagementController
    {
        private readonly IPracticeService _practiceService;
        private readonly ILogger<PracticeManagementController> _logger;


        public TestPracticeManagementController()
        {
            _practiceService = Substitute.For<IPracticeService>();
            _logger = Substitute.For<ILogger<PracticeManagementController>>();
        }

        [Fact]
        public async Task AddPractice_WrongRequest_ShouldFails()
        {
            
            //var practice = AddPracticeRequestMock.Create();
            //practice.FirstName = null;
            //_practiceService.Add(Arg.Any<PracticeDTO>()).Returns(practice);

            //var controller = new PracticeManagementController(_logger, _practiceService);
          
            //var res = await controller.AddPractice(practice);
        }

    
    }
}