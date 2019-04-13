using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Data.SQLite.Concrete;
using System.Data.Entity;
using PupaParserComeback.Data.SQLite.Abstract;
using PupaParserComeback.TestData;

namespace PupaParserComeback.Data.SQLite.Test
{
    [TestClass]
    public class MapperSQLiteTest
    {
        private string _photoExtension = ".jpg";
        
        [TestMethod]
        public void MapToPrizTest()
        {
            var prizExpected = SQLiteData.Build(1, 1, _photoExtension);
            var recruitInfo = MemoryData.Build(1, 1, _photoExtension);
            var prizActual = RecruitInfoMapper.Map(recruitInfo);

            Assert.AreEqual(prizExpected, prizActual);
        }
        
        [TestMethod]
        public void MapToRecruitInfoTest()
        {
            var recruitInfoExpected = MemoryData.Build(1, 1, _photoExtension);
            var priz = SQLiteData.Build(1, 1, _photoExtension);
            var recruitInfoActual = RecruitInfoMapper.Map(priz);

            Assert.AreEqual(recruitInfoExpected, recruitInfoActual);
        }
    }
}
