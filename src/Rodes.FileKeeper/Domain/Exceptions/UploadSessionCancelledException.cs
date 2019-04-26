using System;
using System.Runtime.Serialization;

namespace Rodes.FileKeeper.Domain.Exceptions
{
    public class UploadSessionCancelledException : FileStorageException
    {
        private static readonly string DefaultMessage = Resources.Messages.Error_UploadSessionCancelledException_DefaultMessage;

        public UploadSessionCancelledException() : base(DefaultMessage) { }

        public UploadSessionCancelledException(string message) : base(message) { }

        public UploadSessionCancelledException(string message, Exception innerException) : base(message, innerException) { }

        public UploadSessionCancelledException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
