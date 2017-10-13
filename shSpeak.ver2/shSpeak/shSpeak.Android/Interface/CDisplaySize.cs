using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Xamarin.Forms;
using shSpeak.Droid.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CDisplaySize))]
namespace shSpeak.Droid.Interface
{
    public class CDisplaySize : IDisplaySize
    {
        static IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

        public CDisplaySize()
        {
        }

        public double getHieght()
        {
            Display d = windowManager.DefaultDisplay;
            Android.Util.DisplayMetrics m = new Android.Util.DisplayMetrics();
            d.GetMetrics(m);

            return (int)((m.HeightPixels) / m.Density);
        }

        public double getWidth()
        {
            Display d = windowManager.DefaultDisplay;
            Android.Util.DisplayMetrics m = new Android.Util.DisplayMetrics();
            d.GetMetrics(m);

            return (int)((m.WidthPixels) / m.Density);
        }
    }
}