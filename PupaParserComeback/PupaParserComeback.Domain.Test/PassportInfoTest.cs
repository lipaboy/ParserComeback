using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Domain.DomainModels.Passport;
using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.Test
{
    [TestClass]
    public class PassportInfoTest
    {
        [TestMethod]
        public void PassportIssueInfoTest()
        {
            var issueBy = "ОУФМС РФ по НСО в Ленинском районе";
            var devisionCode = "540007";
            var issueDate = new DateTime(2016, 1, 1);

            var issueInfo = new PassportIssueInfo(issueBy, devisionCode, issueDate);

            Assert.AreEqual(issueBy, issueInfo.IssueBy);
            Assert.AreEqual(devisionCode, issueInfo.DevisionCode);
            Assert.AreEqual(issueDate, issueInfo.IssueDate);
        }

        [TestMethod]
        public void PassportLocationInfoTest()
        {
            var region = "НСО";
            var city = "г. Новосибирск";

            var street1 = "ул. Валдайская";
            var street2 = "ул. Балдуйс";
            var house1 = "д. 19/1";
            var house2 = "д. 1";
            
            var registerLocation = new Address($"{region} {city} {street1} {house1}");
            var actuallyLocation = new Address($"{region} {city} {street2} {house2}");
            var locality = city;

            var locationInfo = new PassportLocationInfo(registerLocation, actuallyLocation, city);

            Assert.AreEqual(registerLocation, locationInfo.RegisterLocation);
            Assert.AreEqual(actuallyLocation, locationInfo.ActuallyLocation);
            Assert.AreEqual(locality, locationInfo.Locality);
        }

        [TestMethod]
        public void PassportFamilyInfoTest()
        {
            var status = FamilyStatus.Single;
            var isHaveBaby = true;

            var familyInfo = new PassportFamilyInfo(status, isHaveBaby);

            Assert.AreEqual(status, familyInfo.FamilyStatus);
            Assert.AreEqual(isHaveBaby, familyInfo.IsHaveBaby);
        }

        [TestMethod]
        public void PassportInfoModelTest()
        {
            var photoName = "1.jpg";

            var code = new Code("5013", "455999");

            var issueInfo = new PassportIssueInfo("ОУФМС РФ по НСО в Ленинском районе", "540-007", 
                new DateTime(2016, 1, 1));

            var fullName = new FullName("Иванов", "Иван", "Иванович");
            var birthInfo = new BirthInfo(new DateTime(1995, 1, 1), "НСО, г. Новосибирск");
            var personInfo = new PersonInfo(fullName, birthInfo);

            var registerLocation = new Address("НСО, г. Новосибирск, ул. Валдайская, д. 19/1");
            var actuallyLocation = new Address("НСО, г. Новосибирск, ул. Балдуйс, д. 1");
            var locality = "г. Новосибирск";

            var locationInfo = new PassportLocationInfo(registerLocation, actuallyLocation, locality);

            var passportFamilyInfo = new PassportFamilyInfo(familyStatus: FamilyStatus.Single, isHaveBaby: true);

            var passportInfo = new PassportInfo(photoName, code, issueInfo, personInfo, locationInfo, passportFamilyInfo);

            Assert.AreEqual(photoName, passportInfo.PhotoName);
            Assert.AreEqual(code, passportInfo.Code);
            Assert.AreEqual(issueInfo, passportInfo.IssueInfo);
            Assert.AreEqual(personInfo, passportInfo.PersonInfo);
            Assert.AreEqual(locationInfo, passportInfo.LocationInfo);
            Assert.AreEqual(passportFamilyInfo, passportInfo.FamilyInfo);
        }
    }
}
