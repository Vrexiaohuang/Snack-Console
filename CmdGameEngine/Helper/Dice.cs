using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine
{
    class Dice
    {
        public static Random r = new Random();

        public static int Next(int min, int max)
        {
            return r.Next(min, max + 1);
        }

        public static Vector2 NextV2(int minX, int maxX, int minY, int maxY)
        {
            return new Vector2(Next(minX, maxX), Next(minY, maxY));
        }

        public static ConsoleColor NextColor()
        {
            return (ConsoleColor)r.Next(9, 16);
        }
    }
}
