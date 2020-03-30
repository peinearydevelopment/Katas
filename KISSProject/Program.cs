using System;

namespace KISSProject
{
    public static class Program
    {
        public static void Main(string[] args)
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
            var navigationalArea = new NavigationalArea(graphExtremityCoordinates);

            Console.Write("Rover 1 Starting Position: ");
            var rover1StartingPosition = Console.ReadLine();
            var rover1 = new Rover(navigationalArea, rover1StartingPosition);
            Console.Write("Rover 1 Movement Plan: ");
            var rover1MovementPlan = Console.ReadLine();
            var rover1FinalPosition = rover1.Navigate(rover1MovementPlan);
            Console.WriteLine(rover1FinalPosition);

            Console.Write("Rover 2 Starting Position: ");
            var rover2StartingPosition = Console.ReadLine();
            var rover2 = new Rover(navigationalArea, rover2StartingPosition);
            Console.Write("Rover 2 Movement Plan: ");
            var rover2MovementPlan = Console.ReadLine();
            var rover2FinalPosition = rover2.Navigate(rover2MovementPlan);
            Console.WriteLine(rover2FinalPosition);
        }
    }
}
