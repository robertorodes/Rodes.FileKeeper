using Eventual.Common.Logging;
using Ninject;

namespace Rodes.FileKeeper.Infrastructure.Dependencies
{
    public class NinjectServiceFactory : IServiceFactory
    {
        #region Attributes

        IKernel kernel;

        #endregion

        #region Constructors

        public NinjectServiceFactory()
        {
            this.kernel = new StandardKernel(new NinjectFileStorageModule());
        }

        #endregion

        #region Properties

        public ILogger Logger
        {
            get
            {
                return this.kernel.Get<ILogger>();
            }
        }

        #endregion
    }
}
