using Rodes.FileKeeper.Domain.Assertions;
using Rodes.FileKeeper.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class UploadSession : EventSourcedAggregateRoot
    {
        #region Attributes

        private SessionState state;
        private OwnerId ownerId;
        private DateTimeOffset? startDate;
        private DateTimeOffset? endDate;
        private List<FileUploadDescription> innerFileDescriptions;
        private bool uploadAsPublic;

        #endregion

        #region Constructors

        private UploadSession() { }

        private UploadSession(Guid sessionId, OwnerId ownerId, List<FileUploadDescription> fileDescriptions, bool uploadAsPublic)
        {
            this.Id = sessionId;
            this.OwnerId = ownerId;
            this.State = SessionState.NotStarted;
            this.StartDate = null;
            this.EndDate = null;
            this.SetFileDescriptions(fileDescriptions);
            this.UploadAsPublic = uploadAsPublic;

            this.AddChange(new UploadSessionStarted(sessionId.ToString(), ownerId, fileDescriptions, uploadAsPublic));
        }

        #endregion

        #region Methods

        public static UploadSession Start(Guid sessionId, OwnerId ownerId, List<FileUploadDescription> fileDescriptions, bool uploadAsPublic)
        {
            UploadSession session = new UploadSession(sessionId, ownerId, fileDescriptions, uploadAsPublic);
            session.State = SessionState.Started;
            session.StartDate = DateTimeOffset.UtcNow;

            return session;
        }

        public void End()
        {
            if (this.IsCompleted)
            {
                return;
            }

            if (this.IsCanceled)
            {
                throw new UploadSessionCancelledException(Resources.Messages.Error_UploadSession_EndMethod_SessionIsCanceled);
            }

            if (this.IsStarted)
            {
                if (!this.AreAllFilesUploaded)
                {
                    throw new UploadSessionMissingFilesException(Resources.Messages.Error_UploadSession_EndMethod_MissingFiles);
                }
                else
                {
                    this.EndDate = DateTimeOffset.UtcNow;
                    this.State = SessionState.Completed;
                    this.AddChange(new UploadSessionEnded(this.Id.ToString()));
                }
            }
        }

        private void SetFileDescriptions(List<FileUploadDescription> fileDescriptions)
        {
            AssertionHelper.AssertNotNull(fileDescriptions, "fileDescriptions", Resources.Messages.Error_UploadSession_FileDescriptions_IsNullOrEmpty);
            AssertionHelper.AssertNotEmpty(fileDescriptions, "fileDescriptions", Resources.Messages.Error_UploadSession_FileDescriptions_IsNullOrEmpty);
            this.InnerFileDescriptions = fileDescriptions;
        }

        public void MarkFileAsUploaded(Guid fileId)
        {
            if (!this.IsStarted)
            {
                throw new UploadSessionNotStartedException(Resources.Messages.Error_UploadSession_MarkFilesAsUploadedMethod_SessionIsNotStarted);
            }

            FileUploadDescription fileDescription = this.GetFileUploadDescription(fileId);

            fileDescription.MarkAsUploaded();

            this.AddChange(new UploadSessionFileMarkedAsUploaded(this.Id.ToString(), fileDescription.Id.ToString()));
        }

        public void MarkFileAsUsed(Guid fileId)
        {
            if (!this.IsCompleted)
            {
                throw new UploadSessionNotCompletedException(Resources.Messages.Error_UploadSession_MarkFilesAsUsedMethod_UploadIsNotCompleted);
            }

            FileUploadDescription fileDescription = this.GetFileUploadDescription(fileId);

            fileDescription.MarkAsUsed();

            this.AddChange(new UploadSessionFileMarkedAsUsed(this.Id.ToString(), fileDescription.Id.ToString()));
        }

        public void MarkFilesAsUsed(IEnumerable<Guid> fileIds)
        {
            AssertionHelper.AssertNotNull(fileIds, "fileIds");

            try
            {
                foreach (Guid id in fileIds)
                {
                    this.MarkFileAsUsed(id);
                }
            }
            catch (UploadSessionFileNotRegisteredException e)
            {
                throw new UploadSessionFileNotRegisteredException(string.Format(Resources.Messages.Error_UploadSession_MarkFilesAsUsedMethod_FileIsNotRegistered, this.Id), e);
            }
        }

        public void AssignUriToFileDescription(Guid fileId, ResourceUri uri)
        {
            this.GetFileUploadDescription(fileId).AssignUri(uri);
        }

        public FileUploadDescription GetFileUploadDescription(Guid fileId)
        {
            if (this.IsNotStarted)
            {
                throw new UploadSessionNotStartedException(Resources.Messages.Error_UploadSession_GetFileUploadDescriptionMethod_SessioIsNotStarted);
            }

            FileUploadDescription fileDescription = (from file in this.InnerFileDescriptions
                                                     where file.Id == fileId
                                                     select file)
                                                     .SingleOrDefault();

            if (fileDescription == null)
            {
                throw new UploadSessionFileNotRegisteredException(fileId);
            }

            return fileDescription;
        }

        public bool IsFileRegistered(Guid fileId)
        {
            return this.InnerFileDescriptions.Any(fileDescription => fileDescription.Id == fileId);
        }

        public bool AreFilesRegistered(IEnumerable<Guid> fileIds)
        {
            foreach (var id in fileIds)
            {
                if (!this.IsFileRegistered(id))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsFileUploaded(Guid fileId)
        {
            if (!IsFileRegistered(fileId))
            {
                throw new UploadSessionFileNotRegisteredException(fileId);
            }

            return this.InnerFileDescriptions.Any(fileDescription => fileDescription.Id == fileId && fileDescription.IsUploaded);
        }

        #endregion

        #region Properties

        public SessionState State
        {
            get
            {
                return this.state;
            }
            private set
            {
                AssertionHelper.AssertIsInEnum<SessionState>(value, "State", Resources.Messages.Error_UploadSession_State_IsNotValid);
                this.state = value;
            }
        }

        public OwnerId OwnerId
        {
            get
            {
                return this.ownerId;
            }
            private set
            {
                AssertionHelper.AssertNotNull(value, "OwnerId", Resources.Messages.Error_UploadSession_OwnerId_IsNull);
                this.ownerId = value;
            }
        }

        public DateTimeOffset? StartDate
        {
            get
            {
                return this.startDate;
            }
            private set
            {
                this.startDate = value;
            }
        }

        public DateTimeOffset? EndDate
        {
            get
            {
                return this.endDate;
            }
            private set
            {
                this.endDate = value;
            }
        }

        internal List<FileUploadDescription> InnerFileDescriptions
        {
            get
            {
                return this.innerFileDescriptions;
            }
            private set
            {
                this.innerFileDescriptions = value;
            }
        }

        public IReadOnlyCollection<FileUploadDescription> FileDescriptions
        {
            get
            {
                return this.InnerFileDescriptions.AsReadOnly();
            }
        }

        public bool UploadAsPublic
        {
            get
            {
                return this.uploadAsPublic;
            }
            private set
            {
                this.uploadAsPublic = value;
            }
        }

        public bool IsNotStarted
        {
            get
            {
                return this.State == SessionState.NotStarted;
            }
        }

        public bool IsStarted
        {
            get
            {
                return this.State == SessionState.Started;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return this.State == SessionState.Completed;
            }
        }

        public bool IsCanceled
        {
            get
            {
                return this.State == SessionState.Canceled;
            }
        }

        public bool AreAllFilesUploaded
        {
            get
            {
                return this.InnerFileDescriptions.All(file => file.IsUploaded);
            }
        }

        public bool AreAllFilesInUse
        {
            get
            {
                return this.FileDescriptions.All(f => f.IsInUse);
            }
        }

        #endregion
    }
}
