using Eventual.EventStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class FileUploaded : Event
    {
        #region Constructors

        public FileUploaded(string fileId, OwnerId ownerId, string fileName, string contentType, bool isPublic) 
            : base(schemaVersion: 1)
        {
            this.FileId = fileId;
            this.OwnerId = ownerId;
            this.FileName = FileName;
            this.ContentType = contentType;
            this.IsPublic = isPublic;
        }

        #endregion

        #region Properties

        public string FileId { get; private set; }

        public OwnerId OwnerId { get; private set; }

        public string FileName { get; private set; }

        public string ContentType { get; private set; }

        public bool IsPublic { get; private set; }

        #endregion
    }
}
