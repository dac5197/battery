using BatteryApp.Models;
using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.StatusModel;
using BatteryApp.Models.UserProfileModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DbSet<Charge> Charges { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Battery> Batteries { get; set; }

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
        }
    }
}
