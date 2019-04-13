using PupaParserComeback.Domain.DomainModels.Civil;
using PupaParserComeback.Domain.DomainModels.Medicine;
using PupaParserComeback.Domain.Enums;
using System;

namespace PupaParserComeback.Domain.DomainModels
{
    public class RecruitInfo : IEquatable<RecruitInfo>
    {
        public ServiceInfo ServiceInfo { get; private set; }
        public CriminalInfo CriminalInfo { get; private set; }
        public MedicineInfo MedicineInfo { get; private set; }
        public Envelope Envelope { get; private set; }

        public RecruitInfo(ServiceInfo serviceInfo,
                           CriminalInfo criminalInfo,
                           MedicineInfo medicineInfo,
                           Envelope envelope)
        {
            if (serviceInfo == null)
            {
                throw new ArgumentNullException(nameof(serviceInfo));
            }

            if (criminalInfo == null)
            {
                throw new ArgumentNullException(nameof(criminalInfo));
            }

            if (medicineInfo == null)
            {
                throw new ArgumentNullException(nameof(medicineInfo));
            }

            if (envelope == null)
            {
                throw new ArgumentNullException(nameof(envelope));
            }

            ServiceInfo = serviceInfo;
            CriminalInfo = criminalInfo;
            MedicineInfo = medicineInfo;
            Envelope = envelope;
        }

        public Storage Storage
        {
            get
            {
                if (ServiceInfo.FirebirdId.HasValue) return Storage.Firebird;
                if (ServiceInfo.SqliteId.HasValue) return Storage.Sqlite;

                return Storage.File;
            }
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as RecruitInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return ServiceInfo.GetHashCode() ^ CriminalInfo.GetHashCode() ^
                   MedicineInfo.GetHashCode() ^ Envelope.GetHashCode();
        }

        public bool Equals(RecruitInfo other)
        {
            if (other == null) return false;

            return ServiceInfo == other.ServiceInfo &&
                   CriminalInfo == other.CriminalInfo &&
                   MedicineInfo == other.MedicineInfo &&
                   Envelope == other.Envelope;
        }

        public static bool operator ==(RecruitInfo left, RecruitInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(RecruitInfo left, RecruitInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
