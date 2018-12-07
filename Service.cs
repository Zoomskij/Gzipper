using Gzipper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Gzipper
{
    public class Service 
    {
        public Service()
        {

        }

        public void Compress(string inputFile, string outputFile)
        {
            using (FileStream sourceStream = new FileStream(inputFile, FileMode.Open))
            {
                using (FileStream targetStream = new FileStream(outputFile, FileMode.Create))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                        Console.WriteLine($"Сжатие файла {inputFile} завершено. Исходный размер: {sourceStream.Length}  сжатый размер: {targetStream.Length}.");
                    }
                }
            }
        }

        public void Decompress(string compressedFile, string targetFile)
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
    }
}
