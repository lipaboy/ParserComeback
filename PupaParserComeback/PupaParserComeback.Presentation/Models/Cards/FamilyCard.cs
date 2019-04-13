using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using PupaParserComeback.Presentation.Commands;
using PupaParserComeback.Domain.DomainModels.Civil;

namespace PupaParserComeback.Presentation.Models.Cards
{
    public class FamilyCard : UIModel, IDataErrorInfo
    {
        public const string ParentFamilyStatusFieldName = "Семейный статус";

        public static IEnumerable<string> ParentFamilyStatusEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(ParentFamilyStatus)).Cast<ParentFamilyStatus>()
                    .Select(pfs => pfs.ToParentFamilyStatusString());
            }
        }

        private string _familyStatus;
        public string ParentFamilyStatus
        {
            get { return _familyStatus; }
            set
            {
                if (_familyStatus == value) return;
                _familyStatus = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RelativeInfoCard> _relativeInfoUIModels =
            new ObservableCollection<RelativeInfoCard>();
        public ObservableCollection<RelativeInfoCard> RelativeInfoUIModels
        {
            get { return _relativeInfoUIModels; }
            set
            {
                if (_relativeInfoUIModels == value) return;
                _relativeInfoUIModels = value;
                OnPropertyChanged();
            }
        }

        private RelativeInfoCard _selectedRelativeInfoUIModel;
        public RelativeInfoCard SelectedRelativeInfoUIModel
        {
            get { return _selectedRelativeInfoUIModel; }
            set
            {
                if (_selectedRelativeInfoUIModel == value) return;
                _selectedRelativeInfoUIModel = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(ParentFamilyStatus) &&
                    string.IsNullOrWhiteSpace(ParentFamilyStatus))
                {
                    return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    ParentFamilyStatusFieldName);
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
                    this[nameof(ParentFamilyStatus)],
                };

                var relativeErrors = RelativeInfoUIModels.Select(r => r.Error).ToList();

                errors.AddRange(relativeErrors);
                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }

        private ICommand _addRelativeCommand;
        public ICommand AddRelativeCommand
        {
            get
            {
                return _addRelativeCommand ?? (_addRelativeCommand = new ActionCommand(vm =>
                {
                    var relative = new RelativeInfoCard();

                    RelativeInfoUIModels.Add(relative);
                    SelectedRelativeInfoUIModel = relative;
                },
                canExecute: vm => RelativeInfoUIModels.Count < FamilyInfo.MaxRelativesCount));
            }
        }

        private ICommand _removeRelativeCommand;
        public ICommand RemoveRelativeCommand
        {
            get
            {
                return _removeRelativeCommand ?? (_removeRelativeCommand = new ActionCommand(vm =>
                {
                    RelativeInfoUIModels.Remove(SelectedRelativeInfoUIModel);
                },
                canExecute: vm => RelativeInfoUIModels.Count > FamilyInfo.MinRelativesCount));
            }
        }
    }
}
