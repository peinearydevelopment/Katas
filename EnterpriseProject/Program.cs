using System;
using EnterpriseProject.Contracts;

namespace EnterpriseProject
{
    class Program
    {
        static void Main(string[] args)
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
            Console.Write("Enter Graph Upper Right Coordinate: ");
            var graphExtremityCoordinates = Console.ReadLine();
            var parser = new SimpleCoordinateParser();
            var area = new SimpleRectangularArea(parser, graphExtremityCoordinates);
            var navigationalCoordinator = new NavigationCoordinator<int>(area);

            Console.Write("Rover 1 Starting Position: ");
            var rover1StartingPosition = Console.ReadLine();
            var rover1 = new SimpleRover(parser, rover1StartingPosition);
            navigationalCoordinator.TryAddObject(rover1);
            Console.Write("Rover 1 Movement Plan: ");
            var rover1MovementPlan = Console.ReadLine();
            navigationalCoordinator.Navigate(rover1, rover1MovementPlan);
            Console.WriteLine($"{rover1.X} {rover1.Y} {rover1.Bearing.TranslateBearing()}");

            Console.Write("Rover 2 Starting Position: ");
            var rover2StartingPosition = Console.ReadLine();
            var rover2 = new SimpleRover(parser, rover2StartingPosition);
            navigationalCoordinator.TryAddObject(rover2);
            Console.Write("Rover 2 Movement Plan: ");
            var rover2MovementPlan = Console.ReadLine();
            navigationalCoordinator.Navigate(rover2, rover2MovementPlan);
            Console.WriteLine($"{rover2.X} {rover2.Y} {rover2.Bearing.TranslateBearing()}");
        }
    }
}
