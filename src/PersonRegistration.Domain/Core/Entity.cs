using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PersonRegistration.Domain.Core
{
    public abstract class Entity 
    {

        protected Entity()
        {
            Id = Guid.NewGuid();
            Errors = new ReadOnlyCollection<DomainNotification>(new List<DomainNotification>());
        }

        public IReadOnlyCollection<DomainNotification> Errors { get; protected set; }

        public Guid Id { get; protected set; }

        public bool IsValid => !Errors.Any();

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash += Id.GetHashCode() * 677;

                return hash;
            }
        }

        public static bool operator == (Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator != (Entity a, Entity b)
        {
            return !(a == b);
        }
    }
}
