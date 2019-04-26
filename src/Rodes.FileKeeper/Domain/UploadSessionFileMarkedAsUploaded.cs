using Eventual.EventStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class UploadSessionFileMarkedAsUploaded : Event
    {
        #region Constructors

        public UploadSessionFileMarkedAsUploaded(string sessionId, string fileId)
            : base(schemaVersion: 1)
        {
            // TODO: Complete member initialization
            this.SessionId = sessionId;
            this.FileId = fileId;
        }

        #endregion

        #region Properties

        public string SessionId { get; private set; }

        public string FileId { get; private set; }

        #endregion
    }
}
