using System;
using Android.App;
using Android.Content;
using Android.Views.InputMethods;
using Xamarin.Forms;
using shSpeak.Droid.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CKeyBoard))]
namespace shSpeak.Droid.Interface
{
    public class CKeyBoard : IKeyBoard
    {
        /*
        public void HideKeyboard()
        {
            var context = Forms.Context;
            var inputMedthodManager = context.GetSystemService(Context.InputService) as InputMethodManager;
            if (inputMedthodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMedthodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
                activity.Window.DecorView.ClearFocus();
            }
        }
        */

        public void HideKeyboard()
        {
            var inputMethodManager = Xamarin.Forms.Forms.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && Xamarin.Forms.Forms.Context is Activity)
            {
                var activity = Xamarin.Forms.Forms.Context as Activity;
                var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, 0);
            }
        }
        
    }
}