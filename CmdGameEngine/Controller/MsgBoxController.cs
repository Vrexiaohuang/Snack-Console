using CmdGameEngine.GameEngine;
using CmdGameEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Controller
{
    class MsgBoxController
    {
        public void ShowMsgBox(Vector2 pos, string text, int showTime = 1000, ConsoleColor fColor = ConsoleColor.Yellow, ConsoleColor borderColor = ConsoleColor.Blue)
        {
            MsgBox mb = new MsgBox();
            NowScene.Ins.AddObjectToNowScene(mb);
            mb.Layer = 61;
            mb.Position = pos;
            mb.textColor = fColor;
            mb.showTime = showTime;
            mb.borderColor = borderColor;
            mb.Text = text;
        }
    }
}
