using Eventual.EventStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class UploadSessionStarted : Event
    {
        #region Constructors

        public UploadSessionStarted(string sessionId, OwnerId ownerId, IEnumerable<FileUploadDescription> fileDescriptions, bool uploadAsPublic) 
            : base(schemaVersion: 1)
        {
            // TODO: Complete member initialization
            this.SessionId = sessionId;
            this.OwnerId = ownerId;
            this.FileDescriptions = fileDescriptions;
            this.UploadAsPublic = uploadAsPublic;
        }

        #endregion

        #region Properties

        public string SessionId { get; private set; }

        public OwnerId OwnerId { get; private set; }

        public IEnumerable<FileUploadDescription> FileDescriptions { get; private set; }

        public bool UploadAsPublic { get; private set; }

        #endregion
    }
}
