using System;

namespace EnterpriseProject.Contracts
{
    public class RectangularArea<T> : Area<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        public void CreateBoundaries(T topExtremity, T rightExtremity, T bottomExtremity, T leftExtremity)
        {
            AddBoundary(new Boundary<T> { Extremity1 = new Location<T> { X = bottomExtremity, Y = leftExtremity } });
            AddBoundary(new Boundary<T> { Extremity1 = new Location<T> { X = topExtremity, Y = leftExtremity } });
            AddBoundary(new Boundary<T> { Extremity1 = new Location<T> { X = topExtremity, Y = rightExtremity } });
            AddBoundary(new Boundary<T> { Extremity1 = new Location<T> { X = bottomExtremity, Y = rightExtremity } });
        }

        public void CreateBoundaries(ILocation<T> upperRightExtremity, ILocation<T> lowerLeftExtremity)
        {
            AddBoundary(new Boundary<T>
            {
                Extremity1 = new Location<T> { Y = lowerLeftExtremity.Y, X = lowerLeftExtremity.X },
                Extremity2 = new Location<T> { Y = upperRightExtremity.Y, X = lowerLeftExtremity.X },
                DirectionOfEnclosure = 180
            });

            AddBoundary(new Boundary<T>
            {
                Extremity1 = new Location<T> { Y = upperRightExtremity.Y, X = lowerLeftExtremity.X },
                Extremity2 = new Location<T> { Y = upperRightExtremity.Y, X = upperRightExtremity.X },
                DirectionOfEnclosure = 270
            });

            AddBoundary(new Boundary<T>
            {
                Extremity1 = new Location<T> { Y = upperRightExtremity.Y, X = upperRightExtremity.X },
                Extremity2 = new Location<T> { Y = lowerLeftExtremity.Y, X = upperRightExtremity.X },
                DirectionOfEnclosure = 0
            });

            AddBoundary(new Boundary<T>
            {
                Extremity1 = new Location<T> { Y = lowerLeftExtremity.Y, X = upperRightExtremity.X },
                Extremity2 = new Location<T> { Y = lowerLeftExtremity.Y, X = lowerLeftExtremity.X },
                DirectionOfEnclosure = 90
            });
        }
    }
}
