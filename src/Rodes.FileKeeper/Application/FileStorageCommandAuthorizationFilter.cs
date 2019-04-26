using Rodes.FileKeeper.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Application
{
    public class FileStorageCommandAuthorizationFilter : FileStorageCommandFilter
    {
        #region Attributes

        #endregion

        #region Constructors

        public FileStorageCommandAuthorizationFilter(IFileStorageCommandApplicationService applicationService) : base(applicationService)
        {

        }

        #endregion

        #region Methods

        public async Task StartUploadSessionAsync(StartUploadSessionCommand command)
        {
            await base.StartUploadSessionAsync(command);
        }

        public async Task UploadFilesAsync(UploadFilesCommand command)
        {
            await base.UploadFilesAsync(command);
        }

        public async Task CancelUploadSessionAsync(CancelUploadSessionCommand command)
        {
            await base.CancelUploadSessionAsync(command);
        }

        public async Task EndUploadSessionAsync(EndUploadSessionCommand command)
        {
            await base.EndUploadSessionAsync(command);
        }

        public async Task MarkFilesAsUsedAsync(MarkFilesAsUsedCommand command)
        {
            await base.MarkFilesAsUsedAsync(command);
        }

        public async Task DeleteFilesAsync(DeleteFilesCommand command)
        {
            await base.DeleteFilesAsync(command);
        }

        public async Task CleanupExpiredUploadSessions(CleanupStorageCommand command)
        {
            await base.CleanupExpiredUploadSessions(command);
        }

        #endregion

        #region Properties

        #endregion
    }
}
