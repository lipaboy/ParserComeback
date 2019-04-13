using PupaParserComeback.Domain.DomainModels.Military;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PupaParserComeback.Presentation.Commands;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class DistributionCard : UIModel, IDataErrorInfo
    {
        public const string SpecialityFieldName = "Военно-учётная специальность (ВУС)";
        public const string TeamModeFieldName = "Команда";

        private string _speciality;
        public string Speciality
        {
            get { return _speciality; }
            set
            {
                if (_speciality == value) return;
                _speciality = value;
                OnPropertyChanged();
            }
        }

        private string _teamMode;
        public string TeamMode
        {
            get { return _teamMode; }
            set
            {
                if (_teamMode == value) return;
                _teamMode = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Speciality):
                        {
                            if (string.IsNullOrWhiteSpace(Speciality))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    SpecialityFieldName);
                            }

                            break;
                        }
                    case nameof(TeamMode):
                        {
                            if (string.IsNullOrWhiteSpace(TeamMode))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    TeamModeFieldName);
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
                    this[nameof(Speciality)],
                    this[nameof(TeamMode)]
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }

        private ICommand _resetSpecialityCommand;
        public ICommand ResetSpecialityCommand
        {
            get
            {
                return _resetSpecialityCommand ?? (_resetSpecialityCommand = new ActionCommand(vm =>
                {
                    Speciality = MilitaryInfo.NoSpeciality;
                }));
            }
        }

        private ICommand _resetTeamModeCommand;
        public ICommand ResetTeamModeCommand
        {
            get
            {
                return _resetTeamModeCommand ?? (_resetTeamModeCommand = new ActionCommand(vm =>
                {
                    TeamMode = MilitaryInfo.NoTeamMode;
                }));
            }
        }
    }
}
