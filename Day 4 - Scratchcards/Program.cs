namespace Day_4___Scratchcards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
            var inputText = File.ReadAllLines(inputPath).ToList();

            var cards = new List<Card>();
            foreach (var line in inputText)
            {
                var cardSplit = line.Split(':');

                var id = int.Parse(cardSplit[0].Replace("Card", "").Trim());
                var card = new Card(id);

                var numSplit = cardSplit[1].Split('|');
                var winningSplit = numSplit[0].Trim().Split(" ");
                var haveSplit = numSplit[1].Trim().Split(" ");

                var winNums = new List<int>();
                var haveNums = new List<int>();
                foreach (var win in winningSplit)
                {
                    if (string.IsNullOrEmpty(win))
                        continue;

                    winNums.Add(int.Parse(win.Trim()));
                }

                foreach (var have in haveSplit)
                {
                    if (string.IsNullOrEmpty(have))
                        continue;

                    haveNums.Add(int.Parse(have.Trim()));
                }

                card.WinningNums = winNums;
                card.Numbers = haveNums;
                cards.Add(card);
            }

            PartOne(cards);
            PartTwo(cards);


            Console.ReadKey();
        }

        static void PartOne(List<Card> cards)
        {
            int points = 0;
            foreach (var card in cards)
            {
                var cnt = card.Numbers.Count(n => card.WinningNums.Contains(n));
                points += (1 << cnt) / 2;
            }

            Console.WriteLine($"Part One Points: {points}");
        }

        static void PartTwo(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var cnt = card.Numbers.Count(n => card.WinningNums.Contains(n));

                for (int j = i + 1; j <= i + cnt; j++)
                {
                    cards[j].Multiplier += 1 * card.Multiplier;
                }
            }

            var totalCards = cards.Sum(c => c.Multiplier);

            Console.WriteLine($"Part Two Cards: {totalCards}");
        }
    }
}