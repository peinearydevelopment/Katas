using System;

namespace EnterpriseProject.Contracts
{
    public interface ICoordinateParser<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        ILocation<T> ParseCoordinates(string coordinates);
        (ILocation<T> location, decimal bearing) ParseCoordinatesAndBearing(string position);
    }
}
