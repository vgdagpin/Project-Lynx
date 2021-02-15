using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace Lynx.MobileApp.Common.Constants
{
    public abstract class SQLiteConstants
    {
        public static string FilePath => Path.Combine(FileSystem.AppDataDirectory, "LynxDb_SQLite.db3");
    }
}
