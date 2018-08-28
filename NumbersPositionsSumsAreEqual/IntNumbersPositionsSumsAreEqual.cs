using System.Collections.Generic;
using System.Linq;

namespace CodeKatas
{
    public class IntNumbersPositionsSumsAreEqual : INumbersPositionsSumsAreEqual
    {
        public bool IsValidUserInput(string input)
        {
            return int.TryParse(input, out int a);
        }

        private IEnumerable<int> GetSums(string input1, string input2)
        {
            var int1 = int.Parse(input1);
            var int2 = int.Parse(input2);

            while (int1 > 0 || int2 > 0)
            {
                yield return (int1 % 10) + (int2 % 10);
                int1 /= 10;
                int2 /= 10;
            }
        }

        public bool NumberPositionSumsAreEqual(string input1, string input2)
        {
            var positionSums = GetSums(input1, input2);
            return positionSums.Distinct().Count() == 1;
        }
    }
}
