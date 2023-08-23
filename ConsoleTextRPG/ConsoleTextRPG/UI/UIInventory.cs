using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace ConsoleTextRPG.UI
{
    [SupportedOSPlatform("windows")]
    public class UIInventory : UIWindow
    {
        public Inventory inven
        {
            get { return GameManager.I.Player.Inven; }
        }
        public UIPlayerStatus status;

        public UIInventory(int width, int height) : base(width, height)
        {
            WindowName = "Inventory";
            status = new UIPlayerStatus(50, 45);
            AddComponent(status);
            SetLayout();
        }
        public void SetLayout()
        {
            base.SetLayout();
            string NameIcon =
                "⣀⢀⣀⠀⢀⡀⣀⠀⠀⣀⢀⣀⣀⢀⣀⠀⣀⢀⣀⣀⣀⢀⣀⣀⡀⢀⣀⣀⡀⣀⡀⢀⡀ \r\n" +
                "⣿⢸⡿⣧⢸⡇⢹⣇⣸⡏⢸⣏⡉⢸⣿⣆⣿⠈⢹⣿⠉⣿⠉⠙⣿⢸⣿⣸⡿⠸⣷⡿⠁\r\n" +
                "⣿⢸⡇⠹⣿⡇⠀⣿⡿⠀⢸⣇⣀⢸⡇⠹⣿⠀⢸⣿⠀⢿⣆⣰⡿⢸⣿⠹⣧⠀⢹⡇⠀⠀";
            SetTextExceptBorder(NameIcon, 0, 0, Alignment.Middle);
            SetSeparatorBar(1, 5, Width - 2);
            status.SetPosition(0, 5);
            status.SetStatus();
            SetText("이름", 50 + 5, 6);
            SetText("종류", 74 + 5, 6);
            SetText("공격력", 88 + 4, 6);
            SetText("마법공격력", 102 + 2, 6);
            SetText("방어력", 116 + 4, 6);
            SetText("마법방어력", 130 + 2, 6);
            SetText("가격", 144 + 5, 6);
            SetText("설명", 160 + 18, 6);
            SetSeparatorBar(50, 7, 149);
            SetText("├", 49, 7);
            SetText("┤", 199, 7);

        }

        public override void HandleKeyboardInput(ConsoleKeyInfo keyInput)
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
