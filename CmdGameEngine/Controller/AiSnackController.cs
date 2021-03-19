using CmdGameEngine.GameEngine;
using CmdGameEngine.Model.Snack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Controller
{
    class AiSnackController : GameObject
    {
        //蛇的数量
        int count = 0;

        public override void Update()
        {
            base.Update();

            if (!isOn) return;

            if (Program.WaitFrame(200))
            {
                SpawnASnack();
                count++;
            }
        }

        public void SpawnASnack()
        {
            if (count >= 4) return;

            AiSnackHead head = new AiSnackHead();

            head.isOn = true;
            head.parentScene = NowScene.Ins.nowScene;

            NowScene.Ins.AddObjectToNowScene(head);

            head.Position = Dice.NextV2(1, 47, 1, 39);

            SnackBody lastBody = null;

            for (int i = 0; i < 4; i++)
            {
                SnackBody sb = new SnackBody();

                sb.isOn = true;
                sb.parentScene = NowScene.Ins.nowScene;

                sb.Position = head.Position;

                NowScene.Ins.AddObjectToNowScene(sb);

                if (lastBody == null)
                {
                    sb.target = head;
                }
                else
                {
                    sb.target = lastBody;
                }
                lastBody = sb;
            }

        }

        public AiSnackController Clone()
        {
            AiSnackController obj = CloneGameObject<AiSnackController>();
            obj.count = count;

            return obj;
        }

    }
}
