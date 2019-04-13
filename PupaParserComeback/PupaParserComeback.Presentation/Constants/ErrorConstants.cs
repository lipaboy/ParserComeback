namespace PupaParserComeback.Presentation.Constants
{
    public class ErrorConstants
    {
        public const string FieldShouldBeNotEmpty = "Поле '{0}' не должно быть пустым";
        public const string FieldShouldContainsExistsPath = "Поле '{0}' должно содержать существующий путь";
        public const string FieldNotContainsFileInDirectory = "Поле '{0}' не содержит файла, существующего в директории '{1}'";

        public const string FieldShouldBePassportCode = "Поле '{0}' должно содержать 4 цифры (серия) и 6 цифр (номер)";
        public const string FieldShouldBeDevisionCode = "Поле '{0}' должно содержать 3 цифры, дефис, 3 цифры";
        public const string FieldShouldBePersonalNumber = "Поле '{0}' должно содержать 1-2 буквы и 6 цифр";
        public const string FieldShouldBeMilitaryBilletCode = "Поле '{0}' должно содержать 2 буквы и 7 цифр";
        public const string FieldShouldBeSecretAccessNumber = "Поле '{0}' должно содержать 1-4 цифры";
        public const string FieldShouldBeDriverLicenseCode = "Поле '{0}' должно содержать 2-4 буквенно-цифровых символа и 2-6 цифр";

        public const string FieldShouldBeAdditionalRequirementsTable = "Поле '{0}' должно содержать графы (числа), разделенные запятой или тире";
        public const string FieldShouldBeDiseaseArticles = "Поле '{0}' должно содержать статьи, разделенные запятой";

        public const string FieldShouldBePhysiologicalCharacteristic = "Поле '{0}' должно содержать 1-3 цифры";

        public const string FieldShouldBeCorrectFormatWithExample = "Поле '{0}' должно иметь правильный формат, пример: {1}";
    }
}
