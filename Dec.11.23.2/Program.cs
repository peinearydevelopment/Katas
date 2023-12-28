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
var columnsWithGalaxies = Enumerable.Empty<int>();

// 3,7
var rowsWithoutGalaxies = Enumerable.Empty<int>();
foreach (var line in lines.Select((line, i) => new { Row = line, Index = i }))
{
    if (line.Row.Contains('#'))
    {
        columnsWithGalaxies = columnsWithGalaxies.Concat(line.Row.Select((c, i) => new { IsGalaxy = c == '#', Index = i }).Where(l => l.IsGalaxy).Select(l => l.Index));
    }
    else
    {
        rowsWithoutGalaxies = rowsWithoutGalaxies.Append(line.Index);
    }
}

columnsWithGalaxies = columnsWithGalaxies.Distinct();
// 2,5,8
var columnsWithoutGalaxies = lines[0].Select((c, i) => i).Where((c, i) => !columnsWithGalaxies.Contains(i));

var galaxyLocations = lines.SelectMany((row, i) => row.Select((column, j) => new { ColumnValue = column, RowIndex = i, ColumnIndex = j })).Where(item => item.ColumnValue == '#').ToArray();

var leapMultiplier = 1000000;
long sum = 0;
for (var i = 0; i < galaxyLocations.Length; i++)
{
    var thisGalaxy = galaxyLocations[i];
    for (var j = i + 1; j < galaxyLocations.Length; j++)
    {
        var otherGalaxy = galaxyLocations[j];

        var biggerColumn = otherGalaxy.ColumnIndex > thisGalaxy.ColumnIndex ? otherGalaxy.ColumnIndex : thisGalaxy.ColumnIndex;
        var smallerColumn = otherGalaxy.ColumnIndex < thisGalaxy.ColumnIndex ? otherGalaxy.ColumnIndex : thisGalaxy.ColumnIndex;

        var biggerRow = otherGalaxy.RowIndex > thisGalaxy.RowIndex ? otherGalaxy.RowIndex : thisGalaxy.RowIndex;
        var smallerRow = otherGalaxy.RowIndex < thisGalaxy.RowIndex ? otherGalaxy.RowIndex : thisGalaxy.RowIndex;

        var numColumnLeaps = columnsWithoutGalaxies.Where(cwg => cwg >= smallerColumn && cwg <= biggerColumn).Count();
        var numRowLeaps = rowsWithoutGalaxies.Where(rwg => rwg >= smallerRow && rwg <= biggerRow).Count();
        sum += (biggerColumn - smallerColumn - numColumnLeaps + (numColumnLeaps * leapMultiplier)) + (biggerRow - smallerRow - numRowLeaps + (numRowLeaps * leapMultiplier));
    }
}

Console.WriteLine(sum);