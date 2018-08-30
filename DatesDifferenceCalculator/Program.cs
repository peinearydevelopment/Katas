using System;

namespace CodeKatas
{
    public class Program
    {
        private static IDatesDifferenceCalculator DatesDifferenceCalculator { get; set; }

        static void Main(string[] args)
        {
            DatesDifferenceCalculator = new DatesDifferenceCalculator();
            var input1 = GetUserInput();
            var input2 = GetUserInput();

            var (Days, Hours, Minutes) = DatesDifferenceCalculator.CalculateDifference(input1, input2);
            Console.WriteLine($"There are {Days} days, {Hours} hours and {Minutes} minutes between the dates entered.");

            Console.ReadLine();
        }

        static string GetUserInput()
        {
            Console.Write("Enter date: ");

            string input = string.Empty;

            while (string.IsNullOrWhiteSpace(input))
            {
                input = Console.ReadLine();

                if (input.Length == 0)
                {
                    Console.Write("Enter date: ");
                }

                if (!DatesDifferenceCalculator.IsValidUserInput(input))
                {
                    Console.Write("Invalid input.");
                    Console.Write("Enter date: ");
                    input = string.Empty;
                }
            }

            return input;
        }


    }
}
