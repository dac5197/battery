using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.StatusModel
{
    public interface IStatusService
    {
        Task<Status> AddAsync(Status status);
        Task DeleteAsync(int id);
        Task<List<Status>> GetAsync();
        Task<List<Status>> GetAsync(Battery battery);
        Task<List<Status>> GetAsync(string userId);
        Task<Status> GetAsync(int id);
        Task<Status> GetCompletedStatusAsync(int batteryId);
        List<Status> GetDefaultValues();
        Task<Status> GetInitialStatusAsync(int batteryId);
        Task<Status> UpdateAsync(Status status);
    }
}