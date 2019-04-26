using Eventual.EventStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class UploadSessionFileMarkedAsUsed : Event
    {
        #region Constructors

        public UploadSessionFileMarkedAsUsed(string uploadSessionId, string fileId) 
            : base(schemaVersion: 1)
        {
            this.UploadSessionId = uploadSessionId;
            this.FileId = fileId;
        }

        #endregion

        #region Properties

        public string UploadSessionId { get; private set; }

        public string FileId { get; private set; }

        #endregion
    }
}
