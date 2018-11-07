﻿using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;

namespace Xamarin.Essentials
{
    public partial class FileSystem
    {
        static string PlatformCacheDirectory
            => Platform.AppContext.CacheDir.AbsolutePath;

        static string PlatformAppDataDirectory
            => Platform.AppContext.FilesDir.AbsolutePath;

        static Task<Stream> PlatformOpenAppPackageFileAsync(string filename)
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            filename = filename.Replace('\\', Path.DirectorySeparatorChar);
            try
            {
                return Task.FromResult(Platform.AppContext.Assets.Open(filename));
            }
            catch (Java.IO.FileNotFoundException ex)
            {
                throw new FileNotFoundException(ex.Message, filename, ex);
            }
        }

        internal static string GetAbsolutePath(this global::Android.Net.Uri uri)
        {
            using (var file = new Java.IO.File(uri.ToString()))
            {
                return file.AbsolutePath;
            }
        }
    }
}
