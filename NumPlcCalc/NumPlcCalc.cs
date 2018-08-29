using CodeKatas;
using System;
using System.Collections.Generic;

namespace DavidEdelstein
{
    public class NumPlcCalc : INumbersPositionsSumsAreEqual
    {
        public bool IsValidUserInput(string input) => throw new NotImplementedException();

        public bool NumberPositionSumsAreEqual(string input1, string input2)
        {
            return Convert.ToBoolean(NumberPlacesEqual(SeperateNumberPlaces(Convert.ToInt32(input1)), SeperateNumberPlaces(Convert.ToInt32(input2))));
        }

        /* https://github.com/DavidEdelstein/GC-LAB1-1/blob/master/Program.cs */
        //Code block #1 Get the users input--
        public static Tuple<int, int> GetUserInput()
        {
            // get first number from user--
            Console.WriteLine("Enter Number 1:");
            int number1 = Convert.ToInt32(Console.ReadLine());

            // get second number from user--
            Console.WriteLine("Enter Number 2:");
            int number2 = Convert.ToInt32(Console.ReadLine());

            //Return is to return the method result to the console--
            return new Tuple<int, int>(number1, number2);

        }

        // Code block #2 Seperate input number places (eg; 1's, 10's, 100's)

        public static List<int> SeperateNumberPlaces(int num)
        {
            // Create a list that holds the functions to be able to seperate the coresponding number places--
            List<int> listnum1 = new List<int>();

            // While loop needed to seperate more than single digit numbers.
            // Loop has to solve the equations until the int 0, num>0--
            while (num > 0)
            {
                listnum1.Add(num % 10);
                num = num / 10;
            }
            return listnum1;
        }

        /* Code block #3 Take each individual numbers that were seperated from the while loop and add them to 
           their coressponding number places and if the sum of all numbers equal then program will print out 
           True if any one of the sums do not equal then program will print false.*/

        public static string NumberPlacesEqual(List<int> listnum1, List<int> listnum2)
        {
            // Employ an array
            String result = "";
            for (int i = 0; i < listnum1.Count - 1; i++)
            {
                //if else will set up the result if the conditions are equal or not--
                if (listnum1[i] + listnum2[i] == listnum1[i + 1] + listnum2[i + 1] && result != "False")
                {
                    result = "True";
                }
                else
                {
                    result = "False";
                }
            }
            return result;
        }
    }
}
