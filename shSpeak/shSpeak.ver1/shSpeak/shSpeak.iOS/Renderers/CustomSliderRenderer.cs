using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using shSpeak.Renderers;
using shSpeak.iOS.Renderers;

[assembly: ExportRenderer(typeof(CustomSlider), typeof(CustomSliderRenderer))]
namespace shSpeak.iOS.Renderers
{
    public class CustomSliderRenderer : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var newElement = e.NewElement as CustomSlider;
                Control.ThumbTintColor = UIColor.FromRGB(255, 0, 0);
                Control.MinimumTrackTintColor = UIColor.FromRGB(255, 120, 120);
                Control.MaximumTrackTintColor = UIColor.FromRGB(255, 14, 14);
            }
        }
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            var slider = (CustomSlider)Element;

            if (e.PropertyName == CustomSlider.IsErrorProperty.PropertyName)
            {
                SetBorderStyle(slider);
            }
        }
        void SetBorderStyle(CustomSlider slider)
        {
            if (slider.IsError == true)
            {
                Control.ThumbTintColor = UIColor.FromRGB(255, 0, 0);
                Control.MinimumTrackTintColor = UIColor.FromRGB(255, 120, 120);
                Control.MaximumTrackTintColor = UIColor.FromRGB(255, 14, 14);
            }
            else
            {
                Control.ThumbTintColor = UIColor.FromRGB(255, 0, 0);
                Control.MinimumTrackTintColor = UIColor.FromRGB(255, 120, 120);
                Control.MaximumTrackTintColor = UIColor.FromRGB(255, 14, 14);
            }
        }
    }
}