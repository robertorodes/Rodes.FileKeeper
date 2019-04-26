using Eventual.EventStore.Services;
using Rodes.FileKeeper.Domain;
using Eventual.Common.Logging;
using ReflectionMagic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Application
{
    public class FileStorageEventProcessor : IEventProcessor
    {
        #region Constants

        private readonly IList<Type> SupportedEvents = new List<Type>()
        {
            typeof(UploadSessionStarted),
            typeof(UploadSessionFileMarkedAsUploaded)
        };

        #endregion

        #region Attributes

        private IFileStorageCommandApplicationService fileStorageService;
        private ILogger logger;

        #endregion

        #region Constructors

        public FileStorageEventProcessor(IFileStorageCommandApplicationService applicationService, ILogger logger)
        {
            this.FileStorageService = applicationService;
            this.Logger = logger;
        }

        #endregion

        #region Methods

        public async Task Process(Event @event)
        {
            if (this.IsEventSupported(@event))
            {
                await this.AsDynamic().When(@event);
            }
        }

        private bool IsEventSupported(Event @event)
        {
            if (this.SupportedEvents.Contains(@event.GetType()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task When(UploadSessionStarted @event)
        {

        }

        private async Task When(UploadSessionFileMarkedAsUploaded @event)
        {

        }

        #endregion

        #region Properties

        private IFileStorageCommandApplicationService FileStorageService
        {
            get
            {
                return this.fileStorageService;
            }
            set
            {
                //TODO: Add validation code
                this.fileStorageService = value;
            }
        }

        private ILogger Logger
        {
            get
            {
                return this.logger;
            }
            set
            {
                //TODO: Add validation code
                this.logger = value;
            }
        }

        #endregion
    }
}
