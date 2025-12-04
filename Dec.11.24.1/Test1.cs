using System.Reflection;

namespace Dec._11._24._1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"125 17";

            var output = Helper(input);

            Assert.AreEqual(55312, output);
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
            var iterations = 25;
            var ll = new LinkedList<long>();
            foreach (var i in input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
            {
                ll.AddLast(i);
            }

            for (int i = 0; i < iterations; i++)
            {
                for (var item = ll.First; item != null; item = item.Next)
                {
                    if (item.Value == 0)
                    {
                        item.Value = 1;
                    }
                    else if (long.IsEvenInteger(item.Value.ToString().Length))
                    {
                        var strVal = item.Value.ToString();
                        var middle = strVal.Length / 2;
                        item.Value = long.Parse(strVal[..middle]);
                        var newNode = ll.AddAfter(item, long.Parse(strVal[middle..]));
                        item = newNode;
                    }
                    else
                    {
                        item.Value *= 2024;
                    }
                }
            }

            return ll.Count;
        }
    }
}
