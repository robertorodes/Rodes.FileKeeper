using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.AppContext
{
    public class ApplicationContext : IApplicationContext
    {
        #region Properties

        public string CorrelationId { get; set; }

        public string CausationId { get; set; }

        public Dictionary<string, string> CustomMetadata { get; set; }

        #endregion
    }
}
