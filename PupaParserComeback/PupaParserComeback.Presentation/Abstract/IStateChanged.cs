using PupaParserComeback.Presentation.Enums;
using System;

namespace PupaParserComeback.Presentation.Abstract
{
    public interface IStateChanged
    {
        void OnStateChanged(string state, StateResult stateResult, Exception ex = null);
    }
}
