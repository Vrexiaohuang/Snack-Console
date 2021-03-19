using CmdGameEngine.Controller;
using CmdGameEngine.GameEngine;
using CmdGameEngine.GameEngine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.GameEngine
{
    /// <summary>
    /// 当前显示的场景
    /// </summary>
    public class NowScene
    {
        #region 单例
        static NowScene ins = null;

        NowScene()
        {

        }

        public static NowScene Ins
        {
            get
            {
                if (ins == null)
                {
                    ins = new NowScene();
                }
                return ins;
            }
        }
        #endregion

        public static object drawLock = new object();

        /// <summary>
        /// 当前地图
        /// </summary>
        List<MItem> map = new List<MItem>();

        /// <summary>
        /// 当前场景
        /// </summary>
        public Scene nowScene = null;

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(Scene scene)
        {
            ButtonController.Ins.selectBtn = null;
            Console.Clear();
            map.Clear();
            if (nowScene != null)
            {
                foreach (GameObject item in nowScene.allObject)
                {
                    item.isOn = false;
                }
            }

            nowScene = scene;

            foreach (GameObject item in scene.allObject)
            {
                item.isOn = true;
                item.Init();
            }


            DrawNowSceneAll();

        }

        public void AddObjectToNowScene(GameObject go)
        {
            if (nowScene != null)
            {
                go.isOn = true;
                nowScene.AddObject(go);
            }
        }

        /// <summary>
        /// 初始化绘制场景
        /// </summary>
        public void DrawNowSceneAll()
        {
            lock (drawLock)
            {
                //先清理整个屏幕
                Console.Clear();
                map.Clear();

                try
                {
                    //便利当前场景的所有GameObject
                    for (int i = 0; i < nowScene.allObject.Count; i++)
                    {
                        GameObject go = nowScene.allObject[i];

                        #region 存入map

                        map.AddRange(ArrayHelper.Ins.AddDxy(go.Image, (int)go.Position.X, (int)go.Position.Y, go.Layer, 1));

                        #endregion
                    }

                    for (int i = 0; i < nowScene.allObject.Count; i++)
                    {
                        GameObject go = nowScene.allObject[i];

                        #region 绘制

                        ReFreshGameObject(go);

                        #endregion
                    }
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 移动一个游戏物体
        /// </summary>
        /// <param name="go"></param>
        /// <param name="position"></param>
        public void MoveGameObject(GameObject go, Vector2 position)
        {
            lock (drawLock)
            {
                Vector2 goPos = go.Position;

                List<MItem> goImage = go.Image.Skip(0).Take(go.Image.Count).ToList();
                List<MItem> goImage2 = go.Image.Skip(0).Take(go.Image.Count).ToList();

                goImage = ArrayHelper.Ins.AddDxy(goImage, (int)goPos.X, (int)goPos.Y, go.Layer);

                goImage2 = ArrayHelper.Ins.AddDxy(goImage2, (int)position.X, (int)position.Y, go.Layer);

                List<MItem> C_ClearBlock = new List<MItem>();
                List<MItem> C_DrawBlock = new List<MItem>();

                List<MItem> S_DrawBlock = new List<MItem>();

                #region 绘制新位置
                for (int i = 0; i < goImage2.Count; i++)
                {
                    MItem temp = goImage2[i];

                    //如果有同样位置，且比这个的层级高的，而且还显示的，那就不绘制
                    if (map.Exists(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer > go.Layer && t.parent.Visible))
                    {

                    }
                    else
                    {
                        //如果没有，就绘制
                        S_DrawBlock.Add(temp);

                        //切换状态
                        int index = map.IndexOf(map.Where(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer == go.Layer && t.parent == go).FirstOrDefault());

                        try
                        {
                            if (index == -1)
                            {
                                temp.isDraw = true;
                                map.Add(temp);
                            }
                            else
                            {
                                temp.isDraw = true;
                                map[index] = temp;
                            }
                        }
                        catch
                        {

                        }

                    }
                }

                if (go.isOn)
                {
                    for (int i = 0; i < S_DrawBlock.Count; i++)
                    {
                        DrawHelper.Ins.DrawABlock(S_DrawBlock[i]);
                    }
                }

                #endregion

                #region 清除原本位置

                for (int i = 0; i < goImage.Count; i++)
                {
                    MItem temp = goImage[i];

                    //如果有同样位置，且比这个的层级高的，而且还显示的，那就不清除
                    if (map.Exists(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer > go.Layer && t.parent.Visible && t.parent != go))
                    {
                        //切换状态
                        map.RemoveAll(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer == go.Layer && t.parent == go);
                    }
                    //如果刚刚绘制完
                    else if (map.Exists(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer == go.Layer && t.parent == go && t.isDraw))
                    {


                    }
                    else
                    {
                        //如果没有同位置层数小于或等于此层级且显示的，那就清除
                        if (!map.Exists(t => t.parent != go && (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer <= go.Layer && t.parent.Visible && !t.isDraw))
                        {
                            //清除
                            C_ClearBlock.Add(temp);
                        }
                        else
                        {
                            //绘制下一格的图像
                            List<MItem> tempp = map.Where(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer <= go.Layer && t.parent.Visible && t.parent != go).ToList();
                            MItem temp2 = tempp.Where(t => t.layer == tempp.Max(x => x.layer)).FirstOrDefault();

                            //绘制
                            C_DrawBlock.Add(temp2);
                        }
                        //切换状态
                        map.RemoveAll(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer == go.Layer && t.parent == go);
                    }


                }

                for (int k = 0; k < map.Count; k++)
                {
                    MItem mi = map[k];
                    mi.isDraw = false;
                    map[k] = mi;
                }

                if (go.isOn)
                {
                    for (int i = 0; i < C_ClearBlock.Count; i++)
                    {
                        DrawHelper.Ins.ClearABlock(C_ClearBlock[i]);
                    }
                    for (int i = 0; i < C_DrawBlock.Count; i++)
                    {
                        DrawHelper.Ins.DrawABlock(C_DrawBlock[i]);
                    }
                }
                #endregion

                map.RemoveAll(t => t.parent.Visible == false);
            }

        }




        /// <summary>
        /// 重刷场景的GameObject
        /// </summary>
        /// <param name="go"></param>
        public void ReFreshGameObject(GameObject go)
        {
            lock (drawLock)
            {
                List<MItem> goImage = go.Image.Skip(0).Take(go.Image.Count).ToList();

                goImage = ArrayHelper.Ins.AddDxy(goImage, (int)go.Position.X, (int)go.Position.Y, go.Layer);

                List<MItem> S_DrawBlock = new List<MItem>();

                #region 绘制新位置
                for (int i = 0; i < goImage.Count; i++)
                {
                    MItem temp = goImage[i];

                    //如果有同样位置，且比这个的层级高的，而且还显示的，那就不绘制
                    if (map.Exists(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.layer > goImage[i].layer && t.parent.Visible))
                    {

                    }
                    else
                    {
                        //如果没有，就绘制
                        S_DrawBlock.Add(temp);
                    }

                    //切换状态
                    int index = map.IndexOf(map.Where(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.layer == temp.layer && t.parent == go).FirstOrDefault());
                    if (index == -1)
                    {
                        temp.isDraw = true;
                        map.Add(temp);
                    }
                    else
                    {
                        temp.isDraw = true;
                        map[index] = temp;
                    }

                }

                if (go.isOn)
                {
                    for (int i = 0; i < S_DrawBlock.Count; i++)
                    {
                        DrawHelper.Ins.DrawABlock(S_DrawBlock[i]);
                    }
                }

                #endregion

                #region 清除原本位置

                List<MItem> goMapItem = map.Where(t => t.parent == go && !t.isDraw).ToList();

                List<MItem> C_ClearBlock = new List<MItem>();
                List<MItem> C_DrawBlock = new List<MItem>();

                for (int i = 0; i < goMapItem.Count; i++)
                {
                    MItem temp = goMapItem[i];

                    //如果有同样位置，且比这个的层级高的，而且还显示的，那就不清除
                    if (map.Exists(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer > go.Layer && t.parent.Visible))
                    {

                    }
                    //如果刚刚绘制完
                    else if (map.Exists(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer == go.Layer && t.parent == go && t.isDraw))
                    {

                    }
                    else
                    {
                        //如果没有同位置层数小于或等于此层级且显示的，那就清除
                        if (!map.Exists(t => t.parent != go && (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer <= go.Layer && t.parent.Visible))
                        {
                            //清除
                            C_ClearBlock.Add(temp);
                        }
                        else
                        {

                            //绘制下一格的图像
                            List<MItem> tempp = map.Where(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer <= go.Layer && t.parent.Visible && t.parent != go).ToList();
                            MItem temp2 = tempp.Where(t => t.layer == tempp.Max(x => x.layer)).FirstOrDefault();

                            //绘制
                            C_DrawBlock.Add(temp2);
                        }

                        //切换状态
                        map.RemoveAll(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer == go.Layer && t.parent == go);
                    }


                }

                for (int k = 0; k < map.Count; k++)
                {
                    MItem mi = map[k];
                    mi.isDraw = false;
                    map[k] = mi;
                }

                if (go.isOn)
                {
                    for (int i = 0; i < C_ClearBlock.Count; i++)
                    {
                        DrawHelper.Ins.ClearABlock(C_ClearBlock[i]);
                    }
                    for (int i = 0; i < C_DrawBlock.Count; i++)
                    {
                        DrawHelper.Ins.DrawABlock(C_DrawBlock[i]);
                    }
                }


                #endregion

                map.RemoveAll(t => t.parent.Visible == false);
            }

        }

        /// <summary>
        /// 显示场景中的GameObject
        /// </summary>
        /// <param name="go"></param>
        public void ShowGameObject(GameObject go)
        {
            lock (drawLock)
            {
                List<MItem> goImage = go.Image;

                goImage = ArrayHelper.Ins.AddDxy(goImage, (int)go.Position.X, (int)go.Position.Y, go.Layer);

                for (int i = 0; i < goImage.Count; i++)
                {
                    MItem temp = goImage[i];

                    //如果有同样位置，且比这个的层级高的，而且还显示的，那就不绘制
                    if (map.Exists(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.layer > goImage[i].layer && t.parent.Visible))
                    {

                    }
                    else
                    {
                        //如果没有，就绘制
                        DrawHelper.Ins.DrawABlock((int)temp.position.X, (int)temp.position.Y, temp.text, temp.fColor, temp.bColor);
                    }

                }
            }
            

        }

        /// <summary>
        /// 隐藏场景中的GameObject
        /// </summary>
        /// <param name="go"></param>
        public void HideGameObject(GameObject go)
        {
            lock (drawLock)
            {
                List<MItem> goImage = go.Image.Skip(0).Take(go.Image.Count).ToList();
                goImage = ArrayHelper.Ins.AddDxy(goImage, (int)go.Position.X, (int)go.Position.Y, go.Layer);

                List<MItem> C_ClearBlock = new List<MItem>();
                List<MItem> C_DrawBlock = new List<MItem>();

                for (int i = 0; i < goImage.Count; i++)
                {
                    MItem temp = goImage[i];

                    //如果有同样位置，且比这个的层级高的，而且还显示的，那就不清除
                    if (map.Exists(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer > go.Layer && t.parent.Visible && t.parent != go))
                    {
                        //切换状态
                        map.RemoveAll(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer == go.Layer && t.parent == go);
                    }
                    else
                    {
                        //如果没有同位置层数小于或等于此层级且显示的，那就清除
                        if (!map.Exists(t => t.parent != go && (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer <= go.Layer && t.parent.Visible && !t.isDraw))
                        {
                            //清除
                            C_ClearBlock.Add(temp);
                        }
                        else
                        {
                            //绘制下一格的图像
                            List<MItem> tempp = map.Where(t => t.position.X == temp.position.X && t.position.Y == temp.position.Y && t.parent.Layer <= go.Layer && t.parent.Visible && t.parent != go).ToList();
                            MItem temp2 = tempp.Where(t => t.layer == tempp.Max(x => x.layer)).FirstOrDefault();

                            //绘制
                            C_DrawBlock.Add(temp2);
                        }
                        //切换状态
                        map.RemoveAll(t => (int)t.position.X == (int)temp.position.X && (int)t.position.Y == (int)temp.position.Y && t.parent.Layer == go.Layer && t.parent == go);
                    }
                }

                if (go.isOn)
                {
                    for (int i = 0; i < C_ClearBlock.Count; i++)
                    {
                        DrawHelper.Ins.ClearABlock(C_ClearBlock[i]);
                    }
                    for (int i = 0; i < C_DrawBlock.Count; i++)
                    {
                        DrawHelper.Ins.DrawABlock(C_DrawBlock[i]);
                    }
                }

                map.RemoveAll(t => t.parent.Visible == false);
            }
            
        }


    }
}
