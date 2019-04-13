using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.EventArguments;
using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Presentation.Models.CardGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.Interfaces
{
    public interface IRecruitCardGroupFactory
    {
        RecruitCardGroup Create(RecruitOperationEventArgs e);
    }
}
