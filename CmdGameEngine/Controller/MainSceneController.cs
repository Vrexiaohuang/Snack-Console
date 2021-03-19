using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Controller
{
    class MainSceneController : GameObject
    {
        #region 单例
        static MainSceneController ins = null;

        MainSceneController()
        {

        }

        public static MainSceneController Ins
        {
            get
            {
                if (ins == null)
                {
                    ins = new MainSceneController();
                }
                return ins;
            }
        }
        #endregion




    }
}
