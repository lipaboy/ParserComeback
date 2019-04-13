using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using PupaParserComeback.Presentation.Models.CardGroups;
using PupaParserComeback.Presentation.Models.Cards;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PupaParserComeback.Presentation.Mappers
{
    public class CardGroupMapper
    {
        private readonly string _personalPhotoDirectoryPath;
        private readonly RecruitInfo _recruitInfo;

        public CardGroupMapper(string personalPhotoDirectoryPath,
            RecruitInfo recruitInfo)
        {
            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }

            if (recruitInfo == null)
            {
                throw new ArgumentNullException(nameof(recruitInfo));
            }
            
            _personalPhotoDirectoryPath = personalPhotoDirectoryPath;

            _recruitInfo = recruitInfo;
        }

        public RecruitCardGroup Map()
        {
            var serviceCard = BuildServiceCard();
            var firstCardGroup = BuildFirstCardGroup();
            var secondCardGroup = BuildSecondCard();
            var thirdCardGroup = BuildThirdCardGroup();

            return new RecruitCardGroup(serviceCard,
                firstCardGroup,
                secondCardGroup,
                thirdCardGroup);
        }

        private ServiceCard BuildServiceCard()
        {
            return new ServiceCard()
            {
                SqliteId = _recruitInfo.ServiceInfo.SqliteId.HasValue 
                    ? _recruitInfo.ServiceInfo.SqliteId.Value.ToString() 
                    : string.Empty,

                FirebirdId = _recruitInfo.ServiceInfo.FirebirdId.HasValue
                    ? _recruitInfo.ServiceInfo.FirebirdId.Value.ToString()
                    : string.Empty,

                RegionalCollectionPoint = _recruitInfo.ServiceInfo.RegionalCollectionPoint,
                ConscriptionDate = _recruitInfo.ServiceInfo.ConscriptionDate
            };
        }

        private FirstCardGroup BuildFirstCardGroup()
        {
            var passportInfo = new PassportInfoCard()
            {
                Code = _recruitInfo.Envelope.PassportInfo.Code.Value,
                IssueBy = _recruitInfo.Envelope.PassportInfo.IssueInfo.IssueBy,
                IssueDate = _recruitInfo.Envelope.PassportInfo.IssueInfo.IssueDate,
                DevisionCode = _recruitInfo.Envelope.PassportInfo.IssueInfo.DevisionCode
            };

            var passportPersonInfoCard = new PassportPersonInfoCard(_personalPhotoDirectoryPath)
            {
                PhotoName = _recruitInfo.Envelope.PassportInfo.PhotoName,
                Surname = _recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Surname,
                Name = _recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Name,
                Patronymic = _recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Patronymic,
                BirthDate = _recruitInfo.Envelope.PassportInfo.PersonInfo.BirthInfo.Date,
                BirthPlace = _recruitInfo.Envelope.PassportInfo.PersonInfo.BirthInfo.Place
            };

            var passportAccommodationCard = new PassportAccommodationCard()
            {
                Locality = _recruitInfo.Envelope.PassportInfo.LocationInfo.Locality,
                RegisterLocation = _recruitInfo.Envelope.PassportInfo.LocationInfo.RegisterLocation.Value,
                ActuallyLocation = _recruitInfo.Envelope.PassportInfo.LocationInfo.ActuallyLocation.Value,
            };

            var passportFamilyInfoCard = new PassportFamilyInfoCard()
            {
                FamilyStatus = _recruitInfo.Envelope.PassportInfo.FamilyInfo.FamilyStatus.ToFamilyStatusString(),
                IsHaveBaby = _recruitInfo.Envelope.PassportInfo.FamilyInfo.IsHaveBaby
            };

            var criminalCard = new CriminalCard()
            {
                RegisterStatus = _recruitInfo.CriminalInfo.RegisterStatus.ToRegisterStatusString(),
                CriminalStatus = _recruitInfo.CriminalInfo.CriminalStatus.ToCriminalStatusString()
            };

            return new FirstCardGroup(passportInfo,
                passportPersonInfoCard,
                passportAccommodationCard,
                passportFamilyInfoCard,
                criminalCard);
        }

        private SecondCardGroup BuildSecondCard()
        {
            bool isHaveSecretAccess = _recruitInfo.Envelope.MilitaryInfo.Billet.IsHaveSecretAccess;
            var militaryDocumentCard = new MilitaryDocumentCard()
            {
                PersonalNumber = _recruitInfo.Envelope.MilitaryInfo.PersonalNumber.Value,
                MilitaryBilletCode = _recruitInfo.Envelope.MilitaryInfo.Billet.BilletNumber.Value,
                IsHaveSecretAccess = isHaveSecretAccess,

                AccessForm = isHaveSecretAccess 
                    ? _recruitInfo.Envelope.MilitaryInfo.Billet.SecretAccess.AccessForm.ToAccessFormString() 
                    : string.Empty,

                SecretAccessNumber = isHaveSecretAccess
                    ? _recruitInfo.Envelope.MilitaryInfo.Billet.SecretAccess.SecretAccessNumber
                    : string.Empty,

                SecretAccessIssueDate = isHaveSecretAccess
                    ? _recruitInfo.Envelope.MilitaryInfo.Billet.SecretAccess.IssueDate
                    : (DateTime?)null
            };
            
            var proficiencyCard = new ProficiencyCard()
            {
                ProficiencyCategory = _recruitInfo.Envelope.MilitaryInfo.ProficiencyCard.ProficiencyCategory.ToProficiencyCategoryString(),
                OfficialStatus = _recruitInfo.Envelope.MilitaryInfo.ProficiencyCard.OfficialStatus.ToOfficialStatusString(),
                NervouslyPsychologicalStability = _recruitInfo.Envelope.MilitaryInfo.ProficiencyCard.NervouslyPsychologicalStability.ToNervouslyPsychologicalStatusString(),
                GeneralPsychologicalStability = _recruitInfo.Envelope.MilitaryInfo.ProficiencyCard.GeneralPsychologicalStability.ToGeneralPsychologicalStatusString()
            };

            bool isDriver = _recruitInfo.Envelope.IsDriver;
            var driverCard = new DriverCard()
            {
                IsDriver = isDriver,
                
                DriverLicenseCode = isDriver
                    ? _recruitInfo.Envelope.DriverInfo.Code.Value
                    : string.Empty,

                DriverLicenseIssueDate = isDriver
                    ? _recruitInfo.Envelope.DriverInfo.IssueDate
                    : (DateTime?)null
            };

            var distributionCard = new DistributionCard()
            {
                Speciality = _recruitInfo.Envelope.MilitaryInfo.Speciality,
                TeamMode = _recruitInfo.Envelope.MilitaryInfo.TeamMode
            };

            var civilCard = new CivilCard()
            {
                Education = _recruitInfo.Envelope.CivilInfo.Education.ToEducationStatusString(),
                Profession = _recruitInfo.Envelope.CivilInfo.Profession,
                Occupation = _recruitInfo.Envelope.CivilInfo.Occupation.ToOccupationStatusString()
            };

            return new SecondCardGroup(militaryDocumentCard,
                proficiencyCard,
                driverCard,
                distributionCard,
                civilCard);
        }

        private ThirdCardGroup BuildThirdCardGroup()
        {
            var medicineCard = new MedicineCard()
            {
                Rank = _recruitInfo.MedicineInfo.Health.MedicineRank.ToMedicineRankString(),
                AdditionalRequirementsTable = _recruitInfo.MedicineInfo.Health.AdditionalRequirementsTableGraphs,
                DiseaseArticles = _recruitInfo.MedicineInfo.Health.DiseaseArticles,
                Vision = _recruitInfo.MedicineInfo.Health.Vision,
                BloodType = _recruitInfo.MedicineInfo.Health.BloodType.ToBloodTypeString()
            };

            var physiologicalCharacteristicsCard = new PhysiologicalCharacteristicsCard()
            {
                Height = _recruitInfo.MedicineInfo.PhysiologicalCharacteristics.Height.ToString(),
                Weight = _recruitInfo.MedicineInfo.PhysiologicalCharacteristics.Weight.ToString(),
                HeadSize = _recruitInfo.MedicineInfo.PhysiologicalCharacteristics.HeadSize.ToString(),
                ClothingSize = _recruitInfo.MedicineInfo.PhysiologicalCharacteristics.ClothingSize,
                ShoesSize = _recruitInfo.MedicineInfo.PhysiologicalCharacteristics.ShoesSize.ToString()
            };

            var sportCard = new SportCard()
            {
                Rank = _recruitInfo.MedicineInfo.SportInfo.Rank.ToSportRankString(),
                Kind = _recruitInfo.MedicineInfo.SportInfo.Kind
            };

            var contactsCard = new ContactsCard()
            {
                HomePhone = _recruitInfo.Envelope.Contacts.HomeNumber.Value,
                MobilePhone = _recruitInfo.Envelope.Contacts.MobileNumber.Value
            };
            
            var relativeInfoUIModels = new List<RelativeInfoCard>();
            foreach (var relative in _recruitInfo.Envelope.FamilyInfo.Relatives)
            {
                var relativeInfoUIModel = new RelativeInfoCard()
                {
                    RelativeStatus = relative.RelativeStatus.ToRelativeStatusString(),
                    FullName = relative.PersonInfo.FullName.Value,
                    BirthDate = relative.PersonInfo.BirthInfo.Date,
                    BirthPlace = relative.PersonInfo.BirthInfo.Place,
                    WorkPlace = relative.WorkPlace
                };

                relativeInfoUIModels.Add(relativeInfoUIModel);
            }

            var familyCard = new FamilyCard()
            {
                ParentFamilyStatus = _recruitInfo.Envelope.FamilyInfo.ParentFamilyStatus.ToParentFamilyStatusString(),
                RelativeInfoUIModels = new ObservableCollection<RelativeInfoCard>(relativeInfoUIModels),
                SelectedRelativeInfoUIModel = relativeInfoUIModels.FirstOrDefault()
            };

            return new ThirdCardGroup(medicineCard,
                physiologicalCharacteristicsCard,
                sportCard,
                contactsCard,
                familyCard);
        }
    }
}
