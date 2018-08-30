using System;

namespace CodeKatas
{
    public interface IDatesDifferenceCalculator
    {
        bool IsValidUserInput(string input);
        (int Days, int Hours, int Minutes) CalculateDifference(string input1, string input2);
    }
}
