using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public class CancelUploadSessionCommand : Command
    {
        #region Constructors

        public CancelUploadSessionCommand(string correlationId, string commandId, CommandIssuer commandIssuer) 
            : base(correlationId, commandId, commandIssuer)
        {
        }

        #endregion

        #region Properties

        #endregion
    }
}
