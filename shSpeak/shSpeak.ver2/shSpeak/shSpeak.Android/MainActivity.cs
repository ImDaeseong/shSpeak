using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Android.Preferences;

namespace shSpeak.Droid
{
    [Activity(Label = "shSpeak", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            //RemoveShortcut();
            //AddShortcut();
        }

        private void AddShortcut()
        {            
            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            var IsShortCut = sharedPreferences.GetBoolean("IsShortCut", false);
            if (!IsShortCut)
            {
                var shortcutIntent = new Intent(this, typeof(SplashActivity));
                shortcutIntent.SetAction(Intent.ActionMain);

                var iconResource = Intent.ShortcutIconResource.FromContext(this, Resource.Drawable.icon);

                var intent = new Intent();
                intent.PutExtra(Intent.ExtraShortcutIntent, shortcutIntent);
                intent.PutExtra(Intent.ExtraShortcutName, "혼자 말하기");
                intent.PutExtra(Intent.ExtraShortcutIconResource, iconResource);
                intent.SetAction("com.android.launcher.action.INSTALL_SHORTCUT");
                intent.PutExtra("duplicate", false);
                SendBroadcast(intent);

                //업데이트 설정
                ISharedPreferencesEditor editor = sharedPreferences.Edit();
                editor.PutBoolean("IsShortCut", true);
                editor.Apply();
            }                  
        }
               
        private void RemoveShortcut()
        {
            var shortcutIntent = new Intent(this, typeof(SplashActivity));
            shortcutIntent.SetAction(Intent.ActionMain);

            var intent = new Intent();
            intent.PutExtra(Intent.ExtraShortcutIntent, shortcutIntent);
            intent.PutExtra(Intent.ExtraShortcutName, "혼자 말하기");
            intent.SetAction("com.android.launcher.action.UNINSTALL_SHORTCUT");
            SendBroadcast(intent);
        }

    }
}

