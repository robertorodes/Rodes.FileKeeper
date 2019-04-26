using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class FileInformationIndex : Entity
    {
        #region Attributes

        private OwnerId ownerId;
        private string fileName;
        private ContentType contentType;
        private int contentLength;
        private ResourceUri uri;
        private bool isPublic;

        #endregion

        #region Constructors

        private FileInformationIndex() { }

        private FileInformationIndex(Guid fileId, OwnerId ownerId, string fileName,
            ContentType contentType, int contentLength, ResourceUri uri, bool isPublic)
        {
            this.Id = fileId;
            this.OwnerId = ownerId;
            this.FileName = fileName;
            this.ContentType = contentType;
            this.ContentLength = contentLength;
            this.Uri = uri;
            this.IsPublic = isPublic;
        }

        #endregion

        #region Methods

        public static FileInformationIndex Create(Guid fileId, OwnerId ownerId, string fileName,
            ContentType contentType, int contentLength, ResourceUri uri, bool isPublic)
        {
            return new FileInformationIndex(fileId, ownerId, fileName, contentType,
                contentLength, uri, isPublic);
        }

        #endregion

        #region Properties

        public OwnerId OwnerId
        {
            get
            {
                return this.ownerId;
            }
            private set
            {
                //TODO: Add validation code
                this.ownerId = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            private set
            {
                //TODO: Add validation code
                this.fileName = value;
            }
        }

        public ContentType ContentType
        {
            get
            {
                return this.contentType;
            }
            private set
            {
                //TODO: Add validation code
                this.contentType = value;
            }
        }

        public int ContentLength
        {
            get
            {
                return this.contentLength;
            }
            private set
            {
                //TODO: Add validation code
                this.contentLength = value;
            }
        }

        public ResourceUri Uri
        {
            get
            {
                return this.uri;
            }
            private set
            {
                //TODO: Add validation code
                this.uri = value;
            }
        }

        public bool IsPublic
        {
            get
            {
                return this.isPublic;
            }
            private set
            {
                //TODO: Add validation code
                this.isPublic = value;
            }
        }

        #endregion
    }
}
