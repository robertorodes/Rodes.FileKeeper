using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ninject;
using Rodes.FileKeeper.Infrastructure.Dependencies;
using Rodes.FileKeeper.Domain;
using Rodes.FileKeeper.Infrastructure.Configuration;
using Rodes.FileKeeper.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rodes.FileKeeper.Tests.Integration
{
    public class InProcessFileKeeperTestsFixture : IDisposable
    {
        #region Properties

        public StandardKernel Kernel { get; private set; }
        private StandardKernel InitializationKernel { get; set; }

        #endregion

        #region Constructors

        public InProcessFileKeeperTestsFixture()
        {
            // TODO: Fix this dependency (the following is just a temporal solution to solve dependency problem with Json library
            //Console.WriteLine(Newtonsoft.Json.DateFormatHandling.IsoDateFormat);
            string workaround = JsonConvert.SerializeObject(new { workaround = "workaround" });
            Console.WriteLine(workaround);

            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<FileStorageDbContext>()
                            .UseSqlServer(ApplicationSettings.Storage.DataStoreConnectionString)
                            .Options;

            using (var dbContext = new FileStorageDbContext(options))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.SaveChanges();

                ConfigureDependencies(dbContext);
                IUploadSessionRepository uploadSessionRepository = this.InitializationKernel.Get<IUploadSessionRepository>();

                DatabaseSeeder.Seed(uploadSessionRepository);
            }
        }

        private void ConfigureDependencies(FileStorageDbContext dbContext)
        {
            // Create and initialize inner kernel for test suite initialization
            this.InitializationKernel = new StandardKernel(new NinjectFileStorageModule());
            this.InitializationKernel.Unbind<FileStorageDbContext>();
            this.InitializationKernel.Bind<FileStorageDbContext>().ToConstant(dbContext);

            // Create shared kernel to be used by each individual test case
            this.Kernel = new StandardKernel(new NinjectFileStorageModule());
        }

        #endregion

        #region Public methods

        public void Dispose()
        {
            this.Kernel.Dispose();
        }

        #endregion
    }
}
