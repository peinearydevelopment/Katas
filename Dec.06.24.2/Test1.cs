using System.Reflection;

namespace Dec._06._24._2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...";

            var output = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(6, output);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var total = Helper(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "input.txt")));

            var output = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "output.txt");
            if (File.Exists(output))
            {
                File.Delete(output);
            }

            File.WriteAllText(output, total.ToString());
        }

        private int Helper(string[] map)
        {
            var guardsPathMap = CreateMarkedMap(map);
            (int x, int y) currentPosition = (0, 0);
            foreach (var row in map.Select((row, rowIndex) => new { Row = row, RowIndex = rowIndex }))
            {
                foreach (var item in row.Row.Select((value, columnIndex) => new { Value = value, ColumnIndex = columnIndex }).Where(i => i.Value == '^'))
                {
                    currentPosition = (item.ColumnIndex, row.RowIndex);
                }
            }

            var currentDirection = Direction.Up;
            var cursorInsideMap = true;
            static bool IsOutOfBounds(string[] map, (int x, int y) nextCoordinates) => nextCoordinates.x < 0 || nextCoordinates.x == map[0].Length || nextCoordinates.y < 0 || nextCoordinates.y == map.Length;
            static bool IsObstacle(string[] map, (int x, int y) coordinates) => map[coordinates.y][coordinates.x] == '#';
            void MarkPositionAsVisited((int x, int y) coordinates)
            {
                if (map[coordinates.y][coordinates.x] != '^')
                {
                    map[coordinates.y] = map[coordinates.y].Remove(coordinates.x, 1).Insert(coordinates.x, "X");
                }
            }
            (int x, int y) GetNextCoordinates((int x, int y) currentCoordinates, Direction direction) =>
            direction switch
            {
                Direction.Right => (currentCoordinates.x + 1, currentCoordinates.y),
                Direction.Left => (currentCoordinates.x - 1, currentCoordinates.y),
                Direction.Down => (currentCoordinates.x, currentCoordinates.y + 1),
                Direction.Up => (currentCoordinates.x, currentCoordinates.y - 1),
                _ => throw new Exception(),
            };

            while (cursorInsideMap)
            {
                var nextCoordinates = GetNextCoordinates(currentPosition, currentDirection);
                try
                {
                    if (IsOutOfBounds(map, nextCoordinates))
                    {
                        MarkPositionAsVisited(currentPosition);
                        cursorInsideMap = false;
                    }
                    else if (IsObstacle(map, nextCoordinates))
                    {
                        currentDirection = currentDirection switch
                        {
                            Direction.Right => Direction.Down,
                            Direction.Left => Direction.Up,
                            Direction.Down => Direction.Left,
                            Direction.Up => Direction.Right,
                            _ => throw new Exception(),
                        };
                    }
                    else
                    {
                        MarkPositionAsVisited(currentPosition);
                        currentPosition = nextCoordinates;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return map.Select(line => line.Count(i => i == 'X' || i == '^')).Sum();
        }

        private string[] CreateMarkedMap(string[] map)
        {
            (int x, int y) currentPosition = (0, 0);
            foreach (var row in map.Select((row, rowIndex) => new { Row = row, RowIndex = rowIndex }))
            {
                foreach (var item in row.Row.Select((value, columnIndex) => new { Value = value, ColumnIndex = columnIndex }).Where(i => i.Value == '^'))
                {
                    currentPosition = (item.ColumnIndex, row.RowIndex);
                }
            }

            var currentDirection = Direction.Up;
            var cursorInsideMap = true;
            static bool IsOutOfBounds(string[] map, (int x, int y) nextCoordinates) => nextCoordinates.x < 0 || nextCoordinates.x == map[0].Length || nextCoordinates.y < 0 || nextCoordinates.y == map.Length;
            static bool IsObstacle(string[] map, (int x, int y) coordinates) => map[coordinates.y][coordinates.x] == '#';
            void MarkPositionAsVisited((int x, int y) coordinates)
            {
                if (map[coordinates.y][coordinates.x] != '^')
                {
                    map[coordinates.y] = map[coordinates.y].Remove(coordinates.x, 1).Insert(coordinates.x, "X");
                }
            }
            (int x, int y) GetNextCoordinates((int x, int y) currentCoordinates, Direction direction) =>
            direction switch
            {
                Direction.Right => (currentCoordinates.x + 1, currentCoordinates.y),
                Direction.Left => (currentCoordinates.x - 1, currentCoordinates.y),
                Direction.Down => (currentCoordinates.x, currentCoordinates.y + 1),
                Direction.Up => (currentCoordinates.x, currentCoordinates.y - 1),
                _ => throw new Exception(),
            };

            while (cursorInsideMap)
            {
                var nextCoordinates = GetNextCoordinates(currentPosition, currentDirection);
                try
                {
                    if (IsOutOfBounds(map, nextCoordinates))
                    {
                        MarkPositionAsVisited(currentPosition);
                        cursorInsideMap = false;
                    }
                    else if (IsObstacle(map, nextCoordinates))
                    {
                        currentDirection = currentDirection switch
                        {
                            Direction.Right => Direction.Down,
                            Direction.Left => Direction.Up,
                            Direction.Down => Direction.Left,
                            Direction.Up => Direction.Right,
                            _ => throw new Exception(),
                        };
                    }
                    else
                    {
                        MarkPositionAsVisited(currentPosition);
                        currentPosition = nextCoordinates;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return map;
        }
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
