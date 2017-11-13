using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using shSpeak.controls;
using shSpeak.Interface;

namespace shSpeak
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test2Page : ContentPage
    {
        private SpeakSetting Setting = SpeakSetting.getInstance;
        private ToSpeak TextToSpeech = ToSpeak.getInstance;

        public Test2Page()
        {
            InitializeComponent();

            InitUserImage();

            InitSliders();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (this.Width < 0 || this.Height < 0) return;

            if (this.Width > this.Height)
                BgImg.HeightRequest = 100;
            else
                BgImg.HeightRequest = 150;
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
                if (textlabel.Text == "" || textlabel.Text == null) return;

                TextToSpeech.Speak(textlabel.Text.ToString(), (float)sliderPitch.Value, (float)sliderRate.Value);
            }
            catch
            {
                ToastMessage("지원되지 않는 음성입니다.");
            }
        }

        private async Task<string> PickPhotoAsync()
        {
            try
            {
                var imagePath = string.Empty;

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return null;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file != null && file.Path != null)
                {
                    imagePath = file.Path;
                }

                return imagePath;
            }
            catch { }

            return null;
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

        private async void HeadLineView_Click(object sender, EventArgs e)
        {
            try
            {
                if (((BtnEventArgs)e).ClickBtnEvent == "UserImg")
                {
                    string sPath = await PickPhotoAsync();
                    if (sPath == null || sPath == "") return;

                    UserImg.Source = ImageSource.FromFile(sPath);
                    Setting.UserImagePath = sPath;
                }
                else if (((BtnEventArgs)e).ClickBtnEvent == "BgImg")
                {
                    string sPath = await PickPhotoAsync();
                    if (sPath == null || sPath == "") return;

                    BgImg.Source = ImageSource.FromFile(sPath);
                    Setting.BGImagePath = sPath;
                }
            }
            catch
            {
                BgImg.Source = "bg.png";
                UserImg.Source = "User.png";
            }
        }

        private void ToastMessage(string sMessage)
        {
            DependencyService.Get<IToast>().Show(sMessage);
        }

    }

}