using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KISSProject.UnitTests
{
    /*  N
     * W E [5 0] [5 1] [5 2] [5 3] [5 4] [5 5]
     *  S  [4 0] [4 1] [4 2] [4 3] [4 4] [4 5]
     *     [3 0] [3 1] [3 2] [3 3] [3 4] [3 5]
     *     [2 0] [2 1] [2 2] [2 3] [2 4] [2 5]
     *     [1 0] [1 1] [1 2] [1 3] [1 4] [1 5]
     *     [0 0] [0 1] [0 2] [0 3] [0 4] [5 5]
     *     Rover1: N2,1* W2,1 W2,0 S2,0 S1,0 E1,0 E1,1 N1,1 N2,1 N3,1
     *     Rover2: E3,3* E3,4 E3,5 S3,5 S2,5 S1,5 W1,5 W1,4 N1,4 E1,4 E1,5
     */

    [TestClass]
    public class KISSProjectTests
    {
        [TestMethod]
        public void ValidateParseCoordinates()
        {
            var (X, Y) = CoordinateParser.ParseCoordinates("0 0");
            Assert.AreEqual(0, X);
            Assert.AreEqual(0, Y);
        }

        [TestMethod]
        public void ValidateParseCoordinatesAndForwardDirection()
        {
            var (X, Y, forwardPosition) = CoordinateParser.ParseCoordinatesAndForwardDirection("5 3 N");
            Assert.AreEqual(5, X);
            Assert.AreEqual(3, Y);
            Assert.AreEqual('N', forwardPosition);
        }

        [TestMethod]
        public void ValidateGivenTestScenarios()
        {
            var graphExtremityCoordinates = "5 5";
            var navigationalArea = new NavigationalArea(graphExtremityCoordinates);

            var rover1StartingPosition = "1 2 N";
            var rover1 = new Rover(navigationalArea, rover1StartingPosition);
            var rover1MovementPlan = "LMLMLMLMM";

            Assert.AreEqual("1 3 N", rover1.Navigate(rover1MovementPlan));

            var rover2StartingPosition = "3 3 E";
            var rover2 = new Rover(navigationalArea, rover2StartingPosition);
            var rover2MovementPlan = "MMRMMRMRRM";
            Assert.AreEqual("5 1 E", rover2.Navigate(rover2MovementPlan));
        }
    }
}
