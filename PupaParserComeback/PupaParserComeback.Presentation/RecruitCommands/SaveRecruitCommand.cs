using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.EventArguments;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.Mappers;
using PupaParserComeback.Presentation.Models.CardGroups;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands
{
    public class SaveRecruitCommand : IParameterizedCommandAsync<SaveRecruitCommandParameters>
    {
        private readonly IRecruitInfoRepository _recruitInfoRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IEventService _eventService;

        public SaveRecruitCommand(IRecruitInfoRepository recruitInfoRepository,
            IUnitOfWorkFactory unitOfWorkFactory,
            IEventService eventService)
        {
            if (recruitInfoRepository == null)
            {
                throw new ArgumentNullException(nameof(recruitInfoRepository));
            }

            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }

            if (eventService == null)
            {
                throw new ArgumentNullException(nameof(eventService));
            }

            _recruitInfoRepository = recruitInfoRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            
            _eventService = eventService;
        }

        public async Task ExecuteAsync(SaveRecruitCommandParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var recruitInfoMapper = new RecruitInfoMapper(parameters.RecruitCardGroup);
            var recruitInfo = recruitInfoMapper.Map();

            switch (parameters.RecruitOperationEventArgs.RecruitOperation)
            {
                case RecruitOperation.Import:
                case RecruitOperation.Add:
                    IParameterizedCommandAsync<AddRecruitCommandParameters> addRecruitCommand =
                        new AddRecruitCommand(_unitOfWorkFactory,
                            _recruitInfoRepository,
                            _eventService);

                    var addRecruitCommandParameters = new AddRecruitCommandParameters(recruitInfo,
                        parameters.StateChanged);

                    await addRecruitCommand.ExecuteAsync(addRecruitCommandParameters);

                    break;
                case RecruitOperation.Edit:
                    IParameterizedCommandAsync<EditRecruitCommandParameters> editRecruitCommand =
                        new EditRecruitCommand(_unitOfWorkFactory,
                            _recruitInfoRepository,
                            _eventService);

                    var editRecruitCommandParameters = new EditRecruitCommandParameters(recruitInfo,
                        parameters.StateChanged);

                    await editRecruitCommand.ExecuteAsync(editRecruitCommandParameters);

                    break;
            }
        }
    }
}
