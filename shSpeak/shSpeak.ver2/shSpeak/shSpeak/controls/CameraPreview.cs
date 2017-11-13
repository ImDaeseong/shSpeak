using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace shSpeak.controls
{
    public class CameraPreview : View
    {
        public static readonly BindableProperty CameraProperty =
            BindableProperty.Create(
                propertyName: "Camera",
                returnType: typeof(CameraOptions),
                declaringType: typeof(CameraPreview),
                defaultValue: CameraOptions.Rear);

        public CameraOptions Camera
        {
            get { return (CameraOptions)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        public Func<Task<ImageSource>> TakePhotoAsync { set; get; }

        public Func<CameraOptions, bool> SwitchCamera { set; get; }
    }

    public enum CameraOptions
    {
        Rear,
        Front
    }

}
