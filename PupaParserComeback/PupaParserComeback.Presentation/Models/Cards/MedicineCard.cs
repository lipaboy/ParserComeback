using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using PupaParserComeback.Presentation.Commands;
using PupaParserComeback.Domain.DomainModels.Medicine;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class MedicineCard : UIModel, IDataErrorInfo
    {
        public const string RankFieldName = "Категория годности";
        public const string AdditionalRequirementsTableFieldName = "Графы таблицы дополнительных требований";
        public const string DiseaseArticlesFieldName = "Статьи расписания болезней";
        public const string VisionFieldName = "Зрение";
        public const string BloodTypeFieldName = "Группа крови";

        private Regex _additionalRequirementsTableRegex = new Regex(RegexConstants.AdditionalRequirementsTablePattern);
        private Regex _diseaseArticlesRegex = new Regex(RegexConstants.DiseaseArticlesPattern);

        private const string VisionExample = "1,0/1,0 или 1.0/1.0";

        private Regex _visionRegex = new Regex(RegexConstants.VisionPattern);

        public static IEnumerable<string> MedicineRankEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(MedicineRank)).Cast<MedicineRank>()
                    .Select(mr => mr.ToMedicineRankString());
            }
        }

        public static IEnumerable<string> BloodTypeEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(BloodType)).Cast<BloodType>()
                    .Select(bt => bt.ToBloodTypeString());
            }
        }

        private string _rank;
        public string Rank
        {
            get { return _rank; }
            set
            {
                if (_rank == value) return;
                _rank = value;
                OnPropertyChanged();
            }
        }

        private string _additionalRequirementsTable;
        public string AdditionalRequirementsTable
        {
            get { return _additionalRequirementsTable; }
            set
            {
                if (_additionalRequirementsTable == value) return;
                _additionalRequirementsTable = value;
                OnPropertyChanged();
            }
        }

        private string _diseaseArticles;
        public string DiseaseArticles
        {
            get { return _diseaseArticles; }
            set
            {
                if (_diseaseArticles == value) return;
                _diseaseArticles = value;
                OnPropertyChanged();
            }
        }

        private string _vision;
        public string Vision
        {
            get { return _vision; }
            set
            {
                if (_vision == value) return;
                _vision = value;
                OnPropertyChanged();
            }
        }

        private string _bloodType;
        public string BloodType
        {
            get { return _bloodType; }
            set
            {
                if (_bloodType == value) return;
                _bloodType = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Rank):
                        {
                            if (string.IsNullOrWhiteSpace(Rank))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    RankFieldName);
                            }

                            break;
                        }
                    case nameof(AdditionalRequirementsTable):
                        {
                            if (string.IsNullOrWhiteSpace(AdditionalRequirementsTable))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    AdditionalRequirementsTableFieldName);
                            }

                            if (!_additionalRequirementsTableRegex.IsMatch(AdditionalRequirementsTable))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeAdditionalRequirementsTable,
                                    AdditionalRequirementsTableFieldName);
                            }

                            break;
                        }
                    case nameof(DiseaseArticles):
                        {
                            if (!string.IsNullOrWhiteSpace(DiseaseArticles) &&
                                !_diseaseArticlesRegex.IsMatch(DiseaseArticles))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeDiseaseArticles,
                                    DiseaseArticlesFieldName);
                            }

                            break;
                        }
                    case nameof(Vision):
                        {
                            if (string.IsNullOrWhiteSpace(Vision))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    VisionFieldName);
                            }

                            if (!_visionRegex.IsMatch(Vision))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeCorrectFormatWithExample,
                                    VisionFieldName, VisionExample);
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
                    this[nameof(Rank)],
                    this[nameof(AdditionalRequirementsTable)],
                    this[nameof(DiseaseArticles)],
                    this[nameof(Vision)],
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }

        private ICommand _resetAdditionalRequirementsCommand;
        public ICommand ResetAdditionalRequirementsCommand
        {
            get
            {
                return _resetAdditionalRequirementsCommand ?? (_resetAdditionalRequirementsCommand = new ActionCommand(vm =>
                {
                    AdditionalRequirementsTable = Health.DefaultAdditionalRequirementsTableGraphs;
                }));
            }
        }

        private ICommand _resetVisionCommand;
        public ICommand ResetVisionCommand
        {
            get
            {
                return _resetVisionCommand ?? (_resetVisionCommand = new ActionCommand(vm =>
                {
                    Vision = Health.DefaultVision;
                }));
            }
        }
    }
}
