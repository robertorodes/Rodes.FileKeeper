using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public class FileDescriptionData
    {
        #region Constructors

        public FileDescriptionData(string fileId, string fileName, string contentType, int contentLength)
        {
            this.FileId = fileId;
            this.FileName = fileName;
            this.ContentType = contentType;
            this.ContentLength = contentLength;
        }

        #endregion

        #region Properties

        public string FileId { get; private set; }

        public string FileName { get; private set; }

        public string ContentType { get; private set; }

        public int ContentLength { get; private set; }

        #endregion
    }
}
