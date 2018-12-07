using Gzipper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;


namespace Gzipper
{
    public class Program
    {
        //private static string directoryPath = @"D://temp/OrderPrint.pdf";

        private static string sourceFile = "D://temp/CLR.pdf";
        private static string compressedFile = "D://temp/CLR.gz";
        private static string targetFile = "D://temp/CLR_RE.pdf";
        static void Main(string[] args)
        {
            //var parameters = Service.FetchParams(args);
            var parameters = new Parameters
            {
                Input = "D://temp/CLR.pdf",
                Output = "D://temp/CLR.gz"
            };

            var service = new Service();
            parameters.Mode = Mode.Compress;
            switch (parameters.Mode)
            {
                case Mode.Compress:
                    service.Compress(parameters.Input, parameters.Output);
                    break;
                case Mode.Decompress:
                    service.Decompress(parameters.Input, parameters.Output);
                    break;
                default:
                    break;
            }
            Console.ReadLine();

        } 
    }
}
