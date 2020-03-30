namespace KISSProject
{
    public static class CoordinateParser
    {
        public static (int X, int Y) ParseCoordinates(this string coordinate)
        {
            var coordinates = coordinate.Trim().Split(' ');
            return (
                int.Parse(coordinates[0]),
                int.Parse(coordinates[1])
            );
        }

        public static (int X, int Y, char forwardDirection) ParseCoordinatesAndForwardDirection(this string position)
        {
            position = position.Trim();
            var lastDelimiter = position.LastIndexOf(' ');
            var (X, Y) = ParseCoordinates(position.Substring(0, lastDelimiter + 1));
            return (
                X,
                Y,
                position.Substring(lastDelimiter + 1)[0]
            );
        }
    }
}
