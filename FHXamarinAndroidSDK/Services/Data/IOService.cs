using System.IO;
using System.Text;
using Android.App;
using Android.OS;
using FHSDK.Services.Data;
using File = Java.IO.File;

namespace FHSDK.Services
{
    public class IOService : IIOService
    {
        private const string Tag = "FHSDK.IOService";

        public string ReadFile(string fullPath)
        {
            var file = new File(fullPath);
            if (!file.Exists()) return null;
            var sr = new StreamReader(file.AbsolutePath, Encoding.UTF8);
            var content = sr.ReadToEnd();
            sr.Close();
            return content;
        }

        public void WriteFile(string fullPath, string content)
        {
            var file = new File(fullPath);
            if (!file.Exists())
            {
                var parentDir = file.ParentFile;
                if (!parentDir.Exists())
                {
                    parentDir.Mkdirs();
                }
                file.CreateNewFile();
            }

            var writer = new StreamWriter(file.AbsolutePath);
            writer.Write(content);
            writer.Close();
        }

        public bool Exists(string fullPath)
        {
            var file = new File(fullPath);
            return file.Exists();
        }

        public string GetDataPersistDir()
        {
            var dataDir = Application.Context.GetExternalFilesDir(Environment.DirectoryDownloads) ??
                          new File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            return dataDir.AbsolutePath;
        }
    }
}