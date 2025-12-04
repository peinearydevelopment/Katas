using System.Reflection;

namespace Dec._05._24._1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47";

            var (CorrectlyOrderedLineCount, MiddlePageSum) = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(3, CorrectlyOrderedLineCount);
            Assert.AreEqual(143, MiddlePageSum);
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

        private (int CorrectlyOrderedLineCount, int MiddlePageSum) Helper(string[] input)
        {
            var orderings = input
                                .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
                                .Select(line =>
                                {
                                    var parts = line.Split('|');
                                    return new { Before = int.Parse(parts[0]), After = int.Parse(parts[1]) };
                                })
                                .ToLookup(item => item.Before, item => item.After);

            var goodLines = input
                                .SkipWhile(line => !string.IsNullOrWhiteSpace(line))
                                .Skip(1)
                                .Select(line => line.Split(',').Select(int.Parse).ToArray())
                                .Where(pages => pages.All(beforePage => orderings[beforePage].All(afterPage => Array.IndexOf(pages, afterPage) == -1 || Array.IndexOf(pages, afterPage) > Array.IndexOf(pages, beforePage))))
                                .ToArray();
            var total = goodLines.Select(line => line[line.Length / 2]).Sum();
            return (goodLines.Length, total);
        }
    }
}
