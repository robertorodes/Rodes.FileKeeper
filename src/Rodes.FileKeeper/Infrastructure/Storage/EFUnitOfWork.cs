using Eventual.EventStore.Core;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.Storage
{
    public class EFUnitOfWork : IUnitOfWork
    {
        #region Attributes

        private FileStorageDbContext dbContext;

        #endregion

        #region Constructors

        public EFUnitOfWork(FileStorageDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        #endregion

        #region Methods

        public async Task CommitChangesAsync()
        {
            await this.DbContext.CommitChangesAsync();
        }

        #endregion

        #region Properties

        public FileStorageDbContext DbContext
        {
            get
            {
                return this.dbContext;
            }
            private set
            {
                this.dbContext = value;
            }
        }

        #endregion
    }
}
