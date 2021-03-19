using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.GameEngine.Controller
{
    public class GameObjectDrawController
    {
        GameObject go = null;

        public GameObjectDrawController(GameObject go)
        {
            this.go = go;
        }

        public ConsoleColor fColor = ConsoleColor.White;

        public ConsoleColor bColor = ConsoleColor.Black;

        Vector2 nowPosition = new Vector2(0, 0);

        public Vector2 nowPos
        {
            get
            {
                return nowPosition;
            }
        }

        public void SetMousePosition(int x, int y)
        {
            nowPosition = new Vector2(x, y);
        }

        public void Write(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                MItem mi = new MItem();
                mi.layer = go.Layer;
                mi.parent = go;
                mi.isVisible = true;
                mi.fColor = fColor;
                mi.bColor = bColor;
                mi.position = nowPosition;
                mi.text = text[i] == ' ' ? "  " : text[i].ToString();
                if (!go.Image.Exists(t => t.position.X == nowPosition.X && t.position.Y == nowPosition.Y))
                {
                    go.Image.Add(mi);
                }
                else
                {
                    int index = go.Image.IndexOf(go.Image.Where(t => t.position.X == nowPosition.X && t.position.Y == nowPosition.Y).FirstOrDefault());
                    go.Image[index] = mi;
                }
                nowPosition = new Vector2(nowPosition.X + 1, nowPosition.Y);
            }


        }

        public void WriteLine(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                MItem mi = new MItem();
                mi.layer = go.Layer;
                mi.parent = go;
                mi.isVisible = true;
                mi.fColor = fColor;
                mi.bColor = bColor;
                mi.position = nowPosition;
                mi.text = text[i] == ' ' ? "  " : text[i].ToString();
                if (!go.Image.Exists(t => t.position.X == nowPosition.X && t.position.Y == nowPosition.Y))
                {
                    go.Image.Add(mi);
                }
                else
                {
                    int index = go.Image.IndexOf(go.Image.Where(t => t.position.X == nowPosition.X && t.position.Y == nowPosition.Y).FirstOrDefault());
                    go.Image[index] = mi;
                }
                nowPosition = new Vector2(nowPosition.X + 1, nowPosition.Y);
            }
            nowPosition = new Vector2(0, nowPosition.Y + 1);

        }
    }
}
