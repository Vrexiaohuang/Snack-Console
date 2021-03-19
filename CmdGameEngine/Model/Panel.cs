using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model
{
    [Serializable]
    public class Panel : GameObject
    {
        public int width = 10;
        public int height = 10;

        /// <summary>
        /// 模式，0代表楼空，1代表填满
        /// </summary>
        public int mode = 0;

        public ConsoleColor borderColor = ConsoleColor.Cyan;

        public Panel()
        {

        }

        public Panel(int width, int height)
        {
            this.width = width;
            this.height = height;
            Init();
        }

        public override void Init()
        {
            base.Init();

            Image.Clear();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if ((i == 0 && j == 0) || (i == 0 && j == height - 1) || (i == width - 1 && j == 0) || (i == width - 1 && j == height - 1))
                    {
                        drawTool.fColor = ConsoleColor.Magenta;
                        drawTool.SetMousePosition(i, j);
                        drawTool.Write("※");
                        drawTool.fColor = ConsoleColor.White;
                    }
                    else if ((j == 0 || j == height - 1) && !(i == 0 || i == width - 1))
                    {
                        drawTool.fColor = borderColor;
                        drawTool.SetMousePosition(i, j);
                        drawTool.Write("┉");
                        drawTool.fColor = ConsoleColor.White;
                    }
                    else if (!(j == 0 || j == height - 1) && (i == 0 || i == width - 1))
                    {
                        drawTool.fColor = borderColor;
                        drawTool.SetMousePosition(i, j);
                        drawTool.Write("┋");
                        drawTool.fColor = ConsoleColor.White;
                    }
                    else if(mode == 1)
                    {
                        drawTool.SetMousePosition(i, j);
                        drawTool.Write(" ");
                        drawTool.fColor = ConsoleColor.White;
                    }
                }
            }

            NowScene.Ins.ReFreshGameObject(this);
        }

        public Panel Clone()
        {
            Panel p = CloneGameObject<Panel>();
            p.width = width;
            p.height = height;
            p.mode = mode;
            p.borderColor = borderColor;

            return p;
        }

    }
}
