using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model.Snack
{
    class PlayerSnackBody : GameObject
    {
        public GameObject target = null;

        public bool followHead = false;

        public PlayerSnackHead player = null;

        ConsoleColor fColor = ConsoleColor.Yellow;
        public ConsoleColor FColor
        {
            get
            {
                return fColor;
            }
            set
            {
                fColor = value;
                if (!isOn) return;
                Init();
            }
        }

        public override void Init()
        {
            base.Init();

            if (!isOn) return;

            Image.Clear();

            drawTool.SetMousePosition(0, 0);
            drawTool.fColor = fColor;
            drawTool.Write("⊙");
        }

        public override void Update()
        {
            base.Update();
            if (!isOn) return;

            if (target == null) return;

            Vector2 tar = target.Position;

            if (followHead)
            {
                tar = player.Position;
                if (player.goTop)
                {
                    tar.X += 0.5f;
                    //tar.Y += 1f;
                }
                if (player.goDown)
                {
                    tar.X += 0.5f;
                }
                if (player.goLeft)
                {
                    //tar.X += 1f;
                    tar.Y += 0.5f;
                }
                if (player.goRight)
                {
                    tar.Y += 0.5f;
                }
            }

            if (Vector2.Distance(Position, tar) <= 1f)
            {
                canFly = false;
                return;
            }
            FlyTo(tar);
        }
    }
}
