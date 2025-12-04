using System.Reflection;

namespace Dec._07._24._2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20";

            var total = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(11387, total);
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

        private long Helper(string[] lines)
        {
            long total = 0;
            foreach (var line in lines)
            {
                var lineParts = line.Split(':');
                var checkSum = long.Parse(lineParts[0]);
                var numbers = lineParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                var possibleCalibrationResults = GetPossibleValues(numbers.Reverse().ToArray(), ["+", "*", "||"]).ToArray();
                if (possibleCalibrationResults.Any(result => result == checkSum))
                {
                    total += checkSum;
                }
            }

            return total;
        }

        private IEnumerable<long> GetPossibleValues(long[] numbers, string[] operators)
        {
            if (numbers.Length == 1)
            {
                yield return numbers[0];
            }

            if (numbers.Length > 1)
            {
                foreach (var newSetOfNumbers in GetPossibleValues(numbers.Skip(1).ToArray(), operators))
                {
                    foreach (var @operator in operators)
                    {
                        yield return @operator switch
                        {
                            "+" => (numbers.Length == 0 ? 0 : numbers[0]) + newSetOfNumbers,
                            "*" => (numbers.Length == 0 ? 1 : numbers[0]) * newSetOfNumbers,
                            "||" => numbers.Length == 0 ? newSetOfNumbers : long.Parse($"{newSetOfNumbers}{numbers[0]}"),
                            _ => throw new NotImplementedException()
                        };
                    }
                }
            }
        }
    }
}
