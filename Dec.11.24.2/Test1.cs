using System.Reflection;

namespace Dec._11._24._2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"125 17";

            var output = Helper(input);

            Assert.AreEqual(0, output);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var total = Helper(File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "input.txt")));

            var output = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "output.txt");
            if (File.Exists(output))
            {
                File.Delete(output);
            }

            File.WriteAllText(output, total.ToString());
        }

        private long Helper(string input)
        {
            var iterations = 75;

            var d = new Dictionary<long, Dictionary<long, long>>();
            return input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => CountChildren(iterations, int.Parse(i), d)).Sum();
        }

        private long CountChildren(int iteration, long number, Dictionary<long, Dictionary<long, long>> lookup)
        {
            if (iteration == 0)
            {
                return 1;
            }
            else if (lookup.ContainsKey(number) && lookup[number].ContainsKey(iteration))
            {
                return lookup[number][iteration];
            }
            else if (number == 0)
            {
                var count = CountChildren(iteration - 1, 1, lookup);
                if (!lookup.ContainsKey(number))
                {
                    lookup.Add(number, new Dictionary<long, long>());
                }
                if (!lookup[number].ContainsKey(iteration))
                {
                    lookup[number].Add(iteration, count);
                }
                return count;
            }
            else if (long.IsEvenInteger(number.ToString().Length))
            {
                var strVal = number.ToString();
                var middle = strVal.Length / 2;
                var count = CountChildren(iteration - 1, long.Parse(strVal[..middle]), lookup) + CountChildren(iteration - 1, long.Parse(strVal[middle..]), lookup);
                if (!lookup.ContainsKey(number))
                {
                    lookup.Add(number, new Dictionary<long, long>());
                }
                if (!lookup[number].ContainsKey(iteration))
                {
                    lookup[number].Add(iteration, count);
                }
                return count;
            }
            else
            {
                var count = CountChildren(iteration - 1, number * 2024, lookup);
                if (!lookup.ContainsKey(number))
                {
                    lookup.Add(number, new Dictionary<long, long>());
                }
                if (!lookup[number].ContainsKey(iteration))
                {
                    lookup[number].Add(iteration, count);
                }
                return count;
            }
        }
    }
}
