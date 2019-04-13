using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace PupaParserComeback.Import.Test
{
    [TestClass]
    public class ImportTest
    {
        private const string TestEnvironmentName = "TestEnvironment";

        private string _importDirectoryPath;

        [TestInitialize]
        public void Initialize()
        {
            var curDir = Directory.GetCurrentDirectory();
            var binPath = Directory.GetParent(curDir).FullName;
            var projPath = Directory.GetParent(binPath).FullName;

            _importDirectoryPath = Path.Combine(projPath, TestEnvironmentName);
        }

        [TestMethod]
        public void ImportRecruitTest()
        {
        }
    }
}
