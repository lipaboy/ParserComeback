using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Data.Firebird.Concrete;
using System.Data.Entity;
using PupaParserComeback.Data.Firebird.Abstract;
using PupaParserComeback.Data.Firebird.Dto;
using System.Linq;
using PupaParserComeback.TestData;
using PupaParserComeback.Data.Firebird.Implementations;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Data.Firebird.ExtensionMethods;

namespace PupaParserComeback.Data.Firebird.Test
{
    /// <summary>
    /// Summary description for PrizRepositoryTest
    /// </summary>
    [TestClass]
    public class PrizRepositoryFirebirdTest
    {
        private IDbContextFactory _dbContextFactory;
        private IDbContextCache _dbContextCache;

        private IUnitOfWorkFactory _unitOfWorkFactory;

        private IPrizQuery _prizQuery;
        private IPrizCommand _prizCommand;

        [TestInitialize]
        public void Initialize()
        {
            _dbContextFactory = new DbContextTestFactory("FormDbContextTest");
            _dbContextCache = new DbContextCache(_dbContextFactory);

            _unitOfWorkFactory = new UnitOfWorkFactory(_dbContextCache);

            _prizQuery = new PrizQuery(_dbContextFactory);
            _prizCommand = new PrizCommand(_dbContextCache);
        }

        [TestMethod]
        public void GetAllPrizFirebirdTest()
        {
            var prizInMemory = FirebirdData.Build(1);
            AddPriz(prizInMemory);

            var all = _prizQuery.GetAll();
            Assert.AreNotEqual(0, all.Count());
        }

        [TestMethod]
        public void GetByIdPrizFirebirdTest()
        {
            var prizInMemory = FirebirdData.Build(1);
            AddPriz(prizInMemory);

            var first = _prizQuery.Get(prizInMemory.ID);
            Assert.IsNotNull(first);
        }

        [TestMethod]
        public void CreatePrizFirebirdTest()
        {
            var prizInMemory = FirebirdData.Build(1);
            AddPriz(prizInMemory);
            
            var prizInDb = _prizQuery.Get(prizInMemory.ID);

            Assert.AreEqual(prizInMemory, prizInDb);
        }

        [TestMethod]
        public void CreatePrizeRangeFirebirdTest()
        {
            var priz1 = FirebirdData.Build(1);
            var priz2 = FirebirdData.Build(2);

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _prizCommand.Insert(new List<PRIZ>() { priz1, priz2 });
                unitOfWork.Commit();
            }

            var expectedCount = 2;
            var actualCount = _prizQuery.GetAll().Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void DeletePrizFirebirdTest()
        {
            var prizInMemory = FirebirdData.Build(1);
            AddPriz(prizInMemory);
            
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _prizCommand.Delete(1);
                unitOfWork.Commit();
            }

            var prizInDb = _prizQuery.Get(1);
            Assert.IsNull(prizInDb);
        }

        private void AddPriz(PRIZ prizInMemory)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _prizCommand.Insert(prizInMemory);
                unitOfWork.Commit();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            var dbContext = _dbContextFactory.Create();

            dbContext.ClearTable(nameof(PRIZ));
            dbContext.ClearGenerators("G_PRIZ");
        }
    }
}
