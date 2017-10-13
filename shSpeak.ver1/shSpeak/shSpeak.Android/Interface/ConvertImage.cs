using System;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;
using shSpeak.Droid.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(ConvertImage))]
namespace shSpeak.Droid.Interface
{
    public class ConvertImage : IConvertImage
    {
        private int rows = 1;
        private int columns = 16;
        private Bitmap bitmap;

        public byte[] ImageToBinary(string sImg)
        {
            var rbitmap = Xamarin.Forms.Forms.Context.Resources.GetBitmap(sImg);
            if (rbitmap == null)
                return null;

            var ms = new MemoryStream();
            rbitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
            return ms.ToArray();
        }

        public void LoadImageSource(string sImg)
        {
            bitmap = Xamarin.Forms.Forms.Context.Resources.GetBitmap(sImg);
        }

        public byte[] Crop(int index)
        {
            if (bitmap == null) return null;

            var sourceWidth = bitmap.Width / columns;
            var sourceHeight = bitmap.Height / rows;

            var xPosition = (index % columns) * sourceWidth;
            var yPosition = (index / columns) * sourceHeight;

            var croppedBitmap = Bitmap.CreateBitmap(bitmap, xPosition, yPosition, sourceWidth, sourceHeight);
            var ms = new MemoryStream();
            croppedBitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
            return ms.ToArray();
        }

        public ImageSource CropImage(int index)
        {
            if (bitmap == null) return null;

            var sourceWidth = bitmap.Width / columns;
            var sourceHeight = bitmap.Height / rows;

            var xPosition = (index % columns) * sourceWidth;
            var yPosition = (index / columns) * sourceHeight;

            var croppedBitmap = Bitmap.CreateBitmap(bitmap, xPosition, yPosition, sourceWidth, sourceHeight);
            var ms = new MemoryStream();
            croppedBitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
            byte[] buffer = ms.ToArray();

            ImageSource retSource = null;
            if (buffer != null)
                retSource = ImageSource.FromStream(() => new MemoryStream(buffer));
            return retSource;
        }

        private byte[] Crop(int x, int y, int width, int height)
        {
            if (bitmap == null) return null;

            Bitmap croped = Bitmap.CreateBitmap(bitmap, x, y, width, height);
            bitmap.Recycle();
            bitmap.Dispose();

            var ms = new MemoryStream();
            croped.Compress(Bitmap.CompressFormat.Png, 100, ms);
            return ms.ToArray();
        }

    }
}