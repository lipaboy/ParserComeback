using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Domain.DomainModels;

namespace PupaParserComeback.Domain.Test
{
    [TestClass]
    public class ServiceInfoTest
    {
        [TestMethod]
        public void ServiceInfoModelTest()
        {
            int sqliteId = 1;
            int formId = 1;
            var rcp = "Барабинский";
            var conscriptionDate = new DateTime(2017, 5, 19);

            var serviceInfo = new ServiceInfo(rcp, conscriptionDate, sqliteId, formId);

            Assert.AreEqual(sqliteId, serviceInfo.SqliteId);
            Assert.AreEqual(formId, serviceInfo.FirebirdId);
            Assert.AreEqual(rcp, serviceInfo.RegionalCollectionPoint);
            Assert.AreEqual(conscriptionDate, serviceInfo.ConscriptionDate);
        }
    }
}
