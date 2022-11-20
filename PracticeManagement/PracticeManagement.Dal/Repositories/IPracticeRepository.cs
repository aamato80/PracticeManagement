using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;

namespace PracticeManagement.Dal.Repositories
{
    public interface IPracticeRepository
    {
        Task<Practice> Add(Practice entity);
        Task<int> Update(Practice entity);
        Task UpdateStatus(int id, PracticeStatus status,PracticeResult result);
        Task<PracticeStatus> GetStatus(int id);


    }
}
