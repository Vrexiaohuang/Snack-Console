using CmdGameEngine.Controller;
using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Model
{
    class ButtonBase : GameObject
    {
        public delegate void BtnClickEvent(ButtonBase btn);

        public event BtnClickEvent OnClick;

        public ButtonBase top = null;
        public ButtonBase down = null;
        public ButtonBase left = null;
        public ButtonBase right = null;

        public override void Init()
        {
            base.Init();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnKeyDown(ConsoleKey key)
        {
            base.OnKeyDown(key);
        }

        public virtual void Select()
        {
            ButtonController.Ins.selectBtn = this;
        }

        public virtual void UnSelect()
        {

        }

        public void TriggerClick()
        {
            OnClick(this);
        }
    }
}
