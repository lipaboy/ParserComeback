using PupaParserComeback.Import.Constants;
using System;
using System.IO;

namespace PupaParserComeback.Import.ImportSources
{
    public class ImportFileReader
    {
        private readonly string _plainPath;

        public ImportFileReader(string plainPath)
        {
            if (string.IsNullOrWhiteSpace(plainPath))
            {
                throw new ArgumentNullException(nameof(plainPath));
            }

            if (Path.GetExtension(plainPath) != Extensions.PlainExtension)
            {
                throw new ArgumentException(nameof(plainPath));
            }

            _plainPath = plainPath;
        }

        public string[] ReadAllWords()
        {
            using (var file = File.OpenRead(_plainPath))
            using (var txt = new StreamReader(file))
            {
                return txt.ReadToEnd().Split(ControlChars.Tab);
            }
        }
    }
}
