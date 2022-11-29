using Dapper;
using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DossierManagement.Dal.Repositories
{
    public class DossierChangeStatusRepository : BaseRepository, IDossierChangeStatusRepository
    {
        public DossierChangeStatusRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<DossierChangeStatus> Add(DossierChangeStatus entity)
        {

            await Connection.ExecuteAsync(
              @"INSERT INTO DossierChangeStatus(DossierId,Status,Result,Date) 
                 VALUES(@dossierId, @Status,@Result,@Date)",
              param: new
              {
                  DossierId = entity.DossierId,
                  Status = entity.Status,
                  Result = entity.Result,
                  Date = DateTime.UtcNow,
              },
              transaction: Transaction
           );
            return entity;
        }
        public async Task<IEnumerable<DossierChangeStatus>> GetStatusChanges(int dossierId)
        {
            var entities = await Connection.QueryAsync<DossierChangeStatus>(
                @"Select * FROM DossierChangeStatus pcs WHERE pcs.dossierId =@dossierId",
                transaction: Transaction,
                param: new
                {
                    dossierId = dossierId
                });
            return entities;
        }
    }
}
