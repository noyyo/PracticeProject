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
        public Dictionary<int, UIItemSlot> ItemSlots;
        private int _focusIndex;
        public int FocusIndex
        {
            get { return _focusIndex; }
            set
            {
                if (value < 0 )
                {
                    _focusIndex = ItemSlots.Count - 1;
                }
                else if (value > ItemSlots.Count - 1)
                {
                    _focusIndex = 0;
                }
                else
                {
                    _focusIndex = value;
                }
            }
        }

        public UIInventory(int width, int height) : base(width, height)
        {
            WindowName = "Inventory";
            ItemSlots = new Dictionary<int, UIItemSlot>();
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
            SetItemSlots();

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
                    ReleaseFocus();
                    FocusIndex--;
                    SetFocus(GetItemSlotByIndex(FocusIndex));
                    break;
                case ConsoleKey.DownArrow:
                    ReleaseFocus();
                    FocusIndex++;
                    SetFocus(GetItemSlotByIndex(FocusIndex));
                    break;
                case ConsoleKey.Escape:
                    ScreenManager.I.CloseUIWindow();
                    GameManager.I.InputMode = GameInputMode.Game;
                    break;
            }
        }
        public void SetItemSlots()
        {
            if (inven.Items == null)
                return;
            for (int i = 0; i < inven.Items.Count; i++)
            {
                UIItemSlot slot = new UIItemSlot(148, 1, inven.Items[i]);
                slot.SetPosition(51, 8 + i);
                Components.Add(slot);
                ItemSlots.Add(i, slot);
            }
        }
        public UIItemSlot GetItemSlotByIndex(int index)
        {
            return ItemSlots[index];
        }
    }
}
