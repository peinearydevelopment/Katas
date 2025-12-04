using System.Reflection;
using System.Text.RegularExpressions;

namespace Dec._03._24._1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

            var total = Helper(input);

            Assert.AreEqual(161, total);
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
            return new Regex("mul\\(([0-9]{1,3}),([0-9]{1,3})\\)")
                        .Matches(input)
                        .Select(match => int.Parse(match.Groups[1].ValueSpan) * int.Parse(match.Groups[2].ValueSpan))
                        .Sum();
        }
    }
}
