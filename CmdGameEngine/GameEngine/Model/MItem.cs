using CmdGameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public struct MItem
{
    public Vector2 position;
    public ConsoleColor fColor;
    public ConsoleColor bColor;
    public string text;
    public bool isVisible;
    public int layer;
    public GameObject parent;
    public bool isDraw;
}

