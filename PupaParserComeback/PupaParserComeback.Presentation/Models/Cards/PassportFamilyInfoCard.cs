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
    public class PassportFamilyInfoCard : UIModel, IDataErrorInfo
    {
        public const string FamilyStatusFieldName = "Семейное положение";
        public const string IsHaveBabyFieldName = "Наличие ребёнка";

        public static IEnumerable<string> FamilyStatusEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(FamilyStatus)).Cast<FamilyStatus>()
                    .Select(fs => fs.ToFamilyStatusString());
            }
        }

        private string _familyStatus;
        public string FamilyStatus
        {
            get { return _familyStatus; }
            set
            {
                if (_familyStatus == value) return;
                _familyStatus = value;
                OnPropertyChanged();
            }
        }

        private bool _isHaveBaby;
        public bool IsHaveBaby
        {
            get { return _isHaveBaby; }
            set
            {
                if (_isHaveBaby == value) return;
                _isHaveBaby = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(FamilyStatus) &&
                    string.IsNullOrWhiteSpace(FamilyStatus))
                {
                    return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    FamilyStatusFieldName);
                }

                return string.Empty;
            }
        }

        public string Error
        {
            get
            {
                return this[nameof(FamilyStatus)];
            }
        }
    }
}
