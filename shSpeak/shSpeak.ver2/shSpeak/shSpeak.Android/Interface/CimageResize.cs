using System;
using System.IO;
using Android.Graphics;
using Xamarin.Forms;
using shSpeak.Droid.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CimageResize))]
namespace shSpeak.Droid.Interface
{
    public  class CimageResize : IimageResize
    {
        public void SaveImage(string sFileName, byte[] ImageData)
        {
            string filePath = GetFilePath(sFileName);
            File.WriteAllBytes(filePath, ImageData);
        }

        private string GetFilePath(string sFileName)
        {
            var sPathPersonal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var sFilePath = System.IO.Path.Combine(sPathPersonal, sFileName);
            return sFilePath;
        }

        public byte[] ResizeImage(byte[] ImageData, float fWidth, float fHeight)
        {
            Bitmap originalImage = BitmapFactory.DecodeByteArray(ImageData, 0, ImageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)fWidth, (int)fHeight, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

        public long GetFileSize(string sFileName)
        {
            var info = new FileInfo(sFileName);
            return info.Length;
        }

    }
}