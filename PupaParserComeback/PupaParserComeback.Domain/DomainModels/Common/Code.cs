using System;

namespace PupaParserComeback.Domain.DomainModels.Common
{
    public class Code : IEquatable<Code>
    {
        private const int CodeWordsCount = 2;

        public string Serie { get; private set; }
        public string Number { get; private set; }

        public string Value
        {
            get { return $"{Serie} {Number}"; }
        }

        public Code(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            var words = code.Split(' ');

            if (words.Length == CodeWordsCount)
            {
                var serie = words[0];
                var number = words[1];

                Init(serie, number);
            }
            else
            {
                throw new ArgumentException(nameof(code));
            }
        }

        public Code(string serie, string number)
        {
            Init(serie, number);
        }

        private void Init(string serie, string number)
        {
            ChangeSerie(serie);
            ChangeNumber(number);
        }

        public void ChangeSerie(string serie)
        {
            if (string.IsNullOrWhiteSpace(serie))
            {
                throw new ArgumentNullException(nameof(serie));
            }

            Serie = serie;
        }

        public void ChangeNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException(nameof(number));
            }

            Number = number;
        }
        
        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as BirthInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Serie.GetHashCode() ^ Number.GetHashCode();
        }

        public bool Equals(Code other)
        {
            if (other == null) return false;

            return Serie == other.Serie &&
                   Number == other.Number;
        }

        public static bool operator ==(Code left, Code right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(Code left, Code right)
        {
            return !(left == right);
        }

        #endregion
    }
}
