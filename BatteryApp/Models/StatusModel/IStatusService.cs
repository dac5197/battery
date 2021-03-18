using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.StatusModel
{
    public interface IStatusService
    {
        Task<Status> Add(Status status);
        Task Delete(int id);
        Task<List<Status>> Get();
        Task<Status> Get(int id);
        Task<Status> Update(Status status);
    }
}