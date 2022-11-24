using PracticeManagement.Dal.Repositories;

namespace PracticeManagement.Dal
{
    public interface IUnitOfWork : IDisposable
    {
        IPracticeRepository PracticeRepository { get; }
        IPracticeChangeStatusRepository PracticeChangeStatusRepository { get; }

        void Commit();
    }
}
