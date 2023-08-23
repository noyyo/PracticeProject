using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG.UI
{
    [SupportedOSPlatform("windows")]
    public class UIPlayerStatus : UIComponent
    {
        private Player _player;
        public UIPlayerStatus(int width, int height) : base(width, height)
        {
            _player = GameManager.I.Player;
            Name = "PlayerStatus";
            IsFocusable = false;
            Init();
        }
        new public void Init()
        {
            base.Init();
            SetBorder("├", "┬", "└", "┴");
            SetStatus();
        }

        public void SetStatus()
        {
            SetTextExceptBorder(_player.ClassIcon, 0, 0, Alignment.Middle);
            SetTextExceptBorder($"{_player.Name} {_player.ClassName} LV{_player.Level} ", 0, 12, Alignment.Middle);
            SetTextExceptBorder($"경험치 : {_player.EXP} / {100 * Math.Pow(_player.Level - 1, 1.4)}", 0, 13, Alignment.Middle);
            SetSeparatorBar(1, 14, Width - 2);
            SetTextExceptBorder("최대체력 : ", 5, 16);
            SetTextExceptBorder($"{_player.MaxHP}", 5, 16, Alignment.Right);
            SetTextExceptBorder("최대마나 : ", 5, 17);
            SetTextExceptBorder($"{_player.MaxMana}", 5, 17, Alignment.Right);
            SetTextExceptBorder("공격력 : ", 5, 18);
            SetTextExceptBorder($"{_player.AttackPower}", 5, 18, Alignment.Right);
            SetTextExceptBorder("마법공격력 : ", 5, 19);
            SetTextExceptBorder($"{_player.MagicPower}", 5, 19, Alignment.Right);
            SetTextExceptBorder("방어력 : ", 5, 20);
            SetTextExceptBorder($"{_player.Defence}", 5, 20, Alignment.Right);
            SetTextExceptBorder("마법방어력 : ", 5, 21);
            SetTextExceptBorder($"{_player.MagicDefence}", 5, 21, Alignment.Right);
            SetTextExceptBorder("소지금 : ", 5, 22);
            SetTextExceptBorder($"{_player.Inven.Gold} Gold", 5, 22, Alignment.Right);
        }

    }
}
