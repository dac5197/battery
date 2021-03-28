using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.TagModel
{
    public interface IChargeTagRelationService
    {
        Task<ChargeTagRelation> Add(ChargeTagRelation relation);
        Task<ChargeTagRelation> Add(int chargeId, int tagId);
        Task Delete(int id);
        Task Delete(int chargeId, int tagId);
        Task<List<ChargeTagRelation>> Get();
        Task<ChargeTagRelation> Get(int id);
        Task<ChargeTagRelation> Get(int chargeId, int tagId);
        Task<List<ChargeTagRelation>> GetAllRelationsForTag(int tagId);
        Task<List<ChargeTagRelation>> GetAllRelationsForCharge(int chargeId);
        Task<ChargeTagRelation> Update(ChargeTagRelation relation);
        Task<ChargeTagRelation> Update(int chargeId, int tagId);
    }
}