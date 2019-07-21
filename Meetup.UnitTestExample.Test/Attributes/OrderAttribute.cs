using System;

namespace Meetup.UnitTestExample.Test.Attributes
{
    public class OrderAttribute : Attribute
    {
        public int Order { get; }

        public OrderAttribute(int order)
        {
            Order = order;
        }
    }
}
