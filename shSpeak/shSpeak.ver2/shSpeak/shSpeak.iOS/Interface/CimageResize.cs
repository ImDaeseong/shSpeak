using System;
using UIKit;
using System.IO;
using CoreGraphics;
using Xamarin.Forms;
using shSpeak.iOS.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CimageResize))]
namespace shSpeak.iOS.Interface
{
    public class CimageResize : IimageResize
    {
        public void SaveImage(string sFileName, byte[] ImageData)
        {
            string filePath = GetFilePath(sFileName);
            File.WriteAllBytes(filePath, ImageData);
        }

        private string GetFilePath(string sFileName)
        {
            var sPathPersonal = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var sFilePath = Path.Combine(sPathPersonal, sFileName);
            return sFilePath;
        }

        public byte[] ResizeImage(byte[] ImageData, float fWidth, float fHeight)
        {
            UIImage originalImage = ImageFromByteArray(ImageData);
            UIImageOrientation orientation = originalImage.Orientation;

            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)fWidth, (int)fHeight, 8,
                                                 (int)(4 * fWidth), CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {

                CGRect imageRect = new CGRect(0, 0, fWidth, fHeight);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
        }

        private static UIImage ImageFromByteArray(byte[] ImageData)
        {
            if (ImageData == null)
            {
                return null;
            }

            UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(ImageData));
            }
            catch 
            {               
                return null;
            }
            return image;
        }

        public long GetFileSize(string sFileName)
        {
            var info = new FileInfo(sFileName);
            return info.Length;
        }

    }
}