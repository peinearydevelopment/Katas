var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));
//var a = @"...........
//.S-------7.
//.|F-----7|.
//.||.....||.
//.||.....||.
//.|L-7.F-J|.
//.|..|.|..|.
//.L--J.L--J.
//...........";
//var b = @"FF7FSF7F7F7F7F7F---7
//L|LJ||||||||||||F--J
//FL-7LJLJ||||||LJL-77
//F--JF--7||LJLJ7F7FJ-
//L---JF-JLJ.||-FJLJJ7
//|F|F-JF---7F7-L7L|7|
//|FFJF7L7F-JF7|JL---7
//7-L-JL7||F7|L7F-7F7|
//L.L7LFJ|||||FJL7||LJ
//L7JLJL-JLJLJL--JLJ.L";
//var lines = b.Split(Environment.NewLine);

var map = new Map(lines);
map.ToDirectionedMap();
Console.WriteLine(map.MarkAndCountEnclosed());
Console.WriteLine(map.Print());

class Map
{
    private char[][] _map;
    public Coordinate Start { get; }
    public LinkedList<Coordinate> Loop { get; }

    public Map(string[] map)
    {
        Start = map.Select((line, i) => new { line, i })
                    .Where(l => l.line.Contains((char)Direction.Start))
                    .Select(l => new Coordinate { Row = l.i, Column = l.line.IndexOf((char)Direction.Start) })
                    .Single();
        _map = map.Select(l => l.ToCharArray()).ToArray();
        Loop = GetLoop();
    }

    public void ToDirectionedMap()
    {
        ClearJunk();

        var bearing = (char)Direction.Start;
        var previousCoordinate = Loop.First.Value;
        var corners = new[] { Direction.NW, Direction.NE, Direction.SW, Direction.SE };
        foreach (var coordinate in Loop)
        {
            var currentDirection = GetDirectionAtCoordinate(coordinate);
            if (previousCoordinate.Column < coordinate.Column)
            {
                bearing = '>';
            }
            else if (previousCoordinate.Column > coordinate.Column)
            {
                bearing = '<';
            }

            if (!corners.Contains(currentDirection))
            {
                UpdateMap(coordinate, bearing);
            }
            previousCoordinate = coordinate;
        }
    }

    public void ClearJunk()
    {
        foreach (var nonPathCoordinate in _map.SelectMany((line, i) => line.Select((c, j) => new Coordinate { Row = i, Column = j })).Where(mc => !Loop.Any(pc => pc.Row == mc.Row && pc.Column == mc.Column)))
        {
            UpdateMap(nonPathCoordinate, '.');
        }
    }

    public void UpdateMap(Coordinate coordinateToUpdate, char newValue)
    {
        _map[coordinateToUpdate.Row][coordinateToUpdate.Column] = newValue;
    }

    public string Print()
    {
        return string.Join(Environment.NewLine, _map.Select(l => string.Concat(l)));
    }

    public int MarkAndCountEnclosed()
    {
        var enclosed = 0;

        foreach (var row in _map.Select((line, i) => new { Line = line, Index = i }))
        {
            for (var i = 0; i < row.Line.Length; i++)
            {
                if (row.Line[i] == '<' || row.Line[i] == '>')
                {
                    while (i < row.Line.Length && (row.Line[i] == '<' || row.Line[i] == '>'))
                    {
                        i++;
                    }

                    if (i == row.Line.Length)
                    {
                        continue;
                    }

                    if (row.Line[i] == '.')
                    {
                        var count = 0;
                        var symbol = row.Line[i - 1];
                        while (i < row.Line.Length && row.Line[i] == '.')
                        {
                            i++;
                            count++;
                        }

                        if (i == row.Line.Length)
                        {
                            continue;
                        }

                        if ((row.Line[i] == '<' && symbol == '>') || (row.Line[i] == '>' && symbol == '<'))
                        {
                            enclosed += count;

                            for (var j = count; j > 0; j--)
                            {
                                UpdateMap(new Coordinate { Row = row.Index, Column = i - j }, 'I');
                            }
                        }
                    }
                }
            }
        }

        return enclosed;
    }

    private LinkedList<Coordinate> GetLoop()
    {
        var north = new Coordinate { Row = Start.Row - 1, Column = Start.Column };
        var loop = GetLoopCoordinates(north);
        if (loop is not null)
        {
            return loop;
        }

        var east = new Coordinate { Row = Start.Row, Column = Start.Column + 1 };
        loop = GetLoopCoordinates(east);
        if (loop is not null)
        {
            return loop;
        }

        var south = new Coordinate { Row = Start.Row + 1, Column = Start.Column };
        loop = GetLoopCoordinates(south);
        if (loop is not null)
        {
            return loop;
        }

        var west = new Coordinate { Row = Start.Row, Column = Start.Column - 1 };
        loop = GetLoopCoordinates(west);
        return loop;
    }

    private LinkedList<Coordinate>? GetLoopCoordinates(Coordinate nextMove)
    {
        var length = 1;
        var current = Start;
        var next = nextMove;
        var ll = new LinkedList<Coordinate>(new[] { current, next });
        while (next is not null && GetDirectionAtCoordinate(next) != Direction.Ground && GetDirectionAtCoordinate(next) != Direction.Start)
        {
            var temp = GetNextCoordinate(current, next);
            current = next;
            next = temp;
            ll.AddLast(temp);
            length++;
        }

        return next is not null && GetDirectionAtCoordinate(next) == Direction.Start ? ll : null;
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
    NS = '|',     // <>
    EW = '-',     // ^v
    NE = 'L',     // >
    NW = 'J',     // <
    SW = '7',     // <
    SE = 'F',     // >
    Ground = '.',
    Start = 'S'
}