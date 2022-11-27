using DossierManagement.Api.Attachments;
using DossierManagement.Api.DTOs;
using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;
using DossierManagement.Api.Utils;
using System.Drawing;
using System.Linq.Expressions;
using System.Xml.Linq;
using DossierManagement.Dal;
using DossierManagement.Dal.Repositories;
using DossierManagement.Api.Exceptions;

namespace DossierManagement.Api.Services
{
    public class DossierService : IDossierService
    {
        private readonly ILogger<DossierService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentManager _attachmentManager;

        public DossierService(
            IUnitOfWork unitOfWork,
            ILogger<DossierService> logger,
            IAttachmentManager attachmentManager
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _attachmentManager = attachmentManager;
        }

        public async Task<Dossier> Add(DossierDto dossierDto)
        {
            try
            {
                var dossier = await _unitOfWork.DossierRepository.Add(new Dossier()
                {
                    FirstName = dossierDto.FirstName,
                    LastName = dossierDto.LastName,
                    BirthDate = dossierDto.BirthDate.Value,
                    FiscalCode = dossierDto.FiscalCode
                });
                await _unitOfWork.DossierChangeStatusRepository.Add(new DossierChangeStatus()
                {
                    dossierId = dossier.Id,
                });
                _attachmentManager.Save(dossierDto.Attachment.OpenReadStream(), dossier.Id.ToString());

                _unitOfWork.Commit();
                return dossier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                throw;
            }
        }

        public async Task<Dossier> Get(int id)
        {
            try
            {
                var dossier = await _unitOfWork.DossierRepository.Get(id);
                if (dossier != null)
                {
                    dossier.StatusChanges = await _unitOfWork.DossierChangeStatusRepository.GetStatusChanges(id);
                    return dossier;
                }
                else
                {
                   throw new DossierNotFoundException(ErrorMessages.DossierNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task UpdateStatus(int dossierId, DossierResult result)
        {
            try
            {
                var status = await GetStatus(dossierId);
                var newStatus = status.GetNewValue();

                if (!IsResultCongruentWithStatus(newStatus, result))
                {
                    throw new NotCongruentDossierResultException(ErrorMessages.NotCongruentDossierResult);
                }

                await _unitOfWork.DossierRepository.UpdateStatus(dossierId, status, result);
                await _unitOfWork.DossierChangeStatusRepository.Add(new DossierChangeStatus()
                {
                    Result = result,
                    Status = newStatus,
                    dossierId = dossierId
                });
                _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<DossierStatus> GetStatus(int dossierId)
        {
            try
            {
                return await _unitOfWork.DossierRepository.GetStatus(dossierId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }

        public async Task<int> Update(int dossierId, DossierDto dossierDto)
        {
            try
            {
                if (!(await IsStatusCongruentForUpdate(dossierId)))
                {
                    throw new IncongruentStatusForUpdateException();
                }

                var modified = await _unitOfWork.DossierRepository.Update(new Dossier()
                {
                    Id = dossierId,
                    FirstName = dossierDto.FirstName,
                    LastName = dossierDto.LastName,
                    BirthDate = dossierDto.BirthDate.Value,
                    FiscalCode = dossierDto.FiscalCode
                });
                if (modified > 0)
                {
                    _attachmentManager.Save(dossierDto.Attachment.OpenReadStream(), dossierId.ToString());
                    _unitOfWork.Commit();
                    return modified;
                }
                else
                {
                    throw new DossierNotFoundException(ErrorMessages.DossierNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private bool IsResultCongruentWithStatus(DossierStatus newStatus, DossierResult result)
        {
            return newStatus != DossierStatus.Completed && result == DossierResult.None
                || newStatus == DossierStatus.Completed && result != DossierResult.None;
        }

        public async Task<Stream> GetAttachment(int dossierId)
        {
            try
            {
                return await _attachmentManager.Load(dossierId.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsStatusCongruentForUpdate(int dossierId)
        {
            return (await GetStatus(dossierId)) == DossierStatus.Created;
        }
    }
}
