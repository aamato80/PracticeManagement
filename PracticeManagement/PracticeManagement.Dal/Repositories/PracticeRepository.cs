using Dapper;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;
using System.Data;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace PracticeManagement.Dal.Repositories
{
    public class PracticeRepository : BaseRepository, IPracticeRepository
    {
        public PracticeRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<Practice> Add(Practice entity)
        {
            entity.Status = PracticeStatus.Created;
            entity.Result = PracticeResult.None;
            entity.Id = await Connection.ExecuteScalarAsync<int>(
               @"INSERT INTO Practices(FirstName,LastName,FiscalCode,BirthDate,Status,Result) 
                 VALUES(@FirstName, @LastName,@FiscalCode,@BirthDate,@Status,@Result); SELECT SCOPE_IDENTITY()",
               param: new
               {
                   FirstName = entity.FirstName,
                   LastName = entity.LastName,
                   FiscalCode = entity.FiscalCode,
                   BirthDate = entity.BirthDate,
                   Status = (int)PracticeStatus.Created,
                   Result = (int)PracticeResult.None,
               },
               transaction: Transaction
            );
            return entity;
        }

        public async Task<PracticeStatus> GetStatus(int id)
        {
            try
            {
                var status = await Connection.QueryFirstOrDefaultAsync<PracticeStatus>("Select Status From Practices WHERE Id = @Id",
                param: new { Id = id },
                transaction: Transaction
                );
                return status;
            }
             
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<int> Update(Practice entity)
        {
            return await Connection.ExecuteAsync(
                 "UPDATE Practices SET FirstName = @FirstName, LastName = @LastName, FiscalCode = @FiscalCode, BirthDate = @BirthDate " +
                 "WHERE Id = @Id",
                 param: new
                 {
                     Id = entity.Id,
                     FirstName = entity.FirstName,
                     LastName = entity.LastName,
                     FiscalCode = entity.FiscalCode,
                     BirthDate = entity.BirthDate
                 },
                 transaction: Transaction
             );
        }

        public async Task UpdateStatus(int id, PracticeStatus status, PracticeResult result)
        {
            await Connection.ExecuteAsync(
                 "UPDATE Practices SET Status=@Status,Result=@Result " +
                 " WHERE Id = @Id",
                 param: new
                 {
                     Id = id,
                     Status = status,
                     Result = result
                 },
                 transaction: Transaction
             );
        }
    }
}
