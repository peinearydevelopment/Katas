using System.Reflection;

namespace Dec._04._24._2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"M S S M
 A   A 
M S S M
       
M M S S
 A   A 
S S M M";

            var total = Helper(input.Split(Environment.NewLine));

            Assert.AreEqual(4, total);
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
                    if (c != 'A') continue;
                    // 012

                    if (i > 0 && j > 0 && i < input.Length - 1 && j < l.Length - 1)
                    {
                        // M S
                        //  A
                        // M S
                        if (input[i - 1][j - 1] == 'M' && input[i + 1][j - 1] == 'M' && input[i - 1][j + 1] == 'S' && input[i + 1][j + 1] == 'S')
                        {
                            count++;
                        }
                        // S M
                        //  A
                        // S M
                        if (input[i - 1][j - 1] == 'S' && input[i + 1][j - 1] == 'S' && input[i - 1][j + 1] == 'M' && input[i + 1][j + 1] == 'M')
                        {
                            count++;
                        }
                        // M M
                        //  A
                        // S S
                        if (input[i - 1][j - 1] == 'M' && input[i - 1][j + 1] == 'M' && input[i + 1][j - 1] == 'S' && input[i + 1][j + 1] == 'S')
                        {
                            count++;
                        }
                        // S S
                        //  A
                        // M M
                        if (input[i + 1][j - 1] == 'M' && input[i + 1][j + 1] == 'M' && input[i - 1][j - 1] == 'S' && input[i - 1][j + 1] == 'S')
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
