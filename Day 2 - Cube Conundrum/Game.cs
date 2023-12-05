using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2___Cube_Conundrum
{
    public class Game
    {
        public int ID { get; set; }

        public List<Set> Sets { get; set; }


        public Game(int id,  List<Set> sets)
        {
            ID = id;
            Sets = sets;
        }

        public Game(int id)
        {
            ID = id;
            Sets = new List<Set>();
        }

        public override string ToString()
        {
            string msg = string.Empty;
            msg += $"ID: {ID} \n";

            Sets.ForEach(s =>  msg += $"{s}\n");

            return msg;
        }

    }
}
