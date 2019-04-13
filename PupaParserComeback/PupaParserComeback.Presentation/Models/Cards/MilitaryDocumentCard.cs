using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class MilitaryDocumentCard : UIModel, IDataErrorInfo
    {
        public const string PersonalNumberFieldName = "Личный номер";
        public const string MilitaryBilletCodeFieldName = "Серия и номер военного билета";
        public const string IsHaveSecretAccessFieldName = "Имеет ли допуск";
        public const string AccessFormFieldName = "Форма допуска";
        public const string SecretAccessNumberFieldName = "Номер допуска";
        public const string SecretAccessIssueDateFieldName = "Дата выдачи допуска";

        private Regex _personalNumberRegex = new Regex(RegexConstants.PersonalNumberPattern);
        private Regex _militaryBilletCodeRegex = new Regex(RegexConstants.MilitaryBilletCodePattern);
        private Regex _secretAccessNumberRegex = new Regex(RegexConstants.SecretAccessNumberPattern);

        public static IEnumerable<string> AccessFormEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(AccessForm)).Cast<AccessForm>()
                    .Select(af => af.ToAccessFormString());
            }
        }

        private string _personalNumber;
        public string PersonalNumber
        {
            get { return _personalNumber; }
            set
            {
                if (_personalNumber == value) return;
                _personalNumber = value;
                OnPropertyChanged();
            }
        }

        private string _militaryBilletCode;
        public string MilitaryBilletCode
        {
            get { return _militaryBilletCode; }
            set
            {
                if (_militaryBilletCode == value) return;
                _militaryBilletCode = value;
                OnPropertyChanged();
            }
        }

        private bool _isHaveSecretAccess;
        public bool IsHaveSecretAccess
        {
            get { return _isHaveSecretAccess; }
            set
            {
                if (_isHaveSecretAccess == value) return;
                _isHaveSecretAccess = value;

                if (_isHaveSecretAccess)
                {
                    SecretAccessIssueDate = DateTime.Now;
                }
                else
                {
                    AccessForm = string.Empty;
                    SecretAccessNumber = string.Empty;
                    SecretAccessIssueDate = null;
                }

                OnPropertyChanged();
            }
        }

        private string _accessForm;
        public string AccessForm
        {
            get { return _accessForm; }
            set
            {
                if (_accessForm == value) return;
                _accessForm = value;
                OnPropertyChanged();
            }
        }

        private string _secretAccessNumber;
        public string SecretAccessNumber
        {
            get { return _secretAccessNumber; }
            set
            {
                if (_secretAccessNumber == value) return;
                _secretAccessNumber = value;
                OnPropertyChanged();
            }
        }
        
        private DateTime? _secretAccessIssueDate;
        public DateTime? SecretAccessIssueDate
        {
            get { return _secretAccessIssueDate; }
            set
            {
                if (_secretAccessIssueDate == value) return;
                _secretAccessIssueDate = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(PersonalNumber):
                        {
                            if (string.IsNullOrWhiteSpace(PersonalNumber))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    PersonalNumberFieldName);
                            }

                            if (!_personalNumberRegex.IsMatch(PersonalNumber))
                            {
                                return string.Format(ErrorConstants.FieldShouldBePersonalNumber,
                                    PersonalNumberFieldName);
                            }

                            break;
                        }
                    case nameof(MilitaryBilletCode):
                        {
                            if (string.IsNullOrWhiteSpace(MilitaryBilletCode))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    MilitaryBilletCodeFieldName);
                            }

                            if (!_militaryBilletCodeRegex.IsMatch(MilitaryBilletCode))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeMilitaryBilletCode,
                                    MilitaryBilletCodeFieldName);
                            }

                            break;
                        }
                    case nameof(AccessForm):
                        {
                            if (string.IsNullOrWhiteSpace(AccessForm))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    AccessFormFieldName);
                            }

                            break;
                        }
                    case nameof(SecretAccessNumber):
                        {
                            if (string.IsNullOrWhiteSpace(SecretAccessNumber))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    SecretAccessNumberFieldName);
                            }

                            if (!_secretAccessNumberRegex.IsMatch(SecretAccessNumber))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeSecretAccessNumber,
                                    SecretAccessNumberFieldName);
                            }

                            break;
                        }
                    case nameof(SecretAccessIssueDate):
                        {
                            if (!SecretAccessIssueDate.HasValue)
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    SecretAccessIssueDateFieldName);
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
                    this[nameof(PersonalNumber)],
                    this[nameof(MilitaryBilletCode)]
                };

                if (IsHaveSecretAccess)
                {
                    errors.Add(this[nameof(AccessForm)]);
                    errors.Add(this[nameof(SecretAccessNumber)]);
                    errors.Add(this[nameof(SecretAccessIssueDate)]);
                }

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
