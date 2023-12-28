var input = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

//var input = @"O....#....
//O.OO#....#
//.....##...
//OO.#O....O
//.O.....O#.
//O.#..O.#.#
//..O..#O..O
//.......O..
//#....###..
//#OO..#....".Split(Environment.NewLine);

var lines = input.Select(line => line.ToCharArray())
                .ToArray();

for (var i = 1; i < lines.Length; i++)
{
    var line = lines[i];
    for (var j =  0; j < line.Length; j++)
    {
        var item = line[j];
        if (item == 'O')
        {
            var rollingRockLineIndex = i;
            while (rollingRockLineIndex > 0 && lines[rollingRockLineIndex - 1][j] == '.')
            {
                rollingRockLineIndex--;
            }

            lines[rollingRockLineIndex][j] = 'O';

            if (rollingRockLineIndex != i)
            {
                lines[i][j] = '.';
            }
        }
    }
}

Console.WriteLine(string.Join(Environment.NewLine, lines.Select(line => string.Concat(line))));

Console.WriteLine(lines.Select((line, i) => line.Count(c => c == 'O') * (lines.Length - i)).Sum());