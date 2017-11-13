using Android.App;
using Android.OS;
using System.Threading.Tasks;
using System.Threading;
using Android.Content;

namespace shSpeak.Droid
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon", NoHistory = true, Theme = "@style/Theme.Splash")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SplashLayout);
            
            //LoadMainActivity();
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() => { LoadMainActivity(); });
            startupWork.Start();
        }


        private async void LoadMainActivity()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));

            /*
            await Task.Run(() => {
                Thread.Sleep(100);
                StartActivity(typeof(MainActivity));
                Finish();
            });
            */
        }

        public override void OnBackPressed()
        {
        }

    }
}