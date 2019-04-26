using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public interface ICommand
    {
        string CorrelationId { get; }

        string CommandId { get; }

        CommandIssuer CommandIssuer { get; }
    }
}
