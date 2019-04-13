using System;
using System.Collections.Generic;

namespace PupaParserComeback.Export.Models
{
    public class RecruitsExcelTable
    {
        public string ConscriptionDate { get; }
        public string RegionalCollectionPoint { get; }

        public IEnumerable<RecruitExcelTableItem> TableItems { get; }

        public RecruitsExcelTable(string conscriptionDate, string regionalCollectionPoint,
            IEnumerable<RecruitExcelTableItem> tableItems)
        {
            if (string.IsNullOrWhiteSpace(conscriptionDate))
            {
                throw new ArgumentNullException(nameof(conscriptionDate));
            }

            if (string.IsNullOrWhiteSpace(regionalCollectionPoint))
            {
                throw new ArgumentNullException(nameof(regionalCollectionPoint));
            }

            if (tableItems == null)
            {
                throw new ArgumentNullException(nameof(tableItems));
            }

            ConscriptionDate = conscriptionDate;
            RegionalCollectionPoint = regionalCollectionPoint;
            TableItems = tableItems;
        }
    }
}
