using CodeKatas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.NumbersPositionsSumsAreEqual
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(new CharNumbersPositionsSumsAreEqual().NumberPositionSumsAreEqual("1", "2"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsFalse(new CharNumbersPositionsSumsAreEqual().NumberPositionSumsAreEqual("1", "12"));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.IsTrue(new CharNumbersPositionsSumsAreEqual().NumberPositionSumsAreEqual("11", "11"));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.IsFalse(new CharNumbersPositionsSumsAreEqual().NumberPositionSumsAreEqual("12", "11"));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.IsFalse(new CharNumbersPositionsSumsAreEqual().IsValidUserInput(""));
        }

        [TestMethod]
        public void TestMethod6()
        {
            Assert.IsFalse(new CharNumbersPositionsSumsAreEqual().IsValidUserInput("\r"));
        }

        [TestMethod]
        public void TestMethod7()
        {
            Assert.IsFalse(new CharNumbersPositionsSumsAreEqual().IsValidUserInput("\n"));
        }

        [TestMethod]
        public void TestMethod8()
        {
            Assert.IsFalse(new CharNumbersPositionsSumsAreEqual().IsValidUserInput("\t"));
        }

        [TestMethod]
        public void TestMethod9()
        {
            Assert.IsFalse(new CharNumbersPositionsSumsAreEqual().IsValidUserInput("123a"));
        }

        [TestMethod]
        public void TestMethod10()
        {
            Assert.IsTrue(new CharNumbersPositionsSumsAreEqual().IsValidUserInput("90"));
        }
    }
}
