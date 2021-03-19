using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model.Food
{
    class SpeedFood : GameObject
    {
        public bool Enabled = true;

        public GameObject target = null;

        public Vector2 targetV2;

        public override void Init()
        {
            base.Init();
            if (!isOn) return;
            Image.Clear();

            flySpeed = 0.02f;

            drawTool.SetMousePosition(-1, -1);
            drawTool.fColor = Dice.NextColor();
            drawTool.Write("速");
            drawTool.SetMousePosition(0, -1);
            drawTool.fColor = Dice.NextColor();
            drawTool.Write("度");
            drawTool.SetMousePosition(1, -1);
            drawTool.fColor = Dice.NextColor();
            drawTool.Write("！");

            drawTool.SetMousePosition(0, 0);
            drawTool.fColor = Dice.NextColor();
            drawTool.Write("★");
        }

        public void RandomPos()
        {
            Position = Dice.NextV2(4, 43, 4, 25);
        }

        public void RandomFlyPos()
        {
            targetV2 = Dice.NextV2(4, 43, 4, 25);
        }

        public override void Update()
        {
            if (!isOn) return;
            if (!Enabled) return;
            base.Update();

            Init();

            if (!canFly)
            {
                canFly = true;
            }

            if (Vector2.Distance(Position, target.Position) <= 1f)
            {
                canFly = false;
                return;
            }
            FlyTo(target.Position);

        }

        public void Clear()
        {
            Enabled = false;
            Visible = false;
        }
    }
}
