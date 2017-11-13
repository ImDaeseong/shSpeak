using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using shSpeak.Renderers;
using shSpeak.iOS.Renderers;

[assembly: ExportRenderer(typeof(TextButton), typeof(TextButtonRenderer))]
namespace shSpeak.iOS.Renderers
{
    public class TextButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                Control.ContentEdgeInsets = new UIEdgeInsets(0, 10, 0, 10);
                Control.Layer.CornerRadius = 0f;
            }
        }
    }
}