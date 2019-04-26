using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.AppContext
{
    public interface IApplicationContextRegistry
    {
        IApplicationContext ApplicationContext { get; set; }
    }
}
