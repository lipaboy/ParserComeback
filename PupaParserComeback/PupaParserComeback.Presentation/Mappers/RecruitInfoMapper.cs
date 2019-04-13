using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.DomainModels.Civil;
using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.DomainModels.Medicine;
using PupaParserComeback.Domain.DomainModels.Military;
using PupaParserComeback.Domain.DomainModels.Passport;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using PupaParserComeback.Presentation.Models.CardGroups;
using PupaParserComeback.Presentation.Models.Cards;
using System;
using System.Collections.Generic;

namespace PupaParserComeback.Presentation.Mappers
{
    public class RecruitInfoMapper
    {
        private readonly ServiceCard _serviceCard;
        private readonly FirstCardGroup _firstCardGroup;
        private readonly SecondCardGroup _secondCardGroup;
        private readonly ThirdCardGroup _thirdCardGroup;

        public RecruitInfoMapper(RecruitCardGroup recruitCardGroup)
        {
            if (recruitCardGroup == null)
            {
                throw new ArgumentNullException(nameof(recruitCardGroup));
            }

            _serviceCard = recruitCardGroup.ServiceCard;
            _firstCardGroup = recruitCardGroup.FirstCardGroup;
            _secondCardGroup = recruitCardGroup.SecondCardGroup;
            _thirdCardGroup = recruitCardGroup.ThirdCardGroup;
        }

        public RecruitInfo Map()
        {
            var serviceInfo = BuildServiceInfo();
            var criminalInfo = BuildCriminalInfo();
            var medicineInfo = BuildMedicineInfo();

            var passportInfo = BuildPassportInfo();
            var militaryInfo = BuildMilitaryInfo();
            var civilInfo = BuildCivilInfo();
            var contacts = BuildContacts();
            var familyInfo = BuildFamilyInfo();

            DriverInfo driverInfo = null;
            if (_secondCardGroup.DriverCard.IsDriver)
            {
                driverInfo = BuildDriverInfo();
            }

            var envelope = new Envelope(passportInfo, militaryInfo, civilInfo, contacts, familyInfo, driverInfo);

            return new RecruitInfo(serviceInfo, criminalInfo, medicineInfo, envelope);
        }

        private ServiceInfo BuildServiceInfo()
        {
            int id;
            long? sqliteId = int.TryParse(_serviceCard.SqliteId, out id) ? (int?)id : null;
            int? formId = int.TryParse(_serviceCard.FirebirdId, out id) ? (int?)id : null;
            
            if (!_serviceCard.ConscriptionDate.HasValue)
            {
                throw new NullReferenceException(nameof(_serviceCard.ConscriptionDate));
            }

            return new ServiceInfo(_serviceCard.RegionalCollectionPoint, _serviceCard.ConscriptionDate.Value,
                sqliteId, formId);
        }

        private CriminalInfo BuildCriminalInfo()
        {
            var registerStatus = _firstCardGroup.CriminalCard.RegisterStatus.ToRegisterStatusEnum();
            var criminalStatus = _firstCardGroup.CriminalCard.CriminalStatus.ToCriminalStatusEnum();

            return new CriminalInfo(registerStatus, criminalStatus);
        }

        private MedicineInfo BuildMedicineInfo()
        {
            var medicineRank = _thirdCardGroup.MedicineCard.Rank.ToMedicineRankEnum();
            var additionalRequirementsTableGraphs = _thirdCardGroup.MedicineCard.AdditionalRequirementsTable;
            var diseaseArticles = _thirdCardGroup.MedicineCard.DiseaseArticles;
            var vision = _thirdCardGroup.MedicineCard.Vision;
            
            var bloodType = _thirdCardGroup.MedicineCard.BloodType.ToBloodTypeEnum();

            var health = new Health(medicineRank, additionalRequirementsTableGraphs,
                                    diseaseArticles, vision, bloodType);

            int h;
            var height = int.TryParse(_thirdCardGroup.PhysiologicalCharacteristicsCard.Height, out h) 
                ? h : (int?)null;

            int w;
            var weight = int.TryParse(_thirdCardGroup.PhysiologicalCharacteristicsCard.Weight, out w)
                ? w : (int?)null;

            int hs;
            var headSize = int.TryParse(_thirdCardGroup.PhysiologicalCharacteristicsCard.HeadSize, out hs)
                ? hs : (int?)null;

            var clothingSize = _thirdCardGroup.PhysiologicalCharacteristicsCard.ClothingSize;

            int ss;
            var shoesSize = int.TryParse(_thirdCardGroup.PhysiologicalCharacteristicsCard.ShoesSize, out ss)
                ? ss : (int?)null;

            var physiologicalCharacteristics = new PhysiologicalCharacteristics(height, weight,
                                                   headSize, clothingSize, shoesSize);

            var sportRank = _thirdCardGroup.SportCard.Rank.ToSportRankEnum();
            var kind = _thirdCardGroup.SportCard.Kind;
            var sportInfo = new SportInfo(sportRank, kind);

            return new MedicineInfo(health, physiologicalCharacteristics, sportInfo);
        }

        private PassportInfo BuildPassportInfo()
        {
            var photoName = _firstCardGroup.PassportPersonInfoCard.PhotoName;

            var code = new Code(_firstCardGroup.PassportInfoCard.Code);

            if (!_firstCardGroup.PassportInfoCard.IssueDate.HasValue)
            {
                throw new NullReferenceException(nameof(_firstCardGroup.PassportInfoCard.IssueDate));
            }

            var issueInfo = new PassportIssueInfo(_firstCardGroup.PassportInfoCard.IssueBy, 
                _firstCardGroup.PassportInfoCard.DevisionCode,
                _firstCardGroup.PassportInfoCard.IssueDate.Value);

            var fullName = new FullName(_firstCardGroup.PassportPersonInfoCard.Surname,
                _firstCardGroup.PassportPersonInfoCard.Name,
                _firstCardGroup.PassportPersonInfoCard.Patronymic);

            if (!_firstCardGroup.PassportPersonInfoCard.BirthDate.HasValue)
            {
                throw new NullReferenceException(nameof(_firstCardGroup.PassportPersonInfoCard.BirthDate));
            }

            var birthInfo = new BirthInfo(_firstCardGroup.PassportPersonInfoCard.BirthDate.Value,
                _firstCardGroup.PassportPersonInfoCard.BirthPlace);
            
            var personInfo = new PersonInfo(fullName, birthInfo);

            var registerLocation = new Address(_firstCardGroup.PassportAccommodationCard.RegisterLocation);
            var actuallyLocation = new Address(_firstCardGroup.PassportAccommodationCard.ActuallyLocation);
            var locality = _firstCardGroup.PassportAccommodationCard.Locality;

            var locationInfo = new PassportLocationInfo(registerLocation, actuallyLocation, locality);

            var passportFamilyInfo = new PassportFamilyInfo(_firstCardGroup.PassportFamilyInfoCard.FamilyStatus.ToFamilyStatusEnum(),
                _firstCardGroup.PassportFamilyInfoCard.IsHaveBaby);

            return new PassportInfo(photoName, code, issueInfo, personInfo, locationInfo, passportFamilyInfo);
        }

        private MilitaryInfo BuildMilitaryInfo()
        {
            var personalNumber = new Code(_secondCardGroup.MilitaryDocumentCard.PersonalNumber);
            var billetNumber = new Code(_secondCardGroup.MilitaryDocumentCard.MilitaryBilletCode);

            SecretAccess secretAccess = null;
            if (_secondCardGroup.MilitaryDocumentCard.IsHaveSecretAccess)
            {
                var accessForm = _secondCardGroup.MilitaryDocumentCard.AccessForm.ToAccessFormEnum();
                var number = _secondCardGroup.MilitaryDocumentCard.SecretAccessNumber;

                if (!_secondCardGroup.MilitaryDocumentCard.SecretAccessIssueDate.HasValue)
                {
                    throw new NullReferenceException(nameof(_secondCardGroup.MilitaryDocumentCard.SecretAccessIssueDate));
                }

                secretAccess = new SecretAccess(accessForm, number, 
                    _secondCardGroup.MilitaryDocumentCard.SecretAccessIssueDate.Value);
            }

            var militaryBillet = new MilitaryBillet(billetNumber, secretAccess);

            var proficiencyCategory = _secondCardGroup.ProficiencyCard.ProficiencyCategory.ToProficiencyCategoryEnum();
            var officialStatus = _secondCardGroup.ProficiencyCard.OfficialStatus.ToOfficialStatusEnum();
            var nervously = _secondCardGroup.ProficiencyCard.NervouslyPsychologicalStability.ToNervouslyPsychologicalStatusEnum();
            var general = _secondCardGroup.ProficiencyCard.GeneralPsychologicalStability.ToGeneralPsychologicalStatusEnum();
            var proficiencyCard = new ProficiencyCardInfo(proficiencyCategory, officialStatus, nervously, general);

            var speciality = _secondCardGroup.DistributionCard.Speciality;
            var teamMode = _secondCardGroup.DistributionCard.TeamMode;

            return new MilitaryInfo(personalNumber, militaryBillet, proficiencyCard, speciality, teamMode);
        }

        private CivilInfo BuildCivilInfo()
        {
            var education = _secondCardGroup.CivilCard.Education.ToEducationStatusEnum();
            var profession = _secondCardGroup.CivilCard.Profession;
            var occupation = _secondCardGroup.CivilCard.Occupation.ToOccupationStatusEnum();

            return new CivilInfo(education, profession, occupation);
        }

        private Contacts BuildContacts()
        {
            var mobilePhoneNumber = new PhoneNumber(_thirdCardGroup.ContactsCard.MobilePhone);
            var homePhoneNumber = new PhoneNumber(_thirdCardGroup.ContactsCard.HomePhone);

            return new Contacts(mobilePhoneNumber, homePhoneNumber);
        }

        private FamilyInfo BuildFamilyInfo()
        {
            var parentFamilyStatus = _thirdCardGroup.FamilyCard.ParentFamilyStatus.ToParentFamilyStatusEnum();

            var relatives = new List<RelativeInfo>();
            foreach (var relativeUIModel in _thirdCardGroup.FamilyCard.RelativeInfoUIModels)
            {
                var relativeStatus = relativeUIModel.RelativeStatus.ToRelativeStatusEnum();
                var fullName = new FullName(relativeUIModel.FullName);

                if (!relativeUIModel.BirthDate.HasValue)
                {
                    throw new NullReferenceException(nameof(relativeUIModel.BirthDate));
                }

                var birthInfo = new BirthInfo(relativeUIModel.BirthDate.Value, relativeUIModel.BirthPlace);

                var personInfo = new PersonInfo(fullName, birthInfo);

                var workPlace = relativeUIModel.WorkPlace;

                var relativeInfo = new RelativeInfo(relativeStatus, personInfo, workPlace);
                relatives.Add(relativeInfo);
            }
            
            return new FamilyInfo(parentFamilyStatus, relatives);
        }

        private DriverInfo BuildDriverInfo()
        {
            var code = new Code(_secondCardGroup.DriverCard.DriverLicenseCode);

            if (!_secondCardGroup.DriverCard.DriverLicenseIssueDate.HasValue)
            {
                throw new NullReferenceException(nameof(_secondCardGroup.DriverCard.DriverLicenseIssueDate));
            }

            return new DriverInfo(code, _secondCardGroup.DriverCard.DriverLicenseIssueDate.Value);
        }
    }
}
