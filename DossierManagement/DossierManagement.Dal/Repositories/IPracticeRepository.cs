using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;

namespace DossierManagement.Dal.Repositories
{
    public interface IDossierRepository
    {
        Task<Dossier> Add(Dossier entity);
        Task<Dossier> Get(int id);
        Task<int> Update(Dossier entity);
        Task UpdateStatus(int id, DossierStatus status,DossierResult result);
        Task<DossierStatus> GetStatus(int id);


    }
}
