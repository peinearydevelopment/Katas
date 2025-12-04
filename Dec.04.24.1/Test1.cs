using System.Reflection;
using System.Text.RegularExpressions;

namespace Dec._04._24._1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";

            var total = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(18, total);
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
            var count = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var l = input[i];
                for (var j = 0; j < l.Length; j++)
                {
                    var c = l[j];
                    if (c != 'X') continue;

                    // -->
                    if (j < l.Length - 3)
                    {
                        if (l[j + 1] == 'M' && l[j + 2] == 'A' && l[j + 3] == 'S')
                        {
                            count++;
                        }
                    }

                    // <--
                    if (j > 2)
                    {
                        if (l[j - 1] == 'M' && l[j - 2] == 'A' && l[j - 3] == 'S')
                        {
                            count++;
                        }
                    }

                    // ^
                    // |
                    if (i > 2)
                    {
                        if (input[i - 1][j] == 'M' && input[i - 2][j] == 'A' && input[i - 3][j] == 'S')
                        {
                            count++;
                        }
                    }

                    // |
                    // v
                    if (i < input.Length - 3)
                    {
                            if (input[i + 1][j] == 'M' && input[i + 2][j] == 'A' && input[i + 3][j] == 'S')
                        {
                            count++;
                        }
                    }

                    //  ◹
                    // /
                    if (j < l.Length - 3 && i > 2)
                    {
                        if (input[i - 1][j + 1] == 'M' && input[i - 2][j + 2] == 'A' && input[i - 3][j + 3] == 'S')
                        {
                            count++;
                        }
                    }

                    // \
                    //  ◿
                    if (j < l.Length - 3 && i < input.Length - 3)
                    {
                        if (input[i + 1][j + 1] == 'M' && input[i + 2][j + 2] == 'A' && input[i + 3][j + 3] == 'S')
                        {
                            count++;
                        }
                    }

                    //  /
                    // ◺
                    if (j > 2 && i < input.Length - 3)
                    {
                        if (input[i + 1][j - 1] == 'M' && input[i + 2][j - 2] == 'A' && input[i + 3][j - 3] == 'S')
                        {
                            count++;
                        }
                    }

                    // ◸
                    //  \
                    if (j > 2 && i > 2)
                    {
                        if (input[i - 1][j - 1] == 'M' && input[i - 2][j - 2] == 'A' && input[i - 3][j - 3] == 'S')
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}
