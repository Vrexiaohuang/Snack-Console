using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using CmdGameEngine.Test;
using CmdGameEngine.Controller;
using System.Timers;
using System.Runtime.InteropServices;

/// <summary>
/// 控制台游戏引擎 v1.0
/// BY：黄琨智
/// 
/// 自己的游戏物体请继承GameObject
/// 
/// 具体实现请看TestObj1和2！
/// </summary>

namespace CmdGameEngine
{
    class Program
    {

        #region 引擎字段
        public delegate void UpdateDel();
        public static UpdateDel updateDel;

        public delegate void KeyDel(ConsoleKey key);
        public static KeyDel keyDel;

        public static int runningTime = 0;

        public static int nowFrame = 0;

        public static object keyLock = new object();
        #endregion

        /// <summary>
        /// 游戏初始化方法，可以在里面写你的游戏对象和场景
        /// </summary>
        public static void Start()
        {
            #region 设置窗口大小
            Console.WindowHeight = 42;
            Console.WindowWidth = 100;

            Console.BufferHeight = 42;
            Console.BufferWidth = 100;
            #endregion

            #region 各模块初始化
            SceneController sc = SceneController.Ins;

            RankController rc = RankController.Ins;

            //sc.Init();

            sc.LoadStartScene();

            #endregion
        }

        #region 底层，引擎相关


        [DllImport("user32.dll")]
        public static extern short GetKeyState(int keyCode);
        public static bool CheckKey(ConsoleKey key)
        {
            return (GetKeyState((int)key) & 0x8000) != 0;
        }

        static void Main(string[] args)
        {
            Console.Title = "Snake 贪吃蛇！ By：黄琨智";
            SetWindowPositionCenter();
            Console.CursorVisible = false;
            Start();

            #region 定时垃圾清理
            System.Timers.Timer gcT = new System.Timers.Timer();
            gcT.Elapsed += GcT_Elapsed;
            gcT.Interval = 10000;
            gcT.Start();
            #endregion

            #region 按键委托执行
            Thread keyThread = new Thread(() =>
            {
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    lock (keyLock)
                    {
                        keyDel.Invoke(key);
                    }
                    

                }
            });
            keyThread.Start();
            #endregion

            #region Update委托执行
            while (true)
            {
                if (Environment.TickCount - runningTime > 5)
                {
                    runningTime = Environment.TickCount;
                    nowFrame += 1;

                    lock (keyLock)
                    {
                        updateDel.Invoke();
                    }

                }
            }
            #endregion

        }

        private static void GcT_Elapsed(object sender, ElapsedEventArgs e)
        {
            GC.Collect();
        }

        public static bool WaitFrame(int frame)
        {
            if (nowFrame % frame == 0) return true;
            return false;
        }
        #endregion


        #region 设置窗体显示
        private struct RECT { public int left, top, right, bottom; }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

        /// <summary>
        /// 控制台窗体居中
        /// </summary>
        public static void SetWindowPositionCenter()
        {
            IntPtr hWin = GetConsoleWindow();
            RECT rc;
            GetWindowRect(hWin, out rc);

            MoveWindow(hWin, 500, 150, rc.right - rc.left, rc.bottom - rc.top, true);
        }
        #endregion

    }
}
