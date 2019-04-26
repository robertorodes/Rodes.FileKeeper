using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public class MarkFilesAsUsedCommand : Command
    {
        #region Constructors

        public MarkFilesAsUsedCommand(string correlationId, string commandId, CommandIssuer commandIssuer, IEnumerable<string> fileIds) 
            : base(correlationId, commandId, commandIssuer)
        {
            this.FileIds = fileIds;
        }

        #endregion

        #region Properties

        public IEnumerable<string> FileIds { get; private set; }

        #endregion
    }
}
