using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public abstract class Command : ICommand
    {
        #region Constructors

        public Command(string correlationId, string commandId, CommandIssuer commandIssuer)
        {
            this.CorrelationId = correlationId;
            this.CommandId = commandId;
            this.CommandIssuer = commandIssuer;
        }

        #endregion

        #region Properties

        public string CorrelationId { get; }

        public string CommandId { get; }

        public CommandIssuer CommandIssuer { get; }

        #endregion
    }
}
