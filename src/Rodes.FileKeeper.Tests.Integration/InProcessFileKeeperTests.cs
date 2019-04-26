using Eventual.Common.Logging.DiagnosticsTraceSource;
using Eventual.EventStore.EntityFrameworkCore;
using Eventual.EventStore.Serialization.Json;
using Eventual.EventStore.Services;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Rodes.FileKeeper.Infrastructure.Dependencies;
using Rodes.FileKeeper.Application;
using Rodes.FileKeeper.Contracts.Commands;
using Rodes.FileKeeper.Domain;
using Rodes.FileKeeper.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rodes.FileKeeper.Tests.Integration
{
    public class InProcessFileKeeperTests: IClassFixture<InProcessFileKeeperTestsFixture>
    {
        #region Properties

        public InProcessFileKeeperTestsFixture Fixture { get; private set; }

        #endregion

        #region Constructors

        public InProcessFileKeeperTests(InProcessFileKeeperTestsFixture fixture)
        {
            this.Fixture = fixture;
        }

        #endregion

        #region StartUploadSession command tests

        [Fact]
        public async Task StartUploadSession_GivenValidArgs_DoesNotThrowException()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Console.WriteLine(config.FilePath);

            // Arrange
            string sessionId = "16715015-d920-4998-87fa-9344145c901c";
            StartUploadSessionCommand command = new StartUploadSessionCommand(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new CommandIssuer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                sessionId,
                new OwnerIdData(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                new List<FileDescriptionData>()
                {
                    new FileDescriptionData(Guid.NewGuid().ToString(), "testFile.pdf", "application/pdf", 1048576),
                    new FileDescriptionData(Guid.NewGuid().ToString(), "testFile2.pdf", "application/msword", 1048576)
                },
                true);
            var applicationService = this.Fixture.Kernel.Get<IFileStorageCommandApplicationService>();
            var uploadSessionRepository = this.Fixture.Kernel.Get<IUploadSessionRepository>();

            // Act
            await applicationService.StartUploadSessionAsync(command);

            // Assert
            UploadSession session = await uploadSessionRepository.GetByIdAsync(new Guid(sessionId));
            Assert.NotNull(session);
            Assert.Equal(sessionId, session.Id.ToString());
        }

        #endregion

        #region UploadFiles command tests

        [Fact]
        public async Task UploadFiles_GivenValidArgsAndSessionStarted_DoesNotThrowException()
        {
            // Arrange
            string sessionId = "780352d9-ed37-47de-b03d-d47327421df9";
            string file01Id = "d9c2e134-9687-4c2f-a639-4080d4bf15d9";
            string file02Id = "8a4a155b-92b9-48f9-8d5c-d5f9c495ea53";
            UploadFilesCommand command = new UploadFilesCommand(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new CommandIssuer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                sessionId, new List<FileData>()
                {
                    new FileData(file01Id, new MemoryStream(Encoding.UTF8.GetBytes("testcontent01"))),
                    new FileData(file02Id, new MemoryStream(Encoding.UTF8.GetBytes("testcontent02"))),
                });
            var applicationService = this.Fixture.Kernel.Get<IFileStorageCommandApplicationService>();
            var uploadSessionRepository = this.Fixture.Kernel.Get<IUploadSessionRepository>();

            // Act
            await applicationService.UploadFilesAsync(command);

            // Assert
            UploadSession session = await uploadSessionRepository.GetByIdAsync(new Guid(sessionId));
            Assert.NotNull(session);
            Assert.True(session.IsFileUploaded(new Guid(file01Id)));
            Assert.True(session.IsFileUploaded(new Guid(file02Id)));
        }

        #endregion

        #region EndUploadSession command tests

        [Fact]
        public async Task EndUploadSession_GivenValidArgsAndSessionStarted_DoesNotThrowException()
        {
            // Arrange
            string sessionId = "66339b0d-e3a1-4c7c-ae87-a52a10bb4bba";
            EndUploadSessionCommand command = new EndUploadSessionCommand(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new CommandIssuer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                sessionId);
            var applicationService = this.Fixture.Kernel.Get<IFileStorageCommandApplicationService>();
            var uploadSessionRepository = this.Fixture.Kernel.Get<IUploadSessionRepository>();

            // Act
            await applicationService.EndUploadSessionAsync(command);

            // Assert
            UploadSession session = await uploadSessionRepository.GetByIdAsync(new Guid(sessionId));
            Assert.NotNull(session);
            Assert.Equal(sessionId, session.Id.ToString());
            Assert.True(session.AreAllFilesUploaded);
            Assert.True(session.IsCompleted);
        }

        #endregion

        #region MarkFilesAsUsed command tests

        [Fact]
        public async Task MarkFilesAsUsed_GivenValidArgsAndSessionEnded_DoesNotThrowException()
        {
            // Arrange
            string sessionId = "273fbe3c-5e1d-47ea-bef3-d37783e10dd1";
            string file01Id = "939be7f6-33ef-412b-850f-c27f240a7787";
            string file02Id = "2c10700b-6779-4621-9117-eb4c9c41fa72";
            List<string> fileIds = new List<string>()
            {
                file01Id,
                file02Id
            };

            MarkFilesAsUsedCommand command = new MarkFilesAsUsedCommand(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new CommandIssuer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                fileIds);
            var applicationService = this.Fixture.Kernel.Get<IFileStorageCommandApplicationService>();
            var uploadSessionRepository = this.Fixture.Kernel.Get<IUploadSessionRepository>();
            var fileInformationIndexRepository = this.Fixture.Kernel.Get<IFileInformationIndexRepository>();

            // Act
            await applicationService.MarkFilesAsUsedAsync(command);

            // Assert
            UploadSession session = await uploadSessionRepository.GetByIdAsync(new Guid(sessionId));
            FileInformationIndex index01 = await fileInformationIndexRepository.GetByIdAsync(new Guid(file01Id));
            FileInformationIndex index02 = await fileInformationIndexRepository.GetByIdAsync(new Guid(file02Id));
            Assert.NotNull(session);
            Assert.Equal(sessionId, session.Id.ToString());
            Assert.True(session.AreAllFilesInUse);
            Assert.NotNull(index01);
            Assert.NotNull(index02);
        }

        #endregion
    }
}
