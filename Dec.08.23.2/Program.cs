// https://github.com/tomaszkacmajor/advent-of-code-2023/blob/master/AdventOfCode2023/Day8.cs
var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

//var lines = @"LR

//11A = (11B, XXX)
//11B = (XXX, 11Z)
//11Z = (11B, XXX)
//22A = (22B, XXX)
//22B = (22C, 22C)
//22C = (22Z, 22Z)
//22Z = (22B, 22B)
//XXX = (XXX, XXX)".Split(Environment.NewLine);
var instructions = lines[0].ToCharArray();

var nodes = lines.Skip(2)
                    .Select(line =>
                    {
                        var lineParts = line.Split('=', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
                        var directionParts = lineParts[1].Split(",", StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
                        return new
                        {
                            Id = lineParts[0].Trim(),
                            Directions = new
                            {
                                Left = directionParts[0].Trim().Substring(1),
                                Right = directionParts[1].Trim().Substring(0, directionParts[1].Trim().Length - 1)
                            }

                        };
                    })
                    .ToDictionary(d => d.Id, d => d.Directions);

var currentLocations = nodes.Where(n => n.Key.Last() == 'A').Select(n => n.Key).ToArray();

long[] values = new long[currentLocations.Length];

for (int nodeInd = 0; nodeInd < currentLocations.Length; nodeInd++)
{
    bool endFound = false;
    int i = 0;

    while (!endFound)
    {
        var instruction = instructions[i % instructions.Length];
        currentLocations[nodeInd] = instruction == 'L' ? nodes[currentLocations[nodeInd]].Left : nodes[currentLocations[nodeInd]].Right;

        if (currentLocations[nodeInd].EndsWith("Z"))
            endFound = true;
        i++;
    }

    values[nodeInd] = i;
}

Console.WriteLine(Lcm(values));

static long Gcd(long a, long b)
{
    if (b == 0)
        return a;
    else
        return Gcd(b, a % b);
}

static long Lcm(long[] values)
{
    return values.Aggregate((a, b) => a * b / Gcd(a, b));
}