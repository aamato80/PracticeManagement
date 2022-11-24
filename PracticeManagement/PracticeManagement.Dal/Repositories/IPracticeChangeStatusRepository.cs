using PracticeManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement.Dal.Repositories
{
    public interface IPracticeChangeStatusRepository
    {
        Task<PracticeChangeStatus> Add(PracticeChangeStatus entity);
        Task<IEnumerable<PracticeChangeStatus>> GetStatusChanges(int practiceId);
    }
}
