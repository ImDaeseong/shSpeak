using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using shSpeak.controls;
using System.Threading.Tasks;

namespace shSpeak
{
    public partial class Test1Page : ContentPage
    {
        private SpeakSetting Setting = SpeakSetting.getInstance;
        private ToSpeak TextToSpeech = ToSpeak.getInstance;

        public Test1Page()
        {
            InitializeComponent();

            InitSliders();


            if (Setting.BGImagePath != "")
            {
                this.BackgroundImage = Setting.BGImagePath;
                image.Source = ImageSource.FromFile(Setting.BGImagePath);
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

        private async void TakeCamera_Clicked(object sender, System.EventArgs e)
        {
            // Init
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "shSpeak",
                Name = "bg.png"
            });

            if (file == null)
                return;

            image.Source = ImageSource.FromFile(file.Path);

            Setting.BGImagePath = file.Path;
        }

        private async void SelectImage_Clicked(object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            image.Source = ImageSource.FromFile(file.Path);

            Setting.BGImagePath = file.Path;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            TextToSpeech.Speak(textlabel.Text.ToString(), (float)sliderPitch.Value, (float)sliderRate.Value);
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

        private async Task<string> TakePhotoAsync()
        {
            try
            {
                var imagePath = string.Empty;

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return null;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());

                if (file != null && file.Path != null)
                {
                    imagePath = file.Path;
                }

                return imagePath;
            }
            catch { }

            return null;
        }

    }


}