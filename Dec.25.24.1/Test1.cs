using System.Reflection;

namespace Dec._25._24._1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            foreach (var line in File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "input.txt")))
            {
                Console.WriteLine(line);
            }

            var output = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "output.txt");
            if (File.Exists(output))
            {
                File.Delete(output);
            }
            File.WriteAllText(output, "");
        }
    }
}
