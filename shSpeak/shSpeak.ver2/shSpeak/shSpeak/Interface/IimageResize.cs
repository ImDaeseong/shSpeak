using System;

namespace shSpeak.Interface
{
    public interface IimageResize
    {
        byte[] ResizeImage(byte[] ImageData, float fWidth, float fHeight);

        void SaveImage(string sFileName, byte[] ImageData);

        long GetFileSize(string sFileName);
    }
}
