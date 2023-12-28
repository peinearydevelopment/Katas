var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

var times = lines
                .First().Split(':')
                .Last().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => int.Parse(t.Trim()))
                .ToArray();

var distances = lines
                .Last().Split(':')
                .Last().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => int.Parse(t.Trim()))
                .ToArray();

long total = 1;
for (var i = 0; i < times.Length; i++)
{
    var numOfWaysToWin = 0;
    var time = times[i];
    var distance = distances[i];
    for (var j = 0; j <= time; j++)
    {
        var thisDistance = (time - j) * j;
        if (thisDistance > distance)
        {
            numOfWaysToWin++;
        }
    }
    total *= numOfWaysToWin;
}
Console.WriteLine(total);