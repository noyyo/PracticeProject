using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG.UI
{
    [SupportedOSPlatform("windows")]
    public class UIComponent : IDrawable
    {
        public Point[,] Points;
        public bool HaveBorder = false;
        public bool IsFocusable = true;
        public string Name;
        public int Width;
        public int Height;

        public UIComponent(int width, int height)
        {
            Points = new Point[width, height];
            Name = "";
            Width = width;
            Height = height;
            Init();
        }
        public void Init()
        {
            for (int row = 0; row < Points.GetLength(1); row++)
            {
                for (int column = 0; column < Points.GetLength(0); column++)
                {
                    Points[column, row] = new Point(column, row);
                }
            }
        }
        public void SetPosition(int posX, int posY)
        {
            for (int row = 0; row < Points.GetLength(1); row++)
            {
                for (int column = 0; column < Points.GetLength(0); column++)
                {
                    Points[column, row].X = posX + column;
                    Points[column, row].Y = posY + row;
                }
            }
        }
        public void SetText(string Text, int startLocalPosX, int startLocalPosY, Alignment alignmnet = Alignment.Left)
        {
            int posX = startLocalPosX;
            int posY = startLocalPosY;

            foreach (string str in Text.Split("\n"))
            {
                str.Trim();
                if (alignmnet == Alignment.Left)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        Points[posX, posY].Value = str.ElementAt(i).ToString();
                        posX++;
                        if (isKorean(str.ElementAt(i)))
                            posX++;
                    }
                }
                else if (alignmnet == Alignment.Middle)
                {
                    int strHalf = str.Length / 2;
                    int screenHalf = Width / 2;

                    for (int i = 0; i < str.Length; i++)
                    {
                        Points[posX - strHalf + screenHalf, posY].Value = str.ElementAt(i).ToString();
                        posX++;
                        if (isKorean(str.ElementAt(i)))
                            posX++;
                    }
                }
                else if (alignmnet == Alignment.Right)
                {
                    posX = Width - posX;
                    for (int i = str.Length - 1; i >= 0; i--)
                    {
                        Points[posX , posY].Value = str.ElementAt(i).ToString();
                        posX--;
                        if (isKorean(str.ElementAt(i)))
                            posX--;
                    }
                }
                posX = startLocalPosX;
                posY++;
            }
        }
        public void SetTextExceptBorder(string Text, int startLocalPosX, int startLocalPosY, Alignment alignmnet = Alignment.Left)
        {
            switch (alignmnet)
            {
                case Alignment.Left:
                    SetText(Text, startLocalPosX + 1, startLocalPosY + 1);
                    break;
                case Alignment.Middle:
                    SetText(Text, startLocalPosX, startLocalPosY + 1, alignmnet);
                    break;
                case Alignment.Right:
                    SetText(Text, startLocalPosX - 1, startLocalPosY + 1, alignmnet);
                    break;
            }
        }
        public void SetSeparatorBar(int startLocalPosX, int startLocalPosY, int length)
        {
            int posX = startLocalPosX;
            int posY = startLocalPosY;
            for (int i = 0; i < length; i++)
            {
                Points[posX, posY].Value = "─";
                posX++;
            }
        }
        public void SetBorder(string upperLeft = "┌", string upperRight = "┐", string lowerLeft = "└", string lowerRight = "┘")
        {
            Points[0, 0].Value = upperLeft;
            Points[Points.GetLength(0) - 1, 0].Value = upperRight;
            Points[0, Points.GetLength(1) - 1].Value = lowerLeft;
            Points[Points.GetLength(0) - 1, Points.GetLength(1) - 1].Value = lowerRight;

            for (int column = 1; column < Points.GetLength(0) - 1; column++)
                Points[column, 0].Value = "─";
            for (int column = 1; column < Points.GetLength(0) - 1; column++)
                Points[column, Points.GetLength(1) - 1].Value = "─";
            for (int row = 1; row < Points.GetLength(1) - 1; row++)
                Points[0, row].Value = "│";
            for (int row = 1; row < Points.GetLength(1) - 1; row++)
                Points[Points.GetLength(0) - 1, row].Value = "│";

            HaveBorder = true;
        }
        public void Draw()
        {
            foreach (Point p in Points)
            {
                p.Draw();
            }
        }
        public bool isKorean(char ch)
        {
            if (ch >= '\u1100' && ch <= '\u11ff')
                return true;
            else if (ch >= '\uac00' && ch <= '\ud7af')
                return true;
            else
                return false;
        }
    }
}
