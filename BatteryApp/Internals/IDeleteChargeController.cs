using BatteryApp.Models.ChargeModel;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public interface IDeleteChargeController
    {
        Task DeleteChargeAndAllRelatedItemsAsync(Charge charge);
        Task DeleteChargeOnlyAsync(Charge charge);
        Task DeleteChildrenAsync(Charge charge);
        Task DeleteTagRelationshipsAsync(int chargeId);
    }
}