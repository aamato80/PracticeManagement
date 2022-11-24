using PracticeManagement.Api.Attachments;
using PracticeManagement.Api.DTOs;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;
using PracticeManagement.Api.Utils;
using System.Drawing;
using System.Linq.Expressions;
using System.Xml.Linq;
using PracticeManagement.Dal;

namespace PracticeManagement.Api.Services
{
    public class PracticeService : IPracticeService
    {
        private readonly ILogger<PracticeService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentManager _attachmentManager;

        public PracticeService(
            IUnitOfWork unitOfWork,
            ILogger<PracticeService> logger,
            IAttachmentManager attachmentManager
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _attachmentManager = attachmentManager;
        }

        public async Task<Practice> Add(PracticeDTO practiceDto)
        {
            try
            {
                var practice = await _unitOfWork.PracticeRepository.Add(new Practice()
                {
                    FirstName = practiceDto.FirstName,
                    LastName = practiceDto.LastName,
                    BirthDate = practiceDto.BirthDate,
                    FiscalCode = practiceDto.FiscalCode
                });
                await _unitOfWork.PracticeChangeStatusRepository.Add(new PracticeChangeStatus()
                {
                    PracticeId = practice.Id,
                });
                _attachmentManager.Save(practiceDto.Attachment.OpenReadStream(), practice.Id.ToString());

                _unitOfWork.Commit();
                return practice;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Practice> Get(int id)
        {
            try
            {
                var practice = await _unitOfWork.PracticeRepository.Get(id);
                practice.StatusChanges =await _unitOfWork.PracticeChangeStatusRepository.GetStatusChanges(id);
                return practice;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateStatus(int practiceId, PracticeResult result)
        {
            try
            {
                var status = await GetStatus(practiceId);
                var newStatus = status.GetNewValue();

                if (!IsResultCongruentWithStatus(newStatus,result))
                {
                    throw new Exception();
                }

                await _unitOfWork.PracticeRepository.UpdateStatus(practiceId, status, result);
                await _unitOfWork.PracticeChangeStatusRepository.Add(new PracticeChangeStatus()
                {
                    Result = result,
                    Status = newStatus,
                    PracticeId = practiceId
                });
                _unitOfWork.Commit();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PracticeStatus> GetStatus(int practiceId)
        {
            try
            {
                return await _unitOfWork.PracticeRepository.GetStatus(practiceId);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(int practiceId, PracticeDTO practiceDto)
        {
            try
            {
                var modified = await _unitOfWork.PracticeRepository.Update(new Practice()
                {
                    Id = practiceId,
                    FirstName = practiceDto.FirstName,
                    LastName = practiceDto.LastName,
                    BirthDate = practiceDto.BirthDate,
                    FiscalCode = practiceDto.FiscalCode
                });
                _attachmentManager.Save(practiceDto.Attachment.OpenReadStream(), practiceId.ToString());
                _unitOfWork.Commit();
                return modified;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsResultCongruentWithStatus(PracticeStatus newStatus,PracticeResult result)
        {
            return newStatus != PracticeStatus.Completed && result == PracticeResult.None
                || newStatus == PracticeStatus.Completed && result != PracticeResult.None;
        }

        public async Task<Stream> GetAttachment(int practiceId)
        {
            try
            {
               return await _attachmentManager.Load(practiceId.ToString());

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
