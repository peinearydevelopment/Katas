var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

var ranges = ParseRanges(lines.Skip(2)).ToArray();

var maps = ranges[0];
for (var i = 1; i < ranges.Length; i++)
{
    foreach (var b in maps)
    {
        Console.WriteLine($"{b.Source.Begin}:{b.Source.End} {b.Target.Begin}:{b.Target.End}");
    }
    foreach (var b in ranges[i])
    {
        Console.WriteLine($"{b.Source.Begin}:{b.Source.End} {b.Target.Begin}:{b.Target.End}");
    }
    maps = MergeMapsClever(maps, ranges[i]);
}

var seedRanges = new List<ResourceRange>();
var parsedSeedNumbers = lines.First().Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => long.Parse(n.Trim())).ToArray();
for (var i = 0; i < parsedSeedNumbers.Length; i += 2)
{
    seedRanges.Add(new ResourceRange(parsedSeedNumbers[i], parsedSeedNumbers[i + 1]));
}

var lowest = long.MaxValue;
var mapsOrderedByTarget = maps.OrderBy(m => m.Target.Begin).ToArray();

foreach (var b in mapsOrderedByTarget)
{
    Console.WriteLine($"{b.Source.Begin}:{b.Source.End}");
}
foreach (var b in mapsOrderedByTarget)
{
    Console.WriteLine($"{b.Target.Begin}:{b.Target.End}");
}

for (var i = 0; i < seedRanges.Count(); i++)
{
    var seedRange = seedRanges[i];
    var lowestTargetRangeMap = mapsOrderedByTarget.FirstOrDefault(mpbt => mpbt.Source.Overlaps(seedRange));

    if (lowestTargetRangeMap != null)
    {
        if (lowestTargetRangeMap.Source.OverlapsBeginning(seedRange))
        {
            var a = lowestTargetRangeMap.GetCorrespondingTargetValue(seedRange.Begin);
            if (a < lowest)
            {
                lowest = a;
            }
        }

        if (lowestTargetRangeMap.Source.OverlapsEnd(seedRange))
        {
            var a = lowestTargetRangeMap.GetCorrespondingTargetValue(lowestTargetRangeMap.Source.Begin);
            if (a < lowest)
            {
                lowest = a;
            }
        }

        if (lowestTargetRangeMap.Source.Contains(seedRange))
        {
            var a = lowestTargetRangeMap.GetCorrespondingTargetValue(seedRange.Begin);
            if (a < lowest)
            {
                lowest = a;
            }
        }

        if (seedRange.Contains(lowestTargetRangeMap.Source))
        {
            var a = lowestTargetRangeMap.GetCorrespondingTargetValue(lowestTargetRangeMap.Source.Begin);
            if (a < lowest)
            {
                lowest = a;
            }
        }
    }
}

Console.WriteLine(lowest);



RangeMap[] MergeMapsClever(RangeMap[] sourceRangesToXTargetRanges, RangeMap[] xTargetRangesToYTargetRanges)
{
    var gaplessSourceRangesToXTargetRanges = new List<RangeMap>();
    var gaplessXTargetRangesToYTargetRanges = new List<RangeMap>();
    var mergedRanges = new List<RangeMap>();


    var indices = sourceRangesToXTargetRanges.Select(srtxtr => srtxtr.Target.Begin).Concat(xTargetRangesToYTargetRanges.Select(xtrtytr => xtrtytr.Source.Begin)).Distinct()
                    .Concat(sourceRangesToXTargetRanges.Select(srtxtr => srtxtr.Target.End).Concat(xTargetRangesToYTargetRanges.Select(xtrtytr => xtrtytr.Source.End)).Distinct())
                .Order()
                .Take(new Range(0, (sourceRangesToXTargetRanges.Count() * 2) + (xTargetRangesToYTargetRanges.Count() * 2) - 2))
                .ToArray();

    for (var i = 0; i < indices.Length; i += 2)
    {
        var start = indices[i];
        var end = indices[i + 1];
        var range = end - start + 1;

        var newXResourceRange = new ResourceRange(start, range);
        var foo = sourceRangesToXTargetRanges.Where(srtxtr => srtxtr.Target.Contains(newXResourceRange));
        var sourceRangeToXTargetRange = sourceRangesToXTargetRanges.Single(srtxtr => srtxtr.Target.Contains(newXResourceRange));
        gaplessSourceRangesToXTargetRanges.Add(new RangeMap(new ResourceRange(sourceRangeToXTargetRange.GetCorrespondingSourceValue(start), range), newXResourceRange));

        var xTargetRangeToYTargetRange = xTargetRangesToYTargetRanges.Single(xtrtytr => xtrtytr.Source.Contains(newXResourceRange));
        gaplessXTargetRangesToYTargetRanges.Add(new RangeMap(newXResourceRange, new ResourceRange(xTargetRangeToYTargetRange.GetCorrespondingTargetValue(start), range)));

        mergedRanges.Add(new RangeMap(new ResourceRange(sourceRangeToXTargetRange.GetCorrespondingSourceValue(start), range), new ResourceRange(xTargetRangeToYTargetRange.GetCorrespondingTargetValue(start), range)));
    }

    return mergedRanges.ToArray();
}

IEnumerable<RangeMap[]> ParseRanges(IEnumerable<string> lines)
{
    static bool lineStartsNewRange(string line) => string.IsNullOrWhiteSpace(line);

    var maps = new List<RangeMap>();
    foreach (var line in lines)
    {
        if (lineStartsNewRange(line))
        {
            yield return CreateMapWithoutSourceGaps(maps).OrderBy(m => m.Source.Begin).ToArray();
            maps = [];
            continue;
        }

        if (char.IsDigit(line[0]))
        {
            maps.Add(new RangeMap(line));
        }
    }

    yield return CreateMapWithoutSourceGaps(maps).OrderBy(m => m.Source.Begin).ToArray();
}

IEnumerable<RangeMap> CreateMapWithoutSourceGaps(List<RangeMap> maps)
{
    var orderedMaps = maps.OrderBy(m => m.Source.Begin);
    RangeMap previousMap = null;
    foreach (var map in orderedMaps)
    {
        if (previousMap == null)
        {
            if (map.Source.Begin != 0)
            {
                yield return new RangeMap(new ResourceRange(0, map.Source.Begin), new ResourceRange(0, map.Source.Begin));
            }

            yield return map;
        }
        else if (map == orderedMaps.Last())
        {
            yield return map;

            if (map.Source.End != long.MaxValue)
            {
                yield return new RangeMap(new ResourceRange(map.Source.End + 1, long.MaxValue - map.Source.End), new ResourceRange(map.Source.End + 1, long.MaxValue - map.Source.End));
            }
        }
        else
        {
            if (previousMap.Source.End + 1 != map.Source.Begin)
            {
                yield return new RangeMap(new ResourceRange(previousMap.Source.End + 1, map.Source.Begin - previousMap.Source.End + 1), new ResourceRange(previousMap.Source.End + 1, map.Source.Begin - previousMap.Source.End + 1));
            }

            yield return map;
        }

        previousMap = map;
    }
}

class RangeMap
{
    public ResourceRange Source { get; }
    public ResourceRange Target { get; }

    /*
     * Line format: num1 num2 num 3
     *     - num1: TargetStartIndex
     *     - num2: SourceStartIndex
     *     - num3: NumberOfIndexesInRange
     */
    public RangeMap(string line)
    {
        var nums = line.Split(' ').Select(i => long.Parse(i.Trim())).ToArray();
        Target = new ResourceRange(nums[0], nums[2]);
        Source = new ResourceRange(nums[1], nums[2]);
    }

    public RangeMap(ResourceRange source, ResourceRange target)
    {
        Source = source;
        Target = target;
    }

    public RangeMap Clone()
    {
        return new RangeMap(
            new ResourceRange(Source.Begin, Source.End - Source.Begin + 1),
            new ResourceRange(Target.Begin, Target.End - Target.Begin + 1)
        );
    }

    public long GetCorrespondingSourceValue(long targetValue)
    {
        return Source.Begin + targetValue - Target.Begin;
    }

    public long GetCorrespondingTargetValue(long sourceValue)
    {
        return Target.Begin + sourceValue - Source.Begin;
    }
}

class ResourceRange
{
    private long Range { get; }
    public long Begin { get; }
    public long End => Begin + Range - 1;
    public bool HasRange => Range > 0;

    public ResourceRange(long begin, long range)
    {
        Begin = begin;
        Range = range;
    }

    /*
     *  |-|  |--|
     *   |-|  |-|
     */
    public bool OverlapsBeginning(ResourceRange other)
    {
        return Begin.CompareTo(other.Begin) < 0 && End.CompareTo(other.Begin) > 0 && End.CompareTo(other.End) <= 0;
    }

    /*
     *   |-| |--|
     *  |-|  |-|
     */
    public bool OverlapsEnd(ResourceRange other)
    {
        return Begin.CompareTo(other.Begin) >= 0 && Begin.CompareTo(other.End) < 0 && End.CompareTo(other.End) > 0;
    }

    /*
     *  |-| |--| |--| |---|
     *  |-| |-|   |-|  |-|
     */
    public bool Contains(ResourceRange other)
    {
        return Begin.CompareTo(other.Begin) <= 0 && End.CompareTo(other.End) >= 0;
    }

    public bool Overlaps(ResourceRange other)
    {
        return
            Begin.CompareTo(other.Begin) == 0
            || (Begin.CompareTo(other.Begin) < 0 && other.Begin.CompareTo(End) < 0)
            || (End.CompareTo(other.Begin) > 0 && other.End.CompareTo(Begin) > 0);
    }
}