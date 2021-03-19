using CmdGameEngine.Controller;
using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model
{
    class Star : GameObject
    {
        public bool canGo = true;

        public StarBgController parentSBC = null;
        public override void Init()
        {
            base.Init();
            Image.Clear();
            drawTool.SetMousePosition(0, 0);
            Layer = 0;
            drawTool.fColor = Dice.NextColor();
            drawTool.Write("─");
        }

        public override void Update()
        {
            base.Update();
            if (!isOn) return;
            if (!canGo)
            {
                Visible = false;
                if(parentSBC != null)
                {
                    parentSBC.stars.Remove(this);
                }
                parentSBC = null;
                Program.updateDel -= Update;
                Program.updateDel -= base.Update;
                Program.keyDel -= OnKeyDown;
                Program.keyDel -= base.OnKeyDown;

                drawTool = null;
                parentScene = null;

                NowScene.Ins.nowScene.allObject.Remove(this);
                return;
            }

        }

        public override void OnKeyDown(ConsoleKey key)
        {
            base.OnKeyDown(key);
        }

        ~Star()
        {
            
        }
    }
}
