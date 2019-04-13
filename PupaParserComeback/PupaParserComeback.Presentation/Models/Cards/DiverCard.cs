using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class DriverCard : UIModel, IDataErrorInfo
    {
        public const string DriverLicenseCodeFieldName = "Серия и номер водительских прав";
        public const string DriverLicenseIssueDateFieldName = "Дата выдачи прав";

        private Regex _driverLicenseCodeRegex = new Regex(RegexConstants.DriverLicenseCodePattern);

        private bool _isDriver;
        public bool IsDriver
        {
            get { return _isDriver; }
            set
            {
                if (_isDriver == value) return;
                _isDriver = value;

                if (_isDriver)
                {
                    DriverLicenseIssueDate = DateTime.Now;
                }
                else
                {
                    DriverLicenseCode = string.Empty;
                    DriverLicenseIssueDate = null;
                }

                OnPropertyChanged();
            }
        }

        private string _driverLicenseCode;
        public string DriverLicenseCode
        {
            get { return _driverLicenseCode; }
            set
            {
                if (_driverLicenseCode == value) return;
                _driverLicenseCode = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _driverLicenseIssueDate;
        public DateTime? DriverLicenseIssueDate
        {
            get { return _driverLicenseIssueDate; }
            set
            {
                if (_driverLicenseIssueDate == value) return;
                _driverLicenseIssueDate = value;
                OnPropertyChanged();
            }
        }
        
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(DriverLicenseCode):
                        {
                            if (string.IsNullOrWhiteSpace(DriverLicenseCode))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    DriverLicenseCodeFieldName);
                            }

                            if (!_driverLicenseCodeRegex.IsMatch(DriverLicenseCode))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeDriverLicenseCode,
                                    DriverLicenseCodeFieldName);
                            }

                            break;
                        }
                    case nameof(DriverLicenseIssueDate):
                        {
                            if (!DriverLicenseIssueDate.HasValue)
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    DriverLicenseIssueDateFieldName);
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
                if (IsDriver)
                {
                    var errors = new List<string>()
                    {
                        this[nameof(DriverLicenseCode)],
                        this[nameof(DriverLicenseIssueDate)],
                    };

                    errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                    return string.Join(SeparatorConstants.CommaSeparator, errors);
                }

                return string.Empty;
            }
        }
    }
}
