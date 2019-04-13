using PupaParserComeback.Presentation.Enums;
using System;

namespace PupaParserComeback.Presentation.EventArguments
{
    public class StateEventArgs : EventArgs
    {
        public string StateText { get; }
        public StateResult StateResult { get; }

        public Exception Ex { get; }

        public StateEventArgs(string stateText, StateResult stateResult,
            Exception ex = null)
        {
            StateText = stateText;
            StateResult = stateResult;

            Ex = ex;
        }
    }
}
