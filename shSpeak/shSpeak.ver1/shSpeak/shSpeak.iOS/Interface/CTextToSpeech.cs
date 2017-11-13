using System;
using AVFoundation;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using shSpeak.iOS.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CTextToSpeech))]
namespace shSpeak.iOS.Interface
{
    public class CTextToSpeech : ITextToSpeech
    {
        public event EventHandler SpeechCompleted;

        private readonly AVSpeechSynthesizer speechSynthesizer = new AVSpeechSynthesizer();
        private AVSpeechSynthesisVoice language = AVSpeechSynthesisVoice.FromLanguage("ko-KR");

        public CTextToSpeech()
        {
        }

        public bool IsSpeaking
        {
            get { return this.speechSynthesizer.Speaking; }
        }

        public void Speak(string text, double pitch, double speed)
        {
            var speechUtterance = new AVSpeechUtterance(text)
            {
                Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
                Voice = language,//AVSpeechSynthesisVoice.FromLanguage("ko-KR"),
                Volume = 0.5f,
                PitchMultiplier = (float)pitch//1.0f
            };
            speechSynthesizer.SpeakUtterance(speechUtterance);
            
            //음성 읽기 완성
            SpeechCompleted(this, null);
        }

        public void Stop()
        {
            this.speechSynthesizer.StopSpeaking(AVSpeechBoundary.Immediate);
        }

        public void SetLanguage(string sLanguage)
        {
            switch (sLanguage)
            {
                case "Japanese":
                    this.language = AVSpeechSynthesisVoice.FromLanguage("ja-JP");
                    break;

                case "Korean":
                    this.language = AVSpeechSynthesisVoice.FromLanguage("ko-KR");
                    break;

                default:
                    this.language = AVSpeechSynthesisVoice.FromLanguage("en-US");
                    break;
            }
        }

        public IEnumerable<string> GetInstalledLanguages()
        {
            /*
            AVSpeechSynthesisVoice[] locale = AVSpeechSynthesisVoice.GetSpeechVoices();
            foreach (AVSpeechSynthesisVoice loc in locale)
            {
            }
            */

            return AVSpeechSynthesisVoice.GetSpeechVoices().Select(a => a.Language).Distinct();
        }

        public IEnumerable<CrossLocale> GetCrossLocales()
        {
            return null;
            //return AVSpeechSynthesisVoice.GetSpeechVoices().Select(a => new CrossLocale { Country = a..Country, Language = a.Language, DisplayName = a.DisplayName });
        }

    }


    public struct CrossLocale
    {
        public string Language { get; set; }
        public string Country { get; set; }
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return Language + (string.IsNullOrWhiteSpace(Country) ? string.Empty : "-" + Country);
        }
    }
}