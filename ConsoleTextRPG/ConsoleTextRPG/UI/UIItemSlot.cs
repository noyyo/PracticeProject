using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG.UI
{
    [SupportedOSPlatform("windows")]
    public class UIItemSlot : UIComponent
    {
        private Item? _item;
        private int _cellSpace = 14;
        private int _descCellSpace = 40;
        UIItemSlot(int width,  int height, Item? item = null) : base(width, height)
        {
            _item = item;
        }
        public void SetLayout()
        {
            if (_item == null)
                return;
            SetText(_item.Name, 0, 0);
            SetText(_item.Category.ToString(), _cellSpace, 0);
            SetText(_item.MeleePower.ToString(), _cellSpace * 2 + _item.MeleePower.ToString().Length / 2, 0);
            SetText(_item.MagicPower.ToString(), _cellSpace * 3 + _item.MagicPower.ToString().Length / 2, 0);
            SetText(_item.Defence.ToString(), _cellSpace * 4 + _item.Defence.ToString().Length / 2, 0);
            SetText(_item.MagicDefence.ToString(), _cellSpace * 5 + _item.MagicDefence.ToString().Length / 2, 0);
            SetText(_item.Price.ToString(), _cellSpace * 6 + _item.Price.ToString().Length / 2, 0);
            SetText(_item.Description, _cellSpace * 7, 0);
        }
        public void SetCellSpace(int cellSpace = 14)
        {
            _cellSpace = cellSpace;
            _descCellSpace = Width - _cellSpace * 7;
        }
        public void SetItem(Item item)
        {
            _item = item;
        }
    }
}
