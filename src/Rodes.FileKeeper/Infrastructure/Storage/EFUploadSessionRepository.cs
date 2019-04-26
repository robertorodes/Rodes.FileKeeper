using Eventual.EventStore.Services;
using Microsoft.EntityFrameworkCore;
using Rodes.FileKeeper.Domain;
using Rodes.FileKeeper.Infrastructure.AppContext;
using Eventual.Common.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.Storage
{
    public class EFUploadSessionRepository : IUploadSessionRepository, IApplicationContextRegistry
    {
        #region Attributes

        private FileStorageDbContext dbContext;
        private ITypedEventStore eventStore;
        private ILogger logger;
        private IApplicationContext applicationContext;

        #endregion

        #region Constructors

        public EFUploadSessionRepository(FileStorageDbContext dbContext, ITypedEventStore eventStore, ILogger logger)
        {
            this.DbContext = dbContext;
            this.EventStore = eventStore;
            this.Logger = logger;
            this.ApplicationContext = new ApplicationContext();
        }

        #endregion

        #region Methods

        public async Task Add(UploadSession session)
        {
            this.DbContext.UploadSessions.Add(session);

            await this.SaveEvents(session);
        }

        private async Task SaveEvents(UploadSession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            var changes = session.GetUncommittedChanges();
            if (changes != null && changes.Count() > 0)
            {
                TypedEventStreamCommit commit = new TypedEventStreamCommit(session.Id,
                    session.GetType(), session.Version, this.ApplicationContext.CorrelationId,
                    this.ApplicationContext.CausationId, this.ApplicationContext.CustomMetadata, changes);
                await this.EventStore.CommitChangesAsync(commit);
            }
        }

        public async Task<UploadSession> GetByIdAsync(Guid sessionId)
        {
            //return await (from session in this.DbContext.UploadSessions
            //        where session.Id == sessionId
            //        select session)
            //        .Include(s => s.InnerFileDescriptions)
            //        .SingleOrDefaultAsync();

            var result = await (from session in this.DbContext.UploadSessions
                                join revision in this.DbContext.StoredRevisions
                                on session.Id equals revision.AggregateId
                                where session.Id == sessionId
                                orderby revision.RevisionId descending
                                select new
                                {
                                    Session = session,
                                    // The following line is a workaround for eager loading the inner collection in session
                                    SessionInnerFileDescriptions = session.InnerFileDescriptions,
                                    Version = revision.RevisionId
                                })
                                            //.Include(s => s.Session.InnerFileDescriptions)
                                            .FirstOrDefaultAsync();

            //var result = await this.DbContext.UploadSessions
            //    .Include(s => s.OwnerId)
            //    .Include(s => s.FileDescriptions)
            //    .ThenInclude(d => d.ContentType)
            //    .Include(s => s.FileDescriptions)
            //    .ThenInclude(d => d.AssignedUri)
            //    .Join(
            //        this.DbContext.StoredRevisions,
            //        sessions => sessions.Id,
            //        revisions => revisions.AggregateId,
            //        (s, r) => new {
            //            Session = s,
            //            Revision = r })
            //    .Where(p => p.Session.Id == sessionId)
            //    .OrderByDescending(p => p.Revision.RevisionId)
            //    .Select(p => new {
            //        Session = p.Session,
            //        Version = p.Revision.RevisionId
            //    })
            //    .FirstOrDefaultAsync();

            if (result != null)
            {
                result.Session.Version = result.Version;
                return result.Session;
            }
            else
            {
                return null;
            }
        }

        public async Task<UploadSession> GetByFileIdAsync(Guid fileId)
        {
            var result = await (from session in this.DbContext.UploadSessions
                                join revision in this.DbContext.StoredRevisions
                                on session.Id equals revision.AggregateId
                                where session.InnerFileDescriptions.Any(f => f.Id == fileId)
                                orderby revision.RevisionId descending
                                select new
                                {
                                    Session = session,
                                    // The following line is a workaround for eager loading the inner collection in session
                                    SessionInnerFileDescriptions = session.InnerFileDescriptions,
                                    Version = revision.RevisionId
                                })
                                .FirstOrDefaultAsync();

            if (result != null)
            {
                result.Session.Version = result.Version;
                return result.Session;
            }
            else
            {
                return null;
            }
        }

        public async Task SaveChangesAsync()
        {
            await this.DbContext.SaveChangesAsync();
        }

        #endregion

        #region Properties

        public FileStorageDbContext DbContext
        {
            get
            {
                return this.dbContext;
            }
            set
            {
                //TODO: Insert validation code here
                this.dbContext = value;
            }
        }

        private ITypedEventStore EventStore
        {
            get
            {
                return this.eventStore;
            }
            set
            {
                //TODO: Insert validation code here
                this.eventStore = value;
            }
        }

        private ILogger Logger
        {
            get
            {
                return this.logger;
            }
            set
            {
                //TODO: Insert validation code here
                this.logger = value;
            }
        }

        public IApplicationContext ApplicationContext
        {
            get
            {
                return this.applicationContext;
            }
            set
            {
                //TODO: Insert validation code here
                this.applicationContext = value;
            }
        }

        #endregion
    }
}
