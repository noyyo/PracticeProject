using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG
{
    [SupportedOSPlatform("windows")]
    public class GameManager
    {
        public delegate void KeyboardInputHandler(ConsoleKey key);
        private static GameManager? _i;
        public static GameManager I
        {
            get
            {
                _i ??= new GameManager();
                return _i;
            }
            set
            {
                if (_i != null)
                    return;
                else
                    _i = value;
            }
        }

        public Player Player;
        public GameInputMode InputMode;
        private bool _isGameEnded;
        public GameManager()
        {
            Player = new Player();
            InputMode = GameInputMode.Game;
            _isGameEnded = false;
        }

        public void Init()
        {
            new ScreenManager();
            Player.Draw();
        }
        public void Update()
        {
            while (!_isGameEnded)
            {
                // FPS  = 60;
                Thread.Sleep(1000 / 60);
                GetKeyboardInput();

            }
        }
        public void GetKeyboardInput()
        {
            if (!Console.KeyAvailable)
                return;
            ConsoleKeyInfo keyInput = Console.ReadKey(true);
            HandleKeyboardInput(keyInput);
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
        public void HandleKeyboardInput(ConsoleKeyInfo keyInput)
        {
            switch (keyInput.Key) 
            {
                case ConsoleKey.Enter:
                    break;
                case ConsoleKey.LeftArrow:
                    Player.Move(Direction.LEFT);
                    break;
                case ConsoleKey.RightArrow:
                    Player.Move(Direction.RIGHT);
                    break;
                case ConsoleKey.UpArrow:
                    Player.Move(Direction.UP);
                    break;
                case ConsoleKey.DownArrow:
                    Player.Move(Direction.DOWN);
                    break;
                default:
                    InputMode = GameInputMode.UI;
                    ScreenManager.I.HandleKeyboardInput(keyInput);
                    break;
            }
        }
    }
}
