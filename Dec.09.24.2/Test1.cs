using System.Reflection;

namespace Dec._09._24._2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"2333133121414131402";

            var total = Helper(input);

            Assert.AreEqual(2858, total);
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

            var number = int.MaxValue;
            var countOfNumbers = 0;
            var numberIndexStart = int.MaxValue;
            for (var i = state.Length - 1; i >= 0; i--)
            {
                var tmp = state[i];
                if (number == int.MaxValue && state[i] != int.MinValue)
                {
                    number = state[i];
                    numberIndexStart = i;
                    countOfNumbers++;
                }
                else if (number == state[i])
                {
                    numberIndexStart = i;
                    countOfNumbers++;
                }
                else
                {
                    var emptyIndexStart = int.MinValue;
                    var emptyCount = 0;
                    for (var j = 0; j < i; j++)
                    {
                        if (state[j] == int.MinValue && emptyCount == 0)
                        {
                            emptyIndexStart = j;
                            emptyCount++;
                        }
                        else if (state[j] == int.MinValue)
                        {
                            emptyCount++;
                        }
                        else
                        {
                            if (emptyCount >= countOfNumbers)
                            {
                                for (var k = countOfNumbers; k > 0; k--)
                                {
                                    state[emptyIndexStart] = number;
                                    state[numberIndexStart] = int.MinValue;
                                    emptyIndexStart++;
                                    numberIndexStart++;
                                }
                                emptyIndexStart = int.MinValue;
                                emptyCount = 0;
                                if (state[i] != int.MinValue)
                                {
                                    number = state[i];
                                    numberIndexStart = i;
                                    countOfNumbers = 1;
                                }
                                else
                                {
                                    number = int.MaxValue;
                                    countOfNumbers = 0;
                                    numberIndexStart = int.MaxValue;
                                }
                                break;
                            }

                            emptyIndexStart = int.MinValue;
                            emptyCount = 0;
                        }

                        if (j + 1 == i)
                        {
                            if (state[i] != int.MinValue)
                            {
                                number = state[i];
                                numberIndexStart = i;
                                countOfNumbers = 1;
                            }
                            else
                            {
                                number = int.MaxValue;
                                countOfNumbers = 0;
                                numberIndexStart = int.MaxValue;
                            }
                        }
                    }
                }
            }

            return state.Select((c, i) => c == int.MinValue ? 0 : (long)(c * i)).Sum();
        }
    }
}
