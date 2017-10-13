using System.IO;
using Android.Content;
using Xamarin.Forms;
using shSpeak.Droid.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CSpeakSetting))]
namespace shSpeak.Droid.Interface
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
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                return prefs.GetString(SETTINGSKEY_BGIMAGEPATH, "");
            }
            set
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                var prefsEditor = prefs.Edit();
                prefsEditor.PutString(SETTINGSKEY_BGIMAGEPATH, value);
                prefsEditor.Commit();
            }
        }

        public string UserImagePath
        {
            get
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                return prefs.GetString(SETTINGSKEY_USERIMAGEPATH, "");
            }
            set
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                var prefsEditor = prefs.Edit();
                prefsEditor.PutString(SETTINGSKEY_USERIMAGEPATH, value);
                prefsEditor.Commit();
            }
        }

        public float SliderPitch
        {
            get
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                return prefs.GetFloat(SETTINGSKEY_SLIDERPITCH, 0f);
            }
            set
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                var prefsEditor = prefs.Edit();
                prefsEditor.PutFloat(SETTINGSKEY_SLIDERPITCH, value);
                prefsEditor.Commit();
            }
        }

        public float SliderRate
        {
            get
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                return prefs.GetFloat(SETTINGSKEY_SLIDERRATE, 0f);
            }
            set
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                var prefsEditor = prefs.Edit();
                prefsEditor.PutFloat(SETTINGSKEY_SLIDERRATE, value);
                prefsEditor.Commit();
            }
        }

        public bool IntroPopup
        {
            get
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                return prefs.GetBoolean(SETTINGSKEY_INTROPOPUP, false);
            }
            set
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MySharedPrefs", FileCreationMode.Private);
                var prefsEditor = prefs.Edit();
                prefsEditor.PutBoolean(SETTINGSKEY_INTROPOPUP, value);
                prefsEditor.Commit();
            }
        }

        public bool IsFileExist(string sFilename)
        {
            return File.Exists(sFilename);
        }

    }
}