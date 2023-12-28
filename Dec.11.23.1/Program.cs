var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

//var lines = @"...#......
//.......#..
//#.........
//..........
//......#...
//.#........
//.........#
//..........
//.......#..
//#...#.....".Split(Environment.NewLine);
var expandedRowInput = new List<char[]>();
IEnumerable<int> columnsWithGalaxies = Enumerable.Empty<int>();
foreach (var line in lines)
{
    if (line.Contains('#'))
    {
        expandedRowInput.Add(line.ToCharArray());
        columnsWithGalaxies = columnsWithGalaxies.Concat(line.Select((c, i) => new { IsGalaxy = c == '#', Index = i }).Where(l => l.IsGalaxy).Select(l => l.Index));
    }
    else
    {
        expandedRowInput.Add(line.ToCharArray());
        expandedRowInput.Add(line.ToCharArray());
    }
}

columnsWithGalaxies = columnsWithGalaxies.Distinct();
var expandedInput = expandedRowInput.Select(line => line.SelectMany((c, i) => !columnsWithGalaxies.Contains(i) ? ['.', '.'] : new[] { c }).ToArray()).ToArray();

var galaxyLocations = expandedInput.SelectMany((row, i) => row.Select((column, j) => new { ColumnValue = column, RowIndex = i, ColumnIndex = j })).Where(item => item.ColumnValue == '#').ToArray();

//foreach (var galaxyLocation in galaxyLocations.Select((gl, i) => new { Location = gl, Index = i }))
//{
//    expandedInput[galaxyLocation.Location.RowIndex][galaxyLocation.Location.ColumnIndex] = (char)(galaxyLocation.Index + 49);
//}

/*
 * x. 0,0       .x 1,0
 * .x 1,1       x. 0,1
 * length = 2   length = 2
 */
var sum = 0;
for (var i = 0; i < galaxyLocations.Length; i++)
{
    var thisGalaxy = galaxyLocations[i];
    for (var j = i + 1; j < galaxyLocations.Length; j++)
    {
        var otherGalaxy = galaxyLocations[j];
        sum += Math.Abs(otherGalaxy.ColumnIndex - thisGalaxy.ColumnIndex) + Math.Abs(otherGalaxy.RowIndex - thisGalaxy.RowIndex);
    }
}

Console.WriteLine(string.Join(Environment.NewLine, expandedInput.Select(i => string.Concat(i))));
Console.WriteLine(sum);