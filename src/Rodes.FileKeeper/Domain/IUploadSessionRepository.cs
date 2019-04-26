using System;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public interface IUploadSessionRepository
    {
        Task Add(UploadSession session);

        Task<UploadSession> GetByIdAsync(Guid sessionId);

        Task<UploadSession> GetByFileIdAsync(Guid fileId);

        Task SaveChangesAsync();
    }
}
