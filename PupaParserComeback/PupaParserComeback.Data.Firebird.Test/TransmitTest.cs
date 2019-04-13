using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using PupaParserComeback.Data.Firebird.Abstract;
using PupaParserComeback.Data.Firebird.Concrete;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Data.Firebird.Implementations;
using PupaParserComeback.TestData;
using System.Collections.Generic;
using PupaParserComeback.Domain.DomainModels;
using System.Linq;
using PupaParserComeback.Data.Firebird.ExtensionMethods;
using PupaParserComeback.Data.Firebird.Dto;

namespace PupaParserComeback.Data.Firebird.Test
{
    [TestClass]
    public class TransmitTest
    {
        private IDbContextFactory _dbContextFactory;
        private IDbContextCache _dbContextCache;

        private IUnitOfWorkFactory _unitOfWorkFactory;

        private IPrizQuery _prizQuery;
        private IPrizCommand _prizCommand;

        private ITransmitService _transmitService;

        private string _photoExtension = ".jpg";

        [TestInitialize]
        public void Initialize()
        {
            _dbContextFactory = new DbContextTestFactory("FormDbContextTest");
            _dbContextCache = new DbContextCache(_dbContextFactory);

            _unitOfWorkFactory = new UnitOfWorkFactory(_dbContextCache);

            _prizQuery = new PrizQuery(_dbContextFactory);
            _prizCommand = new PrizCommand(_dbContextCache);
            
            _transmitService = new TransmitService(_prizCommand);
        }

        [TestMethod]
        public void MoveTest()
        {
            var recruitInfo1 = MemoryData.Build(1, 1, _photoExtension);
            var recruitInfo2 = MemoryData.Build(2, 2, _photoExtension);
            var recruitInfo3 = MemoryData.Build(3, 3, _photoExtension);

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _transmitService.Move(new List<RecruitInfo>()
                {
                    recruitInfo1,
                    recruitInfo2,
                    recruitInfo3
                });

                unitOfWork.Commit();
            }

            var expectedCount = 3;
            var actualCount = _prizQuery.GetAll().Count();

            Assert.AreEqual(expectedCount, actualCount);
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
