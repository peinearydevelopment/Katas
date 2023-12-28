var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

var sum = 0;
foreach (var line in lines)
{
    var l = new List<int[]> { line.Split(' ').Select(int.Parse).ToArray() };
    var items = l.Last();
    while (!items.All(x => x == 0))
    {
        var newItems = new int[items.Length - 1];
        for (var i = 0; i < items.Length - 1; i++)
        {
            newItems[i] = items[i + 1] - items[i];
        }
        items = newItems;
        l.Add(items);
    }
    l.Reverse();
    var a = 0;
    for (var i = 1; i < l.Count; i++)
    {
        a = l[i][0] - a;
    }
    sum += a;
}
Console.WriteLine(sum);