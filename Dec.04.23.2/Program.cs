var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));
var matches = lines.Select(line => {
                        var numbersLists = line.Split(':').Last().Trim().Split('|');
                        return new
                        {
                            WinningNumbersList = numbersLists.First().Trim(),
                            MyNumbersList = numbersLists.Last().Trim(),
                        };
                    })
                    .Select(lists =>
                    {
                        var winningNumbers = lists.WinningNumbersList.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n.Trim()));
                        var myNumbers = lists.MyNumbersList.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n.Trim()));
                        return new Temp
                        {
                            NumberOfCards = 1,
                            NumberOfMatches = myNumbers.Where(n => winningNumbers.Contains(n)).Count()
                        };
                    })
                    .ToArray();
for (int i = 0; i < matches.Length; i++)
{
    for (int j = 0; j < matches[i].NumberOfMatches; j++)
    {
        if (i + 1 + j < matches.Length)
        {
            matches[i + 1 + j].NumberOfCards += matches[i].NumberOfCards;
        }
    }
}
Console.WriteLine(matches.Sum(m => m.NumberOfCards));


class Temp
{
    public int NumberOfCards { get; set; }
    public int NumberOfMatches { get; set; }
}