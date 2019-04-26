using Rodes.FileKeeper.Contracts.Commands;
using Rodes.FileKeeper.Domain;
using Rodes.FileKeeper.Domain.Assertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Application.Converters
{
    internal class ExternalContractsConverter
    {
        public static Guid ConvertIdToGuid(string id)
        {
            AssertionHelper.AssertIsValidGuidIdentity(id, "id", "The specified id is not a valid guid. Please, specify a valid guid.");

            return new Guid(id);
        }

        public static FileUploadDescription ConvertToInternalType(string uploadSessionId, FileDescriptionData externalType)
        {
            if (externalType == null)
            {
                return null;
            }

            return new FileUploadDescription(ConvertIdToGuid(externalType.FileId), ConvertIdToGuid(uploadSessionId), externalType.FileName,
                new ContentType(externalType.ContentType), externalType.ContentLength);
        }

        public static List<FileUploadDescription> ConvertToInternalType(string uploadSessionId, IEnumerable<FileDescriptionData> externalType)
        {
            if (externalType == null)
            {
                return null;
            }

            List<FileUploadDescription> result = new List<FileUploadDescription>();
            foreach (var file in externalType)
            {
                result.Add(ConvertToInternalType(uploadSessionId, file));
            }

            return result;
        }
    }
}
