using Rodes.FileKeeper.Domain.Assertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class FileUploadDescription : Entity
    {
        #region Attributes

        private Guid uploadSessionId;
        private string fileName;
        private ContentType contentType;
        private int contentLength;
        private DateTimeOffset? uploadDate;
        private bool isUploaded;
        private bool isInUse;
        private ResourceUri assignedUri;

        #endregion

        #region Constructors

        private FileUploadDescription() { }

        public FileUploadDescription(Guid fileId, Guid uploadSessionId, string fileName, ContentType contentType, int contentLength)
        {
            this.Id = fileId;
            this.UploadSessionId = uploadSessionId;
            this.FileName = fileName;
            this.ContentType = contentType;
            this.ContentLength = contentLength;
            this.IsUploaded = false;
            this.IsInUse = false;
            this.AssignedUri = ResourceUri.Empty;
        }

        #endregion

        #region Methods

        public void MarkAsUploaded()
        {
            this.IsUploaded = true;
            this.UploadDate = DateTimeOffset.UtcNow;
        }

        public void MarkAsUsed()
        {
            this.IsInUse = true;
        }

        public void AssignUri(ResourceUri uri)
        {
            this.AssignedUri = uri;
        }

        #endregion

        #region Properties

        public Guid UploadSessionId
        {
            get
            {
                return this.uploadSessionId;
            }
            private set
            {
                //TODO: Add validation code
                this.uploadSessionId = value;
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
                AssertionHelper.AssertNotNull(value, "FileName", Resources.Messages.Error_FileDescription_FileName_IsNullOrEmpty);
                AssertionHelper.AssertNotWhiteSpace(value, "FileName", Resources.Messages.Error_FileDescription_FileName_IsNullOrEmpty);
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
                AssertionHelper.AssertNotNull(value, "ContentType", Resources.Messages.Error_FileDescription_ContentType_IsNull);
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
                AssertionHelper.AssertIsGreaterThan(value, "ContentLength", 0, Resources.Messages.Error_FileDescription_ContentLength_IsLessThanOne);
                this.contentLength = value;
            }
        }

        public DateTimeOffset? UploadDate
        {
            get
            {
                return this.uploadDate;
            }
            private set
            {
                //TODO: Add validation code
                this.uploadDate = value;
            }
        }

        public bool IsUploaded
        {
            get
            {
                return this.isUploaded;
            }
            private set
            {
                this.isUploaded = value;
            }
        }

        public bool IsInUse
        {
            get
            {
                return this.isInUse;
            }
            private set
            {
                this.isInUse = value;
            }
        }

        public ResourceUri AssignedUri
        {
            get
            {
                return this.assignedUri;
            }
            private set
            {
                this.assignedUri = value;
            }
        }

        #endregion
    }
}
