using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Models.Cards;
using PupaParserComeback.Presentation.Commands;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using PupaParserComeback.Presentation.EventArguments;
using PupaParserComeback.Presentation.Enums;

namespace PupaParserComeback.Presentation.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private const string SaveSettingsCommandSuccess = "Настройки сохранены";

        private readonly Dictionary<string, string> _settings;
        private readonly Action<string> _notValidCallback;

        public SettingsCard SettingsCard { get; }

        public event EventHandler<SettingsEventArgs> SettingsSaved;
        public void OnSettingsSaved(SettingsEventArgs e)
        {
            SettingsSaved?.Invoke(this, e);
        }

        public SettingsViewModel(IReadOnlyDictionary<string, string> settings, Action<string> notValidCallback)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (notValidCallback == null)
            {
                throw new ArgumentNullException(nameof(notValidCallback));
            }

            _settings = new Dictionary<string, string>();
            foreach (var setting in settings)
            {
                _settings.Add(setting.Key, setting.Value);
            }

            _notValidCallback = notValidCallback;

            SettingsCard = new SettingsCard();

            SettingsCard.SqliteLocalFilePath = _settings["SqliteLocalFilePath"];
            SettingsCard.FirebirdLocalFilePath = _settings["FirebirdLocalFilePath"];
            SettingsCard.PersonalPhotoDirectoryPath = _settings["PersonalPhotoDirectoryPath"];
            SettingsCard.ImportDirectoryPath = _settings["ImportDirectoryPath"];
            SettingsCard.ExportDirectoryPath = _settings["ExportDirectoryPath"];
            SettingsCard.ExportTemplateFilePath = _settings["ExportTemplateFilePath"];
            SettingsCard.ExportTableTemplateFilePath = _settings["ExportTableTemplateFilePath"];
        }

        private ICommand _saveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get
            {
                return _saveSettingsCommand ?? (_saveSettingsCommand = new ActionCommand(vm =>
                {
                    if (!IsValid)
                    {
                        _notValidCallback(SettingsCard.Error);
                        return;
                    }

                    _settings["SqliteLocalFilePath"] = SettingsCard.SqliteLocalFilePath;
                    _settings["FirebirdLocalFilePath"] = SettingsCard.FirebirdLocalFilePath;

                    _settings["PersonalPhotoDirectoryPath"] = SettingsCard.PersonalPhotoDirectoryPath;
                    _settings["ImportDirectoryPath"] = SettingsCard.ImportDirectoryPath;
                    _settings["ExportDirectoryPath"] = SettingsCard.ExportDirectoryPath;
                    _settings["ExportTemplateFilePath"] = SettingsCard.ExportTemplateFilePath;
                    _settings["ExportTableTemplateFilePath"] = SettingsCard.ExportTableTemplateFilePath;

                    OnSettingsSaved(new SettingsEventArgs(_settings));
                    OnStateChanged(SaveSettingsCommandSuccess, StateResult.Success);
                },
                this));
            }
        }

        private bool IsValid
        {
            get { return string.IsNullOrWhiteSpace(SettingsCard.Error); }
        }
    }
}
