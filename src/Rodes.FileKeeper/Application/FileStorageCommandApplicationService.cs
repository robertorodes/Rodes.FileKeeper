using AutoMapper;
using Eventual.EventStore.Core;
using Rodes.FileKeeper.Application.Converters;
using Rodes.FileKeeper.Contracts.Commands;
using Rodes.FileKeeper.Domain;
using Rodes.FileKeeper.Domain.Assertions;
using Rodes.FileKeeper.Domain.Exceptions;
using Rodes.FileKeeper.Infrastructure.AppContext;
using Eventual.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Application
{
    public class FileStorageCommandApplicationService : IFileStorageCommandApplicationService
    {
        #region Attributes

        private IUnitOfWork unitOfWork;
        private IUploadSessionRepository uploadSessionRepository;
        private IFileRepository fileRepository;
        private IFileInformationIndexRepository fileInformationIndexRepository;
        private ILogger logger;

        #endregion

        #region Constructors

        public FileStorageCommandApplicationService(IUnitOfWork unitOfWork, IUploadSessionRepository uploadSessionRepository,
            IFileRepository fileRepository, IFileInformationIndexRepository fileInformationIndexRepository, ILogger logger)
        {
            this.UnitOfWork = unitOfWork;
            this.UploadSessionRepository = uploadSessionRepository;
            this.FileRepository = fileRepository;
            this.FileInformationIndexRepository = fileInformationIndexRepository;
            this.Logger = logger;
        }

        #endregion

        #region Methods

        public async Task StartUploadSessionAsync(StartUploadSessionCommand command)
        {
            // TODO: Check that id's specified in the command are valid and unique (including file id's)
            Guid sessionId = new Guid(command.SessionId);

            // Start session
            UploadSession session = UploadSession.Start(sessionId,
                new OwnerId(command.OwnerId.BusinessId, command.OwnerId.UserId),
                ExternalContractsConverter.ConvertToInternalType(command.SessionId, command.FileDescriptions),
                command.UploadAsPublic);

            // Set application context
            SetApplicationContext(command);

            // Save changes
            await this.UploadSessionRepository.Add(session);
            await this.UnitOfWork.CommitChangesAsync();
        }

        public async Task UploadFilesAsync(UploadFilesCommand command)
        {
            // Check file metadata against upload session information
            Guid sessionId = new Guid(command.UploadSessionId);
            UploadSession session = await this.UploadSessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                //TODO: Throw exception: the specified upload session does not exist.
                throw new UploadSessionNotStartedException("No upload session has been started. Please, make sure to first start the upload session before uploading files.");
            }

            if (!session.IsStarted)
            {
                throw new UploadSessionNotStartedException("The specified upload session is not in the started state so no files can be uploaded. Please, start a new session for uploading.");
            }

            // Check that id's specified in the command are valid
            if (!session.AreFilesRegistered(command.Files.Select(f => new Guid(f.FileId)).AsEnumerable()))
            {
                throw new UploadSessionFileNotRegisteredException("Some of the files specified were not previously registered for upload. Please, make sure that all files are first registered for upload at session start.");
            }

            // Upload files (first store file content, second update session information)
            foreach (var file in command.Files)
            {
                Guid fileId = new Guid(file.FileId);
                FileUploadDescription uploadDescription = session.GetFileUploadDescription(fileId);

                // Upload file
                ResourceUri sourceStoreFileUri = await this.UploadFileWithoutCommitingDomainChangesAsync(fileId, session.OwnerId, uploadDescription.FileName,
                    uploadDescription.ContentType, uploadDescription.ContentLength, file.Content, session.UploadAsPublic);

                // Update session (mark file as uploaded and assign access uri)
                ResourceUri fileAccessUri = RoutingService.BuildUriForFile(uploadDescription, sourceStoreFileUri, session.UploadAsPublic);
                session.MarkFileAsUploaded(fileId);
                session.AssignUriToFileDescription(fileId, fileAccessUri);
            }

            // Save changes
            await this.unitOfWork.CommitChangesAsync();
        }

        private async Task<ResourceUri> UploadFileWithoutCommitingDomainChangesAsync(Guid fileId, OwnerId ownerId,
            string fileName, ContentType contentType, int contentLength, Stream content, bool uploadAsPublic)
        {
            this.Logger.LogInformation("[Start] Uploading file '{0}' with id '{1}'.",
                fileName, fileId);

            // Create file
            var newFile = Rodes.FileKeeper.Domain.File.Upload(fileId, ownerId, fileName, contentType, content, uploadAsPublic);

            // Upload file to binary storage
            ResourceUri assignedUri = await this.FileRepository.SaveAsync(newFile);

            this.Logger.LogInformation("[Finish] Uploading file '{0}' with id '{1}'.",
                fileName, fileId);

            return assignedUri;
        }

        public async Task CancelUploadSessionAsync(CancelUploadSessionCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task EndUploadSessionAsync(EndUploadSessionCommand command)
        {
            // Load session
            Guid sessionId = new Guid(command.UploadSessionId);
            UploadSession session = await this.UploadSessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                //TODO: Throw exception: the specified upload session does not exist.
                throw new UploadSessionNotStartedException(string.Format("No upload session with the specified id ('{0}') has been previously started.", command.UploadSessionId));
            }

            //TODO: Add the owner identifier to the command and ensure that it corresponds to the same person that started the upload session (authorization)

            // End session
            session.End();

            // Save changes
            await this.UploadSessionRepository.SaveChangesAsync();
        }

        public async Task MarkFilesAsUsedAsync(MarkFilesAsUsedCommand command)
        {
            // VALIDATION
            AssertionHelper.AssertNotNull(command, "command");
            //TODO: Validate that file id's specified are valid guid's.
            if (command.FileIds == null || command.FileIds.Count() < 1)
            {
                //TODO: Throw exception
            }

            // Load session based on the identifier of the first file.
            UploadSession session = await this.UploadSessionRepository.GetByFileIdAsync(new Guid(command.FileIds.ElementAt(0)));
            if (session == null)
            {
                //TODO: Localize message
                throw new UploadSessionNotStartedException(string.Format("No upload session containing some of the specified files has been previously started. Please, make sure that the file identifiers have been properly uploaded correctly."));
            }

            // Mark file description in upload session as used
            session.MarkFilesAsUsed(Mapper.Map<IEnumerable<Guid>>(command.FileIds));

            // Index file information
            foreach (var id in command.FileIds)
            {
                FileUploadDescription fileDescription = session.GetFileUploadDescription(new Guid(id));
                FileInformationIndex index = FileInformationIndex.Create(fileDescription.Id, session.OwnerId, fileDescription.FileName, fileDescription.ContentType,
                    fileDescription.ContentLength, fileDescription.AssignedUri, session.UploadAsPublic);

                // Save index
                this.FileInformationIndexRepository.Add(index);
            }

            // Commit changes
            await this.UnitOfWork.CommitChangesAsync();
        }

        public Task DeleteFilesAsync(DeleteFilesCommand command)
        {
            throw new NotImplementedException();
        }

        public Task CleanupExpiredUploadSessions(CleanupStorageCommand command)
        {
            throw new NotImplementedException();
        }

        private void SetApplicationContext(ICommand command)
        {
            IApplicationContext appContext = new ApplicationContext()
            {
                CorrelationId = command.CorrelationId.ToString(),
                CausationId = command.CommandId.ToString(),
                CustomMetadata = new Dictionary<string, string>()
                    {
                        { "UserId", Thread.CurrentPrincipal?.Identity?.Name },
                        { "CommandIssuer_BusinessId", command.CommandIssuer.BusinessId },
                        { "CommandIssuer_UserId", command.CommandIssuer.UserId}
                    }
            };

            if (this.UploadSessionRepository is IApplicationContextRegistry)
            {
                ((IApplicationContextRegistry)this.UploadSessionRepository).ApplicationContext = appContext;
            }
        }

        #endregion

        #region Properties

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this.unitOfWork;
            }
            private set
            {
                //TODO: Add validation code
                this.unitOfWork = value;
            }
        }

        public IUploadSessionRepository UploadSessionRepository
        {
            get
            {
                return this.uploadSessionRepository;
            }
            private set
            {
                //TODO: Add validation code
                this.uploadSessionRepository = value;
            }
        }

        public IFileRepository FileRepository
        {
            get
            {
                return this.fileRepository;
            }
            private set
            {
                //TODO: Add validation code
                this.fileRepository = value;
            }
        }

        public IFileInformationIndexRepository FileInformationIndexRepository
        {
            get
            {
                return this.fileInformationIndexRepository;
            }
            private set
            {
                //TODO: Add validation code
                this.fileInformationIndexRepository = value;
            }
        }

        public ILogger Logger
        {
            get
            {
                return this.logger;
            }
            private set
            {
                //TODO: Add validation code
                this.logger = value;
            }
        }

        #endregion
    }
}
