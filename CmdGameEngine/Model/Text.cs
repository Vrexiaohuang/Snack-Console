using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model
{
    class Text : GameObject
    {
        string text = "";

        public string TextValue
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                Init();

                if (isOn)
                {
                    NowScene.Ins.ReFreshGameObject(this);
                }
            }
        }

        ConsoleColor fColor = ConsoleColor.White;
        public ConsoleColor FColor
        {
            get
            {
                return fColor;
            }
            set
            {
                fColor = value;
                Init();

                if (isOn)
                {
                    NowScene.Ins.ReFreshGameObject(this);
                }
            }
        }
        public override void Init()
        {
            base.Init();

            Image.Clear();

            drawTool.SetMousePosition(0, 0);
            drawTool.fColor = fColor;
            drawTool.Write(text);
        }

        public Text Clone()
        {
            Text obj = CloneGameObject<Text>();
            obj.text = text;
            obj.fColor = fColor;

            return obj;
        }
    }
}
