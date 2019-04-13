using PupaParserComeback.Data.SQLite.Abstract;
using PupaParserComeback.Data.SQLite.Exceptions;
using PupaParserComeback.Domain.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.SQLite.Implementations
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
                    var sqliteException = updateException.InnerException as SQLiteException;
                    if (sqliteException != null)
                    {
                        var resultCode = sqliteException.ResultCode;

                        if (resultCode == SQLiteErrorCode.Constraint)
                        {
                            throw new AlreadyExistsException(sqliteException);
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
