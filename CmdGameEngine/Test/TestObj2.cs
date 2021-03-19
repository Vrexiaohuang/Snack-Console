using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CmdGameEngine.Test
{
    public class TestObj2 : GameObject
    {
        public GameObject target = null;

        public override void Init()
        {
            base.Init();
            Layer = 1;
            drawTool.bColor = ConsoleColor.Blue;

            drawTool.WriteLine(" ");
        }

        public override void Update()
        {
            base.Update();

            if (target == null) return;

            if (Vector2.Distance(Position, target.Position) <= 1f)
            {
                canFly = false;
                return;
            }
            FlyTo(target.Position);

        }

        public override void OnKeyDown(ConsoleKey key)
        {
            base.OnKeyDown(key);
            if (key == ConsoleKey.UpArrow)
            {
                MoveOffset(0, -1);
            }
            if (key == ConsoleKey.DownArrow)
            {
                MoveOffset(0, 1);
            }
            if (key == ConsoleKey.LeftArrow)
            {
                MoveOffset(-1, 0);
            }
            if (key == ConsoleKey.RightArrow)
            {
                MoveOffset(1, 0);
            }
            if (key == ConsoleKey.G)
            {
                FlyTo(new Vector2(15, 20));
            }
        }
    }
}
