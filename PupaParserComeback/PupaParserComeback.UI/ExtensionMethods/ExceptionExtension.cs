using PupaParserComeback.UI.Exceptions;
using System;

namespace PupaParserComeback.UI.ExtensionMethods
{
    public static class ExceptionExtension
    {
        public static Exception WrapException(this Exception ex)
        {
            var source = ex.Source;
            
            if (source.EndsWith("Domain"))
            {
                return new DomainException(ex);
            }

            if (source.EndsWith("Import"))
            {
                return new ImportException(ex);
            }

            if (source.EndsWith("Export"))
            {
                return new ExportException(ex);
            }
            
            return ex;
        }
    }
}
