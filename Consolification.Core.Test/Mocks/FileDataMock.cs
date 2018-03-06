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
        [CIArgument("/FILE")]
        [CIFileContent]
        public byte[] FileByteArray { get; set; }

        [CIArgument("/FILELINES")]
        [CIFileContent]
        public string[] FileLines { get; set; }

        [CIArgument("/FILESTRING")]
        [CIFileContent("UTF8")]
        public string FileString { get; set; }

        [CIArgument("/FILECHAR")]
        [CIFileContent]
        public char[] FileCharArray { get; set; }

        [CIArgument("/FILESTREAM")]
        [CIFileContent]
        public FileStream FileStream { get; set; }
    }
}
