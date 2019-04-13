using System;

namespace PupaParserComeback.Export.Models
{
    public class RecruitExcelTableItem
    {
        public string Surname { get; }
        public string Name { get; }
        public string Patronymic { get; }
        public string BirthDate { get; set; }
        public string RegionalCollectionPoint { get; set; }
        public string FormId { get; }

        public RecruitExcelTableItem(string surname, 
            string name, 
            string patronymic,
            string birthDate,
            string regionalCollectionPoint,
            string formId)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentNullException(nameof(surname));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (patronymic == null)
            {
                throw new ArgumentNullException(nameof(patronymic));
            }

            if (string.IsNullOrWhiteSpace(birthDate))
            {
                throw new ArgumentNullException(nameof(birthDate));
            }

            if (string.IsNullOrWhiteSpace(regionalCollectionPoint))
            {
                throw new ArgumentNullException(nameof(regionalCollectionPoint));
            }

            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            BirthDate = birthDate;
            RegionalCollectionPoint = regionalCollectionPoint;
            FormId = formId;
        }
    }
}
