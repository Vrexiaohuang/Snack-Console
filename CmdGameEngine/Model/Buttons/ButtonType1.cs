using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model.Buttons
{
    class ButtonType1 : ButtonBase
    {
        private string text = "";

        public ConsoleColor selectColor = ConsoleColor.Red;

        public Vector2 btnBasePos = new Vector2();

        public bool isSelect = false;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                Init();
            }
        }

        public override void Init()
        {
            base.Init();

            flySpeed = 0.2f;

            if (isSelect)
            {
                Select();
            }
            else
            {
                NormalMode();
            }
        }

        public void NormalMode()
        {
            int dxLeft = 0;
            int dxRight = 12;

            Image.Clear();

            ConsoleColor borderColor = ConsoleColor.DarkCyan;

            ConsoleColor textColor = ConsoleColor.White;

            drawTool.fColor = borderColor;
            drawTool.SetMousePosition(0, 0);
            drawTool.WriteLine("━━━━━━━━━━━━");
            drawTool.SetMousePosition(dxLeft, 1);
            drawTool.Write("╲");

            drawTool.SetMousePosition(dxRight, 1);
            drawTool.Write("╲");


            drawTool.SetMousePosition(dxLeft + 1, 2);
            drawTool.Write("╲");

            drawTool.fColor = textColor;
            drawTool.SetMousePosition(dxLeft + 14 / 2 - (text.Length + 1) / 2, 2);
            drawTool.Write(Text);

            drawTool.fColor = borderColor;

            drawTool.SetMousePosition(dxRight + 1, 2);
            drawTool.Write("╲");

            drawTool.SetMousePosition(dxLeft + 2, 3);
            drawTool.Write("╲");

            drawTool.SetMousePosition(dxRight + 2, 3);
            drawTool.Write("╲");

            drawTool.SetMousePosition(dxLeft + 3, 4);
            drawTool.Write("━━━━━━━━━━━━");

            NowScene.Ins.ReFreshGameObject(this);

            
        }

        public override void Select()
        {
            base.Select();

            isSelect = true;

            int dxLeft = 0;
            int dxRight = 12;

            ConsoleColor borderColor = selectColor;

            drawTool.SetMousePosition(0, 0);

            drawTool.fColor = borderColor;
            drawTool.WriteLine("━━━━━━━━━━━━");
            drawTool.SetMousePosition(dxLeft, 1);
            drawTool.Write("╲");

            drawTool.SetMousePosition(dxRight, 1);
            drawTool.Write("╲");


            drawTool.SetMousePosition(dxLeft + 1, 2);
            drawTool.Write("╲");

            drawTool.fColor = ConsoleColor.Yellow;
            drawTool.SetMousePosition((dxLeft + 14 / 2 - (text.Length + 1) / 2) - 1, 2);
            drawTool.Write("◤");
            drawTool.fColor = ConsoleColor.Cyan;
            drawTool.SetMousePosition(dxLeft + 14 / 2 - (text.Length + 1) / 2, 2);
            drawTool.Write(Text);
            drawTool.fColor = ConsoleColor.Yellow;
            drawTool.Write("◢");
            drawTool.fColor = borderColor;

            drawTool.SetMousePosition(dxRight + 1, 2);
            drawTool.Write("╲");

            drawTool.SetMousePosition(dxLeft + 2, 3);
            drawTool.Write("╲");

            drawTool.SetMousePosition(dxRight + 2, 3);
            drawTool.Write("╲");

            drawTool.SetMousePosition(dxLeft + 3, 4);
            drawTool.Write("━━━━━━━━━━━━");

            flyToPoint = new Vector2(btnBasePos.X + 2, btnBasePos.Y);
            canFly = true;

            NowScene.Ins.ReFreshGameObject(this);
        }

        public override void UnSelect()
        {
            base.UnSelect();

            isSelect = false;
            NormalMode();

            flyToPoint = new Vector2(btnBasePos.X, btnBasePos.Y);
            canFly = true;
        }

        public ButtonType1 Clone()
        {
            ButtonType1 obj = CloneGameObject<ButtonType1>();
            obj.text = text;
            obj.selectColor = selectColor;
            obj.btnBasePos = btnBasePos;
            obj.isSelect = isSelect;

            return obj;
        }
    }
}
