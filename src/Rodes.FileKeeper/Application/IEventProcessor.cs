using Eventual.EventStore.Services;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Application
{
    public interface IEventProcessor
    {
        Task Process(Event @event);
    }
}
