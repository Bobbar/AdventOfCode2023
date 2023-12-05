using System.Drawing;

namespace Day_3___Gear_Ratios
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
            var inputText = File.ReadAllLines(inputPath).ToList();

            var dims = new Size(inputText[0].Length, inputText.Count);

            char[,] grid = new char[dims.Width, dims.Height];

            for (int i = 0; i < inputText.Count; i++)
            {
                var line = inputText[i];

                for (int j = 0; j < line.Length; j++)
                {
                    grid[i, j] = line[j];
                }
            }


            PartOne(grid);
            PartTwo(grid);


            Console.ReadKey();
        }

        static void PartOne(char[,] grid)
        {

            var targetNums = new List<int>();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                var nums = new List<char>();
                bool isAdjacent = false;

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var c = grid[i, j];

                    if (char.IsDigit(c))
                    {
                        nums.Add(c);

                        if (IsNearSymbol(grid, i, j))
                            isAdjacent = true;

                    }
                    else
                    {
                        if (nums.Count > 0)
                        {
                            var num = int.Parse(nums.ToArray());

                            if (isAdjacent)
                                targetNums.Add(num);

                            nums.Clear();
                            isAdjacent = false;

                        }
                    }
                }

                // Handle numbers on edge.
                if (nums.Count > 0)
                {
                    var num = int.Parse(nums.ToArray());

                    if (isAdjacent)
                        targetNums.Add(num);

                    nums.Clear();
                    isAdjacent = false;

                }
            }

            int sum = 0;
            targetNums.ForEach(n => sum += n);

            Console.WriteLine($"Part One Sum: {sum}");
        }

        static void PartTwo(char[,] grid)
        {
            // Key = X/Row, Value = List of part numbers with ranges (Y/Cols) they occupy.
            var ranges = new Dictionary<int, List<Tuple<int, Range>>>(); // This is the most cursed type I've ever created...

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                var nums = new List<char>();
                int startIdx = -1;

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var c = grid[i, j];

                    if (char.IsDigit(c))
                    {
                        if (nums.Count == 0)
                            startIdx = j;

                        nums.Add(c);
                    }
                    else
                    {
                        if (nums.Count > 0)
                        {
                            var num = int.Parse(nums.ToArray());

                            if (ranges.ContainsKey(i))
                                ranges[i].Add(new Tuple<int, Range>(num, new Range(startIdx, j - 1)));
                            else
                                ranges.Add(i, new List<Tuple<int, Range>>() { new Tuple<int, Range>(num, new Range(startIdx, j - 1)) });

                            nums.Clear();
                            startIdx = -1;


                        }
                    }
                }

                // Handle numbers on edge.
                if (nums.Count > 0)
                {
                    var num = int.Parse(nums.ToArray());

                    if (ranges.ContainsKey(i))
                        ranges[i].Add(new Tuple<int, Range>(num, new Range(startIdx, grid.GetLength(1) - 1)));
                    else
                        ranges.Add(i, new List<Tuple<int, Range>>() { new Tuple<int, Range>(num, new Range(startIdx, grid.GetLength(1) - 1)) });

                    nums.Clear();
                }
            }

            int sum = 0;

            // Look for gear symbols and find nearby part numbers.
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var c = grid[i, j];

                    if (c == '*')
                    {
                        var nearNums = FindNeighborNums(i, j, ranges);

                        if (nearNums.Count == 2)
                        {
                            sum += nearNums.First() * nearNums.Last();
                        }
                    }
                }
            }

            Console.WriteLine($"Part Two Sum: {sum}");
        }

        static List<int> FindNeighborNums(int row, int col, Dictionary<int, List<Tuple<int, Range>>> ranges)
        {
            var nums = new HashSet<int>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int ox = row + x;
                    int oy = col + y;

                    if (ranges.TryGetValue(ox, out List<Tuple<int, Range>> r))
                    {
                        foreach (var range in r)
                        {
                            if (oy >= range.Item2.Start.Value && oy <= range.Item2.End.Value)
                            {
                                nums.Add(range.Item1);
                            }
                        }
                        
                    }
                }
            }

            return nums.ToList();
        }

        static bool IsNearSymbol(char[,] grid, int idxX, int idxY)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int ox = idxX + x;
                    int oy = idxY + y;

                    if (ox >= 0 && oy >= 0 && ox < grid.GetLength(0) && oy < grid.GetLength(1))
                    {
                        if (!char.IsDigit(grid[ox, oy]) && grid[ox, oy] != '.')
                        {
                            return true;
                        }
                    }

                }
            }

            return false;
        }
    }
}