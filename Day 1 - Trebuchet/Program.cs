using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day_1___Trebuchet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
            var inputText = File.ReadAllLines(inputPath).ToList();


            PartOne(inputText);

            PartTwo(inputText);

            Console.ReadKey();
        }

        static void PartOne(List<string> input)
        {
            int total = 0;

            foreach (var line in input)
            {
                var chrs = new List<char>();
                int idx = 0;
                foreach (var c in line)
                {
                    if (char.IsDigit(c))
                        chrs.Add(c);
                }

                if (int.TryParse(chrs.First().ToString() + chrs.Last().ToString(), out int res))
                    total += res;
            }

            Console.WriteLine($"Total Part 1: {total}");
        }


        static void PartTwo(List<string> input)
        {
            int total = 0;

            var numWords = new Dictionary<string, int>()
            {
                { "zero", 0 },
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };
           
            foreach (var line in input)
            {
                var numsSort = new SortedList<int, int>();

                for (int i = 0; i < line.Length; i++)
                {
                    var c = line[i];

                    if (char.IsDigit(c))
                    {
                        numsSort.Add(i, int.Parse(c.ToString()));
                    }
                }

                foreach (var numWord in numWords)
                {
                    var occurs = IndexOfAll(line, numWord.Key);
                    foreach (var o in occurs)
                    {
                        numsSort.Add(o, numWords[line.Substring(o, numWord.Key.Length)]);
                    }
                }

                //Console.Write($"{line} -> ");

                //foreach (var n in numsSort.Values)
                //    Console.Write(n);

                //Console.WriteLine();

                if (int.TryParse(numsSort.Values.First().ToString() + numsSort.Values.Last().ToString(), out int res))
                    total += res;
            }

            Console.WriteLine($"Total Part 2: {total}");
        }

        public static IEnumerable<int> IndexOfAll(string sourceString, string subString)
        {
            return Regex.Matches(sourceString, subString).Cast<Match>().Select(m => m.Index);
        }
    }
}