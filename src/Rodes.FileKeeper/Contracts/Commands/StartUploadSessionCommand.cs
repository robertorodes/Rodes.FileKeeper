using Rodes.FileKeeper.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Contracts.Commands
{
    public class StartUploadSessionCommand : Command
    {
        #region Constructors

        public StartUploadSessionCommand(string correlationId, string commandId, CommandIssuer commandIssuer,
            string sessionId, OwnerIdData ownerId, IEnumerable<FileDescriptionData> fileDescriptions, bool uploadAsPublic) 
            : base(correlationId, commandId, commandIssuer)
        {
            this.SessionId = sessionId;
            this.OwnerId = ownerId;
            this.FileDescriptions = fileDescriptions;
            this.UploadAsPublic = uploadAsPublic;
        }

        #endregion

        #region Properties

        public string SessionId { get; private set; }

        public OwnerIdData OwnerId { get; private set; }

        public IEnumerable<FileDescriptionData> FileDescriptions { get; private set; }

        public bool UploadAsPublic { get; private set; }

        #endregion
    }
}
