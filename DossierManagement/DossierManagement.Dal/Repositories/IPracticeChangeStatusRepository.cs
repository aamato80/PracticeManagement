using DossierManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierManagement.Dal.Repositories
{
    public interface IDossierChangeStatusRepository
    {
        Task<DossierChangeStatus> Add(DossierChangeStatus entity);
        Task<IEnumerable<DossierChangeStatus>> GetStatusChanges(int dossierId);
    }
}
