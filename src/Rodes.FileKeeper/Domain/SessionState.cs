using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public enum SessionState
    {
        NotStarted,
        Started,
        Completed,
        Canceled
    }
}
