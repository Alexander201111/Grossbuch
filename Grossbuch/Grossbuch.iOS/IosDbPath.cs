using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Xamarin.Forms;
using System.IO;
using Grossbuch.iOS;

using Foundation;
using UIKit;

[assembly: Dependency(typeof(IosDbPath))]
namespace Grossbuch.iOS
{
    class IosDbPath : IPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", sqliteFilename);
        }
    }
}