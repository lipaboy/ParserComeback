using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.DomainModels.Civil;
using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.DomainModels.Medicine;
using PupaParserComeback.Domain.DomainModels.Military;
using PupaParserComeback.Domain.DomainModels.Passport;
using PupaParserComeback.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PupaParserComeback.TestData
{
    public static class MemoryData
    {
        public static RecruitInfo Build(int sqliteId, int formId, string photoExtension)
        {
            var serviceInfo = BuildServiceInfo(sqliteId, formId);
            var criminalInfo = BuildCriminalInfo();
            var medicineInfo = BuildMedicineInfo();

            var passportInfo = BuildPassportInfo(sqliteId, photoExtension);
            var militaryInfo = BuildMilitaryInfo();
            var civilInfo = BuildCivilInfo();
            var contacts = BuildContacts();
            var familyInfo = BuildFamilyInfo();
            var driverInfo = BuildDriverInfo();
            var envelope = new Envelope(passportInfo, militaryInfo, civilInfo, contacts, familyInfo, driverInfo);

            return new RecruitInfo(serviceInfo, criminalInfo, medicineInfo, envelope);
        }

        private static ServiceInfo BuildServiceInfo(int sqliteId , int formId)
        {
            var rcp = "Барабинский";
            var conscriptionDate = new DateTime(2017, 1, 1);

            return new ServiceInfo(rcp, conscriptionDate, sqliteId, formId);
        }

        private static CriminalInfo BuildCriminalInfo()
        {
            var registerStatus = RegisterStatus.WasNot;
            var criminalStatus = CriminalStatus.HaveNot;

            return new CriminalInfo(registerStatus, criminalStatus);
        }

        private static MedicineInfo BuildMedicineInfo()
        {
            var medicineRank = MedicineRank.A2;
            var additionalRequirementsTableGraphs = "2,4-10";
            var diseaseArticles = "13д";
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

            var sportRank = SportRank.HaveNot;
            var kind = "Футбол";
            var sportInfo = new SportInfo(sportRank, kind);

            return new MedicineInfo(health, physiologicalCharacteristics, sportInfo);
        }

        private static PassportInfo BuildPassportInfo(int sqliteId, string photoExtension)
        {
            var photoName = $"{sqliteId}{photoExtension}";

            var code = new Code("5013", "055467");

            var issueInfo = new PassportIssueInfo("ОУФМС РФ по НСО в Ленинском районе", "540007",
                new DateTime(2016, 1, 1));

            var fullName = new FullName("Иванов" + sqliteId, "Иван" + sqliteId, "Иванович" + sqliteId);
            var birthInfo = new BirthInfo(new DateTime(1999, 1, 1), "НСО, г. Новосибирск");
            var personInfo = new PersonInfo(fullName, birthInfo);

            var registerLocation = new Address("Новосибирская обл., г. Новосибирск, ул. Валдайская, д. 19/1");
            var actuallyLocation = new Address("Новосибирская обл., г. Новосибирск, ул. Валдайская, д. 19/1");
            var locality = "г. Новосибирск";

            var locationInfo = new PassportLocationInfo(registerLocation, actuallyLocation, locality);

            var passportFamilyInfo = new PassportFamilyInfo(familyStatus: FamilyStatus.Single, isHaveBaby: false);

            return new PassportInfo(photoName, code, issueInfo, personInfo, locationInfo, passportFamilyInfo);
        }

        private static MilitaryInfo BuildMilitaryInfo()
        {
            var personalNumber = new Code("ВС 052572");
            var billetNumber = new Code("АН 2281488");
            var accessForm = AccessForm.First;
            var number = "1488";
            var issueDate = new DateTime(2017, 1, 1);
            var secretAccess = new SecretAccess(accessForm, number, issueDate);
            var militaryBillet = new MilitaryBillet(billetNumber, secretAccess);
            var proficiencyCategory = ProficiencyCategory.First;
            var officialStatus = OfficialStatus.Team;
            var nervously = NervouslyPsychologicalStatus.High;
            var general = GeneralPsychologicalStatus.High;
            var proficiencyCard = new ProficiencyCardInfo(proficiencyCategory, officialStatus, nervously, general);

            var speciality = MilitaryInfo.NoSpeciality;
            var teamMode = "НЕРЕЖ.";

            return new MilitaryInfo(personalNumber, militaryBillet, proficiencyCard, speciality, teamMode);
        }

        private static CivilInfo BuildCivilInfo()
        {
            var education = EducationStatus.Basic;
            var profession = "Повар";
            var occupation = OccupationStatus.NotWorkNotStudy;

            return new CivilInfo(education, profession, occupation);
        }

        private static Contacts BuildContacts()
        {
            var mobile = "9517925897";
            var home = "9517925897";
            var mobilePhoneNumber = new PhoneNumber(mobile);
            var homePhoneNumber = new PhoneNumber(home);

            return new Contacts(mobilePhoneNumber, homePhoneNumber);
        }

        private static FamilyInfo BuildFamilyInfo()
        {
            var parentFamilyStatus = ParentFamilyStatus.Full;

            var fatherRelativeStatus = RelativeStatus.Father;
            var fatherFullName = new FullName("Грибко", "Иван", "Викторович");
            var fatherBirthInfo = new BirthInfo(new DateTime(1960, 1, 1), "Новосибирская обл., г. Новосибирск");
            var fatherPersonInfo = new PersonInfo(fatherFullName, fatherBirthInfo);
            var fatherWorkPlace = "Пекарь";

            var fatherInfo = new RelativeInfo(fatherRelativeStatus, fatherPersonInfo, fatherWorkPlace);

            var motherRelativeStatus = RelativeStatus.Mother;
            var motherFullName = new FullName("Грибко", "Юлия", "Вадимовна");
            var motherBirthInfo = new BirthInfo(new DateTime(1965, 1, 1), "Новосибирская обл., г. Новосибирск");
            var motherPersonInfo = new PersonInfo(motherFullName, motherBirthInfo);
            var motherWorkPlace = "Повар";

            var motherInfo = new RelativeInfo(motherRelativeStatus, motherPersonInfo, motherWorkPlace);

            var sisterRelativeStatus = RelativeStatus.Sister;
            var sisterFullName = new FullName("Грибко", "Валерия", "Ивановна");
            var sisterBirthInfo = new BirthInfo(new DateTime(1990, 1, 1), "Новосибирская обл., г. Новосибирск");
            var sisterPersonInfo = new PersonInfo(sisterFullName, sisterBirthInfo);
            var sisterWorkPlace = "Художник";

            var sisterInfo = new RelativeInfo(sisterRelativeStatus, sisterPersonInfo, sisterWorkPlace);

            var relatives = new List<RelativeInfo>() { fatherInfo, motherInfo, sisterInfo };
            return new FamilyInfo(parentFamilyStatus, relatives);
        }

        private static DriverInfo BuildDriverInfo()
        {
            var code = new Code("54CE", "625478");
            var issueDate = new DateTime(2016, 1, 1);

            return new DriverInfo(code, issueDate);
        }
    }
}
