using CmdGameEngine.GameEngine;
using CmdGameEngine.GameEngine.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CmdGameEngine.Test
{
    class TestObj1 : GameObject
    {
        public ConsoleKey upDown = ConsoleKey.S;

        public ConsoleKey leftRight = ConsoleKey.D;

        public override void Init()
        {
            base.Init();
            Layer = 2;
            drawTool.bColor = ConsoleColor.Red;
            drawTool.WriteLine(" ");
        }

        public override void Update()
        {
            base.Update();

            if (Program.WaitFrame(10))
            {
                if (upDown == ConsoleKey.W)
                {
                    MoveOffset(0, -1);
                }
                if (upDown == ConsoleKey.S)
                {
                    MoveOffset(0, 1);
                }
                if (leftRight == ConsoleKey.A)
                {
                    MoveOffset(-1, 0);
                }
                if (leftRight == ConsoleKey.D)
                {
                    MoveOffset(1, 0);
                }
            }
            
        }

        public override void OnKeyDown(ConsoleKey key)
        {
            base.OnKeyDown(key);
            if(key == ConsoleKey.W)
            {
                upDown = key;
            }
            if(key == ConsoleKey.S)
            {
                upDown = key;
            }
            if(key == ConsoleKey.A)
            {
                leftRight = key;
            }
            if(key == ConsoleKey.D)
            {
                leftRight = key;
            }
        }
    }
}
