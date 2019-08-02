using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace WilfredGreeter
{
    public class ImageLoader
    {
        private readonly string _path;
        public ImageLoader(string imagePath)
        {
            _path = File.Exists(imagePath) ? imagePath : @"Resources\Wilfred.png";
        }

        public BitmapImage Image
        {
            get
            {
                BitmapImage bitmap = new BitmapImage();  
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bitmap.UriSource = new Uri(_path, UriKind.RelativeOrAbsolute);  
                bitmap.EndInit();
                return bitmap;
            }
        }
    }
}