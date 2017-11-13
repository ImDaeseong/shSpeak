using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using shSpeak.Renderers;
using shSpeak.Droid.Renderers;

[assembly: ExportRenderer(typeof(CustomSlider), typeof(CustomSliderRenderer))]
namespace shSpeak.Droid.Renderers
{
    public class CustomSliderRenderer : SliderRenderer
    {
        global::Android.Widget.SeekBar nativeSlider { get; set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                nativeSlider = (global::Android.Widget.SeekBar)Control;
                var newElement = e.NewElement as CustomSlider;

                if (newElement != null)
                {
                    //events                  
                }

                //Control.SetPadding(15, 0, 15, 0);

                //nativeSlider.ProgressDrawable.SetColorFilter(new PorterDuffColorFilter(Xamarin.Forms.Color.Blue.ToAndroid(), PorterDuff.Mode.SrcIn));
                //nativeSlider.Thumb.SetColorFilter(new PorterDuffColorFilter(Xamarin.Forms.Color.Blue.ToAndroid(), PorterDuff.Mode.SrcIn));

                Control.SetPadding(30, 0, 30, 0);

                nativeSlider.ProgressDrawable.SetColorFilter((new Android.Graphics.Color(86, 160, 205)), PorterDuff.Mode.SrcIn);
                nativeSlider.Thumb.SetColorFilter((new Android.Graphics.Color(86, 160, 205)), PorterDuff.Mode.SrcIn);
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
                nativeSlider.ProgressDrawable.SetColorFilter(new PorterDuffColorFilter(Xamarin.Forms.Color.Red.ToAndroid(), PorterDuff.Mode.SrcIn));
                nativeSlider.Thumb.SetColorFilter(new PorterDuffColorFilter(Xamarin.Forms.Color.Red.ToAndroid(), PorterDuff.Mode.SrcIn));
            }
            else
            {
                nativeSlider.ProgressDrawable.SetColorFilter(new PorterDuffColorFilter(Xamarin.Forms.Color.Blue.ToAndroid(), PorterDuff.Mode.SrcIn));
                nativeSlider.Thumb.SetColorFilter(new PorterDuffColorFilter(Xamarin.Forms.Color.Blue.ToAndroid(), PorterDuff.Mode.SrcIn));
            }
        }
    }

}