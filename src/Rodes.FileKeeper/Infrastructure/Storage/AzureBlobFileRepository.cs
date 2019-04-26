using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Rodes.FileKeeper.Domain;
using Rodes.FileKeeper.Infrastructure.Configuration;
using Eventual.Common.Logging;
using System;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.Storage
{
    //TODO: Encapsulate/Hide azure storage exceptions
    public class AzureBlobFileRepository : IFileRepository
    {
        #region Constants

        private static readonly string PublicContainerName = ApplicationSettings.Storage.AzureBlobs.PublicContainerName;
        private static readonly string PrivateContainerName = ApplicationSettings.Storage.AzureBlobs.PrivateContainerName;
        private const string FileIdMetadataKeyName = "FileId";
        private const string FileNameMetadataKeyName = "FileName";
        private const string FileOwnerBusinessIdMetadataKeyName = "Owner_BusinessId";
        private const string FileOwnerUserIdMetadataKeyName = "Owner_UserId";
        private const string FileIsPublicMetadataKeyName = "IsPublic";

        #endregion

        #region Attributes

        private CloudBlobClient blobClient;
        private ILogger logger;

        #endregion

        #region Constructors

        public AzureBlobFileRepository(string connectionString, ILogger logger)
        {
            this.BlobClient = this.CreateBlobClient(connectionString);
            this.Logger = logger;
        }

        #endregion

        #region Helper methods

        private CloudBlobClient CreateBlobClient(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            return storageAccount.CreateCloudBlobClient();
        }

        private async Task<CloudBlobContainer> CreateBlobContainerIfNotExists(string containerName)
        {
            if (containerName == null)
            {
                throw new ArgumentNullException("containerName");
            }

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            if (await container.CreateIfNotExistsAsync())
            {
                // Enable public access on the newly created container
                await container.SetPermissionsAsync(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    });

                //log.Information("Successfully created Blob Storage Images Container and made it public");
            }

            return container;
        }

        #endregion

        #region Methods

        public async Task<ResourceUri> SaveAsync(File file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            string containerName = (file.IsPublic) ? PublicContainerName : PrivateContainerName;
            CloudBlobContainer container = await this.CreateBlobContainerIfNotExists(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.Id.ToString());
            blockBlob.Properties.ContentType = file.ContentType.Type;
            blockBlob.Metadata.Add(FileIdMetadataKeyName, file.Id.ToString());
            blockBlob.Metadata.Add(FileNameMetadataKeyName, file.FileName);
            blockBlob.Metadata.Add(FileOwnerBusinessIdMetadataKeyName, file.OwnerId.BusinessId);
            blockBlob.Metadata.Add(FileOwnerUserIdMetadataKeyName, file.OwnerId.UserId);
            blockBlob.Metadata.Add(FileIsPublicMetadataKeyName, file.IsPublic.ToString());
            await blockBlob.UploadFromStreamAsync(file.Content);

            file.SetOriginUri(blockBlob.Uri);

            return new ResourceUri(blockBlob.Uri.AbsoluteUri);
        }

        public async Task<File> GetByIdAsync(Guid fileId)
        {
            File resultFile = null;
            CloudBlobContainer privateContainer = blobClient.GetContainerReference(PrivateContainerName);
            CloudBlobContainer publicContainer = blobClient.GetContainerReference(PublicContainerName);

            // Search file on private container
            var searchInPrivateContainerTask = this.GetFileFromBlobContainer(privateContainer, fileId, false);

            // Search file on public container
            var searchInPublicContainerTask = this.GetFileFromBlobContainer(publicContainer, fileId, true);

            File[] files = await Task.WhenAll(searchInPrivateContainerTask, searchInPublicContainerTask);

            foreach (var file in files)
            {
                if (file != null)
                {
                    resultFile = file;
                }
            }

            return resultFile;
        }

        private async Task<File> GetFileFromBlobContainer(CloudBlobContainer container, Guid fileId, bool isPublicContainer)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileId.ToString());
            File resultFile = null;

            if (await blockBlob.ExistsAsync())
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    await blockBlob.DownloadToStreamAsync(stream);

                    string ownerBusinessId = blockBlob.Metadata[FileOwnerBusinessIdMetadataKeyName];
                    string ownerUserId = blockBlob.Metadata[FileOwnerUserIdMetadataKeyName];
                    string fileName = blockBlob.Metadata[FileNameMetadataKeyName];
                    ContentType contentType = new ContentType(blockBlob.Properties.ContentType);
                    OwnerId ownerId = new OwnerId(ownerBusinessId, ownerUserId);
                    resultFile = File.Upload(fileId, ownerId, fileName, contentType, stream, isPublicContainer);
                }
            }

            return resultFile;
        }

        #endregion

        #region Properties

        public CloudBlobClient BlobClient
        {
            get
            {
                return this.blobClient;
            }
            private set
            {
                //TODO: Add validation code
                this.blobClient = value;
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
