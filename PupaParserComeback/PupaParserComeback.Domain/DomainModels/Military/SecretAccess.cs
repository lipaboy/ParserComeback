using PupaParserComeback.Domain.Enums;
using System;

namespace PupaParserComeback.Domain.DomainModels.Military
{
    public class SecretAccess : IEquatable<SecretAccess>
    {
        public AccessForm AccessForm { get; private set; }
        public string SecretAccessNumber { get; private set; }
        public DateTime IssueDate { get; private set; }

        public SecretAccess(AccessForm accessForm, string secretAccessNumber, DateTime issueDate)
        {
            ChangeAccessForm(accessForm);
            ChangeSecretAccessNumber(secretAccessNumber);
            ChangeIssueDate(issueDate);
        }

        public void ChangeAccessForm(AccessForm accessForm)
        {
            AccessForm = accessForm;
        }

        public void ChangeSecretAccessNumber(string secretAccessNumber)
        {
            if (string.IsNullOrWhiteSpace(secretAccessNumber))
            {
                throw new ArgumentNullException(nameof(secretAccessNumber));
            }

            SecretAccessNumber = secretAccessNumber;
        }

        public void ChangeIssueDate(DateTime issueDate)
        {
            IssueDate = issueDate;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as SecretAccess;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return AccessForm.GetHashCode() ^ SecretAccessNumber.GetHashCode() ^ IssueDate.GetHashCode();
        }

        public bool Equals(SecretAccess other)
        {
            if (other == null) return false;

            return AccessForm == other.AccessForm &&
                   SecretAccessNumber == other.SecretAccessNumber &&
                   IssueDate == other.IssueDate;
        }

        public static bool operator ==(SecretAccess left, SecretAccess right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(SecretAccess left, SecretAccess right)
        {
            return !(left == right);
        }

        #endregion
    }
}
