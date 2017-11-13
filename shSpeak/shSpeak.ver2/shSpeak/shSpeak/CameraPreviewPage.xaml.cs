using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using shSpeak.controls;

namespace shSpeak
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPreviewPage : ContentPage
    {
        public CameraPreviewPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (this.Width < 0 || this.Height < 0) return;

            cameraPreview.WidthRequest = this.Width - 10;
            cameraPreview.HeightRequest = this.Height - 50;
        }

        private void SwitchCamera_Tapped(object sender, EventArgs e)
        {
            if (cameraPreview.Camera == CameraOptions.Front)
            {
                cameraPreview.Camera = CameraOptions.Rear;
            }
            else
            {
                cameraPreview.Camera = CameraOptions.Front;
            }
            cameraPreview.SwitchCamera(cameraPreview.Camera);
        }

        private async void fTopMenu_Click(object sender, EventArgs e)
        {
            try
            {                
                await Navigation.PopModalAsync();
            }
            catch { }
        }
    }
}