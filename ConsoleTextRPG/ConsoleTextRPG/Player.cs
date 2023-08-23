using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG
{

    [SupportedOSPlatform("windows")]
    public class Player : IDrawable
    {
        public string Name = "";
        public string ClassName = "";
        public string ClassIcon =
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                "⠀⢸⣿⣿⣿⣄⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⣿⠀⠀\r\n" +
                "⠀⠀⢿⣿⣿⣿⣷⣄⠀⠀⠀⢀⣴⣿⣿⣿⣿⠟⠀⠀\r\n" +
                "⠀⠀⠀⠹⢿⣿⣿⣿⣷⣄⣴⣿⣿⣿⣿⡿⠃⠀⠀⠀\r\n" +
                "⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣿⡻⣿⡿⠋⠀⠀⠀⠀⠀\r\n" +
                "⠀⠀⠀⠀⠀⠀⣨⡻⣿⣿⣿⣿⣮⡀⠀⠀⠀⠀⠀⠀\r\n" +
                "⠀⠀⠀⢾⣦⣾⣿⣿⣮⣻⣿⣿⣿⣿⣦⣾⠇⠀⠀⠀\r\n" +
                "⠀⠀⠀⣠⣿⣿⣿⢿⠟⠁⠙⢿⣿⣿⣿⣧⡀⠀⠀⠀\r\n" +
                "⠀⠀⣴⣿⣿⡿⠛⢷⡤⠀⠀⣴⡟⠻⣿⣿⣷⣄⠀⠀\r\n" +
                "⢰⣾⣿⠟⠋⠀⠀⠈⠀⠀⠀⠈⠀⠀⠀⠙⢿⣿⣶⠀\r\n" +
                "⠘⠛⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠛⠀";
        public uint Level = 1;
        public uint EXP;
        public uint CurrentHP;
        public uint MaxHP;
        public uint CurrentMana;
        public uint MaxMana;
        public uint AttackPower;
        public uint MagicPower;
        public uint Defence;
        public uint MagicDefence;
        public Inventory Inven;
        private Point _playerIcon;
        public Player()
        {
            Inven = new Inventory();
            Inven.LoadInventory();
            _playerIcon = new Point(50, 25, "♀");
        }
        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            _playerIcon.Draw();
            Console.ResetColor();
        }
        public void Move(Direction direction)
        {
            int posX = _playerIcon.X;
            int posY = _playerIcon.Y;
            switch (direction)
            {
                case Direction.UP:
                    if (!ScreenManager.I.isMovable(posX, posY - 1) || !ScreenManager.I.isMovable(posX + 1, posY - 1))
                        return;
                    new Point(posX, posY).Draw();
                    new Point(posX + 1, posY).Draw();
                    _playerIcon.Y--;
                    break;
                case Direction.DOWN:
                    if (!ScreenManager.I.isMovable(posX, posY + 1) || !ScreenManager.I.isMovable(posX + 1, posY + 1))
                        return;
                    new Point(posX, posY).Draw();
                    new Point(posX + 1, posY).Draw();
                    _playerIcon.Y++;
                    break;
                case Direction.LEFT:
                    if (!ScreenManager.I.isMovable(posX - 1, posY))
                        return;
                    new Point(posX, posY).Draw();
                    new Point(posX + 1, posY).Draw();
                    _playerIcon.X--;
                    break;
                case Direction.RIGHT:
                    if (!ScreenManager.I.isMovable(posX + 2, posY))
                        return;
                    new Point(posX, posY).Draw();
                    _playerIcon.X++;
                    break;
                default:
                    break;
            }
            Draw();
        }



    }
}
