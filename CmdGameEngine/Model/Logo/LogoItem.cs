using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model.Logo
{
    class LogoItem : GameObject
    {
        public override void Init()
        {
            base.Init();

        }

        public override void Update()
        {
            base.Update();
            if (!isOn) return;
            if (Program.WaitFrame(10))
            {
                ConsoleColor rColor = Dice.NextColor();

                for (int i = 0; i < Image.Count; i++)
                {
                    MItem mi = Image[i];
                    mi.fColor = rColor;
                    Image[i] = mi;
                }
                NowScene.Ins.ReFreshGameObject(this);
            }

        }

        public override void OnKeyDown(ConsoleKey key)
        {
            base.OnKeyDown(key);
        }

        public void DrawS()
        {
            drawTool.WriteLine("┏━━┓");
            drawTool.WriteLine("┃┏━┛");
            drawTool.WriteLine("┃┗━┓");
            drawTool.WriteLine("┗━┓┃");
            drawTool.WriteLine("┏━┛┃");
            drawTool.WriteLine("┗━━┛");
        }

        public void DrawN()
        {
            drawTool.WriteLine("┏━━┓");
            drawTool.WriteLine("┃┏┓┃");
            drawTool.WriteLine("┃┃┃┃");
            drawTool.WriteLine("┃┃┃┃");
            drawTool.WriteLine("┃┃┃┃");
            drawTool.WriteLine("┗┛┗┛");
        }

        public void DrawA()
        {
            drawTool.WriteLine("┏━━┓");
            drawTool.WriteLine("┃┏┓┃");
            drawTool.WriteLine("┃┗┛┃");
            drawTool.WriteLine("┃┏┓┃");
            drawTool.WriteLine("┃┃┃┃");
            drawTool.WriteLine("┗┛┗┛");
        }

        public void DrawK()
        {

            drawTool.WriteLine("┏┓┏┓");
            drawTool.WriteLine("┃┗┛┃");
            drawTool.WriteLine("┃┏━┛");
            drawTool.WriteLine("┃┗━┓");
            drawTool.WriteLine("┃┏┓┃");
            drawTool.WriteLine("┗┛┗┛");
        }

        public void DrawE()
        {
            drawTool.WriteLine("┏━━┓");
            drawTool.WriteLine("┃┏┓┃");
            drawTool.WriteLine("┃┗┛┃");
            drawTool.WriteLine("┃┏━┛");
            drawTool.WriteLine("┃┗━┓");
            drawTool.WriteLine("┗━━┛");
        }
    }
}
