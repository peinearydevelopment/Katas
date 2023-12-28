Console.WriteLine(
    File.ReadLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")))
        .First()
        .Split(',')
        .Sum(item => item.Aggregate<char, long>(0, (c1, c2) => ((c1 + c2) * 17) % 256))
);
