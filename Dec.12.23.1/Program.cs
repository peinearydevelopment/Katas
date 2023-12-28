//var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

var lines = @"???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1".Split(Environment.NewLine);

var sets = lines.Select(line =>
{
    var segments = line.Split(' ');
    return new
    {
        PossibleLocations = segments[0].Split('.', StringSplitOptions.RemoveEmptyEntries),
        SegmentLengths = segments[1].Split(',').Select(s => int.Parse(s.Trim()))
    };
});

Console.WriteLine();