using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Presentation.Models;
using System.Collections.Generic;

namespace PupaParserComeback.Presentation.Interfaces
{
    public interface IRecruitExcelExporter
    {
        void ExportRecruitInfoesToExcel(RecruitInfo firstRecruit, 
            RecruitInfo secondRecruit = null);

        void ExportRecruitShortUIModelsToExcelTable(IEnumerable<RecruitShortUIModel> recruitShortUIModels,
            string regionalCollectionPoint, 
            string conscriptionDate);
    }
}
