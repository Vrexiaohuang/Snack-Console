using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model.Snack
{
    class AiSnackHead : GameObject
    {
        public GameObject target = null;

        public Vector2 targetV2;

        public override void Init()
        {
            base.Init();

            Image.Clear();

            flySpeed = 0.01f;

            Layer = 2;
            drawTool.fColor = ConsoleColor.Green;
            drawTool.Write("●");

            targetV2 = Dice.NextV2(1, 47, 1, 39);
        }

        public override void Update()
        {
            base.Update();

            if (!isOn) return;

            if (!canFly)
            {
                //随机另一个位置
                while (true)
                {
                    targetV2 = Dice.NextV2(1, 47, 1, 39);
                    if (targetV2.X > 10 && targetV2.X < 40 && targetV2.Y > 10 && targetV2.Y < 32) continue;
                    break;
                }
                
            }

            if (targetV2 == null) return;

            if (Vector2.Distance(Position, targetV2) <= 1f)
            {
                canFly = false;
                return;
            }
            FlyTo(targetV2);

        }

        public override void OnKeyDown(ConsoleKey key)
        {
            base.OnKeyDown(key);
        }
    }
}
