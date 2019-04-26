using Rodes.FileKeeper.Contracts.Commands;
using Rodes.FileKeeper.Domain.Assertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Application
{
    public abstract class FileStorageCommandFilter : IFileStorageCommandApplicationService
    {
        #region Attributes

        private IFileStorageCommandApplicationService applicationService;

        #endregion

        #region Constructors

        public FileStorageCommandFilter(IFileStorageCommandApplicationService applicationService)
        {
            this.ApplicationService = applicationService;
        }

        #endregion

        #region Methods
        
        public async virtual Task StartUploadSessionAsync(StartUploadSessionCommand command)
        {
            await this.ApplicationService.StartUploadSessionAsync(command);
        }

        public async virtual Task UploadFilesAsync(UploadFilesCommand command)
        {
            await this.ApplicationService.UploadFilesAsync(command);
        }

        public async virtual Task CancelUploadSessionAsync(CancelUploadSessionCommand command)
        {
            await this.ApplicationService.CancelUploadSessionAsync(command);
        }

        public async virtual Task EndUploadSessionAsync(EndUploadSessionCommand command)
        {
            await this.ApplicationService.EndUploadSessionAsync(command);
        }

        public async virtual Task MarkFilesAsUsedAsync(MarkFilesAsUsedCommand command)
        {
            await this.ApplicationService.MarkFilesAsUsedAsync(command);
        }

        public async virtual Task DeleteFilesAsync(DeleteFilesCommand command)
        {
            await this.ApplicationService.DeleteFilesAsync(command);
        }

        public async virtual Task CleanupExpiredUploadSessions(CleanupStorageCommand command)
        {
            await this.ApplicationService.CleanupExpiredUploadSessions(command);
        }

        #endregion

        #region Properties            

        private IFileStorageCommandApplicationService ApplicationService
        {
            get
            {
                return this.applicationService;
            }
            set
            {
                AssertionHelper.AssertNotNull(value, "ApplicationService");
                this.applicationService = value;
            }
        }

        #endregion
    }
}
