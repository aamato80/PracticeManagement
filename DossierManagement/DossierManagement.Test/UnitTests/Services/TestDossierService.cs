using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using DossierManagement.Api.Attachments;
using DossierManagement.Api.DTOs;
using DossierManagement.Api.Services;
using DossierManagement.Dal;
using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;
using DossierManagement.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using DossierManagement.Api.Exceptions;

namespace DossierManagement.Test.UnitTests.Services
{
    public class TestDossierService
    {
        private IUnitOfWork _unitOfWork;
        private ILogger<DossierService> _logger;
        private IAttachmentManager _attachmentManager;
        private IDossierService _dossierService;
        public TestDossierService()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _attachmentManager = Substitute.For<IAttachmentManager>();
            _logger = Substitute.For<ILogger<DossierService>>();
            _dossierService = new DossierService(_unitOfWork, _logger, _attachmentManager);

        }

        [Fact]
        public async Task Dossier_Add_ShouldWork()
        {
            var dto = DossierDtoMock.CreateRandom();

            var Dossier = new Dossier()
            {
                Id = 0,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _unitOfWork.DossierRepository.Add(Arg.Any<Dossier>()).Returns(Dossier);
            await _dossierService.Add(dto);
            await _unitOfWork.DossierRepository.Received().Add(Arg.Is<Dossier>(x => x.IsEquivalentTo(Dossier)));
            await _unitOfWork.DossierChangeStatusRepository.Received().Add(Arg.Is<DossierChangeStatus>(x => x.dossierId == Dossier.Id));
            _attachmentManager.Received().Save(Arg.Any<Stream>(), Dossier.Id.ToString());
            _unitOfWork.Received(1).Commit();
        }

        [Fact]
        public async Task Dossier_Update_ShouldWork()
        {
            var dto = DossierDtoMock.CreateRandom(id: 10);

            var dossier = new Dossier()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _unitOfWork.DossierRepository.Update(Arg.Any<Dossier>()).Returns(1);
            await _dossierService.Update(dto.Id.Value, dto);
            await _unitOfWork.DossierRepository.Received().Update(Arg.Is<Dossier>(x => x.IsEquivalentTo(dossier)));
            _attachmentManager.Received().Save(Arg.Any<Stream>(), dossier.Id.ToString());
            _unitOfWork.Received(1).Commit();
        }

        [Fact]
        public async Task Dossier_Update_ShouldFailWithNotFound()
        {
            var dto = DossierDtoMock.CreateRandom(id:10);

            var dossier = new Dossier()
            {
                Id =dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            _unitOfWork.DossierRepository.Update(Arg.Any<Dossier>()).Returns(0);
            await Assert.ThrowsAsync<DossierNotFoundException>(async () => await _dossierService.Update(dto.Id.Value, dto));
        }

        [Fact]
        public async Task Dossier_Update_ShouldFailWithIncongruentStatus()
        {
            var dto = DossierDtoMock.CreateRandom(id: 10);

            var dossier = new Dossier()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,
                Status = DossierStatus.InProgress

            };
            _unitOfWork.DossierRepository.Update(Arg.Any<Dossier>()).Returns(1);
            _dossierService.GetStatus(dossier.Id).Returns(dossier.Status);
            await Assert.ThrowsAsync<IncongruentStatusForUpdateException>(async () => await _dossierService.Update(dto.Id.Value, dto));
        }

        [Fact]
        public async Task Dossier_Get_ShouldWork()
        {
            var dto = DossierDtoMock.CreateRandom(10);

            var Dossier = new Dossier()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            var changesStatus = DossierChangeStatusMock.CreateRandomList(Dossier.Id, 3);
            Dossier.StatusChanges = changesStatus;

            _unitOfWork.DossierRepository.Get(dto.Id.Value).Returns(Dossier);
            _unitOfWork.DossierChangeStatusRepository.GetStatusChanges(dto.Id.Value).Returns(changesStatus);
            var result = await _dossierService.Get(dto.Id.Value);


            result.Should().Be(Dossier);
            await _unitOfWork.DossierRepository.Received().Get(dto.Id.Value);

        }

        public async Task Dossier_Get_ShouldReturnNull()
        {
            var dto = DossierDtoMock.CreateRandom(10);

            var Dossier = new Dossier()
            {
                Id = dto.Id.Value,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate.Value,
                FiscalCode = dto.FiscalCode,

            };
            var changesStatus = DossierChangeStatusMock.CreateRandomList(Dossier.Id, 3);
            Dossier.StatusChanges = changesStatus;

            _unitOfWork.DossierRepository.Get(dto.Id.Value).Returns((Dossier)null);
            var result = await _dossierService.Get(dto.Id.Value);


            await _unitOfWork.DossierChangeStatusRepository.DidNotReceive().GetStatusChanges(dto.Id.Value);
            result.Should().Be(null);
            await _unitOfWork.DossierRepository.Received().Get(dto.Id.Value);
        }

        [Theory]
        [InlineData(DossierStatus.Created)]
        [InlineData(DossierStatus.InProgress)]
        [InlineData(DossierStatus.Completed)]

        public async Task Dossier_GetStatus_ShouldReturnCorrectValue(DossierStatus status)
        {

            _unitOfWork.DossierRepository.GetStatus(Arg.Any<int>()).Returns(status);

            var result = await _dossierService.GetStatus(Utils.CreateRandomNumber(10));
            result.Should().Be(status);
        }

        [Fact]

        public async Task Dossier_GetAttachement_LoadShouldBeCalled()
        {

            var dossierId = Utils.CreateRandomNumber(10);
            await _dossierService.GetAttachment(dossierId);

            await _attachmentManager.Received(1).Load(dossierId.ToString());
        }

        [Fact]

        public async Task Dossier2_GetAttachement_LoadShouldBeCalled()
        {

            var dossierId = Utils.CreateRandomNumber(10);
            await _dossierService.GetAttachment(dossierId);

            await _attachmentManager.Received(1).Load(dossierId.ToString());
        }
    }
}
