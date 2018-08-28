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
            Assert.IsTrue(Program.NumberPositionSumsAreEqual("1", "2"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsFalse(Program.NumberPositionSumsAreEqual("1", "12"));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.IsTrue(Program.NumberPositionSumsAreEqual("11", "11"));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.IsFalse(Program.NumberPositionSumsAreEqual("12", "11"));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.IsFalse(Program.IsValidUserInput(""));
        }

        [TestMethod]
        public void TestMethod6()
        {
            Assert.IsFalse(Program.IsValidUserInput("\r"));
        }

        [TestMethod]
        public void TestMethod7()
        {
            Assert.IsFalse(Program.IsValidUserInput("\n"));
        }

        [TestMethod]
        public void TestMethod8()
        {
            Assert.IsFalse(Program.IsValidUserInput("\t"));
        }

        [TestMethod]
        public void TestMethod9()
        {
            Assert.IsFalse(Program.IsValidUserInput("123a"));
        }

        [TestMethod]
        public void TestMethod10()
        {
            Assert.IsTrue(Program.IsValidUserInput("90"));
        }
    }
}
