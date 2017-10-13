using CoreGraphics;
using Foundation;
using System.IO;
using UIKit;
using Xamarin.Forms;
using shSpeak.iOS.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(ConvertImage))]
namespace shSpeak.iOS.Interface
{
    public class ConvertImage : IConvertImage
    {
        private int rows = 1;
        private int columns = 16;
        private UIImage iOSbitmap = null;

        public byte[] ImageToBinary(string sImg)
        {
            CGDataProvider dataProvider = new CGDataProvider(sImg);
            var rbitmap = CGImage.FromPNG(dataProvider, null, false, CGColorRenderingIntent.Default);
            if (rbitmap == null)
                return null;

            UIImage uiImage = UIImage.FromImage(rbitmap);
            if (uiImage == null)
                return null;

            NSData nsData = uiImage.AsPNG();
            if (nsData == null)
                return null;

            return nsData.ToArray();
        }

        public void LoadImageSource(string sImg)
        {
            CGDataProvider dataProvider = new CGDataProvider(sImg);
            var rbitmap = CGImage.FromPNG(dataProvider, null, false, CGColorRenderingIntent.Default);
            if (rbitmap != null)
                iOSbitmap = UIImage.FromImage(rbitmap);
        }

        public byte[] Crop(int index)
        {
            if (iOSbitmap == null) return null;

            var sourceWidth = iOSbitmap.Size.Width / columns;
            var sourceHeight = iOSbitmap.Size.Height / rows;

            var xPosition = (index % columns) * sourceWidth;
            var yPosition = (index / columns) * sourceHeight;

            var rect = new CGRect(xPosition, yPosition, sourceWidth, sourceHeight);
            var crop = iOSbitmap.CGImage.WithImageInRect(rect);
            iOSbitmap.Dispose();
            var modifiedImage = new UIImage(crop);

            var data = modifiedImage.AsPNG();
            return data.ToArray();
        }

        public ImageSource CropImage(int index)
        {
            if (iOSbitmap == null) return null;

            var sourceWidth = iOSbitmap.Size.Width / columns;
            var sourceHeight = iOSbitmap.Size.Height / rows;

            var xPosition = (index % columns) * sourceWidth;
            var yPosition = (index / columns) * sourceHeight;

            var rect = new CGRect(xPosition, yPosition, sourceWidth, sourceHeight);
            var crop = iOSbitmap.CGImage.WithImageInRect(rect);
            iOSbitmap.Dispose();
            var modifiedImage = new UIImage(crop);

            var data = modifiedImage.AsPNG();
            byte[] buffer = data.ToArray();

            ImageSource retSource = null;
            if (buffer != null)
                retSource = ImageSource.FromStream(() => new MemoryStream(buffer));
            return retSource;
        }

        private byte[] Crop(int x, int y, int width, int height)
        {
            if (iOSbitmap == null) return null;

            var rect = new CGRect(x, y, width, height);
            var crop = iOSbitmap.CGImage.WithImageInRect(rect);
            iOSbitmap.Dispose();
            var modifiedImage = new UIImage(crop);

            var data = modifiedImage.AsPNG();
            return data.ToArray();
        }
    }

}