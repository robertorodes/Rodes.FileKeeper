using Eventual.Common.Logging;
using Ninject;
using Nito.AsyncEx;
using Rodes.FileKeeper.Infrastructure.Dependencies;
using Rodes.FileKeeper.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.EventProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

        static async void MainAsync(string[] args)
        {
            StandardKernel Kernel = new StandardKernel(new NinjectFileStorageModule());
            ILogger logger = Kernel.Get<ILogger>();
            IFileKeeperEventStoreTrackingClient client = Kernel.Get<IFileKeeperEventStoreTrackingClient>();

            ResolvedExternalEvent result = null;

            // Act
            CancellationTokenSource tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            while (true)
            {
                try
                {
                    await client.CatchUpAllEventStreamsAsync("Rodes.FileKeeper.EventProcessor", async @event =>
                    {
                        result = @event;
                        Console.WriteLine(string.Format("Processing event {0} of stream with id = {1}", @event.EventId, @event.StreamId));
                        //await sp.Handle(result.SourceEvent);
                    }, tokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    logger.LogInformation("[Start] A TaskCanceledException has been captured in the ProjectionsUpdater in ShopperProjection.");
                }
                catch (FileStorageException)
                {
                    logger.LogInformation("[Start] A SocialPostsQueryStackException has been captured in the ProjectionsUpdater in ShopperProjection.");
                }
            }
        }
    }
}
