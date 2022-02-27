using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using NReco.ImageGenerator;
namespace RedditTTS.Modules
{
    static class HtmlToImage
    {
        static HtmlToImageConverter htmlToImageConv = new HtmlToImageConverter();

        public static byte[] Convert(string html, int width = 1920, int height = 1080)
        {
            htmlToImageConv.Width = width;
            htmlToImageConv.Height = height;
            byte[] imgBytes = htmlToImageConv.GenerateImage(html, Singleton.imageFileFormat);
            if (Singleton.mainForm.GetTransparency()) {
                Bitmap bmp = ByteToImage(imgBytes);
                for (var x = 0; x < bmp.Width; x++)
                {
                    for (var y = 0; y < bmp.Height; y++)
                    {
                        Color pixel = bmp.GetPixel(x, y);
                        if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255)
                        {
                            bmp.SetPixel(x, y, Color.Transparent);
                        }
                    }
                }
                return ImageToByte(bmp);
            }
            else
            {
                return imgBytes;
            }
        }
        public static void TestImage(string html)
        {
            byte[] imageBytes = Convert(html, int.Parse(Config.Get("imageWidth")), int.Parse(Config.Get("imageHeight")));
            var memoryStream = new MemoryStream(imageBytes);
            Image image = Image.FromStream(memoryStream);
            Singleton.mainForm.SetBackgroundPreviewImage(image);
        }
        public static Bitmap ByteToImage(byte[] blob)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                mStream.Write(blob, 0, blob.Length);
                mStream.Seek(0, SeekOrigin.Begin);

                Bitmap bm = new Bitmap(mStream);
                return bm;
            }
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
