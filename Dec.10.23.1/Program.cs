var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

var map = new Map(lines);
Console.WriteLine(GetLoopLength(map) / 2);

int GetLoopLength(Map map)
{
    var startingCoordinates = lines.Select((line, i) => new { line, i })
                                .Where(l => l.line.Contains((char)Direction.Start))
                                .Select(l => new Coordinate { Row = l.i, Column = l.line.IndexOf((char)Direction.Start) })
                                .Single();

    var north = new Coordinate { Row = map.Start.Row - 1, Column = map.Start.Column };
    var loopLength = GetLoopLength2(map, north);
    if (loopLength > 0)
    {
        return loopLength;
    }

    var east = new Coordinate { Row = map.Start.Row, Column = map.Start.Column + 1 };
    loopLength = GetLoopLength2(map, east);
    if (loopLength > 0)
    {
        return loopLength;
    }

    var south = new Coordinate { Row = map.Start.Row + 1, Column = map.Start.Column };
    loopLength = GetLoopLength2(map, south);
    if (loopLength > 0)
    {
        return loopLength;
    }

    var west = new Coordinate { Row = map.Start.Row, Column = map.Start.Column - 1 };
    loopLength = GetLoopLength2(map, west);
    return loopLength;
}

int GetLoopLength2(Map map, Coordinate nextMove)
{
    var length = 1;
    var current = map.Start;
    var next = nextMove;
    while (next is not null && map.GetDirectionAtCoordinate(next) != Direction.Ground && map.GetDirectionAtCoordinate(next) != Direction.Start)
    {
        var temp = map.GetNextCoordinate(current, next);
        current = next;
        next = temp;
        length++;
    }

    return next is not null && map.GetDirectionAtCoordinate(next) == Direction.Start ? length : -1;
}

class Map
{
    private string[] _map;
    public Coordinate Start { get; }

    public Map(string[] map)
    {
        Start = map.Select((line, i) => new { line, i })
                    .Where(l => l.line.Contains((char)Direction.Start))
                    .Select(l => new Coordinate { Row = l.i, Column = l.line.IndexOf((char)Direction.Start) })
                    .Single();
        _map = map;
    }

    public Direction GetDirectionAtCoordinate(Coordinate coordinate)
    {
        if (coordinate.Row < 0 || coordinate.Column < 0 || coordinate.Row == _map.Length || coordinate.Column == _map[0].Length)
        {
            return Direction.Ground;
        }

        return (Direction)_map[coordinate.Row][coordinate.Column];
    }

    public Coordinate? GetNextCoordinate(Coordinate coordinate, Coordinate nextCoordinate)
    {
        var nextDirection = GetDirectionAtCoordinate(nextCoordinate);
        /* ..... .....
         * .@-o. .o-@.
         * ..... .....
         * @ -> 1,1; - -> 1,2; o -> 1,3
         * @ -> 1,3; - -> 1,2; o -> 1,1
         */
        if (nextDirection == Direction.EW)
        {
            if (coordinate.Column == nextCoordinate.Column - 1)
            {
                return new Coordinate { Row = nextCoordinate.Row, Column = nextCoordinate.Column + 1 };
            }
            else if (coordinate.Column - 1 == nextCoordinate.Column)
            {
                return new Coordinate { Row = nextCoordinate.Row, Column = nextCoordinate.Column - 1 };
            }
            else
            {
                return null;
            }
        }

        /* ..... ..o..
         * .@... ..L@.
         * .Lo.. .....
         * @ -> 1,1; - -> 2,1; o -> 2,2
         * @ -> 1,3; - -> 1,2; o -> 0,2
         */
        if (nextDirection == Direction.NE)
        {
            if (coordinate.Row == nextCoordinate.Row - 1)
            {
                return new Coordinate { Row = nextCoordinate.Row, Column = nextCoordinate.Column + 1 };
            }
            else if (coordinate.Column == nextCoordinate.Column + 1)
            {
                return new Coordinate { Row = nextCoordinate.Row - 1, Column = nextCoordinate.Column };
            }
            else
            {
                return null;
            }
        }

        /* .@... .o...
         * .|... .|...
         * .o... .@...
         * @ -> 0,1; - -> 1,1; o -> 2,1
         * @ -> 2,1; - -> 1,1; o -> 0,1
         */
        if (nextDirection == Direction.NS)
        {
            if (coordinate.Row == nextCoordinate.Row - 1)
            {
                return new Coordinate { Row = nextCoordinate.Row + 1, Column = nextCoordinate.Column };
            }
            else if (coordinate.Row - 1 == nextCoordinate.Row)
            {
                return new Coordinate { Row = nextCoordinate.Row - 1, Column = nextCoordinate.Column };
            }
            else
            {
                return null;
            }
        }

        /* ..... ..o..
         * .@... .@J..
         * oJ... .....
         * @ -> 1,1; - -> 2,1; o -> 2,0
         * @ -> 1,1; - -> 1,2; o -> 0,2
         */
        if (nextDirection == Direction.NW)
        {
            if (coordinate.Row == nextCoordinate.Row - 1)
            {
                return new Coordinate { Row = nextCoordinate.Row, Column = nextCoordinate.Column - 1 };
            }
            else if (coordinate.Column + 1 == nextCoordinate.Column)
            {
                return new Coordinate { Row = nextCoordinate.Row - 1, Column = nextCoordinate.Column };
            }
            else
            {
                return null;
            }
        }

        /* ..... .....
         * .Fo.. .F@..
         * .@... .o...
         * @ -> 1,1; - -> 2,1; o -> 2,0
         * @ -> 1,1; - -> 1,2; o -> 0,2
         */
        if (nextDirection == Direction.SE)
        {
            if (coordinate.Row - 1 == nextCoordinate.Row)
            {
                return new Coordinate { Row = nextCoordinate.Row, Column = nextCoordinate.Column + 1 };
            }
            else if (coordinate.Column - 1 == nextCoordinate.Column)
            {
                return new Coordinate { Row = nextCoordinate.Row + 1, Column = nextCoordinate.Column };
            }
            else
            {
                return null;
            }
        }

        /* ..... .....
         * o7... @7...
         * .@... .o...
         * @ -> 1,1; - -> 2,1; o -> 2,0
         * @ -> 1,1; - -> 1,2; o -> 0,2
         */
        if (nextDirection == Direction.SW)
        {
            if (coordinate.Row - 1 == nextCoordinate.Row)
            {
                return new Coordinate { Row = nextCoordinate.Row, Column = nextCoordinate.Column - 1 };
            }
            else if (coordinate.Column + 1 == nextCoordinate.Column)
            {
                return new Coordinate { Row = nextCoordinate.Row + 1, Column = nextCoordinate.Column };
            }
            else
            {
                return null;
            }
        }

        return null;
    }
}

class Coordinate
{
    public int Row;
    public int Column;
}

enum Direction
{
    NS = '|',
    EW = '-',
    NE = 'L',
    NW = 'J',
    SW = '7',
    SE = 'F',
    Ground = '.',
    Start = 'S'
}