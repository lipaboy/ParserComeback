using System;

namespace PupaParserComeback.Domain.DomainModels.Common
{
    public class FullName : IEquatable<FullName>
    {
        private const int FullNameWordsCount = 3;
        private const int SurnameAndNameWordsCount = 2;
        private const int OnlySurnameWordsCount = 1;

        public string Surname { get; private set; }
        public string Name { get; private set; }
        public string Patronymic { get; private set; }

        public string Value
        {
            get { return $"{Surname} {Name} {Patronymic}"; }
        }

        public FullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            var words = fullName.Split(' ');

            if (words.Length >= FullNameWordsCount)
            {
                Init(surname: words[0], 
                     name: words[1],
                     patronymic: GetComplexPatronymic(words));
            }
            else if (words.Length == SurnameAndNameWordsCount)
            {
                Init(surname: words[0],
                     name: words[1]);
            }
            else if (words.Length == OnlySurnameWordsCount)
            {
                Init(surname: words[0]);
            }
            else
            {
                throw new ArgumentException(nameof(fullName));
            }
        }

        private string GetComplexPatronymic(string[] words)
        {
            var patronymic = string.Empty;

            for (int i = 2; i < words.Length; i++)
            {
                patronymic += words[i];
            }

            return patronymic;
        }

        public FullName(string surname, string name = "", string patronymic = "")
        {
            Init(surname, name, patronymic);
        }

        private void Init(string surname, string name = "", string patronymic = "")
        {
            ChangeSurname(surname);
            ChangeName(name);
            ChangePatronymic(patronymic);
        }

        public void ChangeSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentNullException(nameof(surname));
            }

            Surname = surname;
        }

        public void ChangeName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        public void ChangePatronymic(string patronymic)
        {
            Patronymic = patronymic;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as FullName;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Surname.GetHashCode() ^ Name.GetHashCode() ^ Patronymic.GetHashCode();
        }

        public bool Equals(FullName other)
        {
            if (other == null) return false;

            return Surname == other.Surname &&
                   Name == other.Name &&
                   Patronymic == other.Patronymic;
        }

        public static bool operator ==(FullName left, FullName right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(FullName left, FullName right)
        {
            return !(left == right);
        }

        #endregion
    }
}
