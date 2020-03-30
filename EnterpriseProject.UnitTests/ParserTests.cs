using EnterpriseProject.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EnterpriseProject.UnitTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Given_ASimpleParser_When_ParseCoordinatesRecievesCoordinatesInCorrectFormat_Then_CoordinatesShouldBeParsed()
        {
            var location = new SimpleCoordinateParser().ParseCoordinates("0 0");
            Assert.AreEqual(0, location.X);
            Assert.AreEqual(0, location.Y);
        }

        [TestMethod]
        public void Given_ASimpleParser_When_ParseCoordinatesRecievesCoordinatesWithExtraSpaceAtEnd_Then_ArgumentExceptionShoulBeThrow()
        {
            Assert.ThrowsException<ArgumentException>(() => new SimpleCoordinateParser().ParseCoordinates("5 3 "));
        }

        [TestMethod]
        public void Given_ASimpleParser_When_ParseCoordinatesRecievesCoordinatesWithExtraSpaceAtBeginning_Then_ArgumentExceptionShoulBeThrow()
        {
            Assert.ThrowsException<ArgumentException>(() => new SimpleCoordinateParser().ParseCoordinates(" 5 3"));
        }

        [TestMethod]
        public void Given_ASimpleParser_When_ParseCoordinatesAndBearingRecievesCoordinatesInCorrectFormat_Then_CoordinatesAndBearingShouldBeParsed()
        {
            var (location, bearing) = new SimpleCoordinateParser().ParseCoordinatesAndBearing("5 3 N");
            Assert.AreEqual(5, location.X);
            Assert.AreEqual(3, location.Y);
            Assert.AreEqual(90, bearing);
        }

        [TestMethod]
        public void Given_ASimpleParser_When_ParseCoordinatesAndBearingRecievesCoordinatesWithExtraSpaceAtEnd_Then_ArgumentExceptionShoulBeThrow()
        {
            Assert.ThrowsException<ArgumentException>(() => new SimpleCoordinateParser().ParseCoordinatesAndBearing("5 3 N "));
        }
    }
}
