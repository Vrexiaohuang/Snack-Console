using CmdGameEngine.Controller;
using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model.Food
{
    class NormalFood : GameObject
    {
        public bool Enabled = true;

        public GameObject target = null;

        public Vector2 targetV2;

        public ConsoleColor fColor = ConsoleColor.Green;

        public float speed = 0.12f;

        public override void Init()
        {
            base.Init();
            if (!isOn) return;
            Image.Clear();

            flySpeed = 0.002f;

            drawTool.SetMousePosition(0, 0);
            drawTool.fColor = fColor;
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

        public void RandomForce()
        {
            force = new Vector2(Dice.Next(0, 10) / 10f, Dice.Next(0, 10) / 10f) * speed;
        }

        public override void Update()
        {
            if (!isOn) return;
            if (!Enabled) return;
            base.Update();

            //if (!canFly)
            //{
            //    //随机另一个位置
            //    RandomFlyPos();
            //    canFly = true;
            //}

            //if (Vector2.Distance(Position, targetV2) <= 1f)
            //{
            //    canFly = false;
            //    return;
            //}
            //FlyTo(targetV2);

            if(Position.X < 4)
            {
                ExplodeController ec = new ExplodeController();
                ec.ExploadOn(Position, fColor);
                force.X = -force.X;
            }
            if(Position.X > 45)
            {
                ExplodeController ec = new ExplodeController();
                ec.ExploadOn(Position, fColor);
                force.X = -force.X;
            }
            if(Position.Y < 4)
            {
                ExplodeController ec = new ExplodeController();
                ec.ExploadOn(Position, fColor);
                force.Y = -force.Y;
            }
            if(Position.Y > 29)
            {
                ExplodeController ec = new ExplodeController();
                ec.ExploadOn(Position, fColor);
                force.Y = -force.Y;
            }


        }

        public void Clear()
        {
            Enabled = false;
            Visible = false;
        }
    }
}
