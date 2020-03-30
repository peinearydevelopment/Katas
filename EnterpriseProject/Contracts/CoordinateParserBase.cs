using System;

namespace EnterpriseProject.Contracts
{
    public abstract class CoordinateParserBase<T> : ICoordinateParser<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        public char[] Delimiters = new[] { ' ' };

        public abstract ILocation<T> ParseCoordinates(string coordinates);
        public abstract (ILocation<T> location, decimal bearing) ParseCoordinatesAndBearing(string position);
    }
}
