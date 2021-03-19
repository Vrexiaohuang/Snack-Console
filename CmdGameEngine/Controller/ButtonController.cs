using CmdGameEngine.GameEngine;
using CmdGameEngine.Model;
using CmdGameEngine.Model.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CmdGameEngine.Controller
{
    class ButtonController : GameObject
    {
        #region 单例
        static ButtonController ins = null;

        ButtonController()
        {

        }

        public static ButtonController Ins
        {
            get
            {
                if (ins == null)
                {
                    ins = new ButtonController();
                }
                return ins;
            }
        }
        #endregion

        public ButtonBase selectBtn = null;

        public override void Init()
        {
            base.Init();
        }

        public override void OnKeyDown(ConsoleKey key)
        {
            base.OnKeyDown(key);

            if (selectBtn == null) return;
            //if (!isOn) return;
            if (key == ConsoleKey.UpArrow)
            {
                if (selectBtn.top != null)
                {
                    selectBtn.UnSelect();
                    selectBtn.top.Select();
                }
            }
            if (key == ConsoleKey.DownArrow)
            {
                if (selectBtn.down != null)
                {
                    selectBtn.UnSelect();
                    selectBtn.down.Select();
                }
            }
            if (key == ConsoleKey.LeftArrow)
            {
                if (selectBtn.left != null)
                {
                    selectBtn.UnSelect();
                    selectBtn.left.Select();
                }
            }
            if (key == ConsoleKey.RightArrow)
            {
                if (selectBtn.right != null)
                {
                    selectBtn.UnSelect();
                    selectBtn.right.Select();
                }
            }
            if (key == ConsoleKey.Enter)
            {
                if (selectBtn != null)
                {
                    try
                    {
                        selectBtn.TriggerClick();
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void LoadMainSceneBtn()
        {
            ButtonType1 startBtn = new ButtonType1();
            startBtn.Layer = 10;
            startBtn.selectColor = ConsoleColor.DarkGreen;
            startBtn.Position = new Vector2(-20, 17);
            startBtn.flyToPoint = new Vector2(15, 17);
            startBtn.btnBasePos = startBtn.flyToPoint;
            startBtn.Text = "开始游戏";
            startBtn.canFly = true;
            startBtn.OnClick += StartBtn_OnClick;
            startBtn.parentScene = SceneController.Ins.mainScene;
            SceneController.Ins.mainScene.AddObject(startBtn);

            ButtonType1 rankBtn = new ButtonType1();
            rankBtn.Layer = 10;
            rankBtn.selectColor = ConsoleColor.DarkYellow;
            rankBtn.Position = new Vector2(60, 22);
            rankBtn.flyToPoint = new Vector2(19, 22);
            rankBtn.btnBasePos = rankBtn.flyToPoint;
            rankBtn.Text = "排行榜";
            rankBtn.canFly = true;
            rankBtn.OnClick += RankBtn_OnClick;
            rankBtn.parentScene = SceneController.Ins.mainScene;
            SceneController.Ins.mainScene.AddObject(rankBtn);

            ButtonType1 quitBtn = new ButtonType1();
            quitBtn.Layer = 10;
            quitBtn.selectColor = ConsoleColor.DarkRed;
            quitBtn.Position = new Vector2(-20, 27);
            quitBtn.flyToPoint = new Vector2(23, 27);
            quitBtn.btnBasePos = quitBtn.flyToPoint;
            quitBtn.Text = "退出游戏";
            quitBtn.canFly = true;
            quitBtn.parentScene = SceneController.Ins.mainScene;
            SceneController.Ins.mainScene.AddObject(quitBtn);

            startBtn.down = rankBtn;
            rankBtn.top = startBtn;
            rankBtn.down = quitBtn;
            quitBtn.top = rankBtn;

            quitBtn.OnClick += QuitBtn_OnClick;

            startBtn.Select();
        }

        

        public void LoadModeChoiceSceneBtn()
        {
            ButtonType1 singlePlayer = new ButtonType1();
            singlePlayer.Layer = 10;
            singlePlayer.selectColor = ConsoleColor.DarkGreen;
            singlePlayer.Position = new Vector2(-20, 17);
            singlePlayer.flyToPoint = new Vector2(11, 17);
            singlePlayer.btnBasePos = singlePlayer.flyToPoint;
            singlePlayer.Text = "单人模式";
            singlePlayer.canFly = true;
            singlePlayer.OnClick += SinglePlayer_OnClick; ;
            singlePlayer.parentScene = SceneController.Ins.modeChoiceScene;
            SceneController.Ins.modeChoiceScene.AddObject(singlePlayer);

            ButtonType1 doublePlayer = new ButtonType1();
            doublePlayer.Layer = 10;
            doublePlayer.selectColor = ConsoleColor.DarkYellow;
            doublePlayer.Position = new Vector2(60, 22);
            doublePlayer.flyToPoint = new Vector2(15, 22);
            doublePlayer.btnBasePos = doublePlayer.flyToPoint;
            doublePlayer.Text = "双人模式";
            doublePlayer.canFly = true;
            doublePlayer.OnClick += DoublePlayer_OnClick;
            doublePlayer.parentScene = SceneController.Ins.modeChoiceScene;
            SceneController.Ins.modeChoiceScene.AddObject(doublePlayer);

            ButtonType1 survivalMode = new ButtonType1();
            survivalMode.Layer = 10;
            survivalMode.selectColor = ConsoleColor.DarkRed;
            survivalMode.Position = new Vector2(-20, 27);
            survivalMode.flyToPoint = new Vector2(19, 27);
            survivalMode.btnBasePos = survivalMode.flyToPoint;
            survivalMode.Text = "生存模式";
            survivalMode.canFly = true;
            survivalMode.OnClick += SurvivalMode_OnClick;
            survivalMode.parentScene = SceneController.Ins.modeChoiceScene;
            SceneController.Ins.modeChoiceScene.AddObject(survivalMode);

            ButtonType1 backBtn = new ButtonType1();
            backBtn.Layer = 10;
            backBtn.selectColor = ConsoleColor.DarkBlue;
            backBtn.Position = new Vector2(60, 32);
            backBtn.flyToPoint = new Vector2(23, 32);
            backBtn.btnBasePos = backBtn.flyToPoint;
            backBtn.Text = "返回主菜单";
            backBtn.canFly = true;
            backBtn.OnClick += BackBtn_OnClick;
            backBtn.parentScene = SceneController.Ins.modeChoiceScene;
            SceneController.Ins.modeChoiceScene.AddObject(backBtn);

            singlePlayer.down = doublePlayer;
            doublePlayer.top = singlePlayer;
            doublePlayer.down = survivalMode;
            survivalMode.top = doublePlayer;
            survivalMode.down = backBtn;
            backBtn.top = survivalMode;

            singlePlayer.Select();
        }

        public void LoadrankSceneBtn()
        {
            ButtonType1 backBtn = new ButtonType1();
            backBtn.Layer = 10;
            backBtn.selectColor = ConsoleColor.DarkBlue;
            backBtn.Position = new Vector2(60, 33);
            backBtn.flyToPoint = new Vector2(17, 33);
            backBtn.btnBasePos = backBtn.flyToPoint;
            backBtn.Text = "返回主菜单";
            backBtn.canFly = true;
            backBtn.OnClick += BackBtn_OnClick;
            backBtn.parentScene = SceneController.Ins.rankScene;
            SceneController.Ins.rankScene.AddObject(backBtn);

            backBtn.Select();
        }

        private void RankBtn_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitRankScene();
            NowScene.Ins.LoadScene(SceneController.Ins.rankScene);
        }

        private void SurvivalMode_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitMode3Scene();
            NowScene.Ins.LoadScene(SceneController.Ins.mode3Scene);
        }

        private void DoublePlayer_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitMode2Scene();
            NowScene.Ins.LoadScene(SceneController.Ins.mode2Scene); 
        }

        private void BackBtn_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitMainScene();
            NowScene.Ins.LoadScene(SceneController.Ins.mainScene);
        }

        private void SinglePlayer_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitMode1Scene();
            NowScene.Ins.LoadScene(SceneController.Ins.mode1Scene);
        }

        private void StartBtn_OnClick(ButtonBase btn)
        {
            SceneController.Ins.InitModeChoiceScene();
            NowScene.Ins.LoadScene(SceneController.Ins.modeChoiceScene);
        }

        private void QuitBtn_OnClick(ButtonBase btn)
        {
            Environment.Exit(0);
        }
    }
}
