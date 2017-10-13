using Android.Widget;
using Xamarin.Forms;
using shSpeak.Droid.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CToast))]
namespace shSpeak.Droid.Interface
{
    public class CToast : IToast
    {
        public void Show(string sMessage)
        {
            Toast.MakeText(Android.App.Application.Context, sMessage, ToastLength.Short).Show();
        }
    }
}