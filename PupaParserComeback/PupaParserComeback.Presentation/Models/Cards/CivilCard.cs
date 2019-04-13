using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class CivilCard : UIModel, IDataErrorInfo
    {
        public const string EducationFieldName = "Образование";
        public const string ProfessionFieldName = "Профессия";
        public const string OccupationFieldName = "Род занятий до призыва";

        public static IEnumerable<string> EducationStatusEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(EducationStatus)).Cast<EducationStatus>()
                    .Select(es => es.ToEducationStatusString());
            }
        }

        public static IEnumerable<string> OccupationStatusEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(OccupationStatus)).Cast<OccupationStatus>()
                    .Select(os => os.ToOccupationStatusString());
            }
        }

        private string _education;
        public string Education
        {
            get { return _education; }
            set
            {
                if (_education == value) return;
                _education = value;
                OnPropertyChanged();
            }
        }

        private string _profession;
        public string Profession
        {
            get { return _profession; }
            set
            {
                if (_profession == value) return;
                _profession = value;
                OnPropertyChanged();
            }
        }

        private string _occupation;
        public string Occupation
        {
            get { return _occupation; }
            set
            {
                if (_occupation == value) return;
                _occupation = value;
                OnPropertyChanged();
            }
        }


        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Education):
                        {
                            if (string.IsNullOrWhiteSpace(Education))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    EducationFieldName);
                            }

                            break;
                        }
                    case nameof(Occupation):
                        {
                            if (string.IsNullOrWhiteSpace(Occupation))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    OccupationFieldName);
                            }

                            break;
                        }
                }

                return string.Empty;
            }
        }

        public string Error
        {
            get
            {
                var errors = new List<string>()
                {
                    this[nameof(Education)],
                    this[nameof(Occupation)]
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
