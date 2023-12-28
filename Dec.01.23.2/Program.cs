using System.Text.RegularExpressions;

var rsum = 0;
var sum = 0;
var digits = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
foreach (var line in File.ReadAllLines("C:\\code\\zp_sandbox\\AdventOfCode2023\\Dec.01.2023.2\\input.txt"))
{
    /*
     * https://stackoverflow.com/a/321391
     */
    var list = new List<Match>();
    var regexObj = new Regex("[1-9]|one|two|three|four|five|six|seven|eight|nine");
    var matchObj = regexObj.Match(line);
    list.Add(matchObj);
    while (matchObj.Success)
    {
        matchObj = regexObj.Match(line, matchObj.Index + 1);
        list.Add(matchObj);
    }

    var orderedList = list.Where(m => !string.IsNullOrWhiteSpace(m.Value)).OrderBy(x => x.Index);
    var rd1 = orderedList.First();
    var rd2 = orderedList.Last();
    rsum += int.Parse($"{Translate(rd1.Value)}{Translate(rd2.Value)}");

    /*correct but really inefficient*/
    var matches = digits.Select(digit => new
    {
        FirstPosition = line.Contains(digit, StringComparison.CurrentCulture) ? line.IndexOf(digit) + digit.Length : -1,
        LastPosition = line.Contains(digit, StringComparison.CurrentCulture) ? line.LastIndexOf(digit) + digit.Length : -1,
        Digit = digit
    }).Where(a => a.FirstPosition >= 0);
    var d1 = matches.OrderBy(a => a.FirstPosition).First().Digit;
    var d2 = matches.OrderBy(a => a.LastPosition).Last().Digit;
    sum += int.Parse($"{Translate(d1)}{Translate(d2)}");
}
Console.WriteLine(rsum);
Console.WriteLine(sum);

static string Translate(string input)
{
    return input switch
    {
        "one" => "1",
        "two" => "2",
        "three" => "3",
        "four" => "4",
        "five" => "5",
        "six" => "6",
        "seven" => "7",
        "eight" => "8",
        "nine" => "9",
        _ => input,
    };
}