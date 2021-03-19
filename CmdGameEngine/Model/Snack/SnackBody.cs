using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model.Snack
{
    class SnackBody : GameObject
    {
        public GameObject target = null;
        public override void Init()
        {
            base.Init();

            Image.Clear();

            Layer = 2;
            drawTool.fColor = ConsoleColor.DarkGreen;
            drawTool.Write("⊙");
        }

        public override void Update()
        {
            base.Update();

            if (!isOn) return;

            if (target == null) return;

            if (Vector2.Distance(Position, target.Position) <= 1f)
            {
                canFly = false;
                return;
            }
            FlyTo(target.Position);
        }
    }
}
