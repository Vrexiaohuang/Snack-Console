using CmdGameEngine.GameEngine;
using CmdGameEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using CmdGameEngine.Model.Snack;
using System.Windows.Forms;

namespace CmdGameEngine.Controller
{
    class SceneController
    {
        #region 单例
        static SceneController ins = null;

        SceneController()
        {
            //Init();
        }

        public static SceneController Ins
        {
            get
            {
                if (ins == null)
                {
                    ins = new SceneController();
                }
                return ins;
            }
        }
        #endregion

        public Scene mainScene = null;

        public Scene modeChoiceScene = null;

        public Scene mode1Scene = null;

        public Scene mode2Scene = null;

        public Scene mode3Scene = null;

        public Scene rankScene = null;

        public void Init()
        {

        }

        public void InitMainScene()
        {
            #region 生成MainScene
            mainScene = new Scene();

            #region GameObject处理
            //背景
            Model.Panel bg = new Model.Panel(48, 40);
            bg.Position = new Vector2(1, 1);

            //logo控制器
            LogoController lc = new LogoController();
            lc.DrawLogo();

            //ai蛇控制器
            AiSnackController ac = new AiSnackController();

            //星空背景控制器
            StarBgController sbc = new StarBgController();

            //按钮初始化
            ButtonController bc = ButtonController.Ins;
            bc.LoadMainSceneBtn();

            mainScene.AddObject(bg);
            mainScene.AddObject(lc);
            mainScene.AddObject(ac);
            mainScene.AddObject(sbc);
            #endregion


            #endregion
        }

        public void InitModeChoiceScene()
        {
            #region 生成ModeChoiceScene
            modeChoiceScene = new Scene();

            //背景
            Model.Panel bg = new Model.Panel(48, 40);
            bg.Position = new Vector2(1, 1);

            //星空背景控制器
            StarBgController sbc2 = new StarBgController();

            //选择游戏模式
            Model.Panel p2 = new Model.Panel(12, 5);
            p2.Position = new Vector2(18, 7);
            Text text1 = new Text();
            text1.TextValue = "选择游戏模式";
            text1.FColor = ConsoleColor.Yellow;
            text1.Position = new Vector2(21, 9);

            p2.Layer = 3;
            text1.Layer = 4;

            modeChoiceScene.AddObject(bg);
            modeChoiceScene.AddObject(sbc2);
            modeChoiceScene.AddObject(p2);
            modeChoiceScene.AddObject(text1);

            //按钮初始化
            ButtonController bc = ButtonController.Ins;
            bc.LoadModeChoiceSceneBtn();
            #endregion
        }

        public void InitMode1Scene()
        {
            #region 生成单人模式地图
            mode1Scene = new Scene();

            //背景
            Model.Panel bg = new Model.Panel(48, 40);
            bg.Position = new Vector2(1, 1);

            Model.Panel gamePanel = new Model.Panel(44, 28);
            gamePanel.borderColor = ConsoleColor.Red;
            gamePanel.Position = new Vector2(3, 3);

            PlayerSnackHead psh = new PlayerSnackHead();
            psh.Layer = 30;
            psh.Position = new Vector2(10, 13);

            FoodController fc = new FoodController();
            fc.followHead = psh;
            fc.isInsSpeedFood = true;

            Mode1Controller m1c = new Mode1Controller();
            m1c.fc = fc;

            psh.m1C = m1c;

            psh.fc = fc;

            #region 分数面板

            Model.Panel infoPanel = new Model.Panel(32, 8);
            infoPanel.Position = new Vector2(3, 31);

            Text label2 = new Text();
            label2.Position = new Vector2(5, 33);
            label2.FColor = ConsoleColor.White;
            label2.TextValue = "操作：↑↓←→控制蛇移动";

            Model.Panel scorePanel = new Model.Panel(12, 8);
            scorePanel.Position = new Vector2(35, 31);

            Text label1 = new Text();
            label1.Position = new Vector2(40, 33);
            label1.FColor = ConsoleColor.Magenta;
            label1.TextValue = "分数：";

            Text scoreText = new Text();
            scoreText.Position = new Vector2(40, 35);
            scoreText.FColor = ConsoleColor.Yellow;
            scoreText.TextValue = "0";

            Mode1ScoreController m1sc = new Mode1ScoreController();
            m1sc.scoreText = scoreText;

            m1c.m1sc = m1sc;
            #endregion

            mode1Scene.AddObject(infoPanel);
            mode1Scene.AddObject(label1);
            mode1Scene.AddObject(label2);
            mode1Scene.AddObject(scorePanel);
            mode1Scene.AddObject(scoreText);
            mode1Scene.AddObject(m1c);
            mode1Scene.AddObject(bg);
            mode1Scene.AddObject(fc);
            mode1Scene.AddObject(gamePanel);
            mode1Scene.AddObject(psh);

            #endregion
        }

        public void InitMode2Scene()
        {
            #region 生成双人模式地图
            mode2Scene = new Scene();

            //背景
            Model.Panel bg = new Model.Panel(48, 40);
            bg.Position = new Vector2(1, 1);

            Model.Panel gamePanel = new Model.Panel(44, 28);
            gamePanel.borderColor = ConsoleColor.Red;
            gamePanel.Position = new Vector2(3, 3);

            PlayerSnackHead psh1 = new PlayerSnackHead();

            psh1.Layer = 30;
            psh1.nowMode = 2;
            psh1.FColor = ConsoleColor.Red;
            psh1.Position = new Vector2(36, 13);
            psh1.goRight = false;
            psh1.goLeft = true;


            PlayerSnackHead psh2 = new PlayerSnackHead();
            psh2.Layer = 30;
            psh2.nowMode = 2;
            psh2.Position = new Vector2(10, 13);
            psh2.FColor = ConsoleColor.Blue;
            psh2.goRight = true;
            psh2.player = 2;

            psh1.otherPlayer = psh2;
            psh2.otherPlayer = psh1;

            Mode2Controller m2c = new Mode2Controller();

            psh1.m2C = m2c;
            psh2.m2C = m2c;

            FoodController fc = new FoodController();

            m2c.fc = fc;

            psh1.fc = fc;
            psh2.fc = fc;


            Model.Panel infoPanel = new Model.Panel(32, 8);
            infoPanel.Position = new Vector2(3, 31);

            Model.Panel scorePanel = new Model.Panel(12, 8);
            scorePanel.Position = new Vector2(35, 31);

            Text label1 = new Text();
            label1.FColor = ConsoleColor.Yellow;
            label1.TextValue = "胜场数";
            label1.Position = new Vector2(39, 32);

            Text label2 = new Text();
            label2.FColor = ConsoleColor.Blue;
            label2.TextValue = "蓝方：";
            label2.Position = new Vector2(38, 34);

            Text label3 = new Text();
            label3.FColor = ConsoleColor.Red;
            label3.TextValue = "红方：";
            label3.Position = new Vector2(38, 36);

            Text blueWin = new Text();
            blueWin.FColor = ConsoleColor.Yellow;
            blueWin.TextValue = Mode2Controller.BlueWin.ToString();
            blueWin.Position = new Vector2(41, 34);

            Text redWin = new Text();
            redWin.FColor = ConsoleColor.Yellow;
            redWin.TextValue = Mode2Controller.RedWin.ToString();
            redWin.Position = new Vector2(41, 36);


            KeyItem ki1 = new KeyItem();
            ki1.keyStr = "J";
            ki1.Position = new Vector2(8, 34);
            ki1.upColor = ConsoleColor.Blue;
            ki1.downColor = ConsoleColor.DarkBlue;

            psh2.ki = ki1;

            KeyItem ki2 = new KeyItem();
            ki2.keyStr = "1";
            ki2.Position = new Vector2(27, 34);

            psh1.ki = ki2;

            Text label4 = new Text();
            label4.Position = new Vector2(8, 33);
            label4.TextValue = "加速！";
            label4.FColor = ConsoleColor.Yellow;

            Text label5 = new Text();
            label5.Position = new Vector2(27, 33);
            label5.TextValue = "加速！";
            label5.FColor = ConsoleColor.Yellow;


            mode2Scene.AddObject(ki1);
            mode2Scene.AddObject(ki2);
            mode2Scene.AddObject(label1);
            mode2Scene.AddObject(label2);
            mode2Scene.AddObject(label3);
            mode2Scene.AddObject(label4);
            mode2Scene.AddObject(label5);
            mode2Scene.AddObject(blueWin);
            mode2Scene.AddObject(redWin);
            mode2Scene.AddObject(bg);
            mode2Scene.AddObject(infoPanel);
            mode2Scene.AddObject(scorePanel);
            mode2Scene.AddObject(fc);
            mode2Scene.AddObject(gamePanel);
            mode2Scene.AddObject(psh1);
            mode2Scene.AddObject(psh2);
            #endregion

        }

        public void InitMode3Scene()
        {
            mode3Scene = new Scene();

            //背景
            Model.Panel bg = new Model.Panel(48, 40);
            bg.Position = new Vector2(1, 1);

            Model.Panel gamePanel = new Model.Panel(44, 28);
            gamePanel.borderColor = ConsoleColor.Red;
            gamePanel.Position = new Vector2(3, 3);

            PlayerSnackHead psh = new PlayerSnackHead();
            psh.Layer = 30;
            psh.Position = new Vector2(10, 13);
            psh.player = 2;
            psh.nowMode = 3;

            FoodController fc = new FoodController();

            Mode3Controller m3C = new Mode3Controller();

            m3C.pbc = psh.pbc;

            psh.m3C = m3C;

            m3C.fc = fc;

            psh.fc = fc;

            fc.followHead = psh;
            fc.isInsBulletFood = true;

            Model.Panel infoPanel = new Model.Panel(32, 8);
            infoPanel.Position = new Vector2(3, 31);

            Model.Panel timePanel = new Model.Panel(12, 8);
            timePanel.Position = new Vector2(35, 31);

            //创建计时器
            Mode3TimerController tc = new Mode3TimerController();

            Text timerText = new Text();
            timerText.Position = new Vector2(40, 35);
            timerText.FColor = ConsoleColor.Yellow;
            timerText.TextValue = "0秒";

            tc.timerText = timerText;

            m3C.tc = tc;

            Text label1 = new Text();
            label1.FColor = ConsoleColor.Magenta;
            label1.TextValue = "生存时间";
            label1.Position = new Vector2(39, 33);

            KeyItem ki1 = new KeyItem();
            ki1.upColor = ConsoleColor.Yellow;
            ki1.downColor = ConsoleColor.DarkYellow;
            ki1.keyStr = "K";
            ki1.Position = new Vector2(27, 34);

            psh.ki = ki1;

            KeyItem ki2 = new KeyItem();
            ki2.upColor = ConsoleColor.Red;
            ki2.downColor = ConsoleColor.DarkRed;
            ki2.keyStr = "J";
            ki2.Position = new Vector2(22, 34);

            psh.jKi = ki2;

            Text label2 = new Text();
            label2.Position = new Vector2(22, 33);
            label2.TextValue = "发射！";
            label2.FColor = ConsoleColor.Yellow;

            Text label3 = new Text();
            label3.Position = new Vector2(27, 33);
            label3.TextValue = "加速！";
            label3.FColor = ConsoleColor.Yellow;


            Text nowB = new Text();
            nowB.TextValue = psh.pbc.nowBCount.ToString();
            nowB.FColor = ConsoleColor.White;
            nowB.Position = new Vector2(10, 35);
            psh.pbc.nowB = nowB;

            Text label4 = new Text();
            label4.TextValue = "/";
            label4.FColor = ConsoleColor.Yellow;
            label4.Position = new Vector2(11, 35);

            Text maxB = new Text();
            maxB.TextValue = psh.pbc.maxBCount.ToString();
            maxB.FColor = ConsoleColor.White;
            maxB.Position = new Vector2(12, 35);
            psh.pbc.maxB = maxB;

            Text label5 = new Text();
            label5.TextValue = "弹药";
            label5.FColor = ConsoleColor.Yellow;
            label5.Position = new Vector2(10, 34);




            mode3Scene.AddObject(bg);
            mode3Scene.AddObject(ki1);
            mode3Scene.AddObject(ki2);
            mode3Scene.AddObject(gamePanel);
            mode3Scene.AddObject(infoPanel);
            mode3Scene.AddObject(timePanel);
            mode3Scene.AddObject(psh);
            mode3Scene.AddObject(fc);
            mode3Scene.AddObject(tc);
            mode3Scene.AddObject(timerText);
            mode3Scene.AddObject(label1);
            mode3Scene.AddObject(label2);
            mode3Scene.AddObject(label3);
            mode3Scene.AddObject(label4);
            mode3Scene.AddObject(label5);
            mode3Scene.AddObject(nowB);
            mode3Scene.AddObject(maxB);

        }

        public void InitRankScene()
        {
            rankScene = new Scene();

            //背景
            Model.Panel bg = new Model.Panel(48, 40);
            bg.Position = new Vector2(1, 1);

            //按钮初始化
            ButtonController bc = ButtonController.Ins;
            bc.LoadrankSceneBtn();

            //星空背景控制器
            StarBgController sbc = new StarBgController();

            Model.Panel m1p = new Model.Panel(14, 27);
            m1p.Position = new Vector2(8, 5);
            m1p.Layer = 10;
            m1p.mode = 1;

            Model.Panel m2p = new Model.Panel(14, 27);
            m2p.Position = new Vector2(28, 5);
            m2p.Layer = 10;
            m2p.mode = 1;

            Text label1 = new Text();
            label1.Layer = 11;
            label1.TextValue = "单人模式";
            label1.FColor = ConsoleColor.Yellow;
            label1.Position = new Vector2(13, 7);

            Text label2 = new Text();
            label2.Layer = 11;
            label2.TextValue = "生存模式";
            label2.FColor = ConsoleColor.Yellow;
            label2.Position = new Vector2(33, 7);

            Text label3 = new Text();
            label3.Layer = 11;
            label3.TextValue = "┉┉┉┉┉┉┉┉┉┉┉┉";
            label3.FColor = ConsoleColor.Magenta;
            label3.Position = new Vector2(9, 9);

            Text label4 = new Text();
            label4.Layer = 11;
            label4.TextValue = "┉┉┉┉┉┉┉┉┉┉┉┉";
            label4.FColor = ConsoleColor.Magenta;
            label4.Position = new Vector2(29, 9);

            foreach (RankItem item in RankController.Ins.m1r.datas)
            {
                if (RankController.Ins.m1r.datas.IndexOf(item) == 7) break;
                Text rank = new Text();
                rank.Layer = 11;
                rank.TextValue = item.name + "：" + item.value;
                rank.FColor = ConsoleColor.White;
                rank.Position = new Vector2(11, 11 + RankController.Ins.m1r.datas.IndexOf(item) * 3);
                rankScene.AddObject(rank);
            }

            foreach (RankItem item in RankController.Ins.m3r.datas)
            {
                if (RankController.Ins.m3r.datas.IndexOf(item) == 7) break;
                Text rank = new Text();
                rank.Layer = 11;
                rank.TextValue = item.name + "：" + item.value;
                rank.FColor = ConsoleColor.White;
                rank.Position = new Vector2(31, 11 + RankController.Ins.m3r.datas.IndexOf(item) * 3);
                rankScene.AddObject(rank);
            }


            rankScene.AddObject(bg);
            rankScene.AddObject(sbc);
            rankScene.AddObject(m1p);
            rankScene.AddObject(m2p);
            rankScene.AddObject(label1);
            rankScene.AddObject(label2);
            rankScene.AddObject(label3);
            rankScene.AddObject(label4);
        }

        public void LoadStartScene()
        {
            InitMainScene();
            NowScene.Ins.LoadScene(mainScene);
        }

    }
}
