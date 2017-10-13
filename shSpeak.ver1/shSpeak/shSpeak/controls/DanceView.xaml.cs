using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using shSpeak.Interface;

namespace shSpeak.controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DanceView : ContentView
    {
        private IConvertImage convertImg;
        private int CurrentIndex = 0;
        private static readonly int TotalCount = 16;
        private ImageSource[] imgList = new ImageSource[TotalCount];

        public DanceView()
        {
            InitializeComponent();

            convertImg = DependencyService.Get<IConvertImage>();
            convertImg.LoadImageSource("dance.png");
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
            {
                ChangeImgTimer();
            }
        }

        private ImageSource LoadByteImage(byte[] buffer)
        {
            ImageSource retSource = null;
            if (buffer != null)
                retSource = ImageSource.FromStream(() => new MemoryStream(buffer));
            return retSource;
        }

        private void ChangeImgTimer()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                Task.Factory.StartNew(async () =>
                {
                    await Task.Run(() => ChangeImageIndex());

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //이미지 쪼개서 하나의 이미지소스에 하면 깜빡임 현상이 생기네
                        changeimg.Source = LoadByteImage(convertImg.Crop(CurrentIndex));

                        ChangeImgTimer();
                    });
                });

                return false;
            });
        }

        private void ChangeImageIndex()
        {
            CurrentIndex++;

            if (CurrentIndex >= (TotalCount))
                CurrentIndex = 0;
        }

    }

}