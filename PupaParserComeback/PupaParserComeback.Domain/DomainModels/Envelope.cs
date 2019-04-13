using PupaParserComeback.Domain.DomainModels.Civil;
using PupaParserComeback.Domain.DomainModels.Military;
using PupaParserComeback.Domain.DomainModels.Passport;
using System;

namespace PupaParserComeback.Domain.DomainModels
{
    public class Envelope : IEquatable<Envelope>
    {
        public PassportInfo PassportInfo { get; private set; }
        public MilitaryInfo MilitaryInfo { get; private set; }
        public CivilInfo CivilInfo { get; private set; }
        public Contacts Contacts { get; private set; }
        public FamilyInfo FamilyInfo { get; private set; }
        public DriverInfo DriverInfo { get; private set; }

        public bool IsDriver
        {
            get { return DriverInfo != null; }
        }

        public Envelope(PassportInfo passportInfo,
                        MilitaryInfo militaryInfo,
                        CivilInfo civilInfo,
                        Contacts contacts,
                        FamilyInfo familyInfo, 
                        DriverInfo driverInfo = null)
        {
            if (passportInfo == null)
            {
                throw new ArgumentNullException(nameof(passportInfo));
            }

            if (militaryInfo == null)
            {
                throw new ArgumentNullException(nameof(passportInfo));
            }

            if (civilInfo == null)
            {
                throw new ArgumentNullException(nameof(passportInfo));
            }

            if (contacts == null)
            {
                throw new ArgumentNullException(nameof(passportInfo));
            }

            if (familyInfo == null)
            {
                throw new ArgumentNullException(nameof(passportInfo));
            }

            PassportInfo = passportInfo;
            MilitaryInfo = militaryInfo;
            CivilInfo = civilInfo;
            Contacts = contacts;
            FamilyInfo = familyInfo;
            DriverInfo = driverInfo;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as Envelope;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return PassportInfo.GetHashCode() ^ MilitaryInfo.GetHashCode() ^
                   CivilInfo.GetHashCode() ^ Contacts.GetHashCode() ^
                   FamilyInfo.GetHashCode() ^ DriverInfo.GetHashCode();
        }

        public bool Equals(Envelope other)
        {
            if (other == null) return false;

            return PassportInfo == other.PassportInfo &&
                   MilitaryInfo == other.MilitaryInfo &&
                   CivilInfo == other.CivilInfo &&
                   Contacts == other.Contacts &&
                   FamilyInfo == other.FamilyInfo &&
                   DriverInfo == other.DriverInfo;
        }

        public static bool operator ==(Envelope left, Envelope right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(Envelope left, Envelope right)
        {
            return !(left == right);
        }

        #endregion
    }
}
