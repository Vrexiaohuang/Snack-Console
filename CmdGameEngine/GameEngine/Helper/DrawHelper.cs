using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.GameEngine
{
    public class DrawHelper
    {
        #region 单例
        static DrawHelper ins = null;

        DrawHelper()
        {

        }

        public static DrawHelper Ins
        {
            get
            {
                if (ins == null)
                {
                    ins = new DrawHelper();
                }
                return ins;
            }
        }
        #endregion

        /// <summary>
        /// 设置鼠标位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetMousePosition(int x, int y)
        {
            Console.SetCursorPosition(x * 2, y);
        }

        /// <summary>
        /// 绘制一格方块
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="fColor"></param>
        /// <param name="bColor"></param>
        public void DrawABlock(int x, int y, string text, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black)
        {
            if (x * 2 < 0 || x * 2 + 2 > Console.BufferWidth || y < 0 || y > Console.BufferHeight) return;
            SetMousePosition(x, y);
            Console.ForegroundColor = fColor;
            Console.BackgroundColor = bColor;
            Console.Write(text);
        }

        public void DrawABlock(MItem mi)
        {
            if ((int)mi.position.X * 2 < 0 || (int)mi.position.X * 2 + 2 > Console.BufferWidth || (int)mi.position.Y < 0 || (int)mi.position.Y > Console.BufferHeight) return;
            SetMousePosition((int)mi.position.X, (int)mi.position.Y);
            Console.ForegroundColor = mi.fColor;
            Console.BackgroundColor = mi.bColor;
            Console.Write(mi.text);
        }

        /// <summary>
        /// 清理一格方块
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="bColor"></param>
        public void ClearABlock(int x, int y, ConsoleColor bColor = ConsoleColor.Black)
        {
            if (x * 2 < 0 || x * 2 + 2 > Console.BufferWidth || y < 0 || y > Console.BufferHeight) return;
            SetMousePosition(x, y);
            Console.BackgroundColor = bColor;
            Console.Write("  ");
        }

        public void ClearABlock(MItem mi, ConsoleColor bColor = ConsoleColor.Black)
        {
            if ((int)mi.position.X * 2 < 0 || (int)mi.position.X * 2 + 2 > Console.BufferWidth || (int)mi.position.Y < 0 || (int)mi.position.Y > Console.BufferHeight) return;
            SetMousePosition((int)mi.position.X, (int)mi.position.Y);
            Console.BackgroundColor = bColor;
            Console.Write("  ");
        }

    }
}
