using System;

namespace AppiumCore.Base
{
    public class ScreenFactory
    {
        private static Lazy<ScreenFactory> _instance = new Lazy<ScreenFactory>(() => new ScreenFactory());

        public static ScreenFactory Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private ScreenFactory() { }

        public BaseScreen CurrentPage { get; set; }
    }
}
