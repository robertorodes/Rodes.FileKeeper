using Rodes.FileKeeper.Domain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Rodes.FileKeeper.Infrastructure.Exceptions
{
    public class FileStorageConfigurationException : FileStorageException
    {
        public FileStorageConfigurationException() : base() { }

        public FileStorageConfigurationException(string message) : base(message) { }

        public FileStorageConfigurationException(string message, Exception innerException) : base(message, innerException) { }

        public FileStorageConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
