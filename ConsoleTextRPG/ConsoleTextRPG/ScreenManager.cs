using ConsoleTextRPG.Map;
using ConsoleTextRPG.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG
{
    [SupportedOSPlatform("windows")]
    public class ScreenManager
    {
        private static ScreenManager _i = new ScreenManager();
        public static ScreenManager I
        {
            get
            {
                if (_i == null)
                {
                    _i = new ScreenManager();
                }
                return _i;
            }
        }
        public const int SCREEN_WIDTH = 200;
        public const int SCREEN_HEIGHT = 50;
        private const int BUFFER_WIDTH = 300;
        private const int BUFFER_HEIGHT = 100;

        public List<UIWindow> windows;
        private UIWindow? _openedWindow;

        public List<ConsoleMap> maps;
        private ConsoleMap? _currentMap;

        public ScreenManager()
        {
            SetConsoleWindow();
            windows = new List<UIWindow>();
            maps = new List<ConsoleMap>();
            LoadAllInterface();
            LoadAllMap();
            _currentMap?.Draw();
        }
        public void SetConsoleWindow()
        {
            Console.SetBufferSize(BUFFER_WIDTH, BUFFER_HEIGHT);
            Console.SetWindowSize(SCREEN_WIDTH + 1, SCREEN_HEIGHT);
            Console.SetWindowPosition(0, 0);
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;

        }
        public void Draw<T>(T drawable) where T : IDrawable
        {
            drawable.Draw();
        }

        public void OpenUIWindow(string windowName)
        {   
            UIWindow? window = windows.Find(window => window.WindowName == windowName);
            window?.OpenWindow();
            _openedWindow = window;
        }
        public void CloseUIWindow()
        {
            _openedWindow = null;
            _currentMap?.Draw();
            GameManager.I.Player.Draw();
        }
        public void HandleKeyboardInput(ConsoleKeyInfo keyInput)
        {
            switch (keyInput.Key)
            {
                case ConsoleKey.I:
                    if (_openedWindow?.WindowName == "Inventory")
                    {
                        CloseUIWindow();
                    }
                    else
                    {
                        OpenUIWindow("Inventory");
                    }
                    break;
                default:
                    _openedWindow?.HandleKeyboardInput(keyInput);
                    break;
            }
        }
        private void LoadAllInterface()
        {
            windows.Add(new UIInventory(SCREEN_WIDTH, SCREEN_HEIGHT));
            
        }
        private void LoadAllMap()
        {
            VillageMap villageMap = new VillageMap(SCREEN_WIDTH, SCREEN_HEIGHT);
            villageMap.LoadMap();
            _currentMap = villageMap;
            // TODO : 맵 정보 파일 또는 코드에서 불러와서 List에 추가.
        }
        public bool isMovable(int posX, int posY)
        {
            if (_currentMap == null) 
                return false;
            return _currentMap.isMovable2D[posX, posY];
        }
    }
}
