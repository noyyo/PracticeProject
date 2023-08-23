using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG.UI
{
    [SupportedOSPlatform("windows")]

    public class UIWindow : IDrawable
    {
        public Point[,] Points;
        public List<UIComponent> Components;
        public string WindowName;
        public int Width;
        public int Height;
        private const ConsoleColor _focusColor = ConsoleColor.Green;
        private UIComponent? _focusedComponent;
        

        public UIWindow(int width, int height)
        {
            Width = width;
            Height = height;
            Points = new Point[width, height];
            Components = new List<UIComponent>();
            WindowName = "";
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
            SetLayout();
        }

        public virtual void SetFocus(UIComponent comp)
        {
            if (!comp.IsFocusable)
                return;

            Console.BackgroundColor = _focusColor;
            ScreenManager.I.Draw(comp);
            Console.ResetColor();
            _focusedComponent = comp;
        }
        public virtual void ReleaseFocus()
        {
            if (_focusedComponent == null)
                return;

            ScreenManager.I.Draw(_focusedComponent);
            _focusedComponent = null;

        }
        public void DrawComponents()
        {
            foreach (UIComponent comp in Components)
            {
                ScreenManager.I.Draw(comp);
            }
        }
        public virtual void OpenWindow()
        {
            ScreenManager.I.Draw(this);
            UIComponent firstComp = Components.First();
            if (firstComp == null)
                return;
            
            SetFocus(firstComp);
        }
        public void AddComponent(UIComponent comp)
        {
            Components.Add(comp);
        }

        public void SetComponentPosition(UIComponent comp, int startLocalPosX, int startLocalPosY)
        {
            comp.SetPosition(Points[0, 0].X + startLocalPosX, Points[0, 0].Y + startLocalPosY);
        }
        public void SetPosition(int posX, int posY)
        {
            for (int row = 0; row < Points.GetLength(1); row++)
            {
                for (int column = 0; column < Points.GetLength(0); column++)
                {
                    Points[column, row].X += posX;
                    Points[column, row].Y += posY;
                }
            }
        }
        public void SetLayout(string upperLeft = "┌", string upperRight = "┐", string lowerLeft = "└", string lowerRight = "┘")
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
        }
        public void Draw()
        {
            foreach (Point p in Points)
            {
                p.Draw();
            }
            DrawComponents();
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
                        Points[posX, posY].Value = str.ElementAt(i).ToString();
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
        public bool isKorean(char ch)
        {
            if (ch >= '\u1100' && ch <= '\u11ff')
                return true;
            else if (ch >= '\uac00' && ch <= '\ud7af')
                return true;
            else
                return false;
        }
        public virtual void HandleKeyboardInput(ConsoleKeyInfo keyInput)
        {
            switch (keyInput.Key)
            {
                case ConsoleKey.Enter:
                    break;
                case ConsoleKey.LeftArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.UpArrow:
                    break;
                case ConsoleKey.DownArrow:
                    break;
                case ConsoleKey.Escape:
                    ScreenManager.I.CloseUIWindow();
                    break;
            }
        }
    }
}
