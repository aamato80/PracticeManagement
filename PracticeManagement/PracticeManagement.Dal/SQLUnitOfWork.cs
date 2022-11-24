using System.Data;
using System.Data.SqlClient;
using PracticeManagement.Dal.Repositories;

namespace PracticeManagement.Dal
{
    public class SQLUnitOfWork : IUnitOfWork
    {
        private IPracticeRepository _practiceRepository;
        private IPracticeChangeStatusRepository _practiceChangeStatusRepository;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public IPracticeRepository PracticeRepository => _practiceRepository ?? new PracticeRepository(_transaction);
        public IPracticeChangeStatusRepository PracticeChangeStatusRepository => _practiceChangeStatusRepository ?? new PracticeChangeStatusRepository(_transaction);

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
                _practiceRepository = null;
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

