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
    public class SportCard : UIModel, IDataErrorInfo
    {
        public const string RankFieldName = "Спортивный разряд";
        public const string KindFieldName = "Вид спорта";

        public static IEnumerable<string> SportRankEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(SportRank)).Cast<SportRank>()
                    .Select(sr => sr.ToSportRankString());
            }
        }

        private string _rank = SportRank.HaveNot.ToSportRankString();
        public string Rank
        {
            get { return _rank; }
            set
            {
                if (_rank == value) return;
                _rank = value;
                OnPropertyChanged();
            }
        }

        private string _kind;
        public string Kind
        {
            get { return _kind; }
            set
            {
                if (_kind == value) return;
                _kind = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Rank):
                        {
                            if (string.IsNullOrWhiteSpace(Rank))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    RankFieldName);
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
                    this[nameof(Rank)]
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
