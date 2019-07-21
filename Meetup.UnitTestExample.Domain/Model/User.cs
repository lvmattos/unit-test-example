using System;

namespace Meetup.UnitTestExample.Domain.Model
{
    public class User : BaseEntity, IEquatable<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool IsBot { get; set; }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id == other.Id &&
                   IsBot == other.IsBot &&
                   FirstName == other.FirstName &&
                   Username == other.Username;
        }

        public override string ToString() => (Username == null
                                         ? FirstName + LastName?.Insert(0, " ")
                                         : $"@{Username}") +
                                     $" ({Id})";

        public User()
        {

        }

        public User(int id)
        {
            Id = id;
        }
    }
}
