using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PupaParserComeback.Presentation.Commands;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class PassportAccommodationCard : UIModel, IDataErrorInfo
    {
        public const string LocalityFieldName = "Населённый пункт";
        public const string RegisterLocationFieldName = "Место регистрации";
        public const string ActuallyLocationFieldName = "Место проживания";

        private string _locality;
        public string Locality
        {
            get { return _locality; }
            set
            {
                if (_locality == value) return;
                _locality = value;
                OnPropertyChanged();
            }
        }

        private string _registerLocation;
        public string RegisterLocation
        {
            get { return _registerLocation; }
            set
            {
                if (_registerLocation == value) return;
                _registerLocation = value;
                OnPropertyChanged();
            }
        }

        private string _actuallyLocation;
        public string ActuallyLocation
        {
            get { return _actuallyLocation; }
            set
            {
                if (_actuallyLocation == value) return;
                _actuallyLocation = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Locality):
                        {
                            if (string.IsNullOrWhiteSpace(Locality))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    LocalityFieldName);
                            }

                            break;
                        }
                    case nameof(RegisterLocation):
                        {
                            if (string.IsNullOrWhiteSpace(RegisterLocation))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    RegisterLocationFieldName);
                            }

                            break;
                        }

                    case nameof(ActuallyLocation):
                        {
                            if (string.IsNullOrWhiteSpace(ActuallyLocation))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    ActuallyLocationFieldName);
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
                    this[nameof(Locality)],
                    this[nameof(RegisterLocation)],
                    this[nameof(ActuallyLocation)],
                };

                errors.RemoveAll(err => err == string.Empty);

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }


        private ICommand _upLocationCommand;
        public ICommand UpLocationCommand
        {
            get
            {
                return _upLocationCommand ?? (_upLocationCommand = new ActionCommand(vm =>
                {
                    RegisterLocation = ActuallyLocation;
                }));
            }
        }

        private ICommand _downLocationCommand;
        public ICommand DownLocationCommand
        {
            get
            {
                return _downLocationCommand ?? (_downLocationCommand = new ActionCommand(vm =>
                {
                    ActuallyLocation = RegisterLocation;
                }));
            }
        }
    }
}
