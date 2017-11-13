using System.IO;
using Foundation;
using Xamarin.Forms;
using shSpeak.iOS.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CSpeakSetting))]
namespace shSpeak.iOS.Interface
{
    public class CSpeakSetting : ISpeakSetting
    {
        const string SETTINGSKEY_BGIMAGEPATH = "bgImagePath";
        const string SETTINGSKEY_USERIMAGEPATH = "UserImagePath";
        const string SETTINGSKEY_SLIDERPITCH = "sliderPitch";
        const string SETTINGSKEY_SLIDERRATE = "sliderRate";
        const string SETTINGSKEY_INTROPOPUP = "Intro";
              

        public string BGImagePath
        {
            get
            {
                return NSUserDefaults.StandardUserDefaults.StringForKey(SETTINGSKEY_BGIMAGEPATH);
            }
            set
            {
                NSUserDefaults.StandardUserDefaults.SetString(value, SETTINGSKEY_BGIMAGEPATH);
                NSUserDefaults.StandardUserDefaults.Synchronize();
            }
        }

        public string UserImagePath
        {
            get
            {
                return NSUserDefaults.StandardUserDefaults.StringForKey(SETTINGSKEY_USERIMAGEPATH);
            }
            set
            {
                NSUserDefaults.StandardUserDefaults.SetString(value, SETTINGSKEY_USERIMAGEPATH);
                NSUserDefaults.StandardUserDefaults.Synchronize();
            }
        }


        public float SliderPitch
        {
            get
            {
                return NSUserDefaults.StandardUserDefaults.FloatForKey(SETTINGSKEY_SLIDERPITCH);
            }
            set
            {
                NSUserDefaults.StandardUserDefaults.SetFloat(value, SETTINGSKEY_SLIDERPITCH);
                NSUserDefaults.StandardUserDefaults.Synchronize();
            }
        }

        public float SliderRate
        {
            get
            {
                return NSUserDefaults.StandardUserDefaults.FloatForKey(SETTINGSKEY_SLIDERRATE);
            }
            set
            {
                NSUserDefaults.StandardUserDefaults.SetFloat(value, SETTINGSKEY_SLIDERRATE);
                NSUserDefaults.StandardUserDefaults.Synchronize();
            }
        }

        public bool IntroPopup
        {
            get
            {
                return NSUserDefaults.StandardUserDefaults.BoolForKey(SETTINGSKEY_INTROPOPUP);
            }
            set
            {
                NSUserDefaults.StandardUserDefaults.SetBool(value, SETTINGSKEY_INTROPOPUP);
                NSUserDefaults.StandardUserDefaults.Synchronize();
            }
        }

        public bool IsFileExist(string sFilename)
        {
            return File.Exists(sFilename);
        }

    }
}