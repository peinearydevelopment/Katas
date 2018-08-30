using System;

namespace CodeKatas
{
    public class DatesDifferenceCalculator : IDatesDifferenceCalculator
    {
        public (int Days, int Hours, int Minutes) CalculateDifference(string input1, string input2)
        {
            var date1 = DateTime.Parse(input1);
            var date2 = DateTime.Parse(input2);

            if (date1.Equals(date2))
            {
                return (0, 0, 0);
            }

            var timespan = date1 < date2 ? date2 - date1 : date1 - date2;
            return (timespan.Days, timespan.Hours, timespan.Minutes);
        }

        public bool IsValidUserInput(string input)
        {
            return DateTime.TryParse(input, out var date);
        }
    }
}
