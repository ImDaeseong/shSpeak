using System;
using System.IO;
using Foundation;
using Xamarin.Forms;
using shSpeak.iOS.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CFileLoad))]
namespace shSpeak.iOS.Interface
{
    public class CFileLoad : IFile
    {
        public string LoadText(string sFileName)
        {
            string strRead = "";
            string[] lines = System.IO.File.ReadAllLines(sFileName, System.Text.Encoding.Default);
            foreach (string value in lines)
            {
                strRead += string.Format("{0}\r\n", value);
            }
            return strRead;

            //string filePath = GetFilePath(sFileName);
            //return File.ReadAllText(sFileName, System.Text.Encoding.UTF8);
        }

        private string GetFilePath(string sFileName)
        {
            return Path.Combine(NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path, sFileName);
        }
    }
}