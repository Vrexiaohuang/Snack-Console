using CmdGameEngine.Controller;
using CmdGameEngine.GameEngine;
using CmdGameEngine.Model.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CmdGameEngine.Model.Snack
{
    class PlayerSnackHead : GameObject
    {
        public bool goTop = false;
        public bool goDown = false;
        public bool goLeft = false;
        public bool goRight = true;

        public bool Enabled = true;

        public int player = 1;

        public FoodController fc = null;

        public PlayerSnackHead otherPlayer = null;

        public List<PlayerSnackBody> allBody = new List<PlayerSnackBody>();

        public PlayerSnackBody lastBody = null;

        public int nowMode = 1;

        public Mode1Controller m1C = null;

        public Mode2Controller m2C = null;

        public Mode3Controller m3C = null;

        public int snackNormalSpeed = 8;

        public int speedChange = 0;

        public KeyItem ki = null;

        public KeyItem jKi = null;

        public bool isJDown = false;

        public PlayerBulletController pbc = new PlayerBulletController();

        public object updateLock = new object();

        ConsoleColor fColor = ConsoleColor.Yellow;
        public ConsoleColor FColor
        {
            get
            {
                return fColor;
            }
            set
            {
                fColor = value;
                if (!isOn) return;
                Init();
            }
        }



        public override void Init()
        {
            base.Init();
            if (!isOn) return;

            drawTool.SetMousePosition(0, 0);
            drawTool.fColor = fColor;
            drawTool.Write("●");//■

            pbc.bullets.Clear();
        }

        public override void Update()
        {
            lock (updateLock)
            {
                base.Update();
                if (!isOn) return;

                if (!Enabled) return;

                if (speedChange < 0)
                {
                    Image.Clear();
                    drawTool.SetMousePosition(0, 0);
                    drawTool.fColor = Dice.NextColor();
                    drawTool.Write("●");
                }
                else
                {
                    Image.Clear();
                    drawTool.SetMousePosition(0, 0);
                    drawTool.fColor = fColor;
                    drawTool.Write("●");
                }

                if (player == 1)
                {
                    if (Input.GetKeyDown(ConsoleKey.UpArrow))
                    {
                        if (!goDown)
                            goTop = true;
                    }
                    else
                    {
                        if (goDown || goLeft || goRight)
                            goTop = false;
                    }
                    if (Input.GetKeyDown(ConsoleKey.DownArrow))
                    {
                        if (!goTop)
                            goDown = true;
                    }
                    else
                    {
                        if (goTop || goLeft || goRight)
                            goDown = false;
                    }
                    if (Input.GetKeyDown(ConsoleKey.LeftArrow))
                    {
                        if (!goRight)
                            goLeft = true;
                    }
                    else
                    {
                        if (goDown || goTop || goRight)
                            goLeft = false;
                    }
                    if (Input.GetKeyDown(ConsoleKey.RightArrow))
                    {
                        if (!goLeft)
                            goRight = true;
                    }
                    else
                    {
                        if (goDown || goLeft || goTop)
                            goRight = false;
                    }

                    if (Input.GetKeyDown(ConsoleKey.NumPad1) && nowMode == 2)
                    {
                        speedChange = -4;
                        if (ki != null && !ki.isDown)
                        {
                            ki.down();
                        }
                    }
                    else if (nowMode == 2)
                    {
                        speedChange = 0;
                        if (ki != null && ki.isDown)
                        {
                            ki.up();
                        }
                    }


                }

                if (player == 2)
                {
                    if (Input.GetKeyDown(ConsoleKey.W))
                    {
                        if (!goDown)
                            goTop = true;
                    }
                    else
                    {
                        if (goDown || goLeft || goRight)
                            goTop = false;
                    }
                    if (Input.GetKeyDown(ConsoleKey.S))
                    {
                        if (!goTop)
                            goDown = true;
                    }
                    else
                    {
                        if (goTop || goLeft || goRight)
                            goDown = false;
                    }
                    if (Input.GetKeyDown(ConsoleKey.A))
                    {
                        if (!goRight)
                            goLeft = true;
                    }
                    else
                    {
                        if (goDown || goTop || goRight)
                            goLeft = false;
                    }
                    if (Input.GetKeyDown(ConsoleKey.D))
                    {
                        if (!goLeft)
                            goRight = true;
                    }
                    else
                    {
                        if (goDown || goLeft || goTop)
                            goRight = false;
                    }

                    if (Input.GetKeyDown(ConsoleKey.J) && nowMode == 2)
                    {
                        speedChange = -4;
                        if (ki != null && !ki.isDown)
                        {
                            ki.down();
                        }
                    }
                    else if (nowMode == 2)
                    {
                        speedChange = 0;
                        if (ki != null && ki.isDown)
                        {
                            ki.up();
                        }
                    }


                    if (Input.GetKeyDown(ConsoleKey.J) && nowMode == 3)
                    {
                        if (jKi != null && !jKi.isDown)
                        {
                            jKi.down();
                        }
                    }
                    else if (nowMode == 3)
                    {
                        if (jKi != null && jKi.isDown)
                        {
                            jKi.up();
                        }
                    }



                    if (Input.GetKeyDown(ConsoleKey.K) && nowMode == 3)
                    {
                        speedChange = -4;
                        if (ki != null && !ki.isDown)
                        {
                            ki.down();
                        }
                    }
                    else if (nowMode == 3)
                    {
                        speedChange = 0;
                        if (ki != null && ki.isDown)
                        {
                            ki.up();
                        }
                    }
                }

                if (Program.WaitFrame(snackNormalSpeed + speedChange))
                {
                    //判断撞对面尾巴
                    if (nowMode == 2 && otherPlayer != null)
                    {
                        if (otherPlayer.allBody.Exists(t => (int)t.Position.X == (int)Position.X && (int)t.Position.Y == (int)Position.Y && t.Visible))
                        {
                            Enabled = false;

                            ExplodeController ec = new ExplodeController();
                            ec.ExploadOn(Position, ConsoleColor.DarkRed);
                            if (nowMode == 2 && m2C != null)
                            {
                                m2C.GameOver(player == 1 ? 2 : 1);
                                otherPlayer.Enabled = false;
                            }
                            return;
                        }
                    }
                    if (nowMode == 2)
                    {
                        if (goTop)
                        {
                            if (!(Position.Y - 1 < 4))
                            {
                                MoveOffset(0, -1);
                            }

                        }
                        if (goDown)
                        {
                            if (!(Position.Y + 1 > 29))
                            {
                                MoveOffset(0, 1);
                            }

                        }
                        if (goLeft)
                        {
                            if (!(Position.X - 1 < 4))
                            {
                                MoveOffset(-1, 0);
                            }

                        }
                        if (goRight)
                        {
                            if (!(Position.X + 1 > 45))
                            {
                                MoveOffset(1, 0);
                            }

                        }
                    }
                    else
                    {
                        if (goTop)
                        {
                            MoveOffset(0, -1);
                        }
                        if (goDown)
                        {
                            MoveOffset(0, 1);
                        }
                        if (goLeft)
                        {
                            MoveOffset(-1, 0);
                        }
                        if (goRight)
                        {
                            MoveOffset(1, 0);
                        }
                    }


                    //判断撞墙
                    if (Position.X < 4 || Position.X > 45 || Position.Y < 4 || Position.Y > 29)
                    {
                        if (nowMode == 1 || nowMode == 3)
                        {
                            Enabled = false;
                            ExplodeController ec = new ExplodeController();
                            ec.ExploadOn(Position, ConsoleColor.DarkRed);
                            if (nowMode == 1 && m1C != null)
                            {
                                m1C.GameOver();
                            }
                            if (nowMode == 3 && m3C != null)
                            {
                                m3C.GameOver();
                            }
                            return;
                        }
                        else if (nowMode == 2)
                        {
                            if (Position.X < 4 || Position.X > 45)
                            {
                                if (Position.Y < 4)
                                {
                                    goDown = true;
                                }
                                else
                                {
                                    goTop = true;
                                }

                                if (goLeft)
                                {
                                    goLeft = false;

                                }
                                else
                                {
                                    goRight = false;
                                }
                            }
                            if (Position.Y < 4 || Position.Y > 29)
                            {
                                if (Position.X < 4)
                                {
                                    goRight = true;
                                }
                                else
                                {
                                    goLeft = true;
                                }
                                if (goTop)
                                {
                                    goTop = false;

                                }
                                else
                                {
                                    goDown = false;
                                }
                            }
                        }

                    }

                    //判断撞自己尾巴

                    if (allBody.Exists(t => (int)t.Position.X == (int)Position.X && (int)t.Position.Y == (int)Position.Y && t.Visible && allBody.IndexOf(t) != 0))
                    {
                        Enabled = false;
                        ExplodeController ec = new ExplodeController();
                        ec.ExploadOn(Position, ConsoleColor.DarkRed);
                        if (nowMode == 1 && m1C != null)
                        {
                            m1C.GameOver();
                        }
                        if (nowMode == 2 && m2C != null)
                        {
                            m2C.GameOver(player == 1 ? 2 : 1);
                            otherPlayer.Enabled = false;
                        }
                        if (nowMode == 3 && m3C != null)
                        {
                            m3C.GameOver();
                        }
                        return;
                    }





                    //判断吃食
                    if (fc != null && fc.allFoods.Exists(t => ((int)t.Position.X >= (int)Position.X - 1 && (int)t.Position.X <= (int)Position.X + 1) && ((int)t.Position.Y >= (int)Position.Y - 1 && (int)t.Position.Y <= (int)Position.Y + 1) && t.Visible))
                    {
                        NormalFood nf = fc.allFoods.Where(t => ((int)t.Position.X >= (int)Position.X - 1 && (int)t.Position.X <= (int)Position.X + 1) && ((int)t.Position.Y >= (int)Position.Y - 1 && (int)t.Position.Y <= (int)Position.Y + 1) && t.Visible).FirstOrDefault();

                        ExplodeController ec = new ExplodeController();

                        if (nowMode == 1)
                        {
                            MsgBoxController msgBoxController = new MsgBoxController();
                            msgBoxController.ShowMsgBox(Position + new Vector2(-1, -2), "+1");
                        }

                        if(nowMode == 3)
                        {
                            if(pbc != null)
                            {
                                pbc.AddBullet(1);
                            }
                        }

                        fc.allFoods.Remove(nf);
                        ec.ExploadOn(Position, nf.fColor);

                        nf.Clear();
                        AddOneBody(nf.fColor);

                        if (m1C != null)
                        {
                            m1C.m1sc.Score++;
                        }

                        

                    }

                    //判断吃速度Buff
                    if (fc != null && fc.allSpeedFoods.Exists(t => ((int)t.Position.X >= (int)Position.X - 1 && (int)t.Position.X <= (int)Position.X + 1) && ((int)t.Position.Y >= (int)Position.Y - 1 && (int)t.Position.Y <= (int)Position.Y + 1) && t.Visible))
                    {
                        SpeedFood sf = fc.allSpeedFoods.Where(t => ((int)t.Position.X >= (int)Position.X - 1 && (int)t.Position.X <= (int)Position.X + 1) && ((int)t.Position.Y >= (int)Position.Y - 1 && (int)t.Position.Y <= (int)Position.Y + 1) && t.Visible).FirstOrDefault();

                        ExplodeController ec = new ExplodeController();

                        if (nowMode == 1)
                        {
                            MsgBoxController msgBoxController = new MsgBoxController();
                            msgBoxController.ShowMsgBox(Position + new Vector2(-1, -2), "加速");
                        }
                        fc.allSpeedFoods.Remove(sf);
                        ec.ExploadOn(Position, ConsoleColor.White);

                        sf.Clear();

                        SpeedUp(4, 3000);

                    }

                    //判断速度buff撞自己尾巴
                    foreach (SpeedFood item1 in fc.allSpeedFoods)
                    {
                        foreach (PlayerSnackBody item2 in allBody)
                        {
                            if ((int)item1.Position.X == (int)item2.Position.X && (int)item1.Position.Y == (int)item2.Position.Y && item1.Visible == true)
                            {
                                DelOneBody();
                                ExplodeController ec = new ExplodeController();

                                if (nowMode == 1)
                                {
                                    MsgBoxController msgBoxController = new MsgBoxController();
                                    msgBoxController.ShowMsgBox(item2.Position + new Vector2(-1, -2), "-1");
                                }
                                if (m1C != null)
                                {
                                    m1C.m1sc.Score--;
                                }


                                ec.ExploadOn(item2.Position, ConsoleColor.DarkRed);
                                goto Out;
                            }
                        }
                    }
                Out: { }

                    //判断子弹撞自己尾巴
                    foreach (BulletFood item1 in fc.allBulletFoods)
                    {
                        foreach (PlayerSnackBody item2 in allBody)
                        {
                            if (((int)item1.Position.X >= (int)item2.Position.X - 1 && (int)item1.Position.X <= (int)item2.Position.X + 1) && ((int)item1.Position.Y >= (int)item2.Position.Y - 1 && (int)item1.Position.Y <= (int)item2.Position.Y + 1) && item1.Visible)
                            {

                                DelOneBody();
                                ExplodeController ec = new ExplodeController();
                                ec.ExploadOn(item2.Position, ConsoleColor.DarkRed);

                                item1.Clear();

                                fc.allBulletFoods.Remove(item1);

                                if (allBody.Count == 0 && nowMode == 3)
                                {
                                    Enabled = false;
                                    m3C.GameOver();
                                }

                                goto Out1;
                            }
                        }
                    }
                Out1: { }

                    //判断撞子弹
                    if (fc.allBulletFoods.Exists(t => (int)t.Position.X == (int)Position.X && (int)t.Position.Y == (int)Position.Y && t.Visible))
                    {
                        Enabled = false;
                        ExplodeController ec = new ExplodeController();
                        ec.ExploadOn(Position, ConsoleColor.DarkRed);
                        //if (nowMode == 1 && m1C != null)
                        //{
                        //    m1C.GameOver();
                        //}
                        //if (nowMode == 2 && m2C != null)
                        //{
                        //    m2C.GameOver(player == 1 ? 2 : 1);
                        //    otherPlayer.Enabled = false;
                        //}
                        if (nowMode == 3 && m3C != null)
                        {
                            m3C.GameOver();
                        }
                        return;
                    }
                }
            }


        }

        System.Timers.Timer speedUpTimer = new System.Timers.Timer();

        public void SpeedUp(int fastMs, int time)
        {
            speedUpTimer.Elapsed -= SpeedUpTimer_Elapsed;
            speedUpTimer.Elapsed += SpeedUpTimer_Elapsed;

            speedUpTimer.Interval = time;
            speedChange = -fastMs;

            speedUpTimer.Start();
        }

        private void SpeedUpTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            speedChange = 0;
            speedUpTimer.Stop();
        }

        public void DelOneBody()
        {
            if (allBody.Count == 0) return;

            PlayerSnackBody psb = allBody.Last();
            psb.target = null;
            psb.Visible = false;
            psb.isOn = false;
            allBody.Remove(psb);
            if (allBody.Count != 0)
            {
                lastBody = allBody.Last();
            }
            else
            {
                lastBody = null;
            }


        }

        public void AddOneBody(ConsoleColor color = ConsoleColor.Yellow)
        {
            PlayerSnackBody psb = new PlayerSnackBody();
            NowScene.Ins.AddObjectToNowScene(psb);
            psb.Init();

            if (lastBody == null)
            {
                psb.target = this;
                psb.followHead = true;
                psb.player = this;
            }
            else
            {
                psb.target = lastBody;
                psb.player = this;
            }

            allBody.Add(psb);

            for (int i = 0; i < allBody.Count; i++)
            {
                if (i % 2 == 0)
                {
                    allBody[i].FColor = ConsoleColor.White;
                }
                else
                {
                    allBody[i].FColor = color;
                }

                allBody[i].Init();
            }

            lastBody = psb;
        }

        public override void OnKeyDown(ConsoleKey key)
        {
            base.OnKeyDown(key);

            if (!isOn) return;

            if (key == ConsoleKey.J && nowMode == 3)
            {
                if (!(fc.allBulletFoods.Count == 0))
                {
                    BulletFood bf = fc.GetNearBullet(Position);

                    Vector2 v = Vector2.Normalize(Vector2.Lerp(Position, bf.Position, 0.1f) - Position);

                    pbc.ShotOne(Position, v, 0.5f, fc);
                }
            }

        }

        public PlayerSnackHead Clone()
        {
            PlayerSnackHead obj = CloneGameObject<PlayerSnackHead>();
            obj.goTop = goTop;
            obj.goDown = goDown;
            obj.goLeft = goLeft;
            obj.goRight = goRight;

            obj.Enabled = Enabled;

            obj.player = player;

            obj.fc = fc;

            obj.otherPlayer = otherPlayer;

            return obj;
        }
    }
}
