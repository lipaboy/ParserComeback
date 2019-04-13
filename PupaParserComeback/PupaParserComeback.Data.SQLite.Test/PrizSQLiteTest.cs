using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Data.SQLite.Abstract;
using PupaParserComeback.Data.SQLite.Dto;
using System.Data.Entity;
using PupaParserComeback.Data.SQLite.Concrete;
using System.Linq;
using PupaParserComeback.TestData;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Data.SQLite.Implementations;
using System.Data.SQLite;
using PupaParserComeback.Data.SQLite.ExtensionMethods;

namespace PupaParserComeback.Data.SQLite.Test
{
    /// <summary>
    /// Summary description for PrizRepositoryTest
    /// </summary>
    [TestClass]
    public class PrizSQLiteTest
    {
        private IDbContextFactory _dbContextFactory;
        private IDbContextCache _dbContextCache;

        private IUnitOfWorkFactory _unitOfWorkFactory;

        private IPrizQuery _prizQuery;
        private IPrizCommand _prizCommand;

        private string _photoExtension = ".jpg";

        [TestInitialize]
        public void Initialize()
        {
            _dbContextFactory = new DbContextTestFactory("PupaDbContextTest");
            _dbContextCache = new DbContextCache(_dbContextFactory);

            _unitOfWorkFactory = new UnitOfWorkFactory(_dbContextCache);

            _prizQuery = new PrizQuery(_dbContextFactory);
            _prizCommand = new PrizCommand(_dbContextCache);
        }

        [TestMethod]
        public void GetAllPrizSQLiteTest()
        {
            var prizInMemory = SQLiteData.Build(1, 1, _photoExtension);
            AddPriz(prizInMemory);

            var all = _prizQuery.GetAll();
            Assert.AreNotEqual(0, all.Count());
        }

        [TestMethod]
        public void GetByIdPrizSQLiteTest()
        {
            var prizInMemory = SQLiteData.Build(1, 1, _photoExtension);
            AddPriz(prizInMemory);

            var first = _prizQuery.Get(prizInMemory.id);
            Assert.IsNotNull(first);
        }

        [TestMethod]
        public void CreatePrizSQLiteTest()
        {
            var prizInMemory = SQLiteData.Build(1, 1, _photoExtension);
            AddPriz(prizInMemory);

            var prizInDb = _prizQuery.Get(prizInMemory.id);

            Assert.AreEqual(prizInMemory, prizInDb);
        }

        [TestMethod]
        public void UpdatePrizSQLiteTest()
        {
            var prizInMemory = SQLiteData.Build(1, 1, _photoExtension);
            AddPriz(prizInMemory);

            var prizInDb = _prizQuery.Get(prizInMemory.id);

            prizInDb.surname = "Иванов 1";

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _prizCommand.Update(prizInDb);
                unitOfWork.Commit();
            }

            var updatedPrizInDb = _prizQuery.Get(prizInMemory.id);
            Assert.AreEqual(prizInDb.surname, updatedPrizInDb.surname);
        }

        [TestMethod]
        public void DeletePrizSQLiteTest()
        {
            var prizInMemory = SQLiteData.Build(1, 1, _photoExtension);
            AddPriz(prizInMemory);
            
            var prizInDb = _prizQuery.Get(prizInMemory.id);
            Assert.IsNotNull(prizInDb);

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _prizCommand.Delete(prizInMemory.id);
                unitOfWork.Commit();
            }

            prizInDb = _prizQuery.Get(prizInMemory.id);
            Assert.IsNull(prizInDb);
        }

        private void AddPriz(priz prizInMemory)
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

            dbContext.ClearTable(nameof(priz));
        }
    }
}
