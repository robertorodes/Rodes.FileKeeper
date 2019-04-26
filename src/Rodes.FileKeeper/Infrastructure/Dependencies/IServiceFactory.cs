using Eventual.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.Dependencies
{
    public interface IServiceFactory
    {
        ILogger Logger { get; }
    }
}
