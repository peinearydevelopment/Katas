var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

//var lines = @"#.##..##.
//..#.##.#.
//##......#
//##......#
//..#.##.#.
//..##..##.
//#.#.##.#.

//#...##..#
//#....#..#
//..##..###
//#####.##.
//#####.##.
//..##..###
//#....#..#".Split(Environment.NewLine);
var sum = 0;
var pattern = new List<string>();
foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        sum += ProcessPattern([.. pattern]);
        pattern.Clear();
    }
    else
    {
        pattern.Add(line);
    }
}
sum += ProcessPattern([.. pattern]);
Console.WriteLine(sum);

int ProcessPattern(string[] rows)
{
    var columns = new string[rows[0].Length];
    for (var i = 0; i < rows.Length; i++)
    {
        for (var j = 0; j < columns.Length; j++)
        {
            columns[j] += rows[i][j];
        }
    }

    var inflectionIndex = GetReflectionPoint(rows);
    if (inflectionIndex != -1)
    {
        return 100 * (inflectionIndex + 1);
    }

    return GetReflectionPoint(columns) + 1;
}

int GetReflectionPoint(string[] pattern)
{
    for (var i = 0; i < pattern.Length - 1; i++)
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