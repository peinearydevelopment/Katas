using System.Reflection;

namespace Dec._01._24._2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"3   4
4   3
2   5
1   3
3   9
3   3";

            var total = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(31, total);
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
            var list1 = new List<string>();
            var lookup = new Dictionary<string, int>();
            foreach (var line in lines)
            {
                var lineParts = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
                list1.Add(lineParts[0]);

                if (lookup.ContainsKey(lineParts[1]))
                {
                    lookup[lineParts[1]]++;
                }
                else
                {
                    lookup[lineParts[1]] = 1;
                }
            }

            return list1.Select(x => lookup.ContainsKey(x) ? (int.Parse(x) * lookup[x]) : 0).Sum();
        }
    }
}
