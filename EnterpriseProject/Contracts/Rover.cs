using System;
using System.Collections.Generic;

namespace EnterpriseProject.Contracts
{
    public abstract class Rover<T> : ILocation<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public decimal Bearing { get; set; }
        public List<Action<char>> MovementAbilities { get; }

        protected T? NextX { get; set; }
        protected T? NextY { get; set; }
        protected decimal? NextBearing { get; set; }

        protected Rover(ICoordinateParser<T> parser, string startingLocation)
        {
            var (location, bearing) = parser.ParseCoordinatesAndBearing(startingLocation);
            X = location.X;
            Y = location.Y;
            Bearing = bearing;
            MovementAbilities = new List<Action<char>>();
        }

        public ILocation<T> ProposeMove(char instruction)
        {
            foreach(var movementAbililty in MovementAbilities)
            {
                movementAbililty(instruction);
            }

            return new Location<T>
            {
                X = NextX ?? X,
                Y = NextY ?? Y
            };
        }

        public virtual void ApplyMove()
        {
            if (NextX.HasValue && NextY.HasValue)
            {
                X = NextX.Value;
                Y = NextY.Value;
            }

            if (NextBearing.HasValue)
            {
                Bearing = NextBearing.Value;
            }

            NextX = null;
            NextY = null;
            NextBearing = null;
        }

        public void CancelMove()
        {
            NextX = null;
            NextY = null;
            NextBearing = null;
        }

        public bool IsCurrentLocation(ILocation<T> location)
        {
            return location.X.Equals(X) && location.Y.Equals(Y);
        }
    }
}
