using CmdGameEngine.GameEngine;
using CmdGameEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CmdGameEngine.Controller
{
    class StarBgController : GameObject
    {
        public List<Star> stars = new List<Star>();

        public bool enabled = true;

        public override void Update()
        {
            base.Update();

            if (!isOn) return;

            if (!enabled) return;

            if (Program.WaitFrame(5))
            {
                Star star = new Star();

                star.parentSBC = this;
                star.isOn = true;
                star.Position = new Vector2(47, Dice.Next(4, Console.BufferHeight - 4));

                NowScene.Ins.AddObjectToNowScene(star);
                stars.Add(star);
            }

            if (Program.WaitFrame(3))
            {
                foreach (Star item in stars)
                {
                    item.MoveOffset(-1, 0);

                    if (item.Position.X < 3)
                    {
                        item.canGo = false;
                    }
                }
            }
        }

    }
}
