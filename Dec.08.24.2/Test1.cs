using System.Linq;
using System.Reflection;

namespace Dec._08._24._2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............";
            /*
##....#....#
.#.#....0...
..#.#0....#.
..##...0....
....0....#..
.#...#A....#
...#..#.....
#....#.#....
..#.....A...
....#....A..
.#........#.
...#......##
             */

            //            var input = @"........
            //........
            //........
            //....x...
            //...x....
            //........
            //........
            //........";

            var total = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(34, total);
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

        private int Helper(string[] lines)
        {
            var frequencyGroups = lines.SelectMany((line, yIndex) => line.Select((item, xIndex) => (item, xIndex, yIndex)).Where(i => i.item != '.'))
                .GroupBy(i => i.item);
            static bool PointIsInMap(string[] map, (int x, int y) p) => p.x >= 0 && p.y >= 0 && p.x < map.First().Length && p.y < map.Length;
            var points = new List<(int x, int y)>();
            foreach (var frequencyGroup in frequencyGroups)
            {
                foreach (var antenna in frequencyGroup)
                {
                    foreach (var otherAntenna in frequencyGroup.Where(otherAntenna => otherAntenna != antenna))
                    {
                        var xdiff = antenna.xIndex - otherAntenna.xIndex;
                        var ydiff = antenna.yIndex - otherAntenna.yIndex;

                        if (xdiff == 0)
                        {
                            var iteration = 1;
                            var newYDiff = ydiff;
                            while (true)
                            {
                                var p1 = (antenna.xIndex, ydiff >= 0 ? antenna.yIndex + newYDiff : antenna.yIndex + newYDiff);
                                var p2 = (antenna.xIndex, ydiff >= 0 ? otherAntenna.yIndex - newYDiff : otherAntenna.yIndex - newYDiff);
                                if (p1.Item2 != antenna.yIndex && p2.Item2 != otherAntenna.yIndex && p1.Item2 != otherAntenna.yIndex && p2.Item2 != antenna.yIndex)
                                {
                                    points.Add(p1);
                                    points.Add(p2);
                                }

                                if (!PointIsInMap(lines, p1) && !PointIsInMap(lines, p2))
                                {
                                    break;
                                }
                                iteration++;
                                newYDiff = ydiff * iteration;
                            }

                        }
                        else if (ydiff == 0)
                        {
                            var iteration = 1;
                            var newXDiff = xdiff;
                            while (true)
                            {
                                var p1 = (xdiff >= 0 ? antenna.xIndex + newXDiff : antenna.xIndex + newXDiff, antenna.yIndex);
                                var p2 = (xdiff >= 0 ? otherAntenna.xIndex - newXDiff : otherAntenna.xIndex - newXDiff, otherAntenna.yIndex);
                                if (p1.Item1 != antenna.xIndex && p2.Item1 != otherAntenna.xIndex && p1.Item1 != otherAntenna.xIndex && p2.Item1 != antenna.xIndex)
                                {
                                    points.Add(p1);
                                    points.Add(p2);
                                }

                                if (!PointIsInMap(lines, p1) && !PointIsInMap(lines, p2))
                                {
                                    break;
                                }
                                iteration++;
                                newXDiff = xdiff * iteration;
                            }
                        }
                        else
                        {
                            var iteration = 1;
                            var newXDiff = xdiff;
                            var newYDiff = ydiff;
                            while (true)
                            {
                                var p1 = (xdiff >= 0 ? antenna.xIndex + newXDiff : antenna.xIndex + newXDiff, ydiff >= 0 ? antenna.yIndex + newYDiff : antenna.yIndex + newYDiff);
                                var p2 = (xdiff >= 0 ? otherAntenna.xIndex - newXDiff : otherAntenna.xIndex - newXDiff, ydiff >= 0 ? otherAntenna.yIndex - newYDiff : otherAntenna.yIndex - newYDiff);
                                if (p1.Item1 != antenna.xIndex && p1.Item2 != antenna.yIndex &&
                                    p1.Item1 != otherAntenna.xIndex && p1.Item2 != otherAntenna.yIndex &&
                                    p2.Item1 != antenna.xIndex && p2.Item2 != antenna.yIndex &&
                                    p2.Item1 != otherAntenna.xIndex && p2.Item2 != otherAntenna.yIndex)
                                {
                                    points.Add(p1);
                                    points.Add(p2);
                                }

                                if (!PointIsInMap(lines, p1) && !PointIsInMap(lines, p2))
                                {
                                    break;
                                }
                                iteration++;
                                newXDiff = xdiff * iteration;
                                newYDiff = ydiff * iteration;
                            }
                        }
                    }
                }
            }
            var a = points.Concat(frequencyGroups.SelectMany(g => g.Count() <= 1 ? [] : g.Select(p => (p.xIndex, p.yIndex)))).Distinct().ToArray();
                var b= a.Where(p => PointIsInMap(lines, p)).OrderBy(p => p.Item2).ThenBy(p => p.Item1).ToArray();
            return b.Count();
        }
    }
}
