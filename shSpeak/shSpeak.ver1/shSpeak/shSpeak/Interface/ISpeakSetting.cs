using System;

namespace shSpeak.Interface
{
    public interface ISpeakSetting
    {
        string BGImagePath { get; set; }

        string UserImagePath { get; set; }

        float SliderPitch { get; set; }

        float SliderRate { get; set; }

        bool IntroPopup { get; set; }

        bool IsFileExist(string sFilename);
    }
}
