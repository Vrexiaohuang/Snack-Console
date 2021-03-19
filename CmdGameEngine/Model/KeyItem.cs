using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model
{
    class KeyItem : GameObject
    {
        public string keyStr = "A";

        public ConsoleColor upColor = ConsoleColor.Red;

        public ConsoleColor downColor = ConsoleColor.DarkRed;

        public bool isDown = false;

        public override void Init()
        {
            base.Init();
            if (!isOn) return;

            Image.Clear();

            drawTool.SetMousePosition(0, 0);

            if (!isDown)
            {
                drawTool.fColor = upColor;
            }
            else
            {
                drawTool.fColor = downColor;
            }

            drawTool.WriteLine("┏━┓");
            drawTool.Write("┃");
            drawTool.Write(keyStr);
            drawTool.WriteLine("┃");
            drawTool.WriteLine("┗━┛");

            NowScene.Ins.ReFreshGameObject(this);

        }

        public override void Update()
        {
            base.Update();
            if (!isOn) return;
        }

        public void up()
        {
            isDown = false;
            Init();
        }

        public void down()
        {
            isDown = true;
            Init();
        }
    }
}
