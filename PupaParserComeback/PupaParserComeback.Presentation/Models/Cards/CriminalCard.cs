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
    public class CriminalCard : UIModel, IDataErrorInfo
    {
        public const string RegisterStatusFieldName = "На учёте";
        public const string CriminalStatusFieldName = "Судимость";

        public static IEnumerable<string> RegisterStatusEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(RegisterStatus)).Cast<RegisterStatus>()
                    .Select(rs => rs.ToRegisterStatusString());
            }
        }

        public static IEnumerable<string> CriminalStatusEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(CriminalStatus)).Cast<CriminalStatus>()
                    .Select(cs => cs.ToCriminalStatusString());
            }
        }

        private string _registerStatus = Domain.Enums.RegisterStatus.WasNot.ToRegisterStatusString();
        public string RegisterStatus
        {
            get { return _registerStatus; }
            set
            {
                if (_registerStatus == value) return;
                _registerStatus = value;
                OnPropertyChanged();
            }
        }

        private string _criminalStatus = Domain.Enums.CriminalStatus.HaveNot.ToCriminalStatusString();
        public string CriminalStatus
        {
            get { return _criminalStatus; }
            set
            {
                if (_criminalStatus == value) return;
                _criminalStatus = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(RegisterStatus):
                        {
                            if (string.IsNullOrWhiteSpace(RegisterStatus))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    RegisterStatusFieldName);
                            }

                            break;
                        }
                    case nameof(CriminalStatus):
                        {
                            if (string.IsNullOrWhiteSpace(CriminalStatus))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    CriminalStatusFieldName);
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
                    this[nameof(RegisterStatus)],
                    this[nameof(CriminalStatus)]
                };

                errors.RemoveAll(err => err == string.Empty);

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
