using System;
using System.Runtime.Serialization;

namespace Rodes.FileKeeper.Domain.Exceptions
{
    public class UploadSessionNotStartedException : FileStorageException
    {
        private static readonly string DefaultMessage = "No upload session has been started. Please, make sure that the upload session has been previously started and is not finished.";

        public UploadSessionNotStartedException() : base(DefaultMessage) { }

        public UploadSessionNotStartedException(string message) : base(message) { }

        public UploadSessionNotStartedException(string message, Exception innerException) : base(message, innerException) { }

        public UploadSessionNotStartedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
