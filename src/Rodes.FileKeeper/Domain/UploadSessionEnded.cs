using Eventual.EventStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class UploadSessionEnded : Event
    {
        #region Constructors

        public UploadSessionEnded(string uploadSessionId) : base(schemaVersion: 1)
        {
            this.UploadSessionId = uploadSessionId;
        }

        #endregion

        #region Properties

        public string UploadSessionId { get; private set; }

        #endregion
    }
}
