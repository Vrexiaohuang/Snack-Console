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
    class PlayerBulletFood : GameObject
    {
        public bool Enabled = true;

        public GameObject target = null;

        public ConsoleColor fColor = ConsoleColor.Magenta;

        public float speed = 0.12f;

        public int crushCount = 0;

        public FoodController fc = null;

        public override void Init()
        {
            base.Init();
            if (!isOn) return;
            Image.Clear();

            drawTool.SetMousePosition(0, 0);
            drawTool.fColor = fColor;
            drawTool.Write("☉");

        }

        public override void Update()
        {
            if (!isOn) return;
            if (!Enabled) return;
            
            base.Update();
            if (Position.X < 4)
            {
                ExplodeController ec = new ExplodeController();
                ec.ExploadOn(Position, fColor);
                force.X = -force.X;
                crushCount++;
            }
            if (Position.X > 45)
            {
                ExplodeController ec = new ExplodeController();
                ec.ExploadOn(Position, fColor);
                force.X = -force.X;
                crushCount++;
            }
            if (Position.Y < 4)
            {
                ExplodeController ec = new ExplodeController();
                ec.ExploadOn(Position, fColor);
                force.Y = -force.Y;
                crushCount++;
            }
            if (Position.Y > 29)
            {
                ExplodeController ec = new ExplodeController();
                ec.ExploadOn(Position, fColor);
                force.Y = -force.Y;
                crushCount++;
            }

            if(crushCount > 2)
            {
                Clear();
            }

            if (fc != null)
            {
                if(fc.allBulletFoods.Exists(t => ((int)t.Position.X >= (int)Position.X - 1 && (int)t.Position.X <= (int)Position.X + 1) && ((int)t.Position.Y >= (int)Position.Y - 1 && (int)t.Position.Y <= (int)Position.Y + 1) && t.Visible))
                {
                    BulletFood bf = fc.allBulletFoods.Where(t => ((int)t.Position.X >= (int)Position.X - 1 && (int)t.Position.X <= (int)Position.X + 1) && ((int)t.Position.Y >= (int)Position.Y - 1 && (int)t.Position.Y <= (int)Position.Y + 1) && t.Visible).FirstOrDefault();

                    fc.allBulletFoods.Remove(bf);
                    bf.Clear();
                    Clear();

                    ExplodeController ec = new ExplodeController();
                    ec.ExploadOn(Position);
                }
            }
        }

        public void Clear()
        {
            Enabled = false;
            Visible = false;
        }
    }
}
