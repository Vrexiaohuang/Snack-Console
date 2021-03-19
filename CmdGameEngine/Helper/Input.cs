using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine
{
    class Input
    {
        public static bool GetKeyDown(ConsoleKey key)
        {
            return Program.CheckKey(key);
        }
    }
}
