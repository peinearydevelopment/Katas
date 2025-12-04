using System.Reflection;

namespace Dec._09._24._1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"2333133121414131402";

            var total = Helper(input);

            Assert.AreEqual(1928, total);
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

        private long Helper(string line)
        {
            var state = line.SelectMany((c, i) => int.IsOddInteger(i) ? Enumerable.Repeat(int.MinValue, c - 48) : Enumerable.Repeat(i == 0 ? 0 : (i / 2), c - 48)).ToArray();
            var leftPointerPosition = 0;
            var rightPointerPosition = state.Length - 1;
            while (leftPointerPosition != rightPointerPosition)
            {
                if (state[rightPointerPosition] == int.MinValue)
                {
                    rightPointerPosition--;
                }
                else if (state[leftPointerPosition] == int.MinValue)
                {
                    state[leftPointerPosition] = state[rightPointerPosition];
                    state[rightPointerPosition] = int.MinValue;
                    leftPointerPosition++;
                }
                else
                {
                    leftPointerPosition++;
                }
                
            }
            return state.Select((c, i) => c == int.MinValue ? 0 : (long)(c * i)).Sum();
        }
    }
}
