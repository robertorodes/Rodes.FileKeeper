using System;
using System.Runtime.Serialization;

namespace Rodes.FileKeeper.Domain.Exceptions
{
    public class FileStorageException : Exception
    {
        public FileStorageException() : base() { }

        public FileStorageException(string message) : base(message) { }

        public FileStorageException(string message, Exception innerException) : base(message, innerException) { }

        public FileStorageException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
