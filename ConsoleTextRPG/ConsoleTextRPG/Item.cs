using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG
{
    public class Item
    {
        public string Name;
        public string Description;
        public ItemCategory Category;
        public int MeleePower;
        public int MagicPower;
        public int Defence;
        public int MagicDefence;
        public int Price;
        [JsonConstructor]
        public Item(string name, string desc, ItemCategory category)
        {
            Name = name;
            Description = desc;
            Category = category;
        }
        public Item(string name, string desc, ItemCategory category, int melee, int magic, int def, int magicDef, int price)
        {
            Name = name;
            Description = desc;
            Category = category;
            SetItemStat(melee, magic, def, magicDef, price);
        }
        
        public void SetItemStat(int melee, int magic, int def, int magicDef, int price)
        {
            MeleePower = melee;
            MagicPower = magic;
            Defence = def;
            MagicDefence = magicDef;
            Price = price;
        }
    }
    
    
}
