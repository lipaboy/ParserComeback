using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class PassportInfoCard : UIModel, IDataErrorInfo
    {
        public const string CodeFieldName = "Серия и номер паспорта";
        public const string IssueByFieldName = "Кем выдан паспорт";
        public const string IssueDateFieldName = "Дата выдачи паспорта";
        public const string DevisionCodeFieldName = "Код УФМС";
        
        private Regex _passportCodeRegex = new Regex(RegexConstants.PassportCodePattern);
        private Regex _devisionCodeRegex = new Regex(RegexConstants.DevisionCodePattern);

        private const string DevisionCodeExample = "540-007";

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value) return;
                _code = value;
                OnPropertyChanged();
            }
        }

        private string _issueBy;
        public string IssueBy
        {
            get { return _issueBy; }
            set
            {
                if (_issueBy == value) return;
                _issueBy = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _issueDate = DateTime.Now;
        public DateTime? IssueDate
        {
            get { return _issueDate; }
            set
            {
                if (_issueDate == value) return;
                _issueDate = value;
                OnPropertyChanged();
            }
        }

        private string _devisionCode;
        public string DevisionCode
        {
            get { return _devisionCode; }
            set
            {
                if (_devisionCode == value) return;
                _devisionCode = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Code):
                        {
                            if (string.IsNullOrWhiteSpace(Code))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty, 
                                    CodeFieldName);
                            }
                            
                            if (!_passportCodeRegex.IsMatch(Code))
                            {
                                return string.Format(ErrorConstants.FieldShouldBePassportCode,
                                    CodeFieldName);
                            }

                            break;
                        }
                    case nameof(IssueBy):
                        {
                            if (string.IsNullOrWhiteSpace(IssueBy))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    IssueByFieldName);
                            }

                            break;
                        }
                    case nameof(IssueDate):
                        {
                            if (!IssueDate.HasValue)
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    IssueDateFieldName);
                            }

                            break;
                        }
                    case nameof(DevisionCode):
                        {
                            if (string.IsNullOrWhiteSpace(DevisionCode))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    DevisionCodeFieldName);
                            }

                            if (!_devisionCodeRegex.IsMatch(DevisionCode))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeDevisionCode,
                                    DevisionCodeFieldName);
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
                    this[nameof(Code)],
                    this[nameof(IssueBy)],
                    this[nameof(IssueDate)],
                    this[nameof(DevisionCode)]
                };

                errors.RemoveAll(err => err == string.Empty);

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
