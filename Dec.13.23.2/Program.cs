var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

//var lines = @"##.....###...
//##.....###...
//##.##.####.##
//###..###.##..
//........#..#.
//#.#..#.#..##.
//##.##.###..#.".Split(Environment.NewLine);
var sum = 0;
var pattern = new List<string>();
foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        sum += FindNewReflectionPoint([.. pattern]);
        pattern.Clear();
    }
    else
    {
        pattern.Add(line);
    }
}
sum += FindNewReflectionPoint([.. pattern]);
Console.WriteLine(sum);

int FindNewReflectionPoint(string[] rows)
{
    var oldReflectionPoint = ProcessPattern(rows);
    for (var i = 0; i < rows.Length; i++)
    {
        var rowAsCharArray = rows[i].ToCharArray();
        for (var j = 0; j < rowAsCharArray.Length; j++)
        {
            rowAsCharArray[j] = rowAsCharArray[j] == '#' ? '.' : '#';

            var newPattern = new string[rows.Length];
            for (var k = 0; k < rows.Length; k++)
            {
                if (k != i)
                {
                    newPattern[k] = rows[k];
                }
                else
                {
                    newPattern[k] = string.Concat(rowAsCharArray);
                }
            }
            var value = ProcessPattern(newPattern);
            if (value == oldReflectionPoint)
            {
                value = ProcessPattern(newPattern, value >= 100 ? value / 100 : value, value >= 100);
            }
            if (value != -1 && value != oldReflectionPoint)
            {
                return value;
            }
            rowAsCharArray[j] = rowAsCharArray[j] == '#' ? '.' : '#';
        }
    }

    return oldReflectionPoint;
}

int ProcessPattern(string[] rows, int startingIndex = 0, bool previousMatchOnRow = false)
{
    var columns = new string[rows[0].Length];
    for (var i = 0; i < rows.Length; i++)
    {
        for (var j = 0; j < columns.Length; j++)
        {
            columns[j] += rows[i][j];
        }
    }

    var inflectionIndex = GetReflectionPoint(rows, previousMatchOnRow ? startingIndex : 0);
    if (inflectionIndex != -1)
    {
        return 100 * (inflectionIndex + 1);
    }

    inflectionIndex = GetReflectionPoint(columns, !previousMatchOnRow ? startingIndex : 0);
    return inflectionIndex == -1 ? inflectionIndex : inflectionIndex + 1;
}

int GetReflectionPoint(string[] pattern, int startingIndex)
{
    for (var i = startingIndex; i < pattern.Length - 1; i++)
    {
        if (pattern[i] == pattern[i + 1])
        {
            var inflectionIndex = i;

            var beforeReflectionIndex = i;
            var afterReflectionIndex = i + 1;
            while (beforeReflectionIndex >= 0 && afterReflectionIndex < pattern.Length && pattern[beforeReflectionIndex] == pattern[afterReflectionIndex])
            {
                beforeReflectionIndex--;
                afterReflectionIndex++;
            }

            if (beforeReflectionIndex < 0 || afterReflectionIndex >= pattern.Length)
            {
                return inflectionIndex;
            }
        }
    }

    return -1;
}