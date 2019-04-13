using PupaParserComeback.Import.Constants;
using System;
using System.IO;

namespace PupaParserComeback.Import.ImportSources
{
    public class ImportFileWriter
    {
        private readonly string _plainPath;

        public ImportFileWriter(string plainPath)
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

        public void WriteAllWords(string[] words)
        {
            ClearFile();
            WriteInFile(words);
        }

        private void ClearFile()
        {
            File.WriteAllText(_plainPath, string.Empty);
        }

        private void WriteInFile(string[] words)
        {
            var str = string.Join(ControlChars.Tab.ToString(), words);

            using (var file = File.OpenWrite(_plainPath))
            using (var txt = new StreamWriter(file))
            {
                txt.Write(str);
            }
        }
    }
}
