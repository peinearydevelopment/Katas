var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));
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

var currentLocation = "AAA";
var stepsTaken = 0;
while (currentLocation != "ZZZ")
{
    for (var i = 0; i < instructions.Length; i++)
    {
        stepsTaken++;
        var directions = nodes[currentLocation];
        currentLocation = instructions[i] == 'L' ? directions.Left : directions.Right;
        if (currentLocation == "ZZZ")
        {
            break;
        }
    }
}
Console.WriteLine(stepsTaken);