using System.Reflection;
using System.Text.RegularExpressions;

namespace Dec._03._24._2
{
    [TestClass]
    public sealed partial class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

            var total = Helper(input);

            Assert.AreEqual(48, total);
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

        private int Helper(string input)
        {
            var foo = "do()";
            var bar = new List<string>();
            foreach (var i in input)
            {
                foo += i;
                if (foo.StartsWith("do()") && foo.EndsWith("don't()"))
                {
                    bar.Add(foo);
                    foo = "don't()";
                }
                else if (foo.StartsWith("don't()") && foo.EndsWith("do()"))
                {
                    foo = "do()";
                }
            }
            bar.Add(foo);
            var mulGroup = InstructionRegex();
            var a = bar.SelectMany(match => mulGroup.Matches(match))
                        .Select(match => int.Parse(match.Groups[1].ValueSpan) * int.Parse(match.Groups[2].ValueSpan))
                        .Sum();

            var enabledGroups = EnabledInstructionsRegex();

            var g1 = enabledGroups.Matches(input).Select(m => m.Value).ToList();
            g1[0] = "do()" + g1.First();

            var x = bar.Except(g1).ToList();
            var b = enabledGroups
                        .Matches(input)
                        .SelectMany(match => mulGroup.Matches(match.Value))
                        .Select(match => int.Parse(match.Groups[1].ValueSpan) * int.Parse(match.Groups[2].ValueSpan))
                        .Sum();
            var c = a == b;


            return bar.SelectMany(match => mulGroup.Matches(match))
                        .Select(match => int.Parse(match.Groups[1].ValueSpan) * int.Parse(match.Groups[2].ValueSpan))
                        .Sum();
        }

        [GeneratedRegex("((^.*?(don't\\(\\)))|(do\\(\\).*?don't\\(\\))|(do\\(\\).*?$))", RegexOptions.Singleline)]
        private static partial Regex EnabledInstructionsRegex();

        [GeneratedRegex("mul\\(([0-9]{1,3}),([0-9]{1,3})\\)")]
        private static partial Regex InstructionRegex();
    }
}
