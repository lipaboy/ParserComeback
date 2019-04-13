using System;

namespace PupaParserComeback.Domain.DomainModels.Passport
{
    public class PassportIssueInfo : IEquatable<PassportIssueInfo>
    {
        public string IssueBy { get; private set; }
        public string DevisionCode { get; private set; }
        public DateTime IssueDate { get; private set; }

        public PassportIssueInfo(string issueBy, string devisionCode, DateTime issueDate)
        {
            ChangeIssueBy(issueBy);
            ChangeDevisionCode(devisionCode);
            ChangeIssueDate(issueDate);
        }

        public void ChangeIssueBy(string issueBy)
        {
            if (string.IsNullOrWhiteSpace(issueBy))
            {
                throw new ArgumentNullException(nameof(issueBy));
            }

            IssueBy = issueBy;
        }

        public void ChangeDevisionCode(string devisionCode)
        {
            if (string.IsNullOrWhiteSpace(devisionCode))
            {
                throw new ArgumentNullException(nameof(devisionCode));
            }

            DevisionCode = devisionCode;
        }

        public void ChangeIssueDate(DateTime issueDate)
        {
            IssueDate = issueDate;
        }


        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as PassportIssueInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return IssueBy.GetHashCode() ^ DevisionCode.GetHashCode() ^ IssueDate.GetHashCode();
        }

        public bool Equals(PassportIssueInfo other)
        {
            if (other == null) return false;

            return IssueBy == other.IssueBy &&
                   DevisionCode == other.DevisionCode &&
                   IssueDate == other.IssueDate;
        }

        public static bool operator ==(PassportIssueInfo left, PassportIssueInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(PassportIssueInfo left, PassportIssueInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
