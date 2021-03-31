using BatteryApp.Models;
using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.NoteModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using BatteryApp.Models.TagModel;
using BatteryApp.Models.UserProfileModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BatteryApp.Data
{
    public class AppDbContextFactory : DbContext
    {
        public AppDbContextFactory(DbContextOptions<AppDbContextFactory> options)
        : base(options)
        {
        }

        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Charge> Charges { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ChargeTagRelation> ChargeTagRelations { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Note> Notes { get; set; }



        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                          cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                SetTimeStamps(entry, utcNow);
            }
        }

        private void SetTimeStamps(EntityEntry entry, DateTime utcNow)
        {
            // for entities that inherit from BaseEntity,
            // set UpdatedOn / CreatedOn appropriately
            if (entry.Entity is BaseEntity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        // set the updated date to "now"
                        trackable.Updated = utcNow;

                        // mark property as "don't touch"
                        // we don't want to update on a Modify operation
                        entry.Property("Created").IsModified = false;
                        break;

                    case EntityState.Added:
                        // set both updated and created date to "now"
                        trackable.Created = utcNow;
                        trackable.Updated = utcNow;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // JSON Conversion for EF Core
            // https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations
            modelBuilder.Entity<Note>()
                .Property(e => e.History)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, null),
                    v => JsonSerializer.Deserialize<List<HistoryJson>>(v, null),
                    new ValueComparer<IList<HistoryJson>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => (IList<HistoryJson>)c.ToList()));
        }      
    }
}
