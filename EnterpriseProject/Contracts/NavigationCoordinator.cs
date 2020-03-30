using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseProject.Contracts
{
    public class NavigationCoordinator<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        private readonly Area<T> Area;
        private readonly List<ILocation<T>> OccupiedLocations;

        public NavigationCoordinator(Area<T> area)
        {
            area.EnsureCompleteEnclosure();
            Area = area;
            OccupiedLocations = new List<ILocation<T>>();
        }

        public void Navigate(Rover<T> rover, string instructions)
        {
            foreach (var instruction in instructions)
            {
                var nextPosition = rover.ProposeMove(instruction);
                if (LocationIsInArea(nextPosition) && (rover.IsCurrentLocation(nextPosition) || LocationIsUnoccupied(nextPosition)))
                {
                    rover.ApplyMove();
                }
                else
                {
                    rover.CancelMove();
                }
            }
        }

        public bool TryAddObject(ILocation<T> location)
        {
            if (LocationIsUnoccupied(location) && LocationIsInArea(location) && !ObjectAlreadyInArea(location))
            {
                OccupiedLocations.Add(location);
                return true;
            }

            return false;
        }

        private bool ObjectAlreadyInArea(ILocation<T> location)
        {
            return OccupiedLocations.Any(ol => ol == location);
        }

        private bool LocationIsUnoccupied(ILocation<T> location)
        {
            return !OccupiedLocations.Any(occupiedLocation => occupiedLocation.Equals(location));
        }

        private bool LocationIsInArea(ILocation<T> location)
        {
            return Area.ContainsLocation(location);
        }
    }
}
