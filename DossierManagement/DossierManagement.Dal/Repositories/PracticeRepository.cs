using Dapper;
using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;
using System.Collections.Specialized;
using System.Data;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace DossierManagement.Dal.Repositories
{
    public class DossierRepository : BaseRepository, IDossierRepository
    {
        public DossierRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<Dossier> Add(Dossier entity)
        {
            entity.Status = DossierStatus.Created;
            entity.Result = DossierResult.None;
            entity.Id = await Connection.ExecuteScalarAsync<int>(
               @"INSERT INTO Dossiers(FirstName,LastName,FiscalCode,BirthDate,Status,Result) 
                 VALUES(@FirstName, @LastName,@FiscalCode,@BirthDate,@Status,@Result); SELECT SCOPE_IDENTITY()",
               param: new
               {
                   FirstName = entity.FirstName,
                   LastName = entity.LastName,
                   FiscalCode = entity.FiscalCode,
                   BirthDate = entity.BirthDate,
                   Status = DossierStatus.Created,
                   Result = DossierResult.None,
               },
               transaction: Transaction
            );
            return entity;
        }

        public async Task<Dossier> Get(int id)
        {
            var Dossiers = await Connection.QueryAsync<Dossier>(
                @"SELECT * FROM Dossiers p WHERE p.Id=@Id",
                transaction: Transaction,
                param: new
                {
                    Id = id,
                });
            return Dossiers.FirstOrDefault();
        }

        public async Task<DossierStatus> GetStatus(int id)
        {
            try
            {
                var status = await Connection.QueryFirstOrDefaultAsync<DossierStatus>("Select Status From Dossiers WHERE Id = @Id",
                param: new { Id = id },
                transaction: Transaction
                );
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> Update(Dossier entity)
        {
            return await Connection.ExecuteAsync(
                 "UPDATE Dossiers SET FirstName = @FirstName, LastName = @LastName, FiscalCode = @FiscalCode, BirthDate = @BirthDate " +
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

        public async Task UpdateStatus(int id, DossierStatus status, DossierResult result)
        {
            await Connection.ExecuteAsync(
                 "UPDATE Dossiers SET Status=@Status,Result=@Result " +
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
