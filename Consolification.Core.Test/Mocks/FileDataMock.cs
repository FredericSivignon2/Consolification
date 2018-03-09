using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class FileDataMock
    {
        [CINamedArgument("/FILE")]
        [CIFileContent]
        public byte[] FileByteArray { get; set; }

        [CINamedArgument("/FILELINES")]
        [CIFileContent]
        public string[] FileLines { get; set; }

        [CINamedArgument("/FILESTRING")]
        [CIFileContent("UTF8")]
        public string FileString { get; set; }

        [CINamedArgument("/FILECHAR")]
        [CIFileContent]
        public char[] FileCharArray { get; set; }

        [CINamedArgument("/FILESTREAM")]
        [CIFileContent]
        public FileStream FileStream { get; set; }
    }
}
