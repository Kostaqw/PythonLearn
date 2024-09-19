using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Drawing;


namespace PythonLearn.DAL.other
{
    public static class ImageProcess
    {
        public static IFormFile ConverToFormFile(byte[] srcImage)
        {
            var stream = new MemoryStream(srcImage);
            IFormFile file = new FormFile(stream, 0, srcImage.Length, "image", "avatar");
            return file;
        }

        public static byte[] GetByteArrayFromIFormFile(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
