using BatteryApp.Models.ChargeModel;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public interface IInitializeChargeChildToParent
    {
        Task<Charge> SetRelationshipAsync(Charge parent, Charge child);
    }
}