using Rodes.FileKeeper.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Application
{
    public class FileStorageCommandValidationFilter : FileStorageCommandFilter
    {
        #region Attributes

        #endregion

        #region Constructors

        public FileStorageCommandValidationFilter(IFileStorageCommandApplicationService applicationService) 
            : base(applicationService)
        {

        }

        #endregion

        #region Methods

        public async override Task StartUploadSessionAsync(StartUploadSessionCommand command)
        {
            await base.StartUploadSessionAsync(command);
        }

        public async override Task UploadFilesAsync(UploadFilesCommand command)
        {
            await base.UploadFilesAsync(command);
        }

        public async override Task CancelUploadSessionAsync(CancelUploadSessionCommand command)
        {
            await base.CancelUploadSessionAsync(command);
        }

        public async override Task EndUploadSessionAsync(EndUploadSessionCommand command)
        {
            await base.EndUploadSessionAsync(command);
        }

        public async override Task MarkFilesAsUsedAsync(MarkFilesAsUsedCommand command)
        {
            await base.MarkFilesAsUsedAsync(command);
        }

        public async override Task DeleteFilesAsync(DeleteFilesCommand command)
        {
            await base.DeleteFilesAsync(command);
        }

        public async override Task CleanupExpiredUploadSessions(CleanupStorageCommand command)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        #endregion
    }
}
