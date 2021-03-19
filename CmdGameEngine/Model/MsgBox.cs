using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CmdGameEngine.Model
{
    class MsgBox : GameObject
    {
        /// <summary>
        /// 持续时间，0为常亮
        /// </summary>
        public int showTime = 0;

        public ConsoleColor borderColor = ConsoleColor.Cyan;

        public ConsoleColor textColor = ConsoleColor.Yellow;

        public Timer t = new Timer();

        string text = "";
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                Init();
            }
        }

        public override void Init()
        {
            base.Init();
            if (!isOn) return;

            int width = text.Length + 2;
            int height = 3;

            #region 画边框
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
                    if ((j == 0 || j == height - 1) && !(i == 0 || i == width - 1))
                    {
                        drawTool.fColor = borderColor;
                        drawTool.SetMousePosition(i, j);
                        drawTool.Write("┉");
                        drawTool.fColor = ConsoleColor.White;
                    }

                    if (!(j == 0 || j == height - 1) && (i == 0 || i == width - 1))
                    {
                        drawTool.fColor = borderColor;
                        drawTool.SetMousePosition(i, j);
                        drawTool.Write("┋");
                        drawTool.fColor = ConsoleColor.White;
                    }
                }
            }
            #endregion

            #region 画文字
            drawTool.SetMousePosition(1, 1);
            drawTool.fColor = textColor;
            drawTool.Write(text);
            #endregion

            t.Interval = showTime;
            t.Elapsed -= T_Elapsed;
            t.Elapsed += T_Elapsed;
            if (showTime != 0)
            {
                t.Start();
            }

            NowScene.Ins.ReFreshGameObject(this);

        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            Visible = false;
            t.Stop();
        }

        public override void Update()
        {
            base.Update();
            if (!isOn) return;
            
        }

        public override void OnKeyDown(ConsoleKey key)
        {
            base.OnKeyDown(key);
            if (!isOn) return;
        }
    }
}
