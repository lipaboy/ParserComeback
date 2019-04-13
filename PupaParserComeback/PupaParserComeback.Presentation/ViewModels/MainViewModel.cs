using PupaParserComeback.Presentation.Commands;
using PupaParserComeback.Domain.Constants;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PupaParserComeback.Domain.Interfaces;
using System.Collections.ObjectModel;
using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Presentation.EventArguments;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Domain.DomainModels;
using System.Collections;
using PupaParserComeback.Presentation.RecruitCommands;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Domain.Indexes;
using PupaParserComeback.Domain.ExtensionMethods;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;

namespace PupaParserComeback.Presentation.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const int MaxCharsCountInState = 250;

        #region Filter Names

        public const string ConscriptionDateFieldName = "Дата призыва";
        public const string RegionalCollectionPointsFieldName = "Военкоматы";
        public const string SurnameFieldName = "Фамилия";

        #endregion

        public const string UpdateRecuitUIModelsCommandName = "Обновить";
        public const string ClearFiltersCommandName = "Очистить фильтры";

        public const string ClearLogCommandName = "Очистка лога";
        public const string ClearLogSuccess = "Лог очищен";

        private readonly IParameterizedCommandAsync<ImportRecruitCommandParameters> _importRecruitCommand;
        private readonly IParameterizedCommandAsync<RemoveRecruitCommandParameters> _removeRecruitParameterizedCommand;
        private readonly IParameterizedCommandAsync<TransmitRecruitCommandParameters> _transmitRecruitParameterizedCommand;
        private readonly IParameterizedCommandAsync<ExportRecruitCommandParameters> _exportRecruitParameterizedCommand;
        private readonly IParameterizedCommandAsync<ExportTableRecruitCommandParameters> _exportTableRecruitParameterizedCommand;
        private readonly IParameterizedCommandAsync<UpdateRecruitsCommandParameters> _updateRecruitsParameterizedCommand;

        private readonly Func<bool> _areYouSureCallback;

        public event EventHandler SettingsViewShowed;
        public void OnSettingsViewShowed()
        {
            SettingsViewShowed?.Invoke(this, new EventArgs());
        }

        public event EventHandler<RecruitOperationEventArgs> RecruitViewShowed;
        public void OnRecruitViewShowed(RecruitOperationEventArgs args)
        {
            RecruitViewShowed?.Invoke(this, args);
        }

        #region Filters

        private DateTime? _conscriptionDate;
        public DateTime? ConscriptionDate
        {
            get { return _conscriptionDate; }
            set
            {
                if (_conscriptionDate == value) return;
                _conscriptionDate = value;
                OnPropertyChanged();

                UpdateRecuitUIModelsCommand.Execute(null);
            }
        }
        
        public IEnumerable<string> RegionalCollectionPoints { get; }

        private string _selectedRegionalCollectionPoint;
        public string SelectedRegionalCollectionPoint
        {
            get { return _selectedRegionalCollectionPoint; }
            set
            {
                if (_selectedRegionalCollectionPoint == value) return;
                _selectedRegionalCollectionPoint = value;
                OnPropertyChanged();

                UpdateRecuitUIModelsCommand.Execute(null);
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
                
                UpdateRecuitUIModelsCommand.Execute(null);
            }
        }

        public IEnumerable<string> Storages { get; }

        private string _selectedStorage;
        public string SelectedStorage
        {
            get { return _selectedStorage; }
            set
            {
                if (_selectedStorage == value) return;
                _selectedStorage = value;
                OnPropertyChanged();

                UpdateRecuitUIModelsCommand.Execute(null);
            }
        }

        #endregion

        private ObservableCollection<RecruitShortUIModel> _recruitShortUIModels;
        public ObservableCollection<RecruitShortUIModel> RecruitShortUIModels
        {
            get { return _recruitShortUIModels; }
            set
            {
                if (_recruitShortUIModels == value) return;
                _recruitShortUIModels = value;
                OnPropertyChanged();
            }
        }

        private RecruitShortUIModel _selectedRecruitShortUIModel;
        public RecruitShortUIModel SelectedRecruitShortUIModel
        {
            get { return _selectedRecruitShortUIModel; }
            set
            {
                if (_selectedRecruitShortUIModel == value) return;
                _selectedRecruitShortUIModel = value;
                OnPropertyChanged();
            }
        }

        #region Loaders

        private bool _isImportingRecruits;
        public bool IsImportingRecruits
        {
            get { return _isImportingRecruits; }
            set
            {
                if (_isImportingRecruits == value) return;
                _isImportingRecruits = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdatingRecruitShortUIModels;
        public bool IsUpdatingRecruitShortUIModels
        {
            get { return _isUpdatingRecruitShortUIModels; }
            set
            {
                if (_isUpdatingRecruitShortUIModels == value) return;
                _isUpdatingRecruitShortUIModels = value;
                OnPropertyChanged();
            }
        }

        private bool _isExportRecruit;
        public bool IsExportRecruit
        {
            get { return _isExportRecruit; }
            set
            {
                if (_isExportRecruit == value) return;
                _isExportRecruit = value;
                OnPropertyChanged();
            }
        }

        private bool _isExportTableRecruit;
        public bool IsExportTableRecruit
        {
            get { return _isExportTableRecruit; }
            set
            {
                if (_isExportTableRecruit == value) return;
                _isExportTableRecruit = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private string _state;
        public string State
        {
            get { return _state; }
            set
            {
                if (_state == value) return;
                _state = value;
                OnPropertyChanged();
            }
        }

        private string _log;
        public string Log
        {
            get { return _log; }
            set
            {
                if (_log == value) return;
                _log = value;

                OnPropertyChanged();
            }
        }

        public MainViewModel(IParameterizedCommandAsync<ImportRecruitCommandParameters> importRecruitCommand,
            IParameterizedCommandAsync<RemoveRecruitCommandParameters> removeRecruitParameterizedCommand,
            IParameterizedCommandAsync<TransmitRecruitCommandParameters> transmitRecruitParameterizedCommand,
            IParameterizedCommandAsync<ExportRecruitCommandParameters> exportRecruitParameterizedCommand,
            IParameterizedCommandAsync<ExportTableRecruitCommandParameters> exportTableRecruitParameterizedCommand,
            IParameterizedCommandAsync<UpdateRecruitsCommandParameters> updateRecruitsParameterizedCommand,
            Func<bool> areYouSureCallback = null)
        {
            if (importRecruitCommand == null)
            {
                throw new ArgumentNullException(nameof(importRecruitCommand));
            }

            if (removeRecruitParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(removeRecruitParameterizedCommand));
            }

            if (transmitRecruitParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(transmitRecruitParameterizedCommand));
            }

            if (exportRecruitParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(exportRecruitParameterizedCommand));
            }

            if (exportTableRecruitParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(exportTableRecruitParameterizedCommand));
            }

            if (updateRecruitsParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(updateRecruitsParameterizedCommand));
            }

            _importRecruitCommand = importRecruitCommand;
            _removeRecruitParameterizedCommand = removeRecruitParameterizedCommand;
            _transmitRecruitParameterizedCommand = transmitRecruitParameterizedCommand;
            _exportRecruitParameterizedCommand = exportRecruitParameterizedCommand;
            _exportTableRecruitParameterizedCommand = exportTableRecruitParameterizedCommand;
            _updateRecruitsParameterizedCommand = updateRecruitsParameterizedCommand;

            _areYouSureCallback = areYouSureCallback;

            RecruitShortUIModels = new ObservableCollection<RecruitShortUIModel>();

            RegionalCollectionPoints = RcpConstants.RegionalCollectionPoints;
            Storages = GenericEnumExtensions.GetStringValues<Storage>(withEmptyValue: true);

            ConscriptionDate = DateTime.Now;
        }

        private ICommand _importCommand;
        public ICommand ImportCommand
        {
            get
            {
                return _importCommand ?? (_importCommand = new AsyncCommand(async vm =>
                {
                    IsImportingRecruits = true;

                    try
                    {
                        var parameters = new ImportRecruitCommandParameters(ConscriptionDate,
                            SelectedRegionalCollectionPoint,
                            this);

                        await _importRecruitCommand.ExecuteAsync(parameters);
                    }
                    finally
                    {
                        IsImportingRecruits = false;
                    }


                    UpdateRecuitUIModelsCommand.Execute(null);
                },
                this));
            }
        }

        private ICommand _addRecruitCommand;
        public ICommand AddRecruitCommand
        {
            get
            {
                return _addRecruitCommand ?? (_addRecruitCommand = new ActionCommand(vm => 
                OnRecruitViewShowed(new RecruitOperationEventArgs(RecruitOperation.Add)),
                this,
                vm => !SaveToFormDatabaseCommand.IsExecuting));
            }
        }

        private ICommand _editRecruitCommand;
        public ICommand EditRecruitCommand
        {
            get
            {
                return _editRecruitCommand ?? (_editRecruitCommand = new ActionCommand(vm =>
                {
                    if (SelectedRecruitShortUIModel == null) return;

                    switch (SelectedRecruitShortUIModel.Storage)
                    {
                        case Storage.File:
                            OnRecruitViewShowed(new RecruitOperationEventArgs(RecruitOperation.Import, SelectedRecruitShortUIModel));
                            break;
                        case Storage.Sqlite:
                        case Storage.Firebird:
                        default:
                            OnRecruitViewShowed(new RecruitOperationEventArgs(RecruitOperation.Edit, SelectedRecruitShortUIModel));
                            break;
                    }
                },
                this,
                vm => SelectedRecruitShortUIModel != null &&
                      !SaveToFormDatabaseCommand.IsExecuting));
            }
        }

        private ICommand _removeRecruitCommand;
        public ICommand RemoveRecruitCommand
        {
            get
            {
                return _removeRecruitCommand ?? (_removeRecruitCommand = new AsyncCommand(async vm =>
                {
                    if (_areYouSureCallback != null)
                    {
                        if (!_areYouSureCallback()) return;
                    }

                    var selectedShortUIModel = SelectedRecruitShortUIModel;

                    RecruitShortUIModels.Remove(SelectedRecruitShortUIModel);

                    var parameters = new RemoveRecruitCommandParameters(selectedShortUIModel, this);
                    await _removeRecruitParameterizedCommand.ExecuteAsync(parameters);
                },
                this,
                vm => SelectedRecruitShortUIModel != null &&
                      !SaveToFormDatabaseCommand.IsExecuting));
            }
        }

        private AsyncCommand _saveToFormDatabaseCommand;
        public AsyncCommand SaveToFormDatabaseCommand
        {
            get
            {
                return _saveToFormDatabaseCommand ?? (_saveToFormDatabaseCommand = new AsyncCommand(async vm =>
                {
                    var list = vm as IList;
                    if (list == null) return;

                    var selectedRecruitShortUIModels = list.Cast<RecruitShortUIModel>();
                    if (selectedRecruitShortUIModels == null) return;
                    
                    bool isAllHaveSqliteIds = selectedRecruitShortUIModels.All(r => r.SqliteId.HasValue);
                    if (isAllHaveSqliteIds)
                    {
                        var ids = selectedRecruitShortUIModels.Select(r => r.SqliteId.Value);

                        var parameters = new TransmitRecruitCommandParameters(ids, this);
                        await _transmitRecruitParameterizedCommand.ExecuteAsync(parameters);

                        UpdateRecuitUIModelsCommand.Execute(null);
                    }
                },
                this,
                vm => 
                {
                    var list = vm as IList;
                    if (list == null) return false;

                    var selectedRecruitShortUIModels = list.Cast<RecruitShortUIModel>();
                    if (selectedRecruitShortUIModels == null) return false;

                    return selectedRecruitShortUIModels.Count() > 0 &&
                           selectedRecruitShortUIModels.All(r => r.Storage == Storage.Sqlite);
                }));
            }
        }

        private ICommand _exportCommand;
        public ICommand ExportCommand
        {
            get
            {
                return _exportCommand ?? (_exportCommand = new AsyncCommand(async vm =>
                {
                    var list = vm as IList;
                    if (list == null) return;

                    IsExportRecruit = true;

                    try
                    {
                        var selectedRecruitShortUIModels = list.Cast<RecruitShortUIModel>();
                        if (selectedRecruitShortUIModels == null) return;

                        var ids = selectedRecruitShortUIModels.Select(r => r.SqliteId.Value).ToList();

                        var parameters = new ExportRecruitCommandParameters(ids, this);
                        await _exportRecruitParameterizedCommand.ExecuteAsync(parameters);
                    }
                    finally
                    {
                        IsExportRecruit = false;
                    }
                },
                this,
                vm =>
                {
                    var list = vm as IList;
                    if (list == null) return false;

                    var selectedRecruitShortUIModels = list.Cast<RecruitShortUIModel>();

                    return selectedRecruitShortUIModels.All(r => r.SqliteId.HasValue);
                }));
            }
        }

        private ICommand _exportTableCommand;
        public ICommand ExportTableCommand
        {
            get
            {
                return _exportTableCommand ?? (_exportTableCommand = new AsyncCommand(async vm =>
                {
                    IsExportTableRecruit = true;

                    try
                    {
                        var recruitShortUIModels = RecruitShortUIModels.ToList();

                        var conscriptionDate = ConscriptionDate.HasValue
                            ? ConscriptionDate.Value.ToString("D")
                            : string.Empty;

                        var regionalCollectionPoint = SelectedRegionalCollectionPoint;

                        var parameters = new ExportTableRecruitCommandParameters(recruitShortUIModels,
                            conscriptionDate,
                            regionalCollectionPoint,
                            this);

                        await _exportTableRecruitParameterizedCommand.ExecuteAsync(parameters);
                    }
                    finally
                    {
                        IsExportTableRecruit = false;
                    }
                }));
            }
        }

        private ICommand _clearLogCommand;
        public ICommand ClearLogCommand
        {
            get
            {
                return _clearLogCommand ?? (_clearLogCommand = new ActionCommand(vm =>
                {
                    Log = string.Empty;
                    State = string.Empty;
                },
                this,
                vm => !string.IsNullOrWhiteSpace(Log)));
            }
        }

        private ICommand _clearFiltersCommand;
        public ICommand ClearFiltersCommand
        {
            get
            {
                return _clearFiltersCommand ?? (_clearFiltersCommand = new ActionCommand(vm =>
                {
                    SelectedRegionalCollectionPoint = string.Empty;
                    ConscriptionDate = null;
                    Surname = null;
                    SelectedStorage = null;

                    UpdateRecuitUIModelsCommand.Execute(null);
                },
                this,
                vm => !string.IsNullOrWhiteSpace(SelectedRegionalCollectionPoint) ||
                      ConscriptionDate != null ||
                      !string.IsNullOrWhiteSpace(Surname) ||
                      !string.IsNullOrWhiteSpace(SelectedStorage)));
            }
        }

        private ICommand _showSettingsViewCommand;
        public ICommand ShowSettingsViewCommand
        {
            get
            {
                return _showSettingsViewCommand ?? (_showSettingsViewCommand = new ActionCommand(vm =>
                {
                    OnSettingsViewShowed();
                },
                this));
            }
        }

        public void RecruitSaved(object sender, RecruitOperationEventArgs e)
        {
            UpdateRecuitUIModelsCommand.Execute(null);
        }

        private ICommand _updateRecuitUIModelsCommand;
        public ICommand UpdateRecuitUIModelsCommand
        {
            get
            {
                return _updateRecuitUIModelsCommand ?? (_updateRecuitUIModelsCommand = new AsyncCommand(async vm =>
                {
                    IsUpdatingRecruitShortUIModels = true;
                    
                    try
                    {
                        var parameters = new UpdateRecruitsCommandParameters(ConscriptionDate,
                            SelectedRegionalCollectionPoint,
                            Surname,
                            SelectedStorage,
                            RecruitShortUIModels,
                            this);

                        await _updateRecruitsParameterizedCommand.ExecuteAsync(parameters);
                    }
                    finally
                    {
                        IsUpdatingRecruitShortUIModels = false;
                    }

                },
                this));
            }
        }
    }
}
