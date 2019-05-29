using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Text;

namespace CharacterPainting
{
    class Program
    {
        static char[] Charts = "@#&$%*o!;.".ToCharArray();
        static void Main(string[] args)
        {
            var imageFile = @"C:\Users\Administrator\Desktop\1.png";
            var savePath = @"C:\Users\Administrator\Pictures\1.txt";

            var result = new StringBuilder();
            using (var image = Image.Load(imageFile))
            {
                var size = image.Size();

                for (var y = 0; y < size.Height; y++)
                {
                    for (var x = 0; x < size.Width; x++)
                    {
                        var pixel = image[x, y];
                        var gray = 0.299f * pixel.R + 0.578 * pixel.G + 0.114f * pixel.B;
                        var index = (int)Math.Round(gray * (Charts.Length + 1) / 255);
                        result.Append(index >= Charts.Length ? " " : $"{Charts[index]}");
                    }
                    result.AppendLine();
                }
            }

            Console.WriteLine(result);

            using (var writer = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var buffer = Encoding.UTF8.GetBytes(result.ToString());
                writer.Write(buffer);
            }
        }
    }
}
