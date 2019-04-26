using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public class FileData
    {
        #region Constructors

        public FileData(string fileId, Stream content)
        {
            this.FileId = fileId;
            this.Content = content;
        }

        #endregion

        #region Properties

        public string FileId { get; private set; }

        public Stream Content { get; private set; }

        #endregion
    }
}
