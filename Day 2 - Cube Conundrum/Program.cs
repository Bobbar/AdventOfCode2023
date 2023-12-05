namespace Day_2___Cube_Conundrum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
            var inputText = File.ReadAllLines(inputPath).ToList();

            var games = new List<Game>();
            foreach (var line in inputText)
            {
                var colonSplit = line.Split(':');
                var gameId = int.Parse(colonSplit[0].Replace("Game ", ""));
                var game = new Game(gameId);

                foreach (var setStr in colonSplit[1].Split(";"))
                {
                    var set = new Set();
                    foreach (var cubeStr in setStr.Split(","))
                    {
                        if (cubeStr.Contains("red"))
                            set.Red = int.Parse(cubeStr.Replace("red", "").Trim());

                        if (cubeStr.Contains("green"))
                            set.Green = int.Parse(cubeStr.Replace("green", "").Trim());

                        if (cubeStr.Contains("blue"))
                            set.Blue = int.Parse(cubeStr.Replace("blue", "").Trim());

                    }

                    game.Sets.Add(set);
                }

                games.Add(game);
            }

            PartOne(games);

            PartTwo(games);

            Console.ReadKey();
        }

        static void PartOne(List<Game> games)
        {
            const int maxRed = 12;
            const int maxGreen = 13;
            const int maxBlue = 14;

            var possibleGames = games.Where(g => g.Sets.All(s => s.Red <= maxRed && s.Green <= maxGreen && s.Blue <= maxBlue)).ToList();

            int sum = 0;
            possibleGames.ForEach(g => sum += g.ID);

            Console.WriteLine($"Part One Sum: {sum}");
        }

        static void PartTwo(List<Game> games)
        {
            int powers = 0;

            foreach (var game in games)
            {
                int mRed = int.MinValue;
                int mGreen = int.MinValue;
                int mBlue = int.MinValue;

                game.Sets.ForEach(s =>
                {
                    mRed = Math.Max(mRed, s.Red);
                    mGreen = Math.Max(mGreen, s.Green);
                    mBlue = Math.Max(mBlue, s.Blue);
                });

                powers += mRed * mGreen * mBlue;
            }

            Console.WriteLine($"Part Two Sum: {powers}");
        }
    }
}