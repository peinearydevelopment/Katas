var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../", "input.txt")));

var hands = lines.Select(line => new CamelCardHand(line)).ToArray();
var handsGroupedByType = hands.GroupBy(c => c.Type)
                                .OrderBy(g => g.Key)
                                .ToArray();
var camelHandComparer = new CamelCardHandComparer();
var handsProcessed = 0;
foreach (var handGroup in handsGroupedByType)
{
    var orderedGroup = handGroup.Order(camelHandComparer).Reverse().ToArray();
    for (var i = 1; i <= orderedGroup.Length; i++)
    {
        orderedGroup[i - 1].Rank = i + handsProcessed;
    }
    handsProcessed += orderedGroup.Length;
}

Console.WriteLine(hands.Sum(h => h.Rank * h.Bid));

enum CamelCardHandType
{
    Unknown = 0,
    HighCard = 1,
    OnePair = 2,
    TwoPair = 3,
    ThreeOfAKind = 4,
    FullHouse = 5,
    FourOfAKind = 6,
    FiveOfAKind = 7
}

class CamelCardHand
{
    public char[] Cards { get; set; }
    public long Bid { get; set; }
    public long Rank { get; set; }
    public CamelCardHandType Type
    {
        get
        {
            var cardCounts = Cards.GroupBy(c => c).Select(g => g.Count());
            if (cardCounts.Contains(5))
            {
                return CamelCardHandType.FiveOfAKind;
            }

            if (cardCounts.Contains(4))
            {
                return CamelCardHandType.FourOfAKind;
            }

            if (cardCounts.Contains(3))
            {
                return cardCounts.Contains(2) ? CamelCardHandType.FullHouse : CamelCardHandType.ThreeOfAKind;
            }

            if (cardCounts.Contains(2))
            {
                return cardCounts.Count(c => c == 2) == 2 ? CamelCardHandType.TwoPair : CamelCardHandType.OnePair;
            }

            return CamelCardHandType.HighCard;
        }

    }

    public CamelCardHand(string line)
    {
        var parts = line.Split(' ');
        Cards = parts[0].ToCharArray();
        Bid = int.Parse(parts[1]);
    }
}

class CamelCardHandComparer : IComparer<CamelCardHand>
{
    private static char[] CardRanks = ['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'];

    public int Compare(CamelCardHand x, CamelCardHand y)
    {
        for (var i = 0; i < x.Cards.Length; i++)
        {
            var cardCompare = CardCompare(x.Cards[i], y.Cards[i]);
            if (cardCompare != 0)
            {
                return cardCompare;
            }
        }

        return 0;
    }

    private int CardCompare(char x, char y)
    {
        if (x == y)
        {
            return 0;
        }

        return Array.IndexOf(CardRanks ,x) > Array.IndexOf(CardRanks ,y) ? 1 : - 1;
    }
}