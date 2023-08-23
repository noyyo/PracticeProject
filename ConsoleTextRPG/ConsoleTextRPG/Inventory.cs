using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG
{
    public class Inventory
    {
        public List<Item>? Items;
        private static readonly string _inventoryInfoFilePath = @"C:/Users/js022/Desktop/VisualStudioWorkSpace/PracticeProject/ConsoleTextRPG/inventoryInfo.txt";
        private static readonly string _goldFilePath = @"C:/Users/js022/Desktop/VisualStudioWorkSpace/PracticeProject/ConsoleTextRPG/Gold.txt";
        public int Gold;
        public Inventory()
        {
            LoadInventory();
        }

        public void SaveInventory()
        {
            File.WriteAllText(_inventoryInfoFilePath, JsonConvert.SerializeObject(Items));
            File.WriteAllText(_goldFilePath, JsonConvert.SerializeObject(Gold));
        }
        public void LoadInventory()
        {
            if (File.Exists(_inventoryInfoFilePath))
            {
                Items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(_inventoryInfoFilePath));
            }
            if (Items == null)
                Items = new List<Item>();
            if (File.Exists(_goldFilePath))
            {
                Gold = JsonConvert.DeserializeObject<int>(File.ReadAllText(_goldFilePath));
            }
        }
        
    }
}
