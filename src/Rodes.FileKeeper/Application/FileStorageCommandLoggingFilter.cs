using Rodes.FileKeeper.Contracts.Commands;
using Rodes.FileKeeper.Domain.Assertions;
using Eventual.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Application
{
    public class FileStorageCommandLoggingFilter : FileStorageCommandFilter
    {
        #region Attributes

        private ILogger logger;

        #endregion

        #region Constructors

        public FileStorageCommandLoggingFilter(IFileStorageCommandApplicationService applicationService, ILogger logger)
            : base(applicationService)
        {
            this.Logger = logger;
        }

        #endregion

        #region Methods

        public async override Task StartUploadSessionAsync(StartUploadSessionCommand command)
        {
            this.Logger.LogInformation("[Start] {0} received.", command.GetType().Name);

            if (command != null)
            {
                this.Logger.LogInformation("[Start] Processing {0} with id = '{1}' and correlationId = '{2}'. Starting upload session with id = '{3}'.",
                    command.GetType().Name, command.CommandId, command.CorrelationId, command.SessionId);
            }

            await base.StartUploadSessionAsync(command);

            if (command != null)
            {
                this.Logger.LogInformation("[Finish] Processing {0} with id = '{1}' and correlationId = '{2}'. Starting upload session with id = {3}'.",
                    command.GetType().Name, command.CommandId, command.CorrelationId, command.SessionId);
            }

            this.Logger.LogInformation("[Finish] {0} received.", command.GetType().Name);
        }

        public async override Task UploadFilesAsync(UploadFilesCommand command)
        {
            this.Logger.LogInformation("[Start] {0} received.", command.GetType().Name);

            if (command != null)
            {
                this.Logger.LogInformation("[Start] Processing {0} with id = '{1}' and correlationId = '{2}'. Uploading files.",
                    command.GetType().Name, command.CommandId, command.CorrelationId);
            }

            await base.UploadFilesAsync(command);

            if (command != null)
            {
                this.Logger.LogInformation("[Finish] Processing {0} with id = '{1}' and correlationId = '{2}'. Uploading files.",
                    command.GetType().Name, command.CommandId, command.CorrelationId);
            }

            this.Logger.LogInformation("[Finish] {0} received.", command.GetType().Name);
        }

        public async override Task CancelUploadSessionAsync(CancelUploadSessionCommand command)
        {
            await base.CancelUploadSessionAsync(command);
        }

        public async override Task EndUploadSessionAsync(EndUploadSessionCommand command)
        {
            this.Logger.LogInformation("[Start] {0} received.", command.GetType().Name);

            if (command != null)
            {
                this.Logger.LogInformation("[Start] Processing {0} with id = '{1}' and correlationId = '{2}'. Ending upload session.",
                    command.GetType().Name, command.CommandId, command.CorrelationId);
            }

            await base.EndUploadSessionAsync(command);

            if (command != null)
            {
                this.Logger.LogInformation("[Finish] Processing {0} with id = '{1}' and correlationId = '{2}'. Ending upload session.",
                    command.GetType().Name, command.CommandId, command.CorrelationId);
            }

            this.Logger.LogInformation("[Finish] {0} received.", command.GetType().Name);
        }

        public async override Task MarkFilesAsUsedAsync(MarkFilesAsUsedCommand command)
        {
            this.Logger.LogInformation("[Start] {0} received.", command.GetType().Name);

            if (command != null)
            {
                this.Logger.LogInformation("[Start] Processing {0} with id = '{1}' and correlationId = '{2}'. Marking files in the upload session as in use.",
                    command.GetType().Name, command.CommandId, command.CorrelationId);
            }

            await base.MarkFilesAsUsedAsync(command);

            if (command != null)
            {
                this.Logger.LogInformation("[Finish] Processing {0} with id = '{1}' and correlationId = '{2}'. Marking files in the upload session as in use.",
                    command.GetType().Name, command.CommandId, command.CorrelationId);
            }

            this.Logger.LogInformation("[Finish] {0} received.", command.GetType().Name);
        }

        public async override Task DeleteFilesAsync(DeleteFilesCommand command)
        {
            await base.DeleteFilesAsync(command);
        }

        public async override Task CleanupExpiredUploadSessions(CleanupStorageCommand command)
        {
            await base.CleanupExpiredUploadSessions(command);
        }

        #endregion

        #region Properties

        public ILogger Logger
        {
            get
            {
                return this.logger;
            }
            private set
            {
                AssertionHelper.AssertNotNull(value, "Logger");
                this.logger = value;
            }
        }

        #endregion
    }
}
