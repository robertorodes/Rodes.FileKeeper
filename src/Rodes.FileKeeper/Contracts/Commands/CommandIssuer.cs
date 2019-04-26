using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public class CommandIssuer
    {
        #region Constructors

        public CommandIssuer(string userId, string businessId)
        {
            this.UserId = userId;
            this.BusinessId = businessId;
        }

        #endregion

        #region Properties

        public string UserId { get; private set; }

        public string BusinessId { get; private set; }

        #endregion
    }
}
