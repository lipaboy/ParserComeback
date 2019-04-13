using PupaParserComeback.Data.SQLite.Dto;
using PupaParserComeback.Data.SQLite.ExtensionMethods;
using PupaParserComeback.Domain.Constants;
using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.DomainModels.Civil;
using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.DomainModels.Medicine;
using PupaParserComeback.Domain.DomainModels.Military;
using PupaParserComeback.Domain.DomainModels.Passport;
using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using System;

namespace PupaParserComeback.Data.SQLite.Concrete
{
    public static class RecruitInfoMapper
    {
        public static priz Map(RecruitInfo source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var priz = new priz();

            FillServiceInfo(priz, source.ServiceInfo);
            FillCriminalInfo(priz, source.CriminalInfo);
            FillMedicineInfo(priz, source.MedicineInfo);

            FillPassportInfo(priz, source.Envelope.PassportInfo);
            FillMilitaryInfo(priz, source.Envelope.MilitaryInfo);
            FillCivilInfo(priz, source.Envelope.CivilInfo);
            FillContacts(priz, source.Envelope.Contacts);
            FillFamilyInfo(priz, source.Envelope.FamilyInfo);

            if (source.Envelope.IsDriver)
            {
                FillDriverInfo(priz, source.Envelope.DriverInfo);
            }

            return priz;
        }

        private static void FillServiceInfo(priz priz, ServiceInfo serviceInfo)
        {
            if (serviceInfo.SqliteId.HasValue)
            {
                priz.id = serviceInfo.SqliteId.Value;
            }
            
            priz.fb_id = serviceInfo.FirebirdId.ToString();
            priz.rvk = serviceInfo.RegionalCollectionPoint;
            priz.d_advent = serviceInfo.ConscriptionDate.ToString(DateConstants.RecruitDateFormat);
            // TODO: add logic for this parameter
            priz.percent = 100;
        }

        private static void FillCriminalInfo(priz priz, CriminalInfo criminalInfo)
        {
            priz.accounting = criminalInfo.RegisterStatus.ToRegisterStatusString();
            priz.gangsta = criminalInfo.CriminalStatus.ToCriminalStatusString();
        }

        private static void FillMedicineInfo(priz priz, MedicineInfo medicineInfo)
        {
            priz.rank = medicineInfo.Health.MedicineRank.ToMedicineRankString();
            priz.tdt = medicineInfo.Health.AdditionalRequirementsTableGraphs;
            priz.article = medicineInfo.Health.DiseaseArticles;
            priz.eye = medicineInfo.Health.Vision;
            priz.blood_type = medicineInfo.Health.BloodType.ToBloodTypeString();

            priz.height = medicineInfo.PhysiologicalCharacteristics.Height.ToString();
            priz.mass = medicineInfo.PhysiologicalCharacteristics.Weight.ToString();
            priz.head = medicineInfo.PhysiologicalCharacteristics.HeadSize.ToString();
            priz.clothes = medicineInfo.PhysiologicalCharacteristics.ClothingSize;
            priz.shoes = medicineInfo.PhysiologicalCharacteristics.ShoesSize.ToString();

            priz.sport = medicineInfo.SportInfo.Rank.ToSportRankString();
            priz.kind_of_sport = medicineInfo.SportInfo.Kind;
        }

        private static void FillPassportInfo(priz priz, PassportInfo passportInfo)
        {
            priz.g_pass = passportInfo.IssueInfo.IssueBy;
            priz.kod_g_pass = passportInfo.IssueInfo.DevisionCode;
            priz.d_pass = passportInfo.IssueInfo.IssueDate.ToString(DateConstants.RecruitDateFormat);
 
            priz.surname = passportInfo.PersonInfo.FullName.Surname;
            priz.name = passportInfo.PersonInfo.FullName.Name;
            priz.patr_name = passportInfo.PersonInfo.FullName.Patronymic;
            priz.born_date = passportInfo.PersonInfo.BirthInfo.Date.ToString(DateConstants.RecruitDateFormat);
            priz.born_place = passportInfo.PersonInfo.BirthInfo.Place;

            priz.pass = passportInfo.Code.Value;

            priz.register_location = passportInfo.LocationInfo.RegisterLocation.Value;
            priz.actually_location = passportInfo.LocationInfo.ActuallyLocation.Value;
            priz.locality = passportInfo.LocationInfo.Locality;

            priz.family_status = passportInfo.FamilyInfo.FamilyStatus.ToFamilyStatusString();
            priz.baby = passportInfo.FamilyInfo.IsHaveBaby 
                ? PassportFamilyInfo.HaveBaby
                : PassportFamilyInfo.NotHaveBaby;

            priz.photo = passportInfo.PhotoName;
        }

        private static void FillMilitaryInfo(priz priz, MilitaryInfo militaryInfo)
        {
            priz.l_n = militaryInfo.PersonalNumber.Value;

            priz.v_b = militaryInfo.Billet.BilletNumber.Value;
            if (militaryInfo.Billet.IsHaveSecretAccess)
            {
                priz.f_access = militaryInfo.Billet.SecretAccess.AccessForm.ToAccessFormString();
                priz.n_access = militaryInfo.Billet.SecretAccess.SecretAccessNumber;
                priz.d_access = militaryInfo.Billet.SecretAccess.IssueDate.ToString(DateConstants.RecruitDateFormat);
            }

            priz.ppo = militaryInfo.ProficiencyCard.ProficiencyCategory.ToProficiencyCategoryString();
            priz.pp_appointment = militaryInfo.ProficiencyCard.OfficialStatus.ToOfficialStatusString();
            priz.npu = militaryInfo.ProficiencyCard.NervouslyPsychologicalStability.ToNervouslyPsychologicalStatusString();
            priz.ops = militaryInfo.ProficiencyCard.GeneralPsychologicalStability.ToGeneralPsychologicalStatusString();

            priz.vus_va = militaryInfo.Speciality;
            priz.destination = militaryInfo.TeamMode;
        }

        private static void FillCivilInfo(priz priz, CivilInfo civilInfo)
        {
            priz.education = civilInfo.Education.ToEducationStatusString();
            priz.spec = civilInfo.Profession;
            priz.activity = civilInfo.Occupation.ToOccupationStatusString();
        }

        private static void FillContacts(priz priz, Contacts contacts)
        {
            priz.modile_phone = contacts.MobileNumber.Value;
            priz.home_phone = contacts.HomeNumber.Value;
        }

        private static void FillFamilyInfo(priz priz, FamilyInfo familyInfo)
        {
            priz.parents = familyInfo.ParentFamilyStatus.ToParentFamilyStatusString();

            // I didn't want write this code, but db structure force me do it

            int curRelativeIdx = 0;
            RelativeInfo currentRelative = null;

            currentRelative = familyInfo.GetRelative(curRelativeIdx++);
            if (currentRelative != null)
            {
                priz.relation = currentRelative.RelativeStatus.ToRelativeStatusString();
                priz.relative_name = currentRelative.PersonInfo.FullName.Value;
                priz.relative_birth_date = currentRelative.PersonInfo.BirthInfo.Date.ToString(DateConstants.RecruitDateFormat);
                priz.relative_birth_place = currentRelative.PersonInfo.BirthInfo.Place;
                priz.relative_work_place = currentRelative.WorkPlace;
            }

            currentRelative = familyInfo.GetRelative(curRelativeIdx++);
            if (currentRelative != null)
            {
                priz.relation2 = currentRelative.RelativeStatus.ToRelativeStatusString();
                priz.relative_name2 = currentRelative.PersonInfo.FullName.Value;
                priz.relative_birth_date2 = currentRelative.PersonInfo.BirthInfo.Date.ToString(DateConstants.RecruitDateFormat);
                priz.relative_birth_place2 = currentRelative.PersonInfo.BirthInfo.Place;
                priz.relative_work_place2 = currentRelative.WorkPlace;
            }

            currentRelative = familyInfo.GetRelative(curRelativeIdx++);
            if (currentRelative != null)
            {
                priz.relation3 = currentRelative.RelativeStatus.ToRelativeStatusString();
                priz.relative_name3 = currentRelative.PersonInfo.FullName.Value;
                priz.relative_birth_date3 = currentRelative.PersonInfo.BirthInfo.Date.ToString(DateConstants.RecruitDateFormat);
                priz.relative_birth_place3 = currentRelative.PersonInfo.BirthInfo.Place;
                priz.relative_work_place3 = currentRelative.WorkPlace;
            }
        }

        private static void FillDriverInfo(priz priz, DriverInfo driverInfo)
        {
            priz.va = driverInfo.Code.Value;
            priz.va_date = driverInfo.IssueDate.ToString(DateConstants.RecruitDateFormat);
        }
        
        public static RecruitInfo Map(priz source)
        {
            var serviceInfo = BuildServiceInfo(source);
            var criminalInfo = BuildCriminalInfo(source);
            var medicineInfo = BuildMedicineInfo(source);

            var passportInfo = BuildPassportInfo(source);
            var militaryInfo = BuildMilitaryInfo(source);
            var civilInfo = BuildCivilInfo(source);
            var contacts = BuildContacts(source);
            var familyInfo = BuildFamilyInfo(source);

            DriverInfo driverInfo = null;
            if (!string.IsNullOrWhiteSpace(source.va))
            {
                driverInfo = BuildDriverInfo(source);
            }

            var envelope = new Envelope(passportInfo, militaryInfo, civilInfo, contacts, familyInfo, driverInfo);

            return new RecruitInfo(serviceInfo, criminalInfo, medicineInfo, envelope);
        }

        private static ServiceInfo BuildServiceInfo(priz priz)
        {
            DateTime? conscriptionDate = priz.d_advent.GetDateTime();
            if (!conscriptionDate.HasValue)
            {
                throw new ArgumentException(nameof(priz));
            }

            int formId;
            return new ServiceInfo(sqliteId: priz.id, 
                                   firebirdId: int.TryParse(priz.fb_id, out formId) ? formId : (int?)null,
                                   regionalCollecitonPoint: priz.rvk,
                                   conscriptionDate: conscriptionDate.Value);
        }

        private static CriminalInfo BuildCriminalInfo(priz priz)
        {
            return new CriminalInfo(registerStatus: priz.accounting.ToRegisterStatusEnum(),
                                    criminalStatus: priz.gangsta.ToCriminalStatusEnum());
        }

        private static MedicineInfo BuildMedicineInfo(priz priz)
        {
            var health = new Health(medicineRank: priz.rank.ToMedicineRankEnum(),
                                    additionalRequirementsTable: priz.tdt,
                                    diseaseArticles: priz.article,
                                    vision: priz.eye,
                                    bloodType: priz.blood_type.ToBloodTypeEnum());

            int h;
            var height = int.TryParse(priz.height, out h)
                ? h : (int?)null;

            int w;
            var weight = int.TryParse(priz.mass, out w)
                ? w : (int?)null;

            int hs;
            var headSize = int.TryParse(priz.head, out hs)
                ? hs : (int?)null;

            var clothingSize = priz.clothes;

            int ss;
            var shoesSize = int.TryParse(priz.shoes, out ss)
                ? ss : (int?)null;

            var physiologicalCharacteristics = new PhysiologicalCharacteristics(height, weight, 
                headSize, clothingSize, shoesSize);

            var sportInfo = new SportInfo(rank: priz.sport.ToSportRankEnum(),
                                          kind: priz.kind_of_sport);

            return new MedicineInfo(health, physiologicalCharacteristics, sportInfo);
        }

        private static PassportInfo BuildPassportInfo(priz priz)
        {
            DateTime? passportIssueDate = priz.d_pass.GetDateTime();
            if (!passportIssueDate.HasValue)
            {
                throw new ArgumentException(nameof(priz));
            }

            DateTime? birthdate = priz.born_date.GetDateTime();
            if (!birthdate.HasValue)
            {
                throw new ArgumentException(nameof(priz));
            }

            var photoName = priz.photo;

            var passportCode = new Code(priz.pass);

            var issueInfo = new PassportIssueInfo(issueBy: priz.g_pass,
                                                  devisionCode: priz.kod_g_pass,
                                                  issueDate: passportIssueDate.Value);

            var fullName = new FullName(priz.surname, priz.name, priz.patr_name);
            var birthInfo = new BirthInfo(date: birthdate.Value,
                                          place: priz.born_place);

            var personInfo = new PersonInfo(fullName, birthInfo);
            
            var registerLocation = new Address(priz.register_location);
            var actuallyLocation = new Address(priz.actually_location);
            var locality = priz.locality;

            var locationInfo = new PassportLocationInfo(registerLocation, actuallyLocation, locality);
            
            var passportFamilyInfo = new PassportFamilyInfo(familyStatus: priz.family_status.ToFamilyStatusEnum(),
                                                            isHaveBaby: priz.baby == PassportFamilyInfo.HaveBaby);

            return new PassportInfo(photoName, passportCode, issueInfo, personInfo, locationInfo, passportFamilyInfo);
        }

        private static MilitaryInfo BuildMilitaryInfo(priz priz)
        {
            var personalNumber = new Code(priz.l_n);
            var biletNumber = new Code(priz.v_b);
            var secretAccess = BuildSecretAccess(priz);
            var militaryBilet = new MilitaryBillet(biletNumber, secretAccess);
            
            var card = new ProficiencyCardInfo(proficiencyCategory: priz.ppo.ToProficiencyCategoryEnum(),
                                           officialstatus: priz.pp_appointment.ToOfficialStatusEnum(),
                                           nervously: priz.npu.ToNervouslyPsychologicalStatusEnum(),
                                           general: priz.ops.ToGeneralPsychologicalStatusEnum());
            
            var speciality = !string.IsNullOrWhiteSpace(priz.vus_va) 
                ? priz.vus_va 
                : MilitaryInfo.NoSpeciality;

            var teamMode = !string.IsNullOrWhiteSpace(priz.destination) 
                ? priz.destination 
                : MilitaryInfo.NoTeamMode;

            return new MilitaryInfo(personalNumber, militaryBilet, card, speciality, teamMode);
        }

        private static SecretAccess BuildSecretAccess(priz priz)
        {
            SecretAccess secretAccess = null;
            
            var accessForm = priz.f_access.ToAccessFormEnum();
            if (accessForm != AccessForm.None)
            {
                DateTime? secretAccessIssueDate = priz.d_access.GetDateTime();
                if (!secretAccessIssueDate.HasValue)
                {
                    throw new ArgumentException(nameof(priz));
                }
                
                var secretAccessNumber = priz.n_access;
                var issueDate = secretAccessIssueDate.Value;

                secretAccess = new SecretAccess(accessForm, secretAccessNumber, issueDate);
            }

            return secretAccess;
        }

        private static CivilInfo BuildCivilInfo(priz priz)
        {
            return new CivilInfo(education: priz.education.ToEducationStatusEnum(),
                                 profession: priz.spec,
                                 occupation: priz.activity.ToOccupationStatusEnum());
        }

        private static Contacts BuildContacts(priz priz)
        {
            var mobile = new PhoneNumber(priz.modile_phone);
            var home = new PhoneNumber(priz.home_phone);

            return new Contacts(mobile, home);
        }

        private static FamilyInfo BuildFamilyInfo(priz priz)
        {
            var parentFamilyStatus = priz.parents.ToParentFamilyStatusEnum();
            var familyInfo = new FamilyInfo(parentFamilyStatus);

            if (!string.IsNullOrWhiteSpace(priz.relation))
            {
                DateTime? birthdate = priz.relative_birth_date.GetDateTime();
                if (!birthdate.HasValue)
                {
                    throw new ArgumentException(nameof(priz));
                }

                var fullName = new FullName(priz.relative_name);
                var birthInfo = new BirthInfo(date: birthdate.Value,
                                              place: priz.relative_birth_place);

                var personInfo = new PersonInfo(fullName, birthInfo);
                var relative = new RelativeInfo(relativeStatus: priz.relation.ToRelativeStatusEnum(),
                                                personInfo: personInfo,
                                                workPlace: priz.relative_work_place);

                familyInfo.AddRelative(relative);
            }

            if (!string.IsNullOrWhiteSpace(priz.relation2))
            {
                DateTime? birthdate = priz.relative_birth_date2.GetDateTime();
                if (!birthdate.HasValue)
                {
                    throw new ArgumentException(nameof(priz));
                }

                var fullName = new FullName(priz.relative_name2);
                var birthInfo = new BirthInfo(date: birthdate.Value,
                                              place: priz.relative_birth_place2);

                var personInfo = new PersonInfo(fullName, birthInfo);
                var relative = new RelativeInfo(relativeStatus: priz.relation2.ToRelativeStatusEnum(),
                                                personInfo: personInfo,
                                                workPlace: priz.relative_work_place2);

                familyInfo.AddRelative(relative);
            }

            if (!string.IsNullOrWhiteSpace(priz.relation3))
            {
                DateTime? birthdate = priz.relative_birth_date3.GetDateTime();
                if (!birthdate.HasValue)
                {
                    throw new ArgumentException(nameof(priz));
                }

                var fullName = new FullName(priz.relative_name3);
                var birthInfo = new BirthInfo(date: birthdate.Value,
                                              place: priz.relative_birth_place3);

                var personInfo = new PersonInfo(fullName, birthInfo);
                var relative = new RelativeInfo(relativeStatus: priz.relation3.ToRelativeStatusEnum(),
                                                personInfo: personInfo,
                                                workPlace: priz.relative_work_place3);

                familyInfo.AddRelative(relative);
            }

            return familyInfo;
        }

        private static DriverInfo BuildDriverInfo(priz priz)
        {
            DateTime? driverIssueDate = priz.va_date.GetDateTime();
            if (!driverIssueDate.HasValue)
            {
                throw new ArgumentException(nameof(priz));
            }

            var code = new Code(priz.va);
            var issueDate = driverIssueDate.Value;

            return new DriverInfo(code, issueDate);
        }
    }
}
