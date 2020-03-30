using System;

namespace EnterpriseProject.Contracts
{
    public class Boundary<T>
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
         */
        public ILocation<T> Extremity1 { get; set; }
        public ILocation<T> Extremity2 { get; set; }
        /// <summary>
        /// Degree surface can be moved away from based on 0,0 from area boundary is bound by
        /// </summary>
        public decimal DirectionOfEnclosure { get; set; }

        public bool ContainsLocation(ILocation<T> location)
        {
            switch (DirectionOfEnclosure)
            {
                // TODO: Update for more robustness to handle different geometrical possiblilities
                case 0:
                    if (location.X.CompareTo(Extremity1.X) > 0 || location.X.CompareTo(Extremity2.X) > 0)
                    {
                        return false;
                    }
                    break;
                case 90:
                    if (location.Y.CompareTo(Extremity1.Y) < 0 || location.Y.CompareTo(Extremity2.Y) < 0)
                    {
                        return false;
                    }
                    break;
                case 180:
                    if (location.X.CompareTo(Extremity1.X) < 0 || location.X.CompareTo(Extremity2.X) < 0)
                    {
                        return false;
                    }
                    break;
                case 270:
                    if (location.Y.CompareTo(Extremity1.Y) > 0 || location.Y.CompareTo(Extremity2.Y) > 0)
                    {
                        return false;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

            return true;
        }
    }
}
