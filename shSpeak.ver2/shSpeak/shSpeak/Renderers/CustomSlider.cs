using System;
using Xamarin.Forms;

namespace shSpeak.Renderers
{
    public class CustomSlider : Slider
    {
        public static readonly BindableProperty IsErrorProperty = BindableProperty.Create("IsError", typeof(bool), typeof(CustomSlider), false);

        public bool IsError
        {
            get { return (bool)GetValue(IsErrorProperty); }
            set { SetValue(IsErrorProperty, value); }
        }

        public CustomSlider() { }
    }
}
