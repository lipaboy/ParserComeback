using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Data.Firebird.Concrete;
using PupaParserComeback.TestData;

namespace PupaParserComeback.Data.Firebird.Test
{
    [TestClass]
    public class MapperFirebirdTest
    {
        private const string _photoExtension = ".jpg";

        [TestMethod]
        public void MapToPrizTest()
        {
            var prizExpected = FirebirdData.Build(1);
            var recruitInfoInMemory = MemoryData.Build(1, 1, _photoExtension);
            var prizActual = RecruitInfoMapper.Map(recruitInfoInMemory);

            Assert.AreEqual(prizExpected, prizActual);
        }
    }
}
