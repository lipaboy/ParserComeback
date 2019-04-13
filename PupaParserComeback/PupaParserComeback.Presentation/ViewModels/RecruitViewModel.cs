using PupaParserComeback.Domain.DomainModels.Civil;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.EventArguments;
using PupaParserComeback.Presentation.Mappers;
using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Presentation.Models.CardGroups;
using PupaParserComeback.Presentation.Models.Cards;
using PupaParserComeback.Presentation.RecruitCommands;
using PupaParserComeback.Presentation.Commands;
using System;
using System.Windows.Input;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;

namespace PupaParserComeback.Presentation.ViewModels
{
    public class RecruitViewModel : BaseViewModel
    {
        private readonly IParameterizedCommandAsync<SaveRecruitCommandParameters> _saveParameterizedRecruitCommand;

        private readonly RecruitOperationEventArgs _recruitOperationEventArgs;
        private readonly Action<string> _notValidCallback;

        public event EventHandler<RecruitOperationEventArgs> RecruitSaved;
        public void OnRecruitSaved(RecruitOperationEventArgs recruitOperationEventArgs)
        {
            var recruitOperation = recruitOperationEventArgs.RecruitOperation;
            var recruitShortUIModel = recruitOperationEventArgs.RecruitShortUIModel;

            RecruitSaved?.Invoke(this, new RecruitOperationEventArgs(recruitOperation, recruitShortUIModel));
        }

        public RecruitCardGroup RecruitCardGroup { get; }

        public RecruitViewModel(IRecruitCardGroupFactory recruitCardGroupFactory,
            IParameterizedCommandAsync<SaveRecruitCommandParameters> saveParameterizedRecruitCommand,
            RecruitOperationEventArgs recruitOperationEventArgs, 
            Action<string> notValidCallback)
        {
            if (recruitCardGroupFactory == null)
            {
                throw new ArgumentNullException(nameof(recruitCardGroupFactory));
            }

            if (saveParameterizedRecruitCommand == null)
            {
                throw new ArgumentNullException(nameof(saveParameterizedRecruitCommand));
            }

            if (recruitOperationEventArgs == null)
            {
                throw new ArgumentNullException(nameof(recruitOperationEventArgs));
            }

            if (notValidCallback == null)
            {
                throw new ArgumentNullException(nameof(notValidCallback));
            }

            _saveParameterizedRecruitCommand = saveParameterizedRecruitCommand;
            _recruitOperationEventArgs = recruitOperationEventArgs;
            _notValidCallback = notValidCallback;

            RecruitCardGroup = recruitCardGroupFactory.Create(_recruitOperationEventArgs);
        }

        private ICommand _saveRecruitCommand;
        public ICommand SaveRecruitCommand
        {
            get
            {
                return _saveRecruitCommand ?? (_saveRecruitCommand = new AsyncCommand(async vm =>
                {
                    if (!IsValid)
                    {
                        _notValidCallback(RecruitCardGroup.Error);
                        return;
                    }
                    
                    var parameters = new SaveRecruitCommandParameters(_recruitOperationEventArgs, 
                        RecruitCardGroup, 
                        this);

                    await _saveParameterizedRecruitCommand.ExecuteAsync(parameters);

                    OnRecruitSaved(_recruitOperationEventArgs);
                }, 
                this));
            }
        }

        private bool IsValid
        {
            get { return RecruitCardGroup.IsValid; }
        }
    }
}
