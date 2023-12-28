using System.Text.RegularExpressions;

const int maxRedCubes = 12;
const int maxGreenCubes = 13;
const int maxBlueCubes = 14;
var sum = 0;
foreach (var line in File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt"))))
{
    var lineParts = line.Split(':');
    var gameNumber = int.Parse(Regex.Replace(lineParts.First(), "[a-zA-Z]", string.Empty));
    var possible = true;
    foreach (var trie in lineParts.Last().Split(";"))
    {
        foreach (var coloredCubeCount in trie.Split(','))
        {
            var count = int.Parse(Regex.Replace(coloredCubeCount, "[a-zA-Z]", string.Empty));
            var impossible = string.Concat(coloredCubeCount.Where(c => char.IsLetter(c))) switch
            {
                "red" => count > maxRedCubes,
                "green" => count > maxGreenCubes,
                "blue" => count > maxBlueCubes,
                _ => false,
            };

            if (impossible)
            {
                possible = false;
                break;
            }
        }
        if (!possible)
        {
            break;
        }
    }
    if (possible)
    {
        sum += gameNumber;
    }
}
Console.WriteLine(sum);