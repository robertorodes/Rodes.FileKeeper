using Eventual.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class FileUploadService
    {
        #region Attributes

        private IUploadSessionRepository uploadSessionRepository;
        private IFileRepository fileRepository;
        private ILogger logger;

        #endregion

        #region Constructors

        public FileUploadService()
        {

        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        private IUploadSessionRepository UploadSessionRepository
        {
            get
            {
                return this.uploadSessionRepository;
            }
            set
            {
                //TODO: Add validation code
                this.uploadSessionRepository = value;
            }
        }

        private IFileRepository FileRepository
        {
            get
            {
                return this.fileRepository;
            }
            set
            {
                //TODO: Add validation code
                this.fileRepository = value;
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
