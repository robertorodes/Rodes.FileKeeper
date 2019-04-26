using System;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public interface IFileInformationIndexRepository
    {
        void Add(FileInformationIndex fileMetadata);

        Task<FileInformationIndex> GetByIdAsync(Guid fileId);

        Task SaveChangesAsync();
    }
}
