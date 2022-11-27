using DossierManagement.Api.DTOs;
using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;

namespace DossierManagement.Api.Services
{
    public interface IDossierService
    {
        Task<Dossier> Add(DossierDto Dossier);
        Task<int> Update(int dossierId, DossierDto Dossier);
        Task UpdateStatus(int dossierId, DossierResult result);
        Task<DossierStatus> GetStatus(int dossierId);
        Task<bool> IsStatusCongruentForUpdate(int dossierId);
        Task<Dossier> Get(int dossierId);
        Task<Stream> GetAttachment(int dossierId);

    }
}
