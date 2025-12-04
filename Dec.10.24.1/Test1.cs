using System.Reflection;

namespace Dec._10._24._1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732";

            var output = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(36, output);
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

        private int Helper(string[] input)
        {
            var map = input.Select(line => line.Select(c => c - 48).ToArray()).ToArray();

            var a = map.SelectMany((line, yIndex) => line.Select((marker, xIndex) => new { yIndex, xIndex, marker }).Where(m => m.marker == 0).Select(m => Helper2(map, m.xIndex, m.yIndex).Count())).Sum();
            return a;
        }

        private IEnumerable<(int x, int y)> Helper2(int[][] map, int currentX, int currentY)
        {
            IEnumerable<(int x, int y)> coordinatesOf9s = Enumerable.Empty<(int x, int y)>();

            var currentElevation = map[currentY][currentX];
            if (currentElevation == 9) { return coordinatesOf9s.Append((currentX, currentY)); }

            if (currentY - 1 >= 0 && map[currentY - 1][currentX] == currentElevation + 1)
            {
                foreach (var a in Helper2(map, currentX, currentY - 1))
                    coordinatesOf9s = coordinatesOf9s.Append(a);
            }

            if (currentX - 1 >= 0 && map[currentY][currentX - 1] == currentElevation + 1)
            {
                foreach (var a in Helper2(map, currentX - 1, currentY))
                    coordinatesOf9s = coordinatesOf9s.Append(a);
            }

            if (currentY + 1 < map.Length && map[currentY + 1][currentX] == currentElevation + 1)
            {
                foreach (var a in Helper2(map, currentX, currentY + 1))
                    coordinatesOf9s = coordinatesOf9s.Append(a);
            }

            if (currentX + 1 < map[0].Length && map[currentY][currentX + 1] == currentElevation + 1)
            {
                foreach (var a in Helper2(map, currentX + 1, currentY))
                    coordinatesOf9s = coordinatesOf9s.Append(a);
            }

            return coordinatesOf9s.Distinct();
        }
    }
}
