using System;
using Android.Speech.Tts;
using Java.Util;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using shSpeak.Droid.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CTextToSpeech))]
namespace shSpeak.Droid.Interface
{
    public class CTextToSpeech : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {
        public event EventHandler SpeechCompleted;

        private TextToSpeech speaker;
        private Locale language = Locale.Korean;

        public bool IsInitialized { get; private set; }
        public bool IsSpeaking { get; private set; }

        public CTextToSpeech()
        {
            speaker = new TextToSpeech(Forms.Context.ApplicationContext, this);
        }


        public void Speak(string sText, double pitch, double speed)
        {
            if (this.IsSpeaking || !this.IsInitialized)
                return;

            this.IsSpeaking = true;
            try
            {
                this.speaker.SetLanguage(language);

                this.speaker.SetPitch((float)pitch);
                this.speaker.SetSpeechRate((float)speed);
                //this.speaker.SetVoice();
              
                this.speaker.Speak(sText, QueueMode.Flush, null, null);
            }
            finally
            {
                this.IsSpeaking = false;
            }

            //음성 읽기 완성
            SpeechCompleted(this, null);
        }

        public void Stop()
        {
            if (!this.IsSpeaking) return;

            try
            {
                this.speaker.Stop();
            }
            finally
            {
                this.IsSpeaking = false;
            }
        }

        public void SetLanguage(string sLanguage)
        {
            switch (sLanguage)
            {
                case "Japanese":
                    this.language = Locale.Japanese;
                    break;

                case "Korean":
                    this.language = Locale.Korean;
                    break;

                default:
                    this.language = Locale.Us;
                    break;
            }
        }

        public void OnInit(OperationResult status)
        {
            this.IsInitialized = (status == OperationResult.Success);
        }

        public IEnumerable<string> GetInstalledLanguages()
        {
            /*
            Locale[] locale = Locale.GetAvailableLocales();
            foreach(Locale loc in locale)
            {
                string s1 = loc.DisplayCountry;
                string s2 = loc.DisplayLanguage;
                string s3 = loc.DisplayName;
                //System.Diagnostics.Debug.WriteLine(s2);

                LanguageAvailableResult res = speaker.IsLanguageAvailable(loc);
                switch (res)
                {
                    case LanguageAvailableResult.Available:
                        System.Diagnostics.Debug.WriteLine(s2);
                        //langAvailable.Add(loc.DisplayLanguage);
                        break;
                    case LanguageAvailableResult.CountryAvailable:
                        //langAvailable.Add(loc.DisplayLanguage);
                        break;
                    case LanguageAvailableResult.CountryVarAvailable:
                        //langAvailable.Add(loc.DisplayLanguage);
                        break;
                }                
            }
            */
           
            return Locale.GetAvailableLocales().Select(a => a.Language).Distinct();
       }

        public IEnumerable<CrossLocale> GetCrossLocales()
        {
            return Locale.GetAvailableLocales().Select(a => new CrossLocale { Country = a.Country, Language = a.Language, DisplayName = a.DisplayName });
        }

    }


    public struct CrossLocale
    {
        public string Language { get; set; }
        public string Country { get; set; }
        public string DisplayName { get; set; }
        
        public override string ToString()
        {
            return Language +
              (string.IsNullOrWhiteSpace(Country) ? string.Empty : "-" + Country);
        }
    }

}