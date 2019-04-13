using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.DomainModels.Military;
using PupaParserComeback.Domain.DomainModels.Passport;
using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using PupaParserComeback.Import.Constants;
using PupaParserComeback.Presentation.Models.CardGroups;
using PupaParserComeback.Presentation.Models.Cards;
using System;
using System.Collections.ObjectModel;

namespace PupaParserComeback.Import
{
    public class RecruitCardGroupBuilder
    {
        private const string _defaultOccupation = "Не работает и не учится";

        private readonly string _personalPhotoDirectoryPath;

        private readonly string _rcp;
        private readonly string[] _words;

        public RecruitCardGroupBuilder(string personalPhotoDirectoryPath,
            string rcp, string[] words)
        {
            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }

            if (rcp == null)
            {
                throw new ArgumentNullException(nameof(rcp));
            }

            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            if (words.Length != PupaRecruitImporter.RecruitWordsCount)
            {
                throw new ArgumentException(nameof(words));
            }
            
            _personalPhotoDirectoryPath = personalPhotoDirectoryPath;

            _rcp = rcp;
            _words = words;
        }

        public RecruitCardGroup Build()
        {
            var serviceCard = CreateServiceCard();
            var firstCardGroup = CreateFirstCardGroup();
            var secondCardGroup = CreateSecondCardGroup();
            var thirdCardGroup = CreateThirdCardGroup();

            return new RecruitCardGroup(serviceCard, firstCardGroup, secondCardGroup, thirdCardGroup);
        }

        #region Service Card

        private ServiceCard CreateServiceCard()
        {
            return new ServiceCard() { RegionalCollectionPoint = _rcp };
        }

        #endregion

        #region First Card

        private FirstCardGroup CreateFirstCardGroup()
        {
            var passportInfoCard = CreatePassportInfoCard();
            var passportPersonInfoCard = CreatePassportPersonInfoCard();
            var passportAccommodationCard = CreatePassportAccommodationCard();
            var passportFamilyInfoCard = CreatePassportFamilyInfoCard();
            var criminalCard = CreateCriminalCard();

            return new FirstCardGroup(passportInfoCard,
                passportPersonInfoCard,
                passportAccommodationCard,
                passportFamilyInfoCard,
                criminalCard);
        }

        private PassportInfoCard CreatePassportInfoCard()
        {
            DateTime outIssueDate;
            DateTime? issueDate = DateTime.TryParse(_words[8], out outIssueDate)
                ? outIssueDate
                : (DateTime?)null;

            return new PassportInfoCard()
            {
                Code = _words[7],
                IssueBy = _words[9],
                IssueDate = issueDate,
                DevisionCode = _words[63].Split(new string[] { Environment.NewLine },
                    StringSplitOptions.None)[0]
            };
        }

        private PassportPersonInfoCard CreatePassportPersonInfoCard()
        {
            DateTime outBirthDate;
            DateTime? birthDate = DateTime.TryParse(_words[4], out outBirthDate)
                ? outBirthDate
                : (DateTime?)null;

            return new PassportPersonInfoCard(_personalPhotoDirectoryPath)
            {
                PhotoName = _words[51],
                Surname = _words[1],
                Name = _words[2],
                Patronymic = _words[3],
                BirthDate = birthDate,
                BirthPlace = _words[35]
            };
        }

        private PassportAccommodationCard CreatePassportAccommodationCard()
        {
            return new PassportAccommodationCard()
            {
                RegisterLocation = _words[37],
                ActuallyLocation = _words[38]
            };
        }

        private PassportFamilyInfoCard CreatePassportFamilyInfoCard()
        {
            return new PassportFamilyInfoCard()
            {
                FamilyStatus = _words[23],
                IsHaveBaby = _words[24] == PassportFamilyInfo.HaveBaby
            };
        }

        private CriminalCard CreateCriminalCard()
        {
            var criminalStatus = _words[21];
            var registerStatus = _words[22];

            return new CriminalCard()
            {
                RegisterStatus = !string.IsNullOrWhiteSpace(registerStatus) ? registerStatus : RegisterStatus.WasNot.ToRegisterStatusString(),
                CriminalStatus = !string.IsNullOrWhiteSpace(criminalStatus) ? criminalStatus : CriminalStatus.HaveNot.ToCriminalStatusString()
            };
        }

        #endregion

        #region Second Card

        private SecondCardGroup CreateSecondCardGroup()
        {
            var militaryDocumentCard = CreateMilitaryDocumentCard();
            var proficiencyCard = CreateProficiencyCard();
            var driverCard = CreateDriverCard();
            var distributionCard = CreateDistributionCard();
            var civilCard = CreateCivilCard();

            return new SecondCardGroup(militaryDocumentCard,
                proficiencyCard,
                driverCard,
                distributionCard,
                civilCard);
        }

        private MilitaryDocumentCard CreateMilitaryDocumentCard()
        {
            int serieLength = 2;
            var personalNumber = !string.IsNullOrWhiteSpace(_words[5])
                ? _words[5].Insert(serieLength, ControlChars.Space.ToString())
                : string.Empty;

            var militaryDocumentCard = new MilitaryDocumentCard()
            {
                PersonalNumber = personalNumber,
                MilitaryBilletCode = _words[6]
            };

            bool isHaveSecretAccess = !string.IsNullOrWhiteSpace(_words[14]);
            if (isHaveSecretAccess)
            {
                militaryDocumentCard.IsHaveSecretAccess = isHaveSecretAccess;
                militaryDocumentCard.AccessForm = _words[14];
                militaryDocumentCard.SecretAccessNumber = _words[15];

                DateTime outIssueDate;
                militaryDocumentCard.SecretAccessIssueDate = DateTime.TryParse(_words[16], out outIssueDate)
                    ? outIssueDate
                    : (DateTime?)null;
            }

            return militaryDocumentCard;
        }

        private ProficiencyCard CreateProficiencyCard()
        {
            return new ProficiencyCard()
            {
                ProficiencyCategory = _words[17],
                OfficialStatus = _words[49],
                NervouslyPsychologicalStability = _words[18],
                GeneralPsychologicalStability = _words[48]
            };
        }

        private DriverCard CreateDriverCard()
        {
            var driverCard = new DriverCard();
            driverCard.IsDriver = !string.IsNullOrWhiteSpace(_words[10]);

            if (driverCard.IsDriver)
            {
                driverCard.DriverLicenseCode = _words[10];

                DateTime outIssueDate;
                driverCard.DriverLicenseIssueDate = DateTime.TryParse(_words[42], out outIssueDate)
                    ? outIssueDate
                    : (DateTime?)null;
            }

            return driverCard;
        }

        private DistributionCard CreateDistributionCard()
        {
            return new DistributionCard()
            {
                Speciality = _words[11],
                TeamMode = _words[13]
            };
        }

        private CivilCard CreateCivilCard()
        {
            var occupation = _words[25];

            return new CivilCard()
            {
                Education = _words[19],
                Profession = _words[12],
                Occupation = occupation != _defaultOccupation
                    ? occupation
                    : OccupationStatus.NotWorkNotStudy.ToOccupationStatusString()
            };
        }

        #endregion

        #region Third Card

        private ThirdCardGroup CreateThirdCardGroup()
        {
            var medicineCard = CreateMedicineCard();
            var physiologicalCharacteristicsCard = CreatePhysiologicalCharacteristicsCard();
            var sportCard = CreateSportCard();
            var contactsCard = CreateContactsCard();
            var familyCard = CreateFamilyCard();

            return new ThirdCardGroup(medicineCard,
                physiologicalCharacteristicsCard,
                sportCard,
                contactsCard,
                familyCard);
        }

        private MedicineCard CreateMedicineCard()
        {
            return new MedicineCard()
            {
                Rank = _words[26],
                AdditionalRequirementsTable = _words[27],
                DiseaseArticles = _words[31],
                Vision = _words[28],
                BloodType = _words[41]
            };
        }

        private PhysiologicalCharacteristicsCard CreatePhysiologicalCharacteristicsCard()
        {
            return new PhysiologicalCharacteristicsCard()
            {
                Height = _words[29],
                Weight = _words[30],
                HeadSize = _words[32],
                ClothingSize = _words[33],
                ShoesSize = _words[34]
            };
        }

        private SportCard CreateSportCard()
        {
            return new SportCard()
            {
                Rank = _words[20],
                Kind = _words[50]
            };
        }

        private ContactsCard CreateContactsCard()
        {
            return new ContactsCard()
            {
                MobilePhone = _words[39],
                HomePhone = _words[40]
            };
        }

        private FamilyCard CreateFamilyCard()
        {
            var relativeInfoUIModels = new ObservableCollection<RelativeInfoCard>();

            if (!string.IsNullOrWhiteSpace(_words[43]))
            {
                DateTime outParentBirthDate;
                DateTime? parentBirthDate = DateTime.TryParse(_words[45], out outParentBirthDate)
                    ? outParentBirthDate
                    : (DateTime?)null;

                var birthPlace = _words[46];
                var workPlace = _words[47];

                relativeInfoUIModels.Add(new RelativeInfoCard()
                {
                    RelativeStatus = _words[43],
                    FullName = _words[44],
                    BirthDate = parentBirthDate,
                    BirthPlace = !string.IsNullOrWhiteSpace(birthPlace) ? birthPlace : BirthInfo.UnknownPlace,
                    WorkPlace = !string.IsNullOrWhiteSpace(workPlace) ? workPlace : RelativeInfo.NotWorking
                });
            }

            if (!string.IsNullOrWhiteSpace(_words[53]))
            {
                DateTime outParentBirthDate;
                DateTime? parentBirthDate = DateTime.TryParse(_words[55], out outParentBirthDate)
                    ? outParentBirthDate
                    : (DateTime?)null;

                var birthPlace = _words[56];
                var workPlace = _words[57];

                relativeInfoUIModels.Add(new RelativeInfoCard()
                {
                    RelativeStatus = _words[53],
                    FullName = _words[54],
                    BirthDate = parentBirthDate,
                    BirthPlace = !string.IsNullOrWhiteSpace(birthPlace) ? birthPlace : BirthInfo.UnknownPlace,
                    WorkPlace = !string.IsNullOrWhiteSpace(workPlace) ? workPlace : RelativeInfo.NotWorking
                });
            }

            if (!string.IsNullOrWhiteSpace(_words[58]))
            {
                DateTime outParentBirthDate;
                DateTime? parentBirthDate = DateTime.TryParse(_words[60], out outParentBirthDate)
                    ? outParentBirthDate
                    : (DateTime?)null;

                var birthPlace = _words[61];
                var workPlace = _words[62];

                relativeInfoUIModels.Add(new RelativeInfoCard()
                {
                    RelativeStatus = _words[58],
                    FullName = _words[59],
                    BirthDate = parentBirthDate,
                    BirthPlace = !string.IsNullOrWhiteSpace(birthPlace) ? birthPlace : BirthInfo.UnknownPlace,
                    WorkPlace = !string.IsNullOrWhiteSpace(workPlace) ? workPlace : RelativeInfo.NotWorking
                });
            }

            return new FamilyCard()
            {
                ParentFamilyStatus = _words[36],
                RelativeInfoUIModels = relativeInfoUIModels,
                SelectedRelativeInfoUIModel = relativeInfoUIModels.Count > 0
                    ? relativeInfoUIModels[0]
                    : null
            };
        }

        #endregion
    }
}
