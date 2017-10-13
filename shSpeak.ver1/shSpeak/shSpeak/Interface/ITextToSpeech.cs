using System;
using System.Collections.Generic;

namespace shSpeak.Interface
{
    public interface ITextToSpeech
    {
        event EventHandler SpeechCompleted;

        void Speak(string sText, double pitch, double speed);
        void SetLanguage(string sLanguage);
        bool IsSpeaking { get; }

        IEnumerable<string> GetInstalledLanguages();
    }

}
