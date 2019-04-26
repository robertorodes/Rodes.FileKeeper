using Eventual.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.Dependencies
{
    public class ServiceLocator
    {
        #region Attributes

        private static volatile ServiceLocator instance;
        private static object syncRoot = new Object();
        private IServiceFactory serviceFactory;

        #endregion

        #region Constructors

        public ServiceLocator(IServiceFactory serviceFactory)
        {
            this.ServiceFactory = serviceFactory;
        }

        #endregion

        #region Public static methods

        public static void Load(ServiceLocator serviceLocator)
        {
            Instance = serviceLocator;
        }

        #endregion

        #region Public methods

        public static ILogger Logger
        {
            get
            {
                return Instance.ServiceFactory.Logger;
            }
        }

        #endregion

        #region Properties

        private static ServiceLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ServiceLocator(new NinjectServiceFactory());
                        }
                    }
                }

                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private IServiceFactory ServiceFactory
        {
            get
            {
                return this.serviceFactory;
            }
            set
            {
                this.serviceFactory = value;
            }
        }

        #endregion
    }
}
