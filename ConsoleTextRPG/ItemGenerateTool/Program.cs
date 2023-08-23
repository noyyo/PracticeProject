using ConsoleTextRPG;
using Newtonsoft.Json;

namespace ItemGenerateTool
{

    public class ItemGenerateTool
    {
        public static string ItemListPath = @"C:/Users/js022/Desktop/VisualStudioWorkSpace/PracticeProject/ConsoleTextRPG/items.txt";

        static void Main(string[] args)
        {
            List<Item> items = new List<Item>();
            items.Add(new Item("롱소드", "평범한 롱소드입니다.", ItemCategory.WEAPON, 5, 0, 0, 0, 100));
            items.Add(new Item("스태프", "평범한 스태프입니다.", ItemCategory.WEAPON, 0, 5, 0, 0, 100));
            items.Add(new Item("헬멧", "평범한 헬멧입니다.", ItemCategory.WEAPON, 0, 0, 3, 3, 100));
            items.Add(new Item("갑옷", "평범한 갑옷입니다.", ItemCategory.WEAPON, 0, 0, 5, 5, 100));
            items.Add(new Item("장갑", "평범한 장갑입니다.", ItemCategory.WEAPON, 0, 0, 2, 2, 100));
            items.Add(new Item("부츠", "평범한 부츠입니다.", ItemCategory.WEAPON, 0, 0, 2, 2, 100));

            File.WriteAllText(ItemListPath, JsonConvert.SerializeObject(items, Formatting.Indented));

        }
    }
}