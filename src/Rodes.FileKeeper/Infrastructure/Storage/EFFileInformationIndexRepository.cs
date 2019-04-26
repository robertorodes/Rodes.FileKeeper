using Microsoft.EntityFrameworkCore;
using Rodes.FileKeeper.Domain;
using Eventual.Common.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.Storage
{
    public class EFFileInformationIndexRepository : IFileInformationIndexRepository
    {
        #region Attributes

        private FileStorageDbContext dbContext;
        private ILogger logger;

        #endregion

        #region Constructors

        public EFFileInformationIndexRepository(FileStorageDbContext dbContext, ILogger logger)
        {
            this.DbContext = dbContext;
            this.Logger = logger;
        }

        #endregion

        #region Methods

        public void Add(FileInformationIndex fileInformationIndex)
        {
            this.DbContext.FileInformationIndexes.Add(fileInformationIndex);
        }

        public async Task<FileInformationIndex> GetByIdAsync(Guid fileId)
        {
            return await (from index in this.DbContext.FileInformationIndexes
                          where index.Id == fileId
                          select index)
                    .SingleOrDefaultAsync();
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
            private set
            {
                //TODO: Add validation code
                this.dbContext = value;
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
