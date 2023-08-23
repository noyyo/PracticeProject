using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG
{

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
            switch (direction)
            {
                case Direction.UP:
                    _playerIcon.Y--;
                    break;
                case Direction.DOWN:
                    _playerIcon.Y++;
                    break;
                case Direction.LEFT:
                    _playerIcon.X--;
                    break;
                case Direction.RIGHT:
                    _playerIcon.X++;
                    break;
                default:
                    break;
            }
            Draw();
        }



    }
}
