using BatteryApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.UserProfileModel
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public UserProfileService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<UserProfile>> Get()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.UserProfiles.ToListAsync();
        }

        public async Task<UserProfile> Get(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var UserProfile = await context.UserProfiles.FindAsync(id);
            return UserProfile;
        }

        public async Task<UserProfile> Add(UserProfile UserProfile)
        {
            using var context = _contextFactory.CreateDbContext();
            context.UserProfiles.Add(UserProfile);
            await context.SaveChangesAsync();
            return UserProfile;
        }

        public async Task<UserProfile> Update(UserProfile UserProfile)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(UserProfile).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UserProfile;
        }

        public async Task Delete(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var UserProfile = await context.UserProfiles.FindAsync(id);
            context.UserProfiles.Remove(UserProfile);
            await context.SaveChangesAsync();
        }
    }
}
