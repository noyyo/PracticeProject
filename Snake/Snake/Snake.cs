using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            // 콘솔 창 화면 크기 및 버퍼 세팅.
            Console.SetBufferSize(400, 200);
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(200, 50);

            Map map = new Map();
            map.DrawMap();
            bool isEnd = false;
            while (!isEnd)
            {
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                
                ConsoleKeyInfo info = Console.ReadKey(false);
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        map.MoveSnake(Direction.UP);
                        break;
                    case ConsoleKey.DownArrow:
                        map.MoveSnake(Direction.DOWN);
                        break;
                    case ConsoleKey.LeftArrow:
                        map.MoveSnake(Direction.LEFT);
                        break;
                    case ConsoleKey.RightArrow:
                        map.MoveSnake(Direction.RIGHT);
                        break;
                }
                map.snake.Draw();
                Thread.Sleep(100);
            }

        }
    }

    public class Point
    {
        public int x;
        public int y;
        public string sym;
        public Point(int x, int y, string symbol)
        {
            this.x = x;
            this.y = y;
            sym = symbol;
        }
        public void Draw()
        {
            Console.SetCursorPosition(x * 2, y);
            Console.Write(sym);
        }
        public void Clear()
        {
            sym = "  ";
            Draw();
        }
        public void ReplaceSym(string symbol)
        {
            sym = symbol;
        }
    }
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public class Snake
    {
        public List<Point> body = new List<Point>();
        public Point? snakeHead;
        public string headSymbol = "◆";
        public string bodySymbol = "≡";

        public void MoveBody()
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].x = body[i - 1].x;
                body[i].y = body[i - 1].y;
            }

        }
        public void EatFood()
        {
            body.Add(new Point(body.Last().x, body.Last().y, bodySymbol));
        }

        public void Draw()
        {
            for (int i = body.Count -1; i >= 0; i--)
            {
                body[i].Draw();
            }
        }
    }

    public class Map
    {
        public Point[,] map2D = new Point[45, 80];
        private const string wallSymbol = "■";
        private const string foodSymbol = "★";
        public Snake snake;
        private const int foodAmountCap = 5;
        private int currentFoodAmount = 0;

        public Map()
        {
            Init();
            MakeWalls();
            MakeSnake();
            MakeFood();
        }
        // 맵 생성
        public void Init()
        {
            for (int i = 0; i < map2D.Length; i++)
            {
                int x = i % map2D.GetLength(1);
                int y = i / map2D.GetLength(1);
                // Border 설정
                if (x == 0 || y == 0 || x == map2D.GetLength(1) - 1 || y == map2D.GetLength(0) - 1)
                {

                    map2D[y, x] = new Point(x, y, wallSymbol);
                }
                else
                {
                    map2D[y, x] = new Point(x, y, "  ");
                }
            }
        }
        // 랜덤으로 벽 생성
        public void MakeWalls()
        {
            Random rand = new Random();
            bool makedEnough = false;

            while (!makedEnough)
            {
                WallPattern wallPattern = (WallPattern)rand.Next((int)(WallPattern.LENGTH));
                Wall wall = new Wall(wallPattern);

                int randX = rand.Next(map2D.GetLength(1));
                int randY = rand.Next(map2D.GetLength(0));
                if (isEmptyArea(map2D[randY, randX], wall.boxSize))
                {
                    int x = randX;
                    int y = randY;
                    for (int i = 0; i < wall.boxSize * wall.boxSize; i++)
                    {
                        if (x >= randX + wall.boxSize)
                        {
                            x = randX;
                            y++;
                        }

                        map2D[y, x].ReplaceSym(wall.patternSymbol[i]);
                        x++;
                    }
                }
                if (GetEmptyPointPercentage() < 0.7f)
                    makedEnough = true;
            }

        }
        public float GetEmptyPointPercentage()
        {
            int counter = 0;
            foreach (Point p in map2D)
            {
                if (p.sym == "  ")
                    counter++;
            }
            return (float)counter / (map2D.GetLength(0) * map2D.GetLength(1));
        }
        public bool isEmptyArea(Point startPoint, int boxSize)
        {
            int x = startPoint.x;
            int y = startPoint.y;
            
            if (x >= map2D.GetLength(1) - boxSize || y >= map2D.GetLength(0) - boxSize)
                return false;

            for (int i = 0; i < boxSize * boxSize; i++)
            {
                if (x >= startPoint.x + boxSize)
                {
                    x = startPoint.x;
                    y++;
                }

                if (map2D[y, x].sym != "  ")
                {
                    return false;
                }
                else
                {
                    x++;
                }
            }
            return true;
        }
        public void DrawMap()
        {
            for (int i = 0; i < map2D.Length; i++)
            {
                int x = i % map2D.GetLength(1);
                int y = i / map2D.GetLength(1);
                map2D[y, x].Draw();

            }
            snake.Draw();
        }
        public void MakeSnake()
        {
            snake = new Snake();
            Random rand = new Random();
            bool isMaked = false;
            while(!isMaked)
            {
                int x = rand.Next(map2D.GetLength(1));
                int y = rand.Next(map2D.GetLength(0));
                Point startPoint = new Point(x, y, snake.headSymbol);
                if (isEmptyArea(startPoint, 1))
                {
                    snake.snakeHead = startPoint;
                    snake.body.Add(snake.snakeHead);
                    isMaked = true;
                }
            }
            
        }
        public void MoveSnake(Direction direction)
        {
            if (snake.snakeHead == null)
                return;

            int x = snake.snakeHead.x;
            int y = snake.snakeHead.y;

            switch (direction)
            {
                case Direction.LEFT:
                    x--;
                    break;
                case Direction.RIGHT:
                    x++;
                    break;
                case Direction.UP:
                    y--;
                    break;
                case Direction.DOWN:
                    y++;
                    break;
            }

            if (map2D[y, x].sym == wallSymbol)
                return;

            if (map2D[y, x].sym == foodSymbol)
            {
                snake.EatFood();
                MakeFood();
            }
            Point tailPoint = map2D[snake.body.Last().y, snake.body.Last().x];
            snake.MoveBody();
            snake.snakeHead.x = x;
            snake.snakeHead.y = y;
            tailPoint.Clear();
        }

        public void MakeFood()
        {
            Random rand = new Random();
            bool isEnough = false;

            while (!isEnough)
            {
                int x = rand.Next(map2D.GetLength(1));
                int y = rand.Next(map2D.GetLength(0));
                if (map2D[y, x].sym != "  ")
                    continue;
                map2D[y, x].ReplaceSym(foodSymbol);
                map2D[y, x].Draw();
                currentFoodAmount++;

                if (currentFoodAmount >= foodAmountCap)
                {
                    isEnough = true;
                }
            }
        }
    }
    public enum WallPattern
    {
        BOXPATTERN,
        UPATTERN,
        REVERSEUPATTERN,
        SPATTERN,
        LENGTH
    }
    public class Wall
    {
        public WallPattern pattern;
        public int boxSize;
        public string[] patternSymbol;
        public Wall(WallPattern pattern)
        {
            switch (pattern)
            {
                case WallPattern.BOXPATTERN:
                    {
                        this.pattern = pattern;
                        boxSize = 4;
                        break;
                    }
                case WallPattern.UPATTERN:
                    {
                        this.pattern = pattern;
                        boxSize = 5;
                        break;
                    }
                case WallPattern.REVERSEUPATTERN:
                    {
                        this.pattern = pattern;
                        boxSize = 5;
                        break;
                    }
                case WallPattern.SPATTERN:
                    {
                        this.pattern = pattern;
                        boxSize = 7;
                        break;
                    }
            }
            SelectSymbol();
        }
        // 패턴에 따라 벽모양 문자열 선택
        private void SelectSymbol()
        {
            switch (pattern)
            {
                case WallPattern.BOXPATTERN:
                    {
                        string[] symbol = {
                                                "  ", "  ", "  ", "  ",
                                                "  ", "■", "■", "  ",
                                                "  ", "■", "■", "  ",
                                                "  ", "  ", "  ", "  ",};
                        patternSymbol = symbol;
                        break;
                    }
                case WallPattern.UPATTERN:
                    {
                        string[] symbol = {
                                                "  ", "  ", "  ", "  ", "  ",
                                                "  ", "■", "  ", "■", "  ",
                                                "  ", "■", "  ", "■", "  ",
                                                "  ", "■", "■", "■", "  ",
                                                "  ", "  ", "  ", "  ", "  "};
                        patternSymbol = symbol;
                        break;
                    }
                case WallPattern.REVERSEUPATTERN:
                    {
                        string[] symbol = {
                                                "  ", "  ", "  ", "  ", "  ",
                                                "  ", "■", "■", "■", "  ",
                                                "  ", "■", "  ", "■", "  ",
                                                "  ", "■", "  ", "■", "  ",
                                                "  ", "  ", "  ", "  ", "  "};
                        patternSymbol = symbol;
                        break;
                    }
                case WallPattern.SPATTERN:
                    {
                        string[] symbol = {
                                                "  ", "  ", "  ", "  ", "  ", "  ", "  ",
                                                "  ", "■", "■", "■", "■", "■", "  ",
                                                "  ", "■", "  ", "  ", "  ", "  ", "  ",
                                                "  ", "■", "■", "■", "■", "■", "  ",
                                                "  ", "  ", "  ", "  ", "  ", "■", "  ",
                                                "  ", "■", "■", "■", "■", "■", "  ",
                                                "  ", "  ", "  ", "  ", "  ", "  ", "  "};
                        patternSymbol = symbol;
                        break;
                    }
            }
        }
    }
}