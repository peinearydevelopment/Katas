using System;

namespace EnterpriseProject.Contracts
{
    public class Location<T> : ILocation<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
    }
}
