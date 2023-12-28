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
            var cardCounts = Cards.GroupBy(c => c).Select(g => new { Card = g.Key, Count = g.Count() });
            if (cardCounts.Any(c => c.Count == 5))
            {
                return CamelCardHandType.FiveOfAKind;
            }

            var oneOfAKinds = cardCounts.Where(c => c.Count == 1);
            var hasSingleJoker = oneOfAKinds.Any(c => c.Card == 'J');
            var fourOfAKind = cardCounts.FirstOrDefault(c => c.Count == 4);
            if (fourOfAKind is not null)
            {
                return fourOfAKind.Card == 'J' || hasSingleJoker ? CamelCardHandType.FiveOfAKind : CamelCardHandType.FourOfAKind;
            }

            var threeOfAKind = cardCounts.FirstOrDefault(c => c.Count == 3);
            var twoOfAKinds = cardCounts.Where(c => c.Count == 2);
            if (threeOfAKind is not null)
            {
                if (twoOfAKinds.Any())
                {
                    return twoOfAKinds.First().Card == 'J' || threeOfAKind.Card == 'J' ? CamelCardHandType.FiveOfAKind : CamelCardHandType.FullHouse;
                }

                return threeOfAKind.Card == 'J' || hasSingleJoker ? CamelCardHandType.FourOfAKind : CamelCardHandType.ThreeOfAKind;
            }

            if (twoOfAKinds.Any())
            {
                if (twoOfAKinds.Any(twoOfAKind => twoOfAKind.Card == 'J'))
                {
                    return twoOfAKinds.Count() == 1 ? CamelCardHandType.ThreeOfAKind : CamelCardHandType.FourOfAKind;
                }

                if (hasSingleJoker)
                {
                    return twoOfAKinds.Count() == 1 ? CamelCardHandType.ThreeOfAKind : CamelCardHandType.FullHouse;
                }

                return twoOfAKinds.Count() == 1 ? CamelCardHandType.OnePair : CamelCardHandType.TwoPair;
            }

            return hasSingleJoker ? CamelCardHandType.OnePair : CamelCardHandType.HighCard;
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
    private static char[] CardRanks = ['A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'];

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

        return Array.IndexOf(CardRanks, x) > Array.IndexOf(CardRanks, y) ? 1 : -1;
    }
}