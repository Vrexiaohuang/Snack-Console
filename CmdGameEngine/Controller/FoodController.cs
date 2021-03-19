using CmdGameEngine.GameEngine;
using CmdGameEngine.Model.Food;
using CmdGameEngine.Model.Snack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CmdGameEngine.Controller
{
    class FoodController : GameObject
    {
        public bool Enabled = true;

        public List<NormalFood> allFoods = new List<NormalFood>();

        public List<SpeedFood> allSpeedFoods = new List<SpeedFood>();

        public List<BulletFood> allBulletFoods = new List<BulletFood>();

        public Timer t = new Timer();

        public PlayerSnackHead followHead = null;


        public bool isInsNormalFood = true;

        public bool isInsSpeedFood = false;

        public bool isInsBulletFood = false;


        public override void Init()
        {
            base.Init();
            t.Interval = 2000;
            t.Elapsed -= T_Elapsed;
            t.Elapsed += T_Elapsed;
            if (!isOn) return;
            if (!Enabled) return;
            t.Start();

        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!Enabled)
            {
                t.Stop();
                return;
            }

            //随机一种食物
            int rNum = Dice.Next(1, 100);
            if (rNum < 20 && isInsSpeedFood)
            {
                SpeedFood sf = new SpeedFood();
                NowScene.Ins.AddObjectToNowScene(sf);
                sf.target = followHead;
                sf.Init();
                sf.RandomPos();
                allSpeedFoods.Add(sf);
            }
            else if (rNum < 67 && isInsBulletFood)
            {
                BulletFood bf = new BulletFood();
                NowScene.Ins.AddObjectToNowScene(bf);
                bf.Init();
                bf.RandomPos();
                bf.RandomForce();
                allBulletFoods.Add(bf);

            }
            else
            {
                NormalFood nf = new NormalFood();
                nf.fColor = Dice.NextColor();
                NowScene.Ins.AddObjectToNowScene(nf);
                nf.Init();
                nf.RandomPos();
                nf.RandomForce();
                allFoods.Add(nf);
            }
        }

        public override void Update()
        {
            base.Update();
            if (!isOn) return;
            if (!Enabled)
            {
                t.Stop();
            }

        }

        public BulletFood GetNearBullet(Vector2 pos)
        {
            BulletFood bf = null;
            foreach (BulletFood item in allBulletFoods)
            {
                if (bf == null)
                {
                    bf = item;
                    continue;
                }
                if (Vector2.Distance(item.Position, pos) < Vector2.Distance(bf.Position, pos))
                {
                    bf = item;
                }
            }
            return bf;
        }


    }
}
