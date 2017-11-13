using Xamarin.Forms;
using shSpeak.Interface;

namespace shSpeak.controls
{
    public class KeyBoardSetting
    {
        private static KeyBoardSetting selfInstance = null;
        public static KeyBoardSetting getInstance
        {
            get
            {
                if (selfInstance == null) selfInstance = new KeyBoardSetting();
                return selfInstance;
            }
        }

        static IKeyBoard KeyBoardSet
        {
            get
            {
                return DependencyService.Get<IKeyBoard>();
            }
        }

        public void HideKeyboard()
        {
            KeyBoardSet.HideKeyboard();
        }
    }
}
