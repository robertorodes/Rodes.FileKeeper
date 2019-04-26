using System;
using System.Runtime.Serialization;

namespace Rodes.FileKeeper.Domain.Exceptions
{
    public class UploadSessionMissingFilesException : FileStorageException
    {
        public UploadSessionMissingFilesException() : base() { }

        public UploadSessionMissingFilesException(string message) : base(message) { }

        public UploadSessionMissingFilesException(string message, Exception innerException) : base(message, innerException) { }

        public UploadSessionMissingFilesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
