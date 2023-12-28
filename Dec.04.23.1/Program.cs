var sum = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")))
                .Select(line => {
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
                    return myNumbers.Where(n => winningNumbers.Contains(n)).Count();
                })
                .Where(n => n > 0)
                .Sum(n => Math.Pow(2, n-1));
Console.WriteLine(sum);