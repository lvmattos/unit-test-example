using System;
using System.Collections.Generic;

namespace Meetup.UnitTestExample.Domain.Model
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        public int Id { get; protected set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        public bool Equals(BaseEntity other)
        {
            return other != null &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(BaseEntity entity1, BaseEntity entity2)
        {
            return EqualityComparer<BaseEntity>.Default.Equals(entity1, entity2);
        }

        public static bool operator !=(BaseEntity entity1, BaseEntity entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
