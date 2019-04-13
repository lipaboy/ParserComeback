using PupaParserComeback.Domain.Constants;
using System;

namespace PupaParserComeback.Domain.DomainModels
{
    public class ServiceInfo : IEquatable<ServiceInfo>
    {
        public string RegionalCollectionPoint { get; private set; }
        public DateTime ConscriptionDate { get; private set; }
        public long? SqliteId { get; private set; }
        public int? FirebirdId { get; private set; }

        public ServiceInfo(string regionalCollecitonPoint, DateTime conscriptionDate, 
            long? sqliteId = null, int? firebirdId = null)
        {
            ChangeRegionalCollectionPoint(regionalCollecitonPoint);
            ChangeConscriptionDate(conscriptionDate);
            ChangeSqliteId(sqliteId);
            ChangeFirebirdId(firebirdId);
        }

        public void ChangeRegionalCollectionPoint(string regionalCollectionPoint)
        {
            if (string.IsNullOrWhiteSpace(regionalCollectionPoint))
            {
                throw new ArgumentNullException(nameof(regionalCollectionPoint));
            }

            if (!RcpConstants.RegionalCollectionPoints.Contains(regionalCollectionPoint))
            {
                throw new ArgumentException(nameof(regionalCollectionPoint));
            }

            RegionalCollectionPoint = regionalCollectionPoint;
        }

        public void ChangeConscriptionDate(DateTime conscriptionDate)
        {
            ConscriptionDate = conscriptionDate;
        }

        public void ChangeSqliteId(long? sqliteId)
        {
            if (sqliteId.HasValue && sqliteId < 0)
            {
                throw new ArgumentException(nameof(sqliteId));
            }

            SqliteId = sqliteId;
        }

        public void ChangeFirebirdId(int? firebirdId)
        {
            if (firebirdId.HasValue && firebirdId < 0)
            {
                throw new ArgumentException(nameof(firebirdId));
            }

            FirebirdId = firebirdId;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as ServiceInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return SqliteId.GetHashCode() ^ FirebirdId.GetHashCode() ^
                   RegionalCollectionPoint.GetHashCode() ^ ConscriptionDate.GetHashCode();
        }

        public bool Equals(ServiceInfo other)
        {
            if (other == null) return false;

            return SqliteId == other.SqliteId &&
                   FirebirdId == other.FirebirdId &&
                   RegionalCollectionPoint == other.RegionalCollectionPoint &&
                   ConscriptionDate == other.ConscriptionDate;
        }

        public static bool operator ==(ServiceInfo left, ServiceInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(ServiceInfo left, ServiceInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
