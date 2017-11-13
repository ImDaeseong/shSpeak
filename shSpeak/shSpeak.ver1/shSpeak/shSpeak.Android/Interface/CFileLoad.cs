using System;
using System.IO;
using Xamarin.Forms;
using shSpeak.Droid.Interface;
using shSpeak.Interface;

[assembly: Dependency(typeof(CFileLoad))]
namespace shSpeak.Droid.Interface
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
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), sFileName);
        }

    }
}