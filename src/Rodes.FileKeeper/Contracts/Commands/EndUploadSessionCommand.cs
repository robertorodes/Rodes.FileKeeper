using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public class EndUploadSessionCommand : Command
    {
        #region Constructors

        public EndUploadSessionCommand(string correlationId, string commandId, CommandIssuer commandIssuer, string uploadSessionId) 
            : base(correlationId, commandId, commandIssuer)
        {
            this.UploadSessionId = uploadSessionId;
        }

        #endregion

        #region Properties

        public string UploadSessionId { get; private set; }

        #endregion
    }
}
