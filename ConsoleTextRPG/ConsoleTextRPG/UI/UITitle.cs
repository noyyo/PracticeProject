using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRPG.UI
{
    [SupportedOSPlatform("windows")]
    public class UITitle : UIWindow
    {
        public UITitle(int width, int height) : base(width, height)
        {
            WindowName = "Title";
            SetLayout();
        }

        public void SetLayout()
        {
            base.SetLayout();
            string titleIcon =
            "⢠⣾⠟⠛⠃⠀⣠⣤⡀⢀⣀⣠⣄⠀⢀⣤⣄⠀⣠⣤⡀⠀⣿⠀⢀⣤⣀⢸⣿⡟⣿⣧⢸⣿⠛⣿⡄⣴⡟⠛⠛⠀\r\n" +
            "⢸⣿⠀⠀⠀⣾⡏⠙⣿⣾⣿⠉⣿⡇⢿⣭⣍⣾⡏⠙⣿⡄⣿⢸⣿⣭⣿⣾⣿⣿⣿⡁⢸⣿⡾⠟⠑⣿⡀⠿⣿⡇\r\n" +
            "⠈⠻⢷⣶⠇⠙⢷⡾⠟⠸⠿⠀⠿⠇⢶⣾⠟⠹⢷⣾⠟⠀⠿⠘⠻⣶⡾⠸⠿⠇⠹⠷⠸⠿⠀⠀⠀⠻⢿⣶⠿⠃";
            SetTextExceptBorder(titleIcon, 0, Height / 2 - 5, Alignment.Middle);
        }





    }
}
