using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.Test
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void DocumentCodeTest()
        {
            var serie = "5013";
            var number = "455999";
            var codeValue = $"{serie} {number}";

            var passportCode = new Code(serie, number);

            Assert.AreEqual(serie, passportCode.Serie);
            Assert.AreEqual(number, passportCode.Number);
            Assert.AreEqual(codeValue, passportCode.Value);
        }

        [TestMethod]
        public void FullNameTest()
        {
            var surname = "Иванов";
            var name = "Иван";
            var patronymic = "Иванович";
            var fullNameValue = $"{surname} {name} {patronymic}";

            var fullName = new FullName(surname, name, patronymic);

            Assert.AreEqual(surname, fullName.Surname);
            Assert.AreEqual(name, fullName.Name);
            Assert.AreEqual(patronymic, fullName.Patronymic);
            Assert.AreEqual(fullNameValue, fullName.Value);
        }

        [TestMethod]
        public void BirthInfoTest()
        {
            var date = new DateTime(1995, 1, 1);
            var place = "НСО, г. Новосибирск";

            var birthInfo = new BirthInfo(date, place);

            Assert.AreEqual(date, birthInfo.Date);
            Assert.AreEqual(place, birthInfo.Place);
        }

        [TestMethod]
        public void PersonInfoTest()
        {
            var surname = "Иванов";
            var name = "Иван";
            var patronymic = "Иванович";
            var fullNameValue = $"{surname} {name} {patronymic}";
            var fullName = new FullName(surname, name, patronymic);

            var date = new DateTime(1995, 1, 1);
            var place = "НСО, г. Новосибирск";
            var birthInfo = new BirthInfo(date, place);

            var personInfo = new PersonInfo(fullName, birthInfo);

            Assert.AreEqual(surname, personInfo.FullName.Surname);
            Assert.AreEqual(name, personInfo.FullName.Name);
            Assert.AreEqual(patronymic, personInfo.FullName.Patronymic);
            Assert.AreEqual(fullNameValue, personInfo.FullName.Value);

            Assert.AreEqual(date, personInfo.BirthInfo.Date);
            Assert.AreEqual(place, personInfo.BirthInfo.Place);
        }

        [TestMethod]
        public void AddressTest()
        {
            var region = "НСО"; 
            var city = "г. Новосибирск";
            var street = "ул. Валдайская";
            var house = "д. 20/1";
            var addressValue = $"{region}, {city}, {street}, {house}";

            var address = new Address(addressValue);

            Assert.AreEqual(addressValue, address.Value);
        }

        [TestMethod]
        public void PhoneNumberTest()
        {
            var mobile = "+7 954 122 11 22";
            var home = "545-789";

            var mobilePhoneNumber = new PhoneNumber(mobile);
            var homePhoneNumber = new PhoneNumber(home);

            Assert.AreEqual(mobile, mobilePhoneNumber.Value);
            Assert.AreEqual(home, homePhoneNumber.Value); 
        }

        [TestMethod]
        public void RelativeInfoTest()
        {
            var motherRelativeStatus = RelativeStatus.Mother;

            var motherSurname = "Евдокимова";
            var motherName = "Евгения";
            var motherPatronymic = "Вадимовна";
            var motherFullName = new FullName(motherSurname, motherName, motherPatronymic);

            var motherBirthDate = new DateTime(1980, 1, 1);
            var motherBirthPlace = "г. Барнаул";
            var motherBirthInfo = new BirthInfo(motherBirthDate, motherBirthPlace);

            var motherPersonInfo = new PersonInfo(motherFullName, motherBirthInfo);

            var motherWorkPlace = "Магазин игрушек";

            var motherInfo = new RelativeInfo(motherRelativeStatus, motherPersonInfo, motherWorkPlace);

            Assert.AreEqual(motherRelativeStatus, motherInfo.RelativeStatus);

            Assert.AreEqual(motherSurname, motherInfo.PersonInfo.FullName.Surname);
            Assert.AreEqual(motherName, motherInfo.PersonInfo.FullName.Name);
            Assert.AreEqual(motherPatronymic, motherInfo.PersonInfo.FullName.Patronymic);

            Assert.AreEqual(motherBirthDate, motherInfo.PersonInfo.BirthInfo.Date);
            Assert.AreEqual(motherBirthPlace, motherInfo.PersonInfo.BirthInfo.Place);

            Assert.AreEqual(motherWorkPlace, motherInfo.WorkPlace);
        }
    }
}
