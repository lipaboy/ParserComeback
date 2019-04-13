using System.Collections.Generic;
using System.Configuration;

namespace PupaParserComeback.UI.Configurations
{
    public class UserSettings
    {
        private Dictionary<string, string> _value = new Dictionary<string, string>();

        public UserSettings()
        {
            foreach (SettingsProperty setting in Properties.Settings.Default.Properties)
            {
                var name = setting.Name;
                var value = Properties.Settings.Default[setting.Name] as string;

                if (value != null)
                {
                    _value.Add(name, value);
                }
            }
        }

        public void ChangeSetting(string key, string value)
        {
            _value[key] = value;
            Properties.Settings.Default[key] = value;
        }

        public IReadOnlyDictionary<string, string> Value
        {
            get { return _value; }
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
