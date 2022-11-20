using PracticeManagement.Api.DTOs;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;

namespace PracticeManagement.Api.Services
{
    public interface IPracticeService
    {
        Task<Practice> Add(PracticeDTO practice);
        Task<int> Update(int practiceId,PracticeDTO practice);
        Task UpdateStatus(int practiceId, PracticeResult result);
        Task<PracticeStatus> GetStatus(int practiceId);

    }
}
