using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CmdGameEngine.GameEngine.Helper
{
    public class ArrayHelper
    {
        #region 单例
        static ArrayHelper ins = null;

        ArrayHelper()
        {

        }

        public static ArrayHelper Ins
        {
            get
            {
                if (ins == null)
                {
                    ins = new ArrayHelper();
                }
                return ins;
            }
        }
        #endregion

        public List<MItem> AddDxy(List<MItem> old, int dx, int dy, int layer = -1000, int vb = -1)
        {

            List<MItem> res = new List<MItem>();
            foreach (MItem item in old)
            {
                MItem temp = item;
                temp.position = new Vector2(temp.position.X + dx, temp.position.Y + dy);
                if (layer != -1000)
                {
                    temp.layer = layer;
                }
                if (vb == 1)
                {
                    temp.isVisible = true;
                }
                else if (vb == 0)
                {
                    temp.isVisible = false;
                }
                res.Add(temp);
            }
            return res;
        }

    }


}


