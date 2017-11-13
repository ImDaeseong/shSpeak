using System;
using System.Collections.Generic;
using Android.Graphics;
using Java.IO;
using Xamarin.Forms;
using Camera = Android.Hardware.Camera;
using shSpeak.Interface;
using shSpeak.Droid.Interface;

[assembly: Dependency(typeof(CLamp))]
namespace shSpeak.Droid.Interface
{
    public class CLamp : ILamp
    {
        private Camera camera;

        public void TurnOn()
        {
            if (camera == null)
                camera = Camera.Open();

            if (camera == null)
                return;
            
            var p = camera.GetParameters();
            var supportedFlashModes = p.SupportedFlashModes;

            if (supportedFlashModes == null)
                supportedFlashModes = new List<string>();

            var flashMode = string.Empty;

            if (supportedFlashModes.Contains(Android.Hardware.Camera.Parameters.FlashModeTorch))
                flashMode = Android.Hardware.Camera.Parameters.FlashModeTorch;

            if (!string.IsNullOrEmpty(flashMode))
            {
                p.FlashMode = flashMode;
                camera.SetParameters(p);
            }

            camera.StartPreview();

            try
            {
                camera.SetPreviewTexture(new SurfaceTexture(0));
            }
            catch { }
        }
                
        public void TurnOff()
        {
            if (camera == null)
                camera = Camera.Open();

            if (camera == null)
                return;
          
            var p = camera.GetParameters();
            var supportedFlashModes = p.SupportedFlashModes;

            if (supportedFlashModes == null)
                supportedFlashModes = new List<string>();

            var flashMode = string.Empty;

            if (supportedFlashModes.Contains(Android.Hardware.Camera.Parameters.FlashModeTorch))
                flashMode = Android.Hardware.Camera.Parameters.FlashModeOff;

            if (!string.IsNullOrEmpty(flashMode))
            {
                p.FlashMode = flashMode;
                camera.SetParameters(p);
            }
        }

    }
}