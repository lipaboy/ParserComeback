using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class MedicineRankExtensions
    {
        public static string ToMedicineRankString(this MedicineRank source)
        {
            switch (source)
            {
                case MedicineRank.A1: return "А-1";
                case MedicineRank.A2: return "А-2";
                case MedicineRank.A3: return "А-3";
                case MedicineRank.A4: return "А-4";
                case MedicineRank.B1: return "Б-1";
                case MedicineRank.B2: return "Б-2";
                case MedicineRank.B3: return "Б-3";
                case MedicineRank.B4: return "Б-4";
                case MedicineRank.C: return "В";
                case MedicineRank.D: return "Г";
                case MedicineRank.E: return "Е";
            }

            return string.Empty;
        }

        public static MedicineRank ToMedicineRankEnum(this string source)
        {
            switch (source)
            {
                case "А-1": return MedicineRank.A1;
                case "А-2": return MedicineRank.A2;
                case "А-3": return MedicineRank.A3;
                case "А-4": return MedicineRank.A4;
                case "Б-1": return MedicineRank.B1;
                case "Б-2": return MedicineRank.B2;
                case "Б-3": return MedicineRank.B3;
                case "Б-4": return MedicineRank.B4;
                case "В": return MedicineRank.C;
                case "Г": return MedicineRank.D;
                case "Е": return MedicineRank.E;
            }

            return MedicineRank.None;
        }
    }
}
