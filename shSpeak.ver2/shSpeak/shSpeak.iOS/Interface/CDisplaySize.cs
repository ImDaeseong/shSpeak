using System;
using UIKit;
using Xamarin.Forms;
using shSpeak.iOS.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CDisplaySize))]
namespace shSpeak.iOS.Interface
{
    public class CDisplaySize : IDisplaySize
    {
        public CDisplaySize()
        {
        }

        public double getHieght()
        {
            return UIScreen.MainScreen.Bounds.Height;
        }

        public double getWidth()
        {
            return UIScreen.MainScreen.Bounds.Width;
        }
    }

}