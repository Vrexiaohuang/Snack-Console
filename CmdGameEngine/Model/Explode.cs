using CmdGameEngine.Controller;
using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model
{
    class Explode : GameObject
    {
        public bool Enabled = true;

        public ConsoleColor fColor = ConsoleColor.Yellow;

        public int eCount = 0;

        public override void Update()
        {
            base.Update();

            if (!isOn) return;
            if (!Enabled) return;


            if (Program.WaitFrame(5))
            {
                if (eCount == 3)
                {
                    Enabled = false;
                    Visible = false;
                    return;
                }
                else if (eCount == 2)
                {
                    DrawThree();
                    eCount++;
                }
                else if (eCount == 1)
                {
                    DrawTwo();
                    eCount++;
                }
            }



        }

        public void DrawOne()
        {
            lock (NowScene.drawLock)
            {
                Image.Clear();
                drawTool.SetMousePosition(-1, -1);

                drawTool.bColor = fColor;

                drawTool.WriteLine("   ");
                drawTool.SetMousePosition(-1, 0);
                drawTool.Write(" ");
                drawTool.SetMousePosition(1, 0);
                drawTool.WriteLine(" ");
                drawTool.SetMousePosition(-1, 1);
                drawTool.WriteLine("   ");

                eCount = 1;

                NowScene.Ins.ReFreshGameObject(this);
            }


        }

        public void DrawTwo()
        {
            lock (NowScene.drawLock)
            {
                Image.Clear();
                drawTool.SetMousePosition(-2, -2);

                drawTool.bColor = fColor;

                drawTool.WriteLine("     ");
                drawTool.SetMousePosition(-2, -1);
                drawTool.Write(" ");
                drawTool.SetMousePosition(2, -1);
                drawTool.WriteLine(" ");
                drawTool.SetMousePosition(-2, 0);
                drawTool.Write(" ");
                drawTool.SetMousePosition(2, 0);
                drawTool.WriteLine(" ");
                drawTool.SetMousePosition(-2, 1);
                drawTool.Write(" ");
                drawTool.SetMousePosition(2, 1);
                drawTool.WriteLine(" ");
                drawTool.SetMousePosition(-2, 2);
                drawTool.WriteLine("     ");


                NowScene.Ins.ReFreshGameObject(this);
            }


        }

        public void DrawThree()
        {
            lock (NowScene.drawLock)
            {
                Image.Clear();
                drawTool.SetMousePosition(-3, -3);

                drawTool.bColor = fColor;

                drawTool.WriteLine("       ");
                drawTool.SetMousePosition(-3, -2);
                drawTool.Write(" ");
                drawTool.SetMousePosition(3, -2);
                drawTool.WriteLine(" ");
                drawTool.SetMousePosition(-3, -1);
                drawTool.Write(" ");
                drawTool.SetMousePosition(3, -1);
                drawTool.WriteLine(" ");
                drawTool.SetMousePosition(-3, 0);
                drawTool.Write(" ");
                drawTool.SetMousePosition(3, 0);
                drawTool.WriteLine(" ");
                drawTool.SetMousePosition(-3, 1);
                drawTool.Write(" ");
                drawTool.SetMousePosition(3, 1);
                drawTool.WriteLine(" ");
                drawTool.SetMousePosition(-3, 2);
                drawTool.Write(" ");
                drawTool.SetMousePosition(3, 2);
                drawTool.WriteLine(" ");
                drawTool.SetMousePosition(-3, 3);
                drawTool.WriteLine("       ");

                NowScene.Ins.ReFreshGameObject(this);
            }


        }



    }


}
