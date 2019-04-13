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
    public class ProficiencyCard : UIModel, IDataErrorInfo
    {
        public const string ProficiencyCategoryFieldName = "Категория профпригодности";
        public const string OfficialStatusFieldName = "Должность профпригодности";
        public const string NervouslyPsychologicalStabilityFieldName = "Нервно-психическая устойчивость (НПУ)";
        public const string GeneralPsychologicalStabilityFieldName = "Критерий ОПС";

        public static IEnumerable<string> ProficiencyCategoryEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(ProficiencyCategory)).Cast<ProficiencyCategory>()
                    .Select(pc => pc.ToProficiencyCategoryString());
            }
        }

        public static IEnumerable<string> OfficialStatusEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(OfficialStatus)).Cast<OfficialStatus>()
                    .Select(os => os.ToOfficialStatusString());
            }
        }

        public static IEnumerable<string> NervouslyPsychologicalStabilityEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(NervouslyPsychologicalStatus)).Cast<NervouslyPsychologicalStatus>()
                    .Select(nps => nps.ToNervouslyPsychologicalStatusString());
            }
        }

        public static IEnumerable<string> GeneralPsychologicalStabilityEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(GeneralPsychologicalStatus)).Cast<GeneralPsychologicalStatus>()
                    .Select(gps => gps.ToGeneralPsychologicalStatusString());
            }
        }

        private string _proficiencyCategory;
        public string ProficiencyCategory
        {
            get { return _proficiencyCategory; }
            set
            {
                if (_proficiencyCategory == value) return;
                _proficiencyCategory = value;
                OnPropertyChanged();
            }
        }

        private string _officialStatus;
        public string OfficialStatus
        {
            get { return _officialStatus; }
            set
            {
                if (_officialStatus == value) return;
                _officialStatus = value;
                OnPropertyChanged();
            }
        }

        private string _nervouslyPsychologicalStability;
        public string NervouslyPsychologicalStability
        {
            get { return _nervouslyPsychologicalStability; }
            set
            {
                if (_nervouslyPsychologicalStability == value) return;
                _nervouslyPsychologicalStability = value;
                OnPropertyChanged();
            }
        }

        private string _generalPsychologicalStability;
        public string GeneralPsychologicalStability
        {
            get { return _generalPsychologicalStability; }
            set
            {
                if (_generalPsychologicalStability == value) return;
                _generalPsychologicalStability = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case (nameof(ProficiencyCategory)):
                        {
                            if (string.IsNullOrWhiteSpace(ProficiencyCategory))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    ProficiencyCategoryFieldName);
                            }

                            break;
                        }
                    case (nameof(OfficialStatus)):
                        {
                            if (string.IsNullOrWhiteSpace(OfficialStatus))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    OfficialStatusFieldName);
                            }

                            break;
                        }
                    case (nameof(NervouslyPsychologicalStability)):
                        {
                            if (string.IsNullOrWhiteSpace(NervouslyPsychologicalStability))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    NervouslyPsychologicalStabilityFieldName);
                            }

                            break;
                        }
                    case (nameof(GeneralPsychologicalStability)):
                        {
                            if (string.IsNullOrWhiteSpace(GeneralPsychologicalStability))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    GeneralPsychologicalStabilityFieldName);
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
                    this[nameof(ProficiencyCategory)],
                    this[nameof(OfficialStatus)],
                    this[nameof(NervouslyPsychologicalStability)],
                    this[nameof(GeneralPsychologicalStability)],
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
