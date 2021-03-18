using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.UserProfileModel
{
    public interface IUserProfileService
    {
        Task<UserProfile> Add(UserProfile UserProfile);
        Task Delete(string id);
        Task<List<UserProfile>> Get();
        Task<UserProfile> Get(string id);
        Task<UserProfile> Update(UserProfile UserProfile);
    }
}