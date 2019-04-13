using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Domain.DomainModels.Civil;
using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using System;
using System.Collections.Generic;

namespace PupaParserComeback.Domain.Test
{
    [TestClass]
    public class CivilTest
    {
        [TestMethod]
        public void CriminalInfoTest()
        {
            var registerStatus = RegisterStatus.Police;
            var criminalStatus = CriminalStatus.InProcess;

            var criminalInfo = new CriminalInfo(registerStatus, criminalStatus);

            Assert.AreEqual(registerStatus, criminalInfo.RegisterStatus);
            Assert.AreEqual(criminalStatus, criminalInfo.CriminalStatus);

            Assert.AreEqual("Отбывает наказание", criminalInfo.CriminalStatus.ToCriminalStatusString());
            Assert.AreEqual("В полиции", criminalInfo.RegisterStatus.ToRegisterStatusString());
        }

        [TestMethod]
        public void ContactsTest()
        {
            var mobile = "+7 954 122 11 22";
            var home = "545-789";

            var mobilePhoneNumber = new PhoneNumber(mobile);
            var homePhoneNumber = new PhoneNumber(home);

            var contacts = new Contacts(mobilePhoneNumber, homePhoneNumber);

            Assert.AreEqual(mobile, contacts.MobileNumber.Value);
            Assert.AreEqual(home, contacts.HomeNumber.Value);
        }

        [TestMethod]
        public void CivilInfoTest()
        {
            var education = EducationStatus.HigherVocational;
            var profession = "Инженер автоматики и вычислительной техники";
            var occupation = OccupationStatus.WorkOnCommercialEnterprise;

            var civilInfo = new CivilInfo(education, profession, occupation);

            Assert.AreEqual(education, civilInfo.Education);
            Assert.AreEqual(profession, civilInfo.Profession);
            Assert.AreEqual(occupation, civilInfo.Occupation);
        }

        [TestMethod]
        public void DriverInfoTest()
        {
            var code = new Code("2228", "625478");
            var issueDate = new DateTime(2016, 5, 5);

            var driverInfo = new DriverInfo(code, issueDate);

            Assert.AreEqual(code.Value, driverInfo.Code.Value);
            Assert.AreEqual(issueDate, driverInfo.IssueDate);
        }

        [TestMethod]
        public void FamilyInfoTest()
        {
            var parentFamilyStatus = ParentFamilyStatus.Full;

            var motherRelativeStatus = RelativeStatus.Mother;
            var motherFullName = new FullName("Евдокимова", "Евгения", "Вадимовна");
            var motherBirthInfo = new BirthInfo(new DateTime(1980, 1, 1), "г. Барнаул");
            var motherPersonInfo = new PersonInfo(motherFullName, motherBirthInfo);
            var motherWorkPlace = "Магазин игрушек";

            var motherInfo = new RelativeInfo(motherRelativeStatus, motherPersonInfo, motherWorkPlace);

            var fatherRelativeStatus = RelativeStatus.Father;
            var fatherFullName = new FullName("Евдокимов", "Евгений", "Сергеевич");
            var fatherBirthInfo = new BirthInfo(new DateTime(1981, 1, 1), "г. Барнаул");
            var fatherPersonInfo = new PersonInfo(fatherFullName, fatherBirthInfo);
            var fatherWorkPlace = "Завод";

            var fatherInfo = new RelativeInfo(fatherRelativeStatus, fatherPersonInfo, fatherWorkPlace);

            var relatives = new List<RelativeInfo>() { motherInfo, fatherInfo };
            var familyInfo = new FamilyInfo(parentFamilyStatus, relatives);

            Assert.AreEqual(parentFamilyStatus, familyInfo.ParentFamilyStatus);
            Assert.AreEqual(motherInfo, familyInfo.GetRelative(0));
            Assert.AreEqual(fatherInfo, familyInfo.GetRelative(1));
        }

        [TestMethod]
        public void FamilyInfoGettingExceptionTest()
        {
            var parentFamilyStatus = ParentFamilyStatus.Full;
            var motherRelativeStatus = RelativeStatus.Mother;
            var motherFullName = new FullName("Евдокимова", "Евгения", "Вадимовна");
            var motherBirthInfo = new BirthInfo(new DateTime(1980, 1, 1), "г. Барнаул");
            var motherPersonInfo = new PersonInfo(motherFullName, motherBirthInfo);
            var motherWorkPlace = "Магазин игрушек";

            var motherInfo = new RelativeInfo(motherRelativeStatus, motherPersonInfo, motherWorkPlace);

            var familyInfo = new FamilyInfo(parentFamilyStatus);
            familyInfo.AddRelative(motherInfo);

            Assert.IsNull(familyInfo.GetRelative(1));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FamilyInfoAddingExceptionTest()
        {
            var parentFamilyStatus = ParentFamilyStatus.Full;
            var motherRelativeStatus = RelativeStatus.Mother;
            var motherFullName = new FullName("Евдокимова", "Евгения", "Вадимовна");
            var motherBirthInfo = new BirthInfo(new DateTime(1980, 1, 1), "г. Барнаул");
            var motherPersonInfo = new PersonInfo(motherFullName, motherBirthInfo);
            var motherWorkPlace = "Магазин игрушек";

            var motherInfo1 = new RelativeInfo(motherRelativeStatus, motherPersonInfo, motherWorkPlace);
            var motherInfo2 = new RelativeInfo(motherRelativeStatus, motherPersonInfo, motherWorkPlace);
            var motherInfo3 = new RelativeInfo(motherRelativeStatus, motherPersonInfo, motherWorkPlace);
            var motherInfo4 = new RelativeInfo(motherRelativeStatus, motherPersonInfo, motherWorkPlace);

            var familyInfo = new FamilyInfo(parentFamilyStatus);

            familyInfo.AddRelative(motherInfo1);
            familyInfo.AddRelative(motherInfo2);
            familyInfo.AddRelative(motherInfo3);
            familyInfo.AddRelative(motherInfo4);
        }
    }
}
