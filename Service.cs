using Gzipper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;

namespace Gzipper
{
    public class Service 
    {
        private object locker = new object();
        private int _size = 1024 * 1024;
        public Service()
        {

        }

        public void Compress(string inputFile, string outputFile)
        {
            using (FileStream sourceStream = new FileStream(inputFile, FileMode.Open))
            {
                using (FileStream targetStream = new FileStream(outputFile, FileMode.Create))
                {
                   
                    while (sourceStream.Position < sourceStream.Length) //Пока не прочли весь исходный файл
                    {
                        var block = new Block
                        {
                            Size = _size,
                            Data = new byte[_size]
                        };

                        sourceStream.Read(block.Data, 0, block.Size);
                        block = DataCompress(block);

                        targetStream.Write(block.CompressedData, 0, block.CompressedData.Length);

                        //Thread _reader = new Thread(() =>
                        //{
                        //    block = DataCompress(block);
                        //    targetStream.Write(block.CompressedData, 0, block.CompressedData.Length);
                        //});
                        //_reader.Start();

                        
                    }

                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                        Console.WriteLine($"Сжатие файла {inputFile} завершено. Исходный размер: {sourceStream.Length}  сжатый размер: {targetStream.Length}.");
                    }
                }
            }
        }

        private Block DataCompress(Block block)
        {
            lock (locker)
            {
                using (var data = new MemoryStream())
                {
                    using (var compressionStream = new GZipStream(data, CompressionMode.Compress))
                    {
                        compressionStream.Write(block.Data, 0, block.Size);
                        block.CompressedData = data.ToArray();
                    }
                    return block;
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
