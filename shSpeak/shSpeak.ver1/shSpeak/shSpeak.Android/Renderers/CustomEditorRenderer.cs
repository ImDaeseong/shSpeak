using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using shSpeak.Renderers;
using shSpeak.Droid.Renderers;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace shSpeak.Droid.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            if (Control != null)
            {
                Control.Background = Resources.GetDrawable(Resource.Drawable.EditorBorder);
                Control.SetHint(Resource.String.description);
                InputFilterLengthFilter filter = new InputFilterLengthFilter(255);
                Control.SetFilters(new IInputFilter[] { filter });
            }
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
        }
    }
}