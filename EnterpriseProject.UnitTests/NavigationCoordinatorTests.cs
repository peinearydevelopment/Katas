using EnterpriseProject.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseProject.UnitTests
{
    [TestClass]
    public class NavigationCoordinatorTests
    {
        [TestMethod]
        public void Given_ANavigationCoordinator_When_NavigateIsGivenTestScenarioInputs_Then_TestScenarioOutputsShouldResult()
        {
            /*
                Enter Graph Upper Right Coordinate: 5 5
                Rover 1 Starting Position: 1 2 N
                Rover 1 Movement Plan: LMLMLMLMM
                Rover 1 Output: 1 3 N
                Rover 2 Starting Position: 3 3 E
                Rover 2 Movement Plan: MMRMMRMRRM
                Rover 2 Output: 5 1 E
             */
            var graphExtremityCoordinates = "5 5";
            var parser = new SimpleCoordinateParser();
            var area = new SimpleRectangularArea(parser, graphExtremityCoordinates);
            var navigationalCoordinator = new NavigationCoordinator<int>(area);

            var rover1StartingPosition = "1 2 N";
            var rover1 = new SimpleRover(parser, rover1StartingPosition);
            navigationalCoordinator.TryAddObject(rover1);

            var rover1MovementPlan = "LMLMLMLMM";
            navigationalCoordinator.Navigate(rover1, rover1MovementPlan);

            Assert.AreEqual("1 3 N", $"{rover1.X} {rover1.Y} {rover1.Bearing.TranslateBearing()}");

            var rover2StartingPosition = "3 3 E";
            var rover2 = new SimpleRover(parser, rover2StartingPosition);
            var rover2MovementPlan = "MMRMMRMRRM";
            navigationalCoordinator.Navigate(rover2, rover2MovementPlan);
            Assert.AreEqual("5 1 E", $"{rover2.X} {rover2.Y} {rover2.Bearing.TranslateBearing()}");
        }

        [TestMethod]
        public void Given_ANavigationCoordinator_When_NavigateIsGivenDirectionsToMoveWestOutsideOfGivenArea_Then_TheRoverShouldNotMove()
        {
            var graphExtremityCoordinates = "5 5";
            var parser = new SimpleCoordinateParser();
            var area = new SimpleRectangularArea(parser, graphExtremityCoordinates);
            var navigationalCoordinator = new NavigationCoordinator<int>(area);

            var rover1StartingPosition = "0 0 W";
            var rover1 = new SimpleRover(parser, rover1StartingPosition);
            navigationalCoordinator.TryAddObject(rover1);

            var rover1MovementPlan = "M";
            navigationalCoordinator.Navigate(rover1, rover1MovementPlan);

            Assert.AreEqual("0 0 W", $"{rover1.X} {rover1.Y} {rover1.Bearing.TranslateBearing()}");
        }

        [TestMethod]
        public void Given_ANavigationCoordinator_When_NavigateIsGivenDirectionsToMoveSouthOutsideOfGivenArea_Then_TheRoverShouldNotMove()
        {
            var graphExtremityCoordinates = "5 5";
            var parser = new SimpleCoordinateParser();
            var area = new SimpleRectangularArea(parser, graphExtremityCoordinates);
            var navigationalCoordinator = new NavigationCoordinator<int>(area);

            var rover1StartingPosition = "0 0 S";
            var rover1 = new SimpleRover(parser, rover1StartingPosition);
            navigationalCoordinator.TryAddObject(rover1);

            var rover1MovementPlan = "M";
            navigationalCoordinator.Navigate(rover1, rover1MovementPlan);

            Assert.AreEqual("0 0 S", $"{rover1.X} {rover1.Y} {rover1.Bearing.TranslateBearing()}");
        }

        [TestMethod]
        public void Given_ANavigationCoordinator_When_NavigateIsGivenDirectionsToMoveEastOutsideOfGivenArea_Then_TheRoverShouldNotMove()
        {
            var graphExtremityCoordinates = "5 5";
            var parser = new SimpleCoordinateParser();
            var area = new SimpleRectangularArea(parser, graphExtremityCoordinates);
            var navigationalCoordinator = new NavigationCoordinator<int>(area);

            var rover1StartingPosition = "5 5 E";
            var rover1 = new SimpleRover(parser, rover1StartingPosition);
            navigationalCoordinator.TryAddObject(rover1);

            var rover1MovementPlan = "M";
            navigationalCoordinator.Navigate(rover1, rover1MovementPlan);

            Assert.AreEqual("5 5 E", $"{rover1.X} {rover1.Y} {rover1.Bearing.TranslateBearing()}");
        }

        [TestMethod]
        public void Given_ANavigationCoordinator_When_NavigateIsGivenDirectionsToMoveNorthOutsideOfGivenArea_Then_TheRoverShouldNotMove()
        {
            var graphExtremityCoordinates = "5 5";
            var parser = new SimpleCoordinateParser();
            var area = new SimpleRectangularArea(parser, graphExtremityCoordinates);
            var navigationalCoordinator = new NavigationCoordinator<int>(area);

            var rover1StartingPosition = "5 5 N";
            var rover1 = new SimpleRover(parser, rover1StartingPosition);
            navigationalCoordinator.TryAddObject(rover1);

            var rover1MovementPlan = "M";
            navigationalCoordinator.Navigate(rover1, rover1MovementPlan);

            Assert.AreEqual("5 5 N", $"{rover1.X} {rover1.Y} {rover1.Bearing.TranslateBearing()}");
        }
    }
}
