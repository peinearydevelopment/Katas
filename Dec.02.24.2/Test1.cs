using System.Reflection;

namespace Dec._02._24._2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input =
@"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
1 2 7 5 6
87 90 88 85 84 81 79
1 3 4 5 8 10 7
14 11 14 17 18 19";
            /*
             * safe
             * unsafe
             * unsafe
             * safe
             * safe
             * safe
             * safe
             * safe
             * safe
             * safe
             */

            var total = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(8, total);
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
            var total = 0;

            foreach (var line in lines)
            {
                var lineParts = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                total += TestLine(lineParts);
            }

            return total;
        }

        private int TestLine(int[] lineParts)
        {
            var dampenerLinesToTry = new List<int[]>();
            var previousLevelDifference = 0;
            for (int i = 0; i < lineParts.Length - 1; i++)
            {
                var levelDifference = lineParts[i] - lineParts[i + 1];
                if (Math.Abs(levelDifference) < 1 || Math.Abs(levelDifference) > 3)
                {
                    dampenerLinesToTry.Add(lineParts.Where((item, index) => index != i).ToArray());
                    dampenerLinesToTry.Add(lineParts.Where((item, index) => index != (i + 1)).ToArray());
                }
                else if (i == 0)
                {
                    previousLevelDifference = levelDifference;
                }
                else if ((previousLevelDifference > 0 && levelDifference < 0) || (previousLevelDifference < 0 && levelDifference > 0))
                {
                    if (i == 1)
                    {
                        dampenerLinesToTry.Add(lineParts.Skip(1).ToArray());
                    }
                    dampenerLinesToTry.Add(lineParts.Where((item, index) => index != i).ToArray());
                    dampenerLinesToTry.Add(lineParts.Where((item, index) => index != (i + 1)).ToArray());
                }
            }

            if (dampenerLinesToTry.Any())
            {
                var isOk = dampenerLinesToTry.Select(TestLine2).Any(x => x > 0);
                if (!isOk)
                {
                    Console.WriteLine(string.Join(' ', lineParts));
                    foreach (var l in dampenerLinesToTry)
                    {
                        Console.WriteLine(string.Join(' ', l));
                    }
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            Console.Write(string.Join(' ', lineParts));
            return 1;
        }

        private int TestLine2(int[] lineParts)
        {
            var dampenerLinesToTry = new List<int[]>();
            var previousLevelDifference = 0;
            for (int i = 0; i < lineParts.Length - 1; i++)
            {
                var levelDifference = lineParts[i] - lineParts[i + 1];
                if (Math.Abs(levelDifference) < 1 || Math.Abs(levelDifference) > 3)
                {
                    return 0;
                }
                else if (i == 0)
                {
                    previousLevelDifference = levelDifference;
                }
                else if ((previousLevelDifference > 0 && levelDifference < 0) || (previousLevelDifference < 0 && levelDifference > 0))
                {
                    return 0;
                }
                else if (i == lineParts.Length - 2)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
