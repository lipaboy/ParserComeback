using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class PassportPersonInfoCard : UIModel, IDataErrorInfo
    {
        public const string SurnameFieldName = "Фамилия";
        public const string NameFieldName = "Имя";
        public const string PatronymicFieldName = "Отчество";
        public const string BirthDateFieldName = "Дата рождения";
        public const string BirthPlaceFieldName = "Место рождения";
        public const string PhotoFieldName = "Фото";
        
        private string _personalPhotoDirectoryPath;
        
        public PassportPersonInfoCard(string personalPhotoDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }
            
            _personalPhotoDirectoryPath = personalPhotoDirectoryPath;
        }
        
        private string _photoName;
        public string PhotoName
        {
            get { return _photoName; }
            set
            {
                if (_photoName == value) return;
                _photoName = value;

                PhotoPath = _personalPhotoDirectoryPath + _photoName;

                OnPropertyChanged();
            }
        }

        private string _photoPath;
        public string PhotoPath
        {
            get { return _photoPath; }
            set
            {
                if (_photoPath == value) return;
                _photoPath = value;
                OnPropertyChanged();
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname == value) return;
                _surname = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _patronymic;
        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                if (_patronymic == value) return;
                _patronymic = value;
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

        private string _birthPlace;
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
        
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Surname):
                        {
                            if (string.IsNullOrWhiteSpace(Surname))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    SurnameFieldName);
                            }

                            break;
                        }
                    case nameof(Name):
                        {
                            if (string.IsNullOrWhiteSpace(Name))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    NameFieldName);
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
                    this[nameof(Surname)],
                    this[nameof(Name)],
                    this[nameof(BirthDate)],
                    this[nameof(BirthPlace)],
                };

                errors.RemoveAll(err => err == string.Empty);

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
