using PupaParserComeback.Domain.Constants;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class ServiceCard : UIModel, IDataErrorInfo
    {
        public const string SqliteIdFieldName = "Sqlite ID";
        public const string FormIdFieldName = "Form ID";
        public const string RegionalCollectionPointFieldName = "Военкомат";
        public const string ConscriptionDateFieldName = "Дата призыва";

        public static IEnumerable<string> RegionalCollectionPoints
        {
            get { return RcpConstants.RegionalCollectionPoints; }
        }

        private string _sqliteId;
        public string SqliteId
        {
            get { return _sqliteId; }
            set
            {
                if (_sqliteId == value) return;
                _sqliteId = value;
                OnPropertyChanged();
            }
        }

        private string _firebirdId;
        public string FirebirdId
        {
            get { return _firebirdId; }
            set
            {
                if (_firebirdId == value) return;
                _firebirdId = value;
                OnPropertyChanged();
            }
        }

        private string _regionalCollectionPoint;
        public string RegionalCollectionPoint
        {
            get { return _regionalCollectionPoint; }
            set
            {
                if (_regionalCollectionPoint == value) return;
                _regionalCollectionPoint = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _conscriptionDate = DateTime.Now;
        public DateTime? ConscriptionDate
        {
            get { return _conscriptionDate; }
            set
            {
                if (_conscriptionDate == value) return;
                _conscriptionDate = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(RegionalCollectionPoint):
                        {
                            if (string.IsNullOrWhiteSpace(RegionalCollectionPoint))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    RegionalCollectionPointFieldName);
                            }

                            break;
                        }
                    case nameof(ConscriptionDate):
                        {
                            if (!ConscriptionDate.HasValue)
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    ConscriptionDateFieldName);
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
                        this[nameof(RegionalCollectionPoint)],
                        this[nameof(ConscriptionDate)],
                    };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
