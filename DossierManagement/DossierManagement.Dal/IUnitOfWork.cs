using DossierManagement.Dal.Repositories;

namespace DossierManagement.Dal
{
    public interface IUnitOfWork : IDisposable
    {
        IDossierRepository DossierRepository { get; }
        IDossierChangeStatusRepository DossierChangeStatusRepository { get; }
        void Commit();
    }
}
