using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Presentation.Abstract;
using System;

namespace PupaParserComeback.Presentation.Models
{
    public class RecruitShortUIModel : UIModel
    {
        public const string StorageFieldName = "Хранилище";
        public const string NumberFieldName = "№";
        public const string SurnameFieldName = "Фамилия";
        public const string NameFieldName = "Имя";
        public const string PatronymicFieldName = "Отчество";
        public const string PassportCodeFieldName = "Паспорт";
        public const string BirthDateFieldName = "Дата рождения";
        public const string RegionalCollectionPointFieldName = "Военкомат";
        public const string ConscriptionDateFieldName = "Дата призыва";

        public const string SqliteIdFieldName = "S ID";
        public const string FirebirdIdFieldName = "F ID";

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        private Storage _storage;
        public Storage Storage
        {
            get { return _storage; }
            set
            {
                if (_storage == value) return;
                _storage = value;
                OnPropertyChanged();
            }
        }

        public string Number { get; set; }

        public string Surname { get; }
        public string Name { get; }
        public string Patronymic { get; }
        public string PassportCode { get; }
        public string BirthDate { get; }
        public string RegionalCollectionPoint { get; }
        public string ConscriptionDate { get; }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath == value) return;
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private long? _sqliteId;
        public long? SqliteId
        {
            get { return _sqliteId; }
            set
            {
                if (_sqliteId == value) return;
                _sqliteId = value;
                OnPropertyChanged();
            }
        }

        private int? _firebirdId;
        public int? FirebirdId
        {
            get { return _firebirdId; }
            set
            {
                if (_firebirdId == value) return;
                _firebirdId = value;
                OnPropertyChanged();
            }
        }

        public RecruitShortUIModel(string surname,
            string name,
            string patronymic,
            string passportCode,
            DateTime? birthDate,
            string regionalCollectionPoint,
            DateTime? conscriptionDate,
            Storage storage,
            string filePath = null,
            long? sqliteId = null,
            int? firebirdId = null)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            PassportCode = passportCode;

            BirthDate = birthDate.HasValue
                ? birthDate.Value.ToString("D")
                : string.Empty;

            RegionalCollectionPoint = regionalCollectionPoint;
            ConscriptionDate = conscriptionDate.HasValue
                ? conscriptionDate.Value.ToString("D")
                : string.Empty;

            Storage = storage;
            FilePath = filePath;

            SqliteId = sqliteId;
            FirebirdId = firebirdId;
        }
    }
}
