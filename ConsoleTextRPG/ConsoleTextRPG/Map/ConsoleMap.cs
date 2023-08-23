using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG.Map
{
    public class ConsoleMap : IDrawable
    {
        public Point[,] Map2D;
        public bool[,] isMovable2D;
        public string MapName = "";
        protected string _solutionPath = @"C:/Users/js022/Desktop/VisualStudioWorkSpace/PracticeProject/ConsoleTextRPG/";
        protected string _mapFilePath = "";
        public int Width;
        public int Height;

        public ConsoleMap(int width, int height)
        {
            Map2D = new Point[width, height];
            isMovable2D = new bool[width, height];
            Width = width;
            Height = height;
            Init();
        }

        public void Init()
        {
            for (int row = 0; row < Map2D.GetLength(1); row++)
            {
                for (int column = 0; column < Map2D.GetLength(0); column++)
                {
                    Map2D[column, row] = new Point(column, row);
                    isMovable2D[column, row] = true;
                }
            }
        }
        public void SetBorder()
        {
            for (int column = 0; column < Map2D.GetLength(0); column += 2)
            {
                Map2D[column, 0].Value = "■";
                isMovable2D[column, 0] = false;
                isMovable2D[column + 1, 0] = false;
            }
            for (int column = 0; column < Map2D.GetLength(0); column += 2)
            {
                Map2D[column, Map2D.GetLength(1) - 1].Value = "■";
                isMovable2D[column, Map2D.GetLength(1) - 1] = false;
                isMovable2D[column + 1, Map2D.GetLength(1) - 1] = false;
            }
            for (int row = 1; row < Map2D.GetLength(1) - 1; row++)
            {
                Map2D[0, row].Value = "■";
                isMovable2D[0, row] = false;
                isMovable2D[1, row] = false;
            }
            for (int row = 1; row < Map2D.GetLength(1) - 1; row++)
            {
                Map2D[Map2D.GetLength(0) - 2, row].Value = "■";
                isMovable2D[Map2D.GetLength(0) - 2, row] = false;
                isMovable2D[Map2D.GetLength(0) - 1, row] = false;
            }
        }
        public void SetEntrance(Entrance entrance)
        {

        }
        public void Draw()
        {
            for (int row = 0; row < Map2D.GetLength(1); row++)
            {
                for (int column = 0; column < Map2D.GetLength(0); column++)
                {
                    Map2D[column, row].Draw();
                    if (column != Console.GetCursorPosition().Left - 1)
                        column++;
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
                        if (str.ElementAt(i).ToString() == " ")
                            continue;
                        Map2D[posX, posY].Value = str.ElementAt(i).ToString();
                        isMovable2D[posX, posY] = false;
                        posX++;
                        if (isKorean(str.ElementAt(i)))
                        {
                            isMovable2D[posX + 1, posY] = false;
                            posX++;
                        }
                    }
                }
                else if (alignmnet == Alignment.Middle)
                {
                    int strHalf = str.Length / 2;
                    int screenHalf = Width / 2;

                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str.ElementAt(i).ToString() == " ")
                            continue;
                        Map2D[posX - strHalf + screenHalf, posY].Value = str.ElementAt(i).ToString();
                        isMovable2D[posX, posY] = false;
                        posX++;
                        if (isKorean(str.ElementAt(i)))
                        {
                            isMovable2D[posX + 1, posY] = false;
                            posX++;
                        }
                    }
                }
                else if (alignmnet == Alignment.Right)
                {
                    posX = Width - posX;
                    for (int i = str.Length - 1; i >= 0; i--)
                    {
                        if (str.ElementAt(i).ToString() == " ")
                            continue;
                        Map2D[posX, posY].Value = str.ElementAt(i).ToString();
                        isMovable2D[posX, posY] = false;
                        posX--;
                        if (isKorean(str.ElementAt(i)))
                        {
                            isMovable2D[posX - 1, posY] = false;
                            posX--;
                        }
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
        public bool isKorean(char ch)
        {
            if (ch >= '\u1100' && ch <= '\u11ff')
                return true;
            else if (ch >= '\uac00' && ch <= '\ud7af')
                return true;
            else
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(ch);
                if (Console.GetCursorPosition().Left == 2)
                {
                    Map2D[0, 0].Draw();
                    return true;
                }
                else
                {
                    Map2D[0, 0].Draw();
                    return false;
                }
            }
        }
        public void SaveMap()
        {
            _mapFilePath = _solutionPath + $"{MapName}.json";
            File.WriteAllText(_mapFilePath,JsonConvert.SerializeObject(Map2D, Formatting.Indented) +"##seperator##" + JsonConvert.SerializeObject(isMovable2D, Formatting.Indented));
        }
        public void LoadMap()
        {
            _mapFilePath = _solutionPath + $"{MapName}.json";
            if (File.Exists(_mapFilePath))
            {
                string json = File.ReadAllText(_mapFilePath);
                string [] jsonStrings = json.Split("##seperator##");
                jsonStrings[1].Replace("##seperator##", "");
                
                Point[,]? points = JsonConvert.DeserializeObject<Point[,]>(jsonStrings[0]);
                if (points != null)
                    Map2D = points;

                bool[,]? isMoveable = JsonConvert.DeserializeObject<bool[,]>(jsonStrings[1]);
                if (isMoveable != null)
                    isMovable2D = isMoveable;
                    
            }
        }
    }
}
