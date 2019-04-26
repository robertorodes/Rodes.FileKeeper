using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public class CleanupStorageCommand : Command
    {
        #region Constructors

        public CleanupStorageCommand(string correlationId, string commandId, CommandIssuer commandIssuer) 
            : base(correlationId, commandId, commandIssuer)
        {
        }

        #endregion

        #region Properties

        #endregion
    }
}
