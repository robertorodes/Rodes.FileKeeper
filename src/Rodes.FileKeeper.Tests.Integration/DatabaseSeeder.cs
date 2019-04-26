using Rodes.FileKeeper.Domain;
using Rodes.FileKeeper.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rodes.FileKeeper.Tests.Integration
{
    static class DatabaseSeeder
    {
        static public void Seed(IUploadSessionRepository uploadSessionRepository)
        {
            try
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                // Session 1 (session started, pending file upload)
                Guid sessionId = new Guid("780352d9-ed37-47de-b03d-d47327421df9");
                OwnerId ownerId = new OwnerId(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                Guid file01Id = new Guid("d9c2e134-9687-4c2f-a639-4080d4bf15d9");
                Guid file02Id = new Guid("8a4a155b-92b9-48f9-8d5c-d5f9c495ea53");
                UploadSession seedSession = UploadSession.Start(sessionId,
                    ownerId,
                    new List<FileUploadDescription>()
                    {
                        new FileUploadDescription(file01Id, sessionId, "file01.pdf", new ContentType("application/pdf"), 1000000),
                        new FileUploadDescription(file02Id, sessionId, "file02.pdf", new ContentType("application/pdf"), 2000000)
                    },
                    true);

                uploadSessionRepository.Add(seedSession);

                // Session 2 (session started and files uploaded)
                sessionId = new Guid("66339b0d-e3a1-4c7c-ae87-a52a10bb4bba");
                ownerId = new OwnerId(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                file01Id = new Guid("4e972d88-127b-4e25-9a92-7edce0be29b7");
                file02Id = new Guid("b2b6b647-944a-45a3-b54c-c832e04d224d");
                seedSession = UploadSession.Start(sessionId,
                    ownerId,
                    new List<FileUploadDescription>()
                    {
                        new FileUploadDescription(file01Id, sessionId, "file01.pdf", new ContentType("application/pdf"), 1000000),
                        new FileUploadDescription(file02Id, sessionId, "file02.pdf", new ContentType("application/pdf"), 2000000)
                    },
                    true);
                foreach (var file in seedSession.FileDescriptions)
                {
                    file.MarkAsUploaded();
                    file.AssignUri(new ResourceUri("https://thisisatest.com"));
                }

                uploadSessionRepository.Add(seedSession);

                // Session 3 (files uploaded and session ended, files not marked as used)
                sessionId = new Guid("273fbe3c-5e1d-47ea-bef3-d37783e10dd1");
                ownerId = new OwnerId(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                file01Id = new Guid("939be7f6-33ef-412b-850f-c27f240a7787");
                file02Id = new Guid("2c10700b-6779-4621-9117-eb4c9c41fa72");
                seedSession = UploadSession.Start(sessionId,
                    ownerId,
                    new List<FileUploadDescription>()
                    {
                        new FileUploadDescription(file01Id, sessionId, "file01.pdf", new ContentType("application/pdf"), 1000000),
                        new FileUploadDescription(file02Id, sessionId, "file02.pdf", new ContentType("application/pdf"), 2000000)
                    },
                    true);
                foreach (var file in seedSession.FileDescriptions)
                {
                    file.MarkAsUploaded();
                    file.AssignUri(new ResourceUri("https://thisisatest.com"));
                }
                seedSession.End();

                uploadSessionRepository.Add(seedSession);

                uploadSessionRepository.SaveChangesAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
