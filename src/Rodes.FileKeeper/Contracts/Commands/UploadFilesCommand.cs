using Rodes.FileKeeper.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public class UploadFilesCommand : Command
    {
        #region Constructors

        public UploadFilesCommand(string correlationId, string commandId, CommandIssuer commandIssuer, string uploadSessionId, IEnumerable<FileData> files) 
            : base(correlationId, commandId, commandIssuer)
        {
            this.UploadSessionId = uploadSessionId;
            this.Files = files;
        }

        #endregion

        #region Properties

        public string UploadSessionId { get; private set; }

        public IEnumerable<FileData> Files { get; private set; }

        #endregion
    }
}
