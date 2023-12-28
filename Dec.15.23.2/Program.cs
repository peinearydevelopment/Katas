var line = File.ReadLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt"))).First();

//var line = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";

var lenses = line
                .Split(',')
                .Select(x =>
                {
                    var parts = x.Split(['=', '-']);
                    return new Lense
                    {
                        Label = parts[0],
                        FocalLength = string.IsNullOrWhiteSpace(parts[1]) ? null : int.Parse(parts[1]),
                        BoxIndex = parts[0].Aggregate(0, (c1, c2) => ((c1 + c2) * 17) % 256),
                        Action = x.Contains('=') ? '=' : '-'
                    };
                })
                .ToArray();

var arr = new List<Lense>[255];
for (var i = 0; i < arr.Length; i++)
{
    arr[i] = [];
}
foreach (var lense in lenses)
{
    var box = arr[lense.BoxIndex];
    var existingLenseWithMatchingLabel = box.SingleOrDefault(l => l.Label == lense.Label);
    if (lense.Action == '=')
    {
        if (existingLenseWithMatchingLabel is not null)
        {
            box[box.IndexOf(existingLenseWithMatchingLabel)] = lense;
        }
        else
        {
            box.Add(lense);
        }
    }
    else
    {
        if (existingLenseWithMatchingLabel is not null)
        {
            box.Remove(existingLenseWithMatchingLabel);
        }
    }
}

var sum = 0;
foreach (var box in arr.Select((a, i) => new { Box = a, Index = i }))
{
    foreach (var item in box.Box.Select((a, i) => new { Item = a, Index = i }))
    {
        var focusingPower = (box.Index + 1) * (item.Index + 1) * item.Item.FocalLength!.Value;
        Console.Write($"[{item.Item.Label} {item.Item.FocalLength}]({focusingPower}) ");
        sum += focusingPower;
    }

    if (box.Box.Count > 0)
    {
        Console.WriteLine();
    }
}
Console.WriteLine(sum);

class Lense
{
    public string Label { get; set; }
    public int? FocalLength { get; set; }
    public int BoxIndex { get; set; }
    public char Action { get; set; }
}