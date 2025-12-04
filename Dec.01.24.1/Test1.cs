using System.Reflection;

namespace Dec._01._24._1
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

            Assert.AreEqual(11, total);
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
            var list1 = new List<int>();
            var list2 = new List<int>();
            foreach (var line in lines)
            {
                var lineParts = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
                list1.Add(int.Parse(lineParts[0]));
                list2.Add(int.Parse(lineParts[1]));
            }

            list1.Sort();
            list2.Sort();

            var total = 0;
            for (var i = 0; i < list1.Count; i++)
            {
                total += Math.Abs(list1[i] - list2[i]);
            }

            return total;
        }
    }
}
