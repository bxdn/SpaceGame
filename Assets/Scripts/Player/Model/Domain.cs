using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Domain
    {
        private Guid id;
        public Domain()
        {
            id = Guid.NewGuid();
        }
        public override bool Equals(object obj)
        {
            return obj is Domain domain &&
                   id.Equals(domain.id);
        }
        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public static bool operator ==(Domain left, Domain right)
        {
            return EqualityComparer<Domain>.Default.Equals(left, right);
        }

        public static bool operator !=(Domain left, Domain right)
        {
            return !(left == right);
        }
    }
}
