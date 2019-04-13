using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Data.SQLite.Abstract;
using PupaParserComeback.Data.SQLite.Concrete;
using PupaParserComeback.Data.SQLite.Dto;
using PupaParserComeback.Data.SQLite.Implementations;
using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.TestData;
using System;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;

namespace PupaParserComeback.Data.SQLite.Test
{
    [TestClass]
    public class RecruitInfoRepositoryTest
    {
        private IDbContextFactory _dbContextFactory;
        private IDbContextCache _dbContextCache;

        private IUnitOfWorkFactory _unitOfWorkFactory;

        private IPrizQuery _prizQuery;
        private IPrizCommand _prizCommand;

        private IRecruitInfoRepository _recruitInfoRepository;

        private string _photoExtension = ".jpg";

        [TestInitialize]
        public void Initialize()
        {
            _dbContextFactory = new DbContextTestFactory("PupaDbContextTest");
            _dbContextCache = new DbContextCache(_dbContextFactory);

            _unitOfWorkFactory = new UnitOfWorkFactory(_dbContextCache);

            _prizQuery = new PrizQuery(_dbContextFactory);
            _prizCommand = new PrizCommand(_dbContextCache);

            _recruitInfoRepository = new RecruitInfoRepository(_prizQuery, _prizCommand);

            var priz = SQLiteData.Build(1, 1, _photoExtension);
            var recruitInfo = MemoryData.Build(1, 1, _photoExtension);
        }

        [TestMethod]
        public void GetByRcpConscriptionDateRecruitInfoSQLiteTest()
        {
            var recruitInfo1 = MemoryData.Build(1, 1, _photoExtension);
            var recruitInfo2 = MemoryData.Build(2, 2, _photoExtension);

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _recruitInfoRepository.Add(recruitInfo1);
                _recruitInfoRepository.Add(recruitInfo2);

                unitOfWork.Commit();
            }

            var recruitInfos = _recruitInfoRepository.Get("Барабинский", new DateTime(2017, 1, 1), null);

            Assert.IsTrue(recruitInfos.Count() == 2);
        }

        [TestMethod]
        public void GetByIdRecruitInfoSQLiteTest()
        {
            var recruitInfo = MemoryData.Build(1, 1, _photoExtension);
            AddRecruitInfo(recruitInfo);

            var first = _recruitInfoRepository.Get(1);
            Assert.IsNotNull(first);
        }

        [TestMethod]
        public void AddRecruitInfoSQLiteTest()
        {
            var recruitInfo = MemoryData.Build(1, 1, _photoExtension);
            AddRecruitInfo(recruitInfo);
            
            var expected = recruitInfo;
            var actual = _recruitInfoRepository.Get(1);

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void UpdateRecruitInfoSQLiteTest()
        {
            var recruitInfo = MemoryData.Build(1, 1, _photoExtension);
            AddRecruitInfo(recruitInfo);
            
            var first = _recruitInfoRepository.Get(1);
            var homeNumber = "123";
            first.Envelope.Contacts.ChangeHomeNumber(new PhoneNumber(homeNumber));
            
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _recruitInfoRepository.Change(first);

                unitOfWork.Commit();
            }

            var updated = _recruitInfoRepository.Get(1);

            Assert.AreEqual(homeNumber, updated.Envelope.Contacts.HomeNumber.Value);
        }

        [TestMethod]
        public void RemoveRecruitInfoSQLiteTest()
        {
            var recruitInfo = MemoryData.Build(1, 1, _photoExtension);
            AddRecruitInfo(recruitInfo);

            var first = _recruitInfoRepository.Get(1);
            Assert.IsNotNull(first);

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _recruitInfoRepository.Remove(1);

                unitOfWork.Commit();
            }

            first = _recruitInfoRepository.Get(1);
            Assert.IsNull(first);
        }

        private void AddRecruitInfo(RecruitInfo recruitInfo)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _recruitInfoRepository.Add(recruitInfo);

                unitOfWork.Commit();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            var dbContext = _dbContextFactory.Create();

            ClearTable(dbContext.Database, nameof(priz));
            ClearTable(dbContext.Database, nameof(log));
        }

        private void ClearTable(Database db, string tableName)
        {
            db.ExecuteSqlCommand($"delete from {tableName};");
            db.ExecuteSqlCommand("delete from sqlite_sequence where name = @tableName;",
                new SQLiteParameter("tableName", tableName));
        }
    }
}
