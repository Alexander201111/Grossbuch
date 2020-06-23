using System.IO;
using Grossbuch.Droid;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(AndroidDbPath))]
namespace Grossbuch.Droid
{
    public class AndroidDbPath : IPath
    {
        public AndroidDbPath() { }

        public string GetDatabasePath(string filename)
        {
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, filename);
            return path;
        }
    }
}