using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using shSpeak.controls;
using shSpeak.Interface;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;

namespace shSpeak
{
    public partial class MainPage : ContentPage
    {
        private SpeakSetting Setting = SpeakSetting.getInstance;
        private ToSpeak TextToSpeech = ToSpeak.getInstance;
        private IFile fFile;
        private IDisplaySize Display;


        public MainPage()
        {
            InitializeComponent();

            InitUserImage();

            InitSliders();

            fFile = DependencyService.Get<IFile>();
            Display = DependencyService.Get<IDisplaySize>();

            TextToSpeech.TextToSpeech_Completed += TextToSpeech_TextToSpeech_Completed;

            NavigationPage.SetHasNavigationBar(this, false);

            //if ( Setting.IntroPopup == false)
            //    Navigation.PushModalAsync(new TestCarouselPage());      
        }

        private void TextToSpeech_TextToSpeech_Completed(object sender, EventArgs e)
        {
            //음성 읽기 완성시 텍스트박스 초기화
            textlabel.Text = "";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //InitFloatButton();
        }

        private void InitFloatButton()
        {
            RelativeLayout rltPage = new RelativeLayout();

            FloatingActionButton btnFloat = new controls.FloatingActionButton()
            {
                Image = "addkata.png",
                Scale = 0.5,
                HeightRequest = 80,
                WidthRequest = 80,
                ButtonColor = Color.FromHex("#FFC10E")
            };
            btnFloat.Clicked += BtnFloat_Clicked;

            Func<RelativeLayout, double> getFloatWidth = (p) => btnFloat.Measure(this.Width, this.Height).Request.Width;
            Func<RelativeLayout, double> getFloatHeight = (p) => btnFloat.Measure(this.Width, this.Height).Request.Height;

            rltPage.Children.Add(btnFloat,
                Constraint.RelativeToParent((parent) => Display.getWidth() - (getFloatHeight(parent))),
                Constraint.RelativeToParent((parent) => (Display.getHieght() / 2) - (getFloatHeight(parent)) - 20)
            );
            xScroll.Children.Add(rltPage);
        }

        private async void BtnFloat_Clicked(object sender, EventArgs e)
        {
            try
            {
                IFilePicker filePicker = CrossFilePicker.Current;
                FileData fileData = await filePicker.PickFile();

                if (fileData != null)
                {
                    string sText = fFile.LoadText(fileData.FileName);
                    textlabel.Text = sText;
                }
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

        private async void TopMenu_Click(object sender, EventArgs e)
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
                else if (((BtnEventArgs)e).ClickBtnEvent == "Eraser")
                {
                    BgImg.Source = "bg.png";
                    UserImg.Source = "User.png";
                    Setting.BGImagePath = "";
                    Setting.UserImagePath = "";
                }
            }
            catch { }
        }

        private void ToastMessage(string sMessage)
        {
            DependencyService.Get<IToast>().Show(sMessage);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                FileData file = await CrossFilePicker.Current.PickFile();
                if (file != null)
                {
                    string sText = fFile.LoadText(file.FilePath);
                    textlabel.Text = sText;
                }
            }
            catch { }
        }

    }
}
