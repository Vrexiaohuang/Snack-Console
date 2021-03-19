using CmdGameEngine.GameEngine;
using CmdGameEngine.Model;
using CmdGameEngine.Model.Buttons;
using CmdGameEngine.Model.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Controller
{
    class Mode2Controller
    {
        public static int RedWin = 0;
        public static int BlueWin = 0;

        public FoodController fc = null;

        public void GameOver(int winner)
        {
            fc.Enabled = false;
            foreach (NormalFood item in fc.allFoods)
            {
                item.Enabled = false;
            }

            if(winner == 1)
            {
                RedWin++;
            }
            else
            {
                BlueWin++;
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
            label1.Layer = 80;
            label1.Position = new Vector2(23, 9);

            Text label2 = new Text();
            label2.FColor = ConsoleColor.Yellow;
            string winnnerStr = winner == 1 ? "红色" : "蓝色";
            label2.TextValue = winnnerStr + "获胜";
            NowScene.Ins.AddObjectToNowScene(label2);
            label2.Layer = 80;
            label2.Position = new Vector2(25 - winnnerStr.Length, 11);

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

        }

        private void Btn2_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitModeChoiceScene();
            NowScene.Ins.LoadScene(SceneController.Ins.modeChoiceScene);
        }

        private void Btn1_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitMode2Scene();
            NowScene.Ins.LoadScene(SceneController.Ins.mode2Scene);
        }
    }
}
