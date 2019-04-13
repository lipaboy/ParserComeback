using PupaParserComeback.Domain.DomainModels.Common;
using PupaParserComeback.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PupaParserComeback.Domain.DomainModels.Civil
{
    public class FamilyInfo : IEquatable<FamilyInfo>
    {
        public const int MinRelativesCount = 0;
        public const int MaxRelativesCount = 3;

        private int _relativesCount;

        public ParentFamilyStatus ParentFamilyStatus { get; private set; }

        private RelativeInfo[] _relatives;
        public IEnumerable<RelativeInfo> Relatives
        {
            get
            {
                var relatives = new List<RelativeInfo>();

                foreach (var relative in _relatives)
                {
                    if (relative == null) break;
                    relatives.Add(relative);
                }

                return relatives;
            }
        }

        public FamilyInfo(ParentFamilyStatus parentFamilyStatus)
        {
            ParentFamilyStatus = parentFamilyStatus;
            
            _relatives = new RelativeInfo[MaxRelativesCount];
        }

        public FamilyInfo(ParentFamilyStatus parentFamilyStatus, IEnumerable<RelativeInfo> relatives)
            : this(parentFamilyStatus)
        {
            if (relatives == null)
            {
                throw new ArgumentNullException(nameof(relatives));
            }

            foreach (var relative in relatives)
            {
                AddRelative(relative);
            }
        }

        public void AddRelative(RelativeInfo relative)
        {
            if (relative == null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            if (_relativesCount >= MaxRelativesCount)
            {
                throw new InvalidOperationException($"Relatives count can't be more than '{MaxRelativesCount}'");
            }

            var idx = _relativesCount;
            _relatives[idx] = relative;
            _relativesCount++;
        }

        public RelativeInfo GetRelative(int index)
        {
            bool isCorrectIndex = MinRelativesCount <= index && index < MaxRelativesCount;
            if (!isCorrectIndex)
            {
                throw new IndexOutOfRangeException();
            }

            return _relatives[index];
        }

        public bool IsOneParent
        {
            get
            {
                return ParentFamilyStatus == ParentFamilyStatus.OnlyFather ||
                       ParentFamilyStatus == ParentFamilyStatus.OnlyMother;
            }
        }

        public bool IsWithoutParents
        {
            get
            {
                return ParentFamilyStatus == ParentFamilyStatus.AnOrphan ||
                       ParentFamilyStatus == ParentFamilyStatus.BoardingSchool ||
                       ParentFamilyStatus == ParentFamilyStatus.Guardianship ||
                       ParentFamilyStatus == ParentFamilyStatus.Relatives;
            }
        }

        public void ChangeParentFamilyStatus(ParentFamilyStatus parentFamilyStatus)
        {
            ParentFamilyStatus = parentFamilyStatus;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as FamilyInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return ParentFamilyStatus.GetHashCode() ^ Relatives.GetHashCode();
        }

        public bool Equals(FamilyInfo other)
        {
            if (other == null) return false;

            return ParentFamilyStatus == other.ParentFamilyStatus &&
                   Relatives.SequenceEqual(other.Relatives);
        }

        public static bool operator ==(FamilyInfo left, FamilyInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(FamilyInfo left, FamilyInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}