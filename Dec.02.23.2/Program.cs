using System.Text.RegularExpressions;

var sum = 0;
foreach (var line in File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt"))))
{
    sum += line.Split(':')
                        .Last()
                        .Split(";")
                        .SelectMany(s => s.Split(','))
                        .Select(t => new
                        {
                            Count = int.Parse(Regex.Replace(t, "[a-zA-Z]", string.Empty)),
                            Color = string.Concat(t.Where(c => char.IsLetter(c)))
                        })
                        .GroupBy(t => t.Color)
                        .Select(g => g.OrderByDescending(i => i.Count).First().Count)
                        .Aggregate((i1, i2) => i1 * i2);

}
Console.WriteLine(sum);