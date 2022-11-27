using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PracticeManagement.Api.Attachments;
using PracticeManagement.Api.DTOs;
using PracticeManagement.Api.Services;
using PracticeManagement.Dal;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;
using PracticeManagement.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement.Test.UnitTests.Services
{
    public class TestPracticeService
    {
        private IUnitOfWork _unitOfWork;
        private ILogger<PracticeService> _logger;
        private IAttachmentManager _attachmentManager;
        private IPracticeService _practiceService;
        public TestPracticeService()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _attachmentManager = Substitute.For<IAttachmentManager>();
            _logger = Substitute.For<ILogger<PracticeService>>();
            _practiceService = new PracticeService(_unitOfWork, _logger, _attachmentManager);

        }

        [Fact]
        public async Task Practice_Add_ShouldWork()
        {
            var dto = PracticeDTOMock.CreateRandom();

            var practice = new Practice()
            {
                Id = 0,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _unitOfWork.PracticeRepository.Add(Arg.Any<Practice>()).Returns(practice);
            await _practiceService.Add(dto);
            await _unitOfWork.PracticeRepository.Received().Add(Arg.Is<Practice>(x => x.IsEquivalentTo(practice)));
            await _unitOfWork.PracticeChangeStatusRepository.Received().Add(Arg.Is<PracticeChangeStatus>(x => x.PracticeId == practice.Id));
            _attachmentManager.Received().Save(Arg.Any<Stream>(), practice.Id.ToString());
            _unitOfWork.Received(1).Commit();
        }

        [Fact]
        public async Task Practice_Update_ShouldWork()
        {
            var dto = PracticeDTOMock.CreateRandom();

            var practice = new Practice()
            {
                Id = 0,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _unitOfWork.PracticeRepository.Add(Arg.Any<Practice>()).Returns(practice);
            await _practiceService.Add(dto);
            await _unitOfWork.PracticeRepository.Received().Add(Arg.Is<Practice>(x => x.IsEquivalentTo(practice)));
            await _unitOfWork.PracticeChangeStatusRepository.Received().Add(Arg.Is<PracticeChangeStatus>(x => x.PracticeId == practice.Id));
            _attachmentManager.Received().Save(Arg.Any<Stream>(), practice.Id.ToString());
            _unitOfWork.Received(1).Commit();
        }

        [Fact]
        public async Task Practice_Get_ShouldWork()
        {
            var dto = PracticeDTOMock.CreateRandom(10);

            var practice = new Practice()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            var changesStatus = PracticeChangeStatusMock.CreateRandomList(practice.Id, 3);
            practice.StatusChanges = changesStatus;

            _unitOfWork.PracticeRepository.Get(dto.Id.Value).Returns(practice);
            _unitOfWork.PracticeChangeStatusRepository.GetStatusChanges(dto.Id.Value).Returns(changesStatus);
            var result = await _practiceService.Get(dto.Id.Value);


            result.Should().Be(practice);
            await _unitOfWork.PracticeRepository.Received().Get(dto.Id.Value);

        }

        public async Task Practice_Get_ShouldReturnNull()
        {
            var dto = PracticeDTOMock.CreateRandom(10);

            var practice = new Practice()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            var changesStatus = PracticeChangeStatusMock.CreateRandomList(practice.Id, 3);
            practice.StatusChanges = changesStatus;

            _unitOfWork.PracticeRepository.Get(dto.Id.Value).Returns((Practice)null);
            var result = await _practiceService.Get(dto.Id.Value);


            await _unitOfWork.PracticeChangeStatusRepository.DidNotReceive().GetStatusChanges(dto.Id.Value);
            result.Should().Be(null);
            await _unitOfWork.PracticeRepository.Received().Get(dto.Id.Value);
        }

        [Theory]
        [InlineData(PracticeStatus.Created)]
        [InlineData(PracticeStatus.InProgress)]
        [InlineData(PracticeStatus.Completed)]

        public async Task Practice_GetStatus_ShouldReturnCorrectValue(PracticeStatus status)
        {

            _unitOfWork.PracticeRepository.GetStatus(Arg.Any<int>()).Returns(status);

            var result = await _practiceService.GetStatus(Utils.CreateRandomNumber(10));
            result.Should().Be(status);
        }

        [Fact]

        public async Task Practice_GetAttachement_LoadShouldBeCalled()
        {

            var practiceId = Utils.CreateRandomNumber(10);
            await _practiceService.GetAttachment(practiceId);

            await _attachmentManager.Received(1).Load(practiceId.ToString());
        }
    }
}
