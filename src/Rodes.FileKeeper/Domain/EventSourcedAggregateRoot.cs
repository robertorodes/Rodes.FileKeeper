using Eventual.EventStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public abstract class EventSourcedAggregateRoot : Entity
    {
        #region Attributes

        private readonly List<Event> changes = new List<Event>();
        private int version;

        #endregion

        #region Methods

        public EventSourcedAggregateRoot()
        {
            this.Version = -1;
        }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return changes.AsReadOnly();
        }

        public void MarkChangesAsCommitted()
        {
            changes.Clear();
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        protected void AddChange(Event @event)
        {
            changes.Add(@event);
        }

        #endregion

        #region Properties

        public int Version
        {
            get
            {
                return this.version;
            }
            set
            {
                //TODO: Insert validation code here
                this.version = value;
            }
        }

        #endregion
    }
}
