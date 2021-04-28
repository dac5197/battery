using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.StatusModel
{
    public interface IStatusService
    {
        Task<Status> Add(Status status);
        Task Delete(int id);
        Task<List<Status>> Get();
        Task<List<Status>> Get(Battery battery);
        Task<Status> Get(int id);
        Task<Status> GetCompletedStatus(int batteryId);
        List<Status> GetDefaultValues();
        Task<Status> GetInitialStatus(int batteryId);
        Task<Status> Update(Status status);
    }
}