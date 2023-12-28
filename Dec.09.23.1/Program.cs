var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

var sum = 0;
foreach (var line in lines)
{
    var l = new List<int[]> { line.Split(' ').Select(int.Parse).ToArray() };
    var items = l.Last();
    while (!items.All(x => x == 0))
    {
        var newItems = new int[items.Length - 1];
        for (int i = 0; i < items.Length - 1; i++)
        {
            newItems[i] = items[i + 1] - items[i];
        }
        items = newItems;
        l.Add(items);
    }
    sum += l.Select(i => i.Last()).Sum();
}
Console.WriteLine(sum);