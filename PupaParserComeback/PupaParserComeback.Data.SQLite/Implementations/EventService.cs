using PupaParserComeback.Data.SQLite.Abstract;
using PupaParserComeback.Data.SQLite.Dto;
using PupaParserComeback.Domain.Constants;
using PupaParserComeback.Domain.Interfaces;
using System;

namespace PupaParserComeback.Data.SQLite.Implementations
{
    public class EventService : IEventService
    {
        private string _hostname;
        private ILogCommand _logCommand;

        public EventService(string hostname, ILogCommand logCommand)
        {
            if (string.IsNullOrWhiteSpace(hostname))
            {
                throw new ArgumentNullException(nameof(hostname));
            }
            
            if (logCommand == null)
            {
                throw new ArgumentNullException(nameof(logCommand));
            }

            _hostname = hostname;
            _logCommand = logCommand;
        }

        public void Fire(string message)
        {
            var now = DateTime.Now;

            _logCommand.Insert(new log()
            {
                hostname = _hostname,
                action = message,
                date = now.ToString(DateConstants.EventDateFormat),
                time = now.ToString(DateConstants.EventTimeFormat)
            });
        }
    }
}
