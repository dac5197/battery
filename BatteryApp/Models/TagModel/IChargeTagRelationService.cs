using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.TagModel
{
    public interface IChargeTagRelationService
    {
        Task<ChargeTagRelation> AddAsync(ChargeTagRelation relation);
        Task<ChargeTagRelation> AddAsync(int chargeId, int tagId);
        Task DeleteAsync(int id);
        Task DeleteAsync(int chargeId, int tagId);
        Task<List<ChargeTagRelation>> GetAsync();
        Task<ChargeTagRelation> GetAsync(int id);
        Task<ChargeTagRelation> GetAsync(int chargeId, int tagId);
        Task<List<ChargeTagRelation>> GetAllRelationsForTagAsync(int tagId);
        Task<List<ChargeTagRelation>> GetAllRelationsForChargeAsync(int chargeId);
        Task<ChargeTagRelation> UpdateAsync(ChargeTagRelation relation);
        Task<ChargeTagRelation> UpdateAsync(int chargeId, int tagId);
    }
}