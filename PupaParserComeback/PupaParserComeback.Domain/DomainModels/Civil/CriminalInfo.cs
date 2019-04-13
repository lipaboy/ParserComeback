using PupaParserComeback.Domain.Enums;
using System;

namespace PupaParserComeback.Domain.DomainModels.Civil
{
    public class CriminalInfo : IEquatable<CriminalInfo>
    {
        public RegisterStatus RegisterStatus { get; private set; }
        public CriminalStatus CriminalStatus { get; private set; }

        public CriminalInfo(RegisterStatus registerStatus, CriminalStatus criminalStatus)
        {
            RegisterStatus = registerStatus;
            CriminalStatus = criminalStatus;
        }

        public void ChangeRegisterStatus(RegisterStatus registerStatus)
        {
            RegisterStatus = registerStatus;
        }

        public void ChangeCriminalStatus(CriminalStatus criminalStatus)
        {
            CriminalStatus = criminalStatus;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as CriminalInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return RegisterStatus.GetHashCode() ^ CriminalStatus.GetHashCode();
        }

        public bool Equals(CriminalInfo other)
        {
            if (other == null) return false;

            return RegisterStatus == other.RegisterStatus &&
                   CriminalStatus == other.CriminalStatus;
        }

        public static bool operator ==(CriminalInfo left, CriminalInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(CriminalInfo left, CriminalInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
