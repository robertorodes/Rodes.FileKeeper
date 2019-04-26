using System;
using System.Runtime.Serialization;

namespace Rodes.FileKeeper.Domain.Exceptions
{
    public class UploadSessionNotCompletedException : FileStorageException
    {
        private static readonly string DefaultMessage = "The upload process is not completed.";

        public UploadSessionNotCompletedException() : base(DefaultMessage) { }

        public UploadSessionNotCompletedException(string message) : base(message) { }

        public UploadSessionNotCompletedException(string message, Exception innerException) : base(message, innerException) { }

        public UploadSessionNotCompletedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
