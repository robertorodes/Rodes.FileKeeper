using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class File : EventSourcedAggregateRoot
    {
        #region Attributes

        private OwnerId ownerId;
        //TODO: Assess whether 'containerName' should be left as a persistence layer responsibility (and thus be removed from here)
        private string containerName;
        private string fileName;
        private ContentType contentType;
        private Stream content;
        private Uri originUri;
        private Uri accessUri;
        private bool isPublic;

        #endregion

        #region Constructors

        private File(Guid fileId, OwnerId ownerId, string fileName,
            ContentType contentType, Stream content, bool isPublic)
        {
            this.Id = fileId;
            this.OwnerId = ownerId;
            this.FileName = fileName;
            this.ContentType = contentType;
            this.Content = content;
            this.IsPublic = isPublic;
            this.ContainerName = null;
            this.OriginUri = null;
            this.AccessUri = null;

            this.AddChange(new FileUploaded(this.Id.ToString(), this.OwnerId, this.FileName,
                this.ContentType.ToString(), this.IsPublic));
        }

        #endregion

        #region Methods

        public static File Upload(Guid fileId, OwnerId ownerId, string fileName,
            ContentType contentType, Stream content, bool isPublic)
        {
            return new File(fileId, ownerId, fileName, contentType, content, isPublic);
        }

        public void SetOriginUri(Uri originUri)
        {
            this.OriginUri = originUri;
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
                //TODO: Insert validation code here
                this.ownerId = value;
            }
        }

        public string ContainerName
        {
            get
            {
                return this.containerName;
            }
            private set
            {
                //TODO: Insert validation code here
                this.containerName = value;
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
                //TODO: Insert validation code here
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
                //TODO: Insert validation code here
                this.contentType = value;
            }
        }

        public Stream Content
        {
            get
            {
                return this.content;
            }
            private set
            {
                //TODO: Insert validation code here
                this.content = value;
            }
        }

        public Uri OriginUri
        {
            get
            {
                return this.originUri;
            }
            private set
            {
                //TODO: Insert validation code here
                this.originUri = value;
            }
        }

        public Uri AccessUri
        {
            get
            {
                return this.accessUri;
            }
            private set
            {
                //TODO: Insert validation code here
                this.accessUri = value;
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
                //TODO: Insert validation code here
                this.isPublic = value;
            }
        }

        #endregion
    }
}
