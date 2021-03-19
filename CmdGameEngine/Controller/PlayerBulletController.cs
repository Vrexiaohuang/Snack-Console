using CmdGameEngine.GameEngine;
using CmdGameEngine.Model;
using CmdGameEngine.Model.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Controller
{
    class PlayerBulletController
    {
        public Text nowB = null;
        public Text maxB = null;

        public int nowBCount = 3;
        public int maxBCount = 6;

        public List<PlayerBulletFood> bullets = new List<PlayerBulletFood>();

        public void ShotOne(Vector2 pos, Vector2 force, float speed, FoodController fc)
        {
            if(nowBCount <= 0)
            {
                return;
            }
            PlayerBulletFood pbf = new PlayerBulletFood();
            pbf.fc = fc;
            NowScene.Ins.AddObjectToNowScene(pbf);
            pbf.Init();
            pbf.Position = pos;
            pbf.force = force * speed;
            bullets.Add(pbf);
            nowBCount--;

            if(nowB != null && maxB != null)
            {
                nowB.TextValue = nowBCount.ToString();
                maxB.TextValue = maxBCount.ToString();
            }

        }

        public void AddBullet(int count)
        {
            if(nowBCount + count > maxBCount)
            {
                nowBCount = maxBCount;
            }
            else
            {
                nowBCount += count;
            }

            if (nowB != null && maxB != null)
            {
                nowB.TextValue = nowBCount.ToString();
                maxB.TextValue = maxBCount.ToString();
            }
        }

    }
}
