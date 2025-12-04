using System.Reflection;

namespace Dec._02._24._1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";

            var total = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(2, total);
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
                var previousLevelDifference = 0;
                for (int i = 0; i < lineParts.Length - 1; i++)
                {
                    var levelDifference = lineParts[i] - lineParts[i + 1];
                    if (Math.Abs(levelDifference) < 1 || Math.Abs(levelDifference) > 3)
                    {
                        break;
                    }
                    
                    if (i == 0)
                    {
                        previousLevelDifference = levelDifference;
                    }
                    else if ((previousLevelDifference > 0 && levelDifference < 0) || (previousLevelDifference < 0 && levelDifference > 0))
                    {
                        break;
                    }
                    else if (i == lineParts.Length - 2)
                    {
                        total++;
                    }
                }
            }

            return total;
        }
    }
}
