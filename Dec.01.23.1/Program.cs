var sum = 0;
foreach (var line in File.ReadAllLines("C:\\code\\zp_sandbox\\AdventOfCode2023\\Dec.01.2023.1\\input.txt"))
{
    var d1 = line.First(c => char.IsDigit(c));
    var d2 = line.Reverse().First(c => char.IsDigit(c));
    sum += int.Parse($"{d1}{d2}");
}
Console.WriteLine(sum);