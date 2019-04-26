using Eventual.EventStore.Core;
using Eventual.EventStore.Readers.Reactive;
using Eventual.EventStore.Serialization;
using Eventual.EventStore.Serialization.Json;
using Eventual.EventStore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.EventProcessor
{
    public class FileStorageEventStoreInternalTrackingClient : IFileKeeperEventStoreTrackingClient
    {
        #region Attributes

        private IEventStreamTrackedReactiveReader client;
        private IEventsSerializer eventSerializer;
        private IMetadataSerializer metadataSerializer;

        #endregion

        #region Constructors

        public FileStorageEventStoreInternalTrackingClient(IEventStreamTrackedReactiveReader client)
        {
            this.Client = client;
            this.EventSerializer = new JsonEventsSerializer();
            this.MetadataSerializer = new JsonMetadataSerializer();
        }

        public FileStorageEventStoreInternalTrackingClient(IEventStreamTrackedReactiveReader client, IEventsSerializer eventSerializer,
            IMetadataSerializer metadataSerializer)
        {
            this.Client = client;
            this.EventSerializer = eventSerializer;
            this.MetadataSerializer = metadataSerializer;
        }

        #endregion

        #region Methods

        public async Task CatchUpAllEventStreamsAsync(string trackerId, Action<ResolvedExternalEvent> onNext)
        {
            await this.Client.CatchUpAllEventStreamsAsync(trackerId, revision =>
            {
                this.ProcessRevision(revision, onNext);
            });
        }

        private void ProcessRevision(Revision revision, Action<ResolvedExternalEvent> handle)
        {
            int i = 0;
            IEnumerable<Event> originalEvents = this.EventSerializer.Deserialize(revision.Changes);
            Dictionary<string, string> metadata = this.MetadataSerializer.Deserialize(revision.Metadata);
            foreach (var @event in originalEvents)
            {
                handle(new ResolvedExternalEvent(revision.AggregateId, revision.CommitId, revision.RevisionId,
                    i, revision.CorrelationId, revision.CausationId, metadata, @event));

                i++;
            }
        }

        private async Task ProcessRevisionAsync(Revision revision, Func<ResolvedExternalEvent, Task> handle)
        {
            int i = 0;
            IEnumerable<Event> originalEvents = this.EventSerializer.Deserialize(revision.Changes);
            Dictionary<string, string> metadata = this.MetadataSerializer.Deserialize(revision.Metadata);
            foreach (var @event in originalEvents)
            {
                await handle(new ResolvedExternalEvent(revision.AggregateId, revision.CommitId, revision.RevisionId,
                    i, revision.CorrelationId, revision.CausationId, metadata, @event));

                i++;
            }
        }

        public async Task CatchUpAllEventStreamsAsync(string trackerId, Func<ResolvedExternalEvent, Task> onNext)
        {
            await this.Client.CatchUpAllEventStreamsAsync(trackerId, async revision =>
            {
                await this.ProcessRevisionAsync(revision, onNext);
            });
        }

        public async Task CatchUpAllEventStreamsAsync(string trackerId, Action<ResolvedExternalEvent> onNext, CancellationToken cancellationToken)
        {
            await this.Client.CatchUpAllEventStreamsAsync(trackerId, revision =>
            {
                this.ProcessRevision(revision, onNext);
            }, cancellationToken);
        }

        public async Task CatchUpAllEventStreamsAsync(string trackerId, Func<ResolvedExternalEvent, Task> onNext, CancellationToken cancellationToken)
        {
            await this.Client.CatchUpAllEventStreamsAsync(trackerId, async revision =>
            {
                await this.ProcessRevisionAsync(revision, onNext);
            }, cancellationToken);
        }

        public async Task ContinuouslyCatchUpAllEventStreamsAsync(string trackerId, Action<ResolvedExternalEvent> onNext)
        {
            await this.Client.ContinuouslyCatchUpAllEventStreamsAsync(trackerId, revision =>
            {
                this.ProcessRevision(revision, onNext);
            });
        }

        public async Task ContinuouslyCatchUpAllEventStreamsAsync(string trackerId, Func<ResolvedExternalEvent, Task> onNext)
        {
            await this.Client.ContinuouslyCatchUpAllEventStreamsAsync(trackerId, async revision =>
            {
                await this.ProcessRevisionAsync(revision, onNext);
            });
        }

        public async Task ContinuouslyCatchUpAllEventStreamsAsync(string trackerId, Action<ResolvedExternalEvent> onNext, CancellationToken cancellationToken)
        {
            await this.Client.ContinuouslyCatchUpAllEventStreamsAsync(trackerId, revision =>
            {
                this.ProcessRevision(revision, onNext);
            }, cancellationToken);
        }

        public async Task ContinuouslyCatchUpAllEventStreamsAsync(string trackerId, Func<ResolvedExternalEvent, Task> onNext, CancellationToken cancellationToken)
        {
            await this.Client.ContinuouslyCatchUpAllEventStreamsAsync(trackerId, async revision =>
            {
                await this.ProcessRevisionAsync(revision, onNext);
            }, cancellationToken);
        }

        #endregion

        #region Properties

        private IEventStreamTrackedReactiveReader Client
        {
            get
            {
                return this.client;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Client");
                }

                this.client = value;
            }
        }

        public IEventsSerializer EventSerializer
        {
            get
            {
                return this.eventSerializer;
            }
            private set
            {
                //TODO: Insert validation code here
                this.eventSerializer = value;
            }
        }

        public IMetadataSerializer MetadataSerializer
        {
            get
            {
                return this.metadataSerializer;
            }
            private set
            {
                //TODO: Insert validation code here
                this.metadataSerializer = value;
            }
        }

        #endregion
    }
}
