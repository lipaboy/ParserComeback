using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Data.SQLite.Abstract;
using PupaParserComeback.Data.SQLite.Concrete;
using PupaParserComeback.Data.SQLite.Dto;
using PupaParserComeback.Data.SQLite.ExtensionMethods;
using PupaParserComeback.Data.SQLite.Implementations;
using PupaParserComeback.Domain.Constants;
using PupaParserComeback.Domain.Interfaces;
using System;
using System.Data.Entity;
using System.Data.SQLite;

namespace PupaParserComeback.Data.SQLite.Test
{
    /// <summary>
    /// Summary description for LogRepositorySQLiteTest
    /// </summary>
    [TestClass]
    public class LogSQLiteTest
    {
        private IDbContextFactory _dbContextFactory;
        private IDbContextCache _dbContextCache;

        private IUnitOfWorkFactory _unitOfWorkFactory;

        private ILogQuery _logQuery;
        private ILogCommand _logCommand;

        [TestInitialize]
        public void Initialize()
        {
            _dbContextFactory = new DbContextTestFactory("PupaDbContextTest");
            _dbContextCache = new DbContextCache(_dbContextFactory);

            _unitOfWorkFactory = new UnitOfWorkFactory(_dbContextCache);

            _logQuery = new LogQuery(_dbContextFactory);
            _logCommand = new LogCommand(_dbContextCache);
        }

        [TestMethod]
        public void GetLogMessageSQLiteTest()
        {
            var dateTime = new DateTime(2017, 1, 1, hour: 1, minute: 5, second: 10);

            var logInMemory = new log()
            {
                id = 1,
                hostname = "my_computer",
                action = "create",
                date = dateTime.ToString(DateConstants.EventDateFormat),
                time = dateTime.ToString(DateConstants.EventTimeFormat)
            };

            AddLog(logInMemory);

            var first = _logQuery.Get(logInMemory.id);
            Assert.IsNotNull(first);
        }

        [TestMethod]
        public void CreateLogMessageSQLiteTest()
        {
            var dateTime = new DateTime(2017, 1, 1, hour: 1, minute: 5, second: 10);

            var logInMemory = new log()
            {
                id = 1,
                hostname = "my_computer",
                action = "create",
                date = dateTime.ToString(DateConstants.EventDateFormat),
                time = dateTime.ToString(DateConstants.EventTimeFormat)
            };

            AddLog(logInMemory);

            var logInDb = _logQuery.Get(logInMemory.id);

            Assert.AreEqual(logInMemory, logInDb);
        }

        private void AddLog(log logInMemory)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _logCommand.Insert(logInMemory);

                unitOfWork.Commit();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            var dbContext = _dbContextFactory.Create();
            
            dbContext.ClearTable(nameof(log));
        }
    }
}
