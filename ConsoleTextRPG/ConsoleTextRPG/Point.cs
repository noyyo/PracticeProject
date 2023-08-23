using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG
{
    public class Point
    {
        public int X;
        public int Y;
        public string Value;
        // value는 2바이트 고정 크기입니다.
        public Point(int x, int y, string value = " ")
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
        }
        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Value);
        }
    }
}
