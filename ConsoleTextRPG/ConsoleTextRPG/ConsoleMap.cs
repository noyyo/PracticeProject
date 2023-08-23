using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG
{
    public class ConsoleMap : IDrawable
    {
        public Point[,] Map2D;

        public ConsoleMap(int width, int height)
        {
            Map2D = new Point[width, height];

        }

        public void Init()
        {
            for (int row = 0; row < Map2D.GetLength(1); row++)
            {
                for (int column = 0; column < Map2D.GetLength(0); column++)
                {
                    Map2D[column, row] = new Point(column, row);
                }
            }
            SetBorder();
        }
        public void SetBorder()
        {
            for (int column = 0; column < Map2D.GetLength(0); column++)
                Map2D[column, 0].Value = "■";
            for (int column = 0; column < Map2D.GetLength(0); column++)
                Map2D[column, Map2D.GetLength(1) - 1].Value = "■";
            for (int row = 1; row < Map2D.GetLength(1) - 1; row++)
                Map2D[0, row].Value = "■";
            for (int row = 1; row < Map2D.GetLength(1) - 1; row++)
                Map2D[Map2D.GetLength(0) - 1, row].Value = "■";
        }
        public void SetEntrance(Entrance entrance)
        {

        }
        public void Draw()
        {
            foreach (Point p in Map2D)
            {
                p.Draw();
            }
        }
    }
}
