using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.DomainModels.Civil;
using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.DomainModels.Military;
using PupaParserComeback.Domain.DomainModels.Medicine;
using System.Collections.Generic;
using PupaParserComeback.Domain.DomainModels.Passport;

namespace PupaParserComeback.Domain.Test
{
    [TestClass]
    public class RecruitInfoTest
    {
        [TestMethod]
        public void EnvelopeNotDriverTest()
        {
            var passportInfo = BuildPassportInfo();
            var militaryInfo = BuildMilitaryInfo();
            var civilInfo = BuildCivilInfo();
            var contacts = BuildContacts();
            var familyInfo = BuildFamilyInfo();

            var envelope = new Envelope(passportInfo, militaryInfo, civilInfo, contacts, familyInfo);

            Assert.AreEqual(passportInfo, envelope.PassportInfo);
            Assert.AreEqual(militaryInfo, envelope.MilitaryInfo);
            Assert.AreEqual(civilInfo, envelope.CivilInfo);
            Assert.AreEqual(contacts, envelope.Contacts);
            Assert.AreEqual(familyInfo, envelope.FamilyInfo);
            Assert.IsNull(envelope.DriverInfo);
            Assert.IsFalse(envelope.IsDriver);
        }

        [TestMethod]
        public void EnvelopeDriverTest()
        {
            var passportInfo = BuildPassportInfo();
            var militaryInfo = BuildMilitaryInfo();
            var civilInfo = BuildCivilInfo();
            var contacts = BuildContacts();
            var familyInfo = BuildFamilyInfo();
            var driverInfo = BuildDriverInfo();

            var envelope = new Envelope(passportInfo, militaryInfo, civilInfo, contacts, familyInfo, driverInfo);

            Assert.AreEqual(passportInfo, envelope.PassportInfo);
            Assert.AreEqual(militaryInfo, envelope.MilitaryInfo);
            Assert.AreEqual(civilInfo, envelope.CivilInfo);
            Assert.AreEqual(contacts, envelope.Contacts);
            Assert.AreEqual(familyInfo, envelope.FamilyInfo);
            Assert.AreEqual(driverInfo, envelope.DriverInfo);
            Assert.IsTrue(envelope.IsDriver);
        }

        [TestMethod]
        public void RecruitInfoModelTest()
        {
            var serviceInfo = BuildServiceInfo();
            var criminalInfo = BuildCriminalInfo();
            var medicineInfo = BuildMedicineInfo();

            var passportInfo = BuildPassportInfo();
            var militaryInfo = BuildMilitaryInfo();
            var civilInfo = BuildCivilInfo();
            var contacts = BuildContacts();
            var familyInfo = BuildFamilyInfo();
            var driverInfo = BuildDriverInfo();
            var envelope = new Envelope(passportInfo, militaryInfo, civilInfo, contacts, familyInfo, driverInfo);
            
            var recruitInfo = new RecruitInfo(serviceInfo, criminalInfo, medicineInfo, envelope);

            Assert.AreEqual(serviceInfo, recruitInfo.ServiceInfo);
            Assert.AreEqual(criminalInfo, recruitInfo.CriminalInfo);
            Assert.AreEqual(medicineInfo, recruitInfo.MedicineInfo);

            Assert.AreEqual(passportInfo, recruitInfo.Envelope.PassportInfo);
            Assert.AreEqual(militaryInfo, recruitInfo.Envelope.MilitaryInfo);
            Assert.AreEqual(civilInfo, recruitInfo.Envelope.CivilInfo);
            Assert.AreEqual(contacts, recruitInfo.Envelope.Contacts);
            Assert.AreEqual(familyInfo, recruitInfo.Envelope.FamilyInfo);
            Assert.AreEqual(driverInfo, recruitInfo.Envelope.DriverInfo);
            Assert.IsTrue(recruitInfo.Envelope.IsDriver);
        }

        private ServiceInfo BuildServiceInfo()
        {
            int sqliteId = 1;
            int formId = 1;
            var rcp = "Барабинский";
            var conscriptionDate = new DateTime(2017, 5, 19);

            return new ServiceInfo(rcp, conscriptionDate, sqliteId, formId);
        }

        private CriminalInfo BuildCriminalInfo()
        {
            var registerStatus = RegisterStatus.Police;
            var criminalStatus = CriminalStatus.InProcess;

            return new CriminalInfo(registerStatus, criminalStatus);
        }

        private MedicineInfo BuildMedicineInfo()
        {
            var medicineRank = MedicineRank.A4;
            var additionalRequirementsTableGraphs = "2,4,7-10";
            var diseaseArticles = "13д,64г";
            var vision = "1,0/1,0";
            var bloodType = BloodType.OnePlus;
            var health = new Health(medicineRank, additionalRequirementsTableGraphs,
                                    diseaseArticles, vision, bloodType);

            var height = 180;
            var weight = 80;
            var headSize = 59;
            var clothingSize = "48/3";
            var shoesSize = 43;
            var physiologicalCharacteristics = new PhysiologicalCharacteristics(height, weight,
                                                   headSize, clothingSize, shoesSize);

            var sportRank = SportRank.First;
            var kind = "Футбол";
            var sportInfo = new SportInfo(sportRank, kind);

            return new MedicineInfo(health, physiologicalCharacteristics, sportInfo);
        }

        private PassportInfo BuildPassportInfo()
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

            return new PassportInfo(photoName, code, issueInfo, personInfo, locationInfo, passportFamilyInfo);
        }

        private MilitaryInfo BuildMilitaryInfo()
        {
            var personalNumber = new Code("ВС 052572");
            var billetNumber = new Code("АН 2281488");
            var accessForm = AccessForm.First;
            var number = "1488";
            var issueDate = new DateTime(2017, 1, 1);
            var secretAccess = new SecretAccess(accessForm, number, issueDate);
            var militaryBillet = new MilitaryBillet(billetNumber, secretAccess);
            var proficiencyCategory = ProficiencyCategory.Second;
            var officialStatus = OfficialStatus.Driver;
            var nervously = NervouslyPsychologicalStatus.Satisfactory;
            var general = GeneralPsychologicalStatus.High;
            var proficiencyCard = new ProficiencyCardInfo(proficiencyCategory, officialStatus, nervously, general);
            var speciality = MilitaryInfo.NoSpeciality;
            var teamMode = "К-220А";

            return new MilitaryInfo(personalNumber, militaryBillet, proficiencyCard, speciality, teamMode);
        }

        private CivilInfo BuildCivilInfo()
        {
            var education = EducationStatus.HigherVocational;
            var profession = "Инженер автоматики и вычислительной техники";
            var occupation = OccupationStatus.WorkOnCommercialEnterprise;

            return new CivilInfo(education, profession, occupation);
        }

        private Contacts BuildContacts()
        {
            var mobile = "+7 954 122 11 22";
            var home = "545-789";
            var mobilePhoneNumber = new PhoneNumber(mobile);
            var homePhoneNumber = new PhoneNumber(home);

            return new Contacts(mobilePhoneNumber, homePhoneNumber);
        }

        private FamilyInfo BuildFamilyInfo()
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
            return new FamilyInfo(parentFamilyStatus, relatives);
        }

        private DriverInfo BuildDriverInfo()
        {
            var code = new Code("2228", "625478");
            var issueDate = new DateTime(2016, 5, 5);

            return new DriverInfo(code, issueDate);
        }
    }
}
