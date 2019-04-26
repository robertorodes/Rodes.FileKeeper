using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.AppContext
{
    public interface IApplicationContext
    {
        string CorrelationId { get; set; }

        string CausationId { get; set; }

        Dictionary<string, string> CustomMetadata { get; set; }
    }
}
