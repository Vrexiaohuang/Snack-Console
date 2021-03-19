using CmdGameEngine.GameEngine;
using CmdGameEngine.Model.Logo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CmdGameEngine.Controller
{
    class LogoController : GameObject
    {
        int logoCount = 0;

        bool isBegin = false;
        public override void Init()
        {
            base.Init();
            if (!isOn) return;
        }

        public override void Update()
        {
            base.Update();
            if (!isOn) return;
            if (isBegin && Program.WaitFrame(10) && logoCount < 5)
            {
                LogoItem logoItem = new LogoItem();

                NowScene.Ins.AddObjectToNowScene(logoItem);

                logoItem.Layer = 6;

                int dx = 8;

                int offset = 5;

                if (logoCount == 0)
                {
                    logoItem.DrawS();
                    logoItem.Position = new Vector2(dx, -30);
                    logoItem.flyToPoint = new Vector2(dx, 5);
                }
                if (logoCount == 1)
                {
                    logoItem.DrawN();
                    logoItem.Position = new Vector2(dx + offset, -30);
                    logoItem.flyToPoint = new Vector2(dx + offset, 5);
                }
                if (logoCount == 2)
                {
                    logoItem.DrawA();
                    logoItem.Position = new Vector2(dx + offset * 2, -30);
                    logoItem.flyToPoint = new Vector2(dx + offset * 2, 5);
                }
                if (logoCount == 3)
                {
                    logoItem.DrawK();
                    logoItem.Position = new Vector2(dx + offset * 3, -30);
                    logoItem.flyToPoint = new Vector2(dx + offset * 3, 5);
                }
                if (logoCount == 4)
                {
                    logoItem.DrawE();
                    logoItem.Position = new Vector2(dx + offset * 4, -30);
                    logoItem.flyToPoint = new Vector2(dx + offset * 4, 5);
                }

                logoItem.isOn = true;
                logoItem.canFly = true;

                logoCount++;
            }

        }

        public void DrawLogo()
        {
            isBegin = true;
        }

        public LogoController Clone()
        {
            LogoController obj = CloneGameObject<LogoController>();
            obj.logoCount = logoCount;
            obj.isBegin = isBegin;

            return obj;
        }
    }
}
