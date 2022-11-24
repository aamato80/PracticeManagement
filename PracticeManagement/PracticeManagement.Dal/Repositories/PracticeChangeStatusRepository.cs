using Dapper;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace PracticeManagement.Dal.Repositories
{
    public class PracticeChangeStatusRepository : BaseRepository, IPracticeChangeStatusRepository
    {
        public PracticeChangeStatusRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<PracticeChangeStatus> Add(PracticeChangeStatus entity)
        {

            await Connection.ExecuteAsync(
              @"INSERT INTO PracticeChangeStatus(PracticeId,Status,Result,Date) 
                 VALUES(@PracticeId, @Status,@Result,@Date)",
              param: new
              {
                  PracticeId = entity.PracticeId,
                  Status = entity.Status,
                  Result = entity.Result,
                  Date = DateTime.UtcNow,
              },
              transaction: Transaction
           );
            return entity;
        }
        public async Task<IEnumerable<PracticeChangeStatus>> GetStatusChanges(int practiceId)
        {
            var entities = await Connection.QueryAsync<PracticeChangeStatus>(
                @"Select * FROM PracticeChangeStatus pcs WHERE pcs.PracticeId =@PracticeId",
                transaction: Transaction,
                param: new
                {
                    PracticeId = practiceId
                });
            return entities;
        }
    }
}
