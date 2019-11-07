using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RioSuaveLib
{
    public class ImageService
    {
        public ImageService(string imagePath)
        {
            _imagePath = imagePath;
        }
        public string SaveImage(string filename, Image image)
        {
            var ext = GetImageExt(image);
            var filePath = Path.Combine("images", filename + ext);
            var fullPath = Path.Combine(_imagePath, filePath);

            image.Save(fullPath);

            return filePath;
        }

        public void DeleteImage(string imageUrl)
        {
            var path = Path.Combine(_imagePath, imageUrl);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private static string GetImageExt(Image img)
        {
            if (ImageFormat.Bmp.Equals(img.RawFormat))
                return ".bmp";
            if (ImageFormat.Jpeg.Equals(img.RawFormat))
                return ".jpg";
            if (ImageFormat.Png.Equals(img.RawFormat))
                return ".png";

            return "";
        }

        private readonly string _imagePath;
    }
}
