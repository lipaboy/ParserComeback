using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.UI.Views;
using PupaParserComeback.Views;
using System;
using System.Windows;
using System.Data.Entity;
using PupaParserComeback.Presentation.ViewModels;
using PupaParserComeback.Presentation.EventArguments;
using PupaParserComeback.UI.Configurations;
using FirebirdSql.Data.FirebirdClient;
using PupaParserComeback.UI.DialogViews;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Export;
using PupaParserComeback.Import;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.ExtensionMethods;
using log4net;
using log4net.Config;
using Sqlite = PupaParserComeback.Data.SQLite;
using Firebird = PupaParserComeback.Data.Firebird;
using PupaParserComeback.UI.ExtensionMethods;
using PupaParserComeback.Presentation.Models.CardGroups;
using PupaParserComeback.Presentation.RecruitFactories;
using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;
using PupaParserComeback.Presentation.RecruitCommands;

namespace PupaParserComeback
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool _isOwner;
        private Mutex _appMutex;

        private static readonly ILog _log = LogManager.GetLogger(typeof(App));

        private readonly static UserSettings UserSettings = new UserSettings();
        private readonly string _personalPhotoDirectoryPath = UserSettings.Value["PersonalPhotoDirectoryPath"];

        private IRecruitImporter _recruitImporter;
        private IImportedRecruitRepository _importedRecruitRepository;

        private string _sqliteConnectionStringName = "PupaDbContext";
        private Sqlite.Abstract.IDbContextFactory _sqliteDbContextFactory;
        private Sqlite.Abstract.IDbContextCache _sqliteDbContextCache;

        private Sqlite.Abstract.IPrizQuery _sqlitePrizQuery;
        
        private Sqlite.Abstract.IPrizCommand _sqlitePrizCommand;
        private Sqlite.Abstract.ILogCommand _sqliteLogCommand;
        private IUnitOfWorkFactory _sqliteUnitOfWorkFactory;

        private IRecruitInfoRepository _recruitInfoRepository;
        private IEventService _sqliteEventService;

        private string _firebirdConnectionStringName = "FormDbContext";
        private Firebird.Abstract.IDbContextFactory _firebirdDbContextFactory;
        private Firebird.Abstract.IDbContextCache _firebirdDbContextCache;

        private Firebird.Abstract.IPrizQuery _firebirdPrizQuery;

        private Firebird.Abstract.IPrizCommand _firebirdPrizCommand;
        private IUnitOfWorkFactory _firebirdUnitOfWorkFactory;
        private ITransmitService _transmitService;

        private IRecruitExcelExporterFactory _recruitExcelExporterFactory;

        private IParameterizedCommandAsync<ImportRecruitCommandParameters> _importRecruitParameterizedCommand;
        private IParameterizedCommandAsync<RemoveRecruitCommandParameters> _removeRecruitParameterizedCommand;
        private IParameterizedCommandAsync<TransmitRecruitCommandParameters> _transmitRecruitParameterizedCommand;
        private IParameterizedCommandAsync<ExportRecruitCommandParameters> _exportRecruitParameterizedCommand;
        private IParameterizedCommandAsync<ExportTableRecruitCommandParameters> _exportTableRecruitParameterizedCommand;
        private IParameterizedCommandAsync<UpdateRecruitsCommandParameters> _updateRecruitsParameterizedCommand;

        private IParameterizedCommandAsync<SaveRecruitCommandParameters> _saveRecruitParameterizedCommand;

        private IRecruitCardGroupFactory _recruitCardGroupFactory;

        private MainViewModel _mainViewModel;
        private MainView _mainView;

        private RecruitViewModel _recruitViewModel;
        private RecruitView _recruitView;

        private SettingsViewModel _settingsViewModel;
        private SettingsView _settingsView;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var appId = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();
            _appMutex = new Mutex(initiallyOwned: true, name: appId, createdNew: out _isOwner);

            if (!_isOwner)
            {
                MessageBox.Show(NotificationConstants.AppAlreadyStarted);
                Application.Current.Shutdown();
                return;
            }

            XmlConfigurator.Configure();

            RegisterServices();
            ShowMainView();
            HandleExceptions();
        }
        

        private void RegisterServices()
        {
            InitImport();
            InitDataSqlite();
            InitDataFirebird();
            InitExport();

            InitMainViewCommands();
            InitRecruitViewCommands();

            InitRecruitCardGroupFactory();
        }

        private void InitImport()
        {
            _importedRecruitRepository = new ImportedRecruitRepository();

            var importDirectoryPath = UserSettings.Value["ImportDirectoryPath"];
            var personalPhotoDirectoryPath = UserSettings.Value["PersonalPhotoDirectoryPath"];

            _recruitImporter = new PupaRecruitImporter(importDirectoryPath, personalPhotoDirectoryPath);
        }

        private void InitDataSqlite()
        {
            _sqliteDbContextFactory = new Sqlite.Concrete.DbContextFactory(_sqliteConnectionStringName,
                UserSettings.Value["SqliteLocalFilePath"]);

            _sqlitePrizQuery = new Sqlite.Concrete.PrizQuery(_sqliteDbContextFactory);

            _sqliteDbContextCache = new Sqlite.Concrete.DbContextCache(_sqliteDbContextFactory);
            _sqliteUnitOfWorkFactory = new Sqlite.Implementations.UnitOfWorkFactory(_sqliteDbContextCache);

            _sqlitePrizCommand = new Sqlite.Concrete.PrizCommand(_sqliteDbContextCache);
            _sqliteLogCommand = new Sqlite.Concrete.LogCommand(_sqliteDbContextCache);

            _recruitInfoRepository = new Sqlite.Implementations.RecruitInfoRepository(_sqlitePrizQuery, _sqlitePrizCommand);
            _sqliteEventService = new Sqlite.Implementations.EventService(Environment.MachineName, _sqliteLogCommand);
        }

        private void InitDataFirebird()
        {
            _firebirdDbContextFactory = new Firebird.Concrete.DbContextFactory(_firebirdConnectionStringName,
                UserSettings.Value["FirebirdLocalFilePath"]);

            _firebirdPrizQuery = new Firebird.Concrete.PrizQuery(_firebirdDbContextFactory);

            _firebirdDbContextCache = new Firebird.Concrete.DbContextCache(_firebirdDbContextFactory);
            _firebirdUnitOfWorkFactory = new Firebird.Implementations.UnitOfWorkFactory(_firebirdDbContextCache);

            _firebirdPrizCommand = new Firebird.Concrete.PrizCommand(_firebirdDbContextCache);

            _transmitService = new Firebird.Implementations.TransmitService(_firebirdPrizCommand);
        }

        private void InitExport()
        {
            _recruitExcelExporterFactory = new RecruitExcelExporterFactory(isOpenFile: true);
        }

        private void InitMainViewCommands()
        {
            _importRecruitParameterizedCommand = new ImportRecruitsCommand(_recruitImporter, 
                _importedRecruitRepository);

            _removeRecruitParameterizedCommand = new RemoveRecruitCommand(_sqliteUnitOfWorkFactory, 
                _recruitInfoRepository, 
                _sqliteEventService);

            _transmitRecruitParameterizedCommand = new TransmitRecruitCommand(_firebirdUnitOfWorkFactory,
                _sqliteUnitOfWorkFactory,
                _transmitService,
                _recruitInfoRepository,
                _sqliteEventService);
            
            _exportRecruitParameterizedCommand = new ExportRecruitCommand(_recruitInfoRepository, 
                _recruitExcelExporterFactory,
                UserSettings.Value["ExportTemplateFilePath"],
                UserSettings.Value["ExportDirectoryPath"]);

            _exportTableRecruitParameterizedCommand = new ExportTableRecruitCommand(_recruitExcelExporterFactory,
                UserSettings.Value["ExportTableTemplateFilePath"],
                UserSettings.Value["ExportDirectoryPath"]);

            _updateRecruitsParameterizedCommand = new UpdateRecruitsCommand(_sqliteUnitOfWorkFactory,
                _recruitInfoRepository,
                _importedRecruitRepository,
                _sqliteEventService);
        }

        private void InitRecruitViewCommands()
        {
            _saveRecruitParameterizedCommand = new SaveRecruitCommand(_recruitInfoRepository, 
                _sqliteUnitOfWorkFactory, 
                _sqliteEventService);
        }

        private void InitRecruitCardGroupFactory()
        {
            var recruitCardGroupByAdd = new RecruitCardGroup(_personalPhotoDirectoryPath);

            _recruitCardGroupFactory = new RecruitCardGroupFactory(_personalPhotoDirectoryPath, recruitCardGroupByAdd,
                _recruitImporter, _recruitInfoRepository);
        }

        private void ShowMainView()
        {
            _mainViewModel = new MainViewModel(_importRecruitParameterizedCommand,
                _removeRecruitParameterizedCommand,
                _transmitRecruitParameterizedCommand,
                _exportRecruitParameterizedCommand,
                _exportTableRecruitParameterizedCommand,
                _updateRecruitsParameterizedCommand,
                areYouSureCallback: () =>
                {
                    var dialogResult = new AreYouSureDialogView().ShowDialog();
                    return dialogResult.HasValue && dialogResult.Value;
                });

            _mainViewModel.StateChanged += ViewModel_StateChanged;
            _mainViewModel.SettingsViewShowed += SettingsViewShowed;
            _mainViewModel.RecruitViewShowed += RecruitViewShowed;

            _mainView = new MainView(_mainViewModel);
            _mainView.Show();
        }

        private void SettingsViewShowed(object sender, EventArgs e)
        {
            if (_settingsView == null)
            {
                _settingsViewModel = new SettingsViewModel(UserSettings.Value, 
                    notValidCallback: (message) =>
                    {
                        new NotValidDialogView(message).ShowDialog();
                    });

                _settingsViewModel.StateChanged += ViewModel_StateChanged;
                _settingsViewModel.SettingsSaved += SettingsViewModel_SettingsSaved;

                _settingsView = new SettingsView(_settingsViewModel);
                _settingsView.Closed += SettingsView_Closed;

                _settingsView.Owner = _mainView;
                _settingsView.Show();
            }
        }

        private void SettingsViewModel_SettingsSaved(object sender, SettingsEventArgs e)
        {
            foreach (var setting in e.Settings)
            {
                UserSettings.ChangeSetting(setting.Key, setting.Value);
            }

            UserSettings.SaveSettings();

            _settingsView.Close();
            _mainView.Close();
        }

        private void SettingsView_Closed(object sender, EventArgs e)
        {
            _settingsViewModel.StateChanged -= ViewModel_StateChanged;
            _settingsViewModel.SettingsSaved -= SettingsViewModel_SettingsSaved;
            _settingsViewModel = null;

            _settingsView.Closed -= SettingsView_Closed;
            _settingsView = null;
        }

        private void RecruitViewShowed(object sender, RecruitOperationEventArgs e)
        {
            if (_recruitView == null)
            {
                InitRecruitCardGroupFactory();

                _recruitViewModel = new RecruitViewModel(_recruitCardGroupFactory,
                    _saveRecruitParameterizedCommand,
                    e,
                    notValidCallback: (message) =>
                    {
                        new NotValidDialogView(message).ShowDialog();
                    });

                _recruitViewModel.StateChanged += ViewModel_StateChanged;
                _recruitViewModel.RecruitSaved += RecruitViewModel_RecruitSaved;

                _recruitView = new RecruitView(_recruitViewModel);
                _recruitView.Closed += RecruitView_Closed;
                
                _recruitView.Owner = _mainView;
                _recruitView.Show();
            }
        }

        private void RecruitViewModel_RecruitSaved(object sender, RecruitOperationEventArgs e)
        {
            _mainViewModel.RecruitSaved(sender, e);
            
            _recruitView.Close();
        }

        private void RecruitView_Closed(object sender, EventArgs e)
        {
            _recruitViewModel.StateChanged -= ViewModel_StateChanged;
            _recruitViewModel.RecruitSaved -= RecruitViewModel_RecruitSaved;
            _recruitViewModel = null;

            _recruitView.Closed -= RecruitView_Closed;
            _recruitView = null;
        }

        private void ViewModel_StateChanged(object sender, StateEventArgs e)
        {
            WriteToLog(e.StateText, e.StateResult, e.Ex);
        }

        private void WriteToLog(string stateText, StateResult stateResult, Exception ex = null)
        {
            if (stateResult == StateResult.Error)
            {
                if (ex != null)
                {
                    var wrappedEx = ex.WrapException();
                    var wrapedStateText = wrappedEx.Message;

                    _log.Error(wrapedStateText, wrappedEx);

                    new ErrorDialogView(wrapedStateText).ShowDialog();

                    stateText = wrapedStateText;
                }
                else
                {
                    _log.Error(stateText);

                    new ErrorDialogView(stateText).ShowDialog();
                }
            }

            var message = stateResult.StateResultToString() + stateText;

            _mainViewModel.Log += message + Environment.NewLine;
            _mainViewModel.State = message;
        }

        private void HandleExceptions()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                if (ex != null)
                {
                    OnException(ex);
                }
            };

            Application.Current.DispatcherUnhandledException += (s, e) =>
            {
                var ex = e.Exception;
                if (ex != null)
                {
                    OnException(ex);
                }

                e.Handled = true;
            };
        }

        private static void OnException(Exception ex)
        {
            _log.Fatal(ex.Message, ex);

            var text = "Произошла ошибка.\nВ файле \"err.txt\" находится детальная информация.";
            var caption = "Ошибка приложения";

            var result = MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_appMutex != null)
            {
                if (_isOwner)
                {
                    _appMutex.ReleaseMutex();
                }

                _appMutex.Dispose();
            }

            base.OnExit(e);
        }
    }
}
