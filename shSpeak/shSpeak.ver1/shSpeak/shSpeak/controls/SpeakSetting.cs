using shSpeak.Interface;
using Xamarin.Forms;

namespace shSpeak.controls
{
    public class SpeakSetting
    {
        private static SpeakSetting selfInstance = null;
        public static SpeakSetting getInstance
        {
            get
            {
                if (selfInstance == null) selfInstance = new SpeakSetting();
                return selfInstance;
            }
        }

        static ISpeakSetting LocalSettings
        {
            get
            {
                return DependencyService.Get<ISpeakSetting>();
            }
        }

        public string BGImagePath
        {
            get
            {
                return LocalSettings.BGImagePath;
            }
            set
            {
                LocalSettings.BGImagePath = value;
            }
        }

        public string UserImagePath
        {
            get
            {
                return LocalSettings.UserImagePath;
            }
            set
            {
                LocalSettings.UserImagePath = value;
            }
        }

        public float SliderPitch
        {
            get
            {
                return LocalSettings.SliderPitch;
            }
            set
            {
                LocalSettings.SliderPitch = value;
            }
        }

        public float SliderRate
        {
            get
            {
                return LocalSettings.SliderRate;
            }
            set
            {
                LocalSettings.SliderRate = value;
            }
        }

        public bool IntroPopup
        {
            get
            {
                return LocalSettings.IntroPopup;
            }
            set
            {
                LocalSettings.IntroPopup = value;
            }
        }


        public bool IsFileExist(string sFilename)
        {
            return LocalSettings.IsFileExist(sFilename);
        }
    }
}
