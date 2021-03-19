using CmdGameEngine.Controller;
using CmdGameEngine.GameEngine.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.GameEngine
{
    public class GameObject
    {
        #region 字段
        private int layer = 0;

        private Vector2 position;

        private bool visible = true;

        public List<MItem> Image = new List<MItem>();

        protected GameObjectDrawController drawTool = null;

        public delegate void FlyEvent(GameObject go);

        public event FlyEvent OnFlyOver;

        public bool canFly = false;

        public Scene parentScene = null;

        public bool isOn = false;

        //飞行动画终止位置
        public Vector2 flyToPoint = new Vector2();

        //动画所需变量
        public Vector2 newPoint = new Vector2(-1, -1);

        public float flySpeed = 0.2f;

        public Vector2 force = new Vector2();

        public bool isForce = true;
        #endregion

        #region 属性
        public int Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value;
            }
        }

        public Vector2 Position
        {
            get => position;
            set
            {
                if (!Visible) return;

                NowScene.Ins.MoveGameObject(this, value);
                position = value;

            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                lock (NowScene.drawLock)
                {
                    visible = value;
                    if (visible)
                    {
                        NowScene.Ins.ShowGameObject(this);
                    }
                    else
                    {
                        NowScene.Ins.HideGameObject(this);
                    }
                }
            }
        }
        #endregion

        #region 方法
        public GameObject()
        {
            Program.updateDel -= Update;
            Program.updateDel += Update;
            Program.keyDel -= OnKeyDown;
            Program.keyDel += OnKeyDown;
            Position = new Vector2(-200, -200);
            drawTool = new GameObjectDrawController(this);
            Init();


        }

        public virtual void Init()
        {
            if (!isOn) return;

            NowScene.Ins.ReFreshGameObject(this);
        }

        public virtual void Update()
        {
            if (!isOn) return;
            if (canFly)
            {
                if (Math.Abs(flyToPoint.X - newPoint.X) <= 0.5 && Math.Abs(flyToPoint.Y - newPoint.Y) <= 0.5)
                {
                    canFly = false;
                    Position = flyToPoint;
                    try
                    {
                        OnFlyOver(this);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    if (newPoint.X != -1 && newPoint.Y != -1)
                    {
                        newPoint = Vector2.Lerp(newPoint, flyToPoint, flySpeed);
                    }
                    else
                    {
                        newPoint = Vector2.Lerp(Position, flyToPoint, flySpeed);
                    }

                    Position = newPoint;
                }
            }
            if (isForce)
            {
                if (force == Vector2.Zero) return;
                MoveOffset(force.X, force.Y);
            }
        }

        public virtual void OnKeyDown(ConsoleKey key)
        {
            //if (isOn) return;
        }

        public void MoveOffset(float dx, float dy)
        {
            if (!isOn) return;
            Position = new Vector2(Position.X + dx, Position.Y + dy);
        }

        public void FlyTo(Vector2 target)
        {
            if (!isOn) return;
            newPoint = new Vector2(-1, -1);
            flyToPoint = target;
            canFly = true;
        }
        #endregion

        public T CloneGameObject<T>() where T : GameObject, new()
        {
            T go = new T();
            go.Layer = Layer;
            go.Position = Position;
            go.Visible = Visible;
            Image.ForEach((i) => { go.Image.Add(i); });
            go.canFly = canFly;
            go.parentScene = parentScene;
            go.isOn = isOn;
            go.flyToPoint = flyToPoint;
            go.newPoint = newPoint;
            go.flySpeed = flySpeed;

            return go;
        }
    }
}
