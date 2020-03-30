using System;
using System.Linq;

namespace EnterpriseProject.Contracts
{
    public class SimpleCoordinateParser : CoordinateParserBase<int>
    {
        public override ILocation<int> ParseCoordinates(string coordinate)
        {
            if (coordinate == null)
            {
                throw new ArgumentNullException(nameof(coordinate));
            }

            var coordinates = coordinate.Split(Delimiters);

            if (coordinates.Length != 2 || !int.TryParse(coordinates[0], out int x) || !int.TryParse(coordinates[1], out int y))
            {
                throw new ArgumentException($"The parameter expects two coordinates of type {typeof(int).Name} seperated by one of the following characters: '{string.Join("','", Delimiters)}'.", nameof(coordinate));
            }

            return new SimpleLocation
            {
                X = x,
                Y = y
            };
        }

        public override (ILocation<int> location, decimal bearing) ParseCoordinatesAndBearing(string position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            var lastDelimiter = position.LastIndexOfAny(Delimiters);

            var direction = position.Substring(lastDelimiter + 1);
            if (direction.Length != 1 || !new[] { 'N', 'S', 'W', 'E' }.Contains(direction[0]))
            {
                throw new ArgumentException($"The parameter expects two coordinates of type {typeof(int).Name} and a char indicating direction seperated by one of the following characters: '{string.Join("','", Delimiters)}'.", nameof(position));
            }

            var location = ParseCoordinates(position.Substring(0, lastDelimiter));

            return (
                location,
                direction[0].TranslateBearing()
            );
        }
    }
}
