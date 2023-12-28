var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

var time = long.Parse(
                string.Concat(
                    lines
                        .First().Split(':')
                        .Last().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => long.Parse(t.Trim()))
                )
            );

var distance = long.Parse(
                    string.Concat(
                        lines
                            .Last().Split(':')
                            .Last().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(t => long.Parse(t.Trim()))
                   )
                );

var numOfWaysToWin = 0;
for (var j = 0; j <= time; j++)
{
    var thisDistance = (time - j) * j;
    if (thisDistance > distance)
    {
        numOfWaysToWin++;
    }
}
Console.WriteLine(numOfWaysToWin);