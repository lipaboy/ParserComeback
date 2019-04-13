using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Enums;
using System;

namespace PupaParserComeback.Presentation.Commands
{
    public class ActionCommand : CommandBase
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        private readonly IStateChanged _stateChanged;
        
        public ActionCommand(Action<object> execute, IStateChanged stateChanged = null,
            Predicate<object> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentException("Parameter shouldn't be null", "execute");
            }

            _execute = execute;
            _stateChanged = stateChanged;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            try
            {
                _execute(parameter);
            }
            catch (Exception ex)
            {
                _stateChanged?.OnStateChanged(ex.Message, StateResult.Error, ex);
            }
        }
    }
}
