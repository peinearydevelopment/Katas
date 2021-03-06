using DavidEdelstein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.NumbersPositionsSumsAreEqual
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(new NumPlcCalc().NumberPositionSumsAreEqual("1", "2"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsFalse(new NumPlcCalc().NumberPositionSumsAreEqual("1", "12"));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.IsTrue(new NumPlcCalc().NumberPositionSumsAreEqual("11", "11"));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.IsFalse(new NumPlcCalc().NumberPositionSumsAreEqual("12", "11"));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.IsFalse(new NumPlcCalc().IsValidUserInput(""));
        }

        [TestMethod]
        public void TestMethod6()
        {
            Assert.IsFalse(new NumPlcCalc().IsValidUserInput("\r"));
        }

        [TestMethod]
        public void TestMethod7()
        {
            Assert.IsFalse(new NumPlcCalc().IsValidUserInput("\n"));
        }

        [TestMethod]
        public void TestMethod8()
        {
            Assert.IsFalse(new NumPlcCalc().IsValidUserInput("\t"));
        }

        [TestMethod]
        public void TestMethod9()
        {
            Assert.IsFalse(new NumPlcCalc().IsValidUserInput("123a"));
        }

        [TestMethod]
        public void TestMethod10()
        {
            Assert.IsTrue(new NumPlcCalc().IsValidUserInput("90"));
        }
    }
}
