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
    class ExplodeController
    {
        public void ExploadOn(Vector2 v2, ConsoleColor color = ConsoleColor.Yellow)
        {
            Explode e = new Explode();

            NowScene.Ins.AddObjectToNowScene(e);
            e.Position = v2;
            e.Layer = 60;
            e.fColor = color;
            e.DrawOne();
        }
    }
}
