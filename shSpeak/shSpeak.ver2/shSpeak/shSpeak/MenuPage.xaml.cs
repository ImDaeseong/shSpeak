using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using shSpeak.controls;
using shSpeak.Interface;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Media.Abstractions;

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

        private string GetFileName(string strFilename)
        {
            int nPos = strFilename.LastIndexOf('/');
            int nLength = strFilename.Length;
            if (nPos < nLength)
                return strFilename.Substring(nPos + 1, (nLength - nPos) - 1);
            return strFilename;
        }

        private string GetOnlyFileName(string strFilename)
        {
            string sFilename = GetFileName(strFilename);
            int nPos = sFilename.LastIndexOf('.');
            return sFilename.Substring(0, nPos);
        }

        private string GetFileExtName(string strFilename)
        {
            int nPos = strFilename.LastIndexOf('.');
            int nLength = strFilename.Length;
            if (nPos < nLength)
                return strFilename.Substring(nPos + 1, (nLength - nPos) - 1);
            return string.Empty;
        }

        private string GetFilePath(string strFilename)
        {
            int nPos = strFilename.LastIndexOf('/');
            return strFilename.Substring(0, nPos);
            //return strFilename.Substring(0, nPos + 1);
        }

        private static long GetFileSize(MediaFile file)
        {
            using (var stream = file.GetStream())
            {
                var size = 0L;

                var buffer = new byte[1024 * 1024 * 10];
                var bytesRead = 0L;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    size += bytesRead;
                }
                return size;
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

                //파일 사이즈 큰 경우 오류
                /*
                var file = await CrossMedia.Current.PickPhotoAsync();
                if (file != null && file.Path != null)
                {
                    imagePath = file.Path;
                }
                return imagePath;
                */

                
                var file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null && file.Path == null) return null;


                // up to 2Mb
                if (GetFileSize(file) > (1024 * 1024 * 2) )
                {
                    string sTempFileName = string.Format("Pic{0}.{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), GetFileExtName(file.Path));
                    string sSavedPath = Path.Combine(GetFilePath(file.Path), sTempFileName);

                    byte[] buffer = null;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.GetStream().CopyTo(memoryStream);
                        file.Dispose();
                        buffer = memoryStream.ToArray();
                    }

                    buffer = DependencyService.Get<IimageResize>().ResizeImage(buffer, 350, 300);
                    DependencyService.Get<IimageResize>().SaveImage(sSavedPath, buffer);

                    imagePath = sSavedPath;
                }
                else
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