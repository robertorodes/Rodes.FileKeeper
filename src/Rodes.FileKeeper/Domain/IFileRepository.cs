using System;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public interface IFileRepository
    {
        Task<ResourceUri> SaveAsync(File file);

        Task<File> GetByIdAsync(Guid fileId);
    }
}
