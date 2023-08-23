using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG.Map
{
    public class VillageMap : ConsoleMap
    {
        public VillageMap(int width, int height) :base(width, height) 
        {
            MapName = "VillageMap";
            SetBorder();
            SetMap();
        }
        
        public void SetMap()
        {
            string storeIcon =
                "⣿⣿⣿⣿⣿⣿⡟⢹⣿⣿⣿⣿⣿⣿⢽⣿⣿⣿⣿⣿⣿⢽⣼⣿\n" +
                "⣟⠍⠋⠋⠋⠙⠀⢸⣿⡏⠉⠉⢙⢕⠕⣸⡏⠉⠉⠙⣸⣷⠝⠋\n" +
                "⣗⠀⠀⠀⠀⠀⢠⣿⣿⣿⡄⠀⢀⢕⡰⣿⢿⠀⠀⣦⣿⣿⡇⠀\n" +
                "⣿⣷⣷⡿⠁⢀⣿⣿⣿⣿⣿⣷⣾⣿⣷⣿⣶⣷⣷⣾⣿⣿⣿⣷\n" +
                "⢻⠿⠿⠇⢀⣾⣿⣿⣿⣿⣿⣿⢿⠾⠾⠿⠿⠿⡿⠿⠾⠾⠾⣿\n" +
                "⢸⠀⠀⣀⣾⠿⡿⢿⠿⡿⢿⣿⢸⠀⢸⣿⡇⠀⡇⠀⣿⡇⠀⣿ \n" +
                "⢸⣒⡒⢺⡇⠱⣧⣼⣤⡗⠁⣇⣺⣒⣫⣤⣗⣆⣇⣂⣤⣕⣒⣿\n" +
                "⢸⣿⣿⣿⡇⠀⣿⣿⣿⡇⠠⣿⣿⡿⠿⠿⠿⠟⠛⡿⠿⠿⡟⠛\n" +
                "   ⢸⣷⣾⣿⣿⣿⡳⢺⡇⠀⠀";
            SetTextExceptBorder(storeIcon, 20, 5);
        }



    }
}
