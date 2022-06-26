using AppiumCore.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIMappings.Screens
{
    public class ScreenUtilities
    {
        public static BaseScreen GetScreen(string screen)
        {
            var returnScreen = new BaseScreen();
            switch (screen)
            {
                case "HomeScreen":
                    returnScreen = new HomeScreen();
                    break;
                case "LoginScreen":
                    returnScreen = new LoginScreen();
                    break;
                default:
                    break;
            }

            return returnScreen;
        }
    }
}
