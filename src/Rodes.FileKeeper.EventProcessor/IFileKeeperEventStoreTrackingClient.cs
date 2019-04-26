using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.EventProcessor
{
    interface IFileKeeperEventStoreTrackingClient
    {
        Task CatchUpAllEventStreamsAsync(string trackerId, Action<ResolvedExternalEvent> onNext);

        Task CatchUpAllEventStreamsAsync(string trackerId, Func<ResolvedExternalEvent, Task> onNext);

        Task CatchUpAllEventStreamsAsync(string trackerId, Action<ResolvedExternalEvent> onNext, CancellationToken cancellationToken);

        Task CatchUpAllEventStreamsAsync(string trackerId, Func<ResolvedExternalEvent, Task> onNext, CancellationToken cancellationToken);

        Task ContinuouslyCatchUpAllEventStreamsAsync(string trackerId, Action<ResolvedExternalEvent> onNext);

        Task ContinuouslyCatchUpAllEventStreamsAsync(string trackerId, Func<ResolvedExternalEvent, Task> onNext);

        Task ContinuouslyCatchUpAllEventStreamsAsync(string trackerId, Action<ResolvedExternalEvent> onNext, CancellationToken cancellationToken);

        Task ContinuouslyCatchUpAllEventStreamsAsync(string trackerId, Func<ResolvedExternalEvent, Task> onNext, CancellationToken cancellationToken);
    }
}
