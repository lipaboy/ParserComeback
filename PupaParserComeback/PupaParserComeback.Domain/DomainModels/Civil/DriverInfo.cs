using PupaParserComeback.Domain.DomainModels.Common;
using System;

namespace PupaParserComeback.Domain.DomainModels.Civil
{
    public class DriverInfo : IEquatable<DriverInfo>
    {
        public Code Code { get; private set; }
        public DateTime IssueDate { get; private set; }

        public DriverInfo(Code code, DateTime issueDate)
        {
            ChangeCode(code);
            ChangeIssueDate(issueDate);
        }

        public void ChangeCode(Code code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            Code = code;
        }

        public void ChangeIssueDate(DateTime issueDate)
        {
            IssueDate = issueDate;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as DriverInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode() ^ IssueDate.GetHashCode();
        }

        public bool Equals(DriverInfo other)
        {
            if (other == null) return false;

            return Code == other.Code &&
                   IssueDate == other.IssueDate;
        }

        public static bool operator ==(DriverInfo left, DriverInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(DriverInfo left, DriverInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
