using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }
    public enum GameInputMode
    {
        Game,
        UI,
    }
    public enum ItemCategory
    {
        WEAPON,
        ARMOUR,
        HELMET,
        GLOVE,
        BOOTS,
        MISC,
    }
    public enum Alignment
    {
        Left,
        Middle,
        Right,
    }
}
