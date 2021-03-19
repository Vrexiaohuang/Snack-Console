using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.GameEngine
{
    public class Scene
    {
        public List<GameObject> allObject = new List<GameObject>();

        public void AddObject(GameObject go)
        {
            if (allObject.IndexOf(go) != -1) return;

            go.parentScene = this;

            allObject.Add(go);
        }
    }
}
