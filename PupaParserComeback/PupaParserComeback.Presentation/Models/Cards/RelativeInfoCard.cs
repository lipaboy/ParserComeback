using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using PupaParserComeback.Presentation.Commands;
using PupaParserComeback.Domain.DomainModels.Common;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class RelativeInfoCard : UIModel, IDataErrorInfo
    {
        public const string FullNameFieldName = "ФИО";
        public const string BirthDateFieldName = "Дата рождения";
        public const string BirthPlaceFieldName = "Место рождения";
        public const string WorkPlaceFieldName = "Место работы";
        public const string RelativeStatusFieldName = "Статус";
        
        public static IEnumerable<string> RelativeStatusEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(RelativeStatus)).Cast<RelativeStatus>()
                    .Select(pfs => pfs.ToRelativeStatusString());
            }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName == value) return;
                _fullName = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _birthDate = DateTime.Now;
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate == value) return;
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        private string _birthPlace = BirthInfo.UnknownPlace;
        public string BirthPlace
        {
            get { return _birthPlace; }
            set
            {
                if (_birthPlace == value) return;
                _birthPlace = value;
                OnPropertyChanged();
            }
        }

        private string _workPlace = RelativeInfo.NotWorking;
        public string WorkPlace
        {
            get { return _workPlace; }
            set
            {
                if (_workPlace == value) return;
                _workPlace = value;
                OnPropertyChanged();
            }
        }

        private string _relativeStatus;
        public string RelativeStatus
        {
            get { return _relativeStatus; }
            set
            {
                if (_relativeStatus == value) return;
                _relativeStatus = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FullName):
                        {
                            if (string.IsNullOrWhiteSpace(FullName))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    FullNameFieldName);
                            }

                            break;
                        }
                    case nameof(BirthDate):
                        {
                            if (!BirthDate.HasValue)
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    BirthDateFieldName);
                            }

                            break;
                        }
                    case nameof(BirthPlace):
                        {
                            if (string.IsNullOrWhiteSpace(BirthPlace))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    BirthPlaceFieldName);
                            }

                            break;
                        }
                    case nameof(WorkPlace):
                        {
                            if (string.IsNullOrWhiteSpace(WorkPlace))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    WorkPlaceFieldName);
                            }

                            break;
                        }
                    case nameof(RelativeStatus):
                        {
                            if (string.IsNullOrWhiteSpace(RelativeStatus))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    RelativeStatusFieldName);
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
                    this[nameof(FullName)],
                    this[nameof(BirthDate)],
                    this[nameof(BirthPlace)],
                    this[nameof(WorkPlace)],
                    this[nameof(RelativeStatus)],
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }

        private ICommand _resetBirthPlaceCommand;
        public ICommand ResetBirthPlaceCommand
        {
            get
            {
                return _resetBirthPlaceCommand ?? (_resetBirthPlaceCommand = new ActionCommand(vm =>
                {
                    BirthPlace = BirthInfo.UnknownPlace;
                }));
            }
        }

        private ICommand _resetWorkPlaceCommand;
        public ICommand ResetWorkPlaceCommand
        {
            get
            {
                return _resetWorkPlaceCommand ?? (_resetWorkPlaceCommand = new ActionCommand(vm =>
                {
                    WorkPlace = RelativeInfo.NotWorking;
                }));
            }
        }
    }
}
