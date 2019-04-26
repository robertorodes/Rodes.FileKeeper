using System;
using System.Runtime.Serialization;

namespace Rodes.FileKeeper.Domain.Exceptions
{
    public class UploadSessionFileNotRegisteredException : FileStorageException
    {
        private static readonly string DefaultMessage = Resources.Messages.Error_UploadSessionFileNotRegisteredException_DefaultMessage;
        private static readonly string DefaultDetailedMessage = Resources.Messages.Error_UploadSessionFileNotRegisteredException_DefaultDetailedMessage;

        public UploadSessionFileNotRegisteredException() : base(DefaultMessage) { }

        public UploadSessionFileNotRegisteredException(Guid fileId) : base(string.Format(DefaultDetailedMessage, fileId)) { }

        public UploadSessionFileNotRegisteredException(string message) : base(message) { }

        public UploadSessionFileNotRegisteredException(string message, Exception innerException) : base(message, innerException) { }

        public UploadSessionFileNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
