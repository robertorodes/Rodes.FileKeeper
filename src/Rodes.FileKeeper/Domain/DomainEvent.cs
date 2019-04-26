using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public abstract class DomainEvent
    {
        #region Properties

        public int SchemaVersion { get; protected set; }

        public DateTime OccurrenceDate { get; protected set; }

        #endregion
    }
}
