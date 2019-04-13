namespace PupaParserComeback.Presentation.Constants
{
    public static class RegexConstants
    {
        public const string PassportCodePattern = @"^\d{4} \d{6}$";
        public const string DevisionCodePattern = @"^\d{3}-\d{3}$";

        public const string PersonalNumberPattern = @"^[А-Я]{1,2} \d{6}$";
        public const string MilitaryBilletCodePattern = @"^[А-Я]{2} \d{7}$";
        public const string SecretAccessNumberPattern = @"^\d{1,4}$";
        
        public const string DriverLicenseCodePattern = @"^[A-ZА-Я0-9]{2,4} \d{2,6}$";

        public const string AdditionalRequirementsTablePattern = @"^(?:\d+(?:[,-][ ]?)?)+$";
        public const string DiseaseArticlesPattern = @"^(?:\d+([а-я])?(?:[,][ ]?)?)+$";

        public const string PhysiologicalCharacteristicPattern = @"^\d{1,3}$";
        public const string ClothingSizePattern = @"^\d{1,3}\/\d{1,3}$";

        public const string VisionPattern = @"^\d[,.]\d\/\d[,.]\d$";
    }
}