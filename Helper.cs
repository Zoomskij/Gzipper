using Gzipper.Models;
using System;
using System.IO;

namespace Gzipper
{
    static class Helper
    {
        public static void CopyTo(this Stream input, Stream output)
        {
            var buffer = new byte[1024 * 1024 * 64];
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        public static Parameters FetchParams(string[] args)
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
