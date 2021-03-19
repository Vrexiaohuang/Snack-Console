using CmdGameEngine.GameEngine;
using CmdGameEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CmdGameEngine.Controller
{
    class Mode3TimerController : GameObject
    {
        public Text timerText = null;

        public int second = 0;

        public bool Enabled = true;

        Timer t = new Timer();

        public override void Init()
        {
            base.Init();
            if (!isOn) return;
            if (!Enabled) return;

            t.Elapsed -= T_Elapsed;
            t.Elapsed += T_Elapsed;

            t.Interval = 1000;
            t.Start();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!Enabled)
            {
                t.Stop();
                return;
            }

            second++;

            if (timerText != null)
            {
                timerText.TextValue = second.ToString() + "秒";
            }
        }
    }
}
