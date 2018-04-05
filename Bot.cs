using System;
using System.Collections.Generic;
using System.Linq;

namespace crossesXnoughts
{
    class Bot
    {
        public string Name { get; set; }
        public int CountWin { get; set; }

        public Bot(string name)
        {
            Name = name;
        }

        public int GetMotion(int[] board)
        {
            Random random = new Random();
            List<int> list = new List<int>(board);

            var empty = EmptyIndexies(board);

            while (list.Count > 0)
            {
                var rndElement = random.Next(list.Count);
                if (empty[rndElement])
                    return rndElement;
                else
                {
                    list.RemoveAt(rndElement);
                    empty.RemoveAt(rndElement);
                }
            }
            return 0;
        }


        public List<bool> EmptyIndexies(int[] board)
        {
            return board.Select(n => n == 0).ToList();
        }
    }  
}
