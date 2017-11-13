using System;
using NUnit.Framework;
using Xamarin.Forms;
using shSpeak.iOS.Interface;
using shSpeak.Interface;
using AVFoundation;
using Foundation;

[assembly: Dependency(typeof(CLamp))]
namespace shSpeak.iOS.Interface
{
    public class CLamp : ILamp
    {
        public void TurnOn()
        {
            var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
            if (captureDevice == null)
                return;
          
            NSError error = null;
            captureDevice.LockForConfiguration(out error);
            if (error != null)
            {
                captureDevice.UnlockForConfiguration();
                return;
            }
            else
            {
                if (captureDevice.TorchMode != AVCaptureTorchMode.On)
                {
                    captureDevice.TorchMode = AVCaptureTorchMode.On;
                }
                captureDevice.UnlockForConfiguration();
            }
        }

        public void TurnOff()
        {
            var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
            if (captureDevice == null)
                return;
          
            NSError error = null;
            captureDevice.LockForConfiguration(out error);
            if (error != null)
            {
                captureDevice.UnlockForConfiguration();
                return;
            }
            else
            {
                if (captureDevice.TorchMode != AVCaptureTorchMode.Off)
                {
                    captureDevice.TorchMode = AVCaptureTorchMode.Off;
                }
                captureDevice.UnlockForConfiguration();
            }
        }
        
    }
}