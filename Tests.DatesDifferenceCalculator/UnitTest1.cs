using CodeKatas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CodeKatas
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var (Days, Hours, Minutes) = new DatesDifferenceCalculator().CalculateDifference("01/01/2018", "01/01/2018");
            Assert.AreEqual(0, Days);
            Assert.AreEqual(0, Hours);
            Assert.AreEqual(0, Minutes);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var (Days, Hours, Minutes) = new DatesDifferenceCalculator().CalculateDifference("01/01/2018", "01/02/2018");
            Assert.AreEqual(1, Days);
            Assert.AreEqual(0, Hours);
            Assert.AreEqual(0, Minutes);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var (Days, Hours, Minutes) = new DatesDifferenceCalculator().CalculateDifference("01/02/2018", "01/01/2018");
            Assert.AreEqual(1, Days);
            Assert.AreEqual(0, Hours);
            Assert.AreEqual(0, Minutes);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.IsFalse(new DatesDifferenceCalculator().IsValidUserInput(""));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.IsFalse(new DatesDifferenceCalculator().IsValidUserInput("\r"));
        }

        [TestMethod]
        public void TestMethod6()
        {
            Assert.IsFalse(new DatesDifferenceCalculator().IsValidUserInput("\n"));
        }

        [TestMethod]
        public void TestMethod7()
        {
            Assert.IsFalse(new DatesDifferenceCalculator().IsValidUserInput("\t"));
        }

        [TestMethod]
        public void TestMethod8()
        {
            Assert.IsFalse(new DatesDifferenceCalculator().IsValidUserInput("01012018"));
        }

        [TestMethod]
        public void TestMethod9()
        {
            Assert.IsTrue(new DatesDifferenceCalculator().IsValidUserInput("01/01/2018"));
        }
    }
}
