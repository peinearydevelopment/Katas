using System;

namespace CodeKatas
{
    public class Program
    {
        private static INumbersPositionsSumsAreEqual NumberPositionsSumsAreEqual { get; set; }

        static void Main(string[] args)
        {
            NumberPositionsSumsAreEqual = new CharNumbersPositionsSumsAreEqual();
            //NumberPositionsSumsAreEqual = new IntNumbersPositionsSumsAreEqual();
            var input1 = GetUserInput();
            var input2 = GetUserInput();

            Console.WriteLine(NumberPositionsSumsAreEqual.NumberPositionSumsAreEqual(input1, input2).ToString());

            Console.ReadLine();
        }

        static string GetUserInput()
        {
            Console.Write("Enter number: ");

            string input = string.Empty;

            while (string.IsNullOrWhiteSpace(input))
            {
                input = Console.ReadLine();

                if (input.Length == 0)
                {
                    Console.Write("Enter number: ");
                }

                if (!NumberPositionsSumsAreEqual.IsValidUserInput(input))
                {
                    Console.Write("Input can only contain 0-9");
                    Console.Write("Enter number: ");
                    input = string.Empty;
                }
            }

            return input;
        }


    }
}
