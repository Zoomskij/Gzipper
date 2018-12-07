using Gzipper.Models;
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
            // var parameters = FetchParams(args);
            var parameters = new Parameters();
            parameters.Input = "D://temp/CLR.pdf";
            parameters.Output = "D://temp/CLR.gz";
            parameters.Mode = Mode.Compress;

            Compress(parameters.Input, parameters.Output, parameters.Mode);
            //Decompress(compressedFile, targetFile);

            Console.ReadLine();

        }

        private static void Compress(string inputFile, string outputFile, Mode mode)
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

        private static Parameters FetchParams(string[] args)
        {
            try
            {
                if (args.Length != 3)
                    throw new Exception(Properties.Resources.ErrorCountParameters);
                var mode = Enum.Parse(typeof(Mode), args[0]);

                return new Parameters
                {
                    Mode = (Mode)mode,
                    Input = !string.IsNullOrEmpty(args[1]) ? args[1] : string.Empty,
                    Output = !string.IsNullOrEmpty(args[2]) ? args[2] : string.Empty,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception {ex.InnerException} {ex.Message}");
                throw new Exception(Properties.Resources.ErrorValidationParameters);
            }
        }
    }
}
