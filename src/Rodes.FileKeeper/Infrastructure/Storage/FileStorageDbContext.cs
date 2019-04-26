using Eventual.EventStore.Core;
using Eventual.EventStore.EntityFrameworkCore;
using Rodes.FileKeeper.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rodes.FileKeeper.Infrastructure.Storage
{
    public class FileStorageDbContext : EventStoreDbContext
    {
        #region Properties

        public DbSet<UploadSession> UploadSessions { get; set; }

        public DbSet<FileInformationIndex> FileInformationIndexes { get; set; }

        #endregion

        #region Constructors

        public FileStorageDbContext() : base() { }

        public FileStorageDbContext(DbContextOptions options) : base(options) { }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Check the model generated is right
            // TODO: Clean up this code

            //modelBuilder.Entity<UploadSession>()
            //    .HasMany(s => s.InnerFileDescriptions)
            //    .WithOne()
            //    .IsRequired()
            //    .HasForeignKey(e => e.UploadSessionId)
            //    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UploadSession>()
                .OwnsMany(s => s.InnerFileDescriptions);
            modelBuilder.Entity<UploadSession>().Ignore(e => e.Version);
            modelBuilder.Owned<OwnerId>();
            modelBuilder.Entity<UploadSession>().OwnsOne(e => e.OwnerId);
            //modelBuilder.Entity<UploadSession>()
            //    .Property(p => p.OwnerId)
            //    .IsRequired();

            modelBuilder.Entity<FileUploadDescription>().HasKey(e => new { e.UploadSessionId, e.Id });

            // This sets Id on FileUploadDescription as an alternate key 
            // (another option would be to set a unique index with required property)
            modelBuilder.Entity<FileUploadDescription>()
                .HasAlternateKey(f => f.Id);
            //modelBuilder.Entity<FileUploadDescription>()
            //    .Property(e => e.Id)
            //    .IsRequired()
            //    .HasColumnAnnotation(
            //        IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_FileId", 1) { IsUnique = true }));

            modelBuilder.Entity<FileUploadDescription>().Property(e => e.FileName).IsRequired();

            // TODO: Review the way employed to require properties on owned types
            modelBuilder.Owned<ContentType>();
            modelBuilder.Owned<ResourceUri>();
            modelBuilder.Entity<FileUploadDescription>()
                .OwnsOne(e => e.ContentType)
                .Property(p => p.Type).IsRequired();
            modelBuilder.Entity<FileUploadDescription>()
                .OwnsOne(e => e.AssignedUri);
            //modelBuilder.ComplexType<ContentType>().Property(e => e.Type).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
