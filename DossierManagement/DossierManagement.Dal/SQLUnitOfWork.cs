using System.Data;
using System.Data.SqlClient;
using DossierManagement.Dal.Repositories;

namespace DossierManagement.Dal
{
    public class SQLUnitOfWork : IUnitOfWork
    {
        private IDossierRepository _DossierRepository;
        private IDossierChangeStatusRepository _DossierChangeStatusRepository;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public IDossierRepository DossierRepository => _DossierRepository ?? new DossierRepository(_transaction);
        public IDossierChangeStatusRepository DossierChangeStatusRepository => _DossierChangeStatusRepository ?? new DossierChangeStatusRepository(_transaction);

        public SQLUnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                _DossierRepository = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }

            _disposed = true;
        }

        ~SQLUnitOfWork() => Dispose(false);
    }
}

