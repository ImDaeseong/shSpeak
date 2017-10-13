using System;
using UIKit;
using Foundation;
using CoreGraphics;
using Xamarin.Forms;
using shSpeak.iOS.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CToast))]
namespace shSpeak.iOS.Interface
{
    public class CToast : IToast
    {
        public void Show(string sMessage)
        {
            var toast = new Toast();
            toast.Show(UIApplication.SharedApplication.KeyWindow.RootViewController.View, sMessage);
        }
    }

    class Toast
    {       
        UIView _view;
        UILabel _label;
        int _margin = 30;
        int _height = 40;
        int _width = 150;

        NSTimer _timer = null;

        public Toast()
        {
            _view = new UIView(new CGRect(0, 0, 0, 0))
            {
                BackgroundColor = UIColor.Black,
            };
            _view.Layer.CornerRadius = (nfloat)20.0;

            _label = new UILabel(new CGRect(0, 0, 0, 0))
            {
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White
            };
            _view.AddSubview(_label);
        }
        
        public void Show(UIView parent, string message)
        {
            if (_timer != null)
            {
                _timer.Invalidate();
                _view.RemoveFromSuperview();
            }
            
            _view.Alpha = (nfloat)0.7;
            
            _view.Frame = new CGRect(
                (parent.Bounds.Width - _width) / 2,
                parent.Bounds.Height - _height - _margin,
                _width,
                _height);

            _label.Frame = new CGRect(0, 0, _width, _height);
            _label.Text = message; 

            parent.AddSubview(_view);
            
            var wait = 10; 
            _timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromMilliseconds(100), delegate {
                if (_view.Alpha <= 0)
                {
                    _timer.Invalidate();
                    _view.RemoveFromSuperview();
                }
                else
                {
                    if (wait > 0)
                    {
                        wait--;
                    }
                    else
                    {
                        _view.Alpha -= (nfloat)0.05;
                    }
                }
            });
        }
    }
    
}