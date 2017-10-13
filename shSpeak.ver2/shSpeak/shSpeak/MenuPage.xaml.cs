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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        //CameraPreviewPage PreviewPage;

        private SpeakSetting Setting = SpeakSetting.getInstance;
        private IFile fFile;

        public MasterDetailPage MainView { get; set; }
        public DetailPage DetailView { get; set; }

        private ILamp fLamp;
        public bool TurnOff = false;

        public MenuPage(MasterDetailPage Main, DetailPage Detail)
        {
            InitializeComponent();

            this.BackgroundColor = new Color(99, 172, 213, 0.5);//new Color(255, 255, 255, 0.5);

            InitUserImage();

            MainView = Main;
            DetailView = Detail;
            
            fFile = DependencyService.Get<IFile>();

            fLamp = DependencyService.Get<ILamp>();

            //PreviewPage = new shSpeak.CameraPreviewPage();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                TurnOff = false;
                fLamp.TurnOff();
            }
            catch { }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                TurnOff = false;
                fLamp.TurnOff();             
            }
            catch { }
        }

        private void InitUserImage()
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

        private async void loadFile_Tapped(object sender, EventArgs e)
        {
            try
            {
                IFilePicker filePicker = CrossFilePicker.Current;
                FileData fileData = await filePicker.PickFile();

                if (fileData != null)
                {
                    string sText = fFile.LoadText(fileData.FilePath);
                    DetailView.SetSpeakText(sText);
                }
            }
            catch { }

            //MainView.Detail = new NavigationPage(new AboutPage());
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

        private async void BgPng_Tapped(object sender, EventArgs e)
        {
            try
            {
                string sPath = await PickPhotoAsync();
                if (sPath == null || sPath == "") return;
                Setting.BGImagePath = sPath;
                DetailView.SetBgImage();
            }
            catch { }
        }

        private async void ProPng_Tapped(object sender, EventArgs e)
        {
            try
            {
                string sPath = await PickPhotoAsync();
                if (sPath == null || sPath == "") return;
                Setting.UserImagePath = sPath;
                UserImg.Source = ImageSource.FromFile(Setting.UserImagePath);
                DetailView.SetUserImage();
            }
            catch
            {
                UserImg.Source = "User.png";
            }
        }

        private async void DeletePng_Tapped(object sender, EventArgs e)
        {
            try
            {
                Setting.BGImagePath = "";
                Setting.UserImagePath = "";
                UserImg.Source = "User.png";
                DetailView.SetInitImage();
            }
            catch { }
        }

        private async void ImageButton_Click(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(PreviewPage);
            //return;

            try
            {
                if (TurnOff)
                {
                    TurnOff = false;
                    fLamp.TurnOff();                    
                }
                else
                {
                    TurnOff = true;
                    fLamp.TurnOn();                   
                }
            }
            catch { }
        }

    }
}