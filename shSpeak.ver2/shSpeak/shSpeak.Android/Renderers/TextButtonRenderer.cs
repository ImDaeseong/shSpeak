using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using shSpeak.Renderers;
using shSpeak.Droid.Renderers;

[assembly: ExportRenderer(typeof(TextButton), typeof(TextButtonRenderer))]
namespace shSpeak.Droid.Renderers
{
    public class TextButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {

            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Gravity = GravityFlags.Center;
                Control.SetPadding(0, 15, 0, 0);
                Control.SetBackgroundResource(Resource.Drawable.TextButtonEditText);
            }
        }
    }
}