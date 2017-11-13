using System;
using UIKit;
using Xamarin.Forms;
using shSpeak.iOS.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CKeyBoard))]
namespace shSpeak.iOS.Interface
{
    public class CKeyBoard : IKeyBoard
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}