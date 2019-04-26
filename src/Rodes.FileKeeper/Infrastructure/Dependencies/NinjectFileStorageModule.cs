using Ninject.Extensions.NamedScope;
using Ninject.Extensions.ContextPreservation;
using Ninject.Modules;
using Eventual.EventStore.Core;
using Eventual.EventStore.Services;
using Eventual.EventStore.Serialization;
using Eventual.EventStore.Serialization.Json;
using Eventual.EventStore.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rodes.FileKeeper.Infrastructure.Storage;
using Rodes.FileKeeper.Infrastructure.Configuration;
using Eventual.Common.Logging;
using Eventual.Common.Logging.DiagnosticsTraceSource;
using Rodes.FileKeeper.Application;
using Rodes.FileKeeper.Domain;
using Microsoft.EntityFrameworkCore;

namespace Rodes.FileKeeper.Infrastructure.Dependencies
{
    public class NinjectFileStorageModule : NinjectModule
    {
        public override void Load()
        {
            const string ScopeName = "Rodes.FileKeeper";

            // Bind cross cutting service implementations
            Bind<ILogger>().To<DiagnosticsTraceSourceLogger>()
                .Named(ScopeName)
                .WithConstructorArgument("traceSourceName", "Rodes.FileKeeper");

            // Bind application service implementations and filters
            Bind<IFileStorageCommandApplicationService>().To<FileStorageCommandValidationFilter>()
                .Named(ScopeName)
                .DefinesNamedScope(ScopeName);
            Bind<IFileStorageCommandApplicationService>().To<FileStorageCommandAuthorizationFilter>()
                .WhenInjectedExactlyInto<FileStorageCommandValidationFilter>()
                .Named(ScopeName);
            Bind<IFileStorageCommandApplicationService>().To<FileStorageCommandLoggingFilter>()
                .WhenInjectedExactlyInto<FileStorageCommandAuthorizationFilter>()
                .Named(ScopeName);
            Bind<IFileStorageCommandApplicationService>().To<FileStorageCommandApplicationService>()
                .WhenInjectedExactlyInto<FileStorageCommandLoggingFilter>()
                .Named(ScopeName);

            // Bind storage related implementations (entity data and events)
            //Bind<FileStorageDbContext>().ToSelf().InCallScope().WithConstructorArgument("nameOrConnectionString", ApplicationSettings.Storage.DataStoreConnectionStringName);
            //Bind<FileStorageDbContext>().ToSelf().WhenAnyAncestorNamed("test").InNamedScope("test").WithConstructorArgument("nameOrConnectionString", ApplicationSettings.Storage.DataStoreConnectionStringName);

            var options = new DbContextOptionsBuilder<FileStorageDbContext>()
                .UseSqlServer(ApplicationSettings.Storage.DataStoreConnectionString)
                .Options;
            Bind<FileStorageDbContext>().ToSelf().When(request => request.ParentContext.TryGetNamedScope(ScopeName) != null)
                .InNamedScope(ScopeName)
                .Named(ScopeName)
                .WithConstructorArgument("options", options);
            Bind<FileStorageDbContext>().ToSelf()
                .Named(ScopeName)
                .WithConstructorArgument("options", options);
            //Bind<FileStorageDbContext>().ToSelf().InNamedScope("test").WithConstructorArgument("nameOrConnectionString", ApplicationSettings.Storage.DataStoreConnectionStringName);
            Bind<IUploadSessionRepository>().To<EFUploadSessionRepository>().Named(ScopeName);
            Bind<IFileRepository>().To<AzureBlobFileRepository>()
                .Named(ScopeName)
                .WithConstructorArgument("connectionString", ApplicationSettings.Storage.FileStoreConnectionString);
            Bind<IFileInformationIndexRepository>().To<EFFileInformationIndexRepository>().Named(ScopeName);
            Bind<Eventual.EventStore.Core.IUnitOfWork>().To<EFUnitOfWork>()
                .WhenInjectedExactlyInto<FileStorageCommandApplicationService>()
                .Named(ScopeName);

            Bind<ITypedEventStore>().To<TypedEventStore>()
                .WhenAnyAncestorNamed(ScopeName)
                .Named(ScopeName);
            Bind<IEventStore>().To<EFEventStore>()
                .WhenAnyAncestorNamed(ScopeName)
                .Named(ScopeName);
            Bind<Eventual.Common.Logging.ILogger>().To<Eventual.Common.Logging.DiagnosticsTraceSource.DiagnosticsTraceSourceLogger>()
                .WhenAnyAncestorNamed(ScopeName)
                .Named(ScopeName)
                .WithConstructorArgument("traceSourceName", "Rodes.FileKeeper");
            Bind<IEventsSerializer>().To<JsonEventsSerializer>()
                .WhenAnyAncestorNamed(ScopeName)
                .Named(ScopeName);
            Bind<IMetadataSerializer>().To<JsonMetadataSerializer>()
                .WhenAnyAncestorNamed(ScopeName)
                .Named(ScopeName);
            Bind<EventStoreDbContext>().ToMethod(context =>
            {
                return context.ContextPreservingGet<FileStorageDbContext>();
            })
                .WhenAnyAncestorNamed(ScopeName)
                .Named(ScopeName);
        }
    }
}
