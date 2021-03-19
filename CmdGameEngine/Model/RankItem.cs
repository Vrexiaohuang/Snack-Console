using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model
{
    public class RankItem:IComparable<RankItem>
    {
        public string name = "";
        public int value = 0;

        public int CompareTo(RankItem other)
        {
            if(value > other.value)
            {
                return -1;
            }
            if(value < other.value)
            {
                return 1;
            }
            if(value == other.value)
            {
                return 0;
            }
            return 0;
        }
    }
}
