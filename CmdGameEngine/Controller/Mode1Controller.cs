using CmdGameEngine.GameEngine;
using CmdGameEngine.Model;
using CmdGameEngine.Model.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using CmdGameEngine.Model.Buttons;

namespace CmdGameEngine.Controller
{
    class Mode1Controller : GameObject
    {
        public FoodController fc = null;

        public Mode1ScoreController m1sc;

        public int score = 0;

        public void GameOver()
        {
            score = m1sc.Score;
            fc.Enabled = false;
            foreach (NormalFood item in fc.allFoods)
            {
                item.Enabled = false;
            }
            foreach (SpeedFood item in fc.allSpeedFoods)
            {
                item.Enabled = false;
            }

            Panel overPanel = new Panel(21, 20);
            overPanel.mode = 1;
            NowScene.Ins.AddObjectToNowScene(overPanel);
            overPanel.Init();
            overPanel.Layer = 80;
            overPanel.Position = new Vector2(15, 7);

            Text label1 = new Text();
            label1.FColor = ConsoleColor.Magenta;
            label1.TextValue = "游戏结束";
            NowScene.Ins.AddObjectToNowScene(label1);
            label1.Layer = 81;
            label1.Position = new Vector2(23, 9);

            Text label2 = new Text();
            label2.FColor = ConsoleColor.Yellow;
            label2.TextValue = m1sc.Score.ToString() + "分";
            NowScene.Ins.AddObjectToNowScene(label2);
            label2.Layer = 81;
            label2.Position = new Vector2(25 - m1sc.Score.ToString().Length, 11);

            ButtonType1 btn1 = new ButtonType1();
            btn1.Layer = 100;
            NowScene.Ins.AddObjectToNowScene(btn1);
            btn1.Text = "重新开始";
            btn1.Position = new Vector2(17, 15);
            btn1.btnBasePos = btn1.Position;
            btn1.Select();


            ButtonType1 btn2 = new ButtonType1();
            btn2.Layer = 101;
            NowScene.Ins.AddObjectToNowScene(btn2);
            btn2.Text = "返回模式选择";
            btn2.Position = new Vector2(17, 20);
            btn2.btnBasePos = btn2.Position;

            btn1.down = btn2;
            btn2.top = btn1;

            btn1.OnClick += Btn1_OnClick;
            btn2.OnClick += Btn2_OnClick;

            if(RankController.Ins.m1r.datas.Count == 0 || RankController.Ins.m1r.datas.Count < 6 || score > RankController.Ins.m1r.datas[5].value)
            {
                #region 破纪录
                Panel rankOver = new Panel(15, 12);
                rankOver.mode = 1;
                NowScene.Ins.AddObjectToNowScene(rankOver);
                rankOver.Init();
                rankOver.Layer = 130;
                rankOver.Position = new Vector2(18, 8);

                Text rankOverText1 = new Text();
                rankOverText1.Layer = 131;
                rankOverText1.FColor = ConsoleColor.Yellow;
                rankOverText1.TextValue = "恭喜您进入了排行榜！";
                NowScene.Ins.AddObjectToNowScene(rankOverText1);
                rankOverText1.Position = new Vector2(20, 10);

                Text rankOverText2 = new Text();
                rankOverText2.Layer = 131;
                rankOverText2.FColor = ConsoleColor.Yellow;
                rankOverText2.TextValue = "请留下您的尊贵大名！";
                NowScene.Ins.AddObjectToNowScene(rankOverText2);
                rankOverText2.Position = new Vector2(20, 12);

                Console.ForegroundColor = ConsoleColor.Magenta;

                DrawHelper.Ins.SetMousePosition(24, 15);

                Console.CursorVisible = true;
                string name = Console.ReadLine();
                RankController.Ins.AddMode1Rank(name, score);

                Console.CursorVisible = false;

                rankOver.Visible = false;
                rankOverText1.Visible = false;
                rankOverText2.Visible = false;
                #endregion
            }


        }

        private void Btn2_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitModeChoiceScene();
            NowScene.Ins.LoadScene(SceneController.Ins.modeChoiceScene);
        }

        private void Btn1_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitMode1Scene();
            NowScene.Ins.LoadScene(SceneController.Ins.mode1Scene);
        }
    }
}
