using PupaParserComeback.Data.Firebird.Dto;
using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.DomainModels.Civil;
using PupaParserComeback.Domain.DomainModels.Medicine;
using PupaParserComeback.Domain.DomainModels.Military;
using PupaParserComeback.Domain.DomainModels.Passport;
using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using System;

namespace PupaParserComeback.Data.Firebird.Concrete
{
    public class RecruitInfoMapper
    {
        private const string FullProfessionalEducation = "профессиональное";
        private const string ShortProfessionalEducation = "проф.";

        public static PRIZ Map(RecruitInfo source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var priz = new PRIZ();

            FillServiceInfo(priz, source.ServiceInfo);
            FillCriminalInfo(priz, source.CriminalInfo);
            FillMedicineInfo(priz, source.MedicineInfo);

            FillPassportInfo(priz, source.Envelope.PassportInfo);
            FillMilitaryInfo(priz, source.Envelope.MilitaryInfo);
            FillCivilInfo(priz, source.Envelope.CivilInfo);
            FillFamilyInfo(priz, source.Envelope.FamilyInfo);

            if (source.Envelope.IsDriver)
            {
                FillDriverInfo(priz, source.Envelope.DriverInfo);
            }

            FillDefaultInfo(priz);

            return priz;
        }

        private static void FillServiceInfo(PRIZ priz, ServiceInfo serviceInfo)
        {
            if (serviceInfo.FirebirdId.HasValue)
            {
                priz.ID = serviceInfo.FirebirdId.Value;
            }

            priz.RVK = serviceInfo.RegionalCollectionPoint;
            priz.D_PRIB = serviceInfo.ConscriptionDate;
        }

        private static void FillCriminalInfo(PRIZ priz, CriminalInfo criminalInfo)
        {
            priz.NA_UCHETE = criminalInfo.RegisterStatus.ToRegisterStatusString();
            priz.SUD = criminalInfo.CriminalStatus.ToCriminalStatusString();
        }

        private static void FillMedicineInfo(PRIZ priz, MedicineInfo medicineInfo)
        {
            var rank = medicineInfo.Health.MedicineRank.ToMedicineRankString();

            var words = rank.Split('-');
            if (words.Length == 2)
            {
                priz.GODN = words[0];
                priz.P_PREDN = int.Parse(words[1]);
            }
            else
            {
                priz.GODN = words[0];
            }

            priz.TDT = medicineInfo.Health.AdditionalRequirementsTableGraphs;
            priz.STAT = medicineInfo.Health.DiseaseArticles;
            priz.ZREN = !string.IsNullOrWhiteSpace(medicineInfo.Health.Vision) 
                ? medicineInfo.Health.Vision.Replace("/", @"\")
                : string.Empty;

            priz.ROST = medicineInfo.PhysiologicalCharacteristics.Height;
            priz.MASSA = medicineInfo.PhysiologicalCharacteristics.Weight;
            priz.R_G_U = medicineInfo.PhysiologicalCharacteristics.HeadSize.ToString();

            priz.R_O_G = !string.IsNullOrWhiteSpace(medicineInfo.PhysiologicalCharacteristics.ClothingSize) 
                ? medicineInfo.PhysiologicalCharacteristics.ClothingSize.Replace("/", @"\")
                : string.Empty;

            priz.R_OB = medicineInfo.PhysiologicalCharacteristics.ShoesSize.ToString();

            priz.IMEET_RAZR = medicineInfo.SportInfo.Rank == SportRank.HaveNot ? 0 : 1;
        }

        private static void FillPassportInfo(PRIZ priz, PassportInfo passportInfo)
        {
            priz.KEM_VIDAN = passportInfo.IssueInfo.IssueBy;
            priz.D_PASPORT = passportInfo.IssueInfo.IssueDate;

            priz.FAM = passportInfo.PersonInfo.FullName.Surname;
            priz.IM = passportInfo.PersonInfo.FullName.Name;
            priz.OTCH = passportInfo.PersonInfo.FullName.Patronymic;
            priz.D_ROD = passportInfo.PersonInfo.BirthInfo.Date;
            priz.M_ROD = passportInfo.PersonInfo.BirthInfo.Place;

            priz.S_PASPORT = passportInfo.Code.Serie;
            priz.N_PASPORT = passportInfo.Code.Number;

            priz.BRAK = passportInfo.FamilyInfo.FamilyStatus.ToFamilyStatusString();
            priz.IMEET_REB = passportInfo.FamilyInfo.IsHaveBaby ? 1 : 0;
        }

        private static void FillMilitaryInfo(PRIZ priz, MilitaryInfo militaryInfo)
        {
            priz.LN_SER = militaryInfo.PersonalNumber.Serie;
            priz.LN_NUM = militaryInfo.PersonalNumber.Number;

            priz.S_V_BIL = militaryInfo.Billet.BilletNumber.Serie;
            priz.N_V_BIL = militaryInfo.Billet.BilletNumber.Number;

            if (militaryInfo.Billet.IsHaveSecretAccess)
            {
                var af = militaryInfo.Billet.SecretAccess.AccessForm;
                priz.F_DOP = af != AccessForm.None ? (int?)af : null;

                priz.N_DOP = militaryInfo.Billet.SecretAccess.SecretAccessNumber;
                priz.D_DOP = militaryInfo.Billet.SecretAccess.IssueDate;
            }

            priz.PROF_P = militaryInfo.ProficiencyCard.ProficiencyCategory.ToProficiencyCategoryString();
            priz.NPU = militaryInfo.ProficiencyCard.NervouslyPsychologicalStability.ToNervouslyPsychologicalStatusString();

            priz.SPEC = militaryInfo.Speciality;
            priz.REZH_KOM = militaryInfo.TeamMode;
        }

        private static void FillCivilInfo(PRIZ priz, CivilInfo civilInfo)
        {
            priz.OBRAZOV = civilInfo.Education.ToEducationStatusString()
                .Replace(FullProfessionalEducation, ShortProfessionalEducation);

            priz.DO_PRIZ = civilInfo.Occupation.ToOccupationStatusString();
        }

        private static void FillFamilyInfo(PRIZ priz, FamilyInfo familyInfo)
        {
            priz.ODIN_ROD = familyInfo.IsOneParent ? 1 : 0;
            priz.BEZ_ROD = familyInfo.IsWithoutParents ? 1 : 0;
        }

        private static void FillDriverInfo(PRIZ priz, DriverInfo driverInfo)
        {
            var serie = driverInfo.Code.Serie;
            var number = driverInfo.Code.Number;

            priz.S_VA = !string.IsNullOrWhiteSpace(serie) ? serie : string.Empty;
            priz.N_VA = !string.IsNullOrWhiteSpace(number) ? number : string.Empty;
        }

        private static void FillDefaultInfo(PRIZ priz)
        {
            priz.NAVY = 0;
            priz.FL_UB = 0;
            priz.POSTO = 0;
            priz.N_KOM = "0";
            priz.H = "0";
        }
    }
}
