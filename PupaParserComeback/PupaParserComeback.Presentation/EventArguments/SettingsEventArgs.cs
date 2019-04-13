using System;
using System.Collections.Generic;

namespace PupaParserComeback.Presentation.EventArguments
{
    public class SettingsEventArgs : EventArgs
    {
        public IReadOnlyDictionary<string, string> Settings { get; }

        public SettingsEventArgs(IReadOnlyDictionary<string, string> settings)
        {
            Settings = settings;
        }
    }
}
