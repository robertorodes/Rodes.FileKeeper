using Rodes.FileKeeper.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Application
{
    public interface IFileStorageCommandApplicationService
    {
        Task StartUploadSessionAsync(StartUploadSessionCommand command);

        Task UploadFilesAsync(UploadFilesCommand command);

        Task CancelUploadSessionAsync(CancelUploadSessionCommand command);

        Task EndUploadSessionAsync(EndUploadSessionCommand command);

        Task MarkFilesAsUsedAsync(MarkFilesAsUsedCommand command);

        Task DeleteFilesAsync(DeleteFilesCommand command);

        /// <summary>
        /// Cleans up failed upload sessions and unused files.
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        Task CleanupExpiredUploadSessions(CleanupStorageCommand command);
    }
}
