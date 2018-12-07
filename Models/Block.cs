using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gzipper.Models
{
    public class Block
    {
        public int Size { get; set; }
        public byte[] Data { get; set; }
        public byte[] CompressedData { get; set; }
    }
}
