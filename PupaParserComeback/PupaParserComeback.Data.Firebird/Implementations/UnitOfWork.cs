using FirebirdSql.Data.FirebirdClient;
using PupaParserComeback.Data.Firebird.Abstract;
using PupaParserComeback.Data.Firebird.Constants;
using PupaParserComeback.Data.Firebird.Exceptions;
using PupaParserComeback.Domain.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.Firebird.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbContextCache _dbContextCache;

        private readonly DbContext _dbContext;
        private readonly DbContextTransaction _transaction;

        public UnitOfWork(IDbContextCache dbContextCache)
        {
            if (dbContextCache == null)
            {
                throw new ArgumentNullException(nameof(dbContextCache));
            }

            _dbContextCache = dbContextCache;

            _dbContext = dbContextCache.Create();
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();
                _transaction.Commit();
            }
            catch (DbUpdateException dbUpdateEx)
            {
                ThrowAlreadyExistsException(dbUpdateEx);
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                _transaction.Commit();
            }
            catch (DbUpdateException dbUpdateEx)
            {
                ThrowAlreadyExistsException(dbUpdateEx);
            }
        }

        private void ThrowAlreadyExistsException(DbUpdateException dbUpdateEx)
        {
            var dbUpdateException = dbUpdateEx.InnerException;
            if (dbUpdateException != null)
            {
                var updateException = dbUpdateEx.InnerException;
                if (updateException != null)
                {
                    var firebirdException = updateException.InnerException as FbException;
                    if (firebirdException != null)
                    {
                        var errorCode = firebirdException.ErrorCode;

                        if (errorCode == FbErrorCode.UniqueKeyViolation)
                        {
                            throw new AlreadyExistsException(firebirdException);
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            _transaction.Dispose();

            _dbContextCache.Dispose();
        }
    }
}
