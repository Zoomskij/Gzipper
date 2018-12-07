using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;


namespace Gzipper
{
    public static class Program
    {
        //private static string directoryPath = @"D://temp/OrderPrint.pdf";

        private static string sourceFile = "D://temp/CLR.pdf";
        private static string compressedFile = "D://temp/CLR.gz";
        private static string targetFile = "D://temp/CLR_RE.pdf";
        static void Main(string[] args)
        {

            //var file = new FileInfo(directoryPath);
            // создание сжатого файла
            Compress(sourceFile, compressedFile);
            Decompress(compressedFile, targetFile);

            Console.ReadLine();
        }

        private static void Compress(string sourceFile, string compressedFile)
        {
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }

        private static void Decompress(string compressedFile, string targetFile)
        {
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(targetFile))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                    }
                }
            }
        }

        private static void CopyTo(this Stream input, Stream output)
        {
            var buffer = new byte[1024 * 1024 * 64];
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }
    }
}
