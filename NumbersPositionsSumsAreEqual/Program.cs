using System;

namespace CodeKatas
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input1 = GetUserInput();
            var input2 = GetUserInput();

            Console.WriteLine(NumberPositionSumsAreEqual(input1, input2).ToString());

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

                if (!IsValidUserInput(input))
                {
                    Console.Write("Input can only contain 0-9");
                    Console.Write("Enter number: ");
                    input = string.Empty;
                }
            }

            return input;
        }

        public static bool IsValidUserInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] < '0' || input[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }

        public static bool NumberPositionSumsAreEqual(string input1, string input2)
        {
            if (input1.Length != input2.Length)
            {
                return false;
            }

            for (var i = 1; i < input1.Length; i++)
            {
                if (input1[i - 1] + input2[i - 1] != input1[i] + input2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
