using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using shSpeak.Renderers;
using shSpeak.Droid.Renderers;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace shSpeak.Droid.Renderers
{
    public class CustomFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            Background = Resources.GetDrawable(Resource.Drawable.roundedBorder);
        }
    }
}