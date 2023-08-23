using ConsoleTextRPG.UI;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json;

namespace ConsoleTextRPG
{
    [SupportedOSPlatform("windows")]
    public class Program
    {
        public const int SCREEN_WIDTH = 201;
        public const int SCREEN_HEIGHT = 50;
        private const int BUFFER_WIDTH = 300;
        private const int BUFFER_HEIGHT = 100;
        
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.Init();
            
        }
    }
    public class Entrance
    {

    }
}