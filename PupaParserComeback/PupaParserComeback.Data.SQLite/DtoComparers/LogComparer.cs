using System;

namespace PupaParserComeback.Data.SQLite.Dto
{
    public partial class log : IEquatable<log>
    {
        public override bool Equals(object obj)
        {
            var source = obj as log;
            if (source == null) return false;

            return Equals(source);
        }
        
        public override int GetHashCode()
        {
            return id.GetHashCode() ^
                   hostname.GetHashCode() ^
                   action.GetHashCode() ^
                   date.GetHashCode() ^
                   time.GetHashCode();
        }

        public bool Equals(log other)
        {
            return id == other.id &&
                   hostname == other.hostname &&
                   action == other.action &&
                   date == other.date &&
                   time == other.time;
        }
    }
}
