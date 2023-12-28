var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

var mapOfMaps = new Dictionary<string, Dictionary<RangeBase<long>, RangeBase<long>>>();
var mapToMap = new Dictionary<string, string>();
var map = new Dictionary<RangeBase<long>, RangeBase<long>>();
for (var i = 1; i < lines.Length; i++)
{
    var line = lines[i];
    
    if (string.IsNullOrWhiteSpace(line)) continue;
    
    if (char.IsLetter(line[0]))
    {
        var mapNames = line.Split("-to-").Select(name => name.Replace(" map:", string.Empty).Trim());
        mapToMap.Add(mapNames.First(), mapNames.Last());
        map = new Dictionary<RangeBase<long>, RangeBase<long>>();
        mapOfMaps.Add(mapNames.First(), map);
    }

    if (char.IsDigit(line[0]))
    {
        var rangeNumbers = line.Split(' ');
        var destinationStartRange = long.Parse(rangeNumbers[0]);
        var sourceStartRange = long.Parse(rangeNumbers[1]);
        var rangeLength = long.Parse(rangeNumbers[2]);
        map.Add(new RangeBase<long>(sourceStartRange, sourceStartRange + rangeLength - 1), new RangeBase<long>(destinationStartRange, destinationStartRange + rangeLength - 1));
    }
}

var seeds = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);
var lowestLocation = long.MaxValue;
foreach (var seed in seeds)
{
    var destination = "seed";
    var indexToFind = seed;
    while (destination != null)
    {
        if (mapOfMaps[destination].Any(kv => kv.Key.Contains(indexToFind)))
        {
            var sourceToDestinationMap = mapOfMaps[destination].First(kv => kv.Key.Contains(indexToFind));
            var newIndexToFind = sourceToDestinationMap.Value.Start + indexToFind - sourceToDestinationMap.Key.Start;
            Console.WriteLine($"{indexToFind} ({sourceToDestinationMap.Key.Start}, {sourceToDestinationMap.Key.End}) ({sourceToDestinationMap.Value.Start}, {sourceToDestinationMap.Value.End}) {newIndexToFind}");
            indexToFind = newIndexToFind;
        }
        Console.WriteLine($"{indexToFind} (,) (,) {indexToFind}");
        destination = mapToMap[destination];
        if (!mapOfMaps.ContainsKey(destination)) break;
    }

    if (indexToFind < lowestLocation)
    {
        lowestLocation = indexToFind;
    }
    Console.WriteLine();
}
Console.WriteLine(lowestLocation);


public class RangeBase<T> where T : struct, IComparable
{
    public T Start { get; }
    public T End { get; }

    public RangeBase(T start, T end)
    {
        Start = start;
        End = end;
    }

    public bool Contains(T other)
    {
        return Start.CompareTo(other) <= 0 && End.CompareTo(other) >= 0;
    }
}