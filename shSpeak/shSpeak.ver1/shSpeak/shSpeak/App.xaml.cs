using shSpeak.controls;
using Xamarin.Forms;

namespace shSpeak
{
    public partial class App : Application
    {
        private ToSpeak TextToSpeech = ToSpeak.getInstance;

        public App()
        {
            InitializeComponent();

            MainPage = new shSpeak.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            TextToSpeech.InitSpeak();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
