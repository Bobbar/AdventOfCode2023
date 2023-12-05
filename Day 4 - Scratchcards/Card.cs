using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4___Scratchcards
{
    public class Card
    {
        public int Id { get; set; }
        public int Multiplier { get; set; } = 1;

        public List<int> WinningNums { get; set; }
        public List<int> Numbers { get; set; }


        public Card(int id)
        {
            Id = id;
        }

    }
}
