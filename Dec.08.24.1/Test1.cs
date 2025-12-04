using System.Reflection;

namespace Dec._08._24._1
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

            //            var input = @".x
            //x.";

            var total = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(14, total);
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
                            var p1 = (antenna.xIndex, ydiff > 0 ? antenna.yIndex + ydiff : antenna.yIndex + ydiff);
                            var p2 = (antenna.xIndex, ydiff > 0 ? otherAntenna.yIndex - ydiff : otherAntenna.yIndex - ydiff);
                            points.Add(p1);
                            points.Add(p2);
                        }
                        else if (ydiff == 0)
                        {
                            var p1 = (xdiff > 0 ? antenna.xIndex + xdiff : antenna.xIndex + xdiff, antenna.yIndex);
                            var p2 = (xdiff > 0 ? otherAntenna.xIndex - xdiff : otherAntenna.xIndex - xdiff, otherAntenna.yIndex);
                            points.Add(p1);
                            points.Add(p2);
                        }
                        else
                        {
                            var p1 = (xdiff > 0 ? antenna.xIndex + xdiff : antenna.xIndex + xdiff, ydiff > 0 ? antenna.yIndex + ydiff : antenna.yIndex + ydiff);
                            var p2 = (xdiff > 0 ? otherAntenna.xIndex - xdiff : otherAntenna.xIndex - xdiff, ydiff > 0 ? otherAntenna.yIndex - ydiff : otherAntenna.yIndex - ydiff);
                            points.Add(p1);
                            points.Add(p2);
                        }
                    }
                }
            }
            return points.Distinct().Where(p => p.x >= 0 && p.y >= 0 && p.x < lines.First().Length && p.y < lines.Length).Count();
        }
    }
}
