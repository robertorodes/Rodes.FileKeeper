using Eventual.EventStore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rodes.FileKeeper.EventProcessor
{
    public class ResolvedExternalEvent
    {
        #region Attributes

        private Guid streamId;
        private long commitId;
        private int revisionId;
        private int eventId;
        private string correlationId;
        private string causationId;
        private Dictionary<string, string> metadata;
        private Event sourceEvent;

        #endregion

        #region Constructors

        public ResolvedExternalEvent(Guid streamId, long commitId, int revisionId, int eventId,
            string correlationId, string causationId, Dictionary<string, string> metadata,
            Event sourceEvent)
        {
            this.StreamId = streamId;
            this.CommitId = commitId;
            this.RevisionId = revisionId;
            this.EventId = eventId;
            this.CorrelationId = correlationId;
            this.CausationId = causationId;
            this.Metadata = metadata;
            this.SourceEvent = sourceEvent;
        }

        #endregion

        #region Properties

        public Guid StreamId
        {
            get
            {
                return this.streamId;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("StreamId");
                }

                this.streamId = value;
            }
        }

        public long CommitId
        {
            get
            {
                return this.commitId;
            }
            private set
            {
                //if (value == null)
                //{
                //    throw new ArgumentNullException("CommitId");
                //}

                this.commitId = value;
            }
        }

        public int RevisionId
        {
            get
            {
                return this.revisionId;
            }
            private set
            {
                //if (value == null)
                //{
                //    throw new ArgumentNullException("RevisionId");
                //}

                this.revisionId = value;
            }
        }

        public int EventId
        {
            get
            {
                return this.eventId;
            }
            private set
            {
                //if (value == null)
                //{
                //    throw new ArgumentNullException("EventId");
                //}

                this.eventId = value;
            }
        }

        public string CorrelationId
        {
            get
            {
                return this.correlationId;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CorrelationId");
                }

                this.correlationId = value;
            }
        }

        public string CausationId
        {
            get
            {
                return this.causationId;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CausationId");
                }

                this.causationId = value;
            }
        }

        public Dictionary<string, string> Metadata
        {
            get
            {
                return this.metadata;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Metadata");
                }

                this.metadata = value;
            }
        }

        public Event SourceEvent
        {
            get
            {
                return this.sourceEvent;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("SourceEvent");
                }

                this.sourceEvent = value;
            }
        }

        #endregion
    }
}
