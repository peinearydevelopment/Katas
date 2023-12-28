var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt"))).Select(line => line.ToCharArray()).ToArray();
var dict = new Dictionary<string, List<int>>();
for (int lineI = 0; lineI < lines.Length; lineI++)
{
    var line = lines[lineI];
    for (int i = 0; i < line.Length; i++)
    {
        if (IsSymbol(line[i]))
        {
            if (i != 0 && char.IsDigit(line[i - 1]))
            {
                AddToDict(dict, $"{lineI}|{i}", ParseNumber(line, i - 1));
            }

            if (i < line.Length - 1 && char.IsDigit(line[i + 1]))
            {
                AddToDict(dict, $"{lineI}|{i}", ParseNumber(line, i + 1));
            }

            if (lineI != 0)
            {
                var previousLine = lines[lineI - 1];
                if (i != 0 && char.IsDigit(previousLine[i - 1]))
                {
                    AddToDict(dict, $"{lineI}|{i}", ParseNumber(previousLine, i - 1));
                }

                if (char.IsDigit(previousLine[i]))
                {
                    AddToDict(dict, $"{lineI}|{i}", ParseNumber(previousLine, i));
                }

                if (i < previousLine.Length - 1 && char.IsDigit(previousLine[i + 1]))
                {
                    AddToDict(dict, $"{lineI}|{i}", ParseNumber(previousLine, i + 1));
                }
            }
        }

        if (char.IsDigit(line[i]))
        {
            if (lineI != 0)
            {
                var previousLine = lines[lineI - 1];
                if (i != 0 && IsSymbol(previousLine[i - 1]))
                {
                    AddToDict(dict, $"{lineI-1}|{i-1}", ParseNumber(line, i));
                }

                if (IsSymbol(previousLine[i]))
                {
                    AddToDict(dict, $"{lineI-1}|{i}", ParseNumber(line, i));
                }

                if (i < previousLine.Length - 1 && IsSymbol(previousLine[i + 1]))
                {
                    AddToDict(dict, $"{lineI-1}|{i+1}", ParseNumber(line, i));
                }
            }
        }
    }
}
var sum = dict.Where(e => e.Value.Count() == 2).Sum(e => e.Value.First() * e.Value.Last());
Console.WriteLine(sum);


static void AddToDict(Dictionary<string, List<int>> dict, string key, int value)
{
    if (dict.ContainsKey(key))
    {
        dict[key].Add(value);
    }
    else
    {
        dict.Add(key, [value]);
    }
}
static int ParseNumber(char[] line, int index)
{
    while (index != 0 && char.IsDigit(line[index - 1]))
    {
        index--;
    }

    var number = string.Empty;
    while (index < line.Length && char.IsDigit(line[index]))
    {
        number += line[index];
        line[index] = '.';
        index++;
    }

    return int.Parse(number == string.Empty ? "0" : number);
}

static bool IsSymbol(char c)
{
    return c == '*';
}