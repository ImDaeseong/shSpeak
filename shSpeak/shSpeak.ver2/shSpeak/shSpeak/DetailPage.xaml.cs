using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using shSpeak.controls;
using shSpeak.Interface;

namespace shSpeak
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        private SpeakSetting Setting = SpeakSetting.getInstance;
        private ToSpeak TextToSpeech = ToSpeak.getInstance;
        private IDisplaySize Display;
        
        public DetailPage()
        {
            InitializeComponent();

            InitUserImage();

            InitSliders();

            Display = DependencyService.Get<IDisplaySize>();
                        
            TextToSpeech.TextToSpeech_Completed += TextToSpeech_TextToSpeech_Completed;            
        }

        private void TextToSpeech_TextToSpeech_Completed(object sender, EventArgs e)
        {
            //음성 읽기 완성시 텍스트박스 초기화
            textlabel.Text = "";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {                
                TextToSpeech.Stop();                
            }
            catch { }
        }
                
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (this.Width < 0 || this.Height < 0) return;

            if (this.Width > this.Height)
            {
                BgImg.HeightRequest = 100;
            }
            else
            {
                BgImg.HeightRequest = 150;
            }
        }

        private void InitUserImage()
        {
            try
            {
                if (Setting.BGImagePath != "")
                {
                    if (Setting.IsFileExist(Setting.BGImagePath))
                        BgImg.Source = ImageSource.FromFile(Setting.BGImagePath);
                    else
                        BgImg.Source = "bg.png";
                }

                if (Setting.UserImagePath != "")
                {
                    if (Setting.IsFileExist(Setting.UserImagePath))
                        UserImg.Source = ImageSource.FromFile(Setting.UserImagePath);
                    else
                        UserImg.Source = "User.png";
                }
            }
            catch
            {
                BgImg.Source = "bg.png";
                UserImg.Source = "User.png";
            }
        }

        private void InitSliders()
        {
            sliderPitch.Maximum = 1.0f;
            sliderPitch.Minimum = 0f;

            if (Setting.SliderPitch == 0)
                sliderPitch.Value = 0.5f;
            else
                sliderPitch.Value = Setting.SliderPitch;

            sliderRate.Maximum = 2.0f;
            sliderRate.Minimum = 0f;

            if (Setting.SliderRate == 0)
                sliderRate.Value = 1.0f;
            else
                sliderRate.Value = Setting.SliderRate;
        }

        private void SetSliders()
        {
            Setting.SliderPitch = (float)sliderPitch.Value;
            Setting.SliderRate = (float)sliderRate.Value;
        }

        private void TextButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                TextToSpeech.Stop();

                if (textlabel.Text == "" || textlabel.Text == null) return;

                TextToSpeech.Speak(textlabel.Text.ToString(), (float)sliderPitch.Value, (float)sliderRate.Value);
            }
            catch
            {
                ToastMessage("지원되지 않는 음성입니다.");
            }
        }

        private void sliderPitch_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                Setting.SliderPitch = (float)sliderPitch.Value;
            }
            catch { }
        }

        private void sliderRate_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                Setting.SliderRate = (float)sliderRate.Value;
            }
            catch { }
        }

        private void ToastMessage(string sMessage)
        {
            DependencyService.Get<IToast>().Show(sMessage);
        }

        public void SetUserImage()
        {
            try
            {
                if (Setting.UserImagePath != "")
                {
                    if (Setting.IsFileExist(Setting.UserImagePath))
                        UserImg.Source = ImageSource.FromFile(Setting.UserImagePath);
                    else
                        UserImg.Source = "User.png";
                }
            }
            catch
            {
                UserImg.Source = "User.png";
            }
        }

        public void SetBgImage()
        {
            try
            {
                if (Setting.BGImagePath != "")
                {
                    if (Setting.IsFileExist(Setting.BGImagePath))
                        BgImg.Source = ImageSource.FromFile(Setting.BGImagePath);
                    else
                        BgImg.Source = "bg.png";
                }
            }
            catch
            {
                BgImg.Source = "bg.png";
            }
        }

        public void SetInitImage()
        {
            try
            {
                BgImg.Source = "bg.png";
                UserImg.Source = "User.png";
            }
            catch { }
        }

        public void SetSpeakText(string sText)
        {           
            try
            {
                textlabel.Text = sText;                
            }
            catch
            {
                textlabel.Text = "";
            }
        }

    }
}