using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseProject.Contracts
{
    public abstract class Area<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        /*
          left: > 180°
          right: < 0°
          top: v 270°
          bottom: ^ 90°
            -----
            |   |
            |   |
            |   |
            -----

             90°
              |
           0°---180°
              |
             270°
         */
        private readonly List<Boundary<T>> Boundaries;

        public Area()
        {
            Boundaries = new List<Boundary<T>>();
        }

        public Area(List<Boundary<T>> boundaries)
        {
            Boundaries = boundaries;
            EnsureCompleteEnclosure();
        }

        public void AddBoundary(Boundary<T> boundary)
        {
            // TODO: Do geometrical calculations to ensure that boundary isn't outside of current boundaries
            Boundaries.Add(boundary);
        }

        public void EnsureCompleteEnclosure()
        {
            //foreach (var boundary in Boundaries)
            //{
            //    // TODO: Do geometrical calculations to add boundaries to complete enclosure based on current boundaries
            //}
        }

        public bool ContainsLocation(ILocation<T> location)
        {
            return Boundaries.Aggregate(true, (isCurrentlyContained, currentBoundary) => isCurrentlyContained && currentBoundary.ContainsLocation(location));
        }
    }
}
