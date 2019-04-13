using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Enums;
using System;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.Commands
{
    public class AsyncCommand : CommandBase
    {
        private readonly Func<object, Task> _asyncExecute;
        private readonly IStateChanged _stateChanged;
        private readonly Predicate<object> _canExecute;

        private Task _execution;

        public bool IsExecuting
        {
            get { return _execution != null && !_execution.IsCompleted; }
        }
        
        public AsyncCommand(Func<object, Task> asyncExecute, 
            IStateChanged stateChanged = null,
            Predicate<object> canExecute = null)
        {
            if (asyncExecute == null)
            {
                throw new ArgumentException("Parameter shouldn't be null", nameof(asyncExecute));
            }
            
            _asyncExecute = asyncExecute;
            _stateChanged = stateChanged;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return (_canExecute == null ? true : _canExecute(parameter)) && !IsExecuting;
        }

        public override async void Execute(object parameter)
        {
            _execution = ExecuteAsync(parameter);

            try
            {
                await _execution;
            }
            catch (AggregateException aggrEx)
            {
                var ex = aggrEx.InnerException;
                _stateChanged?.OnStateChanged(ex.Message, StateResult.Error, ex);
            }
            catch (Exception ex)
            {
                _stateChanged?.OnStateChanged(ex.Message, StateResult.Error, ex);
            }
        }

        protected virtual async Task ExecuteAsync(object parameter)
        {
            OnCanExecuteChanged();

            await _asyncExecute(parameter);

            OnCanExecuteChanged();
        }
    }
}
