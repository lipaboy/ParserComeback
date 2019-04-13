using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PupaParserComeback.Presentation.Commands;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class ContactsCard : UIModel, IDataErrorInfo
    {
        public const string HomePhoneFieldName = "Домашний телефон";
        public const string MobilePhoneFieldName = "Мобильный телефон";

        private string _homePhone;
        public string HomePhone
        {
            get { return _homePhone; }
            set
            {
                if (_homePhone == value) return;
                _homePhone = value;
                OnPropertyChanged();
            }
        }

        private string _mobilePhone;
        public string MobilePhone
        {
            get { return _mobilePhone; }
            set
            {
                if (_mobilePhone == value) return;
                _mobilePhone = value;
                OnPropertyChanged();
            }
        }
        
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(HomePhone):
                        {
                            if (string.IsNullOrWhiteSpace(HomePhone))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    HomePhoneFieldName);
                            }

                            break;
                        }
                    case nameof(MobilePhone):
                        {
                            if (string.IsNullOrWhiteSpace(MobilePhone))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    MobilePhoneFieldName);
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
                    this[nameof(HomePhone)],
                    this[nameof(MobilePhone)]
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }

        private ICommand _upPhoneNumberCommand;
        public ICommand UpPhoneNumberCommand
        {
            get
            {
                return _upPhoneNumberCommand ?? (_upPhoneNumberCommand = new ActionCommand(vm =>
                {
                    HomePhone = MobilePhone;
                }));
            }
        }

        private ICommand _downPhoneNumberCommand;
        public ICommand DownPhoneNumberCommand
        {
            get
            {
                return _downPhoneNumberCommand ?? (_downPhoneNumberCommand = new ActionCommand(vm =>
                {
                    MobilePhone = HomePhone;
                }));
            }
        }
    }
}
