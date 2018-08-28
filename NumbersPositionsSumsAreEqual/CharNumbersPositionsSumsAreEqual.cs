namespace CodeKatas
{
    public class CharNumbersPositionsSumsAreEqual : INumbersPositionsSumsAreEqual
    {
        public bool IsValidUserInput(string input)
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

        public bool NumberPositionSumsAreEqual(string input1, string input2)
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
